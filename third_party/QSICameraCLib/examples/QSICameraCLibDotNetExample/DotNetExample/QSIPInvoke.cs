using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DotNetExample
{
    // The state of the camera.
    #region Enums
    public enum QSI_CameraState
    {
        QSI_CAMERA_IDLE = 0,        //< At idle state, available to start exposure
        QSI_CAMERA_WAITING = 1,     //< Exposure started but waiting (for shutter, trigger, filter wheel, etc.)
        QSI_CAMERA_EXPOSING = 2,    //< Exposure currently in progress
        QSI_CAMERA_READING = 3,     //< CCD array is being read out (digitized)
        QSI_CAMERA_DOWNLOADING = 4, //< Downloading data to PC
        QSI_CAMERA_ERROR = 5        //< Camera error condition serious enough to prevent further operations (link fail, etc.)
    }

    // The direction when pulse guiding.
    public enum QSI_GuideDirection
    {
        QSI_GUIDE_NORTH = 0,  //< North (+ declination/elevation)
        QSI_GUIDE_SOUTH = 1,  //< South (- declination/elevation)
        QSI_GUIDE_EAST = 2,   //< East (+ right ascension/azimuth)
        QSI_GUIDE_WEST = 3    //< West (- right ascension/azimuth)
    }

    public static class EnumEx
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attr == null ? value.ToString() : attr.Description;
        }
    }

    // The state of the camera's fans.
    public enum QSI_FanMode
    {
        [Description("Off")]
        QSI_FAN_OFF = 0,    //< Fans off
        [Description("Quiet")]
        QSI_FAN_QUIET = 1,  //< Fans at slow speed (but will switch to full if cooling requires)
        [Description("Full Speed")]
        QSI_FAN_FULL = 2    //< Fans at full speed
    }


    // The level of pre-exposure flush.
    public enum QSI_PreExposureFlush
    {
        [Description("None")]
        QSI_FLUSH_NONE = 0,           //< No flushing (0 cycles)
        [Description("Modest")]
        QSI_FLUSH_MODEST = 1,         //< Modest flushing (1 cycle)
        [Description("Normal")]
        QSI_FLUSH_NORMAL = 2,         //< Normal flushing (2 cycles)
        [Description("Aggresive")]
        QSI_FLUSH_AGGRESSIVE = 3,     //< Aggressive flushing (4 cycles)
        [Description("Very Aggresive")]
        QSI_FLUSH_VERY_AGGRESSIVE = 4 //< Very aggressive flushing (8 cycles)
    }


    // The shutter priority mode.
    public enum QSI_ShutterPriority
    {
        [Description("Mechanical")]
        QSI_ShutterPriority_MECHANICAL = 0,  //< The mechanical shutter is closed between exposures and only opens for
                                             //< exposures that are taking an image, i.e. closed for Darks and Bias images.
                                             //< This mode prevents the CCD imager from being flooded with light in between
                                             //< exposures. This can reduce or eliminate "ghost" or residual images when imaging
                                             //< bright objects.
        [Description("Electronic")]
        QSI_ShutterPriority_ELECTRONIC = 1   //< The mechanical shutter is open between exposures and only closes when taking
                                             //< Darks and Bias images. This mode allows the fastest back-to-back image exposures.
    }


    // The gain used during image readout.
    public enum QSI_GainMode
    {
        [Description("High Gain")]
        QSI_GainMode_HIGH = 0, //< High gain mode
        [Description("Low Gain")]
        QSI_GainMode_LOW = 1   //< Low gain mode
    }


    // The anti-bloom mode.
    //
    // @remarks Only applicable on KAI-based cameras.
    public enum QSI_AntiBloom
    {
        [Description("Norma;")]
        QSI_AntiBloom_NORMAL = 0,  //< Normal anti-blooming factor
        [Description("High")]
        QSI_AntiBloom_HIGH = 1     //< High anti-blooming factor
    }


    // The readout speed of the image.
    public enum QSI_ReadoutSpeed
    {
        [Description("High Quality")]
        QSI_READOUT_HIGH_QUALITY = 0, //< High quality readout (less noise but slower read time)
        [Description("Fast Readout")]
        QSI_READOUT_FAST = 1          //< Fast readout (faster read time but more noise)
    }


    // The trigger mode.
    public enum QSI_TriggerMode
    {
        QSI_TRIGGER_SHORT_WAIT = 4, //< Short wait trigger mode
        QSI_TRIGGER_LONG_WAIT = 6   //< Long wait trigger mode
    }


    // The trigger polarity.
    public enum QSI_TriggerPolarity
    {
        QSI_TRIGGER_HIGH_TO_LOW = 0,  //< Trigger on the falling edge
        QSI_TRIGGER_LOW_TO_HIGH = 1   //< Trigger on the rising edge
    }

    /// <summary>
    /// Each function will return a qsi_status which is a concatonation of errors from QSI_Global.h
    /// </summary>
    public class QSIPInvoke
    {
        public static int QSI_HANDLE_INVALID = 0;             //< An invalid handle.
        public static int QSI_LENGTH_SERIAL_NUMBER = 16;            //< The recommended length to use for a camera serial number string.
        public static int QSI_LENGTH_DESCRIPTION = 68;               //< The recommended length to use for a camera description string.
        public static int QSI_LENGTH_DRIVER_INFO = 68;              //< The recommended length to use for a driver information string.
        public static int QSI_LENGTH_MODEL_NAME = 68;               //< The recommended length to use for a camera model name string.
        public static int QSI_LENGTH_MODEL_NUMBER = 68;            //< The recommended length to use for a camera model number string.
        public static int QSI_LENGTH_FILTER_WHEEL_NAME = 64;        //< The recommended length to use for a filter wheel name string.
        public static int QSI_LENGTH_FILTER_WHEEL_POSITION_NAME = 64;  //< The recommended length to use for a filter wheel position name string.
        public static int QSI_LENGTH_ERROR_STRING = 128;             //< The recommended length to use for an error string.
        public static int QSI_LENGTH_LAST_EXPOSURE_START_TIME = 64;  //< The recommended length to use for a last exposure start time string.

        #endregion Enums

        const string DLLPath = "QSICameraCLib.dll";

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_CreateHandle(ref uint handle);
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_ReleaseHandle(uint handle);



        //==================================================================================================
        // Camera Methods
        //==================================================================================================

        ///////////////////////////////////////////////////////////////////
        // Aborts the current exposure, if any, and returns the camera to an idle state.
        //
        // @param handle  The handle.
        //
        // @return An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_StartExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_AbortExposure(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Gets a count of the number of available QSI cameras.
        //
        // @param handle       The handle.
        // @param [out] count  The number of available cameras.
        //
        // @return An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_AvailableCameraList
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_AvailableCameraCount(uint handle, out int count);

        ///////////////////////////////////////////////////////////////////
        // Fills an array of character buffers with the serial numbers of available QSI cameras.
        //
        // @remarks If there are more available cameras than there are entries in the array, then the extra
        //          cameras will be ignored.\n
        //          If there are less available cameras than there
        //          are entries in the array, then the remaining entries will be filled with null
        //          characters.
        // 
        // @param handle       The handle.
        // @param [out] list   A pre-allocated array of character buffers.
        // @param listLength   The length of the array.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_AvailableCameraCount
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int QSI_AvailableCameraList(uint handle, IntPtr list, int listLength);

        ///////////////////////////////////////////////////////////////////
        // Fills an array of character buffers with the serial numbers of available QSI cameras.
        //
        // @remarks If there are more available cameras than there are entries in the array, then the extra
        //          cameras will be ignored.\n
        //          If there are less available cameras than there
        //          are entries in the array, then the remaining entries will be filled with null
        //          characters.
        // 
        // @param handle       The handle.
        // @param [out] list   A pre-allocated array of character buffers.
        // @param listLength   The length of the array.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_AvailableCameraCount
        //
        // Space needs to be created for the array of strings using:
        // string[] strs = Enumerable.Repeat("               ", arrSize).ToArray();
        // before passing the array to this function
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int QSI_AvailableCameras(uint handle, [In, Out] string[] list, int listLength);

        ///////////////////////////////////////////////////////////////////
        // Cancels (disables) the wait for external trigger mode.
        //
        // @param handle  The handle.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_EnableTriggerMode QSI_TerminatePendingTrigger
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_CancelTriggerMode(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Connects to a camera.
        // 
        // If one camera is available and @ref QSI_SetSerialNumber has not been used, then that camera will
        // be connected to.
        // 
        // If a serial number has been specified by using @ref QSI_SetSerialNumber, then that serial number
        // will be used regardless of what cameras are available.
        //
        // If more than one camera is available and @ref QSI_SetSerialNumber has not been used, then the
        // serial number of the last camera connected to will be used.
        // 
        // @remarks A camera must be "connected to" before any calls that relate to camera capabilities are
        //          used.
        //
        // @param handle  The handle.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_Disconnect
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_Connect(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Deletes the specified filter wheel.
        //
        // @param handle       The handle.
        // @param pName        The name.
        // @param nameLength   The length of the name.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelNames QSI_GetFilterWheelPositionNames
        //      QSI_NewFilterWheel QSI_SelectFilterWheel QSI_SetFilterWheelFocusOffsets
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int QSI_DeleteFilterWheel(uint handle, [MarshalAs(UnmanagedType.LPStr)] string pName, int nameLength);

        ///////////////////////////////////////////////////////////////////
        // Disconnects from the camera.
        //
        // Even if an error is returned, the camera connection will still be terminated.
        // 
        // @remarks Don't forget to call @ref QSI_ReleaseHandle if the camera handle is no longer going to
        //          be used.
        // 
        // @param handle  The handle.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_Connect QSI_ReleaseHandle
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_Disconnect(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Enables or disables host timed exposure.
        //
        // On Interline Transfer CCDs, like the KAI-2020M in the QSI 520i, every other column of the CCD is
        // masked to prevent light from striking the underlying pixels. To read an image from an Interline
        // Transfer CCD, the active pixels are transferred to the masked pixels and then shifted out of the
        // CCD.
        // 
        // In normal operation, the active pixels are "flushed" at the beginning of an exposure in order to
        // remove any charge that had built up in the pixels since the last exposure. "HostTimedExposure"
        // mode eliminates this flush allowing you to begin integrating the next exposure while the
        // previous exposure is being transferred to the computer.
        // 
        // This special mode is generally only useful when taking a rapid sequence of short exposures. If
        // significant time is allowed to pass between exposures in this mode, dark current will likely 
        // saturate the CCD. With "HostTimedExposure" mode enabled, it is possible to take small subframes
        // at a rate of multiple images per second, while capturing the majority of the light striking the
        // CCD. This can be useful for some rapid guiding applications.
        // 
        // The "Pre-Exposure Flush" options are ignored when in "HostTimedExposure" mode. The only flushing 
        // that occurs in this mode is to flush the masked columns prior to transferring the image into 
        // them for reading by the computer.
        // 
        // @param handle   The handle.
        // @param value    The value.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_EnableHostTimedExposure(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Enables or disables shutter status output.
        //
        // The guider port can be configured to provide a shutter open/close indication using one of the
        // guider port outputs.  When this feature is enabled, the camera will use the the "up" output
        // (pin 2) to reflect the shutter state.  The camera will pull pin 2 to the common pin 5 while the
        // shutter is open.
        // 
        // The guider ports are opto isolated open collector outputs. Each output is capable of sinking
        // 50ma, 50 VDC maximum.  The common pin must be at ground potential and the "up" output must be
        // pulled up by an external resistor to V+.
        // 
        // This signal is meaningful only when the mechanical shutter is in start/stop mode (an exposure
        // time of greater than 300 milliseconds).
        // 
        // This setting defaults to false on camera power up and is not saved between camera connections
        // or power off.
        // 
        // @remarks Not currently implemented.
        // 
        // @param handle   The handle.
        // @param value    The value.
        //
        // @returns @ref QSI_ERROR_NOT_IMPLEMENTED
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_EnableShutterStatusOutput(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Enables the external trigger mode.
        //
        // After calling this function, a call to @ref QSI_StartExposure will put the camera in a state
        // waiting for an external trigger.
        // 
        // @remarks This feature is only available on RS Series and some OEM cameras.
        //          @par
        //          Non KAI-based cameras support only @ref QSI_TRIGGER_LONG_WAIT.
        //          @par
        //          KAI-based cameras support both @ref QSI_TRIGGER_SHORT_WAIT and
        //          @ref QSI_TRIGGER_LONG_WAIT.
        // 
        // @param handle           The handle.
        // @param triggerMode      The trigger mode.
        // @param triggerPolarity  The trigger polarity.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_CancelTriggerMode QSI_StartExposure QSI_TerminatePendingTrigger
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_EnableTriggerMode(uint handle, QSI_TriggerMode triggerMode, QSI_TriggerPolarity triggerPolarity);

        ///////////////////////////////////////////////////////////////////
        // Manually opens or closes the shutter.
        //
        // Manual shutter mode must be first enabled by calling @ref QSI_SetManualShutterMode.
        // 
        // @param handle   The handle.
        // @param value    The value.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetManualShutterMode QSI_SetManualShutterMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_ManualShutterOpen(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Creates a new filter wheel with the specified name.
        //
        // @param handle       The handle.
        // @param pName        The name of the filter wheel.
        // @param nameLength   The length of the name of the filter wheel.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelNames
        //      QSI_GetFilterWheelPositionNames QSI_SelectFilterWheel QSI_SetFilterWheelFocusOffsets
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int QSI_NewFilterWheel(uint handle, [MarshalAs(UnmanagedType.LPStr)] string pName, int nameLength);

        ///////////////////////////////////////////////////////////////////
        // Activates the guider relay using the supplied arguments.
        //
        // @remarks This method returns immediately after the move has started which allows for 
        //          back-to-back dual axis pulse-guiding.  Use @ref QSI_GetIsPulseGuiding to detect when
        //          all moves have completed.
        //          @par
        //          The pulse duration resolution provided by the camera is in 10ms increments. The 
        //          duration argument to this function is in millisecond increments.
        //          @par
        //          @b Note: Directions are nominal and may depend on exact mount wiring.
        // 
        // @param handle           The handle.
        // @param guideDirection   The guide direction in which the guide-rate motion is to be made.
        // @param duration         The duration of the guide-rate motion (in milliseconds).
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetIsPulseGuiding
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_PulseGuide(uint handle, QSI_GuideDirection guideDirection, long duration);

        ///////////////////////////////////////////////////////////////////
        // Reads an image from the camera.
        //
        // This call is to be made after calling @ref QSI_StartExposure and after @ref QSI_GetImageReady
        // returns @c true.
        // 
        // The size of the image array returned is determined by the dimensions set by @ref
        // QSI_SetImageStartX, @ref QSI_SetImageStartY, @ref QSI_SetImagpublic enumX and @ref QSI_SetImagpublic enumY.
        // The argument @c bufferLength must match this size exactly.
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  The pre-allocated buffer where the image will be written to.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImageReady QSI_ReadImageLong QSI_StartExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_ReadImage(uint handle, [In, Out] ushort[] pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Reads an image from the camera.
        //
        // This call is to be made after calling @ref QSI_StartExposure and after @ref QSI_GetImageReady
        // returns true.
        // 
        // The image returned by this call will be no different than @see QSI_ReadImage except that each
        // pixel value will be stored as a 32-bit integer (instead of an unsigned 16-bit integer).
        // 
        // @remarks Not currently implemented.
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  The pre-allocated buffer where the image will be written to.
        // @param bufferLength   The length of the buffer.
        //
        // @returns @ref QSI_ERROR_NOT_IMPLEMENTED
        //
        // @see QSI_GetImageReady QSI_ReadImage QSI_StartExposure
        //
        //[DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int QSI_ReadImageLong(uint handle, ref int[] pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Selects the specified filter wheel.
        //
        // @param handle       The handle.
        // @param pName        The name of the filter wheel.
        // @param nameLength   The length of the name of the filter wheel.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelNames
        //      QSI_GetFilterWheelPositionNames QSI_NewFilterWheel QSI_SetFilterWheelFocusOffsets
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int QSI_SelectFilterWheel(uint handle, [MarshalAs(UnmanagedType.LPStr)] string pName, int nameLength);

        ///////////////////////////////////////////////////////////////////
        // Shows the camera setup dialog window.
        // 
        // Launches a configuration dialog box for the camera.  The call will not return until the user
        // clicks OK or Cancel manually. The camera must not be connected to in order to change setup
        // values for the camera.
        // 
        // @remarks Not currently implemented.
        //
        // @param handle   The handle.
        //
        // @returns @ref QSI_ERROR_NOT_IMPLEMENTED
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_ShowCameraSetupDialog(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Shows the filter wheel setup dialog window.
        // 
        // @remarks Not currently implemented.
        //
        // @param handle   The handle.
        //
        // @returns @ref QSI_ERROR_NOT_IMPLEMENTED
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_ShowFilterWheelSetupDialog(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Starts an exposure.
        // 
        // Use @ref QSI_GetImageReady to check when the exposure is complete.
        //
        // @remarks If the camera does not have an internal shutter, then the value of the @c light
        //          parameter is ignored.
        // 
        // @param handle     The handle.
        // @param duration   The duration of the exposure (in seconds).
        // @param light      True for light frame and false for dark frame.  Ignored if no shutter present.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_AbortExposure QSI_GetImageReady QSI_ReadImage QSI_StopExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_StartExposure(uint handle, double duration, int light);

        ///////////////////////////////////////////////////////////////////
        // Stops the current exposure.
        //
        // If an exposure is in progress, the readout process is initiated.  Ignored if readout is already
        // in progress.
        // 
        // @param handle   The handle.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanStopExposure QSI_GetImageReady QSI_ReadImage QSI_StartExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_StopExposure(uint handle);

        ///////////////////////////////////////////////////////////////////
        // Terminates the wait for a pending external trigger.
        // 
        // This call would be used after calling @ref QSI_EnableTriggerMode and @ref QSI_StartExposure.
        //
        // @param handle   The handle.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_CancelTriggerMode QSI_EnableTriggerMode QSI_StartExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_TerminatePendingTrigger(uint handle);


        //==================================================================================================
        // Camera Properties - Get
        //==================================================================================================

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating the current camera state.
        //
        // @param handle         The handle.
        // @param [out] pValue   The current camera state.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCameraState(uint handle, out QSI_CameraState pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera can abort an exposure.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera can abort an exposure; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_AbortExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanAbortExposure(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports asymmetric binning.
        //
        // If @c true, the camera can have different binning on the X and Y axes. If @c false, the binning
        // must be equal on the X and Y axes.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports asymmetric binning; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetMaxBinX QSI_GetMaxBinY QSI_GetBinX QSI_GetBinY QSI_SetBinX QSI_SetBinY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanAsymmetricBin(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera can report the cooler power level.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera can report the cooler power level; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCoolerPower
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanGetCoolerPower(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera can report the PCB (printed circuit board)
        // temperature.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera can report the PCB temperature; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetPCBTemperature
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanGetPCBTemperature(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports pulse guiding.
        //
        // Returns @c true if the camera can send auto-guider pulses to the telescope mount; @c false if
        // not.
        // 
        // @b Note: This does not provide any indication of whether the auto-guider cable is actually
        // connected.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports pulse guiding; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_PulseGuide QSI_GetIsPulseGuiding
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanPulseGuide(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports anti-blooming.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports anti-blooming; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetAntiBlooming QSI_SetAntiBlooming
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetAntiBlooming(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports setting of the CCD temperature setpoint.
        //
        // If @c true, the camera's cooler set point can be adjusted. If @c false, the camera either uses
        // open-loop cooling or does not have the ability to adjust temperature from software, and setting
        // the setpoint will have no effect.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports setting of the CCD temperature setpoint;
        //                       otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCCDTemperatureSetpoint QSI_SetCCDTemperatureSetpoint
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetCCDTemperature(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera has multiple gain modes that can be set.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera has multiple gain modes; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetGainMode QSI_SetGainMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetGainMode(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports disabling of the LED.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports disabling of the LED; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetLEDEnabled QSI_SetLEDEnabled
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetLEDEnabled(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports setting of pre-exposure flush.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports setting of pre-exposure flush; otherwise,
        //                       false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetPreExposureFlush QSI_SetPreExposureFlush
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetPreExposureFlush(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera has multiple readout modes.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera has multiple readout modes; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetReadoutSpeed QSI_SetReadoutSpeed
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetReadoutSpeed(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports setting the shutter priority.
        //
        // Shutter priority is only applicable on cameras that have an electronic shutter with a mechanical
        // shutter installed.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports setting the shutter priority; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetShutterPriority QSI_SetShutterPriority
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetShutterPriority(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera supports disabling of the internal speaker.
        //
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera supports disabling of the internal speaker; otherwise,
        //                       false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetSoundEnabled QSI_SetSoundEnabled
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanSetSoundEnabled(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera can stop an exposure.
        //
        // Some cameras support stopping an in progress exposure, which allows the exposure to be
        // terminated before the exposure timer completes, but will still read out the image.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera can stop an exposure; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_StopExposure
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCanStopExposure(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the current CCD temperature (in degrees Celsius).
        //
        // @param handle         The handle.
        // @param [out] pValue   The CCD temperature.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCCDTemperature(uint handle, ref double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the connection state of the camera.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if connected; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_Connect QSI_Disconnect
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetConnectedState(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the current cooler power level (in percent).
        //
        // @param handle         The handle.
        // @param [out] pValue   The cooler power level.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanGetCoolerPower QSI_GetCoolerOn QSI_SetCoolerOn
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCoolerPower(uint handle, out double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the description of the camera.
        //
        // The string shall not exceed 68 characters (for compatibility with FITS headers).
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer to store the description string.
        // @param bufferLength   The length of the buffer.
        // 
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetDescription(uint handle, StringBuilder pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets information about the driver (such as the version).
        //
        // The string shall not exceed 68 characters (for compatibility with FITS headers).
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer to store the driver information string.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetDriverInfo(uint handle, [In, Out] string pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the current gain of the camera (in photoelectrons per A/D unit).
        // 
        // @remarks  This value will change depending on the current gain mode of the camera.
        //
        // @param handle         The handle.
        // @param [out] pValue   The gain of the camera in photoelectrons per A/D unit.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetGainMode QSI_SetGainMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetElectronsPerADU(uint handle, out double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets either the number of saved filter wheels or a list of names of saved filter wheels.
        //
        // @param handle               The handle.
        // @param [out] list           The list of pre-allocated character buffers for storing the names of
        //                             the filter wheel names.
        // @param [in,out] listLength  If @p list is null, the size needed for the list is returned.\n
        //                             If @p list is non-null, the length of the list.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelPositionNames
        //      QSI_NewFilterWheel QSI_SelectFilterWheel QSI_SetFilterWheelFocusOffsets
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFilterWheelNames(uint handle, [Out] string[] list, out int listLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the number of positions on the filter wheel.
        //
        // @param handle         The handle.
        // @param [out] pValue   The number of positions on the filter wheel.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetFilterWheelPosition QSI_SetFilterWheelPosition
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFilterWheelPositionCount(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the current full well capacity of the camera (in electrons).
        // 
        // @remarks  This value will change depending on the current gain mode and binning values of the
        //           camera.
        //
        // @param handle         The handle.
        // @param [out] pValue   The gain of the camera in photoelectrons per A/D unit.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFullWellCapacity(uint handle, out double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera has an internal filter wheel.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera has a filter wheel; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetHasFilterWheel(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera has an internal shutter.
        //
        // @remarks If the camera does not have an internal shutter, @ref QSI_StartExposure will ignore the
        //          @c light parameter.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera has a shutter; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetHasShutter(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the temperature of the heatsink (i.e., body) of the camera (in degrees Celsius).
        //
        // @param handle         The handle.
        // @param [out] pValue   The temperature of the heatsink.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetHeatSinkTemperature(uint handle, out double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether an image is ready to be read.
        //
        // If the value returned is @c false, then any calls to @ref QSI_ReadImage will fail.
        // 
        // @param handle         The handle.
        // @param [out] pValue   If non-null, the value.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_StartExposure QSI_ReadImage QSI_ReadImageLong
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageReady(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the width of the CCD sensor in un-binned pixels.
        //
        // @param handle         The handle.
        // @param [out] pValue   The width of the CCD chip in un-binned pixels.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImageSizeY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageSizeX(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the height of the CCD sensor in un-binned pixels.
        //
        // @param handle         The handle.
        // @param [out] pValue   The height of the CCD chip in un-binned pixels.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImageSizeX
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageSizeY(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera is currently pulse guiding.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if the camera is pulse guiding; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanPulseGuide QSI_PulseGuide
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetIsPulseGuiding(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a description for the last error that occurred.
        //
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer where the error message is to be written.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetLastError(uint handle, [In, Out] string pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the duration of the last exposure.
        // 
        // @remarks This value may differ from the exposure time requested due to shutter latency, camera
        // timing precision, etc.
        //
        // @param handle         The handle.
        // @param [out] pValue   The duration of the last exposure.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetLastExposureStartTime
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetLastExposureDuration(uint handle, out double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the start time of the last exposure.
        // 
        // The format used is the FITs-standard <tt>CCYY-MM-DDThh:mm:ss[.sss...]</tt>.
        //
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer where the start time is to be written.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetLastExposureDuration
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetLastExposureStartTime(uint handle, StringBuilder pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the maximum ADU value that the camera can produce.
        //
        // @param handle         The handle.
        // @param [out] pValue   The maximum ADU value.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetMaxADU(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the maximum allowed X-axis binning factor.
        //
        // @remarks If @ref QSI_GetCanAsymmetricBin reports false, then the value returned is also valid
        //          for the Y-axis.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The maximum allowed X-axis binning factor.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanAsymmetricBin QSI_GetMaxBinY QSI_GetBinX QSI_GetBinY QSI_SetBinX QSI_SetBinY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetMaxBinX(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the maximum allowed Y-axis binning factor.
        //
        // @remarks If @ref QSI_GetCanAsymmetricBin reports false, then the value returned is also valid
        //          for the X-axis.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The maximum allowed Y-axis binning factor.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanAsymmetricBin QSI_GetMaxBinX QSI_GetBinX QSI_GetBinY QSI_SetBinX QSI_SetBinY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetMaxBinY(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the maximum allowed exposure time (in seconds).
        // 
        // @param handle         The handle.
        // @param [out] pValue   The maximum allowed exposure time.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetMinExposureTime
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetMaxExposureTime(uint handle, ref double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the minimum allowed exposure time (in seconds).
        // 
        // @param handle         The handle.
        // @param [out] pValue   The minimum allowed exposure time.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetMinExposureTime
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetMinExposureTime(uint handle, ref double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the model name of the camera.
        // 
        // The string shall not exceed 68 characters (for compatibility with FITS headers).
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer where the model name is to be written.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetModelName(uint handle, StringBuilder pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the model number of the camera.
        // 
        // The string shall not exceed 68 characters (for compatibility with FITS headers).
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer where the model name is to be written.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetModelNumber(uint handle, StringBuilder pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the current PCB (printed circuit board) temperature (in degrees Celsius).
        // 
        // @param handle         The handle.
        // @param [out] pValue   The PCB temperature.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanGetPCBTemperature
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPCBTemperature(uint handle, ref double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the physical width of the CCD sensor pixels (in microns).
        // 
        // @param handle         The handle.
        // @param [out] pValue   The width of the pixels.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetPixelSizeY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPixelSizeX(uint handle, ref double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the physical height of the CCD sensor pixels (in microns).
        // 
        // @param handle         The handle.
        // @param [out] pValue   The height of the pixels.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetPixelSizeX
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPixelSizeY(uint handle, ref double pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera can only bin in powers of two.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if the camera can only bin in powers of two; otherwise, false.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPowerOfTwoBinning(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the name of the currently selected filter wheel.
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  A pre-allocated character buffer where the selected filter wheel name is
        //                       to be written.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SelectFilterWheel
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetSelectedFilterWheelName(uint handle, [In, Out] string pBuffer, int bufferLength);


        //==================================================================================================
        // Camera Properties - Get & Set
        //==================================================================================================

        ///////////////////////////////////////////////////////////////////
        // Gets the current anti-bloom setting.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The anti-bloom setting.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetAntiBlooming QSI_SetAntiBlooming
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetAntiBlooming(uint handle, out QSI_AntiBloom pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the anti-bloom setting.
        // 
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        // 
        // @param handle       The handle.
        // @param [out] value  The anti-bloom setting.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetAntiBlooming QSI_GetCanSetAntiBlooming
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetAntiBlooming(uint handle, QSI_AntiBloom value);

        ///////////////////////////////////////////////////////////////////
        // Gets the current X-axis binning factor.
        // 
        // @remarks Defaults to 1 when the camera link is established.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The X-axis binning factor.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanAsymmetricBin QSI_GetMaxBinX QSI_GetMaxBinY QSI_GetBinY QSI_SetBinX QSI_SetBinY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetBinX(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the X-axis binning factor.
        // 
        // @remarks The driver does not check for compatible sub-frame values when this value is set;
        //          rather they are checked upon @ref QSI_StartExposure.
        //          
        // @param handle   The handle.
        // @param value    The X-axis binning factor.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanAsymmetricBin QSI_GetMaxBinX QSI_GetMaxBinY QSI_GetBinX QSI_GetBinY QSI_SetBinY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetBinX(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the current Y-axis binning factor.
        // 
        // @remarks Defaults to 1 when the camera link is established.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The Y-axis binning factor.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanAsymmetricBin QSI_GetMaxBinX QSI_GetMaxBinY QSI_GetBinX QSI_SetBinX QSI_SetBinY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetBinY(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the Y-axis binning factor.
        // 
        // @remarks The driver does not check for compatible sub-frame values when this value is set;
        //          rather they are checked upon @ref QSI_StartExposure.
        //
        // @param handle   The handle.
        // @param value    The Y-axis binning factor.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanAsymmetricBin QSI_GetMaxBinX QSI_GetMaxBinY QSI_GetBinX QSI_GetBinY QSI_SetBinX
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetBinY(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the CCD temperature setpoint (in Celsius).
        //
        // @param handle         The handle.
        // @param [out] pValue   The CCD temperature setpoint.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetCCDTemperature QSI_SetCCDTemperatureSetpoint
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCCDTemperatureSetpoint(uint handle, out double pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the CCD temperature setpoint (in Celsius).
        // 
        // @param handle   The handle.
        // @param value    The CCD temperature setpoint.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetCCDTemperature QSI_GetCCDTemperatureSetpoint
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetCCDTemperatureSetpoint(uint handle, double value);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the cooler is turned on.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if on; false if off.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SetCoolerOn QSI_GetCCDTemperatureSetpoint QSI_SetCCDTemperatureSetpoint
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetCoolerOn(uint handle, ref int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets a value indicating whether the cooler is turned on.
        // 
        // @b Warning: Turning the cooler off when the cooler is operating at high delta-T (typically >20C
        // below ambient) may result in thermal shock.  Repeated thermal shock may lead to damage to the
        // sensor or cooler stack.
        // 
        // @remarks If the cooler is turned on, calling @ref QSI_Disconnect will not turn it off.
        // 
        // @param handle   The handle.
        // @param value    True if on; false if off.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCoolerOn QSI_GetCCDTemperatureSetpoint QSI_SetCCDTemperatureSetpoint
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetCoolerOn(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the current fan mode.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The fan mode.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SetFanMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFanMode(uint handle, out QSI_FanMode pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the fan mode.
        // 
        // Controls the speed of the camera’s cooling fans.
        // 
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    The fan mode.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetFanMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetFanMode(uint handle, QSI_FanMode value);

        ///////////////////////////////////////////////////////////////////
        // Gets the filter wheel focus offsets for the currently selected filter wheel.
        //
        // @param handle                 The handle.
        // @param [out] pValues          A pre-allocated array where the filter wheel focus offsets will be
        //                               stored.
        // @param [in,out] valuesLength  If @p pValues is null, the size needed for the array is
        //                               returned.\n
        //                               If @p pValues is non-null, the length of the array.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelNames QSI_GetFilterWheelPositionNames
        //      QSI_NewFilterWheel QSI_SelectFilterWheel QSI_SetFilterWheelFocusOffsets
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFilterWheelFocusOffsets(uint handle, long[] pValues, out int valuesLength);

        ///////////////////////////////////////////////////////////////////
        // Sets the filter wheel focus offsets for the currently selected filter wheel.
        //
        // The focus offsets are stored in the registry (under the currently selected filter wheel).
        // 
        // @param handle         The handle.
        // @param pValues        The values.
        // @param valuesLength   Length of the values.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelNames
        //      QSI_GetFilterWheelPositionNames QSI_NewFilterWheel QSI_SelectFilterWheel
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetFilterWheelFocusOffsets(uint handle, long[] pValues, int valuesLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the current filter wheel position.
        //
        // The position is a zero-based index number.  For example, position #1 would be represented by the
        // value '0'.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The filter wheel position.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetFilterWheelPositionCount QSI_SetFilterWheelPosition
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFilterWheelPosition(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the filter wheel position.
        // 
        // The position is a zero-based index number.  For example, position #1 would be represented by the
        // value '0'.
        // 
        // @param handle   The handle.
        // @param pValue   The filter wheel position.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetFilterWheelPosition QSI_GetFilterWheelPositionCount
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetFilterWheelPosition(uint handle, int pValue);

        ///////////////////////////////////////////////////////////////////
        // Gets the names of the filter wheel positions for the currently selected filter wheel.
        //
        // @param handle               The handle.
        // @param [out] list           The list of pre-allocated character buffers for storing the names of
        //                             the filter wheel positions.
        // @param [in,out] listLength  If @p list is null, the size needed for the list is returned.\n
        //                             If @p list is non-null, the length of the list.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelNames
        //      QSI_NewFilterWheel QSI_SelectFilterWheel QSI_SetFilterWheelFocusOffsets
        //      QSI_SetFilterWheelPositionNames
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetFilterWheelPositionNames(uint handle, [Out] string[] list, out int listLength);

        ///////////////////////////////////////////////////////////////////
        // Sets the names of the filter wheel positions for the currently selected filter wheel.
        //
        // The names are stored in the registry (under the currently selected filter wheel).
        // 
        // @param handle       The handle.
        // @param [in] list    A list of character buffers where the names of the filter wheel positions
        //                     are located.
        // @param listLength   The length of the list.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_DeleteFilterWheel QSI_GetFilterWheelFocusOffsets QSI_GetFilterWheelNames 
        //      QSI_GetFilterWheelPositionNames QSI_NewFilterWheel QSI_SelectFilterWheel 
        //      QSI_SetFilterWheelFocusOffsets
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetFilterWheelPositionNames(uint handle, [In, Out] string[] list, int listLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the current gain mode used during image readout.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The sub-frame height.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetGainMode QSI_SetGainMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetGainMode(uint handle, out QSI_GainMode pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the gain mode used during image readout.
        // 
        // Controls the amount of camera gain in adjustable gain cameras. Sets the gain of the camera to
        // maximize dynamic range. High gain is the default and provides the greatest sensitivity. Low
        // gain is useful when binning an image where the binned pixels contain more electrons than a 
        // normal un-binned pixel. Sensitivity is lower, but the full dynamic range of the binned image 
        // can be captured.
        // 
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    The gain mode.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetGainMode QSI_GetGainMode
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetGainMode(uint handle, QSI_GainMode value);

        ///////////////////////////////////////////////////////////////////
        // Gets the sub-frame width.
        //
        // Defaults to value returned by @ref QSI_GetImageSizeX.
        // 
        // If binning is active, then the value is in binned pixels.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The sub-frame width.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX QSI_GetImageStartY
        //      QSI_SetImagpublic enumX QSI_SetImagpublic enumY QSI_SetImageStartX QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageNumX(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the sub-frame width.
        // 
        // If binning is active, then the value needs to be in binned pixels.
        // 
        // @param handle   The handle.
        // @param value    The sub-frame width.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX
        //      QSI_GetImageStartY QSI_SetImagpublic enumY QSI_SetImageStartX QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetImageNumX(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the sub-frame height.
        //
        // Defaults to value returned by @ref QSI_GetImageSizeY.
        // 
        // If binning is active, then the value is in binned pixels.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The sub-frame height.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX QSI_GetImageStartY
        //      QSI_SetImagpublic enumX QSI_SetImagpublic enumY QSI_SetImageStartX QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageNumY(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the sub-frame height.
        // 
        // If binning is active, then the value needs to be in binned pixels.
        // 
        // @param handle   The handle.
        // @param value    The sub-frame height.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX
        //      QSI_GetImageStartY QSI_SetImagpublic enumX QSI_SetImageStartX QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetImageNumY(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the sub-frame start position for the X-axis
        //
        // If binning is active, then the value is in binned pixels.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The sub-frame start position for the X-axis.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartY
        //      QSI_SetImagpublic enumX QSI_SetImagpublic enumY QSI_SetImageStartX QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageStartX(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the sub-frame start position for the X-axis.
        // 
        // If binning is active, then the value needs to be in binned pixels.
        // 
        // @param handle   The handle.
        // @param value    The sub-frame start position for the X-axis.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX
        //      QSI_GetImageStartY QSI_SetImagpublic enumX QSI_SetImagpublic enumY QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetImageStartX(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the sub-frame start position for the Y-axis
        //
        // If binning is active, then the value is in binned pixels.
        // 
        // @param handle         The handle.
        // @param [out] pValue   The sub-frame start position for the Y-axis.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX
        //      QSI_SetImagpublic enumX QSI_SetImagpublic enumY QSI_SetImageStartX QSI_SetImageStartY
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetImageStartY(uint handle, out int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the sub-frame start position for the Y-axis.
        // 
        // If binning is active, then the value needs to be in binned pixels.
        // 
        // @param handle   The handle.
        // @param value    The sub-frame start position for the Y-axis.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetImagpublic enumX QSI_GetImagpublic enumY QSI_GetImageSizeX QSI_GetImageSizeY QSI_GetImageStartX
        //      QSI_GetImageStartY QSI_SetImagpublic enumX QSI_SetImagpublic enumY QSI_SetImageStartX
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetImageStartY(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the camera is in the main camera role.
        // 
        // @param handle         The handle.
        // @param [out] pValue   True if in main camera role; false if in guider camera role.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SetIsMainCamera
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetIsMainCamera(uint handle, ref int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets a value indicating whether the camera is in the main camera role.
        // 
        // Some astro-imaging software requires the concept of a camera being in either a @em main or @em
        // guider role.  This API call serves to allow changing between these roles.  The two different 
        // roles have separate areas in the registry where their settings are stored, which allows the
        // same camera to have two different default behaviors depending on the role it is in.
        // 
        // @remarks The camera must not be connected when changing roles.
        // 
        // @param handle   The handle.
        // @param value    True if in main camera role; false if in guider camera role.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetIsMainCamera
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetIsMainCamera(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether manual shutter mode is enabled.
        //
        // @param handle       The handle.
        // @param [in] pValue  True for enabled; false for disabled.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SetManualShutterMode QSI_ManualShutterOpen
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetManualShutterMode(uint handle, ref int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets a value indicating whether manual shutter mode is enabled.
        //
        // @param handle   The handle.
        // @param value    True for enabled; false for disabled.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetManualShutterMode QSI_ManualShutterOpen
        // 
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetManualShutterMode(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the pixel mask should be applied to the image.
        //
        // @param handle       The handle.
        // @param [in] pValue  True for yes; false for no.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SetMaskPixels QSI_GetPixelMask QSI_SetPixelMask
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetMaskPixels(uint handle, ref int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets a value indicating whether the pixel mask should be applied to the image.
        //
        // If the pixel mask is enabled, each pixel specified by @ref QSI_SetPixelMask is replaced with a
        // fixed value.  The value used depends on the zero target for the camera as set by the camera 
        // firmware.  This is usually 200 ADU on 500 Series cameras, 200 on 600 Series cameras in "High
        // Quality" mode and 1000 on 600 Series cameras in "Fast Readout" mode.
        // 
        // The purpose of this feature is to allow for masking of hot pixels that may interfere with
        // post-processing activities, such as auto guiding when astro-imaging. 
        // 
        // @param handle   The handle.
        // @param value    True for yes; false for no.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetMaskPixels QSI_GetPixelMask QSI_SetPixelMask
        // 
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetMaskPixels(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the list of pixels that are to be masked.
        //
        // Pixels are specified by their un-binned X,Y location.
        //
        // @param handle               The handle.
        // @param [out] pValuesX       The list of X-axis coordinates.
        // @param [out] pValuesY       The list of Y-axis coordinates.
        // @param [out] valuesLength   The length of the lists.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetMaskPixels QSI_SetMaskPixels QSI_SetPixelMask
        //
        //TODO: does this need to be IntPtrs instead are they malloc'd in the c code???
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPixelMask(uint handle, int[] pValuesX, int[] pValuesY, ref int valuesLength);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPixelMask(uint handle, IntPtr pValuesX, IntPtr pValuesY, ref int valuesLength);

        ///////////////////////////////////////////////////////////////////
        // Sets the list of pixels that are to be masked.
        //
        // Pixels are specified by their un-binned X,Y location.
        //
        // The coordinates are stored in the registry (on a per serial number basis) and are automatically
        // restored each time the camera is opened.
        // 
        // @param handle         The handle.
        // @param pValuesX       The list of X-axis coordinates.
        // @param pValuesY       The list of Y-axis coordinates.
        // @param valuesLength   The length of the lists.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetMaskPixels QSI_GetPixelMask QSI_SetMaskPixels
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetPixelMask(uint handle, int[] pValuesX, int[] pValuesY, int valuesLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the current pre-exposure flush setting.
        //
        // @param handle         The handle.
        // @param [out] pValue   The pre-exposure flush value.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetPreExposureFlush QSI_SetPreExposureFlush
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetPreExposureFlush(uint handle, out QSI_PreExposureFlush pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the pre-exposure flush setting.
        //
        // Controls the amount of pre-exposure flushing.  Flushing helps remove any previously accumulated
        // dark current from the CCD imager.
        //
        // With @ref QSI_FLUSH_NONE no flushing performed. The mage will contain any dark current that had
        // accumulated since the last exposure. This mode allows for the fastest back-to-back exposures.
        //
        // The other 4 modes, provide increasingly higher levels of flushing by employing a number of
        // different strategies. Higher levels of flushing take more time to execute.
        // 
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    The pre-exposure flush value.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetPreExposureFlush QSI_GetPreExposureFlush
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetPreExposureFlush(uint handle, QSI_PreExposureFlush value);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether the LED is enabled.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if enabled; false if disabled.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetLEDEnabled QSI_SetLEDEnabled
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetLEDEnabled(uint handle, ref int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets a value indicating whether the LED is enabled.
        //
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    True if enabled; false if disabled.
        //
        // @return An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetLEDEnabled QSI_GetLEDEnabled
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetLEDEnabled(uint handle, int value);

        ///////////////////////////////////////////////////////////////////
        // Gets the current image readout speed.
        //
        // @param handle         The handle.
        // @param [out] pValue   The readout speed.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetReadoutSpeed QSI_SetReadoutSpeed
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetReadoutSpeed(uint handle, out QSI_ReadoutSpeed pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the image readout speed.
        //
        // Controls the readout speed of cameras that have read out speed control capability.  Readout
        // speed selection is a tradeoff between high image quality and fast image readout and download.
        // Typically @ref QSI_READOUT_FAST is used during focusing and other setup operations and @ref
        // QSI_READOUT_HIGH_QUALITY is used during the final image capture.
        // 
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    The readout speed.
        //
        // @return An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetReadoutSpeed QSI_GetReadoutSpeed
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetReadoutSpeed(uint handle, QSI_ReadoutSpeed value);

        ///////////////////////////////////////////////////////////////////
        // Gets the serial number of the camera.
        // 
        // If connected, the serial number of the camera is returned.
        // 
        // If not connected, the value returned will either be an empty string, the serial number of the
        // last camera connected to, or the serial number specified when @ref QSI_SetSerialNumber was
        // called.
        // 
        // @remarks The string shall not exceed 68 characters (for compatibility with FITS headers).
        // 
        // @param handle         The handle.
        // @param [out] pBuffer  The pre-allocated character buffer where the serial number is to be
        //                       written.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_SetSerialNumber QSI_Connect
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetSerialNumber(uint handle, StringBuilder pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Sets the serial number of the camera to connect to.
        //
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle         The handle.
        // @param [in] pBuffer   The buffer where the serial number is located.
        // @param bufferLength   The length of the buffer.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetSerialNumber QSI_Connect
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int QSI_SetSerialNumber(uint handle, [MarshalAs(UnmanagedType.LPStr)] string pBuffer, int bufferLength);

        ///////////////////////////////////////////////////////////////////
        // Gets the current shutter priority.
        //
        // @param handle         The handle.
        // @param [out] pValue   The shutter priority.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetShutterPriority QSI_SetShutterPriority
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetShutterPriority(uint handle, out QSI_ShutterPriority pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets the shutter priority.
        //
        // Shutter priority is only applicable on cameras that have an electronic shutter with a mechanical
        // shutter installed.
        // 
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    The shutter priority.
        //
        // @return An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetShutterPriority QSI_GetShutterPriority
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetShutterPriority(uint handle, QSI_ShutterPriority value);

        ///////////////////////////////////////////////////////////////////
        // Gets a value indicating whether sound is currently enabled.
        //
        // @param handle         The handle.
        // @param [out] pValue   True if enabled; false if disabled.
        //
        // @returns An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetSoundEnabled QSI_SetSoundEnabled
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_GetSoundEnabled(uint handle, ref int pValue);

        ///////////////////////////////////////////////////////////////////
        // Sets a value indicating whether sound is enabled.
        //
        // @remarks This setting is stored in the system Registry and will be maintained between camera
        //          connections.
        //
        // @param handle   The handle.
        // @param value    True if enabled; false if disabled.
        //
        // @return An error code on error; otherwise, @ref QSI_OK.
        //
        // @see QSI_GetCanSetSoundEnabled QSI_GetSoundEnabled
        //
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int QSI_SetSoundEnabled(uint handle, int value);
    }
}
