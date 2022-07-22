//#################################################################################################################
//# Antilock Brake Module     
//#################################################################################################################
//#ABS_SecretKey                                         = 
//ABS_DiagSig_Rx                                        = 0x760
//DiagSig_Rx[ABS_DiagSig_Rx]                            = (ABS_DiagSig_Rx, "1. ABS 0x760", "Antilock Brake Module")
//ABS_DiagSig_Tx                                        = 0x768
//DiagSig_Tx[ABS_DiagSig_Tx]                            = [ABS_DiagSig_Tx, "ABS 0x768", "Antilock Brake Module"]
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
