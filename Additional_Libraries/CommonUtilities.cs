using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FmsSystemMenu.Additional_Libraries
{
    public class CommonUtilities
    {
        
        #region Public Variables
        public static string FhdbSelfExtractingFileName;
        public static string FhdbUsbFileName; //File name and location on USB Disk
        public static string UsbDiskDriveLetter = string.Empty;
        public static bool UsbFlag;
        public static bool HardDriveFlag;
        public const short SystemCalibration = 7;
        public const short NumberOfPrograms = 16;
        public const int MaxBufferSize = 255;
        public static bool[] LoginAccess = new bool[NumberOfPrograms];
        public static bool GoBack;
        public static string ProgramPath;
        public static bool Success;
        public static int StepNumber;
        public static int Drive;
        public static bool ExternalPower;
        public static bool SaifInstalled;
        public static int FpuHandle = 0;
        public const int DriveRemovable = 2;
        public const string GUID = "5d22bcdf-498c-4c0d-bbb7-513ea600aa24";
        public const string PROCTYPE = "GPATSSystemMenu";
        public static string CameraModifier;
        public static string IniFilePath = "C:\\Users\\Public\\Documents\\ATS\\ATS.ini";
        public static bool Unload;

        public static DialogResult DialogResponse = new DialogResult();
        #endregion

        public static string GetSerialNumber()
        {
            return "";
        }
    }
}
