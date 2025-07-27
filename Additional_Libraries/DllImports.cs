using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FmsSystemMenu.Additional_Libraries
{
    public class DllImports
    {
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int SetVolumeLabel(string lpRootPathName, string lpVolumeName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxml_IssueDriverFunctionCall(string XmlBuffer, string Response, short BufferSize);

        //bus functions
        [DllImport("AtXmlDriverFunc.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxmlDF_viWrite(string ResourceName, int vi, string buffer, int count, ref int retCount);


        [DllImport("VISA32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int viReadSTB(int vi, ref short status);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetDriveType(string drive);

        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SHFormatDrive(int hWnd, int drive, int fmtID, int options);

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxml_Initialize(string proctype, string guid);

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxml_ValidateRequirements(string TestRequirements, string Allocation, string Available, short BufferSize);

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxml_Close();

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxml_IssueSignal(StringBuilder SignalDescription, StringBuilder Response, short BufferSize);

        // VISA functions

        /// <summary>
        /// Set VISA Attribute of given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viSetAttribute(string ResourceName, int vi, int attrName, int attrValue);

        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viOut16(string ResourceName, int vi, short accSpace, int offset, short val16);

        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viStatusDesc(string ResourceName, int vi, int status, string desc);

        [DllImport("AtXmlDriverFunc.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxmlDF_viWrite(string ResourceName, int vi, string buffer, int count, int ReturnCount);

        [DllImport("AtXmlDriverFunc.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int atxmlDF_viRead(string ResourceName, int vi, string buffer, int count, int ReturnCount);

        /// <summary>
        /// Read Waveform Data Points from Oscope
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="Source"></param>
        /// <param name="TransferType"></param>
        /// <param name="WaveFormArray"></param>
        /// <param name="NumberOfPoints"></param>
        /// <param name="AcquisitionCount"></param>
        /// <param name="SampleInterval"></param>
        /// <param name="TimeOffset"></param>
        /// <param name="XREFERENCE"></param>
        /// <param name="VoltIncrement"></param>
        /// <param name="VoltOffset"></param>
        /// <param name="YREFERENCE"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_zt1428_read_waveform(string ResourceName, int vi, int Source, int TransferType, ref double WaveFormArray, ref int NumberOfPoints, ref int AcquisitionCount, ref double SampleInterval, ref double TimeOffset, ref int XREFERENCE, ref double VoltIncrement, ref double VoltOffset, ref int YREFERENCE);

        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_eip_selectCorrectionsTable(string ResourceName, string FilePath, string TableName);

        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_eip_setCorrectedPower(string ResourceName, int instrSession, double Frequency, float Power);

        /// <summary>
        /// Execute device clear for given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viClear(string ResourceName, int vi);

        /// <summary>
        /// Read [string] return value from given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <param name="retCount"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viRead(string ResourceName, int vi, ref string buffer, int count, ref int retCount);

        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viIn16(string ResourceName, int vi, short space, int offset, ref short val16);

        /// <summary>
        /// Open a VISA sesion to given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="sesn"></param>
        /// <param name="name"></param>
        /// <param name="timeout"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        public static extern int atxmlDF_viOpen(string ResourceName, int sesn, string name, int timeout, ref int vi);

        [DllImport("VISA32.dll")]
        public static extern int viClose(int vi);

        [DllImport("VISA32.dll")]
        public static extern int viSetAttribute(int vi, int attrName, int attrVal);


        [DllImport("VISA32.dll")]
        public static extern int viIn16(int vi, short accSpace, int offset, ref short val16);
    }
}
