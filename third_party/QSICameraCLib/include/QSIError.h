////////////////////////////////////////////////////////////////////////////////////////////////////
/// @file QSIError.h
///
/// @brief QSI API error codes.
///
/// @internal The COM QSI API library will throw COM exceptions with an error code constructed using
///           the COM MAKE_HRESULT macro. As per ASCOM, the class of the error code is FACILITY_ITF
///           and the QSI error codes begin at 0x0400 (1024). The resulting error codes look like
///           "0x8004XXXX" where XXXX is the QSI error code, the sign bit indicates a fatal error
///           and the 4 indicates FACILITY_ITF.
///           @par
///           ie C++ usage:\n
///           return Error ("Error Text Message", IID_ICCDCCAMERA, MAKE_HRESULT(1, FACILITY_ITF,
///           QSINOTSUPPORTED);
///           @par
///           QSICameraCLib just returns these codes.
/// 
/// @par "Revision History"
/// 12/19/2006 - DRC - Initial release.8\n
/// 09/19/2013 - MWB - More error codes added.\n
/// 09/23/2013 - MWB - Descriptions created for all error codes.
/// 
/// @par "Copyright"
/// This file is copyright 2005-2013 by Quantum Scientific Imaging, Inc.
///

#define QSI_OK                      0x0000 ///< No error occurred.
#define QSI_NOTSUPPORTED            0x0400 ///< Not supported.
#define QSI_UNRECOVERABLE           0x0401 ///< An unrecoverable internal or device error occurred.
#define QSI_NOFILTER                0x0402 ///< No filterwheel available.
#define QSI_NOMEMORY                0x0403 ///< Out of memory.
#define QSI_BADROWSIZE              0x0404 ///< Invalid row size specified.
#define QSI_BADCOLSIZE              0x0405 ///< Invalid column size specified.
#define QSI_INVALIDBIN              0x0406 ///< Invalid bin factor specified.
#define QSI_NOASYMBIN               0x0407 ///< Cannot asymmetric bin.
#define QSI_BADEXPOSURE             0x0408 ///< Invalid exposure duration specified.
#define QSI_BADBINSIZE              0x0409 ///< Invalid bin factor specified.
#define QSI_NOEXPOSURE              0x040A ///< No prior exposure taken.
#define QSI_BADRELAYSTATUS          0x040B ///< An error occurred while getting relay status.
#define QSI_BADABORTRELAYS          0x040C ///< An error occurred while aborting active relays.
#define QSI_RELAYERROR              0x040D ///< An error occurred while activating relays.
#define QSI_INVALIDIMAGEPARAMETER   0x040E ///< One or more image parameters are invalid. Not currently used.
#define QSI_NOIMAGEAVAILABLE        0x040F ///< There is no image available for download.
#define QSI_NOTCONNECTED            0x0410 ///< The camera has not been connected to.
#define QSI_INVALIDFILTERNUMBER     0x0411 ///< Filter position is invalid.
#define QSI_RECOVERABLE             0x0412 ///< A recoverable internal error occurred. Not currently used.
#define QSI_CONNECTED               0x0413 ///< Camera cannot be connected when making this call.
#define QSI_INVALIDTEMP             0x0414 ///< Invalid temperature specified.
#define QSI_TRIGGERTIMEOUT	        0x0415 ///< Trigger timed out.

#define QSI_ERROR_NO_CAMERA         0x0416 ///< No camera available.
#define QSI_ERROR_NO_SHUTTER        0x0417 ///< No shutter available.
#define QSI_ERROR_DOWNLOADING       0x0418 ///< Camera is busy downloading an image.
#define QSI_ERROR_INVALID_HANDLE    0x0419 ///< The specified handle is not valid.
#define QSI_ERROR_BAD_ARGUMENT      0x041A ///< One of the arguments specified is not valid.
#define QSI_ERROR_NULL_POINTER      0x041B ///< A null pointer was passed as an argument.
#define QSI_ERROR_INTERNAL_ERROR    0x041C ///< An undefined error occurred.
#define QSI_ERROR_NOT_IMPLEMENTED   0x041D ///< Not yet implemented.
#define QSI_ERROR_BUFFER_TOO_SMALL  0x041E ///< The specified buffer is too small.
#define QSI_ERROR_BUFFER_TOO_LARGE  0x041F ///< The specified buffer is too small.
#define QSI_ERROR_NOT_ENABLED       0x0420 ///< The requested mode is not enabled.
#define QSI_ERROR_CREATE_HANDLE     0x0421 ///< C API failed to create handle.
#define QSI_ERROR_COMMAND_FAILED    0x0422 ///< The command failed.  This is usually caused by a previous error, camera failure or camera disconnection.
