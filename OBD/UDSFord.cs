#region Copyright (c) 2016, Roland Harrison
/* 
 *#################################################################################################################
 *# Copyright (c) 2022  Jack Leighton
 *# bjakkaleighton@gmail.com // jack.leighton@au.bosch.com
 *# All rights reserved.
 *#################################################################################################################
 *# Redistribution and use in source and binary forms, with or without modification, are permitted provided that
 *# the following conditions are met:
 *# 1.    With the express written consent of the copyright holder.
 *# 2.    Redistributions of source code must retain the above copyright notice, this 
 *#       list of conditions and the following disclaimer.
 *# 3.    Redistributions in binary form must reproduce the above copyright notice, this 
 *#       list of conditions and the following disclaimer in the documentation and/or other 
 *#       materials provided with the distribution.
 *# 4.    Neither the name of the organization nor the names of its contributors may be used to
 *#       endorse or promote products derived from this software without specific prior written permission.
 *#################################################################################################################
 *# THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 *# INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 *# DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *# SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 *# SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 *# WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
 *# USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *#################################################################################################################
 *# Notes
 *#  1. This software can only be distributed with my written permission. It is for my own educational purposes and 
 *#     is potentially dangerous to ECU health and safety. 
 *#############################################################################################################
 * 
 * original file UDSFord.cs by:
 * Copyright (c) 2016, Roland Harrison
 * roland.c.harrison@gmail.com
 * 
 * edited for purposes of creating an ABS config utility
 *
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * Neither the name of the organization nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */
#endregion
using J2534DotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace OBD
{
    /// <summary>
    /// Unified Diagnostic Services (UDS) is a vendor specific extension of OBD hence
    /// we literally extend our OBD implementation and implement the UDS interface
    /// This UDS implementation is via J2534
    /// </summary>
    public class UDSFord : OBDConnection, UDSInterface
    {
		#region details
		//The following commands are supported by the spanish oak PCM
		//0x10	Diagnostic Session Control	
		//0x11	ECU Reset	
		//		0x01 Hard Reset
		//		0x02 Key Off On Reset
		//		0x03 Soft Reset
		//0x14	Clear Diagnostic Information (not supported)
		//0x18	Read DTC By Status?
		//		02 FF FF (FF FF = group of DTC)
		//0x19	Read DTC Information (not supported)
		//0x22	Read Data By Identifier	
		//0x23	Read Memory By Address	
		//0x27	Security Access	
		//0x28	Communication Control (not supported)
		//0x2E	Write Data By Identifier (not supported)
		//0x2F	Input Output Control By Identifier (not supported)
		//0x31	Routine Control	
		//		0x01 Start Routine
		//		0x02 Stop Routine
		//		0x03 Request Routine Result
		//0x34	Request Download
		//0x35	Request Upload (not supported)
		//0x36	Transfer Data	
		//0x37	Transfer Exit (not supported)
		//0x3D	Write Memory By Address (not supported)
		//0x3E	Tester Present	
		//0x85	Control DTC Setting (not supported)
		//0xB1	DiagnosticCommand
		//		0x00 B2 AA 	- 	Erase Flash from address 0
		//		0x00 3C		-	Enable brakes
		//		0x00 2B		-	Disable brakes
		//		0x00 01 A3	-	Do magic??
		
		//https://automotive.wiki/index.php/ISO_14229
		
		#endregion details

        public UDSFord(IJ2534Extended j2534Interface) : base(j2534Interface)
        {
        }

        public void ReadMemoryByAddress(uint address, uint blockSize, out byte[] memory)
        {
            //Send the read memory request
            byte blockSizeUpper = (byte)((blockSize >> 8) & 0xFF);
            byte blockSizeLower = (byte)(blockSize & 0xFF);
            //ISO14229 ReadMemoryByAddress
            //byte1 ServiceID 0x23
            //byte2 AddressAndLengthFormatIdentifier (0 for Ford Spanish Oak)
            //byte3 address byte 1
            //byte4 address byte 2
            //byte5 address byte 3
            //byte6 address byte 4
            //byte7 block size byte1
            //byte8 block size byte2
            byte[] txMsgBytes = { (byte)UDScmd.Mode.READ_MEMORY_BY_ADDRESS, 0, (byte)((address >> 16) & 0xFF), (byte)((address >> 8) & 0xFF), (byte)((address) & 0xFF), blockSizeUpper, blockSizeLower};
            SendMessage(txMsgBytes);

            //We expect 3 messages, rx, start of message, then the payload data
            List<PassThruMsg> rxMsgs;

            //This will throw an exception if we don't get a valid reply
            while (ReadMessage(out rxMsgs, 250) == J2534Err.STATUS_NOERROR)
            {
                if (rxMsgs[0].RxStatus == RxStatus.NONE) break;
            }

            //If we couldn't find the start of the mesage give up
            if(J2534Status != J2534Err.STATUS_NOERROR) throw new J2534Exception(J2534Status);
            if (rxMsgs.Count < 1) throw new J2534Exception(J2534Err.ERR_INVALID_MSG);

            UDSPacket rxPacket = ParseUDSResponse(rxMsgs[0], UDScmd.Mode.READ_MEMORY_BY_ADDRESS);
            if (rxPacket.Response != UDScmd.Response.POSTIVE_RESPONSE) {
                throw new UDSException(rxPacket.NegativeResponse);
            }
            memory = rxPacket.Payload;
        }


        public void Listen()
        {

            List<PassThruMsg> rxMsgs;
            J2534Status = J2534Interface.ReadAllMessages(ChannelId, 10000, 1000, out rxMsgs, true);
            List<byte[]> msgs = new List<byte[]>();
            foreach(var msg in rxMsgs)
            {
                msgs.Add(msg.GetBytes());
            }
        }

        
        public void ECUReset(byte[] command)
        {
            throw new NotImplementedException();
        }
        
        public void ClearDiagnosticInformation(byte[] command)
        {
            throw new NotImplementedException();
        }
        
        public void readDTCByStatus(byte[] command)
        {
            //#################################################################################################################
            //# Troubleshooting SID's
            //# Diagnostic Information + DTCs 
            //#################################################################################################################
            //readFreezeFrameData                                   = 0x12
            //ServiceRequest[readFreezeFrameData]                   = [readFreezeFrameData, "readFreezeFrameData", "0x12"]
            //reportFreezeFrameData                                 = 0x52
            //ServiceRequest[reportFreezeFrameData]                 = [reportFreezeFrameData, "0x52"]
            //requestStoredCodes                                    = 0x13
            //ServiceRequest[requestStoredCodes]                    = [requestStoredCodes, "requestStoredCodes", "0x13"] 
            //clearDiagnosticInformation                            = 0x14
            //ServiceRequest[clearDiagnosticInformation]            = [clearDiagnosticInformation, "clearDiagnosticInformation", "0x14"]
            //readDTCByStatus                                       = 0x18
            //ServiceRequest[readDTCByStatus]                       = [readDTCByStatus, "readDTCByStatus", "0x18"]
            //def ReadDtc(_DiagSig_Rx):
            //    msg = can.Message(arbitration_id = _DiagSig_Rx,
            //                      data           = [0x02, readDTCByStatus, 0x00, 0xFF, 0, 0, 0, 0], is_extended_id = False)
            //    try:
            //        MidSpeedCan.send(msg)
            //        Response = MidSpeedCan.recv()
            //        Parser(Response, 8)
            //        return
            //    except can.CanError:
            //        pass
            //
            //def clearDtc(_DiagSig_Rx): 
            //    msg = can.Message(arbitration_id = DiagSig_Rx.keys(),
            //                      data           = [0x02, clearDiagnosticInformation, 0, 0, 0, 0, 0, 0], is_extended_id = False)
            //    for keys in DiagSig_Rx.keys():
            //        try:    
            //            MidSpeedCan.send(msg)
            //            return
            //        except can.CanError:
            //            print("Message NOT sent")
            //#################################################################################################################
        }

        public void readDataByLocalID(byte[] command)
        {
            // send requestResponse testerPresent message to abs ecu
            byte[] txMsgBytes = {(byte)UDScmd.Mode.READ_DATA_BY_LOCAL_ID, subFunction};
            SendMessage(txMsgBytes);

            // attempt to read at least 1 message as a reply
            List<PassThruMsg> rxMsgs;
            ReadAllMessages(out rxMsgs,1, 200);

        }

        public void readMemoryByCommonID(byte[] command)
        {
            // send requestResponse testerPresent message to abs ecu
            byte[] txMsgBytes = {(byte)UDScmd.Mode.READ_DATA_BY_INDENTIFIER, subFunction};
            SendMessage(txMsgBytes);

            // attempt to read at least 1 message as a reply
            List<PassThruMsg> rxMsgs;
            ReadAllMessages(out rxMsgs,1, 200);

        }

        public void startRoutineByLocalIdentifier(byte[] command)
        {
            throw new NotImplementedException();

        }
        //startRoutineByLocalIdentifier                         = 0x31    
 
        public void stopRoutineByLocalIdentifier(byte[] command)
        {
            throw new NotImplementedException();

        }
        //stopRoutineByLocalIdentifier                          = 0x32

        public void requestRoutineResultsByLocalID(byte[] command)
        {
            throw new NotImplementedException();

        }
        //requestRoutineResultsByLocalID                        = 0x33
        
        public void writeDataByLocalId()
        {

        }

        public void testerPresent(byte[] subFunction)
        {
            // send requestResponse testerPresent message to abs ecu
            byte[] txMsgBytes = {(byte)UDScmd.Mode.TESTER_PRESENT, subFunction};
            SendMessage(txMsgBytes);

            // attempt to read at least 1 message as a reply
            List<PassThruMsg> rxMsgs;
            ReadAllMessages(out rxMsgs,1, 200);

        }

        public void controlDTCSetting(byte[] command)
        {
            // Service 0x85 controlDTCSsetting
            //#############################################################################################################
            //controlDTCSetting                                     = 0x85
            //ServiceRequest[controlDTCSetting]                     = [controlDTCSetting, "controlDTCSetting", "0x85"] 
            //#############################################################################################################
            //_controlDTCSetting                                    = dict()
            //settingOn                                             = 0x01
            //_controlDTCSetting[settingOn]                         = [settingOn, 'controlDTCSet to On,' '0x85']
            //settingOff                                            = 0x02
            //_controlDTCSetting[settingOff]                        = [settingOff, 'controlDTCSet to Off,' '0x85']
            //#############################################################################################################
            //# Service Id 0x85 controlDtcSetting function
            //#############################################################################################################
            //def controlDTCSetting(_DiagSig_Rx, _controlDTCSetting):
            //    controlDTC = can.Message(arbitration_id = _DiagSig_Rx,
            //                             data           = [0x02, 0x85, _controlDTCSetting, 0, 0, 0, 0, 0], is_extended_id = False)
            //    try:
            //        MidSpeedCan.send(controlDTC)
            //    except can.CanError:
            //        print("Message NOT sent")
            //#############################################################################################################
            //

        }

        public void diagnosticCommand(byte[] command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parse the replies checking for a valid response, if we have a valid response extract the payload data
        /// </summary>
        /// <param name="rxMsgs"></param>
        /// <param name="txMode"></param>
        /// <param name="payload"></param>
        /// <returns></returns>

        //public void AsBuiltCheckSumGenerator()
        //{
        //necessary?
        //}
        
        UDSPacket ParseUDSResponse(PassThruMsg rxMsg, UDScmd.Mode txMode)
        {
            var rxMsgBytes = rxMsg.GetBytes();
            UDSPacket rxPacket = new UDSPacket();
            //Iterate the reply bytes to find the echod ECU index, response code, function response and payload data if there is any
            //If we could use some kind of HEX regex this would be a bit neater
            int stateMachine = 0;
            for (int i = 0; i < rxMsgBytes.Length; i++)
            {
                switch (stateMachine)
                {
                    case 0:
                        if (rxMsgBytes[i] == 0x07) stateMachine = 1;
                        else if (rxMsgBytes[i] != 0) return rxPacket;
                        break;
                    case 1:
                        if (rxMsgBytes[i] == 0xE8) stateMachine = 2;
                        else return rxPacket;
                        break;
                    case 2:
                        var payload = new byte[rxMsgBytes.Length - i];

                        int payloadLength = rxMsgBytes.Length - i;
                        if (payloadLength > 0)
                        {
                            payload = new byte[payloadLength];
                            Array.Copy(rxMsgBytes, i, payload, 0, payloadLength);
                            rxPacket = new UDSPacket(payload, txMode);
                            ;
                        }
                        return rxPacket;
                    case 3:
                    default:
                        return rxPacket;
                }
            }
            return rxPacket;
        }

        public class UDSPacket
        {
            //Depending on the request type some of these fields will not be used
            public UDScmd.Response Response = UDScmd.Response.NO_RESPONSE;
            public UDScmd.NegativeResponse NegativeResponse = UDScmd.NegativeResponse.UNKNOWN;

            //The mode is often reflected back, if so this is populated
            public UDScmd.Mode Mode = UDScmd.Mode.UNKNOWN;


            public byte LengthFormatIdentifier = 0;
            public byte MaxNumberOfBlockLength = 0;
            //Typically used by SECURITY_ACCESS
            public byte SubFunction = 0;
            public byte[] Payload = new byte[0];

            public UDSPacket()
            {

            }

            public UDSPacket(byte [] data, UDScmd.Mode mode)
            {
                if (data == null) return;
                if (data.Length < 1) return;
                if((UDScmd.Response)data[0] != UDScmd.Response.NEGATIVE_RESPONSE) Response = (UDScmd.Response)(data[0] - (byte)mode);
                else Response = UDScmd.Response.NEGATIVE_RESPONSE;
                if (data.Length < 2) return;
                Mode = (UDScmd.Mode)data[1];
                int payloadLength = 0;

                switch (mode)
                {
                    case UDScmd.Mode.TRANSFER_DATA:
                    case UDScmd.Mode.DIAGNOSTIC_COMMAND:
                    case UDScmd.Mode.READ_MEMORY_BY_ADDRESS:
                        if (Response == UDScmd.Response.NEGATIVE_RESPONSE)
                        {
                            NegativeResponse = (UDScmd.NegativeResponse)data[2];
                        }
                        payloadLength = data.Length - 1;
                        if (payloadLength < 1) return;
                        Payload = new byte[payloadLength];
                        Buffer.BlockCopy(data, 1, Payload, 0, payloadLength);
                        break;
                    case UDScmd.Mode.REQUEST_DOWNLOAD:
                        if (Response == UDScmd.Response.NEGATIVE_RESPONSE)
                        {
                            NegativeResponse = (UDScmd.NegativeResponse)data[2];
                            return;
                        }
                        if (data.Length < 3) return;
                        LengthFormatIdentifier = data[1];
                        MaxNumberOfBlockLength = data[2];
                        break;
                    case UDScmd.Mode.SECURITY_ACCESS:
                        int offset;
                        if (Response == UDScmd.Response.NEGATIVE_RESPONSE)
                        {
                            offset = 3;
                            if (data.Length < 3) return;
                            NegativeResponse = (UDScmd.NegativeResponse)data[2];
                        }
                        else
                        {
                            offset = 2;
                            SubFunction = data[1];
                        }
                        payloadLength = data.Length - offset;
                        if (payloadLength < 1) return;
                        Payload = new byte[payloadLength];
                        Buffer.BlockCopy(data, offset, Payload, 0, payloadLength);
                        break;
                    default:
                        break;
                }
            }
        }
        // checksum dictionary or generator? there is only going to be static checksums for the first block, and 
        // second block the checksums stay the same for each module config option
        private static readonly Dictionary<int, byte[]> SecretKeysLevel1 = new Dictionary<int, byte[]>
        {
            {0x726, new byte[]{0x3F,0x9E,0x78,0xC5,0x96}},
            {0x727, new byte[]{0x50,0xC8,0x6A,0x49,0xF1}},
            {0x733, new byte[]{0xAA,0xBB,0xCC,0xDD,0xEE}},
            {0x736, new byte[]{0x08,0x30,0x61,0x55,0xAA}},
            {0x737, new byte[]{0x52,0x6F,0x77,0x61,0x6E}},
            {0x760, new byte[]{0x5B,0x41,0x74,0x65,0x7D}},
            {0x765, new byte[]{0x96,0xA2,0x3B,0x83,0x9B}},
            {0x7a6, new byte[]{0x50,0xC8,0x6A,0x49,0xF1}},
            {0x7e0, new byte[]{0x08,0x30,0x61,0xA4,0xC5}}
        };

        private static readonly Dictionary<int, byte[]> SecretKeysLevel2 = new Dictionary<int, byte[]>
        {
            {0x737, new byte[]{0x44,0x49,0x4F,0x44,0x45}},
            {0x7e0, new byte[]{0x5A,0x89,0xE4,0x41,0x72}}
        };
    }

    public class UDScmd
    {
        public enum Mode : byte
        {
            DIAGNOSTIC_SESSION_CONTROL = 0x10,
            ECU_RESET = 0x11,
            CLEAR_DIAGNOSTIC_INFORMATION = 0x14,
            READ_DTC_BY_STATUS = 0x18,
            READ_DTC_INFORMATION = 0x19,
            READ_DATA_BY_LOCAL_ID = 0x21,
            READ_DATA_BY_INDENTIFIER = 0x22,
            READ_MEMORY_BY_ADDRESS = 0x23,
            SECURITY_ACCESS = 0x27,
            COMMUNICATION_CONTROL = 0x28,
            WRITE_DATA_BY_IDENTIFIER = 0x2E,
            IO_CONTROL_BY_IDENTIFIER = 0x2F,
            ROUTINE_CONTROL = 0x31,
            ROUTINE_CONTROL_STOP = 0x32,
            REQUEST_ROUTINE_RESULTS = 0x33,   
            REQUEST_DOWNLOAD = 0x34,
            REQUEST_UPLOAD = 0x35,
            TRANSFER_DATA = 0X36,
            TRANSFER_EXIT = 0X37,
            WRITE_DATA_BY_LOCAL_ID = 0x3B,
            WRITE_MEMORY_BY_ADDRESS = 0X3D,
            TESTER_PRESENT = 0X3E,
            CONTROL_DTC_SETTING = 0X85,
            REQUEST_DIAGNOSTIC_DATA_PACKET = 0xA0,
            DYNAMICALLY_DEFINE_DIAGNOSTIC_DATA_PACKET = 0xA1,
            NO_STORED_CODES_LOGGING_STATE_ENTRY = 0xB0,
            DIAGNOSTIC_COMMAND = 0xB1,
            INPUT_INTEGRITY_TEST_STATE_ENTRY = 0xB2,
            REQUEST_MANUFACTURER_STATE_ENTRY = 0xB4,
            UNKNOWN = 0xFF,
        }

        public enum Response : byte
        {
            NO_RESPONSE = 0,
            POSTIVE_RESPONSE = 0x40,
            NEGATIVE_RESPONSE = 0x7F,
            REPORT_DIAGNOSTIC_STATE = 0x50,
            REPORT_DTC STATUS = 0x58,
            REPORT_DATA_BY_LOCAL_ID = 0x61,
            REPORT_DATA_BY_IDENTIFIER = 0x62,
            REPORT_SELFTEST_COMMENCED = 0x71,
            REPORT_ROUTINE_RESULT = 0x73,
            SUCCESFUL_WRITE_DATA_BY_LOCAL_ID = 0x7B,
            REPORT_RESPONSE_TESTER_PRESENT = 0x7E,
            REPORT_DTC_SETTING = 0xC5,
            REPORT_DIAGNOSTIC_COMMAND = 0xF1,
        }

        public enum NegativeResponse : byte
        {
            POSITIVE_RESPONSE = 0X00,
            GENERAL_REJECT = 0X10,
            SERVICE_NOT_SUPPORTED = 0X11,
            SUBFUNCTION_NOT_SUPPORTED = 0X12,
            INCORRECT_MSG_LENGTH_OR_FORMAT = 0X13,
            RESPONSE_TOO_LONG = 0X14,
            BUSY = 0X21,
            CONDITIONS_NOT_CORRECT = 0X22,
            REQUEST_SEQUENCE_ERROR = 0X24,
            REQUEST_OUT_OF_RANGE = 0X31,
            SECURITY_ACCESS_DENIED = 0X33,
            INVALID_SECURITY_KEY = 0X35,
            SECURITY_ATTEMPS_EXCEEED = 0X36,
            REQUIRED_TIME_DELAY_NOT_EXPIRED = 0X37,
            UPLOAD_DOWNLOAD_NOT_ACCEPTED = 0X70,
            TRANSFER_DATA_SUSPENDED = 0X71,
            GENERAL_PROGRAMMING_FAILURE = 0X72,
            WRONG_BLOCK_SEQUENCE_COUNTER = 0X73,
            REPONSE_PENDING = 0X78,
            INVALID_DATA_BLOCK = 0X79,      //This appears to be a Ford "invalid data block"
            SUBFUNCTION_NOT_SUPPORTED_IN_ACTIVE_SESSION = 0X7E,
            SERVICE_NOT_SUPPORTED_IN_ACTIVE_SESSION = 0X7F,
            RPM_TOO_HIGH = 0X81,
            RPM_TOO_LOW = 0X82,
            ENGINE_IS_RUNNING = 0X83,
            ENGINE_IS_NOT_RUNNING = 0X84,
            ENGINE_RUN_TIME_TOO_LOW = 0X85,
            TEMPERATURE_TOO_HIGH = 0X86,
            TEMPERATURE_TOO_LOW = 0x87,
            VEHICLESPEED_TOO_HIGH = 0x88,
            VEHICLESPEED_TOO_LOW = 0x89,
            THROTTLE_PEDAL_TOO_HIGH = 0x8A,
            THROTTLE_PEDAL_TOOL_LOW = 0x8B,
            TRANSMISSION_RANGE_NOT_IN_NEUTRAL = 0x8C,
            TRANSMISSION_RANGE_NOT_IN_GEAR = 0x8D,
            BRAKE_PEDAL_NOT_PRESSED_OR_NOT_APPLIED = 0x8F,
            SHIFTER_LEVER_NOT_IN_PARK = 0x90,
            TORQUE_CONVERTER_CLUTCH_LOCKED = 0x91,
            VOLTAGE_TOO_HIGH = 0x92,
            VOLTAGE_TOO_LOW = 0x93,         
            ////CAN GENERIC DIAGNOSTIC SPECIFICATION V2003 Negative Response Codes
            //#define  generalReject                            0x10
            //#define  serviceNotSupported                      0x11
            //#define  subFunctionNotSupported                  0x12
            //#define  responseTooLong                          0x14
            //#define  busyRepeatRequest                        0x21
            //#define  conditionsNotCorrect                     0x22
            //#define  requestSequenceError                     0x24
            //#define  requestOutOfRange                        0x31
            //#define  securityAccessDenied                     0x33
            //#define  invalidKey                               0x35
            //#define  exceedNumberOfAttempts                   0x36
            //#define  requiredTimeDelayNotExpired              0x37
            //#define  uploadDownloadNotAccepted                0x70
            //#define  generalProgrammingFailure                0x72
            //#define  requestCorrectlyReceived_ResponsePending 0x78
            //#define  subFuncionNotSupportedInActiveSession    0x7E
            //#define  serviceNotSupportedInActiveSession       0x7F
            //#define  rpmTooHigh                               0x81
            //#define  rpmTooLow                                0x82
            //#define  engineIsRunning                          0x83
            //#define  engineIsNotRunning                       0x84
            //#define  shifterLeverNotInPark                    0x90
            UNKNOWN = 0xFF,
        

        }
        

        public enum ABSDataIdentifiers : byte
        {
            ABSLF_I = 0x00,
            ABSLF_O = 0x00,
            ABSLR_I = 0x00,
            ABSLR_O = 0x00,
            ABSPMPRLY = 0x00,
            ABSRF_I = 0x00,
            ABSRF_O = 0x00,
            ABSRR_I = 0x00,
            ABSRR_O = 0x00,
            ABSR_I = 0x00,
            ABSR_O = 0x00,
            ABSVLVRLY = 0x00,
            ABS_BYTE = 0x00,
            ABS_STAT = 0x00,
            BOO_ABS = 0x00,
            BRAKPRES = 0x00,
            CCNTABS = 0x00,
            DISABLE_SS = 0x00,
            HDC_ENABLE = 0x00,
            HDC_SW = 0x00,
            Ignition = 0x00,
            LAT_ACCL = 0x00,
            LF_WSPD = 0x00,
            LR_WSPD = 0x00,
            PMPSTAT = 0x00,
            PRK_BRAKE = 0x00,
            RF_WSPD = 0x00,
            RR_WSPD = 0x00,
            STEER_ANGL = 0x00,
            TCSPRI1 = 0x00,
            TCSPRI2 = 0x00,
            TCSSWI1 = 0x00,
            TCSSWI2 = 0x00,
            TCYC_FS = 0x00,
            TCYC_SW = 0x00,
            YAW_RATE = 0x00,
        }

        public enum ABSConfigOption : byte
        {
            XTG1G2SportIRS            =  0x100401, // 1049601
            XTSportIRS                =  0x100501, // 1049857
            GTurbo                    =  0x100601, // 1050113
            G6SedanDLPGXTG6G6ESport   =  0x110401, // 1115137
            PoliceSedanDLPGXTSport    =  0x110501, // 1115993
            UteDLPGREBXR612t          =  0x120402, // 1180674
            F6PursultUTE12t4pot       =  0x140102, // 1310978
            F6PursultUTE12t6pot       =  0x140202, // 1311234
            XT                        =  0x160401, // 1442817
            G6SedanDLPGXTStd          =  0x170401, // 1508953
            UteI6RDLPGXL34t           =  0x180402, // 1573890
            F6Force64potBrakes        =  0x190101, // 1638657
            F6Force66potBrakes        =  0x190201, // 1638913
            XR6                       =  0x190401, // 1639425
            XR6Turbo                  =  0x190601, // 1639937
            XR6TurboPolice            =  0x190701, // 1640193
            GTForce4pOtBrakes         =  0x200101, // 2097409
            GTGTPForces6potBrakes     =  0x200201, // 2097665
            XR8                       =  0x200601, // 2098689
            XR8Police                 =  0x200701, // 2098945
            UteI67DLPG1t              =  0x210402, // 2163714
            UteI6                     =  0x220402, // 2229250
            G6SedanDLPGXTHDSus        =  0x230401, // 2294785
            XTPoliceHDFrontSusandIRS  =  0x230501, // 2295041
            XTHDFrontSusandIRS        =  0x230401, // 229478
        }

        public enum AsBuiltConfiguration : byte
        {
            BLOCK1LINE1 = 0xFFFFFFFFFF,
            BLOCK1LINE2 = 0xFFFFFFFFFF,
            BLOCK1LINE3 = 0xFFFFFFFFFF,
            BLOCK1LINE3 = 0xFFFF,
            BLOCK2LINE1 = ABSConfigOption,
        }

        public enum ABSConfigSum : byte
        {
            XTG1G2SportIRS            =  0x    100401, // 1049601
            XTSportIRS                =  0x    100501, // 1049857
            GTurbo                    =  0x    100601, // 1050113
            G6SedanDLPGXTG6G6ESport   =  0x    110401, // 1115137
            PoliceSedanDLPGXTSport    =  0x    110501, // 1115993
            UteDLPGREBXR612t          =  0x    120402, // 1180674
            F6PursultUTE12t4pot       =  0x    140102, // 1310978
            F6PursultUTE12t6pot       =  0x    140202, // 1311234
            XT                        =  0x    160401, // 1442817
            G6SedanDLPGXTStd          =  0x    170401, // 1508953
            UteI6RDLPGXL34t           =  0x    180402, // 1573890
            F6Force64potBrakes        =  0x    190101, // 1638657
            F6Force66potBrakes        =  0x    190201, // 1638913
            XR6                       =  0x    190401, // 1639425
            XR6Turbo                  =  0x    190601, // 1639937
            XR6TurboPolice            =  0x    190701, // 1640193
            GTForce4pOtBrakes         =  0x    200101, // 2097409
            GTGTPForces6potBrakes     =  0x    200201, // 2097665
            XR8                       =  0x91    200601, // 2098689
            XR8Police                 =  0x92    200701, // 2098945
            UteI67DLPG1t              =  0x    210402, // 2163714
            UteI6                     =  0x    220402, // 2229250
            G6SedanDLPGXTHDSus        =  0x    230401, // 2294785
            XTPoliceHDFrontSusandIRS  =  0x230501, // 2295041
            XTHDFrontSusandIRS        =  0x230401, // 229478
        }
        public enum AsBuiltChecksum : byte
        {
            BLOCK1LINE1SUM = 0x64,
            BLOCK1LINE2SUM = 0x65,
            BLOCK1LINE3SUM = 0x66,
            BLOCK1LINE4SUM = 0x6A,
            BLOCK2LINE1SUM = ABSConfigOptionSum,
        

        }
        public enum DTCStatusByte : byte
        {
            TEST_FAILED_THIS_OPERATION_CYCLE = 0x02,
            PENDING_DTC = 0x04,
            CONFIRMED_DTC = 0x08,
            TEST_NOT_COMPLETED_SINCE_LAST_CLEAR = 0x10,
            TEST_FAILED_SINCE_LAST_CLEAR = 0x20,
            TEST_NOT_COMPLETED_THIS_OPERATION_CYCLE = 0x40,
            WARNING_INDICATOR_REQUESTED = 0x80,

        }

    }
}
//#################################################################################################################
//# Antilock Brake Module     
//#################################################################################################################
//#ABS_SecretKey                                         = 
//#ABS_DiagSig_Rx                                        = 0x760
//#DiagSig_Rx[ABS_DiagSig_Rx]                            = (ABS_DiagSig_Rx, "1. ABS 0x760", "Antilock Brake Module")
//#ABS_DiagSig_Tx                                        = 0x768
//#DiagSig_Tx[ABS_DiagSig_Tx]                            = [ABS_DiagSig_Tx, "ABS 0x768", "Antilock Brake Module"]
//#################################################################################################################
// ABS DiagSig_Rx-Tx
#define ECU_ADDR_ABS        0x28
#define ECU_ADDR_ABSB       0x3D
#define DIAG_SIG_RX_ABS     0x760
#define DIAG_SIG_TX_ABS     0x768
#define DIAG_SIG_RX_ABSB    0x7F2
#define DIAG_SIG_TX_ABSB    0x7FA
#define RAPID_DATA_ABS1     0x6B0
#define RAPID_DATA_ABS2     0x6B1
#define FNOS_ID_ABS1        0x516
#define FNOS_ID_ABS2        0x596


// GMRDB 2008 ABS Data Identifiers
#define BrakeActuatorStatus 0x2059  
#define BrakeApplicationCounter 0x2860  
#define BrakeBoosterMembraneDisplacementSensor 0x2822  
#define BrakeBoosterPressureMeasured 0x2028
#define BrakeBoosterPressureControlVacuumManagementStatus 0x2874  
#define BrakeBoosterPressureExpressedAsVacuumBPAPCorrected 0x  2872
#define BrakeBoosterPressureExpressedAsVacuumBPAPMeasuredAdjustedAndFilteredAndBeforeFMEMSubstitution 0x2873  
#define BrakeBoosterPressureSensorVoltage 0x2029
#define BrakeBoosterVacuumTarget 0x280C
#define BrakeControlSystemMode 0x284C
#define BrakeDiscTemperatureInferred 0x2847  
#define BrakeEventCounters 0x2881
#define BrakeFluidLevelSensorStatus 0x2839  
#define BrakeFluidLineHydraulicPressure 0x2B0D  
#define BrakeFluidLineHydraulicPressureCorrected 0x2034  
#define BrakeFluidLineHydraulicPressureCorrected 0x280A  
#define BrakeFluidLineHydraulicPressureSensorVoltage 0x2809  
#define BrakeForceSensor 0x2B10
#define BrakeInputSwitchStatus 0x2B00  
#define BrakeLightActivationStatus 0x2B1E  
#define BrakeLightSwitchActiveToBrakeTorqueMeasuredDelay 0x417B  
#define BrakeLightSwitchtoBrakePressureMeasuredDelay 0x404A
#define BrakeLiningWearSensorFeedback 0xC196
#define BrakeModuleRequestsVacuumFromCombustionEngine 0x483C  
#define BrakeOnOffBOOSwitchOutput 0x42DF
#define BrakeOverrideAcceleratorFunctionActivityMetrics 0x0590  
#define BrakePedalAngleSensorBInputMeasured 0x2013
#define BrakePedalAngleSensorInputMeasured 0x2012 
#define BrakePedalPosition 0x2B35
#define BrakePedalPositionMeasured 0x2823  
#define BrakePedalPositionBMeasured 0x2824
#define BrakePedalPositionSensorA 0x2096
#define BrakePedalPositionSensorB 0x208D
#define BrakePedalPositionSensorB 0x2097
#define BrakePedalTorqueDetectionLevel 0x417A  
#define BrakePower1Current 0x4328
#define BrakePower1Current 0x432A
#define BrakePower2Current 0x4327
#define BrakePower2Current 0x4329
#define BrakePressureDetectionLevel 0x404E  
#define BrakePullReductionActivationCounter 0x205B  
#define BrakeStatus 0xD120
#define BrakeSwitchBToBrakePressureMeasuredDelay 0x404D  
#define BrakeSystemPressureOffsetCorrected 0x2058
#define BrakeSystemStatus 0x7217
#define HillDescentControlIndicatorLightCommandedState 0x283A   
#define HillStartAssistActivationThreshold 0x2861   
#define LeftFrontWheelSpeed 0x210E  
#define LeftFrontWheelSpeedSensorInput 0x2B06   
#define LeftFrontWheelSpeedSensorState 0x2109   
#define RightFrontWheelSpeed 0x210F 
#define RightFrontWheelSpeedSensorInput 0x2B07  
#define RightFrontWheelSpeedSensorState 0x210B  
#define LeftRearWheelSpeed 0x210D       
#define LeftRearWheelSpeedSensorInput 0x2B08        
#define LeftRearWheelSpeedSensorState 0x210A        
#define RightRearWheelSpeed 0x2110  
#define RightRearWheelSpeedSensorInput 0x2B09   
#define RightRearWheelSpeedSensorState 0x210C   

// GMRDB 2022 ABS Data Identifiers


// FGI Falcon Specifc ABS Data 0x22 readDataByLocalId
#define LeftFrontInletValveState
#define LeftFrontOutletValveState
#define LeftRearInletValveState
#define LeftRearOutletValveState
#define ABSPumpMotorRelay
#define RightFrontInletValveState
#define RightFrontOutletValveState
#define RightRearInletValveState
#define RightRearOutletValveState
#define ABSRearInletValveStatus
#define ABSRearOutletValveStatus
#define ABSValveControlRelay
#define ABSProcessByte
#define ECUOperatingStates
#define BrakeONOFF
#define BrakeFluidLineHydraulicPressure
#define ContinuousCodes
#define DisableSafetySoftware
#define HDCEnable
#define HDCSwitchStatus
#define Ignition
#define ABSLateralAccelerationRate
#define Leftfrontwheelspeedsensor
#define Leftrearwheelspeedsensor
#define ABSPumpMotorStatus
#define ParkingbrakeSwitch
#define Rightfrontwheelspeedsensor
#define Rightrearwheelspeedsensor
#define Steeringwheelanglesensor
#define TCSPrimingValve1
#define TCSPrimingValve2
#define TCSSwitchingValve1
#define TCSSwitchingValve2
#define Tractioncontrolsystem
#define TractionControlSwitchStatus
#define ABSYawRateValue
//ABSLF_I   DISABLE     Left Front Inlet Valve State
//ABSLF_O DISABLE     Left Front Outlet Valve State
//ABSLR_I DISABLE     Left Rear Inlet Valve State
//ABSLR_O DISABLE     Left Rear Outlet Valve State
//ABSPMPRLY   DISABLE     ABS Pump Motor Relay
//ABSRF_I DISABLE     Right Front Inlet Valve State
//ABSRF_O DISABLE     Right Front Outlet Valve State
//ABSRR_I DISABLE     Right Rear Inlet Valve State
//ABSRR_O DISABLE     Right Rear Outlet Valve State
//ABSR_I  Off     ABS Rear Inlet Valve Status
//ABSR_O  Off     ABS Rear Outlet Valve Status
//ABSVLVRLY   ENABLE  ABS Valve Control Relay
//ABS_BYTE    204     ABS Process Byte
//ABS_STAT    Diagnostic  ECU Operating States
//BOO_ABS Inactive    Brake ON/OFF
//BRAKPRES    0.0 kPa Brake Fluid Line Hydraulic Pressure
//CCNTABS 2   Continuous Codes
//DISABLE_SS  No  Disable Safety Software
//HDC_ENABLE  Inactive    HDC Enable
//HDC_SW  Inactive    HDC Switch Status
//Ignition    12.02 V Ignition
//LAT_ACCL    0.00 G  ABS Lateral Acceleration Rate
//LF_WSPD 2.0 km/h    Left front wheel speed sensor
//LR_WSPD 2.0 km/h    Left rear wheel speed sensor
//PMPSTAT Inactive    ABS Pump Motor Status
//PRK_BRAKE   Inactive    Parking brake Switch
//RF_WSPD 2.0 km/h    Right front wheel speed sensor
//RR_WSPD 2.0 km/h    Right rear wheel speed sensor
//STEER_ANGL  11.40 ° Steering wheel angle sensor
//TCSPRI1 Inactive    TCS Priming Valve 1
//TCSPRI2 Inactive    TCS Priming Valve 2
//TCSSWI1 Inactive    TCS Switching Valve 1
//TCSSWI2 Inactive    TCS Switching Valve 2
//TCYC_FS Off     Traction control system
//TCYC_SW Up  Traction Control Switch Status
//YAW_RATE    0 1/min ABS Yaw Rate Value

// FGII Falcon Specifc ABS Data 0x22 readDataByLocalId
// FGX Falcon Specifc ABS Data 0x22 readDataByLocalId

// FG Falcon ABS Config Options aka As Built Data block 2 line 1
#define XTG1G2SportIRS                          10 04 01        1049601 
#define XTSportIRS                              10 05 01        1049857 
#define GTurbo                                  10 06 01        1050113 
#define G6SedanDLPGXTG6G6ESport                 11 04 01        1115137 
#define PoliceSedanDLPGXTSport                  11 05 01        1115993 
#define UteDLPGREBXR612t                        12 04 02        1180674 
#define F6PursultUTE12t4pot                     14 01 02        1310978 
#define F6PursultUTE12t6pot                     14 02 02        1311234 
#define XT                                      16 04 01        1442817 
#define G6SedanDLPGXTStd                        17 04 01        1508953 
#define UteI6RDLPGXL34t                         18 04 02        1573890 
#define F6Force64potBrakes                      19 01 01        1638657 
#define F6Force66potBrakes                      19 02 01        1638913 
#define XR6                                     19 04 01        1639425 
#define XR6Turbo                                19 06 01        1639937 
#define XR6TurboPolice                          19 07 01        1640193 
#define GTForce4pOtBrakes                       20 01 01        2097409 
#define GTGTPForces6potBrakes                   20 02 01        2097665 
#define XR8                                     20 06 01        2098689 
#define XR8Police                               20 07 01        2098945 
#define UteI67DLPG1t                            21 04 02        2163714 
#define UteI6                                   22 04 02        2229250 
#define G6SedanDLPGXTHDSus                      23 04 01        2294785 
#define XTHDFrontSusandIRS                      23 04 01        229478 
#define XTPoliceHDFrontSusandIRS                23 05 01        2295041 
