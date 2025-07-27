using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using FmsSystemMenu.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using System.IO.Compression;

namespace FmsSystemMenu.Additional_Libraries
{
    public class FaultHistoryDatabaseServices
    {
        public static bool DatabaseOperationDue;
        public static bool FileError;
        public static bool ZipIt;
        static bool Zipped;
        static bool NoFile;
        static bool Cancel;
        static bool DatabaseOpened;
        static bool DatabaseEmpty;
        static int DatabaseSize;
        static int LastRecordOnSystem;
        static int LastRecordOnUsb;
        static int UsbDriveFreeSpace;
        static long DaysUntilDue;
        static long DaysSinceLastExport;
        static long DaysSinceLastImport;
        static string Space;
        static string SqlQuery;
        static string Argument;
        static string LabelCaption;
        public static string MessageBoxTitle;
        public static string MessageCaption;
        static DateTime UsbLastModified;
        static DateTime SystemLastModified;

        static DAO.Workspace WrkJet;
        static DAO.Recordset RstFaults;
        static DAO.Database DbsFHDB;
        public static DAO.DBEngine DAODBEngine_definst = new DAO.DBEngine();

        #region PubliclVariables
        public static bool ZipToDrive;
        public static bool UnLoad;
        public static int LastSystemRecord;
        public static int DataDumpInterval;
        public static int LastRestoredRecord;
        public static string TempDatabaseFileName;
        public static string DatabaseDirectoryPath;
        public static string BinaryFilePath;
        public static string DatabaseFileName = string.Empty;
        public static string FHDBFilePath;
        public static string BackSlash = string.Empty;
        public static string TestItFile;
        public static DateTime ExportDate;
        public static DateTime ImportDate;
        #endregion

        #region Constants
        const int MinimumUddSize = 200000;
        public const string TestZipFile = "TestIt.zip";
        public const string BinaryFile = "xcdsfx32.bin";
        const string DatabaseFaultTable = "FAULTS";
        const string Import = "IMPORT";
        const string Export = "EXPORT";
        const string None = "NONE";
        const string TempFile = "FHDB_TEMP.mdb";
        const string UddTerminate = "\n \n \nThe UDD Utility will terminate in .......";
        const string LaunchImporter = "The FHDB Importer will automatically Launch.\n";
        const string LaunchExporter = "The FHDB Exporter will automatically Launch.\n";
        const string PriorToUdd = "and must be completed prior to creating a UDD.\n";
        const string ExportDue = "The FHDB Database file size exceeds the \n" + "Free Space Available on the USB Disk.\n" +
            "The FHDB Export Operation must be completed prior to creating a UDD.\n";
        const string ImportPastDue = "The FHDB Import Operation is past due.\n";
        const string UddRecent = "The Database from the UDD is the most recent.";
        const string SystemRecent = "The System Database is most recent";
        const string DatabaseEqual = "The Databases have been compared and found to be equal.";
        #endregion

        /// <summary>Changes the size of the database field.</summary>
        /// <param name="DatabaseName">Name of the database.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="FieldName">Name of the field.</param>
        /// <param name="FieldSize">Size of the field.</param>
        public static void ChangeDatabaseFieldSize(string DatabaseName, string TableName, string FieldName, int FieldSize)
        {
            string sql = "ALTER TABLE " + TableName + " ALTER COLUMN " + FieldName + " TEXT(" + FieldSize + ")";

            try
            {
                WrkJet = DAODBEngine_definst.Workspaces[0];
                DbsFHDB = WrkJet.OpenDatabase(DatabaseName);
                DbsFHDB.Execute(sql);

                DbsFHDB.Close();
                WrkJet.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>If File will fit on USB Disk, Copy it. If it Will Not Fit,
        ///Test it as a Self Extracting Zip File. If it fits, Copy it as a
        ///    Zipped File. If it still won't fit, Force an Import/Export
        ///    Operation and Abort this Operation.                       
        ///    Delays are added to allow the Operator time to read Comments
        ///</summary>
        public static void CheckDatabaseFileSize()
        {
            Windows.UddWizard uddWizard = new Windows.UddWizard();
            try
            {
                //Initialize variables
                UsbDriveFreeSpace = 0;
                DatabaseSize = 0;
                FileError = false;
                ZipToDrive = true;

                ShowFreeSpace(CommonUtilities.UsbDiskDriveLetter);

                if (Cancel)
                {
                    return;
                }

                ShowFileSize(DatabaseFileName);

                if (FileError)
                {
                    return;
                }

                if (MinimumUddSize < UsbDriveFreeSpace)
                {
                    UsbDriveFreeSpace = MinimumUddSize;
                }

                if (DatabaseSize < UsbDriveFreeSpace)
                {
                    DatabaseOperationDue = true;
                    uddWizard.TxtBoxInstructions.Text = "";
                    uddWizard.TxtBoxTitle.Text = "Copying Database";
                    if (CommonUtilities.UsbFlag == true)
                    {
                        uddWizard.TxtBoxInstructions.Text = "Copying Database file to the USB Disk.........";
                    }

                    File.Copy(DatabaseFileName, CommonUtilities.FhdbUsbFileName, true);

                    if (CommonUtilities.UsbFlag == true)
                    {
                        uddWizard.TxtBoxInstructions.Text =
                            "\n\n  The Database file has been successfully \n copied to the USB Disk.";

                    }


                    CommonUtilities.Success = true;
                    return;
                }

                //********** Database File will not fit on USB, Will it fit if Zipped? **********
                FindZippedFileSize();

                if (ZipToDrive == true)
                {
                    ZipIt = true;
                    return;
                }

                else
                {
                    Argument = CheckImportExportDates();

                    switch (Argument)
                    {
                        case None:
                            Argument = Export;
                            return;
                        case Export:
                            LabelCaption = ExportDue + LaunchExporter + UddTerminate;
                            return;
                        case Import:
                            LabelCaption = ImportPastDue + PriorToUdd + LaunchImporter + UddTerminate;
                            return;
                        default:
                            break;
                    }

                    LaunchFHDB(Argument);
                    FmsSystemMenu.MainWindow systemMenu = new MainWindow();
                    systemMenu.WindowState = WindowState.Minimized;
                    UnLoad = true;
                    uddWizard.Hide();
                }

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>Function to find File Extension, True if Zipped
        ///       Checks for which database file type exists on USB, if any  
        /// </summary>
        /// <returns>if zip file exists</returns>
        public static bool CheckForZipFileExists()
        {
            NoFile = false;
            bool zippedFileExists = false;

            if (File.Exists(CommonUtilities.FhdbSelfExtractingFileName))
            {
                zippedFileExists = true;
            }
            else if (File.Exists(CommonUtilities.FhdbUsbFileName))
            {
                zippedFileExists = false;
            }
            else
            {
                MessageBox.Show("The Database file can not be found on the USB Disk", "File Error", MessageBoxButtons.OK);
                NoFile = true;
            }

            return zippedFileExists;
        }

        /// <summary>Calculates Import/Export info. Evaluates data and returns either
        ///    ("IMPORT", "EXPORT", or "NONE") depending on what Operation is 
        ///    due. Also Calculates the days left until the next Export is due.
        /// </summary>
        /// <returns>the import/export date</returns>
        public static string CheckImportExportDates()
        {
            DateTime nextImportExport;
            string returnString = string.Empty;
            bool importExport;

            try
            {
                if (ImportDate.ToOADate() > ExportDate.ToOADate())
                {
                    importExport = true;
                }
                else
                {
                    importExport = false;
                }

                if (importExport == true)
                {
                    nextImportExport = Microsoft.VisualBasic.DateAndTime.DateAdd("d", DataDumpInterval, ExportDate);

                    DaysUntilDue = Microsoft.VisualBasic.DateAndTime.DateDiff("d", ExportDate, DateTime.Now);
                    DaysSinceLastExport = Microsoft.VisualBasic.DateAndTime.DateDiff("d", ExportDate, DateTime.Now);
                    DaysSinceLastImport = Microsoft.VisualBasic.DateAndTime.DateDiff("d", ImportDate, DateTime.Now);

                    if (DateTime.Now.ToOADate() >= nextImportExport.ToOADate())
                    {
                        returnString = Export;
                    }
                    else
                    {
                        returnString = None;
                    }
                }

                else
                {
                    returnString = Import;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            return returnString;
        }

        /// <summary>Open database, find last record number and return it. </summary>
        /// <param name="Database">The database.</param>
        /// <returns>the last record in the db</returns>
        public static int CheckLastRecord(string Database)
        {
            int checkLastRecord = 0;
            try
            {
                SqlQuery = "SELECT * FROM FAULTS ORDER BY Record_Identifier";

                DatabaseOpened = true;

                WrkJet = (DAO.Workspace)DAODBEngine_definst.Workspaces[0];
                DbsFHDB = WrkJet.OpenDatabase(Database);
                RstFaults = DbsFHDB.OpenRecordset(SqlQuery, DAO.RecordsetTypeEnum.dbOpenDynaset);

                if (RstFaults.EOF == false)
                {
                    RstFaults.MoveLast();
                }

                //.CheckLastRecord = rstFaults.Fields("Record_Identifier").Value 'Set Last Record Number value
                checkLastRecord = (int)RstFaults.Fields["Record_Identifier"].Value;

                CloseDatabase();
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            return checkLastRecord;
        }

        /// <summary>Close the Database connection and unload .</summary>
        public static void CloseDatabase()
        {
            try
            {
                if (DatabaseOpened == true)
                {
                    RstFaults.Close();
                    DbsFHDB.Close();
                    WrkJet.Close();
                    DatabaseOpened = false;
                }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>Initialize the Copy or Restore Operation. </summary>
        public static void CopyRestoreDatabase()
        {
            //UddWizard uDD = new UddWizard();
            CommonUtilities.Success = false;

            if (ZipIt == true)
            {
                //uDD.TxtBoxTitle.Text = "Zipping Database File";
                TestZipIt();

                if (CommonUtilities.Success == true)
                {
                    UniqueDataDiskServices.TestFileIntegrity();
                }
                ZipIt = false;
            }

            //uDD.TxtBoxTitle.Text = "Zipping Database File";
            //uDD.TxtBoxInstructions.Text = "Retrieving File Information........";

            //UniqueDataDiskServices.AddMessage("Retrieving File Information........", uDD.TxtBoxTitle.Text);
            Zipped = CheckForZipFileExists();

            if (NoFile == false)
            {
                if (Zipped == true)
                {
                    RestoreZippedDB();
                    RestoreMostRecentDatabase();
                }
                else
                {
                    GetDatabaseFileInfo();
                }
            }
        }

        /// <summary>Parse off file name to find the Directory Path</summary>
        /// <param name="StringToBeParsed">The string to be parsed.</param>
        /// <returns>the path</returns>
        public static string FindPath(string StringToBeParsed)
        {
            int x;
            string path;

            for (x = StringToBeParsed.Length; x == 1; x--)
            {
                if (StringToBeParsed.Substring(x, 1) == "\\")
                {
                    break;
                }
            }
            path = StringToBeParsed.Substring(0, x - 1);
            return path;
        }

        /// <summary>Zips Database to a Temp File then Checks the file size.            ***
        ///    The Temp file is deleted after the file size is found.
        /// </summary>
        public static void FindZippedFileSize()
        {
            UddWizard wizard = new UddWizard();
            wizard.TxtBoxInstructions.Text = "Testing Zipped File Size";

            //Create Temp Zip File
            TestZipIt();
            ShowFileSize(TestItFile); //'Find the size of the Zipped FHDB Database file
            File.Delete(TestItFile); //'Delete the Temp file
        }

        /// <summary>Gets the database fields.</summary>
        /// <param name="DatabaseName">Name of the database.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static List<String> GetDatabaseFields(string DatabaseName, string TableName)
        {
            List<String> fields = new List<String>();

            try
            {
                WrkJet = DAODBEngine_definst.Workspaces[0];
                DbsFHDB = WrkJet.OpenDatabase(DatabaseName);
                for (int fieldNo = 0; fieldNo < DbsFHDB.TableDefs[TableName].Fields.Count - 1; fieldNo++)
                {
                    fields.Add(DbsFHDB.TableDefs[TableName].Fields[fieldNo].Name);
                }

                DbsFHDB.Close();
                WrkJet.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return fields;
        }

        /// <summary>Gets the database file information.</summary>
        public static void GetDatabaseFileInfo()
        {
            UddWizard wizard = new UddWizard();
            List<string> fieldList = new List<string> { };
            string oldFieldName = "ERO";
            string newFieldName = "Service_Request_No";
            string tableName = "FAULTS";
            int fieldSize = 15;
            try
            {
                OpenDatabase();

                if (DatabaseEmpty == true)
                {
                    LastRecordOnSystem = 0;
                }
                else
                {
                    LastRecordOnSystem = (int)RstFaults.Fields["Record_Identifier"].Value;
                }

                CloseDatabase();

                SqlQuery = "SELECT * FROM FAULTS ORDER BY Record_Identifier";

                DatabaseOpened = true;
                WrkJet = DAODBEngine_definst.Workspaces[0];
                DbsFHDB = WrkJet.OpenDatabase(CommonUtilities.FhdbUsbFileName);
                RstFaults = DbsFHDB.OpenRecordset(SqlQuery, DAO.RecordsetTypeEnum.dbOpenDynaset);

                if (RstFaults.BOF)
                {
                    LastRecordOnUsb = 0;
                }
                else
                {
                    RstFaults.MoveLast();
                    LastRecordOnUsb = (int)RstFaults.Fields["Record_Identifier"].Value;
                }

                CloseDatabase();

                UsbLastModified = ShowFileAccessInfo(CommonUtilities.FhdbUsbFileName);
                SystemLastModified = ShowFileAccessInfo(DatabaseFileName);

                if (LastRecordOnUsb < LastRecordOnSystem)
                {
                    MessageBoxTitle = "No Action Necessary";
                    MessageCaption = SystemRecent + "\nThe FHDB Database is obsolete and was not installed on System.";

                    wizard.AddMessageToForm(MessageBoxTitle, MessageCaption);
                }
                else if (LastRecordOnUsb == LastRecordOnSystem)
                {
                    if (UsbLastModified.ToOADate() > SystemLastModified.ToOADate())
                    {
                        //wizard.TxtBoxTitle.Text = "";
                        MessageBoxTitle = "Copying Database";
                        MessageCaption = UddRecent + "\n The FHDB Database has been Restored on the System.";
                        wizard.AddMessageToForm(MessageBoxTitle, MessageCaption);
                        File.Copy(CommonUtilities.FhdbUsbFileName, DatabaseFileName, true);
                        CommonUtilities.Success = true;
                    }
                    else
                    {
                        MessageBoxTitle = "No Action Necessary";
                        MessageCaption = DatabaseEqual + "\nThe FHDB Database was NOT installed on System.";
                        wizard.AddMessageToForm(MessageBoxTitle, MessageCaption);
                        CommonUtilities.Success = true;
                        //CommonUtilities.Delay(0.5);
                    }
                }
                else
                {
                    MessageBoxTitle = "Copying Database";
                    MessageCaption = "Copying Database.............";
                    wizard.AddMessageToForm(MessageBoxTitle, MessageCaption);
                    File.Copy(CommonUtilities.FhdbUsbFileName, DatabaseFileName, true);

                    fieldList = GetDatabaseFields(DatabaseFileName, tableName);
                    if (fieldList[3] == oldFieldName)
                    {
                        RenameDatabaseField(DatabaseFileName, tableName, oldFieldName, newFieldName);
                        ChangeDatabaseFieldSize(DatabaseFileName, tableName, newFieldName, fieldSize);
                    }

                    MessageBoxTitle = "Database Copied";
                    MessageCaption = UddRecent + "\n The System Database is obsolete and has been replaced.";
                    wizard.AddMessageToForm(MessageBoxTitle, MessageCaption);
                    CommonUtilities.Success = true;
                }

                //wizard.TxtBoxTitle.Text = "";
                MessageCaption = "The last Record Number on the UDD FHDB is: " + LastRecordOnUsb + "\nThe last Record Number on the System FHDB is: "
                    + LastRecordOnSystem + "\n";
                wizard.AddMessageToForm(MessageBoxTitle, MessageCaption);

                CommonUtilities.Success = true;

                //CommonUtilities.Delay(1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public static bool InitializeReferenceFiles()
        {
            string exportTime;
            string dataDumpInterval;
            string importTime;
            double exportDate = 0;
            double importDate = 0;
            bool initReferenceFiles = false;
            BackSlash = @"\";
            DatabaseFileName = ProcessIniFiles.ReadIniFile("File Locations", "FHDB_DATABASE", "");

            if (DatabaseFileName.Trim().Length > 0)
            {
                DatabaseDirectoryPath = FindPath(DatabaseFileName);
                TempDatabaseFileName = DatabaseDirectoryPath + BackSlash + TempFile;
                BinaryFilePath = DatabaseDirectoryPath + BackSlash + BinaryFile;
                TestItFile = DatabaseDirectoryPath + BackSlash + TestZipFile;
                FHDBFilePath = ProcessIniFiles.ReadIniFile("File Locations", "FHDB_PROCESSOR", "");
                exportTime = ProcessIniFiles.ReadIniFile("FHDB", "EXPORT_TIME", "");
                dataDumpInterval = ProcessIniFiles.ReadIniFile("FHDB", "DDI", "");
                importTime = ProcessIniFiles.ReadIniFile("FHDB", "IMPORT_TIME", "");
                try
                {

                    exportDate = double.Parse(exportTime);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n\n Information missing in the FHDB section of ATS.ini");
                }

                try
                {
                    importDate = double.Parse(importTime);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\n\n Information missing in the FHDB section of ATS.ini");
                }


                if (exportTime.Length > 1)
                {
                    ExportDate = DateTime.FromOADate(exportDate);
                    initReferenceFiles = true;
                }

                if (dataDumpInterval.Length > 0)
                {
                    DataDumpInterval = Convert.ToInt32(dataDumpInterval);
                }
                if (importTime.Length > 1)
                {
                    ImportDate = DateTime.FromOADate(importDate);
                    initReferenceFiles = true;
                }
            }
            else
            {
                initReferenceFiles = false;
            }

            return initReferenceFiles;
        }
        /// <summary>Gets the FHDB Processor file path, checks for file existance,
        ///    the launches FHDB.exe with the appropreate Argument.
        ///</summary>
        /// <param name="Argument">The Argument.</param>
        public static void LaunchFHDB(string Argument)
        {
            UddWizard wizard = new UddWizard();
            DatabaseOperationDue = true;
            wizard.TxtBoxTitle.Text = "UDD will terminate.";
            wizard.TxtBoxInstructions.Text = LabelCaption;
            wizard.TxtBoxCountDown.Visibility = System.Windows.Visibility.Visible;
            ShowCountdown();

            if (Argument.Length > 0)
            {
                Space = " ";
            }
            else
            {
                Space = "";
            }

            if (File.Exists(FHDBFilePath))
            {
                Process.Start(FHDBFilePath + Space + Argument);
            }
            else
            {
                MessageBox.Show("File Not Found: " + FHDBFilePath, "FHDB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>Opens the database.</summary>
        public static void OpenDatabase()
        {
            string sqlQuery;
            DatabaseFileName = ProcessIniFiles.ReadIniFile("File Locations", "FHDB_DATABASE", "");

            try
            {
                DatabaseEmpty = false;
                sqlQuery = "SELECT * FROM " + DatabaseFaultTable + " ORDER BY Record_Identifier";
                DatabaseOpened = true;

                //open db and fill recordset
                WrkJet = DAODBEngine_definst.Workspaces[0];
                DbsFHDB = WrkJet.OpenDatabase(DatabaseFileName);
                RstFaults = DbsFHDB.OpenRecordset(sqlQuery, DAO.RecordsetTypeEnum.dbOpenDynaset);

                if (RstFaults.BOF)
                {
                    DatabaseEmpty = true;
                    return;
                }
                else
                {
                    RstFaults.MoveLast();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>Renames the database field.</summary>
        /// <param name="DatabaseName">Name of the database.</param>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="OldFieldName">Old name of the field.</param>
        /// <param name="NewFieldName">New name of the field.</param>
        public static void RenameDatabaseField(string DatabaseName, string TableName, string OldFieldName, string NewFieldName)
        {
            try
            {
                WrkJet = DAODBEngine_definst.Workspaces[0];
                DbsFHDB = WrkJet.OpenDatabase(DatabaseName);

                for (int fieldNo = 0; fieldNo < DbsFHDB.TableDefs[TableName].Fields.Count - 1; fieldNo++)
                {
                    if (DbsFHDB.TableDefs[TableName].Fields[fieldNo].Name == OldFieldName)
                    {
                        DbsFHDB.TableDefs[TableName].Fields[fieldNo].Name = NewFieldName;
                    }
                }

                DbsFHDB.Close();
                WrkJet.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>Deletes obsolete Database and renames database if necessary.
        ///   When extracting the Zipped file it extracts as FHDB.mdb 
        ///   The System File has to be renamed to FHDB_TEMP.mdb prior
        ///   to extracting the Zipped file. 
        ///   This is necessary in order to keep the System Database intact.
        /// </summary>
        public static void RestoreMostRecentDatabase()
        {
            List<string> fieldList = new List<string> { };
            string oldFieldName = "ERO";
            string newFieldName = "Service_Request_No";
            string tableName = "FAULTS";
            int fieldSize = 15;

            UsbLastModified = ShowFileAccessInfo(DatabaseDirectoryPath + "\\" + TempFile);

            SystemLastModified = ShowFileAccessInfo(DatabaseFileName);

            if (LastSystemRecord > LastRestoredRecord)
            {
                File.Delete(DatabaseFileName);

                File.Move(TempDatabaseFileName, DatabaseFileName);
                UniqueDataDiskServices.AddMessage("\n\n" + SystemRecent + "\nThe FHDB Database on the UDD is obsolete and was not installed on System.", "");
                CommonUtilities.Success = true;
            }
            else if (LastSystemRecord == LastRestoredRecord)
            {
                if (UsbLastModified.ToOADate() > SystemLastModified.ToOADate())
                {
                    UniqueDataDiskServices.AddMessage("\n\n" + UddRecent + "\nThe FHDB Database has been Restored on the System.", "");
                    CommonUtilities.Success = true;
                }
                else
                {
                    UniqueDataDiskServices.AddMessage("\n" + DatabaseEqual + "\nThe FHDB Database will NOT be Restored to the System.", "");
                    CommonUtilities.Success = true;
                    File.Copy(TempDatabaseFileName, DatabaseFileName, true);
                    File.Delete(TempDatabaseFileName);
                }
            }
            else
            {
                File.Delete(TempDatabaseFileName);
                UniqueDataDiskServices.AddMessage("\n\n" + UddRecent + "\nThe FHDB Database has been Restored on the System.", "");
                CommonUtilities.Success = true;
            }

            fieldList = GetDatabaseFields(DatabaseFileName, tableName);
            if (fieldList[3] == oldFieldName)
            {
                RenameDatabaseField(DatabaseFileName, tableName, oldFieldName, newFieldName);
                ChangeDatabaseFieldSize(DatabaseFileName, tableName, newFieldName, fieldSize);
            }

            UniqueDataDiskServices.AddMessage("\nClick [Next] to continue.", "");
        }

        /// <summary>Runs a 5 Second Countdown with read out at bottom of form. </summary>
        public static void ShowCountdown()
        {
            UsbLastModified = ShowFileAccessInfo(DatabaseDirectoryPath + "\\" + TempFile);

            SystemLastModified = ShowFileAccessInfo(DatabaseFileName);

            if (LastSystemRecord > LastRestoredRecord)
            {
                File.Delete(DatabaseFileName);

                System.IO.File.Move(TempDatabaseFileName, DatabaseFileName);
                UniqueDataDiskServices.AddMessage("\n\n" + SystemRecent + "\nThe FHDB Database on the UDD is obsolete and was not installed on System.", "");
                CommonUtilities.Success = true;
            }
            else if (LastSystemRecord == LastRestoredRecord)
            {
                if (UsbLastModified.ToOADate() > SystemLastModified.ToOADate())
                {
                    UniqueDataDiskServices.AddMessage("\n\n" + UddRecent + "\nThe FHDB Database has been Restored on the System.", "");
                    CommonUtilities.Success = true;
                }
                else
                {
                    UniqueDataDiskServices.AddMessage("\n" + DatabaseEqual + "\nThe FHDB Database will NOT be Restored to the System.", "");
                    CommonUtilities.Success = true;
                    File.Copy(TempDatabaseFileName, DatabaseFileName);
                    File.Delete(TempDatabaseFileName);
                }
            }
            else
            {
                File.Delete(TempDatabaseFileName);
                UniqueDataDiskServices.AddMessage("\n\n" + UddRecent + "\nThe FHDB Database has been Restored on the System.", "");
                CommonUtilities.Success = true;
            }

            UniqueDataDiskServices.AddMessage("\nClick [Next] to continue.", "");
        }

        /// <summary>
        /// Restore the zipped copy of the FHDB Database to the VIPERT System Destination: 
        /// "C:\Program Files\VIPERT\FHDB\FHDB.mdb" Before Restoring, the Zipped file will be checked for file integrit
        /// Unzip Database and Install in correct Directory, Overwrite if file exists
        /// </summary>
        public static void RestoreZippedDB()
        {
            Windows.UddWizard wizard = new UddWizard();
            CommonUtilities.Success = true;
            LastSystemRecord = CheckLastRecord(DatabaseFileName);

            if (System.IO.File.Exists(TempDatabaseFileName))
            {
                File.Delete(TempDatabaseFileName);
            }

            Microsoft.VisualBasic.FileSystem.Rename(DatabaseFileName, TempDatabaseFileName);

            UniqueDataDiskServices.AddMessage("\nBefore Installing the FHDB Databse on the System. \n The Zipped file will be checked for file integrity.", "");
            //CommonUtilities.Delay(2);
            FileInfo fileToDecompress = new FileInfo(CommonUtilities.FhdbSelfExtractingFileName);

            wizard.TxtBoxInstructions.Text = "";
            wizard.TxtBoxTitle.Text = "Extracting Database";
            UniqueDataDiskServices.AddMessage("Extracting Zipped Files ......... ", "");

            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = Path.GetFileName(currentFileName);
                using (FileStream decompressedFileStream = File.Create(DatabaseDirectoryPath + "\\" + newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        originalFileStream.CopyTo(decompressedFileStream);
                    }
                }
                using (ZipArchive archive = ZipFile.OpenRead(DatabaseDirectoryPath + "\\" + newFileName))
                {
                    archive.ExtractToDirectory(DatabaseDirectoryPath);
                }
                File.Delete(DatabaseDirectoryPath + "\\" + newFileName);
            }

            CommonUtilities.Success = false;
            LastRestoredRecord = CheckLastRecord(DatabaseFileName);

            wizard.TxtBoxInstructions.Text = "";
            wizard.TxtBoxTitle.Text = "Comparing Database Files";
            wizard.TxtBoxTitle.Text += "The Last Record Number on the System is : " + LastSystemRecord;
            wizard.TxtBoxTitle.Text += "The Last Record Number Restored is : " + LastRestoredRecord;


            //UniqueDataDiskServices.AddMessage("The Last Record Number on the System is : " + LastSystemRecord, wizard.TxtBoxTitle.Text);
            //         UniqueDataDiskServices.AddMessage("The Last Record Number Restored is : " + LastRestoredRecord, wizard.TxtBoxTitle.Text);
            //CommonUtilities.Delay(2);


        }


        /// <summary>Get the files Last date modified and return date. </summary>
        /// <param name="FileSpec">The file spec.</param>
        /// <returns>Date last modified</returns>
        public static DateTime ShowFileAccessInfo(string FileSpec)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(FileSpec);

            return file.LastWriteTime;
        }

        /// <summary>Finds the File Size of the FHDB Database and returns Results
        ///    File Size formated in Killo Bytes.  
        /// </summary>
        /// <param name="FileSpec">The file spec.</param>
        public static void ShowFileSize(string FileSpec)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(FileSpec);

            try
            {
                FileError = !(file.Exists);

                DatabaseSize = (int)(file.Length / 1024);

                if (UsbDriveFreeSpace > DatabaseSize)
                {
                    ZipToDrive = true;
                }
                else
                {
                    ZipToDrive = false;
                }

                Debug.WriteDebugInfo("Database size: " + DatabaseSize);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FileError = true;
            }
        }

        /// <summary>Finds the Free Space left on the USB Disk.  Results are
        ///    returned formated in Killo Bytes.
        /// </summary>
        /// <param name="DriverPath">The driver path.</param>
        public static void ShowFreeSpace(string DriverPath)
        {
            System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(DriverPath);

            DialogResult response = new DialogResult(); //User's Response to Message Box
            do
            {
                try
                {
                    UsbDriveFreeSpace = (int)(driveInfo.AvailableFreeSpace / 1024);

                    Debug.WriteDebugInfo("Free space on " + CommonUtilities.UsbDiskDriveLetter + ": " + UsbDriveFreeSpace);
                    Cancel = false;
                }
                catch (Exception ex)
                {
                    response = System.Windows.Forms.MessageBox.Show("Disk Not Ready" + "\n" + "Would you like to try again?", "Disk not Ready", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                }
            } while (response == DialogResult.Retry);
        }

        /// <summary>Tests the zip file.</summary>
        public static void TestZipIt()
        {
            try
            {
                using (FileStream zipToOpen = new FileStream(FaultHistoryDatabaseServices.TestItFile, FileMode.CreateNew))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(FaultHistoryDatabaseServices.DatabaseFileName, Path.GetFileName(FaultHistoryDatabaseServices.DatabaseFileName));
                    }
                }
                CommonUtilities.Success = true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "ERROR", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                CommonUtilities.Success = false;
            }
        }
    }
}
