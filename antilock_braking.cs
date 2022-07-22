
namespace ABS
{
    public class ABS_ECU
    {
        public enum ABS_REQ : byte
        {
        startDiagnosticSession                                = 0x10,
        ecuReset                                              = 0x11,
        readFreezeFrameData                                   = 0x12,
        reportFreezeFrameData                                 = 0x52,
        operationalStateEntryRequest                          = 0x20,
        readDataByLocalID                                     = 0x21,
        readMemoryByCommonID                                  = 0x22,
        readMemoryByAddress                                   = 0x23, 
        requestCommonIDScalingMasking                         = 0x24,
        stopTransmittingRequestedData                         = 0x25,
        requestSecurityAccess                                 = 0x27,
        startRoutineByLocalIdentifier                         = 0x31,    
        stopRoutineByLocalIdentifier                          = 0x32,
        requestRoutineResultsByLocalID                        = 0x33,
        requestDownload                                       = 0x34,
        requestUpload                                         = 0x35,
        transferData                                          = 0x36,
        requestTransferExit                                   = 0x37,
        reportDiagnosticState                                 = 0x50,
        reportSecurityAccess                                  = 0x67        	
        }

        public enum ABS_RESP : byte
        {

        reportDiagnosticState                                 = 0x50,
        ecuReset                                              = 0x51,
        reportFreezeFrameData                                 = 0x52,
        operationalStateEntryRequest                          = 0x60,
        readDataByLocalID                                     = 0x61,
        readMemoryByCommonID                                  = 0x62,
        readMemoryByAddress                                   = 0x63, 
        requestCommonIDScalingMasking                         = 0x64,
        stopTransmittingRequestedData                         = 0x65,
        requestSecurityAccess                                 = 0x67,
        startRoutineByLocalIdentifier                         = 0x71,    
        stopRoutineByLocalIdentifier                          = 0x72,
        requestRoutineResultsByLocalID                        = 0x73,
        requestDownload                                       = 0x74,
        requestUpload                                         = 0x75,
        transferData                                          = 0x76,
        requestTransferExit                                   = 0x77,
        reportSecurityAccess                                  = 0x67            
                    
        }
    }

    public class PID
    {
        /// <summary>
        /// REQUEST_VEHICLE_INFORMATION = 0x09
        /// </summary>
        public enum VehicleInformation : byte
        {
            SUPPORTED_PIDS = 0x00,
            VIN_MESSAGE_COUNT = 0x01,
            VIN = 0x02,
            CALIBRATION_ID_MESSAGE_COUNT = 0x03,
            CALIBRATION_ID = 0x04,
            CVN_MESSAGE_COUNT = 0x05,
            CVN = 0x06,
            IN_USE_PERFORMANCE_TRACKING_IGNITION = 0x08,
            ECU_NAME_MESSAGE_COUNT = 0x09,
            ECU_NAME = 0x0A,
            IN_USE_PERFORMANCE_TRACKING_COMPRESSION = 0x0B

        }
    }
    



 
}
