using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmsSystemMenu.Additional_Libraries
{
    public static class ProcessIniFiles
    {
        public static string PathToAtsIni = @"C:\Users\Public\Documents\ATS\ATS.ini";
        public static string TpsDrive = "F:\\";
        public static string GetTpsDrive()
        {

            string tpsDrive = ReadIniFile(PathToAtsIni, "File Locations", "PAWS_HD");

            return tpsDrive;
        }

        public static string GetTpsPath()
        {
            string tpsPath = String.Empty;//(Program.AtsIniPath, "File Locations", "TPS_LOCATION");
            return tpsPath.ToString();
        }

        public static string ReadIniFile(string iniFilePath, string sectionName, string keyName, string defaultString = null)
        {

            IniFileParser iniFileParser = new IniFileParser(iniFilePath);
            string value = iniFileParser.GetValue(sectionName, keyName);

            return value;
        }

        public static void WriteIniFile(string iniFilePath, string sectionName, string keyName, string value)
        {
            IniFileParser iniFileParser = new IniFileParser(iniFilePath);
            iniFileParser.SetValue(sectionName, keyName, value);
            iniFileParser.Save(iniFilePath);
        }
        public static void GetAteInfo()
        {
            string result = "";
            string section = "System Startup";
            string key = "SN";

            //result = ProcessIniFiles.ReadIniFile(Program.AtsIniPath, section, key, "");
            //Program.AteSerialNumber = result;

            //key = "SWR";
            //result = ProcessIniFiles.ReadIniFile(Program.AtsIniPath, section, key, "");

            //Program.AteSoftwareVersion = result;

            //key = "SYSTEM_TYPE";
            //result = ProcessIniFiles.ReadIniFile(Program.AtsIniPath, section, key, "");
            //Program.AteSystemType = result;

            //section = "Calibration";
            //key = "SYSTEM_EFFECTIVE";

            //result = ProcessIniFiles.ReadIniFile(Program.AtsIniPath, section, key, "");

            //Program.CalDate = result;


        }

        public static void GetApsInfo(string ateType, string pathToIni)
        {
            string result = "";
            string section = "TPS";
            string key = "CD_VERSION";
            if (ateType.Contains("717"))
            {
                ////E:\APSDATA\VIPERT\LAV25A2_CCAs\viper_tps.ini
                ////Program.TpsIniFile = pathToIni;

                //result = ProcessIniFiles.ReadIniFile(Program.TpsIniFile, section, key, "");
                //Program.ApsSoftwareVersion = result;

                //key = "CD_PN";
                //result = ProcessIniFiles.ReadIniFile(Program.TpsIniFile, section, key, "");
                //Program.ApsPartNumber = result;
            }
            else if (ateType.Contains("657"))
            {
                ////TETS
                //Program.TpsIniFile = pathToIni;
                //result = ProcessIniFiles.ReadIniFile(Program.TpsIniFile, section, key, "");
                //Program.ApsSoftwareVersion = result;

                //key = "CD_PN";
                //result = ProcessIniFiles.ReadIniFile(Program.TpsIniFile, section, key, "");
                //Program.ApsPartNumber = result;
            }

        }

        public static string GetTpsLocation()
        {
            string tpsLocation = String.Empty;

            tpsLocation = ReadIniFile(PathToAtsIni, "File Locations", "TPS_LOCATION");

            return tpsLocation;
        }
    }
}
