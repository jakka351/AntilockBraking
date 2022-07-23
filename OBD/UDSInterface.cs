using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBD
{
    public interface UDSInterface
    {
        //void ReadMemoryByAddress( address, out byte[] memory);
        void SecurityAccess(byte mode);

        void ECUReset(byte[] command);

        void ClearDiagnosticInformation(byte[] command);
        
        void readDTCByStatus(byte[] command);
        
        void readDataByLocalID(byte[] command);

        void readMemoryByCommonID(byte[] command);

        void startRoutineByLocalIdentifie(byte[] command);
        //startRoutineByLocalIdentifier                         = 0x31    
 
        void stopRoutineByLocalIdentifier(byte[] command);
        //stopRoutineByLocalIdentifier                          = 0x32

        void requestRoutineResultsByLocalID(byte[] command);
        //requestRoutineResultsByLocalID                        = 0x33
        
        void RequestDownload();

        void RequestUpload();

        void writeDataByLocalId();

        void requestReadMemoryBlock();
        
        void writeMemoryByAddress();
   
        void testerPresent(byte[] command);

        void controlDTCSetting(byte[] command);
        // Service 0x85 controlDTCSsetting
        //      
        //DIAGNOSTIC_SESSION_CONTROL = 0x10,
        //    ECU_RESET = 0x11,
        //    CLEAR_DIAGNOSTIC_INFORMATION = 0x14,
        //    READ_DTC_BY_STATUS = 0x18,
        //    READ_DTC_INFORMATION = 0x19,
        //    READ_DATA_BY_INDENTIFIER = 0x22,
        //    READ_MEMORY_BY_ADDRESS = 0x23,
        //    SECURITY_ACCESS = 0x27,
        //    COMMUNICATION_CONTROL = 0x28,
        //    WRITE_DATA_BY_IDENTIFIER = 0x2E,
        //    IO_CONTROL_BY_IDENTIFIER = 0x2F,
        //    ROUTINE_CONTROL = 0x31,
        //    REQUEST_DOWNLOAD = 0x34,
        //    REQUEST_UPLOAD = 0x35,
        //    TRANSFER_DATA = 0X36,
        //    TRANSFER_EXIT = 0X37,
        //    WRITE_MEMORY_BY_ADDRESS = 0X3D,
        //    TESTER_PRESENT = 0X3E,
        //    CONTROL_DTC_SETTING = 0X85,
        //    DIAGNOSTIC_COMMAND = 0xB1,
    }
}
