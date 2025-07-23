using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FmsSystemMenu.Windows
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            GetSystemInformation();
        }

        private void GetSystemInformation()
        {
            string sysInfo; 
            string sectionName = "System Startup";
            string keyName;
            TxtSystem.Text = "GPATS";
            //TxtSystem.Text = "GPATS";
            keyName = "SWR";
            sysInfo = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, sectionName, keyName);

            keyName = string.Empty;
            TxtSoftwareVersion.Text = sysInfo;
            sysInfo = string.Empty;

            keyName = "SYSTEM_TYPE";

            sysInfo = ProcessIniFiles.ReadIniFile(ProcessIniFiles.PathToAtsIni, sectionName, keyName);

            TxtSystemType.Text = sysInfo;
            TxtUserName.Text = Environment.UserName;
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
