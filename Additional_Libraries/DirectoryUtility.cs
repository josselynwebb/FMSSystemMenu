using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FmsSystemMenu.Additional_Libraries
{
    public class DirectoryUtility
    {
        public static List<string> TpsPaths = new List<string>();
        public static List<string> TpsList = new List<string>();
        public static string[] Tps = { };
        public static string PathToJsonFiles = @"C:\Users\Public\Documents\ATS\";
        public static string serialNumber = string.Empty;

        public static string TpsDirectory = string.Empty;

        /// <summary>Browses the folders.</summary>
        /// <param name="hwdOwner">The HWD owner.</param>
        /// <param name="message">The message.</param>
        /// <param name="browse">The browse.</param>
        /// <param name="rootFolder">The root folder.</param>
        /// <returns>the folder</returns>
        public static string BrowseFolders(int hwdOwner, string message, Folders.BrowseType browse, int rootFolder)
        {
            string path = string.Empty;
            string folder = string.Empty;

            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                folder = dlg.SelectedPath;
            }
            return folder;
        }

        /// <summary>Detects if  a usb drive has been detected</summary>
        public static bool DetectUsbDrive()
        {
            //MessageBox.Show("In detect usb");

            DialogResult dialogResult = new DialogResult();
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            CommonUtilities.UsbFlag = false;
            CommonUtilities.HardDriveFlag = false;
            serialNumber = CommonUtilities.GetSerialNumber();

            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "Click on the UDD Drive below...";

            dialog.ShowNewFolderButton = false;

            string path;
            SelectDrive:
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
                path = path.Substring(0, 3);
                switch (path)
                {
                    case "C:\\":
                        MessageBox.Show("You have selected the C drive. You must select a usb drive.");
                        CommonUtilities.HardDriveFlag = true;
                        CommonUtilities.UsbFlag = false;
                        break;
                    case "D:\\":
                        MessageBox.Show("You have selected the D drive. You must select a usb drive.");
                        CommonUtilities.HardDriveFlag = true;
                        CommonUtilities.UsbFlag = false;
                        break;
                    case "E:\\":
                        MessageBox.Show("You have selected the E drive. You must select a usb drive.");
                        CommonUtilities.HardDriveFlag = true;
                        CommonUtilities.UsbFlag = false;
                        break;
                    default:
                        CommonUtilities.UsbDiskDriveLetter = path;
                        CommonUtilities.Drive = 6;
                        CommonUtilities.UsbFlag = true;
                        CommonUtilities.HardDriveFlag = false;

                        CommonUtilities.FhdbSelfExtractingFileName = CommonUtilities.UsbDiskDriveLetter + @"\FHDB_Database.zip";
                        CommonUtilities.FhdbUsbFileName = CommonUtilities.UsbDiskDriveLetter + @"\FHDB.mdb";
                        break;
                }

            }

            if (CommonUtilities.HardDriveFlag == true)
            {
                goto SelectDrive;
            }

            return CommonUtilities.UsbFlag;


        }

        public static List<string> GetAtlasTpsName(string Path)
        {
            List<string> tpsNameList = new List<string>();
            string numOfTps = ProcessIniFiles.ReadIniFile(Path, "TPS", "TPS_COUNT");
            string description = string.Empty;
            string pn = String.Empty;
            int tpsCount = 0;
            bool success = Int32.TryParse(numOfTps.ToString(), out tpsCount);
            for (int i = 1; i <= tpsCount; i++)
            {
                description = ProcessIniFiles.ReadIniFile(Path, "TP" + i, "DESCR");
                pn = ProcessIniFiles.ReadIniFile(Path, "TP" + i, "PN");
                tpsNameList.Add(description.ToString() + " " + pn.ToString());
            }

            return tpsNameList;
        }

        /// <summary>Gets the udd hard drive path.</summary>
        /// <returns>the path to the UDD hard drive</returns>
        public static string GetUddHardDrivePath()
        {
            int hWnd = 0;

            string uddPath = BrowseFolders(hWnd, "Select Folder", Folders.BrowseType.BrowseForFolders, Folders.CSIDL_DESKTOP);

            if (uddPath == "")
            {
                MessageBox.Show("UDD Path not found.", "UDD Path", MessageBoxButtons.OK);
            }

            return uddPath;
        }

        /// <summary>Gets the ietm files.</summary>
        /// <param name="FilePath">The file path.</param>
        /// <returns>Path to the IETM</returns>
        public string GetIetmFiles(string FilePath)
        {
            string directoriesInPath = "";
            string dirName;

            if (Directory.Exists(FilePath))
            {
                dirName = Path.GetFileName(FilePath);
                do
                {
                    if (dirName.Length > 3)
                    {
                        if (FileSystem.GetAttr(FilePath + dirName) != FileAttribute.Directory)
                        {
                            if (dirName.ToUpper().Substring(dirName.Length - 3, 4) == ".IDE" ||
                                dirName.ToUpper().Substring(dirName.Length - 3, 4) == ".SGM" ||
                                dirName.ToUpper().Substring(dirName.Length - 3, 4) == ".XML")
                            {
                                directoriesInPath += dirName + ",";
                            }
                        }
                    }
                    dirName = Path.GetFileName(FilePath);
                } while (dirName != "");

                //Strip Comma Dileneator
                if (directoriesInPath != "")
                {
                    if (directoriesInPath.Substring(1, directoriesInPath.Length) == ",")
                    {
                        directoriesInPath = directoriesInPath.Substring(1, directoriesInPath.Length - 1);
                    }
                }
            }
            return directoriesInPath;
        }

        public static List<string> GetAtlasTpsExePath(string Path)
        {
            List<string> tpsPath = new List<string>();
            string numOfTps = ProcessIniFiles.ReadIniFile(Path, "TPS", "TPS_COUNT");
            string name = ProcessIniFiles.ReadIniFile(Path, "TPS", "NAME");
            string pathToTps = String.Empty;
            string exe = String.Empty;
            int tpsCount = 0;
            bool success = Int32.TryParse(numOfTps.ToString(), out tpsCount);
            for (int i = 1; i <= tpsCount; i++)
            {
                pathToTps = ProcessIniFiles.ReadIniFile(Path, "TP" + i, "PATH");
                exe = ProcessIniFiles.ReadIniFile(Path, "TP" + i, "EXE");
                tpsPath.Add(TpsDirectory + name.ToString() + "\\" + pathToTps.ToString() + exe.ToString());
            }
            return tpsPath;
        }
        /// <summary>Gets the dot net TPS executable path.</summary>
        /// <param name="Path">The path.</param>
        /// <returns>The path to the TPS exe</returns>
        public static List<string> GetDotNetTpsExePath(string Path)
        {
            List<string> tpsPath = new List<string>();

            foreach (string f in Directory.EnumerateFiles(Path, "*.exe", SearchOption.AllDirectories))
            {
                if (f.Contains("TPS"))
                {
                    tpsPath.Add(f);
                }
            }
            return tpsPath;
        }
        public static List<string> GetDotNetTpsName(string Path)
        {
            List<string> tpsList = new List<string>();

            foreach (string f in Directory.EnumerateFiles(Path, "*.exe", SearchOption.AllDirectories))
            {
                Tps = f.Split('\\');
                if (Tps.Length > 5)
                {
                    if (ProcessIniFiles.TpsDrive == @"E:\")
                    {
                        if (Tps[7].StartsWith("TPS"))
                        {

                            tpsList.Add(Tps[7].Substring(0, Tps[7].Length - 4).ToUpper().Replace("_", " "));
                        }

                    }
                    else
                    {
                        if (Tps[5].StartsWith("TPS"))
                        {
                            tpsList.Add(Tps[5].Substring(0, Tps[5].Length - 4).ToUpper());
                        }
                    }
                }
            }
            return tpsList;
        }
        /// <summary>Gets the name of the atlas TPS.</summary>
        /// <param name="apsName">Name of the APS</param>
        /// <param name="systemType">The sytem type</param>
        /// <returns>List of TPSs within the APS<br /></returns>
        public static List<string> GetListOfTpsNames(string apsName, string systemType)
        {
            List<string> tpsList = new List<string>();

            string json = File.ReadAllText(PathToJsonFiles + systemType + @"\" + apsName + ".json");

            List<TpsData> tpsData = JsonConvert.DeserializeObject<List<TpsData>>(json);
            int numberOfTps = tpsData.Count;
            for (int i = 0; i < numberOfTps; i++)
            {
                tpsList.Add(tpsData[i].DisplayName);
            }
            return tpsList;
        }

        
        /// <summary>Gets the atlas TPS executable path.</summary>
        /// <param name="apsName">The APS name</param>
        /// <param name="systemType">The system type</param>
        /// <returns>List of paths to the TPSs within the APS</returns>
        public static List<string> GetListOfTpsExePaths(string apsName, string systemType)
        {
            List<string> tpsPath = new List<string>();
            string json = File.ReadAllText(PathToJsonFiles + systemType + @"\" + apsName + ".json");

            List<TpsData> tpsData = JsonConvert.DeserializeObject<List<TpsData>>(json);
            int numberOfTps = tpsData.Count;
            for (int i = 0; i < numberOfTps; i++)
            {
                if (ProcessIniFiles.TpsDrive == @"E:\")
                {
                    tpsPath.Add(tpsData[i].PathToExeOnHardDrive);
                }
                else if (ProcessIniFiles.TpsDrive == @"F:\")
                {
                    tpsPath.Add(tpsData[i].PathToExeOnDiscDrive);
                }

            }

            return tpsPath;
        }
    }
}
