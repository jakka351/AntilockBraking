

#region Copyright (c) 2016, Roland Harrison
/* 
 *#################################################################################################################
 *# Copyright (c) 2022  Jack Leighton
 *# bjakkaleighton@gmail.com
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
using System.Data.Common;
using System.Threading.Channels;

namespace AntilockUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectVariantToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
836 sloc)  40.2 KB
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
            byte[] txMsgBytes = { (byte)UDScmd.Mode.READ_MEMORY_BY_ADDRESS, 0, (byte)((address >> 16) & 0xFF), (byte)((address >> 8) & 0xFF), (byte)((address) & 0xFF), blockSizeUpper, blockSizeLower };
            SendMessage(txMsgBytes);

            //We expect 3 messages, rx, start of message, then the payload data
            List<PassThruMsg> rxMsgs;

            //This will throw an exception if we don't get a valid reply
            while (ReadMessage(out rxMsgs, 250) == J2534Err.STATUS_NOERROR)
            {
                if (rxMsgs[0].RxStatus == RxStatus.NONE) break;
            }

            //If we couldn't find the start of the mesage give up
            if (J2534Status != J2534Err.STATUS_NOERROR) throw new J2534Exception(J2534Status);
            if (rxMsgs.Count < 1) throw new J2534Exception(J2534Err.ERR_INVALID_MSG);

            UDSPacket rxPacket = ParseUDSResponse(rxMsgs[0], UDScmd.Mode.READ_MEMORY_BY_ADDRESS);
            if (rxPacket.Response != UDScmd.Response.POSTIVE_RESPONSE)
            {
                throw new UDSException(rxPacket.NegativeResponse);
            }
            memory = rxPacket.Payload;
        }


        public void Listen()
        {

            List<PassThruMsg> rxMsgs;
            J2534Status = J2534Interface.ReadAllMessages(ChannelId, 10000, 1000, out rxMsgs, true);
            List<byte[]> msgs = new List<byte[]>();
            foreach (var msg in rxMsgs)
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
            byte[] txMsgBytes = { (byte)UDScmd.Mode.READ_DATA_BY_LOCAL_ID, subFunction };
            SendMessage(txMsgBytes);

            // attempt to read at least 1 message as a reply
            List<PassThruMsg> rxMsgs;
            ReadAllMessages(out rxMsgs, 1, 200);

        }

        public void readMemoryByCommonID(byte[] command)
        {
            // send requestResponse testerPresent message to abs ecu
            byte[] txMsgBytes = { (byte)UDScmd.Mode.READ_DATA_BY_INDENTIFIER, subFunction };
            SendMessage(txMsgBytes);

            // attempt to read at least 1 message as a reply
            List<PassThruMsg> rxMsgs;
            ReadAllMessages(out rxMsgs, 1, 200);

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
            byte[] txMsgBytes = { (byte)UDScmd.Mode.TESTER_PRESENT, subFunction };
            SendMessage(txMsgBytes);

            // attempt to read at least 1 message as a reply
            List<PassThruMsg> rxMsgs;
            ReadAllMessages(out rxMsgs, 1, 200);

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

            public UDSPacket(byte[] data, UDScmd.Mode mode)
            {
                if (data == null) return;
                if (data.Length < 1) return;
                if ((UDScmd.Response)data[0] != UDScmd.Response.NEGATIVE_RESPONSE) Response = (UDScmd.Response)(data[0] - (byte)mode);
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

        }
    }
