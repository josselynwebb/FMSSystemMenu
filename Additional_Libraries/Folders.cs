using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmsSystemMenu.Additional_Libraries
{
    public class Folders
    {
        public static int CSIDL_BITBUCKET = 10;
        public static int CSIDL_CONTROLS = 3;
        public static int CSIDL_DESKTOP = 0;
        public static int CSIDL_DRIVES = 17;
        public static int CSIDL_FONTS = 20;
        public static int CSIDL_NETHOOD = 18;
        public static int CSIDL_NETWORK = 19;
        public static int CSIDL_PERSONAL = 5;
        public static int CSIDL_PRINTERS = 4;
        public static int CSIDL_PROGRAMS = 2;
        public static int CSIDL_RECENT = 8;
        public static int CSIDL_SENDTO = 9;
        public static int CSIDL_STARTMENU = 11;

        public enum BrowseType
        {
            BrowseForFolders = 0x01,
            BrowseForComputers = 0x1000,
            BrowseForPrinters = 0x2000,
            BrowseForEverything = 0x4000
        };



        public struct BrowseInfo
        {
            public int hWndOwner;
            public int pIDLRoot;
            public int pszDisplayName;
            public string lpszTitle;
            public int ulFlags;
            public int lpfnCallback;
            public int lParam;
            public int iImage;
        }
    }
}
