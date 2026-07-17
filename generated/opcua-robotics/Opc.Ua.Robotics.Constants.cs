/* ========================================================================
 * Copyright (c) 2005-2024 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;
using Opc.Ua.DI;

namespace Opc.Ua.DI {}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace Opc.Ua.Robotics
{
    #region DataType Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class DataTypes
    {
        public const uint AxisMotionProfileEnumeration = 3008;

        public const uint ExecutionModeEnumeration = 18191;

        public const uint MotionDeviceCategoryEnumeration = 18193;

        public const uint OperationalModeEnumeration = 3006;
    }
    #endregion

    #region Method Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class Methods
    {
        public const uint OperationStateMachineType_Start = 7001;

        public const uint OperationStateMachineType_Stop = 7002;

        public const uint SystemOperationStateMachineType_Start = 7004;

        public const uint SystemOperationStateMachineType_Stop = 7005;

        public const uint SystemOperationStateMachineType_GetReady = 7006;

        public const uint SystemOperationStateMachineType_StandDown = 7007;

        public const uint TaskControlStateMachineType_Start = 7008;

        public const uint TaskControlStateMachineType_Stop = 7009;

        public const uint TaskControlStateMachineType_LoadByName = 7011;

        public const uint TaskControlStateMachineType_LoadByNodeId = 7010;

        public const uint TaskControlStateMachineType_UnloadByName = 7014;

        public const uint TaskControlStateMachineType_UnloadByNodeId = 7013;

        public const uint TaskControlStateMachineType_UnloadProgram = 7012;

        public const uint ReadySubstateMachineType_ResetToProgramStart = 7003;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Disable = 7021;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Enable = 7022;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AddComment = 7020;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Acknowledge = 7019;

        public const uint AuxiliaryComponentType_Lock_InitLock = 6166;

        public const uint AuxiliaryComponentType_Lock_RenewLock = 6169;

        public const uint AuxiliaryComponentType_Lock_ExitLock = 6171;

        public const uint AuxiliaryComponentType_Lock_BreakLock = 6173;

        public const uint AxisType_Lock_InitLock = 6166;

        public const uint AxisType_Lock_RenewLock = 6169;

        public const uint AxisType_Lock_ExitLock = 6171;

        public const uint AxisType_Lock_BreakLock = 6173;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint ControllerType_Lock_InitLock = 6166;

        public const uint ControllerType_Lock_RenewLock = 6169;

        public const uint ControllerType_Lock_ExitLock = 6171;

        public const uint ControllerType_Lock_BreakLock = 6173;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint ControllerType_Programs_CreateDirectory = 7015;

        public const uint ControllerType_Programs_CreateFile = 7016;

        public const uint ControllerType_Programs_MoveOrCopy = 7018;

        public const uint ControllerType_Programs_Delete = 7017;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint DriveType_Lock_InitLock = 6166;

        public const uint DriveType_Lock_RenewLock = 6169;

        public const uint DriveType_Lock_ExitLock = 6171;

        public const uint DriveType_Lock_BreakLock = 6173;

        public const uint GearType_Lock_InitLock = 6166;

        public const uint GearType_Lock_RenewLock = 6169;

        public const uint GearType_Lock_ExitLock = 6171;

        public const uint GearType_Lock_BreakLock = 6173;

        public const uint MotionDeviceSystemType_Lock_InitLock = 6166;

        public const uint MotionDeviceSystemType_Lock_RenewLock = 6169;

        public const uint MotionDeviceSystemType_Lock_ExitLock = 6171;

        public const uint MotionDeviceSystemType_Lock_BreakLock = 6173;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory = 7015;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile = 7016;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy = 7018;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_Delete = 7017;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint MotionDeviceType_Lock_InitLock = 6166;

        public const uint MotionDeviceType_Lock_RenewLock = 6169;

        public const uint MotionDeviceType_Lock_ExitLock = 6171;

        public const uint MotionDeviceType_Lock_BreakLock = 6173;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint MotorType_Lock_InitLock = 6166;

        public const uint MotorType_Lock_RenewLock = 6169;

        public const uint MotorType_Lock_ExitLock = 6171;

        public const uint MotorType_Lock_BreakLock = 6173;

        public const uint PowerTrainType_Lock_InitLock = 6166;

        public const uint PowerTrainType_Lock_RenewLock = 6169;

        public const uint PowerTrainType_Lock_ExitLock = 6171;

        public const uint PowerTrainType_Lock_BreakLock = 6173;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_BreakLock = 6173;

        public const uint SafetyStateType_Lock_InitLock = 6166;

        public const uint SafetyStateType_Lock_RenewLock = 6169;

        public const uint SafetyStateType_Lock_ExitLock = 6171;

        public const uint SafetyStateType_Lock_BreakLock = 6173;

        public const uint TaskControlType_Lock_InitLock = 6166;

        public const uint TaskControlType_Lock_RenewLock = 6169;

        public const uint TaskControlType_Lock_ExitLock = 6171;

        public const uint TaskControlType_Lock_BreakLock = 6173;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock = 6166;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock = 6169;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock = 6171;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock = 6173;
    }
    #endregion

    #region Object Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class Objects
    {
        public const uint ExecutingSubstateMachineType_Running = 5020;

        public const uint ExecutingSubstateMachineType_RunningToStopping = 5022;

        public const uint ExecutingSubstateMachineType_Stopping = 5021;

        public const uint IdleSubstateMachineType_GettingReady = 5017;

        public const uint IdleSubstateMachineType_GettingReadyToStandBy = 5018;

        public const uint IdleSubstateMachineType_StandBy = 5015;

        public const uint IdleSubstateMachineType_StandByToGettingReady = 5019;

        public const uint OperationStateMachineType_Executing = 5007;

        public const uint OperationStateMachineType_ExecutingToIdle = 5013;

        public const uint OperationStateMachineType_ExecutingToReady = 5011;

        public const uint OperationStateMachineType_Idle = 5005;

        public const uint OperationStateMachineType_IdleToIdle = 5014;

        public const uint OperationStateMachineType_IdleToReady = 5009;

        public const uint OperationStateMachineType_Ready = 5006;

        public const uint OperationStateMachineType_ReadyToExecuting = 5012;

        public const uint OperationStateMachineType_ReadyToIdle = 5008;

        public const uint SystemOperationStateMachineType_Executing = 5032;

        public const uint SystemOperationStateMachineType_ExecutingToIdle = 5038;

        public const uint SystemOperationStateMachineType_ExecutingToReady = 5037;

        public const uint SystemOperationStateMachineType_Idle = 5030;

        public const uint SystemOperationStateMachineType_IdleToIdle = 5033;

        public const uint SystemOperationStateMachineType_IdleToReady = 5034;

        public const uint SystemOperationStateMachineType_Ready = 5031;

        public const uint SystemOperationStateMachineType_ReadyToExecuting = 5036;

        public const uint SystemOperationStateMachineType_ReadyToIdle = 5035;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine = 5028;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine = 5027;

        public const uint TaskControlStateMachineType_Executing = 5042;

        public const uint TaskControlStateMachineType_ExecutingToIdle = 5048;

        public const uint TaskControlStateMachineType_ExecutingToReady = 5047;

        public const uint TaskControlStateMachineType_Idle = 5040;

        public const uint TaskControlStateMachineType_IdleToIdle = 5043;

        public const uint TaskControlStateMachineType_IdleToReady = 5044;

        public const uint TaskControlStateMachineType_Ready = 5041;

        public const uint TaskControlStateMachineType_ReadyToExecuting = 5046;

        public const uint TaskControlStateMachineType_ReadyToIdle = 5045;

        public const uint TaskControlStateMachineType_ReadySubstateMachine = 5039;

        public const uint ReadySubstateMachineType_AtProgramStart = 5023;

        public const uint ReadySubstateMachineType_ProgramStartToSuspended = 5025;

        public const uint ReadySubstateMachineType_Suspended = 5024;

        public const uint ReadySubstateMachineType_SuspendedToProgramStart = 5026;

        public const uint SystemOperationType_Conditions = 5050;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder = 5059;

        public const uint SystemOperationType_SystemOperationStateMachine = 5049;

        public const uint TaskControlOperationType_TaskControlStateMachine = 5051;

        public const uint AxisType_ParameterSet = 16602;

        public const uint AxisType_PowerTrainIdentifier_Placeholder = 18344;

        public const uint AxisType_AdditionalLoad = 16638;

        public const uint ControllerType_ParameterSet = 5004;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder = 18964;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_ParameterSet = 18965;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Axes = 19002;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_PowerTrains = 19080;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder = 18918;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet = 18919;

        public const uint ControllerType_Components = 17252;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder = 18813;

        public const uint ControllerType_CurrentUser = 17249;

        public const uint ControllerType_Programs = 5054;

        public const uint ControllerType_Software = 15800;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder = 18847;

        public const uint ControllerType_SystemOperation = 5055;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine = 5056;

        public const uint ControllerType_TaskControls = 15826;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder = 18881;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet = 18882;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine = 5053;

        public const uint MotionDeviceSystemType_Controllers = 5001;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder = 15405;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_CurrentUser = 15440;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Software = 15483;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine = 5056;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_TaskControls = 15518;

        public const uint MotionDeviceSystemType_MotionDevices = 5002;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder = 15008;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ParameterSet = 15024;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Axes = 15062;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_PowerTrains = 15208;

        public const uint MotionDeviceSystemType_SafetyStates = 5010;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder = 15697;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet = 15698;

        public const uint MotionDeviceType_ParameterSet = 5029;

        public const uint MotionDeviceType_AdditionalComponents = 16566;

        public const uint MotionDeviceType_AdditionalComponents_AdditionalComponentIdentifier_Placeholder = 5003;

        public const uint MotionDeviceType_Axes = 15305;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder = 15743;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet = 15744;

        public const uint MotionDeviceType_FlangeLoad = 5091;

        public const uint MotionDeviceType_PowerTrains = 16443;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder = 15905;

        public const uint MotorType_ParameterSet = 5105;

        public const uint MotorType_DriveIdentifier_Placeholder = 5060;

        public const uint PowerTrainType_AxisIdentifier_Placeholder = 18536;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_ParameterSet = 18537;

        public const uint PowerTrainType_GearIdentifier_Placeholder = 16041;

        public const uint PowerTrainType_MotorIdentifier_Placeholder = 15998;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_ParameterSet = 15999;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder = 18613;

        public const uint SafetyStateType_ParameterSet = 5016;

        public const uint SafetyStateType_EmergencyStopFunctions = 17221;

        public const uint SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder = 18806;

        public const uint SafetyStateType_ProtectiveStopFunctions = 17225;

        public const uint SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder = 18809;

        public const uint TaskControlType_ParameterSet = 15883;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder = 19254;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_ParameterSet = 19255;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Axes = 19292;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_PowerTrains = 19370;

        public const uint TaskControlType_TaskControlOperation = 5052;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine = 5053;

        public const uint TaskControlType_TaskModules = 5057;

        public const uint TaskControlType_TaskModules_TaskModule_Placeholder = 5058;

        public const uint http___opcfoundation_org_UA_Robotics_ = 15011;
    }
    #endregion

    #region ObjectType Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class ObjectTypes
    {
        public const uint MultiAcknowledgeableConditionType = 1015;

        public const uint EmergencyStopFunctionType = 17230;

        public const uint LoadType = 1018;

        public const uint ProtectiveStopFunctionType = 17233;

        public const uint ExecutingSubstateMachineType = 1007;

        public const uint IdleSubstateMachineType = 1009;

        public const uint OperationStateMachineType = 1006;

        public const uint SystemOperationStateMachineType = 1021;

        public const uint TaskControlStateMachineType = 1025;

        public const uint ReadySubstateMachineType = 1012;

        public const uint SystemOperationType = 1028;

        public const uint TaskControlOperationType = 1008;

        public const uint TaskModuleType = 1016;

        public const uint AuxiliaryComponentType = 17725;

        public const uint AxisType = 16601;

        public const uint ControllerType = 1003;

        public const uint DriveType = 17793;

        public const uint GearType = 1022;

        public const uint MotionDeviceSystemType = 1002;

        public const uint MotionDeviceType = 1004;

        public const uint MotorType = 1019;

        public const uint PowerTrainType = 16794;

        public const uint SafetyStateType = 1013;

        public const uint TaskControlType = 1011;

        public const uint UserType = 18175;
    }
    #endregion

    #region ReferenceType Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class ReferenceTypes
    {
        public const uint Controls = 4002;

        public const uint HasSafetyStates = 18182;

        public const uint HasSlave = 18183;

        public const uint IsDrivenBy = 18180;

        public const uint Moves = 18178;

        public const uint Requires = 18179;

        public const uint IsConnectedTo = 18181;
    }
    #endregion

    #region Variable Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class Variables
    {
        public const uint AxisMotionProfileEnumeration_EnumStrings = 6027;

        public const uint ExecutionModeEnumeration_EnumStrings = 18192;

        public const uint MotionDeviceCategoryEnumeration_EnumStrings = 18194;

        public const uint OperationalModeEnumeration_EnumStrings = 6022;

        public const uint MultiAcknowledgeableConditionType_ConditionDescriptions = 6140;

        public const uint EmergencyStopFunctionType_Active = 17232;

        public const uint EmergencyStopFunctionType_Name = 17231;

        public const uint LoadType_CenterOfMass = 6013;

        public const uint LoadType_CenterOfMass_CartesianCoordinates = 16130;

        public const uint LoadType_CenterOfMass_Orientation = 16132;

        public const uint LoadType_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint LoadType_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint LoadType_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint LoadType_CenterOfMass_Orientation_A = 16137;

        public const uint LoadType_CenterOfMass_Orientation_B = 16138;

        public const uint LoadType_CenterOfMass_Orientation_C = 16139;

        public const uint LoadType_Inertia = 18170;

        public const uint LoadType_Inertia_X = 18171;

        public const uint LoadType_Inertia_Y = 18172;

        public const uint LoadType_Inertia_Z = 18173;

        public const uint LoadType_Mass = 6723;

        public const uint LoadType_Mass_EngineeringUnits = 6728;

        public const uint ProtectiveStopFunctionType_Active = 17236;

        public const uint ProtectiveStopFunctionType_Enabled = 17235;

        public const uint ProtectiveStopFunctionType_Name = 17234;

        public const uint ExecutingSubstateMachineType_LastTransition = 6036;

        public const uint ExecutingSubstateMachineType_LastTransition_Id = 6037;

        public const uint ExecutingSubstateMachineType_LastTransitionReason = 6038;

        public const uint ExecutingSubstateMachineType_LastTransitionReason_EnumValues = 6039;

        public const uint ExecutingSubstateMachineType_LastTransitionReason_ValueAsText = 6040;

        public const uint ExecutingSubstateMachineType_Running_StateNumber = 6041;

        public const uint ExecutingSubstateMachineType_RunningToStopping_TransitionNumber = 6043;

        public const uint ExecutingSubstateMachineType_Stopping_StateNumber = 6042;

        public const uint IdleSubstateMachineType_LastTransition = 6026;

        public const uint IdleSubstateMachineType_LastTransition_Id = 6028;

        public const uint IdleSubstateMachineType_GettingReady_StateNumber = 6031;

        public const uint IdleSubstateMachineType_GettingReadyToStandBy_TransitionNumber = 6032;

        public const uint IdleSubstateMachineType_LastTransitionReason = 6029;

        public const uint IdleSubstateMachineType_LastTransitionReason_EnumValues = 6034;

        public const uint IdleSubstateMachineType_LastTransitionReason_ValueAsText = 6035;

        public const uint IdleSubstateMachineType_StandBy_StateNumber = 6030;

        public const uint IdleSubstateMachineType_StandByToGettingReady_TransitionNumber = 6033;

        public const uint OperationStateMachineType_LastTransition = 6005;

        public const uint OperationStateMachineType_LastTransition_Id = 6006;

        public const uint OperationStateMachineType_ConfiguredDefaultStopMode = 6009;

        public const uint OperationStateMachineType_Executing_StateNumber = 6012;

        public const uint OperationStateMachineType_ExecutingToIdle_TransitionNumber = 6018;

        public const uint OperationStateMachineType_ExecutingToReady_TransitionNumber = 6016;

        public const uint OperationStateMachineType_Idle_StateNumber = 6010;

        public const uint OperationStateMachineType_IdleToIdle_TransitionNumber = 6019;

        public const uint OperationStateMachineType_IdleToReady_TransitionNumber = 6015;

        public const uint OperationStateMachineType_LastTransitionReason = 6007;

        public const uint OperationStateMachineType_LastTransitionReason_EnumValues = 6020;

        public const uint OperationStateMachineType_LastTransitionReason_ValueAsText = 6021;

        public const uint OperationStateMachineType_PossibleStopModes = 6008;

        public const uint OperationStateMachineType_Ready_StateNumber = 6011;

        public const uint OperationStateMachineType_ReadyToExecuting_TransitionNumber = 6017;

        public const uint OperationStateMachineType_ReadyToIdle_TransitionNumber = 6014;

        public const uint OperationStateMachineType_Start_OutputArguments = 6023;

        public const uint OperationStateMachineType_Stop_InputArguments = 6024;

        public const uint OperationStateMachineType_Stop_OutputArguments = 6025;

        public const uint SystemOperationStateMachineType_LastTransition = 6058;

        public const uint SystemOperationStateMachineType_LastTransition_Id = 6059;

        public const uint SystemOperationStateMachineType_ConfiguredDefaultStopMode = 6057;

        public const uint SystemOperationStateMachineType_Executing_StateNumber = 6087;

        public const uint SystemOperationStateMachineType_ExecutingToIdle_TransitionNumber = 6093;

        public const uint SystemOperationStateMachineType_ExecutingToReady_TransitionNumber = 6092;

        public const uint SystemOperationStateMachineType_Idle_StateNumber = 6085;

        public const uint SystemOperationStateMachineType_IdleToIdle_TransitionNumber = 6088;

        public const uint SystemOperationStateMachineType_IdleToReady_TransitionNumber = 6089;

        public const uint SystemOperationStateMachineType_LastTransitionReason = 6060;

        public const uint SystemOperationStateMachineType_LastTransitionReason_EnumValues = 6061;

        public const uint SystemOperationStateMachineType_LastTransitionReason_ValueAsText = 6062;

        public const uint SystemOperationStateMachineType_PossibleStopModes = 6063;

        public const uint SystemOperationStateMachineType_Ready_StateNumber = 6086;

        public const uint SystemOperationStateMachineType_ReadyToExecuting_TransitionNumber = 6091;

        public const uint SystemOperationStateMachineType_ReadyToIdle_TransitionNumber = 6090;

        public const uint SystemOperationStateMachineType_Start_OutputArguments = 6064;

        public const uint SystemOperationStateMachineType_Stop_InputArguments = 6065;

        public const uint SystemOperationStateMachineType_Stop_OutputArguments = 6066;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_CurrentState = 6083;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_CurrentState_Id = 6084;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransition = 6078;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransition_Id = 6079;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason = 6080;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason_EnumValues = 6081;

        public const uint SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = 6082;

        public const uint SystemOperationStateMachineType_GetReady_OutputArguments = 6068;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_CurrentState = 6076;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_CurrentState_Id = 6077;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_LastTransition = 6071;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_LastTransition_Id = 6072;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason = 6073;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason_EnumValues = 6074;

        public const uint SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason_ValueAsText = 6075;

        public const uint SystemOperationStateMachineType_StandDown_OutputArguments = 6070;

        public const uint TaskControlStateMachineType_LastTransition = 6095;

        public const uint TaskControlStateMachineType_LastTransition_Id = 6096;

        public const uint TaskControlStateMachineType_ConfiguredDefaultStopMode = 6094;

        public const uint TaskControlStateMachineType_Executing_StateNumber = 6114;

        public const uint TaskControlStateMachineType_ExecutingToIdle_TransitionNumber = 6122;

        public const uint TaskControlStateMachineType_ExecutingToReady_TransitionNumber = 6121;

        public const uint TaskControlStateMachineType_Idle_StateNumber = 6112;

        public const uint TaskControlStateMachineType_IdleToIdle_TransitionNumber = 6115;

        public const uint TaskControlStateMachineType_IdleToReady_TransitionNumber = 6116;

        public const uint TaskControlStateMachineType_LastTransitionReason = 6097;

        public const uint TaskControlStateMachineType_LastTransitionReason_EnumValues = 6098;

        public const uint TaskControlStateMachineType_LastTransitionReason_ValueAsText = 6099;

        public const uint TaskControlStateMachineType_PossibleStopModes = 6100;

        public const uint TaskControlStateMachineType_Ready_StateNumber = 6113;

        public const uint TaskControlStateMachineType_ReadyToExecuting_TransitionNumber = 6120;

        public const uint TaskControlStateMachineType_ReadyToIdle_TransitionNumber = 6119;

        public const uint TaskControlStateMachineType_Start_OutputArguments = 6101;

        public const uint TaskControlStateMachineType_Stop_InputArguments = 6102;

        public const uint TaskControlStateMachineType_Stop_OutputArguments = 6103;

        public const uint TaskControlStateMachineType_LoadByName_InputArguments = 6143;

        public const uint TaskControlStateMachineType_LoadByName_OutputArguments = 6144;

        public const uint TaskControlStateMachineType_LoadByNodeId_InputArguments = 6141;

        public const uint TaskControlStateMachineType_LoadByNodeId_OutputArguments = 6142;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_CurrentState = 6109;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_CurrentState_Id = 6111;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_LastTransition = 6104;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_LastTransition_Id = 6105;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason = 6106;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason_EnumValues = 6107;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason_ValueAsText = 6108;

        public const uint TaskControlStateMachineType_ReadySubstateMachine_ResetToProgramStart_OutputArguments = 6053;

        public const uint TaskControlStateMachineType_UnloadByName_InputArguments = 6067;

        public const uint TaskControlStateMachineType_UnloadByName_OutputArguments = 6069;

        public const uint TaskControlStateMachineType_UnloadByNodeId_InputArguments = 6146;

        public const uint TaskControlStateMachineType_UnloadByNodeId_OutputArguments = 6147;

        public const uint TaskControlStateMachineType_UnloadProgram_OutputArguments = 6145;

        public const uint ReadySubstateMachineType_LastTransition = 6044;

        public const uint ReadySubstateMachineType_LastTransition_Id = 6045;

        public const uint ReadySubstateMachineType_AtProgramStart_StateNumber = 6049;

        public const uint ReadySubstateMachineType_LastTransitionReason = 6046;

        public const uint ReadySubstateMachineType_LastTransitionReason_EnumValues = 6047;

        public const uint ReadySubstateMachineType_LastTransitionReason_ValueAsText = 6048;

        public const uint ReadySubstateMachineType_ProgramStartToSuspended_TransitionNumber = 6051;

        public const uint ReadySubstateMachineType_ResetToProgramStart_OutputArguments = 6053;

        public const uint ReadySubstateMachineType_Suspended_StateNumber = 6050;

        public const uint ReadySubstateMachineType_SuspendedToProgramStart_TransitionNumber = 6052;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EventId = 6205;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EventType = 6206;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_SourceNode = 6211;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_SourceName = 6210;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Time = 6212;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ReceiveTime = 6208;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Message = 6207;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Severity = 6209;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionClassId = 6195;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionClassName = 6196;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionSubClassId = 6198;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionSubClassName = 6199;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionName = 6197;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_BranchId = 6191;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Retain = 6204;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EnabledState = 6188;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EnabledState_Id = 6189;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Quality = 6202;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Quality_SourceTimestamp = 6203;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_LastSeverity = 6200;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_LastSeverity_SourceTimestamp = 6201;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Comment = 6193;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Comment_SourceTimestamp = 6194;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ClientUserId = 6192;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AddComment_InputArguments = 6190;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AckedState = 6185;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AckedState_Id = 6186;

        public const uint SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Acknowledge_InputArguments = 6187;

        public const uint SystemOperationType_DefaultInstanceBrowseName = 6130;

        public const uint SystemOperationType_SystemOperationStateMachine_CurrentState = 6128;

        public const uint SystemOperationType_SystemOperationStateMachine_CurrentState_Id = 6129;

        public const uint SystemOperationType_SystemOperationStateMachine_LastTransition = 6123;

        public const uint SystemOperationType_SystemOperationStateMachine_LastTransition_Id = 6124;

        public const uint SystemOperationType_SystemOperationStateMachine_LastTransitionReason = 6125;

        public const uint SystemOperationType_SystemOperationStateMachine_LastTransitionReason_EnumValues = 6126;

        public const uint SystemOperationType_SystemOperationStateMachine_LastTransitionReason_ValueAsText = 6127;

        public const uint SystemOperationType_SystemOperationStateMachine_Start_OutputArguments = 6064;

        public const uint SystemOperationType_SystemOperationStateMachine_Stop_InputArguments = 6065;

        public const uint SystemOperationType_SystemOperationStateMachine_Stop_OutputArguments = 6066;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState = 6083;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id = 6084;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition = 6078;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id = 6079;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason = 6080;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues = 6081;

        public const uint SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = 6082;

        public const uint SystemOperationType_SystemOperationStateMachine_GetReady_OutputArguments = 6068;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_CurrentState = 6076;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id = 6077;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransition = 6071;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id = 6072;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason = 6073;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues = 6074;

        public const uint SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText = 6075;

        public const uint SystemOperationType_SystemOperationStateMachine_StandDown_OutputArguments = 6070;

        public const uint TaskControlOperationType_DefaultInstanceBrowseName = 6132;

        public const uint TaskControlOperationType_MotionDevicesUnderControl = 6131;

        public const uint TaskControlOperationType_TaskControlStateMachine_CurrentState = 6138;

        public const uint TaskControlOperationType_TaskControlStateMachine_CurrentState_Id = 6139;

        public const uint TaskControlOperationType_TaskControlStateMachine_LastTransition = 6133;

        public const uint TaskControlOperationType_TaskControlStateMachine_LastTransition_Id = 6134;

        public const uint TaskControlOperationType_TaskControlStateMachine_LastTransitionReason = 6135;

        public const uint TaskControlOperationType_TaskControlStateMachine_LastTransitionReason_EnumValues = 6136;

        public const uint TaskControlOperationType_TaskControlStateMachine_LastTransitionReason_ValueAsText = 6137;

        public const uint TaskControlOperationType_TaskControlStateMachine_Start_OutputArguments = 6101;

        public const uint TaskControlOperationType_TaskControlStateMachine_Stop_InputArguments = 6102;

        public const uint TaskControlOperationType_TaskControlStateMachine_Stop_OutputArguments = 6103;

        public const uint TaskControlOperationType_TaskControlStateMachine_LoadByName_InputArguments = 6143;

        public const uint TaskControlOperationType_TaskControlStateMachine_LoadByName_OutputArguments = 6144;

        public const uint TaskControlOperationType_TaskControlStateMachine_LoadByNodeId_InputArguments = 6141;

        public const uint TaskControlOperationType_TaskControlStateMachine_LoadByNodeId_OutputArguments = 6142;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_CurrentState = 6109;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id = 6111;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransition = 6104;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id = 6105;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason = 6106;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues = 6107;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText = 6108;

        public const uint TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments = 6053;

        public const uint TaskControlOperationType_TaskControlStateMachine_UnloadByName_InputArguments = 6067;

        public const uint TaskControlOperationType_TaskControlStateMachine_UnloadByName_OutputArguments = 6069;

        public const uint TaskControlOperationType_TaskControlStateMachine_UnloadByNodeId_InputArguments = 6146;

        public const uint TaskControlOperationType_TaskControlStateMachine_UnloadByNodeId_OutputArguments = 6147;

        public const uint TaskControlOperationType_TaskControlStateMachine_UnloadProgram_OutputArguments = 6145;

        public const uint TaskModuleType_IsReferenced = 6056;

        public const uint TaskModuleType_Name = 6054;

        public const uint TaskModuleType_Version = 6055;

        public const uint AuxiliaryComponentType_Lock_Locked = 6468;

        public const uint AuxiliaryComponentType_Lock_LockingClient = 6163;

        public const uint AuxiliaryComponentType_Lock_LockingUser = 6164;

        public const uint AuxiliaryComponentType_Lock_RemainingLockTime = 6165;

        public const uint AuxiliaryComponentType_Lock_InitLock_InputArguments = 6167;

        public const uint AuxiliaryComponentType_Lock_InitLock_OutputArguments = 6168;

        public const uint AuxiliaryComponentType_Lock_RenewLock_OutputArguments = 6170;

        public const uint AuxiliaryComponentType_Lock_ExitLock_OutputArguments = 6172;

        public const uint AuxiliaryComponentType_Lock_BreakLock_OutputArguments = 6174;

        public const uint AuxiliaryComponentType_ProductCode = 17756;

        public const uint AuxiliaryComponentType_AssetId = 6183;

        public const uint AxisType_Lock_Locked = 6468;

        public const uint AxisType_Lock_LockingClient = 6163;

        public const uint AxisType_Lock_LockingUser = 6164;

        public const uint AxisType_Lock_RemainingLockTime = 6165;

        public const uint AxisType_Lock_InitLock_InputArguments = 6167;

        public const uint AxisType_Lock_InitLock_OutputArguments = 6168;

        public const uint AxisType_Lock_RenewLock_OutputArguments = 6170;

        public const uint AxisType_Lock_ExitLock_OutputArguments = 6172;

        public const uint AxisType_Lock_BreakLock_OutputArguments = 6174;

        public const uint AxisType_AssetId = 6175;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint AxisType_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint AxisType_AdditionalLoad_CenterOfMass_Orientation = 16132;

        public const uint AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint AxisType_AdditionalLoad_CenterOfMass_Orientation_A = 16137;

        public const uint AxisType_AdditionalLoad_CenterOfMass_Orientation_B = 16138;

        public const uint AxisType_AdditionalLoad_CenterOfMass_Orientation_C = 16139;

        public const uint AxisType_AdditionalLoad_Inertia_X = 18171;

        public const uint AxisType_AdditionalLoad_Inertia_Y = 18172;

        public const uint AxisType_AdditionalLoad_Inertia_Z = 18173;

        public const uint AxisType_AdditionalLoad_Mass = 16639;

        public const uint AxisType_AdditionalLoad_Mass_EngineeringUnits = 16644;

        public const uint AxisType_MotionProfile = 16637;

        public const uint AxisType_ParameterSet_ActualAcceleration = 16674;

        public const uint AxisType_ParameterSet_ActualAcceleration_EngineeringUnits = 16679;

        public const uint AxisType_ParameterSet_ActualPosition = 16662;

        public const uint AxisType_ParameterSet_ActualPosition_EngineeringUnits = 16667;

        public const uint AxisType_ParameterSet_ActualSpeed = 16668;

        public const uint AxisType_ParameterSet_ActualSpeed_EngineeringUnits = 16673;

        public const uint ControllerType_Lock_Locked = 6468;

        public const uint ControllerType_Lock_LockingClient = 6163;

        public const uint ControllerType_Lock_LockingUser = 6164;

        public const uint ControllerType_Lock_RemainingLockTime = 6165;

        public const uint ControllerType_Lock_InitLock_InputArguments = 6167;

        public const uint ControllerType_Lock_InitLock_OutputArguments = 6168;

        public const uint ControllerType_Lock_RenewLock_OutputArguments = 6170;

        public const uint ControllerType_Lock_ExitLock_OutputArguments = 6172;

        public const uint ControllerType_Lock_BreakLock_OutputArguments = 6174;

        public const uint ControllerType_Manufacturer = 17237;

        public const uint ControllerType_Model = 17239;

        public const uint ControllerType_ProductCode = 17245;

        public const uint ControllerType_DeviceManual = 6182;

        public const uint ControllerType_SerialNumber = 17240;

        public const uint ControllerType_AssetId = 6180;

        public const uint ControllerType_ComponentName = 6181;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Manufacturer = 18985;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_Model = 18987;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_ProductCode = 18993;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_SerialNumber = 18988;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation = 16132;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A = 16137;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B = 16138;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C = 16139;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X = 18171;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y = 18172;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z = 18173;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass = 6624;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits = 15659;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory = 18998;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride = 19001;

        public const uint ControllerType_MotionDeviceIdentifier_Placeholder_TaskControlReference = 6003;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_EmergencyStop = 18962;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_OperationalMode = 18961;

        public const uint ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_ProtectiveStop = 18963;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint ControllerType_Components_ComponentIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint ControllerType_CurrentUser_Level = 17250;

        public const uint ControllerType_ParameterSet_CabinetFanSpeed = 17365;

        public const uint ControllerType_ParameterSet_CabinetFanSpeed_EngineeringUnits = 17370;

        public const uint ControllerType_ParameterSet_CPUFanSpeed = 17371;

        public const uint ControllerType_ParameterSet_CPUFanSpeed_EngineeringUnits = 17376;

        public const uint ControllerType_ParameterSet_InputVoltage = 17377;

        public const uint ControllerType_ParameterSet_InputVoltage_EngineeringUnits = 17382;

        public const uint ControllerType_ParameterSet_StartUpTime = 15366;

        public const uint ControllerType_ParameterSet_Temperature = 17383;

        public const uint ControllerType_ParameterSet_Temperature_EngineeringUnits = 17388;

        public const uint ControllerType_ParameterSet_TotalEnergyConsumption = 17359;

        public const uint ControllerType_ParameterSet_TotalEnergyConsumption_EngineeringUnits = 17364;

        public const uint ControllerType_ParameterSet_TotalPowerOnTime = 17358;

        public const uint ControllerType_ParameterSet_UpsState = 15365;

        public const uint ControllerType_Programs_CreateDirectory_InputArguments = 6155;

        public const uint ControllerType_Programs_CreateDirectory_OutputArguments = 6156;

        public const uint ControllerType_Programs_CreateFile_InputArguments = 6157;

        public const uint ControllerType_Programs_CreateFile_OutputArguments = 6158;

        public const uint ControllerType_Programs_MoveOrCopy_InputArguments = 6160;

        public const uint ControllerType_Programs_MoveOrCopy_OutputArguments = 6161;

        public const uint ControllerType_Programs_Delete_InputArguments = 6159;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Manufacturer = 18868;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_Model = 18870;

        public const uint ControllerType_Software_SoftwareIdentifier_Placeholder_SoftwareRevision = 18873;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_CurrentState = 6162;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_CurrentState_Id = 6163;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_LastTransition = 6164;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_LastTransition_Id = 6165;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason = 6166;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason_EnumValues = 6167;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason_ValueAsText = 6168;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_Start_OutputArguments = 6064;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_Stop_InputArguments = 6065;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_Stop_OutputArguments = 6066;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState = 6083;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id = 6084;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition = 6078;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id = 6079;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason = 6080;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues = 6081;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = 6082;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_GetReady_OutputArguments = 6068;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState = 6076;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id = 6077;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition = 6071;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id = 6072;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason = 6073;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues = 6074;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText = 6075;

        public const uint ControllerType_SystemOperation_SystemOperationStateMachine_StandDown_OutputArguments = 6070;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ComponentName = 18914;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet_TaskProgramLoaded = 18916;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet_TaskProgramName = 18915;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_CurrentState = 6148;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_CurrentState_Id = 6149;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransition = 6150;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransition_Id = 6151;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason = 6152;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_EnumValues = 6153;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_ValueAsText = 6154;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Start_OutputArguments = 6101;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Stop_InputArguments = 6102;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Stop_OutputArguments = 6103;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByName_InputArguments = 6143;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByName_OutputArguments = 6144;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_InputArguments = 6141;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_OutputArguments = 6142;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState = 6109;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id = 6111;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition = 6104;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id = 6105;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason = 6106;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues = 6107;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText = 6108;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments = 6053;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByName_InputArguments = 6067;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByName_OutputArguments = 6069;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_InputArguments = 6146;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_OutputArguments = 6147;

        public const uint ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadProgram_OutputArguments = 6145;

        public const uint DriveType_Lock_Locked = 6468;

        public const uint DriveType_Lock_LockingClient = 6163;

        public const uint DriveType_Lock_LockingUser = 6164;

        public const uint DriveType_Lock_RemainingLockTime = 6165;

        public const uint DriveType_Lock_InitLock_InputArguments = 6167;

        public const uint DriveType_Lock_InitLock_OutputArguments = 6168;

        public const uint DriveType_Lock_RenewLock_OutputArguments = 6170;

        public const uint DriveType_Lock_ExitLock_OutputArguments = 6172;

        public const uint DriveType_Lock_BreakLock_OutputArguments = 6174;

        public const uint DriveType_ProductCode = 17824;

        public const uint DriveType_AssetId = 6184;

        public const uint GearType_Lock_Locked = 6468;

        public const uint GearType_Lock_LockingClient = 6163;

        public const uint GearType_Lock_LockingUser = 6164;

        public const uint GearType_Lock_RemainingLockTime = 6165;

        public const uint GearType_Lock_InitLock_InputArguments = 6167;

        public const uint GearType_Lock_InitLock_OutputArguments = 6168;

        public const uint GearType_Lock_RenewLock_OutputArguments = 6170;

        public const uint GearType_Lock_ExitLock_OutputArguments = 6172;

        public const uint GearType_Lock_BreakLock_OutputArguments = 6174;

        public const uint GearType_Manufacturer = 17152;

        public const uint GearType_Model = 17154;

        public const uint GearType_ProductCode = 17160;

        public const uint GearType_SerialNumber = 17155;

        public const uint GearType_AssetId = 6178;

        public const uint GearType_GearRatio = 15941;

        public const uint GearType_GearRatio_Numerator = 15615;

        public const uint GearType_GearRatio_Denominator = 15616;

        public const uint GearType_Pitch = 17165;

        public const uint MotionDeviceSystemType_Lock_Locked = 6468;

        public const uint MotionDeviceSystemType_Lock_LockingClient = 6163;

        public const uint MotionDeviceSystemType_Lock_LockingUser = 6164;

        public const uint MotionDeviceSystemType_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceSystemType_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceSystemType_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceSystemType_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceSystemType_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceSystemType_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceSystemType_ComponentName = 6171;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Manufacturer = 15426;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Model = 15428;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ProductCode = 15434;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SerialNumber = 15429;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_CurrentUser_Level = 15441;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_CabinetFanSpeed_EngineeringUnits = 17370;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_CPUFanSpeed_EngineeringUnits = 17376;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_InputVoltage_EngineeringUnits = 17382;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_Temperature_EngineeringUnits = 17388;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_TotalEnergyConsumption_EngineeringUnits = 17364;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory_InputArguments = 6155;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory_OutputArguments = 6156;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile_InputArguments = 6157;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile_OutputArguments = 6158;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy_InputArguments = 6160;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy_OutputArguments = 6161;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_Delete_InputArguments = 6159;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_CurrentState = 6162;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_CurrentState_Id = 6163;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransition = 6164;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransition_Id = 6165;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason = 6166;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason_EnumValues = 6167;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason_ValueAsText = 6168;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Start_OutputArguments = 6064;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Stop_InputArguments = 6065;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Stop_OutputArguments = 6066;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState = 6083;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id = 6084;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition = 6078;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id = 6079;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason = 6080;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues = 6081;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = 6082;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_GetReady_OutputArguments = 6068;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState = 6076;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id = 6077;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition = 6071;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id = 6072;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason = 6073;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues = 6074;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText = 6075;

        public const uint MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_StandDown_OutputArguments = 6070;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Manufacturer = 15045;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Model = 15047;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ProductCode = 15053;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_SerialNumber = 15048;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation = 16132;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A = 16137;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B = 16138;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C = 16139;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X = 18171;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y = 18172;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z = 18173;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass = 6624;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits = 15659;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory = 15058;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride = 15061;

        public const uint MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_TaskControlReference = 6002;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_EmergencyStop = 15741;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_OperationalMode = 15740;

        public const uint MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_ProtectiveStop = 15742;

        public const uint MotionDeviceType_Lock_Locked = 6468;

        public const uint MotionDeviceType_Lock_LockingClient = 6163;

        public const uint MotionDeviceType_Lock_LockingUser = 6164;

        public const uint MotionDeviceType_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceType_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceType_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceType_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceType_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceType_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceType_Manufacturer = 16351;

        public const uint MotionDeviceType_Model = 16353;

        public const uint MotionDeviceType_ProductCode = 16359;

        public const uint MotionDeviceType_DeviceManual = 6174;

        public const uint MotionDeviceType_SerialNumber = 16354;

        public const uint MotionDeviceType_AssetId = 6172;

        public const uint MotionDeviceType_ComponentName = 6173;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation = 16132;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_A = 16137;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_B = 16138;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_C = 16139;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_X = 18171;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Y = 18172;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Z = 18173;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Mass = 16639;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Mass_EngineeringUnits = 16644;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_MotionProfile = 15808;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualAcceleration_EngineeringUnits = 16679;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualPosition = 15863;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualPosition_EngineeringUnits = 15869;

        public const uint MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualSpeed_EngineeringUnits = 16673;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_Orientation = 16132;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_A = 16137;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_B = 16138;

        public const uint MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_C = 16139;

        public const uint MotionDeviceType_FlangeLoad_Inertia_X = 18171;

        public const uint MotionDeviceType_FlangeLoad_Inertia_Y = 18172;

        public const uint MotionDeviceType_FlangeLoad_Inertia_Z = 18173;

        public const uint MotionDeviceType_FlangeLoad_Mass = 6624;

        public const uint MotionDeviceType_FlangeLoad_Mass_EngineeringUnits = 15659;

        public const uint MotionDeviceType_MotionDeviceCategory = 16362;

        public const uint MotionDeviceType_ParameterSet_InControl = 16364;

        public const uint MotionDeviceType_ParameterSet_OnPath = 16363;

        public const uint MotionDeviceType_ParameterSet_SpeedOverride = 16365;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotionDeviceType_TaskControlReference = 6001;

        public const uint MotorType_Lock_Locked = 6468;

        public const uint MotorType_Lock_LockingClient = 6163;

        public const uint MotorType_Lock_LockingUser = 6164;

        public const uint MotorType_Lock_RemainingLockTime = 6165;

        public const uint MotorType_Lock_InitLock_InputArguments = 6167;

        public const uint MotorType_Lock_InitLock_OutputArguments = 6168;

        public const uint MotorType_Lock_RenewLock_OutputArguments = 6170;

        public const uint MotorType_Lock_ExitLock_OutputArguments = 6172;

        public const uint MotorType_Lock_BreakLock_OutputArguments = 6174;

        public const uint MotorType_Manufacturer = 17101;

        public const uint MotorType_Model = 17103;

        public const uint MotorType_ProductCode = 17109;

        public const uint MotorType_SerialNumber = 17104;

        public const uint MotorType_AssetId = 6177;

        public const uint MotorType_ParameterSet_BrakeReleased = 17150;

        public const uint MotorType_ParameterSet_EffectiveLoadRate = 6776;

        public const uint MotorType_ParameterSet_MotorTemperature = 6757;

        public const uint MotorType_ParameterSet_MotorTemperature_EngineeringUnits = 6762;

        public const uint PowerTrainType_Lock_Locked = 6468;

        public const uint PowerTrainType_Lock_LockingClient = 6163;

        public const uint PowerTrainType_Lock_LockingUser = 6164;

        public const uint PowerTrainType_Lock_RemainingLockTime = 6165;

        public const uint PowerTrainType_Lock_InitLock_InputArguments = 6167;

        public const uint PowerTrainType_Lock_InitLock_OutputArguments = 6168;

        public const uint PowerTrainType_Lock_RenewLock_OutputArguments = 6170;

        public const uint PowerTrainType_Lock_ExitLock_OutputArguments = 6172;

        public const uint PowerTrainType_Lock_BreakLock_OutputArguments = 6174;

        public const uint PowerTrainType_ComponentName = 6176;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation = 16132;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_A = 16137;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_B = 16138;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_C = 16139;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_X = 18171;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Y = 18172;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Z = 18173;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Mass = 16639;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Mass_EngineeringUnits = 16644;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_MotionProfile = 18570;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualAcceleration_EngineeringUnits = 16679;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualPosition = 18595;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualPosition_EngineeringUnits = 18600;

        public const uint PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualSpeed_EngineeringUnits = 16673;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Manufacturer = 16062;

        public const uint PowerTrainType_GearIdentifier_Placeholder_Model = 16064;

        public const uint PowerTrainType_GearIdentifier_Placeholder_ProductCode = 16068;

        public const uint PowerTrainType_GearIdentifier_Placeholder_SerialNumber = 16071;

        public const uint PowerTrainType_GearIdentifier_Placeholder_GearRatio = 16076;

        public const uint PowerTrainType_GearIdentifier_Placeholder_GearRatio_Numerator = 16077;

        public const uint PowerTrainType_GearIdentifier_Placeholder_GearRatio_Denominator = 16078;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Manufacturer = 16019;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_Model = 16021;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_ProductCode = 16025;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_SerialNumber = 16028;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_ParameterSet_MotorTemperature = 16034;

        public const uint PowerTrainType_MotorIdentifier_Placeholder_ParameterSet_MotorTemperature_EngineeringUnits = 16039;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint PowerTrainType_PowerTrainIdentifier_Placeholder_ComponentName = 6176;

        public const uint SafetyStateType_Lock_Locked = 6468;

        public const uint SafetyStateType_Lock_LockingClient = 6163;

        public const uint SafetyStateType_Lock_LockingUser = 6164;

        public const uint SafetyStateType_Lock_RemainingLockTime = 6165;

        public const uint SafetyStateType_Lock_InitLock_InputArguments = 6167;

        public const uint SafetyStateType_Lock_InitLock_OutputArguments = 6168;

        public const uint SafetyStateType_Lock_RenewLock_OutputArguments = 6170;

        public const uint SafetyStateType_Lock_ExitLock_OutputArguments = 6172;

        public const uint SafetyStateType_Lock_BreakLock_OutputArguments = 6174;

        public const uint SafetyStateType_ComponentName = 6179;

        public const uint SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder_Active = 18808;

        public const uint SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder_Name = 18807;

        public const uint SafetyStateType_ParameterSet_EmergencyStop = 15882;

        public const uint SafetyStateType_ParameterSet_OperationalMode = 15912;

        public const uint SafetyStateType_ParameterSet_ProtectiveStop = 15913;

        public const uint SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Active = 18812;

        public const uint SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Enabled = 18811;

        public const uint SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Name = 18810;

        public const uint TaskControlType_Lock_Locked = 6468;

        public const uint TaskControlType_Lock_LockingClient = 6163;

        public const uint TaskControlType_Lock_LockingUser = 6164;

        public const uint TaskControlType_Lock_RemainingLockTime = 6165;

        public const uint TaskControlType_Lock_InitLock_InputArguments = 6167;

        public const uint TaskControlType_Lock_InitLock_OutputArguments = 6168;

        public const uint TaskControlType_Lock_RenewLock_OutputArguments = 6170;

        public const uint TaskControlType_Lock_ExitLock_OutputArguments = 6172;

        public const uint TaskControlType_Lock_BreakLock_OutputArguments = 6174;

        public const uint TaskControlType_ComponentName = 17873;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_Locked = 6468;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_LockingClient = 6163;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_LockingUser = 6164;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime = 6165;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments = 6167;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments = 6168;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments = 6170;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments = 6172;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments = 6174;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Manufacturer = 19275;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_Model = 19277;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_ProductCode = 19283;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_SerialNumber = 19278;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates = 16130;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation = 16132;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X = 16134;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = 16135;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = 16136;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A = 16137;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B = 16138;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C = 16139;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X = 18171;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y = 18172;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z = 18173;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass = 6624;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits = 15659;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory = 19288;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride = 19291;

        public const uint TaskControlType_MotionDeviceIdentifier_Placeholder_TaskControlReference = 6004;

        public const uint TaskControlType_ParameterSet_ExecutionMode = 17876;

        public const uint TaskControlType_ParameterSet_TaskProgramLoaded = 17875;

        public const uint TaskControlType_ParameterSet_TaskProgramName = 17874;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_CurrentState = 6148;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_CurrentState_Id = 6149;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransition = 6150;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransition_Id = 6151;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason = 6152;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_EnumValues = 6153;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_ValueAsText = 6154;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_Start_OutputArguments = 6101;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_Stop_InputArguments = 6102;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_Stop_OutputArguments = 6103;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByName_InputArguments = 6143;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByName_OutputArguments = 6144;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_InputArguments = 6141;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_OutputArguments = 6142;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState = 6109;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id = 6111;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition = 6104;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id = 6105;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason = 6106;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues = 6107;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText = 6108;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments = 6053;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByName_InputArguments = 6067;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByName_OutputArguments = 6069;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_InputArguments = 6146;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_OutputArguments = 6147;

        public const uint TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadProgram_OutputArguments = 6145;

        public const uint TaskControlType_TaskModules_TaskModule_Placeholder_Name = 6169;

        public const uint UserType_Level = 18176;

        public const uint UserType_Name = 18177;

        public const uint http___opcfoundation_org_UA_Robotics__NamespaceUri = 15034;

        public const uint http___opcfoundation_org_UA_Robotics__NamespaceVersion = 15064;

        public const uint http___opcfoundation_org_UA_Robotics__NamespacePublicationDate = 15091;

        public const uint http___opcfoundation_org_UA_Robotics__IsNamespaceSubset = 15114;

        public const uint http___opcfoundation_org_UA_Robotics__StaticNodeIdTypes = 15145;

        public const uint http___opcfoundation_org_UA_Robotics__StaticNumericNodeIdRange = 15173;

        public const uint http___opcfoundation_org_UA_Robotics__StaticStringNodeIdPattern = 15209;
    }
    #endregion

    #region DataType Node Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class DataTypeIds
    {
        public static readonly ExpandedNodeId AxisMotionProfileEnumeration = new ExpandedNodeId(Opc.Ua.Robotics.DataTypes.AxisMotionProfileEnumeration, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutionModeEnumeration = new ExpandedNodeId(Opc.Ua.Robotics.DataTypes.ExecutionModeEnumeration, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceCategoryEnumeration = new ExpandedNodeId(Opc.Ua.Robotics.DataTypes.MotionDeviceCategoryEnumeration, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationalModeEnumeration = new ExpandedNodeId(Opc.Ua.Robotics.DataTypes.OperationalModeEnumeration, Opc.Ua.Robotics.Namespaces.Robotics);
    }
    #endregion

    #region Method Node Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class MethodIds
    {
        public static readonly ExpandedNodeId OperationStateMachineType_Start = new ExpandedNodeId(Opc.Ua.Robotics.Methods.OperationStateMachineType_Start, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Stop = new ExpandedNodeId(Opc.Ua.Robotics.Methods.OperationStateMachineType_Stop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Start = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationStateMachineType_Start, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Stop = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationStateMachineType_Stop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_GetReady = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationStateMachineType_GetReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_StandDown = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationStateMachineType_StandDown, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Start = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_Start, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Stop = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_Stop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LoadByName = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_LoadByName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LoadByNodeId = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_LoadByNodeId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadByName = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_UnloadByName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadByNodeId = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_UnloadByNodeId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadProgram = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlStateMachineType_UnloadProgram, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_ResetToProgramStart = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ReadySubstateMachineType_ResetToProgramStart, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Disable = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Disable, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Enable = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Enable, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AddComment = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AddComment, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Acknowledge = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Acknowledge, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AuxiliaryComponentType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AuxiliaryComponentType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AuxiliaryComponentType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AuxiliaryComponentType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_PowerTrainIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_PowerTrainIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.AxisType_PowerTrainIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_CreateDirectory = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Programs_CreateDirectory, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_CreateFile = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Programs_CreateFile, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_MoveOrCopy = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Programs_MoveOrCopy, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_Delete = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Programs_Delete, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.DriveType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.DriveType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.DriveType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.DriveType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.GearType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.GearType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.GearType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.GearType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_Delete = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_Delete, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotorType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotorType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotorType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.MotorType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_AxisIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_AxisIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_AxisIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_GearIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_GearIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_GearIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_MotorIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_MotorIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_MotorIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SafetyStateType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SafetyStateType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SafetyStateType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.SafetyStateType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock = new ExpandedNodeId(Opc.Ua.Robotics.Methods.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock, Opc.Ua.Robotics.Namespaces.Robotics);
    }
    #endregion

    #region Object Node Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class ObjectIds
    {
        public static readonly ExpandedNodeId ExecutingSubstateMachineType_Running = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ExecutingSubstateMachineType_Running, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_RunningToStopping = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ExecutingSubstateMachineType_RunningToStopping, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_Stopping = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ExecutingSubstateMachineType_Stopping, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_GettingReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.IdleSubstateMachineType_GettingReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_GettingReadyToStandBy = new ExpandedNodeId(Opc.Ua.Robotics.Objects.IdleSubstateMachineType_GettingReadyToStandBy, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_StandBy = new ExpandedNodeId(Opc.Ua.Robotics.Objects.IdleSubstateMachineType_StandBy, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_StandByToGettingReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.IdleSubstateMachineType_StandByToGettingReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Executing = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_Executing, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ExecutingToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_ExecutingToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ExecutingToReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_ExecutingToReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Idle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_Idle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_IdleToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_IdleToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_IdleToReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_IdleToReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Ready = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_Ready, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ReadyToExecuting = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_ReadyToExecuting, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ReadyToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.OperationStateMachineType_ReadyToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Executing = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_Executing, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_ExecutingToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingToReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_ExecutingToReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Idle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_Idle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_IdleToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleToReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_IdleToReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Ready = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_Ready, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ReadyToExecuting = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_ReadyToExecuting, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ReadyToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_ReadyToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_ExecutingSubstateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationStateMachineType_IdleSubstateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Executing = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_Executing, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ExecutingToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_ExecutingToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ExecutingToReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_ExecutingToReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Idle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_Idle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_IdleToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_IdleToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_IdleToReady = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_IdleToReady, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Ready = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_Ready, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadyToExecuting = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_ReadyToExecuting, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadyToIdle = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_ReadyToIdle, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlStateMachineType_ReadySubstateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_AtProgramStart = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ReadySubstateMachineType_AtProgramStart, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_ProgramStartToSuspended = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ReadySubstateMachineType_ProgramStartToSuspended, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_Suspended = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ReadySubstateMachineType_Suspended, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_SuspendedToProgramStart = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ReadySubstateMachineType_SuspendedToProgramStart, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationType_Conditions, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SystemOperationType_SystemOperationStateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlOperationType_TaskControlStateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.AxisType_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.AxisType_PowerTrainIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad = new ExpandedNodeId(Opc.Ua.Robotics.Objects.AxisType_AdditionalLoad, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_MotionDeviceIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_MotionDeviceIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Axes = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_MotionDeviceIdentifier_Placeholder_Axes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_PowerTrains = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_MotionDeviceIdentifier_Placeholder_PowerTrains, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_SafetyStatesIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_Components, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_Components_ComponentIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_CurrentUser = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_CurrentUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_Programs, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_Software, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_Software_SoftwareIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_SystemOperation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_SystemOperation_SystemOperationStateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_TaskControls, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_TaskControls_TaskControlIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_Controllers, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_CurrentUser = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_CurrentUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Software = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Software, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_TaskControls = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_TaskControls, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_MotionDevices, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Axes = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Axes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_PowerTrains = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_PowerTrains, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_SafetyStates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_AdditionalComponents = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_AdditionalComponents, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_AdditionalComponents_AdditionalComponentIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_AdditionalComponents_AdditionalComponentIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_Axes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_Axes_AxisIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_FlangeLoad, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_PowerTrains, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotorType_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_DriveIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.MotorType_DriveIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.PowerTrainType_AxisIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.PowerTrainType_AxisIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.PowerTrainType_GearIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.PowerTrainType_MotorIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.PowerTrainType_MotorIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.PowerTrainType_PowerTrainIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SafetyStateType_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_EmergencyStopFunctions = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SafetyStateType_EmergencyStopFunctions, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ProtectiveStopFunctions = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SafetyStateType_ProtectiveStopFunctions, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_MotionDeviceIdentifier_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_ParameterSet = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_MotionDeviceIdentifier_Placeholder_ParameterSet, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Axes = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_MotionDeviceIdentifier_Placeholder_Axes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_PowerTrains = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_MotionDeviceIdentifier_Placeholder_PowerTrains, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_TaskControlOperation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_TaskControlOperation_TaskControlStateMachine, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskModules = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_TaskModules, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskModules_TaskModule_Placeholder = new ExpandedNodeId(Opc.Ua.Robotics.Objects.TaskControlType_TaskModules_TaskModule_Placeholder, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics_ = new ExpandedNodeId(Opc.Ua.Robotics.Objects.http___opcfoundation_org_UA_Robotics_, Opc.Ua.Robotics.Namespaces.Robotics);
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class ObjectTypeIds
    {
        public static readonly ExpandedNodeId MultiAcknowledgeableConditionType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.MultiAcknowledgeableConditionType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId EmergencyStopFunctionType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.EmergencyStopFunctionType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.LoadType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ProtectiveStopFunctionType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.ProtectiveStopFunctionType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.ExecutingSubstateMachineType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.IdleSubstateMachineType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.OperationStateMachineType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.SystemOperationStateMachineType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.TaskControlStateMachineType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.ReadySubstateMachineType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.SystemOperationType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.TaskControlOperationType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskModuleType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.TaskModuleType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.AuxiliaryComponentType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.AxisType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.ControllerType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.DriveType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.GearType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.MotionDeviceSystemType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.MotionDeviceType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.MotorType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.PowerTrainType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.SafetyStateType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.TaskControlType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId UserType = new ExpandedNodeId(Opc.Ua.Robotics.ObjectTypes.UserType, Opc.Ua.Robotics.Namespaces.Robotics);
    }
    #endregion

    #region ReferenceType Node Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class ReferenceTypeIds
    {
        public static readonly ExpandedNodeId Controls = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.Controls, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId HasSafetyStates = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.HasSafetyStates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId HasSlave = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.HasSlave, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IsDrivenBy = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.IsDrivenBy, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId Moves = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.Moves, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId Requires = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.Requires, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IsConnectedTo = new ExpandedNodeId(Opc.Ua.Robotics.ReferenceTypes.IsConnectedTo, Opc.Ua.Robotics.Namespaces.Robotics);
    }
    #endregion

    #region Variable Node Identifiers
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class VariableIds
    {
        public static readonly ExpandedNodeId AxisMotionProfileEnumeration_EnumStrings = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisMotionProfileEnumeration_EnumStrings, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutionModeEnumeration_EnumStrings = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutionModeEnumeration_EnumStrings, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceCategoryEnumeration_EnumStrings = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceCategoryEnumeration_EnumStrings, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationalModeEnumeration_EnumStrings = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationalModeEnumeration_EnumStrings, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MultiAcknowledgeableConditionType_ConditionDescriptions = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MultiAcknowledgeableConditionType_ConditionDescriptions, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId EmergencyStopFunctionType_Active = new ExpandedNodeId(Opc.Ua.Robotics.Variables.EmergencyStopFunctionType_Active, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId EmergencyStopFunctionType_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.EmergencyStopFunctionType_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_Inertia = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_Inertia, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId LoadType_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.LoadType_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ProtectiveStopFunctionType_Active = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ProtectiveStopFunctionType_Active, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ProtectiveStopFunctionType_Enabled = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ProtectiveStopFunctionType_Enabled, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ProtectiveStopFunctionType_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ProtectiveStopFunctionType_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_Running_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_Running_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_RunningToStopping_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_RunningToStopping_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ExecutingSubstateMachineType_Stopping_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ExecutingSubstateMachineType_Stopping_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_GettingReady_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_GettingReady_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_GettingReadyToStandBy_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_GettingReadyToStandBy_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_StandBy_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_StandBy_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId IdleSubstateMachineType_StandByToGettingReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.IdleSubstateMachineType_StandByToGettingReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ConfiguredDefaultStopMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_ConfiguredDefaultStopMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Executing_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_Executing_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ExecutingToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_ExecutingToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ExecutingToReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_ExecutingToReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Idle_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_Idle_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_IdleToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_IdleToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_IdleToReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_IdleToReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_PossibleStopModes = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_PossibleStopModes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Ready_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_Ready_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ReadyToExecuting_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_ReadyToExecuting_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_ReadyToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_ReadyToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId OperationStateMachineType_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.OperationStateMachineType_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ConfiguredDefaultStopMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ConfiguredDefaultStopMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Executing_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_Executing_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingToReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingToReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Idle_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_Idle_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleToReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleToReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_PossibleStopModes = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_PossibleStopModes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Ready_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_Ready_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ReadyToExecuting_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ReadyToExecuting_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ReadyToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ReadyToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_ExecutingSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_GetReady_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_GetReady_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_IdleSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationStateMachineType_StandDown_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationStateMachineType_StandDown_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ConfiguredDefaultStopMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ConfiguredDefaultStopMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Executing_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_Executing_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ExecutingToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ExecutingToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ExecutingToReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ExecutingToReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Idle_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_Idle_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_IdleToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_IdleToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_IdleToReady_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_IdleToReady_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_PossibleStopModes = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_PossibleStopModes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Ready_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_Ready_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadyToExecuting_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadyToExecuting_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadyToIdle_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadyToIdle_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LoadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LoadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LoadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LoadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LoadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LoadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_LoadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_LoadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_ReadySubstateMachine_ResetToProgramStart_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_ReadySubstateMachine_ResetToProgramStart_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_UnloadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_UnloadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_UnloadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_UnloadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlStateMachineType_UnloadProgram_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlStateMachineType_UnloadProgram_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_AtProgramStart_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_AtProgramStart_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_ProgramStartToSuspended_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_ProgramStartToSuspended_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_ResetToProgramStart_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_ResetToProgramStart_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_Suspended_StateNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_Suspended_StateNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ReadySubstateMachineType_SuspendedToProgramStart_TransitionNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ReadySubstateMachineType_SuspendedToProgramStart_TransitionNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EventId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EventId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EventType = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EventType, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_SourceNode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_SourceNode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_SourceName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_SourceName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Time = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Time, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ReceiveTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ReceiveTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Message = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Message, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Severity = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Severity, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionClassId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionClassId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionClassName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionClassName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionSubClassId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionSubClassId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionSubClassName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionSubClassName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ConditionName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_BranchId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_BranchId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Retain = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Retain, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EnabledState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EnabledState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EnabledState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_EnabledState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Quality = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Quality, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Quality_SourceTimestamp = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Quality_SourceTimestamp, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_LastSeverity = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_LastSeverity, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_LastSeverity_SourceTimestamp = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_LastSeverity_SourceTimestamp, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Comment = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Comment, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Comment_SourceTimestamp = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Comment_SourceTimestamp, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ClientUserId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_ClientUserId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AddComment_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AddComment_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AckedState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AckedState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AckedState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_AckedState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Acknowledge_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_Conditions_AcknowledgeableCondition_Placeholder_Acknowledge_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_DefaultInstanceBrowseName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_DefaultInstanceBrowseName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_GetReady_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_GetReady_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SystemOperationType_SystemOperationStateMachine_StandDown_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SystemOperationType_SystemOperationStateMachine_StandDown_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_DefaultInstanceBrowseName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_DefaultInstanceBrowseName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_MotionDevicesUnderControl = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_MotionDevicesUnderControl, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LoadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LoadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LoadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LoadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LoadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LoadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_LoadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_LoadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_UnloadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_UnloadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_UnloadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_UnloadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_UnloadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_UnloadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_UnloadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_UnloadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlOperationType_TaskControlStateMachine_UnloadProgram_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlOperationType_TaskControlStateMachine_UnloadProgram_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskModuleType_IsReferenced = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskModuleType_IsReferenced, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskModuleType_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskModuleType_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskModuleType_Version = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskModuleType_Version, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AuxiliaryComponentType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AuxiliaryComponentType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_AdditionalLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_AdditionalLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_MotionProfile = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_MotionProfile, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet_ActualAcceleration = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_ParameterSet_ActualAcceleration, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet_ActualAcceleration_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_ParameterSet_ActualAcceleration_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet_ActualPosition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_ParameterSet_ActualPosition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet_ActualPosition_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_ParameterSet_ActualPosition_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet_ActualSpeed = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_ParameterSet_ActualSpeed, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId AxisType_ParameterSet_ActualSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.AxisType_ParameterSet_ActualSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_DeviceManual = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_DeviceManual, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_MotionDeviceIdentifier_Placeholder_TaskControlReference = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_MotionDeviceIdentifier_Placeholder_TaskControlReference, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_EmergencyStop = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_EmergencyStop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_OperationalMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_OperationalMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_ProtectiveStop = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SafetyStatesIdentifier_Placeholder_ParameterSet_ProtectiveStop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Components_ComponentIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Components_ComponentIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_CurrentUser_Level = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_CurrentUser_Level, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_CabinetFanSpeed = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_CabinetFanSpeed, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_CabinetFanSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_CabinetFanSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_CPUFanSpeed = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_CPUFanSpeed, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_CPUFanSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_CPUFanSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_InputVoltage = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_InputVoltage, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_InputVoltage_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_InputVoltage_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_StartUpTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_StartUpTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_Temperature = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_Temperature, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_Temperature_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_Temperature_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_TotalEnergyConsumption = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_TotalEnergyConsumption, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_TotalEnergyConsumption_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_TotalEnergyConsumption_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_TotalPowerOnTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_TotalPowerOnTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_ParameterSet_UpsState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_ParameterSet_UpsState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_CreateDirectory_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_CreateDirectory_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_CreateDirectory_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_CreateDirectory_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_CreateFile_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_CreateFile_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_CreateFile_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_CreateFile_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_MoveOrCopy_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_MoveOrCopy_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_MoveOrCopy_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_MoveOrCopy_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Programs_Delete_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Programs_Delete_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_Software_SoftwareIdentifier_Placeholder_SoftwareRevision = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_Software_SoftwareIdentifier_Placeholder_SoftwareRevision, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_GetReady_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_GetReady_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_SystemOperation_SystemOperationStateMachine_StandDown_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_SystemOperation_SystemOperationStateMachine_StandDown_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet_TaskProgramLoaded = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet_TaskProgramLoaded, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet_TaskProgramName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_ParameterSet_TaskProgramName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadProgram_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.ControllerType_TaskControls_TaskControlIdentifier_Placeholder_TaskControlOperation_TaskControlStateMachine_UnloadProgram_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId DriveType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.DriveType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_GearRatio = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_GearRatio, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_GearRatio_Numerator = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_GearRatio_Numerator, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_GearRatio_Denominator = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_GearRatio_Denominator, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId GearType_Pitch = new ExpandedNodeId(Opc.Ua.Robotics.Variables.GearType_Pitch, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_CurrentUser_Level = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_CurrentUser_Level, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_CabinetFanSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_CabinetFanSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_CPUFanSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_CPUFanSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_InputVoltage_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_InputVoltage_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_Temperature_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_Temperature_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_TotalEnergyConsumption_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_ParameterSet_TotalEnergyConsumption_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateDirectory_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_CreateFile_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_MoveOrCopy_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_Delete_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_Programs_Delete_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_ExecutingSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_GetReady_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_GetReady_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_IdleSubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_StandDown_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_Controllers_ControllerIdentifier_Placeholder_SystemOperation_SystemOperationStateMachine_StandDown_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_TaskControlReference = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_MotionDevices_MotionDeviceIdentifier_Placeholder_TaskControlReference, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_EmergencyStop = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_EmergencyStop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_OperationalMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_OperationalMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_ProtectiveStop = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceSystemType_SafetyStates_SafetyStateIdentifier_Placeholder_ParameterSet_ProtectiveStop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_DeviceManual = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_DeviceManual, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_AdditionalLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_MotionProfile = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_MotionProfile, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualAcceleration_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualAcceleration_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualPosition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualPosition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualPosition_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualPosition_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_Axes_AxisIdentifier_Placeholder_ParameterSet_ActualSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_FlangeLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_FlangeLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_MotionDeviceCategory = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_MotionDeviceCategory, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_ParameterSet_InControl = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_ParameterSet_InControl, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_ParameterSet_OnPath = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_ParameterSet_OnPath, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_ParameterSet_SpeedOverride = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_ParameterSet_SpeedOverride, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_PowerTrains_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotionDeviceType_TaskControlReference = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotionDeviceType_TaskControlReference, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_AssetId = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_AssetId, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_ParameterSet_BrakeReleased = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_ParameterSet_BrakeReleased, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_ParameterSet_EffectiveLoadRate = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_ParameterSet_EffectiveLoadRate, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_ParameterSet_MotorTemperature = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_ParameterSet_MotorTemperature, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId MotorType_ParameterSet_MotorTemperature_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.MotorType_ParameterSet_MotorTemperature_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_AdditionalLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_MotionProfile = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_MotionProfile, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualAcceleration_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualAcceleration_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualPosition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualPosition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualPosition_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualPosition_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualSpeed_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_AxisIdentifier_Placeholder_ParameterSet_ActualSpeed_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_GearRatio = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_GearRatio, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_GearRatio_Numerator = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_GearRatio_Numerator, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_GearIdentifier_Placeholder_GearRatio_Denominator = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_GearIdentifier_Placeholder_GearRatio_Denominator, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_ParameterSet_MotorTemperature = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_ParameterSet_MotorTemperature, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_MotorIdentifier_Placeholder_ParameterSet_MotorTemperature_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_MotorIdentifier_Placeholder_ParameterSet_MotorTemperature_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId PowerTrainType_PowerTrainIdentifier_Placeholder_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.PowerTrainType_PowerTrainIdentifier_Placeholder_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder_Active = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder_Active, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_EmergencyStopFunctions_EmergencyStopFunctionIdentifier_Placeholder_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ParameterSet_EmergencyStop = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ParameterSet_EmergencyStop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ParameterSet_OperationalMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ParameterSet_OperationalMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ParameterSet_ProtectiveStop = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ParameterSet_ProtectiveStop, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Active = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Active, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Enabled = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Enabled, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.SafetyStateType_ProtectiveStopFunctions_ProtectiveStopFunctionIdentifier_Placeholder_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_ComponentName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_ComponentName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_Locked = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_Locked, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_LockingClient = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_LockingClient, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_LockingUser = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_LockingUser, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RemainingLockTime, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_InitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_RenewLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_ExitLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Lock_BreakLock_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Manufacturer = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Manufacturer, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_Model = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_Model, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_ProductCode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_ProductCode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_SerialNumber = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_SerialNumber, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_CartesianCoordinates_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_A, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_B, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_CenterOfMass_Orientation_C, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_X, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Y, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Inertia_Z, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_FlangeLoad_Mass_EngineeringUnits, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_MotionDeviceCategory, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_ParameterSet_SpeedOverride, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_MotionDeviceIdentifier_Placeholder_TaskControlReference = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_MotionDeviceIdentifier_Placeholder_TaskControlReference, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_ParameterSet_ExecutionMode = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_ParameterSet_ExecutionMode, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_ParameterSet_TaskProgramLoaded = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_ParameterSet_TaskProgramLoaded, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_ParameterSet_TaskProgramName = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_ParameterSet_TaskProgramName, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_Start_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_Start_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_Stop_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_Stop_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_Stop_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_Stop_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_LoadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_CurrentState_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransition_Id, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_EnumValues, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_LastTransitionReason_ValueAsText, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_ReadySubstateMachine_ResetToProgramStart_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByName_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByName_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByName_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByName_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_InputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_InputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadByNodeId_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadProgram_OutputArguments = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskControlOperation_TaskControlStateMachine_UnloadProgram_OutputArguments, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId TaskControlType_TaskModules_TaskModule_Placeholder_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.TaskControlType_TaskModules_TaskModule_Placeholder_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId UserType_Level = new ExpandedNodeId(Opc.Ua.Robotics.Variables.UserType_Level, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId UserType_Name = new ExpandedNodeId(Opc.Ua.Robotics.Variables.UserType_Name, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__NamespaceUri = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__NamespaceUri, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__NamespaceVersion = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__NamespaceVersion, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__NamespacePublicationDate = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__NamespacePublicationDate, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__IsNamespaceSubset = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__IsNamespaceSubset, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__StaticNodeIdTypes = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__StaticNodeIdTypes, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__StaticNumericNodeIdRange = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__StaticNumericNodeIdRange, Opc.Ua.Robotics.Namespaces.Robotics);

        public static readonly ExpandedNodeId http___opcfoundation_org_UA_Robotics__StaticStringNodeIdPattern = new ExpandedNodeId(Opc.Ua.Robotics.Variables.http___opcfoundation_org_UA_Robotics__StaticStringNodeIdPattern, Opc.Ua.Robotics.Namespaces.Robotics);
    }
    #endregion

    #region BrowseName Declarations
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class BrowseNames
    {
        public const string AcknowledgeableCondition_Placeholder = "<AcknowledgeableCondition>";

        public const string Active = "Active";

        public const string ActualAcceleration = "ActualAcceleration";

        public const string ActualPosition = "ActualPosition";

        public const string ActualSpeed = "ActualSpeed";

        public const string AdditionalComponentIdentifier_Placeholder = "<AdditionalComponentIdentifier>";

        public const string AdditionalComponents = "AdditionalComponents";

        public const string AdditionalLoad = "AdditionalLoad";

        public const string AtProgramStart = "AtProgramStart";

        public const string AuxiliaryComponentType = "AuxiliaryComponentType";

        public const string Axes = "Axes";

        public const string AxisIdentifier_Placeholder = "<AxisIdentifier>";

        public const string AxisMotionProfileEnumeration = "AxisMotionProfileEnumeration";

        public const string AxisType = "AxisType";

        public const string BrakeReleased = "BrakeReleased";

        public const string CabinetFanSpeed = "CabinetFanSpeed";

        public const string CenterOfMass = "CenterOfMass";

        public const string ComponentIdentifier_Placeholder = "<ComponentIdentifier>";

        public const string Components = "Components";

        public const string ConditionDescriptions = "ConditionDescriptions";

        public const string Conditions = "Conditions";

        public const string ConfiguredDefaultStopMode = "ConfiguredDefaultStopMode";

        public const string ControllerIdentifier_Placeholder = "<ControllerIdentifier>";

        public const string Controllers = "Controllers";

        public const string ControllerType = "ControllerType";

        public const string Controls = "Controls";

        public const string CPUFanSpeed = "CPUFanSpeed";

        public const string CurrentUser = "CurrentUser";

        public const string DriveIdentifier_Placeholder = "<DriveIdentifier>";

        public const string DriveType = "DriveType";

        public const string EffectiveLoadRate = "EffectiveLoadRate";

        public const string EmergencyStop = "EmergencyStop";

        public const string EmergencyStopFunctionIdentifier_Placeholder = "<EmergencyStopFunctionIdentifier>";

        public const string EmergencyStopFunctions = "EmergencyStopFunctions";

        public const string EmergencyStopFunctionType = "EmergencyStopFunctionType";

        public const string Enabled = "Enabled";

        public const string Executing = "Executing";

        public const string ExecutingSubstateMachine = "ExecutingSubstateMachine";

        public const string ExecutingSubstateMachineType = "ExecutingSubstateMachineType";

        public const string ExecutingToIdle = "ExecutingToIdle";

        public const string ExecutingToReady = "ExecutingToReady";

        public const string ExecutionMode = "ExecutionMode";

        public const string ExecutionModeEnumeration = "ExecutionModeEnumeration";

        public const string FlangeLoad = "FlangeLoad";

        public const string GearIdentifier_Placeholder = "<GearIdentifier>";

        public const string GearRatio = "GearRatio";

        public const string GearType = "GearType";

        public const string GetReady = "GetReady";

        public const string GettingReady = "GettingReady";

        public const string GettingReadyToStandBy = "GettingReadyToStandBy";

        public const string HasSafetyStates = "HasSafetyStates";

        public const string HasSlave = "HasSlave";

        public const string http___opcfoundation_org_UA_Robotics_ = "http://opcfoundation.org/UA/Robotics/";

        public const string Idle = "Idle";

        public const string IdleSubstateMachine = "IdleSubstateMachine";

        public const string IdleSubstateMachineType = "IdleSubstateMachineType";

        public const string IdleToIdle = "IdleToIdle";

        public const string IdleToReady = "IdleToReady";

        public const string InControl = "InControl";

        public const string Inertia = "Inertia";

        public const string InputVoltage = "InputVoltage";

        public const string IsConnectedTo = "IsConnectedTo";

        public const string IsDrivenBy = "IsDrivenBy";

        public const string IsReferenced = "IsReferenced";

        public const string LastTransitionReason = "LastTransitionReason";

        public const string Level = "Level";

        public const string LoadByName = "LoadByName";

        public const string LoadByNodeId = "LoadByNodeId";

        public const string LoadType = "LoadType";

        public const string Mass = "Mass";

        public const string MotionDeviceCategory = "MotionDeviceCategory";

        public const string MotionDeviceCategoryEnumeration = "MotionDeviceCategoryEnumeration";

        public const string MotionDeviceIdentifier_Placeholder = "<MotionDeviceIdentifier>";

        public const string MotionDevices = "MotionDevices";

        public const string MotionDevicesUnderControl = "MotionDevicesUnderControl";

        public const string MotionDeviceSystemType = "MotionDeviceSystemType";

        public const string MotionDeviceType = "MotionDeviceType";

        public const string MotionProfile = "MotionProfile";

        public const string MotorIdentifier_Placeholder = "<MotorIdentifier>";

        public const string MotorTemperature = "MotorTemperature";

        public const string MotorType = "MotorType";

        public const string Moves = "Moves";

        public const string MultiAcknowledgeableConditionType = "MultiAcknowledgeableConditionType";

        public const string Name = "Name";

        public const string OnPath = "OnPath";

        public const string OperationalMode = "OperationalMode";

        public const string OperationalModeEnumeration = "OperationalModeEnumeration";

        public const string OperationStateMachineType = "OperationStateMachineType";

        public const string Pitch = "Pitch";

        public const string PossibleStopModes = "PossibleStopModes";

        public const string PowerTrainIdentifier_Placeholder = "<PowerTrainIdentifier>";

        public const string PowerTrains = "PowerTrains";

        public const string PowerTrainType = "PowerTrainType";

        public const string Programs = "Programs";

        public const string ProgramStartToSuspended = "ProgramStartToSuspended";

        public const string ProtectiveStop = "ProtectiveStop";

        public const string ProtectiveStopFunctionIdentifier_Placeholder = "<ProtectiveStopFunctionIdentifier>";

        public const string ProtectiveStopFunctions = "ProtectiveStopFunctions";

        public const string ProtectiveStopFunctionType = "ProtectiveStopFunctionType";

        public const string Ready = "Ready";

        public const string ReadySubstateMachine = "ReadySubstateMachine";

        public const string ReadySubstateMachineType = "ReadySubstateMachineType";

        public const string ReadyToExecuting = "ReadyToExecuting";

        public const string ReadyToIdle = "ReadyToIdle";

        public const string Requires = "Requires";

        public const string ResetToProgramStart = "ResetToProgramStart";

        public const string Running = "Running";

        public const string RunningToStopping = "RunningToStopping";

        public const string SafetyStateIdentifier_Placeholder = "<SafetyStateIdentifier>";

        public const string SafetyStates = "SafetyStates";

        public const string SafetyStatesIdentifier_Placeholder = "<SafetyStatesIdentifier>";

        public const string SafetyStateType = "SafetyStateType";

        public const string Software = "Software";

        public const string SoftwareIdentifier_Placeholder = "<SoftwareIdentifier>";

        public const string SpeedOverride = "SpeedOverride";

        public const string StandBy = "StandBy";

        public const string StandByToGettingReady = "StandByToGettingReady";

        public const string StandDown = "StandDown";

        public const string Start = "Start";

        public const string StartUpTime = "StartUpTime";

        public const string Stop = "Stop";

        public const string Stopping = "Stopping";

        public const string Suspended = "Suspended";

        public const string SuspendedToProgramStart = "SuspendedToProgramStart";

        public const string SystemOperation = "SystemOperation";

        public const string SystemOperationStateMachine = "SystemOperationStateMachine";

        public const string SystemOperationStateMachineType = "SystemOperationStateMachineType";

        public const string SystemOperationType = "SystemOperationType";

        public const string TaskControlIdentifier_Placeholder = "<TaskControlIdentifier>";

        public const string TaskControlOperation = "TaskControlOperation";

        public const string TaskControlOperationType = "TaskControlOperationType";

        public const string TaskControlReference = "TaskControlReference";

        public const string TaskControls = "TaskControls";

        public const string TaskControlStateMachine = "TaskControlStateMachine";

        public const string TaskControlStateMachineType = "TaskControlStateMachineType";

        public const string TaskControlType = "TaskControlType";

        public const string TaskModule_Placeholder = "<TaskModule>";

        public const string TaskModules = "TaskModules";

        public const string TaskModuleType = "TaskModuleType";

        public const string TaskProgramLoaded = "TaskProgramLoaded";

        public const string TaskProgramName = "TaskProgramName";

        public const string Temperature = "Temperature";

        public const string TotalEnergyConsumption = "TotalEnergyConsumption";

        public const string TotalPowerOnTime = "TotalPowerOnTime";

        public const string UnloadByName = "UnloadByName";

        public const string UnloadByNodeId = "UnloadByNodeId";

        public const string UnloadProgram = "UnloadProgram";

        public const string UpsState = "UpsState";

        public const string UserType = "UserType";

        public const string Version = "Version";
    }
    #endregion

    #region Namespace Declarations
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the Robotics namespace (.NET code namespace is 'Opc.Ua.Robotics').
        /// </summary>
        public const string Robotics = "http://opcfoundation.org/UA/Robotics/";

        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the DI namespace (.NET code namespace is 'Opc.Ua.DI').
        /// </summary>
        public const string DI = "http://opcfoundation.org/UA/DI/";

        /// <summary>
        /// The URI for the IA namespace (.NET code namespace is 'Opc.Ua.IA').
        /// </summary>
        public const string IA = "http://opcfoundation.org/UA/IA/";
    }
    #endregion
}