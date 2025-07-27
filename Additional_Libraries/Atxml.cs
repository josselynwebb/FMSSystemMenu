using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FmsSystemMenu.Additional_Libraries
{
    public static class Atxml
    {
        #region Constancts

        public const short conNoDLL = 48;
        public const short MAX_XML_SIZE = 4096;

        #endregion

        #region Variables

        private static int Vi = -1;
        private static bool sessionActive = false;
        private static bool initialized = false;

        public static bool Initialized
        {
            get
            {
                return initialized;
            }
            set
            {
                initialized = value;
            }
        }

        #endregion



        public static int Initialize(string ProcType, string GUID)
        {
            //if (GPATSSystemMenu.DebugMode && !GPATSSystemMenu.IsHidden) { Debug.WriteDebugInfo("ATXML.Initialize(" + ProcType + ", " + GUID + ")"); }

            int stat = -1;

            try
            {
                stat = DllImports.atxml_Initialize(ProcType, GUID);
                Initialized = true;
            }
            catch (Exception Ex)
            {
                //if (Program.DebugMode && !Program.IsHidden)
                {
                    Debug.WriteDebugInfo("ATXML.Initialize() - failed to perform action");
                    Debug.WriteDebugInfo("ATXML.Initialize() - Source: " + Ex.Source + Environment.NewLine + "ATXML.Initialize() - Err: " + Ex.HResult.ToString() + Environment.NewLine + "ATXML.Initialize() - Message: " + Ex.Message);
                }
            }

            return stat;
        }


        public static int ValidateDevice(string device)
        {
            int stat = -1;
            string XmlBuf;
            string response = new string(' ', 4096);
            string allocation = @"C:\Program Files (x86)\ATS\ISS\config\PawsAllocation.xml";


            XmlBuf = "<AtXmlTestRequirements>";
            XmlBuf = String.Concat(XmlBuf, "<ResourceRequirement> " + "  <ResourceType>Source</ResourceType> ");
            XmlBuf = String.Concat(XmlBuf, "  <SignalResourceName>" + device + "</SignalResourceName> " + "</ResourceRequirement> ");
            XmlBuf = String.Concat(XmlBuf, "</AtXmlTestRequirements>");

            stat = ValidateRequirements(XmlBuf, allocation, response, 4096);


            return stat;
        }


        public static int ValidateRequirements(string requirements, string alloc, string response, short bufferSize)
        {
            int stat = -1;

            try
            {
                stat = DllImports.atxml_ValidateRequirements(requirements, alloc, response, bufferSize);
            }
            catch (Exception)
            {

            }

            return stat;
        }

        public static int Close()
        {
            // if (Program.DebugMode && !Program.IsHidden) { Debug.WriteDebugInfo("ATXML.Close()"); }

            int stat = -1;

            if (Initialized)
            {
                try
                {
                    stat = DllImports.atxml_Close();
                }
                catch (Exception Ex)
                {
                    // if (Program.DebugMode && !Program.IsHidden)
                    {
                        Debug.WriteDebugInfo("ATXML.Close() - failed to perform action");
                        Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                    }
                }
            }
            else
            {
                stat = 0;
            }

            return stat;
        }


        public static int IssueSignal(string SigDescr, ref string Rspns, short BuffSz)
        {
            // if (Program.DebugMode && !Program.IsHidden) { Debug.WriteDebugInfo("ATXML.IssueSignal(" + SigDescr + ", " + Rspns + ", " + BuffSz.ToString() + ")"); }

            int stat = -1;
            StringBuilder sbDescr = new StringBuilder(SigDescr, BuffSz);
            StringBuilder sbRspns = new StringBuilder(Rspns, BuffSz);

            try
            {
                stat = DllImports.atxml_IssueSignal(sbDescr, sbRspns, BuffSz);
            }
            catch (Exception Ex)
            {
                // if (Program.DebugMode && !Program.IsHidden)
                {
                    Debug.WriteDebugInfo("ATXML.IssueSignal() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }

            SigDescr = sbDescr.ToString();
            Rspns = sbRspns.ToString();

            return stat;
        }


        public static void OpenSession(string Instr, int Session, string SessionName, int TimeOut, ref int Instance)
        {
            int ret = DllImports.atxmlDF_viOpen(Instr, Session, SessionName, TimeOut, ref Instance);

            Vi = Instance;

            if (Vi > 0)
            {
                sessionActive = true;
            }

            return;
        }


        public static void SetAttribute(string Instr, int viSession, int attr, int attrVal)
        {

        }


        public static string Read(string Instr, int Session, string Msg, int charCount, ref int returnCount)
        {
            //if (Program.DebugMode && !Program.IsHidden) { Debug.WriteDebugInfo("ATXML.Read(" + Instr + ", " + Session.ToString() + ", " + Msg + ", " + charCount.ToString() + ", " + returnCount.ToString() + ")"); }

            int stat = -1;

            try
            {
                stat = DllImports.atxmlDF_viRead(Instr, Session, ref Msg, charCount, ref returnCount);
            }
            catch (Exception Ex)
            {
                // if (Program.DebugMode && !Program.IsHidden)
                {
                    Debug.WriteDebugInfo("ATXML.Read() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }

            return Msg;
        }


        public static int Write(string resourceName, string Commands, ref int returnCount)
        {
            //if (Program.DebugMode && !Program.IsHidden) { Debug.WriteDebugInfo("ATXML.Write(" + Instr + ", " + Session.ToString() + ", " + Msg + ", " + charCount.ToString() + ", " + returnCount.ToString() + ")"); }

            int stat = -1;

            try
            {
                stat = DllImports.atxmlDF_viWrite(resourceName, 0, Commands, Commands.Length, ref returnCount);
            }
            catch (Exception Ex)
            {
                //if (Program.DebugMode && !Program.IsHidden)
                {
                    Debug.WriteDebugInfo("ATXML.Write() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }

            return stat;
        }
    }
}
