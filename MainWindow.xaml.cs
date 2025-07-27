
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

using static System.Net.WebRequestMethods;
using Microsoft.VisualBasic;


using System.Windows.Input;
using FmsSystemMenu.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace FmsSystemMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //// Program Unique Debug Constants
        public const string DEBUG_FILE = "C:\\APS\\DATA\\DEBUGIT_SysMenu";
        public const string DEBUG_RECORD = "C:\\APS\\DATA\\SysMenu_DEBUG.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        #region  MouseEvents

        private void BtnTPSPrg_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnTpsPrg;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Test Program Sets";
            }
        }

        private void BtnTPSPrg_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void BtnStest_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnStest;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Self Test";
            }
        }

        private void BtnStest_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        private void BtnSurvey_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnSurvey;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "System Survey";
            }
        }

        private void BtnSurvey_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }


        private void BtnFhdb_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnFhdb;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Fault History Database";
            }
        }

        private void BtnFhdb_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        

        private void BtnWizard_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnWizard;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "UDD Wizard";
            }
        }

        private void BtnWizard_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        
        private void BtnRestore_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnRestore;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "UDD Restore";
            }
        }

        private void BtnRestore_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        

        private void BtnSais_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnSais;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "SAIS Toolbar ";
            }
        }

        private void BtnSais_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        

        private void BtnPaws_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnPaws;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "PAWS";
            }
        }

        private void BtnPaws_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }


        private void BtnLog_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnLog;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "System Log";
            }
        }

        private void BtnLog_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        

        private void BtnStow_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnStow;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Stow Veo2 Collimator";
            }
        }

        private void BtnStow_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

       

        private void BtnVeo2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnVeo2;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Veo2 Camera Configuration";
            }
        }

        private void BtnVeo2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

       

        private void BtnCal_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnCal;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "System Calibration";
            }
        }

        private void BtnCal_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

       

        private void BtnAbout_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnAbout;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "System Information";
            }
        }

        private void BtnAbout_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }

        

        private void BtnClose_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = BtnClose;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = "Close Menu";
            }
        }

        private void BtnClose_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        #endregion

        #region ButtonClicks
        
        private void BtnFhdb_OnClick(object sender, RoutedEventArgs e)
        {
            BtnFhdb.IsEnabled = false;
            SendToBack();

            string fhdbPath = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "FHDB_PROCESSOR");

            StartProcess(fhdbPath, "Fault History Database");
            BtnFhdb.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }
        private void BtnWizard_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRestore_OnClick(object sender, RoutedEventArgs e)
        {

        }
        private void BtnTPSPrg_Click(object sender, RoutedEventArgs e)
        {
            int tries = 3;

            BtnTpsPrg.IsEnabled = false;

            SendToBack();
            if (System.Environment.UserName == "DoD_Admin")
            {

                MessageBox.Show("The Administrator Account can not be used to run APSs");
            }
            else
            {
                string TpsDirectory = ProcessIniFiles.GetTpsLocation();
                ProcessIniFiles.TpsDrive = ProcessIniFiles.GetTpsDrive();
                DialogResult result = new DialogResult();
                if (!Directory.Exists(ProcessIniFiles.TpsDrive + TpsDirectory))
                {

                    for (int i = 0; i < tries; i++)
                    {
                        result = System.Windows.Forms.MessageBox.Show("There are no APSs installed.  Please install the APS(s) to be ran. \n Once complete click 'Retry' to proceed.\n\nClicking 'Cancel' will suspend the ability to run TPSs.", "System Menu", MessageBoxButtons.RetryCancel);

                        if (result == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }
                        else if (Directory.Exists(ProcessIniFiles.TpsDrive + TpsDirectory))
                        {
                            TestPrograms testPrograms = new TestPrograms();
                            testPrograms.Show();
                            testPrograms.Activate();
                        }

                        if (i == 2)
                        {
                            MessageBox.Show("You must install at least one APS!", "System Menu", MessageBoxButtons.OK);
                        }
                    }


                }
                else
                {

                    TestPrograms testPrograms = new TestPrograms();
                    testPrograms.Show();
                    testPrograms.Activate();
                }
            }

            BtnTpsPrg.IsEnabled = true;
            this.Close();
            //System.Windows.Application.Current.Shutdown();



        }
        private void BtnAbout_OnClick(object sender, RoutedEventArgs e)
        {
            BtnAbout.IsEnabled = false;
            SendToBack();
            About about = new About();
            about.Show();
            about.Activate();
            BtnAbout.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }
        private void BtnCal_OnClick(object sender, RoutedEventArgs e)
        {
            BtnCal.IsEnabled = false;
            SendToBack();
            string sysCalPath = @"F:\Syscal.exe"; 
            StartProcess(sysCalPath, "System Calibration");
            BtnCal.IsEnabled = true;
            this.WindowState = WindowState.Minimized;

        }
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            System.Windows.Application.Current.Shutdown();
        }
        private void BtnVeo2_OnClick(object sender, RoutedEventArgs e)
        {
            BtnVeo2.IsEnabled = false;
            SendToBack();
            string systemType = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "System Startup", "SYSTEM_TYPE");

            if (systemType.Contains("717"))
            {
                VEO2.Veo.GetVeo2CameraConfiguration();
            }

            BtnVeo2.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }
        private void BtnStow_OnClick(object sender, RoutedEventArgs e)
        {
            BtnStow.IsEnabled = false;
            SendToBack();
            VEO2.Veo.StowVeo2Collimator();
            BtnStow.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }

        private void BtnLog_OnClick(object sender, RoutedEventArgs e)
        {
            BtnLog.IsEnabled = false;
            SendToBack();
            string logPath = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "SYSTEM_LOG");
            StartProcess(logPath, "System Log");
            BtnLog.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }

        private void StartProcess(string pathToProcess,string caption)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = pathToProcess;
                process.Start();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, caption + ":  File Not Found", MessageBoxButton.OK);
            }
        }
        private void BtnPaws_OnClick(object sender, RoutedEventArgs e)
        {
            string sectionName = "File Locations";
            string keyName = "PAWS_RTS";

            string pawsPath = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, sectionName, keyName);

            BtnPaws.IsEnabled = false;
            SendToBack();
            StartProcess(pawsPath, "PAWs Runtime");
            BtnPaws.IsEnabled =true;
            this.WindowState = WindowState.Minimized;
        }

        private void BtnSais_OnClick(object sender, RoutedEventArgs e)
        {
            BtnSais.IsEnabled = false;
            SendToBack();
            string saisPath = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "SAIS_TOOLBAR");
            StartProcess(saisPath, "SAIS Toolbar");
            BtnSais.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }


        private void BtnSurvey_OnClick(object sender, RoutedEventArgs e)
        {
            BtnSurvey.IsEnabled = false;
            SendToBack();

            string sysSurveyPath = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "SYSTEM_SURVEY");

            StartProcess(sysSurveyPath, "System Survey");

            BtnSurvey.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }


        private void BtnStest_OnClick_Click(object sender, RoutedEventArgs e)
        {
            BtnStest.IsEnabled = false;
            SendToBack();

            string selfTestPath = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, "File Locations", "SYSTEM_SELF_TEST");

            StartProcess(selfTestPath, "System Self Test");

            BtnStest.IsEnabled = true;
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        private void SendToBack()
        {
            this.WindowState = WindowState.Minimized;
            this.WindowState = WindowState.Normal;
        }

    }
}
