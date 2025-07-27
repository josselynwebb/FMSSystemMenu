using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmsSystemMenu.Additional_Libraries
{
    public class Debug
    {
        #region Constants
        // Program Unique Debug Constants
        public const string AtxmlDebugFile = "C:\\APS\\DATA\\DEBUGIT_ATXML";
        public const string AtxmlDebugRecord = "C:\\APS\\DATA\\ATXML_DEBUG.txt";
        #endregion

        /// <summary>
        /// Boolean value identifies whether or not to output debug info
        /// - value is overriden if AsyncRead is specified
        /// </summary>
        public static bool DebugMode = Debug.IsDebug();
        public static bool IsHidden = false;

        /// <summary>
        /// Check for existence of debug output file
        /// </summary>
        /// <returns>boolean</returns>
        public static bool IsDebug()
        {
            if (System.IO.File.Exists(MainWindow.DEBUG_FILE))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create Output file for debug statements
        /// </summary>
        /// <returns></returns>
        public static int CreateDebugFile()
        {
            int stat = 0;

            try
            {
                System.IO.FileStream dbgFile = System.IO.File.Create(MainWindow.DEBUG_RECORD);
                dbgFile.Close();
                dbgFile.Dispose();
            }
            catch (Exception Ex)
            {
                Console.Write(Ex.Message);
                stat = -1;
            }

            return stat;
        }

        /// <summary>
        /// Write debug strings to output file for troubleshooting purposes
        /// </summary>
        /// <param name="DebugString"></param>
        public static void WriteDebugInfo(string DebugString)
        {
            // Send text string to debug output file
            using (System.IO.StreamWriter dbgFile = System.IO.File.AppendText(MainWindow.DEBUG_RECORD))
            {
                try
                {
                    dbgFile.Write(DebugString + Environment.NewLine);
                }
                catch (System.IO.IOException)
                {
                    throw;
                }
                /*catch (Exception Ex)
                {
                    Console.Write(Ex.Message);
                    throw;
                }*/

                dbgFile.Close();
            }
        }
    }
}
