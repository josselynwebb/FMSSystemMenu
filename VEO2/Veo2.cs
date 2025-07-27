using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FmsSystemMenu.VEO2
{
    public class Veo2
    {
        #region DllImports
        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_SYSTEM_CONFIGURATION_INITIATE(int sysConfig);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_SYSTEM_CONFIGURATION_FETCH(ref int sysConfig);

        /// <summary>Initiates the module reset.</summary>
        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void RESET_MODULE_INITIATE();

        /// <summary>Shuts down IRWIN</summary>
        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void IRWIN_SHUTDOWN();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void GET_BIT_DATA_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void GET_BIT_DATA_FETCH(ref int errorNumber);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void GET_MODULE_ID_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void GET_MODULE_ID_FETCH(ref int xIdent);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void GET_STATUS_BYTE_MESSAGE_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void GET_TEMP_TARGET_IR_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void GET_TEMP_TARGET_IR_FETCH(ref float xTargetTemp);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_RDY_WINDOW_IR_INITIATE(float xRdyWindow);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_RDY_WINDOW_IR_FETCH(float xRdyWindow);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TARGET_POSITION_INITIATE(int xTargetPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TARGET_POSITION_FETCH(ref int xTargetPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TEMP_ABSOLUTE_IR_INITIATE(float xTemperature);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TEMP_ABSOLUTE_IR_FETCH(ref float xTemperature);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TEMP_DIFFERENTIAL_IR_INITIATE(float xTemperature);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TEMP_DIFFERENTIAL_IR_FETCH(ref float xTemperature);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_CAMERA_TRIGGER_LASER_INITIATE(int xTrigger);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_CAMERA_TRIGGER_LASER_FETCH(ref int xTrigger);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_CAMERA_DELAY_LASER_INITIATE(float xDelay);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_CAMERA_DELAY_LASER_FETCH(ref float xDelay);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_SOURCE_STAGE_LASER_INITIATE(int sourceStagePosition);

        /// <summary>Sets the source stage laser fetch.</summary>
        /// <param name="sourceStagePosition">The source stage position.</param>
        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_SOURCE_STAGE_LASER_FETCH(ref int sourceStagePosition);

        /// <summary>Initiates the sensor stage laser.</summary>
        /// <param name="sensorStagePosition">The sensor stage position.</param>
        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void SET_SENSOR_STAGE_LASER_INITIATE(int sensorStagePosition);

        /// <summary>Sets the sensor stage laser fetch.</summary>
        /// <param name="sensorStagePosition">The sensor stage position.</param>
        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void SET_SENSOR_STAGE_LASER_FETCH(ref int sensorStagePosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SELECT_DIODE_LASER_INITIATE(int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SELECT_DIODE_LASER_FETCH(ref int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TRIGGER_SOURCE_LASER_INITIATE(int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_TRIGGER_SOURCE_LASER_FETCH(ref int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE_AMPLITUDE_LASER_INITIATE(float xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE_PERIOD_LASER_INITIATE(float xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE_PERIOD_LASER_FETCH(ref float xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_OPERATION_LASER_INITIATE(int xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_OPERATION_LASER_FETCH(ref int xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_RANGE_EMULATION_LASER_INITIATE(float xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_RANGE_EMULATION_LASER_FETCH(ref float xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE2_DELAY_LASER_INITIATE(float xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE2_DELAY_LASER_FETCH(ref int xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SELECT_LARGER_PULSE_LASER_INITIATE(int xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SELECT_LARGER_PULSE_LASER_FETCH(ref int xPA);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE_PERCENTAGE_LASER_INITIATE(float xPercent);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_PULSE_PERCENTAGE_LASER_FETCH(ref float xPercent);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LASER_TEST_INITIATE(int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LASER_TEST_FETCH(ref int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_RADIANCE_VIS_INITIATE(int xRadiance);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_RADIANCE_VIS_FETCH(ref int xRadiance);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LARRS_AZ_LASER_INITIATE(int xPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LARRS_AZ_LASER_FETCH(ref int xPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LARRS_EL_LASER_INITIATE(int yPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LARRS_EL_LASER_FETCH(ref int yPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LARRS_POLARIZE_LASER_INITIATE(int xPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_LARRS_POLARIZE_LASER_FETCH(ref int xPosition);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_CAMERA_POWER_INITIATE(int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void SET_CAMERA_POWER_FETCH(int xSelect);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_LASER_SETUP(int imageNumFrames, int signalTopLeftX, int signalTopLeftY, int signalBotRightX, int signalBotRightY,
            float cameraDelayTime, int cameraTrigger, float intensityRatio);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_LASER_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_LASER_FETCH(ref float lBeamAlignCoordX, ref float lBeamAlignCoordY, ref float lBeamArea, ref int status);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_TV_VIS_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sRadiance, int lTargetPosr,
            int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, int sColorTemp,
            float sIntensityRatio);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_TV_VIS_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_TV_VIS_FETCH(ref float Boresight_X_Coord, ref float Boresight_Y_Coord, ref float LBeam_Area, ref int status);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_IR_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sDiffTemp, int lTargetPos,
            int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float sIntensityRatio);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_IR_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void BORESIGHT_IR_FETCH(ref float Boresight_X_Coord, ref float Boresight_Y_Coord, ref int status);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void IMAGE_UNIFORMITY_IR_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sDiffTemp, int lTargetPos,
            int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void IMAGE_UNIFORMITY_IR_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void IMAGE_UNIFORMITY_IR_FETCH(ref float Image_Uniformity, ref int status);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void MODULATION_TRANSFER_FUNCTION_IR_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sDiffTemp,
            int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection,
            int lOrientation, int lPedestalFilter, int lSmoothing, int lNumFreqPoints, int lCorrectionCurveIndex);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void MODULATION_TRANSFER_FUNCTION_IR_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void MODULATION_TRANSFER_FUNCTION_IR_FETCH(float freqMtf, ref int status);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView,
            float sDiffTemp, int lTargetPos, int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY,
            int lCameraSelection, float sBeginTemp, float sEndTemp, float sTempInterval, int lAmbBlockTopLeftX, int lAmbBlockTopLeftY, int lAmbBlockBotRightX,
            int lAmblockBotRightY);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH(ref float NETD, ref int status);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void CHANNEL_INTEGRITY_IR_SETUP(int lSource, int lNumFrames, float sHFieldOfView, float sVFieldOfView, float sDiffTemp, int lTargetPos,
            int lCenterX, int lCenterY, int lSBlockTopLeftX, int lSBlockTopLeftY, int lSBlockBotRightX, int lSBlockBotRightY, int lCameraSelection, float linesPerChannel,
            float linesFirstChannel, float noiseCriteria);

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void CHANNEL_INTEGRITY_IR_INITIATE();

        [DllImport("C:\\IRWin2001\\VEO2.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern void CHANNEL_INTEGRITY_IR_FETCH(int channelList, ref int status);
        #endregion

        /// <summary>Initiates the module reset.</summary>
        public static void ResetModuleInitiate()
        {
            RESET_MODULE_INITIATE();
        }

        /// <summary>Shuts down IRWIN</summary>
        //public void IrwinShutdown()
        //{
        //    IRWIN_SHUTDOWN();
        //}

        /// <summary>Initiates the sensor stage laser</summary>
        /// <param name="sensorStagePosition">The sensor stage position.</param>
        public void SetSensorStageLaserInitiate(int sensorStagePosition)
        {
            SET_SENSOR_STAGE_LASER_INITIATE(sensorStagePosition);
        }

        /// <summary>Sets the sensor stage laser fetch.</summary>
        /// <param name="sensorStagePosition">The sensor stage position.</param>
        public void SetSensorStageLaserFetch(ref int sensorStagePosition)
        {
            SET_SENSOR_STAGE_LASER_FETCH(ref sensorStagePosition);
        }

        /// <summary>Sets the source stage laser fetch.</summary>
        /// <param name="sourceStagePosition">The source stage position.</param>
        public void SetSourceStageLaserFetch(ref int sourceStagePosition)
        {
            SET_SOURCE_STAGE_LASER_FETCH(ref sourceStagePosition);
        }
    }
}
