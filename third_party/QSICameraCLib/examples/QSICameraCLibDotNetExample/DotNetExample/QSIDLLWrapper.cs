using System;
using System.Linq;
using System.Text;
namespace DotNetExample
{
    public interface IQSIDLL
    {
        //
        // Handle Methods        
        //
        #region HandleMethods
        uint CreateHandle();
        bool ReleaseHandle(uint handle);
        #endregion

        //
        // Camera Methods        
        //
        #region CameraMethods
        bool AbortExposure(uint handle);
        bool AvailableCameraCount(uint handle, out int count);
        // INFO: Don't use this for now
        int AvailableCameraList(uint handle, IntPtr list, int listLength);
        bool AvailableCameras(uint handle, ref string[] list, int listLength);
        bool CancelTriggerMode(uint handle);
        bool Connect(uint handle);
        bool DeleteFilterWheel(uint handle, string pName, int nameLength);
        bool Disconnect(uint handle);
        bool EnableHostTimedExposure(uint handle, int value);
        bool EnableShutterStatusOutput(uint handle, int value);
        bool EnableTriggerMode(uint handle, QSI_TriggerMode triggerMode, QSI_TriggerPolarity triggerPolarity);
        bool ManualShutterOpen(uint handle, bool value);
        bool NewFilterWheel(uint handle, string pName, int nameLength);
        bool PulseGuide(uint handle, QSI_GuideDirection guideDirection, long duration);
        bool ReadImage(uint handle, ref ushort[] pBuffer, int bufferLength);
        // 32bit long in c++, int in c#
        //bool ReadImageLong(uint handle, ref int[] pBuffer, int bufferLength);
        bool SelectFilterWheel(uint handle, string pName, int nameLength);
        bool ShowCameraSetupDialog(uint handle);
        bool ShowFilterWheelSetupDialog(uint handle);
        bool StartExposure(uint handle, double duration, bool light);
        bool StopExposure(uint handle);
        bool TerminatePendingTrigger(uint handle);
        #endregion CameraMethods

        //
        // GET        
        //
        #region GET
        bool GetCameraState(uint handle, out QSI_CameraState pValue);
        bool GetCanAbortExposure(uint handle, out bool pValue);
        bool GetCanAsymmetricBin(uint handle, out bool pValue);
        bool GetCanGetCoolerPower(uint handle, out bool pValue);
        bool GetCanGetPCBTemperature(uint handle, out bool pValue);
        bool GetCanPulseGuide(uint handle, out bool pValue);
        bool GetCanSetAntiBlooming(uint handle, out bool pValue);
        bool GetCanSetCCDTemperature(uint handle, out bool pValue);
        bool GetCanSetGainMode(uint handle, out bool pValue);
        bool GetCanSetLEDEnabled(uint handle, out bool pValue);
        bool GetCanSetPreExposureFlush(uint handle, out bool pValue);
        bool GetCanSetReadoutSpeed(uint handle, out bool pValue);
        bool GetCanSetShutterPriority(uint handle, out bool pValue);
        bool GetCanSetSoundEnabled(uint handle, out bool pValue);
        bool GetCanStopExposure(uint handle, out bool pValue);
        bool GetCCDTemperature(uint handle, ref double pValue);
        bool GetConnectedState(uint handle, ref bool pValue);
        bool GetCoolerPower(uint handle, out double pValue);
        bool GetDescription(uint handle, ref StringBuilder pBuffer, int bufferLength);
        bool GetDriverInfo(uint handle, ref string pBuffer, int bufferLength);
        bool GetElectronsPerADU(uint handle, out double pValue);
        bool GetFilterWheelNames(uint handle, ref string[] list, out int listLength);
        bool GetFilterWheelPositionCount(uint handle, out int pValue);
        bool GetFullWellCapacity(uint handle, out double pValue);
        bool GetHasFilterWheel(uint handle, out bool pValue);
        bool GetHasShutter(uint handle, out bool pValue);
        bool GetHeatSinkTemperature(uint handle, out double pValue);
        bool GetImageReady(uint handle, out bool pValue);
        bool GetImageSizeX(uint handle, out int pValue);
        bool GetImageSizeY(uint handle, out int pValue);
        bool GetIsPulseGuiding(uint handle, out bool pValue);
        bool GetLastError(uint handle, ref string pBuffer, int bufferLength);
        bool GetLastExposureDuration(uint handle, out double pValue);
        bool GetLastExposureStartTime(uint handle, ref StringBuilder pBuffer, int bufferLength);
        bool GetMaxADU(uint handle, out int pValue);
        bool GetMaxBinX(uint handle, out int pValue);
        bool GetMaxBinY(uint handle, out int pValue);
        bool GetMaxExposureTime(uint handle, ref double pValue);
        bool GetMinExposureTime(uint handle, ref double pValue);
        bool GetModelName(uint handle, out string pBuffer, int bufferLength);
        bool GetModelNumber(uint handle, out string pBuffer, int bufferLength);
        bool GetPCBTemperature(uint handle, ref double pValue);
        bool GetPixelSizeX(uint handle, ref double pValue);
        bool GetPixelSizeY(uint handle, ref double pValue);
        bool GetPowerOfTwoBinning(uint handle, out bool pValue);
        bool GetSelectedFilterWheelName(uint handle, ref string pBuffer, int bufferLength);
        #endregion

        //
        // GET/SET        
        //
        #region GET/SET
        bool GetAntiBlooming(uint handle, out QSI_AntiBloom pValue);
        bool SetAntiBlooming(uint handle, QSI_AntiBloom value);
        bool GetBinX(uint handle, out int pValue);
        bool SetBinX(uint handle, int value);
        bool GetBinY(uint handle, out int pValue);
        bool SetBinY(uint handle, int value);
        bool GetCCDTemperatureSetpoint(uint handle, out double pValue);
        bool SetCCDTemperatureSetpoint(uint handle, double value);
        bool GetCoolerOn(uint handle, out bool pValue);
        bool SetCoolerOn(uint handle, bool value);
        bool GetFanMode(uint handle, out QSI_FanMode pValue);
        bool SetFanMode(uint handle, QSI_FanMode value);
        bool GetFilterWheelFocusOffsets(uint handle, long[] pValues, out int valuesLength);
        bool SetFilterWheelFocusOffsets(uint handle, long[] pValues, int valuesLength);
        bool GetFilterWheelPosition(uint handle, out int pValue);
        bool SetFilterWheelPosition(uint handle, int pValue);
        bool GetFilterWheelPositionNames(uint handle, string[] list, out int listLength);
        bool SetFilterWheelPositionNames(uint handle, ref string[] list, int listLength);
        bool GetGainMode(uint handle, out QSI_GainMode pValue);
        bool SetGainMode(uint handle, QSI_GainMode value);
        bool GetImageNumX(uint handle, out int pValue);
        bool SetImageNumX(uint handle, int value);
        bool GetImageNumY(uint handle, out int pValue);
        bool SetImageNumY(uint handle, int value);
        bool GetImageStartX(uint handle, out int pValue);
        bool SetImageStartX(uint handle, int value);
        bool GetImageStartY(uint handle, out int pValue);
        bool SetImageStartY(uint handle, int value);
        bool GetIsMainCamera(uint handle, out bool pValue);
        bool SetIsMainCamera(uint handle, int value);
        bool GetManualShutterMode(uint handle, out bool pValue);
        bool SetManualShutterMode(uint handle, int value);
        bool GetMaskPixels(uint handle, out bool pValue);
        bool SetMaskPixels(uint handle, bool value);
        bool GetPixelMask(uint handle, int[] pValuesX, int[] pValuesY, ref int valuesLength);
        bool SetPixelMask(uint handle, int[] pValuesX, int[] pValuesY, int valuesLength);
        bool GetPreExposureFlush(uint handle, out QSI_PreExposureFlush pValue);
        bool SetPreExposureFlush(uint handle, QSI_PreExposureFlush value);
        bool GetLEDEnabled(uint handle, out bool pValue);
        bool SetLEDEnabled(uint handle, bool value);
        bool GetReadoutSpeed(uint handle, out QSI_ReadoutSpeed pValue);
        bool SetReadoutSpeed(uint handle, QSI_ReadoutSpeed value);
        bool GetSerialNumber(uint handle, out string pBuffer, int bufferLength);
        bool SetSerialNumber(uint handle, string pBuffer, int bufferLength);
        bool GetShutterPriority(uint handle, out QSI_ShutterPriority pValue);
        bool SetShutterPriority(uint handle, QSI_ShutterPriority value);
        bool GetSoundEnabled(uint handle, out bool pValue);
        bool SetSoundEnabled(uint handle, bool value);
        #endregion

        #region Extensions
        bool GetCanSetShutterPriorityEx(uint handle, out bool pValue);

        //bool GetPixelMaskEx(uint handle, out QSIPixelMasks pixelMasks);
        //bool SetPixelMaskEx(uint handle, QSIPixelMasks pixelMasks);
        #endregion Extensions
    }

    public static class QSIDLL
    {
        static QSIDLLWrapper direct = new QSIDLLWrapper();
        public static IQSIDLL Direct { get { return direct; } }
    }

    public static class IQSIDLLExtender
    {
        public static bool Subframe(this IQSIDLL dll, uint handle, int startX, int startY, int width, int height)
        {
            var x = dll.SetImageStartX(handle, startX);
            var y = dll.SetImageStartY(handle, startY);
            var w = dll.SetImageNumX(handle, width);
            var h = dll.SetImageNumY(handle, height);

            return x && y && w && h;
        }

        public static bool Bin(this IQSIDLL dll, uint handle, int binX, int binY)
        {
            var x = dll.SetBinX(handle, binX);
            var y = dll.SetBinY(handle, binY);

            return x && y;
        }
    }

    public class QSIDLLWrapper : IQSIDLL
    {
        bool IsOK(int result)
        {
            return result == 0 ? true : false;
        }

        static Object padlock = new Object();
        //
        // Handle Methods        
        //
        #region HandleMethods

        public uint CreateHandle()
        {
            lock (padlock)
            {
                uint handle = 0;
                if (QSIPInvoke.QSI_CreateHandle(ref handle) == 0)
                    return handle;

                return 0;
            }
        }

        public bool ReleaseHandle(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_ReleaseHandle(handle));
            }
        }

        #endregion HandleMethods

        //
        // Camera Methods        
        //
        #region CameraMethods
        public bool AbortExposure(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_AbortExposure(handle));
            }
        }

        public bool AvailableCameraCount(uint handle, out int count)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_AvailableCameraCount(handle, out count));
            }
        }

        // INFO: Don't use this for now
        public int AvailableCameraList(uint handle, IntPtr list, int listLength)
        {
            return 0;
        }

        public bool AvailableCameras(uint handle, ref string[] list, int listLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_AvailableCameras(handle, list, listLength));
            }
        }

        public bool CancelTriggerMode(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_CancelTriggerMode(handle));
            }
        }

        public bool Connect(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_Connect(handle));

            }
        }
        public bool DeleteFilterWheel(uint handle, string pName, int nameLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_DeleteFilterWheel(handle, pName, nameLength));

            }
        }
        public bool Disconnect(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_Disconnect(handle));

            }
        }
        public bool EnableHostTimedExposure(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_EnableHostTimedExposure(handle, value));

            }
        }
        public bool EnableShutterStatusOutput(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_EnableShutterStatusOutput(handle, value));
            }
        }
        public bool EnableTriggerMode(uint handle, QSI_TriggerMode triggerMode, QSI_TriggerPolarity triggerPolarity)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_EnableTriggerMode(handle, triggerMode, triggerPolarity));
            }
        }
        public bool ManualShutterOpen(uint handle, bool value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_ManualShutterOpen(handle, value ? 1 : 0));
            }
        }
        public bool NewFilterWheel(uint handle, string pName, int nameLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_NewFilterWheel(handle, pName, nameLength));

            }
        }
        public bool PulseGuide(uint handle, QSI_GuideDirection guideDirection, long duration)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_PulseGuide(handle, guideDirection, duration));

            }
        }
        public bool ReadImage(uint handle, ref ushort[] pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_ReadImage(handle, pBuffer, bufferLength));

            }
        }

        // Not Implemented on interface
        //public bool ReadImageLong(uint handle, ref int[] pBuffer, int bufferLength)
        //{
        //    lock (padlock)
        //    {
        //        return IsOK(QSIPInvoke.QSI_ReadImageLong(handle, ref pBuffer, bufferLength));
        //
        //    }
        //}
        public bool SelectFilterWheel(uint handle, string pName, int nameLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SelectFilterWheel(handle, pName, nameLength));

            }
        }
        public bool ShowCameraSetupDialog(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_ShowCameraSetupDialog(handle));

            }
        }
        public bool ShowFilterWheelSetupDialog(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_ShowFilterWheelSetupDialog(handle));

            }
        }
        public bool StartExposure(uint handle, double duration, bool light)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_StartExposure(handle, duration, light ? 1 : 0));

            }
        }
        public bool StopExposure(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_StopExposure(handle));

            }
        }
        public bool TerminatePendingTrigger(uint handle)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_TerminatePendingTrigger(handle));

            }
        }
        #endregion CameraMethods

        //
        //GET
        //
        #region GET
        public bool GetCameraState(uint handle, out QSI_CameraState pValue)
        {

            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetCameraState(handle, out pValue));
            }
        }
        public bool GetCanAbortExposure(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanAbortExposure(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanAsymmetricBin(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanAsymmetricBin(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanGetCoolerPower(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanGetCoolerPower(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanGetPCBTemperature(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanGetPCBTemperature(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanPulseGuide(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanPulseGuide(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetAntiBlooming(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetAntiBlooming(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetCCDTemperature(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetCCDTemperature(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetGainMode(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetGainMode(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetLEDEnabled(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetLEDEnabled(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetPreExposureFlush(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetPreExposureFlush(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetReadoutSpeed(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetReadoutSpeed(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetShutterPriority(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetShutterPriority(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanSetSoundEnabled(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetSoundEnabled(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCanStopExposure(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanStopExposure(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCCDTemperature(uint handle, ref double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetCCDTemperature(handle, ref pValue));
            }
        }
        public bool GetConnectedState(uint handle, ref bool pValue)
        {
            lock (padlock)
            {
                int val;
                var isOk = IsOK(QSIPInvoke.QSI_GetConnectedState(handle, out val));
                pValue = val != 0;
                return isOk;
            }
        }
        public bool GetCoolerPower(uint handle, out double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetCoolerPower(handle, out pValue));
            }
        }
        public bool GetDescription(uint handle, ref StringBuilder pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetDescription(handle, pBuffer, bufferLength));
            }
        }
        public bool GetDriverInfo(uint handle, ref string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetDriverInfo(handle, pBuffer, bufferLength));
            }
        }
        public bool GetElectronsPerADU(uint handle, out double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetElectronsPerADU(handle, out pValue));
            }
        }
        public bool GetFilterWheelNames(uint handle, ref string[] list, out int listLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFilterWheelNames(handle, list, out listLength));
            }
        }
        public bool GetFilterWheelPositionCount(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFilterWheelPositionCount(handle, out pValue));
            }
        }
        public bool GetFullWellCapacity(uint handle, out double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFullWellCapacity(handle, out pValue));
            }
        }
        public bool GetHasFilterWheel(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetHasFilterWheel(handle, out value));
                pValue = value != 0;
                return result;
            }
        }
        public bool GetHasShutter(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetHasShutter(handle, out value));
                pValue = value != 0;
                return result;
            }
        }
        public bool GetHeatSinkTemperature(uint handle, out double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetHeatSinkTemperature(handle, out pValue));
            }
        }
        public bool GetImageReady(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetImageReady(handle, out value));
                pValue = value != 0;
                return result;
            }
        }
        public bool GetImageSizeX(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetImageSizeX(handle, out pValue));
            }
        }
        public bool GetImageSizeY(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetImageSizeY(handle, out pValue));
            }
        }
        public bool GetIsPulseGuiding(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetIsPulseGuiding(handle, out value));
                pValue = value != 0;
                return result;
            }
        }
        public bool GetLastError(uint handle, ref string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetLastError(handle, pBuffer, bufferLength));
            }
        }
        public bool GetLastExposureDuration(uint handle, out double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetLastExposureDuration(handle, out pValue));
            }
        }
        public bool GetLastExposureStartTime(uint handle, ref StringBuilder pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetLastExposureStartTime(handle, pBuffer, bufferLength));
            }
        }
        public bool GetMaxADU(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetMaxADU(handle, out pValue));
            }
        }
        public bool GetMaxBinX(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetMaxBinX(handle, out pValue));
            }
        }
        public bool GetMaxBinY(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetMaxBinY(handle, out pValue));
            }
        }
        public bool GetMaxExposureTime(uint handle, ref double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetMaxExposureTime(handle, ref pValue));
            }
        }
        public bool GetMinExposureTime(uint handle, ref double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetMinExposureTime(handle, ref pValue));
            }
        }
        public bool GetModelName(uint handle, out string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                var temp = new StringBuilder(bufferLength);
                var res = QSIPInvoke.QSI_GetModelName(handle, temp, bufferLength);
                var isOk = IsOK(res);
                pBuffer = temp.ToString();
                return isOk;
            }
        }
        public bool GetModelNumber(uint handle, out string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                var temp = new StringBuilder(bufferLength);
                var res = QSIPInvoke.QSI_GetModelNumber(handle, temp, bufferLength);
                var isOk = IsOK(res);
                pBuffer = temp.ToString();
                return isOk;
            }
        }
        public bool GetPCBTemperature(uint handle, ref double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetPCBTemperature(handle, ref pValue));
            }
        }
        public bool GetPixelSizeX(uint handle, ref double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetPixelSizeX(handle, ref pValue));
            }
        }
        public bool GetPixelSizeY(uint handle, ref double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetPixelSizeY(handle, ref pValue));
            }
        }
        public bool GetPowerOfTwoBinning(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetPowerOfTwoBinning(handle, out value));
                pValue = value != 0;
                return result;
            }
        }
        public bool GetSelectedFilterWheelName(uint handle, ref string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetSelectedFilterWheelName(handle, pBuffer, bufferLength));
            }
        }
        #endregion GET

        //
        //GET/SET
        //
        #region GET/SET

        public bool GetAntiBlooming(uint handle, out QSI_AntiBloom pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetAntiBlooming(handle, out pValue));
            }
        }
        public bool SetAntiBlooming(uint handle, QSI_AntiBloom value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetAntiBlooming(handle, value));
            }
        }
        public bool GetBinX(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetBinX(handle, out pValue));
            }
        }
        public bool SetBinX(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetBinX(handle, value));
            }
        }
        public bool GetBinY(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetBinY(handle, out pValue));
            }
        }
        public bool SetBinY(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetBinY(handle, value));
            }
        }
        public bool GetCCDTemperatureSetpoint(uint handle, out double pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetCCDTemperatureSetpoint(handle, out pValue));
            }
        }
        public bool SetCCDTemperatureSetpoint(uint handle, double value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetCCDTemperatureSetpoint(handle, value));
            }
        }
        public bool GetCoolerOn(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetCoolerOn(handle, ref value));
                pValue = value != 0;
                return result;
            }
        }
        public bool SetCoolerOn(uint handle, bool value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetCoolerOn(handle, value ? 1 : 0));
            }
        }
        public bool GetFanMode(uint handle, out QSI_FanMode pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFanMode(handle, out pValue));
            }
        }
        public bool SetFanMode(uint handle, QSI_FanMode value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetFanMode(handle, value));
            }
        }
        public bool GetFilterWheelFocusOffsets(uint handle, long[] pValues, out int valuesLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFilterWheelFocusOffsets(handle, pValues, out valuesLength));
            }
        }
        public bool SetFilterWheelFocusOffsets(uint handle, long[] pValues, int valuesLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetFilterWheelFocusOffsets(handle, pValues, valuesLength));
            }
        }
        public bool GetFilterWheelPosition(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFilterWheelPosition(handle, out pValue));
            }
        }
        public bool SetFilterWheelPosition(uint handle, int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetFilterWheelPosition(handle, pValue));
            }
        }
        public bool GetFilterWheelPositionNames(uint handle, string[] list, out int listLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetFilterWheelPositionNames(handle, list, out listLength));
            }
        }
        public bool SetFilterWheelPositionNames(uint handle, ref string[] list, int listLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetFilterWheelPositionNames(handle, list, listLength));
            }
        }
        public bool GetGainMode(uint handle, out QSI_GainMode pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetGainMode(handle, out pValue));
            }
        }
        public bool SetGainMode(uint handle, QSI_GainMode value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetGainMode(handle, value));
            }
        }
        public bool GetImageNumX(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetImageNumX(handle, out pValue));
            }
        }
        public bool SetImageNumX(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetImageNumX(handle, value));
            }
        }
        public bool GetImageNumY(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetImageNumY(handle, out pValue));
            }
        }
        public bool SetImageNumY(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetImageNumY(handle, value));
            }
        }
        public bool GetImageStartX(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetImageStartX(handle, out pValue));
            }
        }
        public bool SetImageStartX(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetImageStartX(handle, value));
            }
        }
        public bool GetImageStartY(uint handle, out int pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetImageStartY(handle, out pValue));
            }
        }
        public bool SetImageStartY(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetImageStartY(handle, value));
            }
        }
        public bool GetIsMainCamera(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetIsMainCamera(handle, ref value));
                pValue = value != 0;
                return result;
            }
        }
        public bool SetIsMainCamera(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetIsMainCamera(handle, value));
            }
        }
        public bool GetManualShutterMode(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetManualShutterMode(handle, ref value));
                pValue = value != 0;
                return result;
            }
        }
        public bool SetManualShutterMode(uint handle, int value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetManualShutterMode(handle, value));
            }
        }
        public bool GetMaskPixels(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetMaskPixels(handle, ref value));
                pValue = value != 0;
                return result;
            }
        }
        public bool SetMaskPixels(uint handle, bool value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetMaskPixels(handle, value ? 1 : 0));
            }
        }
        public bool GetPixelMask(uint handle, int[] pValuesX, int[] pValuesY, ref int valuesLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetPixelMask(handle, pValuesX, pValuesY, ref valuesLength));
            }
        }

        public bool GetPixelMask(uint handle, ref int valuesLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetPixelMask(handle, IntPtr.Zero, IntPtr.Zero, ref valuesLength));
            }
        }

        public bool SetPixelMask(uint handle, int[] pValuesX, int[] pValuesY, int valuesLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetPixelMask(handle, pValuesX, pValuesY, valuesLength));
            }
        }
        public bool GetPreExposureFlush(uint handle, out QSI_PreExposureFlush pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetPreExposureFlush(handle, out pValue));
            }
        }
        public bool SetPreExposureFlush(uint handle, QSI_PreExposureFlush value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetPreExposureFlush(handle, value));
            }
        }
        public bool GetLEDEnabled(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetLEDEnabled(handle, ref value));
                pValue = value != 0;
                return result;
            }
        }
        public bool SetLEDEnabled(uint handle, bool value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetLEDEnabled(handle, value ? 1 : 0));
            }
        }
        public bool GetReadoutSpeed(uint handle, out QSI_ReadoutSpeed pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetReadoutSpeed(handle, out pValue));
            }
        }
        public bool SetReadoutSpeed(uint handle, QSI_ReadoutSpeed value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetReadoutSpeed(handle, value));
            }
        }
        public bool GetSerialNumber(uint handle, out string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                var temp = new StringBuilder(bufferLength);
                var isOk = IsOK(QSIPInvoke.QSI_GetSerialNumber(handle, temp, bufferLength));
                pBuffer = temp.ToString();
                return isOk;
            }
        }
        public bool SetSerialNumber(uint handle, string pBuffer, int bufferLength)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetSerialNumber(handle, pBuffer, bufferLength));
            }
        }
        public bool GetShutterPriority(uint handle, out QSI_ShutterPriority pValue)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_GetShutterPriority(handle, out pValue));
            }
        }
        public bool SetShutterPriority(uint handle, QSI_ShutterPriority value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetShutterPriority(handle, value));
            }
        }
        public bool GetSoundEnabled(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int value = 0;
                var result = IsOK(QSIPInvoke.QSI_GetSoundEnabled(handle, ref value));
                pValue = value != 0;
                return result;
            }
        }
        public bool SetSoundEnabled(uint handle, bool value)
        {
            lock (padlock)
            {
                return IsOK(QSIPInvoke.QSI_SetSoundEnabled(handle, value ? 1 : 0));
            }
        }
        #endregion Get/Set

        #region Extensions
        public bool GetCanSetShutterPriorityEx(uint handle, out bool pValue)
        {
            lock (padlock)
            {
                int res1;
                var result = IsOK(QSIPInvoke.QSI_GetHasShutter(handle, out res1));

                int res2;
                var isOk = IsOK(QSIPInvoke.QSI_GetCanSetShutterPriority(handle, out res2));
                pValue = res1 != 0 && res2 != 0;

                return result && isOk;
            }
        }

        //public bool GetPixelMaskEx(uint handle, out QSIPixelMasks pixelMasks)
        //{
        //    int length = 0;
        //    this.GetPixelMask(handle, ref length);

        //    int[] x = new int[length];
        //    int[] y = new int[length];

        //    var res = GetPixelMask(handle, x, y, ref length);

        //    pixelMasks = new QSIPixelMasks(handle, SetPixelMaskEx);

        //    for (var i = 0; i < length; ++i)
        //    {
        //        pixelMasks.Add(new QSIPixelMask(x[i], y[i], pixelMasks));
        //    }

        //    return res;
        //}
        //public bool SetPixelMaskEx(uint handle, QSIPixelMasks pixelMasks)
        //{
        //    var count = pixelMasks.Count;
        //    int[] x = new int[count];
        //    int[] y = new int[count];

        //    for (var i = 0; i < count; ++i)
        //    {
        //        x[i] = pixelMasks[i].X.Value.Value;
        //        y[i] = pixelMasks[i].Y.Value.Value;
        //    }

        //    return SetPixelMask(handle, x, y, count);
        //}
        #endregion Extensions
    }

    //public class QSIPixelMasks : SimpleArray<IQSIPixelMask>
    //{
    //    public QSIPixelMasks()
    //    {
    //        this.handle = 0;
    //    }

    //    public QSIPixelMasks(uint handle, Func<uint, QSIPixelMasks, bool> func)
    //    {
    //        this.handle = handle;
    //        this.func = func;

    //        Updated += Update;
    //    }

    //    public void Update()
    //    {
    //        func.Invoke(handle, this);
    //    }

    //    Func<uint, QSIPixelMasks, bool> func;
    //    uint handle;

    //}

    //public interface IQSIPixelMask
    //{
    //    ISmartRangeInt X { get; }
    //    ISmartRangeInt Y { get; }
    //}

    //public class QSIPixelMask: IQSIPixelMask
    //{
    //    public QSIPixelMask(int X, int Y)
    //    {
    //        this.x = SmartRangeInt.CreateLinear(0, 10000, X, 1);
    //        this.y = SmartRangeInt.CreateLinear(0, 10000, Y, 1);

    //    }

    //    public QSIPixelMask(int X, int Y, QSIPixelMasks parent) : this(X, Y)
    //    {
    //        //x.Updated += parent.Update;
    //        //y.Updated += parent.Update;
    //        x.Value.Updated += parent.Update;
    //        y.Value.Updated += parent.Update;
    //    }

    //    public ISmartRangeInt X { get { return x; } }
    //    public ISmartRangeInt Y { get { return y; } }

    //    readonly SmartRangeInt x;
    //    readonly SmartRangeInt y;
    //}
}