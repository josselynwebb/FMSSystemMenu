using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FmsSystemMenu.Additonal_Libraries;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FmsSystemMenu.Windows
{
    /// <summary>
    /// Interaction logic for TestPrograms.xaml
    /// </summary>
    public partial class TestPrograms : Window
    {
        public string SystemType = String.Empty;
        public string apsName = string.Empty;
        public string typeOfSystem = String.Empty;
        public string apsIniPath = string.Empty;
        public string tpsLocation = String.Empty;
        public string tpsDirectory = String.Empty;
        public TestPrograms()
        {
            InitializeComponent();

            GetSystemInformation();

            ProcessIniFiles.TpsDrive = ProcessIniFiles.GetTpsDrive();
            CommonUtilities.ProgramPath = System.Windows.Forms.Application.StartupPath;

            WriteTpsLocation();

            tpsLocation = ProcessIniFiles.GetTpsLocation();

            tpsDirectory = ProcessIniFiles.TpsDrive + tpsLocation;

            AddToComboBox(SystemType.ToString());

            this.PreviewMouseDoubleClick += new MouseButtonEventHandler(OnPreviewMouseDoubleClick);
        }

        private void SendToBack()
        {
            this.WindowState = WindowState.Minimized;
            this.WindowState = WindowState.Normal;
        }

        private void BtnTechManual_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if ( Tg_Btn.IsChecked == false )
            //{
            //	Popup.PlacementTarget = BtnTechManual;
            //	Popup.Placement = PlacementMode.Right;
            //	Popup.IsOpen = true;
            //	Header.PopupText.Text = "TPS Technical Manual";
            //}
        }

        private void BtnTechManual_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Popup.Visibility = Visibility.Collapsed;
            //Popup.IsOpen = false;
        }

        private void BtnAbout_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnAbout;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "System Information";
            }
        }

        private void BtnAbout_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void BtnClose_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnClose;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Close Menu";
            }
        }

        private void BtnClose_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        /// <summary>Adds the list of APSs to ComboBox.</summary>
        /// <param name="systemType">Type of the system.</param>
        private void AddToComboBox(string systemType)
        {

            DialogResult result = new DialogResult();
            StringBuilder sb = new StringBuilder();
            try
            {
                if (!Directory.Exists(tpsDirectory))
                {
                    System.Windows.Forms.MessageBox.Show("install TPS");
                }
                else
                {
                    string[] directories = Directory.GetDirectories(tpsDirectory);

                    foreach (string d in directories)
                    {
                        if (SystemType.ToString().Contains("717"))
                        {
                            if (tpsDirectory.Contains("TPS_Programs"))
                            {
                                ComboBox.Items.Add(d.Substring(16).Replace("_", " "));
                            }
                            else if (tpsDirectory.Contains("V4"))
                            {
                                ComboBox.Items.Add(d.Substring(20).Replace("_", " "));
                            }
                            else
                            {
                                ComboBox.Items.Add(d.Substring(18).Replace("_", " "));
                            }
                        }
                        else
                        {
                            ComboBox.Items.Add(d.Substring(16).Replace("_", " "));
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }



        }

        /// <summary>Writes the TPS location to the ATS.ini</summary>
        private void WriteTpsLocation()
        {
            if (ProcessIniFiles.TpsDrive == @"F:\")
            {

                ProcessIniFiles.WriteIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "TPS_LOCATION",
                    @"TPS_Programs\");
            }
            else if (ProcessIniFiles.TpsDrive == @"E:\")
            {
                if (SystemType.ToString().Contains("657"))
                {
                    ProcessIniFiles.WriteIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "TPS_LOCATION",
                        @"APSDATA\TETS\");
                }
                else if (SystemType.ToString().Contains("(V)4"))
                {
                    ProcessIniFiles.WriteIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "TPS_LOCATION",
                        @"APSDATA\VIPERTV4\");
                }
                else
                {
                    ProcessIniFiles.WriteIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "TPS_LOCATION",
                        @"APSDATA\VIPERT\");
                }
            }
        }

        /// <summary>Gets the system information and displays it on the form</summary>
        private void GetSystemInformation()
        {
            string result = string.Empty;
            string sectionName = "System Startup";
            string keyName;
            //TxtSystem.Text = "GPATS";
            keyName = "SWR";
            result = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, sectionName, keyName);

            keyName = string.Empty;
            //TxtSoftwareVersion.Text = stringBuilder.ToString();
            

            keyName = "SYSTEM_TYPE";

            SystemType = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, sectionName, keyName);

        }

        /// <summary>Handles the Click event of the BtnClose control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {

            About about = new About();
            about.Show();
            about.Activate();
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>Handles the Click event of the BtnChangeDirectory control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BtnTechManual_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>Handles the SelectionChanged event of the Combo control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Combo_SelectionChanged(object sender, RoutedEventArgs e)
        {
            dynamic item = ComboBox.SelectedItem;
            double rows = 0;
            int count = 0;
            int tpsCount = 0;
            int numColumns = 3;
            int numRows = 0;
            string apsIni = string.Empty;
            //string apsName = string.Empty;
            string result = String.Empty;
            if (SystemType.ToString().Contains("717"))
            {
                apsIni = "vipert_tps.ini";
            }
            else if (SystemType.ToString().Contains("657"))
            {
                apsIni = "tets_tps.ini";
            }
            else
            {
                apsIni = "";
            }

            //use the selcted aps name to get the correct path
            apsName = item;
            if (ProcessIniFiles.TpsDrive == @"F:\")
            {
                apsIniPath = tpsDirectory + "\\" + apsIni;
            }
            else
            {
                apsIniPath = tpsDirectory + apsName.Replace(" ", "_") + "\\" + apsIni;
            }

            ProcessIniFiles.WriteIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "TPS_INI", apsIniPath);
            result = ProcessIniFiles.ReadIniFile(apsIniPath, "TPS", "CD_PN");
            TxtBoxPn.Text = result.ToString();
            

            result = ProcessIniFiles.ReadIniFile(apsIniPath, "TPS", "CD_VERSION");
            TxtBoxVersion.Text = result.ToString();
            TpsGGrid.Children.Clear();

            if (apsName.Contains("M777"))
            {
                DirectoryUtility.TpsList =
                    DirectoryUtility.GetDotNetTpsName(tpsDirectory + apsName.Replace(" ", "_") + "\\");

                DirectoryUtility.TpsPaths =
                    DirectoryUtility.GetDotNetTpsExePath(tpsDirectory + apsName.Replace(" ", "_") +
                                                         "\\");
            }
            else
            {
                DirectoryUtility.TpsList = DirectoryUtility.GetAtlasTpsName(apsIniPath);
                DirectoryUtility.TpsPaths = DirectoryUtility.GetAtlasTpsExePath(apsIniPath);
            }

            tpsCount = DirectoryUtility.TpsList.Count;
            rows = (double)tpsCount / 3;
            numRows = (int)Math.Ceiling(rows);

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    System.Windows.Controls.Button myControl1 = new System.Windows.Controls.Button();
                    myControl1.BorderThickness = new Thickness(2, 2, 2, 2);

                    string tpsName = DirectoryUtility.TpsList[count];
                    string rt = ProcessIniFiles.ReadIniFile(apsIniPath, "TP" + (count + 1), "RT", "");
                    string runTime = rt.ToString();
                    myControl1.Content = tpsName;


                    myControl1.Name = "Button" + count.ToString();
                    myControl1.Background = new SolidColorBrush(Colors.LightBlue);

                    Grid.SetColumn(myControl1, j);
                    Grid.SetRow(myControl1, i);
                    if (runTime != "")
                    {
                        myControl1.ToolTip = "Estimated run time is " + rt.ToString() + " minutes";
                    }

                    TpsGGrid.Children.Add(myControl1);

                    myControl1.Click += MyControl1_Click1;
                    count++;

                    if (count == tpsCount)
                    {
                        break;
                    }
                }
            }

            SetToolTips(apsName);
        }


        /// <summary>Sets the tool tips.</summary>
        /// <param name="ApsName">Name of the aps.</param>
        private void SetToolTips(string ApsName)
        {
            if (ApsName.Contains("AAV"))
            {
                ComboBox.ToolTip = "AN/PSM -115";
            }
            else if (ApsName.Contains("Handheld Weapon Optics"))
            {
                ComboBox.ToolTip = "AN/PSM-117";
            }
            else if (ApsName.Contains("M777 DFCS"))
            {
                ComboBox.ToolTip = "AN/PSM-119A";
            }
            else if (ApsName.Contains("LAV Instrument Panel"))
            {
                ComboBox.ToolTip = "AN/PSM-120";
            }
            else if (ApsName.Contains("LAV25A2 LRUs"))
            {
                ComboBox.ToolTip = "AN/PSM-123";
            }
            else if (ApsName.Contains("VSAT"))
            {
                ComboBox.ToolTip = "AN/PSM-126";
            }
            else if (ApsName.Contains("VSAT"))
            {
                ComboBox.ToolTip = "AN/PSM-126";
            }
            else if (ApsName.Contains("M4A1 Saber"))
            {
                ComboBox.ToolTip = "AN/PSM-129";
            }
            else if (ApsName.Contains("LAV25A2 CCAs"))
            {
                ComboBox.ToolTip = "AN/PSM-130";
            }
            else if (ApsName.Contains("LAVAT"))
            {
                ComboBox.ToolTip = "AN/TSM-223";
            }
        }

        private void OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true; // Suppress the double-click event
        }

        public string ParseTpsPath(string tpsPathList, string tpsNameNeeded)
        {
            string[] tps;
            string parsePath = tpsPathList;
            //for ( int i = 0; i < tpsPathList.Count; i++ )
            //{
            //	parsePath = tpsPathList;
            tps = parsePath.Split('\\');

            if (ProcessIniFiles.TpsDrive == "F:\\")
            {
                if (tps[4] == tpsNameNeeded)
                {
                    parsePath = tps[4];
                }
            }
            else
            {
                if (tps[6] == tpsNameNeeded)
                    parsePath = tps[6];
            }


            //}

            return parsePath;

        }

        /// <summary>Handles the Click1 event of the MyControl1 control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void MyControl1_Click1(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            button.IsEnabled = false;

            SendToBack();
            this.WindowState = System.Windows.WindowState.Minimized;

            string content = button.ToString().Substring(32);
            string path;
            string programName = String.Empty;
            string[] tpsName;
            string PAWSRuntimePath = @"C:\usr\Tyx\Bin\wrts.exe";
            string GTDPath =
                "E:\\APSDATA\\VIPERT\\LAV25A2_LRUs\\LAV25A2_LRUs\\Gun_Turret_Drive_assy\\Gun_Turret_Drive_LRU_16104540-021\\TP_LAVA2_GTD.OBJ";
            string PDAPath =
                "E:\\APSDATA\\VIPERT\\LAV25A2_LRUs\\LAV25A2_LRUs\\Gun_Turret_Drive_assy\\Power_Distribution_Assy_LRU_16104540-021\\TP_LAVA2_PDA.OBJ";
            string GTDAPN = "16104540-021";
            string pn = String.Empty;

            try
            {
                if (!apsName.Contains("M777"))
                {
                    for (int i = 0; i < DirectoryUtility.TpsPaths.Count; i++)
                    {
                        pn = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "TP" + (i + 1), "PN");

                        if (content.Contains(pn))
                        {
                            if (pn== GTDAPN)
                            {
                                if (content.Contains("Power"))
                                {
                                    programName = PDAPath;
                                }
                                else
                                {
                                    programName = GTDPath;
                                }

                            }
                            else
                            {
                                programName = DirectoryUtility.TpsPaths[i];
                            }

                        }
                    }
                }
                else
                {
                    for (int i = 0; i < DirectoryUtility.TpsPaths.Count; i++)
                    {
                        if (DirectoryUtility.TpsPaths[i].Contains(content.Replace(" ", "_")))
                        {
                            programName = DirectoryUtility.TpsPaths[i];
                        }

                    }
                }


                if (apsName.Contains("M777"))
                {

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo processStartInfo =
                        new System.Diagnostics.ProcessStartInfo(programName);

                    process.StartInfo = processStartInfo;

                    try
                    {
                        process.Start();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {


                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo processStartInfo =
                        new System.Diagnostics.ProcessStartInfo(PAWSRuntimePath, programName);
                    processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                    process.StartInfo = processStartInfo;
                    try
                    {

                        process.Start();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            Thread.Sleep(5000);

            button.IsEnabled = true;


        }
    }
}
