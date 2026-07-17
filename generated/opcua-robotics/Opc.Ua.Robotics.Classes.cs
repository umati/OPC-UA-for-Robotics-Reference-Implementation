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
using System.Xml;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Opc.Ua;
using Opc.Ua.DI;

namespace Opc.Ua.DI {}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1515 // Consider making public types internal
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1028 // Enum Storage should be Int32

namespace Opc.Ua.Robotics
{
    #region MultiAcknowledgeableConditionTypeState Class
    #if (!OPCUA_EXCLUDE_MultiAcknowledgeableConditionTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class MultiAcknowledgeableConditionTypeState : AcknowledgeableConditionState
    {
        #region Constructors
        public MultiAcknowledgeableConditionTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.MultiAcknowledgeableConditionType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAKQAAAE11bHRpQWNrbm93bGVkZ2VhYmxlQ29uZGl0aW9uVHlwZUluc3RhbmNlAQH3AwEB" +
           "9wP3AwAA/////xgAAAAVYIkIAgAAAAAABwAAAEV2ZW50SWQBAQAAAC4ARAAP/////wEB/////wAAAAAV" +
           "YIkIAgAAAAAACQAAAEV2ZW50VHlwZQEBAAAALgBEABH/////AQH/////AAAAABVgiQgCAAAAAAAKAAAA" +
           "U291cmNlTm9kZQEBAAAALgBEABH/////AQH/////AAAAABVgiQgCAAAAAAAKAAAAU291cmNlTmFtZQEB" +
           "AAAALgBEAAz/////AQH/////AAAAABVgiQgCAAAAAAAEAAAAVGltZQEBAAAALgBEAQAmAf////8BAf//" +
           "//8AAAAAFWCJCAIAAAAAAAsAAABSZWNlaXZlVGltZQEBAAAALgBEAQAmAf////8BAf////8AAAAAFWCJ" +
           "CAIAAAAAAAcAAABNZXNzYWdlAQEAAAAuAEQAFf////8BAf////8AAAAAFWCJCAIAAAAAAAgAAABTZXZl" +
           "cml0eQEBAAAALgBEAAX/////AQH/////AAAAABVgiQgCAAAAAAAQAAAAQ29uZGl0aW9uQ2xhc3NJZAEB" +
           "AAAALgBEABH/////AQH/////AAAAABVgiQgCAAAAAAASAAAAQ29uZGl0aW9uQ2xhc3NOYW1lAQEAAAAu" +
           "AEQAFf////8BAf////8AAAAAFWCJCAIAAAAAAA0AAABDb25kaXRpb25OYW1lAQEAAAAuAEQADP////8B" +
           "Af////8AAAAAFWCJCAIAAAAAAAgAAABCcmFuY2hJZAEBAAAALgBEABH/////AQH/////AAAAABVgiQgC" +
           "AAAAAAAGAAAAUmV0YWluAQEAAAAuAEQAAf////8BAf////8AAAAAFWCJCAIAAAAAAAwAAABFbmFibGVk" +
           "U3RhdGUBAQAAAC8BACMjABX/////AQECAAAAAQAsIwADAQAsAAAATXVsdGlBY2tub3dsZWRnZWFibGVD" +
           "b25kaXRpb25UeXBlX0Fja2VkU3RhdGUBACwjAAMBADAAAABNdWx0aUFja25vd2xlZGdlYWJsZUNvbmRp" +
           "dGlvblR5cGVfQ29uZmlybWVkU3RhdGUBAAAAFWCJCAIAAAAAAAIAAABJZAEBAAAALgBEAAH/////AQH/" +
           "////AAAAABVgiQgCAAAAAAAHAAAAUXVhbGl0eQEBAAAALwEAKiMAE/////8BAf////8BAAAAFWCJCAIA" +
           "AAAAAA8AAABTb3VyY2VUaW1lc3RhbXABAQAAAC4ARAEAJgH/////AQH/////AAAAABVgiQgCAAAAAAAM" +
           "AAAATGFzdFNldmVyaXR5AQEAAAAvAQAqIwAF/////wEB/////wEAAAAVYIkIAgAAAAAADwAAAFNvdXJj" +
           "ZVRpbWVzdGFtcAEBAAAALgBEAQAmAf////8BAf////8AAAAAFWCJCAIAAAAAAAcAAABDb21tZW50AQEA" +
           "AAAvAQAqIwAV/////wEB/////wEAAAAVYIkIAgAAAAAADwAAAFNvdXJjZVRpbWVzdGFtcAEBAAAALgBE" +
           "AQAmAf////8BAf////8AAAAAFWCJCAIAAAAAAAwAAABDbGllbnRVc2VySWQBAQAAAC4ARAAM/////wEB" +
           "/////wAAAAAEYYIIBAAAAAAABwAAAERpc2FibGUBAQAAAC8BAEQjAQEBAAAAAQD5CwABAPMKAAAAAARh" +
           "gggEAAAAAAAGAAAARW5hYmxlAQEAAAAvAQBDIwEBAQAAAAEA+QsAAQDzCgAAAAAEYYIIBAAAAAAACgAA" +
           "AEFkZENvbW1lbnQBAQAAAC8BAEUjAQEBAAAAAQD5CwABAA0LAQAAABdgqQgCAAAAAAAOAAAASW5wdXRB" +
           "cmd1bWVudHMBAQAAAC4ARJYCAAAAAQAqAQFGAAAABwAAAEV2ZW50SWQAD/////8AAAAAAwAAAAAoAAAA" +
           "VGhlIGlkZW50aWZpZXIgZm9yIHRoZSBldmVudCB0byBjb21tZW50LgEAKgEBQgAAAAcAAABDb21tZW50" +
           "ABX/////AAAAAAMAAAAAJAAAAFRoZSBjb21tZW50IHRvIGFkZCB0byB0aGUgY29uZGl0aW9uLgEAKAEB" +
           "AAAAAQAAAAIAAAABAf////8AAAAAFWCJCAIAAAAAAAoAAABBY2tlZFN0YXRlAwEALAAAAE11bHRpQWNr" +
           "bm93bGVkZ2VhYmxlQ29uZGl0aW9uVHlwZV9BY2tlZFN0YXRlAC8BACMjABX/////AQEBAAAAAQAsIwED" +
           "AQAuAAAATXVsdGlBY2tub3dsZWRnZWFibGVDb25kaXRpb25UeXBlX0VuYWJsZWRTdGF0ZQEAAAAVYIkI" +
           "AgAAAAAAAgAAAElkAQEAAAAuAEQAAf////8BAf////8AAAAABGGCCAQAAAAAAAsAAABBY2tub3dsZWRn" +
           "ZQEBAAAALwEAlyMBAQEAAAABAPkLAAEA8CIBAAAAF2CpCAIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEB" +
           "AAAALgBElgIAAAABACoBAUYAAAAHAAAARXZlbnRJZAAP/////wAAAAADAAAAACgAAABUaGUgaWRlbnRp" +
           "ZmllciBmb3IgdGhlIGV2ZW50IHRvIGNvbW1lbnQuAQAqAQFCAAAABwAAAENvbW1lbnQAFf////8AAAAA" +
           "AwAAAAAkAAAAVGhlIGNvbW1lbnQgdG8gYWRkIHRvIHRoZSBjb25kaXRpb24uAQAoAQEAAAABAAAAAgAA" +
           "AAEB/////wAAAAAXYIkKAgAAAAEAFQAAAENvbmRpdGlvbkRlc2NyaXB0aW9ucwEB/BcALgBE/BcAAAAV" +
           "AQAAAAEAAAAAAAAAAwP/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<LocalizedText[]> ConditionDescriptions
        {
            get => m_conditionDescriptions;

            set
            {
                if (!Object.ReferenceEquals(m_conditionDescriptions, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_conditionDescriptions = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_conditionDescriptions != null)
            {
                children.Add(m_conditionDescriptions);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_conditionDescriptions, child))
            {
                m_conditionDescriptions = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.ConditionDescriptions:
                {
                    if (createOrReplace)
                    {
                        if (ConditionDescriptions == null)
                        {
                            if (replacement == null)
                            {
                                ConditionDescriptions = new PropertyState<LocalizedText[]>(this);
                            }
                            else
                            {
                                ConditionDescriptions = (PropertyState<LocalizedText[]>)replacement;
                            }
                        }
                    }

                    instance = ConditionDescriptions;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<LocalizedText[]> m_conditionDescriptions;
        #endregion
    }
    #endif
    #endregion

    #region EmergencyStopFunctionTypeState Class
    #if (!OPCUA_EXCLUDE_EmergencyStopFunctionTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class EmergencyStopFunctionTypeState : BaseObjectState
    {
        #region Constructors
        public EmergencyStopFunctionTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.EmergencyStopFunctionType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAIQAAAEVtZXJnZW5jeVN0b3BGdW5jdGlvblR5cGVJbnN0YW5jZQEBTkMBAU5DTkMAAP//" +
           "//8CAAAANWCJCgIAAAABAAYAAABBY3RpdmUBAVBDAwAAAACSAAAAVGhlIEFjdGl2ZSB2YXJpYWJsZSBp" +
           "cyBUUlVFIGlmIHRoaXMgcGFydGljdWxhciBlbWVyZ2VuY3kgc3RvcCBmdW5jdGlvbiBpcyBhY3RpdmUs" +
           "IGUuZy4gdGhhdCB0aGUgZW1lcmdlbmN5IHN0b3AgYnV0dG9uIGlzIHByZXNzZWQsIEZBTFNFIG90aGVy" +
           "d2lzZS4ALwA/UEMAAAAB/////wEB/////wAAAAA1YIkKAgAAAAEABAAAAE5hbWUBAU9DAwAAAACHAAAA" +
           "VGhlIE5hbWUgb2YgdGhlIEVtZXJnZW5jeVN0b3BGdW5jdGlvblR5cGUgcHJvdmlkZXMgYSBtYW51ZmFj" +
           "dHVyZXItc3BlY2lmaWMgZW1lcmdlbmN5IHN0b3AgZnVuY3Rpb24gaWRlbnRpZmllciB3aXRoaW4gdGhl" +
           "IHNhZmV0eSBzeXN0ZW0uAC4ARE9DAAAADP////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public BaseDataVariableState<bool> Active
        {
            get => m_active;

            set
            {
                if (!Object.ReferenceEquals(m_active, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_active = value;
            }
        }

        public PropertyState<string> Name
        {
            get => m_name;

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_active != null)
            {
                children.Add(m_active);
            }

            if (m_name != null)
            {
                children.Add(m_name);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_active, child))
            {
                m_active = null;
                return;
            }

            if (Object.ReferenceEquals(m_name, child))
            {
                m_name = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.Active:
                {
                    if (createOrReplace)
                    {
                        if (Active == null)
                        {
                            if (replacement == null)
                            {
                                Active = new BaseDataVariableState<bool>(this);
                            }
                            else
                            {
                                Active = (BaseDataVariableState<bool>)replacement;
                            }
                        }
                    }

                    instance = Active;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new PropertyState<string>(this);
                            }
                            else
                            {
                                Name = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<bool> m_active;
        private PropertyState<string> m_name;
        #endregion
    }
    #endif
    #endregion

    #region LoadTypeState Class
    #if (!OPCUA_EXCLUDE_LoadTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class LoadTypeState : BaseObjectState
    {
        #region Constructors
        public LoadTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.LoadType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (CenterOfMass != null)
            {
                CenterOfMass.Initialize(context, CenterOfMass_InitializationString);
            }

            if (Inertia != null)
            {
                Inertia.Initialize(context, Inertia_InitializationString);
            }
        }

        #region Initialization String
        private const string CenterOfMass_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////81" +
           "YIkKAgAAAAEADAAAAENlbnRlck9mTWFzcwEBfRcDAAAAAIwBAABUaGUgcG9zaXRpb24gYW5kIG9yaWVu" +
           "dGF0aW9uIG9mIHRoZSBjZW50ZXIgb2YgdGhlIG1hc3MgcmVsYXRlZCB0byB0aGUgbW91bnRpbmcgcG9p" +
           "bnQgdXNpbmcgYSBGcmFtZVR5cGUuIFgsIFksIFogZGVmaW5lIHRoZSBwb3NpdGlvbiBvZiB0aGUgY2Vu" +
           "dGVyIG9mIGdyYXZpdHkgcmVsYXRpdmUgdG8gdGhlIG1vdW50aW5nIHBvaW50IGNvb3JkaW5hdGUgc3lz" +
           "dGVtLiBBLCBCLCBDIGRlZmluZSB0aGUgb3JpZW50YXRpb24gb2YgdGhlIHByaW5jaXBhbCBheGVzIG9m" +
           "IGluZXJ0aWEgcmVsYXRpdmUgdG8gdGhlIG1vdW50aW5nIHBvaW50IGNvb3JkaW5hdGUgc3lzdGVtLiBP" +
           "cmllbnRhdGlvbiBBLCBCLCBDIGNhbiBiZSAiMCIgZm9yIHN5c3RlbXMgd2hpY2ggZG8gbm90IG5lZWQg" +
           "dGhlc2UgIHZhbHVlcy4ALwEAZ0l9FwAAAQB+Sf////8BAf////8CAAAAFWCJCgIAAAAAABQAAABDYXJ0" +
           "ZXNpYW5Db29yZGluYXRlcwEBAj8ALwEAVkkCPwAAAQB6Sf////8BAf////8DAAAAFWCJCgIAAAAAAAEA" +
           "AABYAQEGPwAvAD8GPwAAAAv/////AQH/////AAAAABVgiQoCAAAAAAABAAAAWQEBBz8ALwA/Bz8AAAAL" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAAAQAAAFoBAQg/AC8APwg/AAAAC/////8BAf////8AAAAAFWCJ" +
           "CgIAAAAAAAsAAABPcmllbnRhdGlvbgEBBD8ALwEAXUkEPwAAAQB8Sf////8BAf////8DAAAAFWCJCgIA" +
           "AAAAAAEAAABBAQEJPwAvAD8JPwAAAAv/////AQH/////AAAAABVgiQoCAAAAAAABAAAAQgEBCj8ALwA/" +
           "Cj8AAAAL/////wEB/////wAAAAAVYIkKAgAAAAAAAQAAAEMBAQs/AC8APws/AAAAC/////8BAf////8A" +
           "AAAA";

        private const string Inertia_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////81" +
           "YIkKAgAAAAEABwAAAEluZXJ0aWEBAfpGAwAAAAD6AAAAVGhlIEluZXJ0aWEgdXNlcyB0aGUgVmVjdG9y" +
           "VHlwZSB0byBkZXNjcmliZSB0aGUgdGhyZWUgdmFsdWVzIG9mIHRoZSBwcmluY2lwYWwgbW9tZW50cyBv" +
           "ZiBpbmVydGlhIHdpdGggcmVzcGVjdCB0byB0aGUgbW91bnRpbmcgcG9pbnQgY29vcmRpbmF0ZSBzeXN0" +
           "ZW0uIElmIGluZXJ0aWEgdmFsdWVzIGFyZSBwcm92aWRlZCBmb3Igcm90YXJ5IGF4aXMgdGhlIENlbnRl" +
           "ck9mTWFzcyBzaGFsbCBiZSBjb21wbGV0ZWx5IGZpbGxlZCBhcyB3ZWxsLgAvAQA0RfpGAAABAHhJ////" +
           "/wEB/////wMAAAAVYIkKAgAAAAAAAQAAAFgBAftGAC8AP/tGAAAAC/////8BAf////8AAAAAFWCJCgIA" +
           "AAAAAAEAAABZAQH8RgAvAD/8RgAAAAv/////AQH/////AAAAABVgiQoCAAAAAAABAAAAWgEB/UYALwA/" +
           "/UYAAAAL/////wEB/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAEAAAAExvYWRUeXBlSW5zdGFuY2UBAfoDAQH6A/oDAAD/////AwAAADVgiQoCAAAAAQAM" +
           "AAAAQ2VudGVyT2ZNYXNzAQF9FwMAAAAAjAEAAFRoZSBwb3NpdGlvbiBhbmQgb3JpZW50YXRpb24gb2Yg" +
           "dGhlIGNlbnRlciBvZiB0aGUgbWFzcyByZWxhdGVkIHRvIHRoZSBtb3VudGluZyBwb2ludCB1c2luZyBh" +
           "IEZyYW1lVHlwZS4gWCwgWSwgWiBkZWZpbmUgdGhlIHBvc2l0aW9uIG9mIHRoZSBjZW50ZXIgb2YgZ3Jh" +
           "dml0eSByZWxhdGl2ZSB0byB0aGUgbW91bnRpbmcgcG9pbnQgY29vcmRpbmF0ZSBzeXN0ZW0uIEEsIEIs" +
           "IEMgZGVmaW5lIHRoZSBvcmllbnRhdGlvbiBvZiB0aGUgcHJpbmNpcGFsIGF4ZXMgb2YgaW5lcnRpYSBy" +
           "ZWxhdGl2ZSB0byB0aGUgbW91bnRpbmcgcG9pbnQgY29vcmRpbmF0ZSBzeXN0ZW0uIE9yaWVudGF0aW9u" +
           "IEEsIEIsIEMgY2FuIGJlICIwIiBmb3Igc3lzdGVtcyB3aGljaCBkbyBub3QgbmVlZCB0aGVzZSAgdmFs" +
           "dWVzLgAvAQBnSX0XAAABAH5J/////wEB/////wIAAAAVYIkKAgAAAAAAFAAAAENhcnRlc2lhbkNvb3Jk" +
           "aW5hdGVzAQECPwAvAQBWSQI/AAABAHpJ/////wEB/////wMAAAAVYIkKAgAAAAAAAQAAAFgBAQY/AC8A" +
           "PwY/AAAAC/////8BAf////8AAAAAFWCJCgIAAAAAAAEAAABZAQEHPwAvAD8HPwAAAAv/////AQH/////" +
           "AAAAABVgiQoCAAAAAAABAAAAWgEBCD8ALwA/CD8AAAAL/////wEB/////wAAAAAVYIkKAgAAAAAACwAA" +
           "AE9yaWVudGF0aW9uAQEEPwAvAQBdSQQ/AAABAHxJ/////wEB/////wMAAAAVYIkKAgAAAAAAAQAAAEEB" +
           "AQk/AC8APwk/AAAAC/////8BAf////8AAAAAFWCJCgIAAAAAAAEAAABCAQEKPwAvAD8KPwAAAAv/////" +
           "AQH/////AAAAABVgiQoCAAAAAAABAAAAQwEBCz8ALwA/Cz8AAAAL/////wEB/////wAAAAA1YIkKAgAA" +
           "AAEABwAAAEluZXJ0aWEBAfpGAwAAAAD6AAAAVGhlIEluZXJ0aWEgdXNlcyB0aGUgVmVjdG9yVHlwZSB0" +
           "byBkZXNjcmliZSB0aGUgdGhyZWUgdmFsdWVzIG9mIHRoZSBwcmluY2lwYWwgbW9tZW50cyBvZiBpbmVy" +
           "dGlhIHdpdGggcmVzcGVjdCB0byB0aGUgbW91bnRpbmcgcG9pbnQgY29vcmRpbmF0ZSBzeXN0ZW0uIElm" +
           "IGluZXJ0aWEgdmFsdWVzIGFyZSBwcm92aWRlZCBmb3Igcm90YXJ5IGF4aXMgdGhlIENlbnRlck9mTWFz" +
           "cyBzaGFsbCBiZSBjb21wbGV0ZWx5IGZpbGxlZCBhcyB3ZWxsLgAvAQA0RfpGAAABAHhJ/////wEB////" +
           "/wMAAAAVYIkKAgAAAAAAAQAAAFgBAftGAC8AP/tGAAAAC/////8BAf////8AAAAAFWCJCgIAAAAAAAEA" +
           "AABZAQH8RgAvAD/8RgAAAAv/////AQH/////AAAAABVgiQoCAAAAAAABAAAAWgEB/UYALwA//UYAAAAL" +
           "/////wEB/////wAAAAA1YIkKAgAAAAEABAAAAE1hc3MBAUMaAwAAAAA1AAAAVGhlIHdlaWdodCBvZiB0" +
           "aGUgbG9hZCBtb3VudGVkIG9uIG9uZSBtb3VudGluZyBwb2ludC4ALwEAWURDGgAAAAv/////AQH/////" +
           "AQAAABVgiQoCAAAAAAAQAAAARW5naW5lZXJpbmdVbml0cwEBSBoALgBESBoAAAEAdwP/////AQH/////" +
           "AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public ThreeDFrameState CenterOfMass
        {
            get => m_centerOfMass;

            set
            {
                if (!Object.ReferenceEquals(m_centerOfMass, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_centerOfMass = value;
            }
        }

        public ThreeDVectorState Inertia
        {
            get => m_inertia;

            set
            {
                if (!Object.ReferenceEquals(m_inertia, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_inertia = value;
            }
        }

        public AnalogUnitState<double> Mass
        {
            get => m_mass;

            set
            {
                if (!Object.ReferenceEquals(m_mass, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_mass = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_centerOfMass != null)
            {
                children.Add(m_centerOfMass);
            }

            if (m_inertia != null)
            {
                children.Add(m_inertia);
            }

            if (m_mass != null)
            {
                children.Add(m_mass);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_centerOfMass, child))
            {
                m_centerOfMass = null;
                return;
            }

            if (Object.ReferenceEquals(m_inertia, child))
            {
                m_inertia = null;
                return;
            }

            if (Object.ReferenceEquals(m_mass, child))
            {
                m_mass = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.CenterOfMass:
                {
                    if (createOrReplace)
                    {
                        if (CenterOfMass == null)
                        {
                            if (replacement == null)
                            {
                                CenterOfMass = new ThreeDFrameState(this);
                            }
                            else
                            {
                                CenterOfMass = (ThreeDFrameState)replacement;
                            }
                        }
                    }

                    instance = CenterOfMass;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Inertia:
                {
                    if (createOrReplace)
                    {
                        if (Inertia == null)
                        {
                            if (replacement == null)
                            {
                                Inertia = new ThreeDVectorState(this);
                            }
                            else
                            {
                                Inertia = (ThreeDVectorState)replacement;
                            }
                        }
                    }

                    instance = Inertia;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Mass:
                {
                    if (createOrReplace)
                    {
                        if (Mass == null)
                        {
                            if (replacement == null)
                            {
                                Mass = new AnalogUnitState<double>(this);
                            }
                            else
                            {
                                Mass = (AnalogUnitState<double>)replacement;
                            }
                        }
                    }

                    instance = Mass;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private ThreeDFrameState m_centerOfMass;
        private ThreeDVectorState m_inertia;
        private AnalogUnitState<double> m_mass;
        #endregion
    }
    #endif
    #endregion

    #region ProtectiveStopFunctionTypeState Class
    #if (!OPCUA_EXCLUDE_ProtectiveStopFunctionTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class ProtectiveStopFunctionTypeState : BaseObjectState
    {
        #region Constructors
        public ProtectiveStopFunctionTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.ProtectiveStopFunctionType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAIgAAAFByb3RlY3RpdmVTdG9wRnVuY3Rpb25UeXBlSW5zdGFuY2UBAVFDAQFRQ1FDAAD/" +
           "////AwAAADVgiQoCAAAAAQAGAAAAQWN0aXZlAQFUQwMAAAAAtgAAAOKAkwlUaGUgQWN0aXZlIHZhcmlh" +
           "YmxlIGlzIFRSVUUgaWYgdGhpcyBwYXJ0aWN1bGFyIHByb3RlY3RpdmUgc3RvcCBmdW5jdGlvbiBpcyBh" +
           "Y3RpdmUsIGkuZS4gdGhhdCBhIHN0b3AgaXMgaW5pdGlhdGVkLCBGQUxTRSBvdGhlcndpc2UuIElmIEVu" +
           "YWJsZWQgaXMgRkFMU0UgdGhlbiBBY3RpdmUgc2hhbGwgYmUgRkFMU0UuAC8AP1RDAAAAAf////8BAf//" +
           "//8AAAAANWCJCgIAAAABAAcAAABFbmFibGVkAQFTQwMAAAAA3gEAAOKAkwlUaGUgRW5hYmxlZCB2YXJp" +
           "YWJsZSBpcyBUUlVFIGlmIHRoaXMgcHJvdGVjdGl2ZSBzdG9wIGZ1bmN0aW9uIGlzIGN1cnJlbnRseSBz" +
           "dXBlcnZpc2luZyB0aGUgc3lzdGVtLCBGQUxTRSBvdGhlcndpc2UuIEEgcHJvdGVjdGl2ZSBzdG9wIGZ1" +
           "bmN0aW9uIG1heSBvciBtYXkgbm90IGJlIGVuYWJsZWQgYXQgYWxsIHRpbWVzLCBlLmcuIHRoZSBwcm90" +
           "ZWN0aXZlIHN0b3AgZnVuY3Rpb24gb2YgdGhlIHNhZmV0eSBkb29ycyBhcmUgdHlwaWNhbGx5IGVuYWJs" +
           "ZWQgaW4gYXV0b21hdGljIG9wZXJhdGlvbmFsIG1vZGUgYW5kIGRpc2FibGVkIGluIG1hbnVhbCBtb2Rl" +
           "LiBPbiB0aGUgb3RoZXIgaGFuZCBmb3IgZXhhbXBsZSwgdGhlIHByb3RlY3RpdmUgc3RvcCBmdW5jdGlv" +
           "biBvZiB0aGUgdGVhY2ggcGVuZGFudCBlbmFibGluZyBkZXZpY2UgaXMgZW5hYmxlZCBpbiBtYW51YWwg" +
           "bW9kZXMgYW5kIGRpc2FibGVkIGluIGF1dG9tYXRpYyBtb2Rlcy4ALwA/U0MAAAAB/////wEB/////wAA" +
           "AAA1YIkKAgAAAAEABAAAAE5hbWUBAVJDAwAAAACJAAAAVGhlIE5hbWUgb2YgdGhlIFByb3RlY3RpdmVT" +
           "dG9wRnVuY3Rpb25UeXBlIHByb3ZpZGVzIGEgbWFudWZhY3R1cmVyLXNwZWNpZmljIHByb3RlY3RpdmUg" +
           "c3RvcCBmdW5jdGlvbiBpZGVudGlmaWVyIHdpdGhpbiB0aGUgc2FmZXR5IHN5c3RlbS4ALgBEUkMAAAAM" +
           "/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public BaseDataVariableState<bool> Active
        {
            get => m_active;

            set
            {
                if (!Object.ReferenceEquals(m_active, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_active = value;
            }
        }

        public BaseDataVariableState<bool> Enabled
        {
            get => m_enabled;

            set
            {
                if (!Object.ReferenceEquals(m_enabled, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_enabled = value;
            }
        }

        public PropertyState<string> Name
        {
            get => m_name;

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_active != null)
            {
                children.Add(m_active);
            }

            if (m_enabled != null)
            {
                children.Add(m_enabled);
            }

            if (m_name != null)
            {
                children.Add(m_name);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_active, child))
            {
                m_active = null;
                return;
            }

            if (Object.ReferenceEquals(m_enabled, child))
            {
                m_enabled = null;
                return;
            }

            if (Object.ReferenceEquals(m_name, child))
            {
                m_name = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.Active:
                {
                    if (createOrReplace)
                    {
                        if (Active == null)
                        {
                            if (replacement == null)
                            {
                                Active = new BaseDataVariableState<bool>(this);
                            }
                            else
                            {
                                Active = (BaseDataVariableState<bool>)replacement;
                            }
                        }
                    }

                    instance = Active;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Enabled:
                {
                    if (createOrReplace)
                    {
                        if (Enabled == null)
                        {
                            if (replacement == null)
                            {
                                Enabled = new BaseDataVariableState<bool>(this);
                            }
                            else
                            {
                                Enabled = (BaseDataVariableState<bool>)replacement;
                            }
                        }
                    }

                    instance = Enabled;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new PropertyState<string>(this);
                            }
                            else
                            {
                                Name = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<bool> m_active;
        private BaseDataVariableState<bool> m_enabled;
        private PropertyState<string> m_name;
        #endregion
    }
    #endif
    #endregion

    #region ExecutingSubstateMachineTypeState Class
    #if (!OPCUA_EXCLUDE_ExecutingSubstateMachineTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class ExecutingSubstateMachineTypeState : FiniteStateMachineState
    {
        #region Constructors
        public ExecutingSubstateMachineTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.ExecutingSubstateMachineType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAJAAAAEV4ZWN1dGluZ1N1YnN0YXRlTWFjaGluZVR5cGVJbnN0YW5jZQEB7wMBAe8D7wMA" +
           "AAEAAAAAKQABAAcJBgAAABVgiQgCAAAAAAAMAAAAQ3VycmVudFN0YXRlAQEAAAAvAQDICgAV/////wEB" +
           "/////wEAAAAVYIkIAgAAAAAAAgAAAElkAQEAAAAuAEQAEf////8BAf////8AAAAAFWCJCgIAAAAAAA4A" +
           "AABMYXN0VHJhbnNpdGlvbgEBlBcALwEAzwqUFwAAABX/////AQH/////AQAAABVgiQoCAAAAAAACAAAA" +
           "SWQBAZUXAC4ARJUXAAAAEf////8BAf////8AAAAAFWCJCgIAAAABABQAAABMYXN0VHJhbnNpdGlvblJl" +
           "YXNvbgEBlhcALwEA5iuWFwAAAAT/////AwP/////AgAAABdgqQoCAAAAAAAKAAAARW51bVZhbHVlcwEB" +
           "lxcALgBElxcAAJYGAAAAAQA7IAFAAAAAAAAAAAAAAAADAgAAAGVuBwAAAFVua25vd24DAgAAAGVuGwAA" +
           "AENhdXNlZCBieSBhbiB1bmtub3duIHJlYXNvbgEAOyABQgAAAAEAAAAAAAAAAwIAAABlbggAAABFeHRl" +
           "cm5hbAMCAAAAZW4cAAAAQ2F1c2VkIGJ5IGV4dGVybmFsIG9wZXJhdGlvbgEAOyABPgAAAAIAAAAAAAAA" +
           "AwIAAABlbgYAAABEaXJlY3QDAgAAAGVuGgAAAENhdXNlZCBieSBkaXJlY3Qgb3BlcmF0aW9uAQA7IAFG" +
           "AAAAAwAAAAAAAAADAgAAAGVuBgAAAFN5c3RlbQMCAAAAZW4iAAAAQ2F1c2VkIGJ5IHN5c3RlbSBzcGVj" +
           "aWZpYyBiZWhhdmlvcgEAOyABNQAAAAQAAAAAAAAAAwIAAABlbgUAAABFcnJvcgMCAAAAZW4SAAAAQ2F1" +
           "c2VkIGJ5IGFuIGVycm9yAQA7IAFUAAAABQAAAAAAAAADAgAAAGVuCwAAAEFwcGxpY2F0aW9uAwIAAABl" +
           "bisAAABDYXVzZWQgZXhwbGljaXRseSBieSBlbmQgdXNlciBwcm9ncmFtIGxvZ2ljAQCqHQEAAAABAAAA" +
           "AAAAAAEB/////wAAAAAVYKkKAgAAAAAACwAAAFZhbHVlQXNUZXh0AQGYFwAuAESYFwAAFQIHAAAASW52" +
           "YWxpZAAV/////wEB/////wAAAAAEYIAKAQAAAAEABwAAAFJ1bm5pbmcBAZwTAC8BAAUJnBMAAAEAAAAA" +
           "MwEBAZ4TAQAAABVgqQoCAAAAAAALAAAAU3RhdGVOdW1iZXIBAZkXAC4ARJkXAAAHAQAAAAAH/////wEB" +
           "/////wAAAAAEYIAKAQAAAAEAEQAAAFJ1bm5pbmdUb1N0b3BwaW5nAQGeEwAvAQAGCZ4TAAADAAAAADMA" +
           "AQGcEwA0AAEBnRMANgABAAcJAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51bWJlcgEBmxcALgBE" +
           "mxcAAAcBAAAAAAf/////AQH/////AAAAAARggAoBAAAAAQAIAAAAU3RvcHBpbmcBAZ0TAC8BAAMJnRMA" +
           "AAEAAAAANAEBAZ4TAQAAABVgqQoCAAAAAAALAAAAU3RhdGVOdW1iZXIBAZoXAC4ARJoXAAAHAgAAAAAH" +
           "/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public MultiStateValueDiscreteState<short> LastTransitionReason
        {
            get => m_lastTransitionReason;

            set
            {
                if (!Object.ReferenceEquals(m_lastTransitionReason, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_lastTransitionReason = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_lastTransitionReason != null)
            {
                children.Add(m_lastTransitionReason);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_lastTransitionReason, child))
            {
                m_lastTransitionReason = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.LastTransitionReason:
                {
                    if (createOrReplace)
                    {
                        if (LastTransitionReason == null)
                        {
                            if (replacement == null)
                            {
                                LastTransitionReason = new MultiStateValueDiscreteState<short>(this);
                            }
                            else
                            {
                                LastTransitionReason = (MultiStateValueDiscreteState<short>)replacement;
                            }
                        }
                    }

                    instance = LastTransitionReason;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private MultiStateValueDiscreteState<short> m_lastTransitionReason;
        #endregion
    }
    #endif
    #endregion

    #region IdleSubstateMachineTypeState Class
    #if (!OPCUA_EXCLUDE_IdleSubstateMachineTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class IdleSubstateMachineTypeState : FiniteStateMachineState
    {
        #region Constructors
        public IdleSubstateMachineTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.IdleSubstateMachineType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAHwAAAElkbGVTdWJzdGF0ZU1hY2hpbmVUeXBlSW5zdGFuY2UBAfEDAQHxA/EDAAABAAAA" +
           "ACkAAQAHCQcAAAAVYIkIAgAAAAAADAAAAEN1cnJlbnRTdGF0ZQEBAAAALwEAyAoAFf////8BAf////8B" +
           "AAAAFWCJCAIAAAAAAAIAAABJZAEBAAAALgBEABH/////AQH/////AAAAABVgiQoCAAAAAAAOAAAATGFz" +
           "dFRyYW5zaXRpb24BAYoXAC8BAM8KihcAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQGM" +
           "FwAuAESMFwAAABH/////AQH/////AAAAAARggAoBAAAAAQAMAAAAR2V0dGluZ1JlYWR5AQGZEwAvAQAD" +
           "CZkTAAACAAAAADMBAQGaEwA0AQEBmxMBAAAAFWCpCgIAAAAAAAsAAABTdGF0ZU51bWJlcgEBjxcALgBE" +
           "jxcAAAcCAAAAAAf/////AQH/////AAAAAARggAoBAAAAAQAVAAAAR2V0dGluZ1JlYWR5VG9TdGFuZEJ5" +
           "AQGaEwAvAQAGCZoTAAADAAAAADMAAQGZEwA0AAEBlxMANgABAAcJAQAAABVgqQoCAAAAAAAQAAAAVHJh" +
           "bnNpdGlvbk51bWJlcgEBkBcALgBEkBcAAAcCAAAAAAf/////AQH/////AAAAABVgiQoCAAAAAQAUAAAA" +
           "TGFzdFRyYW5zaXRpb25SZWFzb24BAY0XAC8BAOYrjRcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAA" +
           "CgAAAEVudW1WYWx1ZXMBAZIXAC4ARJIXAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABV" +
           "bmtub3duAwIAAABlbhsAAABDYXVzZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAA" +
           "AAMCAAAAZW4IAAAARXh0ZXJuYWwDAgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24B" +
           "ADsgAT4AAAACAAAAAAAAAAMCAAAAZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0" +
           "IG9wZXJhdGlvbgEAOyABRgAAAAMAAAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNl" +
           "ZCBieSBzeXN0ZW0gc3BlY2lmaWMgYmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJy" +
           "b3IDAgAAAGVuEgAAAENhdXNlZCBieSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABB" +
           "cHBsaWNhdGlvbgMCAAAAZW4rAAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBs" +
           "b2dpYwEAqh0BAAAAAQAAAAAAAAABAf////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEBkxcA" +
           "LgBEkxcAABUCBwAAAEludmFsaWQAFf////8BAf////8AAAAABGCACgEAAAABAAcAAABTdGFuZEJ5AQGX" +
           "EwAvAQAFCZcTAAACAAAAADQBAQGaEwAzAQEBmxMBAAAAFWCpCgIAAAAAAAsAAABTdGF0ZU51bWJlcgEB" +
           "jhcALgBEjhcAAAcBAAAAAAf/////AQH/////AAAAAARggAoBAAAAAQAVAAAAU3RhbmRCeVRvR2V0dGlu" +
           "Z1JlYWR5AQGbEwAvAQAGCZsTAAADAAAAADQAAQGZEwAzAAEBlxMANgABAAcJAQAAABVgqQoCAAAAAAAQ" +
           "AAAAVHJhbnNpdGlvbk51bWJlcgEBkRcALgBEkRcAAAcBAAAAAAf/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public MultiStateValueDiscreteState<short> LastTransitionReason
        {
            get => m_lastTransitionReason;

            set
            {
                if (!Object.ReferenceEquals(m_lastTransitionReason, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_lastTransitionReason = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_lastTransitionReason != null)
            {
                children.Add(m_lastTransitionReason);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_lastTransitionReason, child))
            {
                m_lastTransitionReason = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.LastTransitionReason:
                {
                    if (createOrReplace)
                    {
                        if (LastTransitionReason == null)
                        {
                            if (replacement == null)
                            {
                                LastTransitionReason = new MultiStateValueDiscreteState<short>(this);
                            }
                            else
                            {
                                LastTransitionReason = (MultiStateValueDiscreteState<short>)replacement;
                            }
                        }
                    }

                    instance = LastTransitionReason;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private MultiStateValueDiscreteState<short> m_lastTransitionReason;
        #endregion
    }
    #endif
    #endregion

    #region OperationStateMachineTypeState Class
    #if (!OPCUA_EXCLUDE_OperationStateMachineTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class OperationStateMachineTypeState : FiniteStateMachineState
    {
        #region Constructors
        public OperationStateMachineTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.OperationStateMachineType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ConfiguredDefaultStopMode != null)
            {
                ConfiguredDefaultStopMode.Initialize(context, ConfiguredDefaultStopMode_InitializationString);
            }

            if (PossibleStopModes != null)
            {
                PossibleStopModes.Initialize(context, PossibleStopModes_InitializationString);
            }

            if (Start != null)
            {
                Start.Initialize(context, Start_InitializationString);
            }

            if (Stop != null)
            {
                Stop.Initialize(context, Stop_InitializationString);
            }
        }

        #region Initialization String
        private const string ConfiguredDefaultStopMode_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAEAGQAAAENvbmZpZ3VyZWREZWZhdWx0U3RvcE1vZGUBAXkXAC8AP3kXAAAABP////8DA///" +
           "//8AAAAA";

        private const string PossibleStopModes_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8X" +
           "YKkKAgAAAAEAEQAAAFBvc3NpYmxlU3RvcE1vZGVzAQF4FwAvAD94FwAAlgUAAAABADsgAWsAAAABAAAA" +
           "AAAAAAMCAAAAZW4GAAAAT25QYXRoAwIAAABlbkcAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIGluIGEg" +
           "Y29udHJvbGxlZCBtYW5uZXIgYWxvbmcgdGhlIHByb2dyYW1tZWQgcGF0aAEAOyABcgAAAAIAAAAAAAAA" +
           "AwIAAABlbgoAAABFbmRPZkN5Y2xlAwIAAABlbkoAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIHdoZW4g" +
           "dGhlIGN1cnJlbnQgcHJvZHVjdGlvbiBjeWNsZSBoYXMgYmVlbiBmaW5pc2hlZAEAOyAByQAAAAMAAAAA" +
           "AAAAAwIAAABlbgsAAABQcm9jZXNzU3RvcAMCAAAAZW6gAAAAQXBwbGljYXRpb24gZGVwZW5kZW50IHN0" +
           "b3AgaW5zdHJ1Y3Rpb24gdGhhdCBzdG9wcyBwcm9ncmFtIGV4ZWN1dGlvbiBhdCBhIGZhdm91cmFibGUg" +
           "cG9pbnQgZm9yIHRoZSBhcHBsaWNhdGlvbiwgZS5nLiBhdCB0aGUgZW5kIG9mIGEgcGFpbnQgc3Ryb2tl" +
           "IG9yIHNlYWxpbmcgYmVhZAEAOyABrAAAAAQAAAAAAAAAAwIAAABlbgkAAABRdWlja1N0b3ADAgAAAGVu" +
           "hQAAAFRoaXMgc3RvcCBpcyBwZXJmb3JtZWQgYnkgcmFtcGluZyBkb3duIG1vdGlvbiBhcyBmYXN0IGFz" +
           "IHBvc3NpYmxlIHVzaW5nIG9wdGltdW0gbW90b3IgcGVyZm9ybWFuY2UuIFRoZSByb2JvdCBtYXkgbm90" +
           "IHN0YXkgb24gdGhlIHBhdGgBADsgAYsAAAAFAAAAAAAAAAMCAAAAZW4QAAAARW5kT2ZJbnN0cnVjdGlv" +
           "bgMCAAAAZW5dAAAAVGhpcyBzdG9wIGNhbiBiZSB1c2VkIHRvIHN0b3AgdGhlIHByb2dyYW0gZXhlY3V0" +
           "aW9uIHdoZW4gdGhlIGN1cnJlbnQgaW5zdHJ1Y3Rpb24gaXMgY29tcGxldGVkAQCqHQEAAAABAAAAAAAA" +
           "AAMD/////wAAAAA=";

        private const string Start_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEABQAAAFN0YXJ0AQFZGwAvAQFZG1kbAAABAQEAAAAANQEBAZQTAQAAABdgqQoCAAAAAAAP" +
           "AAAAT3V0cHV0QXJndW1lbnRzAQGHFwAuAESHFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////" +
           "AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVk" +
           "IGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3Ig" +
           "YXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";

        private const string Stop_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEABAAAAFN0b3ABAVobAC8BAVobWhsAAAEBAQAAAAA1AQEBkxMCAAAAF2CpCgIAAAAAAA4A" +
           "AABJbnB1dEFyZ3VtZW50cwEBiBcALgBEiBcAAJYBAAAAAQAqAQGtAAAACAAAAFN0b3BNb2RlAAj/////" +
           "AAAAAAKSAAAAcHJvdmlkZXMgYSB3YXkgdG8gZGlmZmVyZW50aWF0ZSBiZXR3ZWVuIGRpZmZlcmVudCBz" +
           "dG9wIG1vZGVzLiBUaGlzIHBhcmFtZXRlciBzaG91bGQgY29ycmVzcG9uZCB0byBvbmUgb2YgdGhlIHZh" +
           "bHVlcyBpbiB0aGUgUG9zc2libGVTdG9wTW9kZXMgYXJyYXkBACgBAQAAAAEAAAABAAAAAQH/////AAAA" +
           "ABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQGJFwAuAESJFwAAlgEAAAABACoBAaMAAAAGAAAA" +
           "U3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVy" +
           "cm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwg" +
           "YmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB////" +
           "/wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAIQAAAE9wZXJhdGlvblN0YXRlTWFjaGluZVR5cGVJbnN0YW5jZQEB7gMBAe4D7gMAAAEA" +
           "AAAAKQABAAcJEAAAABVgiQgCAAAAAAAMAAAAQ3VycmVudFN0YXRlAQEAAAAvAQDICgAV/////wEB////" +
           "/wEAAAAVYIkIAgAAAAAAAgAAAElkAQEAAAAuAEQAEf////8BAf////8AAAAAFWCJCgIAAAAAAA4AAABM" +
           "YXN0VHJhbnNpdGlvbgEBdRcALwEAzwp1FwAAABX/////AQH/////AQAAABVgiQoCAAAAAAACAAAASWQB" +
           "AXYXAC4ARHYXAAAAEf////8BAf////8AAAAAFWCJCgIAAAABABkAAABDb25maWd1cmVkRGVmYXVsdFN0" +
           "b3BNb2RlAQF5FwAvAD95FwAAAAT/////AwP/////AAAAACRggAoBAAAAAQAJAAAARXhlY3V0aW5nAQGP" +
           "EwMAAAAAJgAAAEVudGl0eSBpcyBpbiBhIGNvbmRpdGlvbiBvZiBleGVjdXRpb24uAC8BAAMJjxMAAAMA" +
           "AAAAMwEBAZUTADMBAQGTEwA0AQEBlBMBAAAAFWCpCgIAAAAAAAsAAABTdGF0ZU51bWJlcgEBfBcALgBE" +
           "fBcAAAcDAAAAAAf/////AQH/////AAAAACRggAoBAAAAAQAPAAAARXhlY3V0aW5nVG9JZGxlAQGVEwMA" +
           "AAAAHgAAAENoYW5nZXMgZnJvbSBFeGVjdXRpbmcgdG8gSWRsZQAvAQAGCZUTAAADAAAAADMAAQGPEwA0" +
           "AAEBjRMANgABAAcJAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51bWJlcgEBghcALgBEghcAAAcG" +
           "AAAAAAf/////AQH/////AAAAACRggAoBAAAAAQAQAAAARXhlY3V0aW5nVG9SZWFkeQEBkxMDAAAAAB8A" +
           "AABDaGFuZ2VzIGZyb20gRXhlY3V0aW5nIHRvIFJlYWR5AC8BAAYJkxMAAAQAAAAAMwABAY8TADQAAQGO" +
           "EwA1AAEBWhsANgABAAcJAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51bWJlcgEBgBcALgBEgBcA" +
           "AAcFAAAAAAf/////AQH/////AAAAACRggAoBAAAAAQAEAAAASWRsZQEBjRMDAAAAADAAAABFbnRpdHkg" +
           "aXMgbm90IGluIGEgY29uZGl0aW9uIHRvIHN0YXJ0IGV4ZWN1dGlvbi4ALwEAAwmNEwAABQAAAAA0AQEB" +
           "lRMAMwEBAZYTADQBAQGWEwAzAQEBkRMANAEBAZATAQAAABVgqQoCAAAAAAALAAAAU3RhdGVOdW1iZXIB" +
           "AXoXAC4ARHoXAAAHAQAAAAAH/////wEB/////wAAAAAkYIAKAQAAAAEACgAAAElkbGVUb0lkbGUBAZYT" +
           "AwAAAAAaAAAAQ2hhbmdlcyBmcm9tIElkbGUgdG8gSWRsZS4ALwEABgmWEwAAAwAAAAAzAAEBjRMANAAB" +
           "AY0TADYAAQAHCQEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRpb25OdW1iZXIBAYMXAC4ARIMXAAAHAQAA" +
           "AAAH/////wEB/////wAAAAAkYIAKAQAAAAEACwAAAElkbGVUb1JlYWR5AQGREwMAAAAAGgAAAENoYW5n" +
           "ZXMgZnJvbSBJZGxlIHRvIFJlYWR5AC8BAAYJkRMAAAMAAAAAMwABAY0TADQAAQGOEwA2AAEABwkBAAAA" +
           "FWCpCgIAAAAAABAAAABUcmFuc2l0aW9uTnVtYmVyAQF/FwAuAER/FwAABwIAAAAAB/////8BAf////8A" +
           "AAAAFWCJCgIAAAABABQAAABMYXN0VHJhbnNpdGlvblJlYXNvbgEBdxcALwEA5it3FwAAAAT/////AwP/" +
           "////AgAAABdgqQoCAAAAAAAKAAAARW51bVZhbHVlcwEBhBcALgBEhBcAAJYGAAAAAQA7IAFAAAAAAAAA" +
           "AAAAAAADAgAAAGVuBwAAAFVua25vd24DAgAAAGVuGwAAAENhdXNlZCBieSBhbiB1bmtub3duIHJlYXNv" +
           "bgEAOyABQgAAAAEAAAAAAAAAAwIAAABlbggAAABFeHRlcm5hbAMCAAAAZW4cAAAAQ2F1c2VkIGJ5IGV4" +
           "dGVybmFsIG9wZXJhdGlvbgEAOyABPgAAAAIAAAAAAAAAAwIAAABlbgYAAABEaXJlY3QDAgAAAGVuGgAA" +
           "AENhdXNlZCBieSBkaXJlY3Qgb3BlcmF0aW9uAQA7IAFGAAAAAwAAAAAAAAADAgAAAGVuBgAAAFN5c3Rl" +
           "bQMCAAAAZW4iAAAAQ2F1c2VkIGJ5IHN5c3RlbSBzcGVjaWZpYyBiZWhhdmlvcgEAOyABNQAAAAQAAAAA" +
           "AAAAAwIAAABlbgUAAABFcnJvcgMCAAAAZW4SAAAAQ2F1c2VkIGJ5IGFuIGVycm9yAQA7IAFUAAAABQAA" +
           "AAAAAAADAgAAAGVuCwAAAEFwcGxpY2F0aW9uAwIAAABlbisAAABDYXVzZWQgZXhwbGljaXRseSBieSBl" +
           "bmQgdXNlciBwcm9ncmFtIGxvZ2ljAQCqHQEAAAABAAAAAAAAAAEB/////wAAAAAVYKkKAgAAAAAACwAA" +
           "AFZhbHVlQXNUZXh0AQGFFwAuAESFFwAAFQIHAAAASW52YWxpZAAV/////wEB/////wAAAAAXYKkKAgAA" +
           "AAEAEQAAAFBvc3NpYmxlU3RvcE1vZGVzAQF4FwAvAD94FwAAlgUAAAABADsgAWsAAAABAAAAAAAAAAMC" +
           "AAAAZW4GAAAAT25QYXRoAwIAAABlbkcAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIGluIGEgY29udHJv" +
           "bGxlZCBtYW5uZXIgYWxvbmcgdGhlIHByb2dyYW1tZWQgcGF0aAEAOyABcgAAAAIAAAAAAAAAAwIAAABl" +
           "bgoAAABFbmRPZkN5Y2xlAwIAAABlbkoAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIHdoZW4gdGhlIGN1" +
           "cnJlbnQgcHJvZHVjdGlvbiBjeWNsZSBoYXMgYmVlbiBmaW5pc2hlZAEAOyAByQAAAAMAAAAAAAAAAwIA" +
           "AABlbgsAAABQcm9jZXNzU3RvcAMCAAAAZW6gAAAAQXBwbGljYXRpb24gZGVwZW5kZW50IHN0b3AgaW5z" +
           "dHJ1Y3Rpb24gdGhhdCBzdG9wcyBwcm9ncmFtIGV4ZWN1dGlvbiBhdCBhIGZhdm91cmFibGUgcG9pbnQg" +
           "Zm9yIHRoZSBhcHBsaWNhdGlvbiwgZS5nLiBhdCB0aGUgZW5kIG9mIGEgcGFpbnQgc3Ryb2tlIG9yIHNl" +
           "YWxpbmcgYmVhZAEAOyABrAAAAAQAAAAAAAAAAwIAAABlbgkAAABRdWlja1N0b3ADAgAAAGVuhQAAAFRo" +
           "aXMgc3RvcCBpcyBwZXJmb3JtZWQgYnkgcmFtcGluZyBkb3duIG1vdGlvbiBhcyBmYXN0IGFzIHBvc3Np" +
           "YmxlIHVzaW5nIG9wdGltdW0gbW90b3IgcGVyZm9ybWFuY2UuIFRoZSByb2JvdCBtYXkgbm90IHN0YXkg" +
           "b24gdGhlIHBhdGgBADsgAYsAAAAFAAAAAAAAAAMCAAAAZW4QAAAARW5kT2ZJbnN0cnVjdGlvbgMCAAAA" +
           "ZW5dAAAAVGhpcyBzdG9wIGNhbiBiZSB1c2VkIHRvIHN0b3AgdGhlIHByb2dyYW0gZXhlY3V0aW9uIHdo" +
           "ZW4gdGhlIGN1cnJlbnQgaW5zdHJ1Y3Rpb24gaXMgY29tcGxldGVkAQCqHQEAAAABAAAAAAAAAAMD////" +
           "/wAAAAAkYIAKAQAAAAEABQAAAFJlYWR5AQGOEwMAAAAALAAAAEVudGl0eSBpcyBpbiBhIGNvbmRpdGlv" +
           "biB0byBzdGFydCBleGVjdXRpb24uAC8BAAMJjhMAAAQAAAAANAEBAZMTADQBAQGREwAzAQEBlBMAMwEB" +
           "AZATAQAAABVgqQoCAAAAAAALAAAAU3RhdGVOdW1iZXIBAXsXAC4ARHsXAAAHAgAAAAAH/////wEB////" +
           "/wAAAAAkYIAKAQAAAAEAEAAAAFJlYWR5VG9FeGVjdXRpbmcBAZQTAwAAAAAfAAAAQ2hhbmdlcyBmcm9t" +
           "IFJlYWR5IHRvIEV4ZWN1dGluZwAvAQAGCZQTAAAEAAAAADQAAQGPEwAzAAEBjhMANQABAVkbADYAAQAH" +
           "CQEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRpb25OdW1iZXIBAYEXAC4ARIEXAAAHBAAAAAAH/////wEB" +
           "/////wAAAAAkYIAKAQAAAAEACwAAAFJlYWR5VG9JZGxlAQGQEwMAAAAAGgAAAENoYW5nZXMgZnJvbSBS" +
           "ZWFkeSB0byBJZGxlAC8BAAYJkBMAAAMAAAAANAABAY0TADMAAQGOEwA2AAEABwkBAAAAFWCpCgIAAAAA" +
           "ABAAAABUcmFuc2l0aW9uTnVtYmVyAQF+FwAuAER+FwAABwMAAAAAB/////8BAf////8AAAAABGGCCgQA" +
           "AAABAAUAAABTdGFydAEBWRsALwEBWRtZGwAAAQEBAAAAADUBAQGUEwEAAAAXYKkKAgAAAAAADwAAAE91" +
           "dHB1dEFyZ3VtZW50cwEBhxcALgBEhxcAAJYBAAAAAQAqAQGjAAAABgAAAFN0YXR1cwAG/////wAAAAAC" +
           "igAAADAg4oCTIE9LClZhbHVlcyA+IDAgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5lZCBieSB0" +
           "aGlzIGFuZCBmdXR1cmUgc3RhbmRhcmRzLgpWYWx1ZXMgPCAwIHNoYWxsIGJlIHVzZWQgZm9yIGFwcGxp" +
           "Y2F0aW9uLXNwZWNpZmljIGVycm9ycwEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAABAAQA" +
           "AABTdG9wAQFaGwAvAQFaG1obAAABAQEAAAAANQEBAZMTAgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1" +
           "bWVudHMBAYgXAC4ARIgXAACWAQAAAAEAKgEBrQAAAAgAAABTdG9wTW9kZQAI/////wAAAAACkgAAAHBy" +
           "b3ZpZGVzIGEgd2F5IHRvIGRpZmZlcmVudGlhdGUgYmV0d2VlbiBkaWZmZXJlbnQgc3RvcCBtb2Rlcy4g" +
           "VGhpcyBwYXJhbWV0ZXIgc2hvdWxkIGNvcnJlc3BvbmQgdG8gb25lIG9mIHRoZSB2YWx1ZXMgaW4gdGhl" +
           "IFBvc3NpYmxlU3RvcE1vZGVzIGFycmF5AQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAA" +
           "DwAAAE91dHB1dEFyZ3VtZW50cwEBiRcALgBEiRcAAJYBAAAAAQAqAQGjAAAABgAAAFN0YXR1cwAG////" +
           "/wAAAAACigAAADAg4oCTIE9LClZhbHVlcyA+IDAgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5l" +
           "ZCBieSB0aGlzIGFuZCBmdXR1cmUgc3RhbmRhcmRzLgpWYWx1ZXMgPCAwIHNoYWxsIGJlIHVzZWQgZm9y" +
           "IGFwcGxpY2F0aW9uLXNwZWNpZmljIGVycm9ycwEAKAEBAAAAAQAAAAEAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public BaseDataVariableState<short> ConfiguredDefaultStopMode
        {
            get => m_configuredDefaultStopMode;

            set
            {
                if (!Object.ReferenceEquals(m_configuredDefaultStopMode, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_configuredDefaultStopMode = value;
            }
        }

        public MultiStateValueDiscreteState<short> LastTransitionReason
        {
            get => m_lastTransitionReason;

            set
            {
                if (!Object.ReferenceEquals(m_lastTransitionReason, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_lastTransitionReason = value;
            }
        }

        public BaseDataVariableState<EnumValueType[]> PossibleStopModes
        {
            get => m_possibleStopModes;

            set
            {
                if (!Object.ReferenceEquals(m_possibleStopModes, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_possibleStopModes = value;
            }
        }

        public StartMethodState Start
        {
            get => m_startMethod;

            set
            {
                if (!Object.ReferenceEquals(m_startMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_startMethod = value;
            }
        }

        public StopMethodState Stop
        {
            get => m_stopMethod;

            set
            {
                if (!Object.ReferenceEquals(m_stopMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_stopMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_configuredDefaultStopMode != null)
            {
                children.Add(m_configuredDefaultStopMode);
            }

            if (m_lastTransitionReason != null)
            {
                children.Add(m_lastTransitionReason);
            }

            if (m_possibleStopModes != null)
            {
                children.Add(m_possibleStopModes);
            }

            if (m_startMethod != null)
            {
                children.Add(m_startMethod);
            }

            if (m_stopMethod != null)
            {
                children.Add(m_stopMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_configuredDefaultStopMode, child))
            {
                m_configuredDefaultStopMode = null;
                return;
            }

            if (Object.ReferenceEquals(m_lastTransitionReason, child))
            {
                m_lastTransitionReason = null;
                return;
            }

            if (Object.ReferenceEquals(m_possibleStopModes, child))
            {
                m_possibleStopModes = null;
                return;
            }

            if (Object.ReferenceEquals(m_startMethod, child))
            {
                m_startMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_stopMethod, child))
            {
                m_stopMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.ConfiguredDefaultStopMode:
                {
                    if (createOrReplace)
                    {
                        if (ConfiguredDefaultStopMode == null)
                        {
                            if (replacement == null)
                            {
                                ConfiguredDefaultStopMode = new BaseDataVariableState<short>(this);
                            }
                            else
                            {
                                ConfiguredDefaultStopMode = (BaseDataVariableState<short>)replacement;
                            }
                        }
                    }

                    instance = ConfiguredDefaultStopMode;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.LastTransitionReason:
                {
                    if (createOrReplace)
                    {
                        if (LastTransitionReason == null)
                        {
                            if (replacement == null)
                            {
                                LastTransitionReason = new MultiStateValueDiscreteState<short>(this);
                            }
                            else
                            {
                                LastTransitionReason = (MultiStateValueDiscreteState<short>)replacement;
                            }
                        }
                    }

                    instance = LastTransitionReason;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.PossibleStopModes:
                {
                    if (createOrReplace)
                    {
                        if (PossibleStopModes == null)
                        {
                            if (replacement == null)
                            {
                                PossibleStopModes = new BaseDataVariableState<EnumValueType[]>(this);
                            }
                            else
                            {
                                PossibleStopModes = (BaseDataVariableState<EnumValueType[]>)replacement;
                            }
                        }
                    }

                    instance = PossibleStopModes;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Start:
                {
                    if (createOrReplace)
                    {
                        if (Start == null)
                        {
                            if (replacement == null)
                            {
                                Start = new StartMethodState(this);
                            }
                            else
                            {
                                Start = (StartMethodState)replacement;
                            }
                        }
                    }

                    instance = Start;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Stop:
                {
                    if (createOrReplace)
                    {
                        if (Stop == null)
                        {
                            if (replacement == null)
                            {
                                Stop = new StopMethodState(this);
                            }
                            else
                            {
                                Stop = (StopMethodState)replacement;
                            }
                        }
                    }

                    instance = Stop;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<short> m_configuredDefaultStopMode;
        private MultiStateValueDiscreteState<short> m_lastTransitionReason;
        private BaseDataVariableState<EnumValueType[]> m_possibleStopModes;
        private StartMethodState m_startMethod;
        private StopMethodState m_stopMethod;
        #endregion
    }
    #endif
    #endregion

    #region SystemOperationStateMachineTypeState Class
    #if (!OPCUA_EXCLUDE_SystemOperationStateMachineTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class SystemOperationStateMachineTypeState : OperationStateMachineTypeState
    {
        #region Constructors
        public SystemOperationStateMachineTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.SystemOperationStateMachineType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ConfiguredDefaultStopMode != null)
            {
                ConfiguredDefaultStopMode.Initialize(context, ConfiguredDefaultStopMode_InitializationString);
            }

            if (ExecutingSubstateMachine != null)
            {
                ExecutingSubstateMachine.Initialize(context, ExecutingSubstateMachine_InitializationString);
            }

            if (GetReady != null)
            {
                GetReady.Initialize(context, GetReady_InitializationString);
            }

            if (IdleSubstateMachine != null)
            {
                IdleSubstateMachine.Initialize(context, IdleSubstateMachine_InitializationString);
            }

            if (PossibleStopModes != null)
            {
                PossibleStopModes.Initialize(context, PossibleStopModes_InitializationString);
            }

            if (StandDown != null)
            {
                StandDown.Initialize(context, StandDown_InitializationString);
            }

            if (Start != null)
            {
                Start.Initialize(context, Start_InitializationString);
            }

            if (Stop != null)
            {
                Stop.Initialize(context, Stop_InitializationString);
            }
        }

        #region Initialization String
        private const string ConfiguredDefaultStopMode_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAEAGQAAAENvbmZpZ3VyZWREZWZhdWx0U3RvcE1vZGUBAakXAC8AP6kXAAAABP////8DA///" +
           "//8AAAAA";

        private const string ExecutingSubstateMachine_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEAGAAAAEV4ZWN1dGluZ1N1YnN0YXRlTWFjaGluZQEBpBMALwEB7wOkEwAAAgAAAAB1AQEB" +
           "qBMAKQABAAcJAwAAABVgiQoCAAAAAAAMAAAAQ3VycmVudFN0YXRlAQHDFwAvAQDICsMXAAAAFf////8B" +
           "Af////8BAAAAFWCJCgIAAAAAAAIAAABJZAEBxBcALgBExBcAAAAR/////wEB/////wAAAAAVYIkKAgAA" +
           "AAAADgAAAExhc3RUcmFuc2l0aW9uAQG+FwAvAQDPCr4XAAAAFf////8BAf////8BAAAAFWCJCgIAAAAA" +
           "AAIAAABJZAEBvxcALgBEvxcAAAAR/////wEB/////wAAAAAVYIkKAgAAAAEAFAAAAExhc3RUcmFuc2l0" +
           "aW9uUmVhc29uAQHAFwAvAQDmK8AXAAAABP////8DA/////8CAAAAF2CpCgIAAAAAAAoAAABFbnVtVmFs" +
           "dWVzAQHBFwAuAETBFwAAlgYAAAABADsgAUAAAAAAAAAAAAAAAAMCAAAAZW4HAAAAVW5rbm93bgMCAAAA" +
           "ZW4bAAAAQ2F1c2VkIGJ5IGFuIHVua25vd24gcmVhc29uAQA7IAFCAAAAAQAAAAAAAAADAgAAAGVuCAAA" +
           "AEV4dGVybmFsAwIAAABlbhwAAABDYXVzZWQgYnkgZXh0ZXJuYWwgb3BlcmF0aW9uAQA7IAE+AAAAAgAA" +
           "AAAAAAADAgAAAGVuBgAAAERpcmVjdAMCAAAAZW4aAAAAQ2F1c2VkIGJ5IGRpcmVjdCBvcGVyYXRpb24B" +
           "ADsgAUYAAAADAAAAAAAAAAMCAAAAZW4GAAAAU3lzdGVtAwIAAABlbiIAAABDYXVzZWQgYnkgc3lzdGVt" +
           "IHNwZWNpZmljIGJlaGF2aW9yAQA7IAE1AAAABAAAAAAAAAADAgAAAGVuBQAAAEVycm9yAwIAAABlbhIA" +
           "AABDYXVzZWQgYnkgYW4gZXJyb3IBADsgAVQAAAAFAAAAAAAAAAMCAAAAZW4LAAAAQXBwbGljYXRpb24D" +
           "AgAAAGVuKwAAAENhdXNlZCBleHBsaWNpdGx5IGJ5IGVuZCB1c2VyIHByb2dyYW0gbG9naWMBAKodAQAA" +
           "AAEAAAAAAAAAAQH/////AAAAABVgqQoCAAAAAAALAAAAVmFsdWVBc1RleHQBAcIXAC4ARMIXAAAVAgcA" +
           "AABJbnZhbGlkABX/////AQH/////AAAAAA==";

        private const string GetReady_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEACAAAAEdldFJlYWR5AQFeGwAvAQFeG14bAAABAQEAAAAANQEBAaoTAQAAABdgqQoCAAAA" +
           "AAAPAAAAT3V0cHV0QXJndW1lbnRzAQG0FwAuAES0FwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/" +
           "////AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZp" +
           "bmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBm" +
           "b3IgYXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";

        private const string IdleSubstateMachine_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEAEwAAAElkbGVTdWJzdGF0ZU1hY2hpbmUBAaMTAC8BAfEDoxMAAAIAAAAAdQEBAaYTACkA" +
           "AQAHCQMAAAAVYIkKAgAAAAAADAAAAEN1cnJlbnRTdGF0ZQEBvBcALwEAyAq8FwAAABX/////AQH/////" +
           "AQAAABVgiQoCAAAAAAACAAAASWQBAb0XAC4ARL0XAAAAEf////8BAf////8AAAAAFWCJCgIAAAAAAA4A" +
           "AABMYXN0VHJhbnNpdGlvbgEBtxcALwEAzwq3FwAAABX/////AQH/////AQAAABVgiQoCAAAAAAACAAAA" +
           "SWQBAbgXAC4ARLgXAAAAEf////8BAf////8AAAAAFWCJCgIAAAABABQAAABMYXN0VHJhbnNpdGlvblJl" +
           "YXNvbgEBuRcALwEA5iu5FwAAAAT/////AwP/////AgAAABdgqQoCAAAAAAAKAAAARW51bVZhbHVlcwEB" +
           "uhcALgBEuhcAAJYBAAAAAQA7IAEKAAAAAAAAAAAAAAAAAAEAqh0BAAAAAQAAAAAAAAABAf////8AAAAA" +
           "FWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEBuxcALgBEuxcAABUCBwAAAEludmFsaWQAFf////8BAf//" +
           "//8AAAAA";

        private const string PossibleStopModes_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8X" +
           "YKkKAgAAAAEAEQAAAFBvc3NpYmxlU3RvcE1vZGVzAQGvFwAvAD+vFwAAlgUAAAABADsgAWsAAAABAAAA" +
           "AAAAAAMCAAAAZW4GAAAAT25QYXRoAwIAAABlbkcAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIGluIGEg" +
           "Y29udHJvbGxlZCBtYW5uZXIgYWxvbmcgdGhlIHByb2dyYW1tZWQgcGF0aAEAOyABcgAAAAIAAAAAAAAA" +
           "AwIAAABlbgoAAABFbmRPZkN5Y2xlAwIAAABlbkoAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIHdoZW4g" +
           "dGhlIGN1cnJlbnQgcHJvZHVjdGlvbiBjeWNsZSBoYXMgYmVlbiBmaW5pc2hlZAEAOyAByQAAAAMAAAAA" +
           "AAAAAwIAAABlbgsAAABQcm9jZXNzU3RvcAMCAAAAZW6gAAAAQXBwbGljYXRpb24gZGVwZW5kZW50IHN0" +
           "b3AgaW5zdHJ1Y3Rpb24gdGhhdCBzdG9wcyBwcm9ncmFtIGV4ZWN1dGlvbiBhdCBhIGZhdm91cmFibGUg" +
           "cG9pbnQgZm9yIHRoZSBhcHBsaWNhdGlvbiwgZS5nLiBhdCB0aGUgZW5kIG9mIGEgcGFpbnQgc3Ryb2tl" +
           "IG9yIHNlYWxpbmcgYmVhZAEAOyABrAAAAAQAAAAAAAAAAwIAAABlbgkAAABRdWlja1N0b3ADAgAAAGVu" +
           "hQAAAFRoaXMgc3RvcCBpcyBwZXJmb3JtZWQgYnkgcmFtcGluZyBkb3duIG1vdGlvbiBhcyBmYXN0IGFz" +
           "IHBvc3NpYmxlIHVzaW5nIG9wdGltdW0gbW90b3IgcGVyZm9ybWFuY2UuIFRoZSByb2JvdCBtYXkgbm90" +
           "IHN0YXkgb24gdGhlIHBhdGgBADsgAYsAAAAFAAAAAAAAAAMCAAAAZW4QAAAARW5kT2ZJbnN0cnVjdGlv" +
           "bgMCAAAAZW5dAAAAVGhpcyBzdG9wIGNhbiBiZSB1c2VkIHRvIHN0b3AgdGhlIHByb2dyYW0gZXhlY3V0" +
           "aW9uIHdoZW4gdGhlIGN1cnJlbnQgaW5zdHJ1Y3Rpb24gaXMgY29tcGxldGVkAQCqHQEAAAABAAAAAAAA" +
           "AAMD/////wAAAAA=";

        private const string StandDown_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEACQAAAFN0YW5kRG93bgEBXxsALwEBXxtfGwAAAQECAAAAADUBAQGpEwA1AQEBqxMBAAAA" +
           "F2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAbYXAC4ARLYXAACWAQAAAAEAKgEBowAAAAYAAABT" +
           "dGF0dXMABv////8AAAAAAooAAAAwIOKAkyBPSwpWYWx1ZXMgPiAwIGFyZSByZXNlcnZlZCBmb3IgZXJy" +
           "b3JzIGRlZmluZWQgYnkgdGhpcyBhbmQgZnV0dXJlIHN0YW5kYXJkcy4KVmFsdWVzIDwgMCBzaGFsbCBi" +
           "ZSB1c2VkIGZvciBhcHBsaWNhdGlvbi1zcGVjaWZpYyBlcnJvcnMBACgBAQAAAAEAAAABAAAAAQH/////" +
           "AAAAAA==";

        private const string Start_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEABQAAAFN0YXJ0AQFcGwAvAQFZG1wbAAABAQEAAAAANQEBAawTAQAAABdgqQoCAAAAAAAP" +
           "AAAAT3V0cHV0QXJndW1lbnRzAQGwFwAuAESwFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////" +
           "AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVk" +
           "IGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3Ig" +
           "YXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";

        private const string Stop_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEABAAAAFN0b3ABAV0bAC8BAVobXRsAAAEBAQAAAAA1AQEBrRMCAAAAF2CpCgIAAAAAAA4A" +
           "AABJbnB1dEFyZ3VtZW50cwEBsRcALgBEsRcAAJYBAAAAAQAqAQGtAAAACAAAAFN0b3BNb2RlAAj/////" +
           "AAAAAAKSAAAAcHJvdmlkZXMgYSB3YXkgdG8gZGlmZmVyZW50aWF0ZSBiZXR3ZWVuIGRpZmZlcmVudCBz" +
           "dG9wIG1vZGVzLiBUaGlzIHBhcmFtZXRlciBzaG91bGQgY29ycmVzcG9uZCB0byBvbmUgb2YgdGhlIHZh" +
           "bHVlcyBpbiB0aGUgUG9zc2libGVTdG9wTW9kZXMgYXJyYXkBACgBAQAAAAEAAAABAAAAAQH/////AAAA" +
           "ABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQGyFwAuAESyFwAAlgEAAAABACoBAaMAAAAGAAAA" +
           "U3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVy" +
           "cm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwg" +
           "YmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB////" +
           "/wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAJwAAAFN5c3RlbU9wZXJhdGlvblN0YXRlTWFjaGluZVR5cGVJbnN0YW5jZQEB/QMBAf0D" +
           "/QMAAAEAAAAAKQABAAcJFAAAABVgiQgCAAAAAAAMAAAAQ3VycmVudFN0YXRlAQEAAAAvAQDICgAV////" +
           "/wEB/////wEAAAAVYIkIAgAAAAAAAgAAAElkAQEAAAAuAEQAEf////8BAf////8AAAAAFWCJCgIAAAAA" +
           "AA4AAABMYXN0VHJhbnNpdGlvbgEBqhcALwEAzwqqFwAAABX/////AQH/////AQAAABVgiQoCAAAAAAAC" +
           "AAAASWQBAasXAC4ARKsXAAAAEf////8BAf////8AAAAAFWCJCgIAAAABABkAAABDb25maWd1cmVkRGVm" +
           "YXVsdFN0b3BNb2RlAQGpFwAvAD+pFwAAAAT/////AwP/////AAAAACRggAoBAAAAAQAJAAAARXhlY3V0" +
           "aW5nAQGoEwMAAAAAJgAAAEVudGl0eSBpcyBpbiBhIGNvbmRpdGlvbiBvZiBleGVjdXRpb24uAC8BAAMJ" +
           "qBMAAAQAAAAAMwEBAa4TADMBAQGtEwA0AQEBrBMAdQABAaQTAQAAABVgqQoCAAAAAAALAAAAU3RhdGVO" +
           "dW1iZXIBAccXAC4ARMcXAAAHAwAAAAAH/////wEB/////wAAAAAkYIAKAQAAAAEADwAAAEV4ZWN1dGlu" +
           "Z1RvSWRsZQEBrhMDAAAAAB4AAABDaGFuZ2VzIGZyb20gRXhlY3V0aW5nIHRvIElkbGUALwEABgmuEwAA" +
           "AwAAAAAzAAEBqBMANAABAaYTADYAAQAHCQEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRpb25OdW1iZXIB" +
           "Ac0XAC4ARM0XAAAHBgAAAAAH/////wEB/////wAAAAAkYIAKAQAAAAEAEAAAAEV4ZWN1dGluZ1RvUmVh" +
           "ZHkBAa0TAwAAAAAfAAAAQ2hhbmdlcyBmcm9tIEV4ZWN1dGluZyB0byBSZWFkeQAvAQAGCa0TAAAEAAAA" +
           "ADMAAQGoEwA0AAEBpxMANQABAV0bADYAAQAHCQEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRpb25OdW1i" +
           "ZXIBAcwXAC4ARMwXAAAHBQAAAAAH/////wEB/////wAAAAAkYIAKAQAAAAEABAAAAElkbGUBAaYTAwAA" +
           "AAAwAAAARW50aXR5IGlzIG5vdCBpbiBhIGNvbmRpdGlvbiB0byBzdGFydCBleGVjdXRpb24uAC8BAAMJ" +
           "phMAAAYAAAAANAEBAa4TADMBAQGpEwA0AQEBqRMAMwEBAaoTADQBAQGrEwB1AAEBoxMBAAAAFWCpCgIA" +
           "AAAAAAsAAABTdGF0ZU51bWJlcgEBxRcALgBExRcAAAcBAAAAAAf/////AQH/////AAAAACRggAoBAAAA" +
           "AQAKAAAASWRsZVRvSWRsZQEBqRMDAAAAABoAAABDaGFuZ2VzIGZyb20gSWRsZSB0byBJZGxlLgAvAQAG" +
           "CakTAAAEAAAAADMAAQGmEwA0AAEBphMANgABAAcJADUAAQFfGwEAAAAVYKkKAgAAAAAAEAAAAFRyYW5z" +
           "aXRpb25OdW1iZXIBAcgXAC4ARMgXAAAHAQAAAAAH/////wEB/////wAAAAAkYIAKAQAAAAEACwAAAElk" +
           "bGVUb1JlYWR5AQGqEwMAAAAAGgAAAENoYW5nZXMgZnJvbSBJZGxlIHRvIFJlYWR5AC8BAAYJqhMAAAQA" +
           "AAAAMwABAaYTADQAAQGnEwA2AAEABwkANQABAV4bAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51" +
           "bWJlcgEByRcALgBEyRcAAAcCAAAAAAf/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5z" +
           "aXRpb25SZWFzb24BAawXAC8BAOYrrBcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1W" +
           "YWx1ZXMBAa0XAC4ARK0XAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIA" +
           "AABlbhsAAABDYXVzZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4I" +
           "AAAARXh0ZXJuYWwDAgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAAC" +
           "AAAAAAAAAAMCAAAAZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlv" +
           "bgEAOyABRgAAAAMAAAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0" +
           "ZW0gc3BlY2lmaWMgYmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVu" +
           "EgAAAENhdXNlZCBieSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlv" +
           "bgMCAAAAZW4rAAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0B" +
           "AAAAAQAAAAAAAAABAf////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEBrhcALgBErhcAABUC" +
           "BwAAAEludmFsaWQAFf////8BAf////8AAAAAF2CpCgIAAAABABEAAABQb3NzaWJsZVN0b3BNb2RlcwEB" +
           "rxcALwA/rxcAAJYFAAAAAQA7IAFrAAAAAQAAAAAAAAADAgAAAGVuBgAAAE9uUGF0aAMCAAAAZW5HAAAA" +
           "U3RvcCBwcm9ncmFtIGV4ZWN1dGlvbiBpbiBhIGNvbnRyb2xsZWQgbWFubmVyIGFsb25nIHRoZSBwcm9n" +
           "cmFtbWVkIHBhdGgBADsgAXIAAAACAAAAAAAAAAMCAAAAZW4KAAAARW5kT2ZDeWNsZQMCAAAAZW5KAAAA" +
           "U3RvcCBwcm9ncmFtIGV4ZWN1dGlvbiB3aGVuIHRoZSBjdXJyZW50IHByb2R1Y3Rpb24gY3ljbGUgaGFz" +
           "IGJlZW4gZmluaXNoZWQBADsgAckAAAADAAAAAAAAAAMCAAAAZW4LAAAAUHJvY2Vzc1N0b3ADAgAAAGVu" +
           "oAAAAEFwcGxpY2F0aW9uIGRlcGVuZGVudCBzdG9wIGluc3RydWN0aW9uIHRoYXQgc3RvcHMgcHJvZ3Jh" +
           "bSBleGVjdXRpb24gYXQgYSBmYXZvdXJhYmxlIHBvaW50IGZvciB0aGUgYXBwbGljYXRpb24sIGUuZy4g" +
           "YXQgdGhlIGVuZCBvZiBhIHBhaW50IHN0cm9rZSBvciBzZWFsaW5nIGJlYWQBADsgAawAAAAEAAAAAAAA" +
           "AAMCAAAAZW4JAAAAUXVpY2tTdG9wAwIAAABlboUAAABUaGlzIHN0b3AgaXMgcGVyZm9ybWVkIGJ5IHJh" +
           "bXBpbmcgZG93biBtb3Rpb24gYXMgZmFzdCBhcyBwb3NzaWJsZSB1c2luZyBvcHRpbXVtIG1vdG9yIHBl" +
           "cmZvcm1hbmNlLiBUaGUgcm9ib3QgbWF5IG5vdCBzdGF5IG9uIHRoZSBwYXRoAQA7IAGLAAAABQAAAAAA" +
           "AAADAgAAAGVuEAAAAEVuZE9mSW5zdHJ1Y3Rpb24DAgAAAGVuXQAAAFRoaXMgc3RvcCBjYW4gYmUgdXNl" +
           "ZCB0byBzdG9wIHRoZSBwcm9ncmFtIGV4ZWN1dGlvbiB3aGVuIHRoZSBjdXJyZW50IGluc3RydWN0aW9u" +
           "IGlzIGNvbXBsZXRlZAEAqh0BAAAAAQAAAAAAAAADA/////8AAAAAJGCACgEAAAABAAUAAABSZWFkeQEB" +
           "pxMDAAAAACwAAABFbnRpdHkgaXMgaW4gYSBjb25kaXRpb24gdG8gc3RhcnQgZXhlY3V0aW9uLgAvAQAD" +
           "CacTAAAEAAAAADQBAQGtEwA0AQEBqhMAMwEBAawTADMBAQGrEwEAAAAVYKkKAgAAAAAACwAAAFN0YXRl" +
           "TnVtYmVyAQHGFwAuAETGFwAABwIAAAAAB/////8BAf////8AAAAAJGCACgEAAAABABAAAABSZWFkeVRv" +
           "RXhlY3V0aW5nAQGsEwMAAAAAHwAAAENoYW5nZXMgZnJvbSBSZWFkeSB0byBFeGVjdXRpbmcALwEABgms" +
           "EwAABAAAAAA0AAEBqBMAMwABAacTADUAAQFcGwA2AAEABwkBAAAAFWCpCgIAAAAAABAAAABUcmFuc2l0" +
           "aW9uTnVtYmVyAQHLFwAuAETLFwAABwQAAAAAB/////8BAf////8AAAAAJGCACgEAAAABAAsAAABSZWFk" +
           "eVRvSWRsZQEBqxMDAAAAABoAAABDaGFuZ2VzIGZyb20gUmVhZHkgdG8gSWRsZQAvAQAGCasTAAAEAAAA" +
           "ADQAAQGmEwAzAAEBpxMANgABAAcJADUAAQFfGwEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRpb25OdW1i" +
           "ZXIBAcoXAC4ARMoXAAAHAwAAAAAH/////wEB/////wAAAAAEYYIKBAAAAAEABQAAAFN0YXJ0AQFcGwAv" +
           "AQFZG1wbAAABAQEAAAAANQEBAawTAQAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQGwFwAu" +
           "AESwFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVz" +
           "ID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFu" +
           "ZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3BlY2lmaWMgZXJy" +
           "b3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEABAAAAFN0b3ABAV0bAC8BAVobXRsA" +
           "AAEBAQAAAAA1AQEBrRMCAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBsRcALgBEsRcAAJYB" +
           "AAAAAQAqAQGtAAAACAAAAFN0b3BNb2RlAAj/////AAAAAAKSAAAAcHJvdmlkZXMgYSB3YXkgdG8gZGlm" +
           "ZmVyZW50aWF0ZSBiZXR3ZWVuIGRpZmZlcmVudCBzdG9wIG1vZGVzLiBUaGlzIHBhcmFtZXRlciBzaG91" +
           "bGQgY29ycmVzcG9uZCB0byBvbmUgb2YgdGhlIHZhbHVlcyBpbiB0aGUgUG9zc2libGVTdG9wTW9kZXMg" +
           "YXJyYXkBACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRz" +
           "AQGyFwAuAESyFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMgT0sK" +
           "VmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVy" +
           "ZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3BlY2lm" +
           "aWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYIAKAQAAAAEAGAAAAEV4ZWN1dGluZ1N1" +
           "YnN0YXRlTWFjaGluZQEBpBMALwEB7wOkEwAAAgAAAAB1AQEBqBMAKQABAAcJAwAAABVgiQoCAAAAAAAM" +
           "AAAAQ3VycmVudFN0YXRlAQHDFwAvAQDICsMXAAAAFf////8BAf////8BAAAAFWCJCgIAAAAAAAIAAABJ" +
           "ZAEBxBcALgBExBcAAAAR/////wEB/////wAAAAAVYIkKAgAAAAAADgAAAExhc3RUcmFuc2l0aW9uAQG+" +
           "FwAvAQDPCr4XAAAAFf////8BAf////8BAAAAFWCJCgIAAAAAAAIAAABJZAEBvxcALgBEvxcAAAAR////" +
           "/wEB/////wAAAAAVYIkKAgAAAAEAFAAAAExhc3RUcmFuc2l0aW9uUmVhc29uAQHAFwAvAQDmK8AXAAAA" +
           "BP////8DA/////8CAAAAF2CpCgIAAAAAAAoAAABFbnVtVmFsdWVzAQHBFwAuAETBFwAAlgYAAAABADsg" +
           "AUAAAAAAAAAAAAAAAAMCAAAAZW4HAAAAVW5rbm93bgMCAAAAZW4bAAAAQ2F1c2VkIGJ5IGFuIHVua25v" +
           "d24gcmVhc29uAQA7IAFCAAAAAQAAAAAAAAADAgAAAGVuCAAAAEV4dGVybmFsAwIAAABlbhwAAABDYXVz" +
           "ZWQgYnkgZXh0ZXJuYWwgb3BlcmF0aW9uAQA7IAE+AAAAAgAAAAAAAAADAgAAAGVuBgAAAERpcmVjdAMC" +
           "AAAAZW4aAAAAQ2F1c2VkIGJ5IGRpcmVjdCBvcGVyYXRpb24BADsgAUYAAAADAAAAAAAAAAMCAAAAZW4G" +
           "AAAAU3lzdGVtAwIAAABlbiIAAABDYXVzZWQgYnkgc3lzdGVtIHNwZWNpZmljIGJlaGF2aW9yAQA7IAE1" +
           "AAAABAAAAAAAAAADAgAAAGVuBQAAAEVycm9yAwIAAABlbhIAAABDYXVzZWQgYnkgYW4gZXJyb3IBADsg" +
           "AVQAAAAFAAAAAAAAAAMCAAAAZW4LAAAAQXBwbGljYXRpb24DAgAAAGVuKwAAAENhdXNlZCBleHBsaWNp" +
           "dGx5IGJ5IGVuZCB1c2VyIHByb2dyYW0gbG9naWMBAKodAQAAAAEAAAAAAAAAAQH/////AAAAABVgqQoC" +
           "AAAAAAALAAAAVmFsdWVBc1RleHQBAcIXAC4ARMIXAAAVAgcAAABJbnZhbGlkABX/////AQH/////AAAA" +
           "AARhggoEAAAAAQAIAAAAR2V0UmVhZHkBAV4bAC8BAV4bXhsAAAEBAQAAAAA1AQEBqhMBAAAAF2CpCgIA" +
           "AAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAbQXAC4ARLQXAACWAQAAAAEAKgEBowAAAAYAAABTdGF0dXMA" +
           "Bv////8AAAAAAooAAAAwIOKAkyBPSwpWYWx1ZXMgPiAwIGFyZSByZXNlcnZlZCBmb3IgZXJyb3JzIGRl" +
           "ZmluZWQgYnkgdGhpcyBhbmQgZnV0dXJlIHN0YW5kYXJkcy4KVmFsdWVzIDwgMCBzaGFsbCBiZSB1c2Vk" +
           "IGZvciBhcHBsaWNhdGlvbi1zcGVjaWZpYyBlcnJvcnMBACgBAQAAAAEAAAABAAAAAQH/////AAAAAARg" +
           "gAoBAAAAAQATAAAASWRsZVN1YnN0YXRlTWFjaGluZQEBoxMALwEB8QOjEwAAAgAAAAB1AQEBphMAKQAB" +
           "AAcJAwAAABVgiQoCAAAAAAAMAAAAQ3VycmVudFN0YXRlAQG8FwAvAQDICrwXAAAAFf////8BAf////8B" +
           "AAAAFWCJCgIAAAAAAAIAAABJZAEBvRcALgBEvRcAAAAR/////wEB/////wAAAAAVYIkKAgAAAAAADgAA" +
           "AExhc3RUcmFuc2l0aW9uAQG3FwAvAQDPCrcXAAAAFf////8BAf////8BAAAAFWCJCgIAAAAAAAIAAABJ" +
           "ZAEBuBcALgBEuBcAAAAR/////wEB/////wAAAAAVYIkKAgAAAAEAFAAAAExhc3RUcmFuc2l0aW9uUmVh" +
           "c29uAQG5FwAvAQDmK7kXAAAABP////8DA/////8CAAAAF2CpCgIAAAAAAAoAAABFbnVtVmFsdWVzAQG6" +
           "FwAuAES6FwAAlgEAAAABADsgAQoAAAAAAAAAAAAAAAAAAQCqHQEAAAABAAAAAAAAAAEB/////wAAAAAV" +
           "YKkKAgAAAAAACwAAAFZhbHVlQXNUZXh0AQG7FwAuAES7FwAAFQIHAAAASW52YWxpZAAV/////wEB////" +
           "/wAAAAAEYYIKBAAAAAEACQAAAFN0YW5kRG93bgEBXxsALwEBXxtfGwAAAQECAAAAADUBAQGpEwA1AQEB" +
           "qxMBAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAbYXAC4ARLYXAACWAQAAAAEAKgEBowAA" +
           "AAYAAABTdGF0dXMABv////8AAAAAAooAAAAwIOKAkyBPSwpWYWx1ZXMgPiAwIGFyZSByZXNlcnZlZCBm" +
           "b3IgZXJyb3JzIGRlZmluZWQgYnkgdGhpcyBhbmQgZnV0dXJlIHN0YW5kYXJkcy4KVmFsdWVzIDwgMCBz" +
           "aGFsbCBiZSB1c2VkIGZvciBhcHBsaWNhdGlvbi1zcGVjaWZpYyBlcnJvcnMBACgBAQAAAAEAAAABAAAA" +
           "AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public ExecutingSubstateMachineTypeState ExecutingSubstateMachine
        {
            get => m_executingSubstateMachine;

            set
            {
                if (!Object.ReferenceEquals(m_executingSubstateMachine, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_executingSubstateMachine = value;
            }
        }

        public GetReadyMethodState GetReady
        {
            get => m_getReadyMethod;

            set
            {
                if (!Object.ReferenceEquals(m_getReadyMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_getReadyMethod = value;
            }
        }

        public IdleSubstateMachineTypeState IdleSubstateMachine
        {
            get => m_idleSubstateMachine;

            set
            {
                if (!Object.ReferenceEquals(m_idleSubstateMachine, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_idleSubstateMachine = value;
            }
        }

        public StandDownMethodState StandDown
        {
            get => m_standDownMethod;

            set
            {
                if (!Object.ReferenceEquals(m_standDownMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_standDownMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_executingSubstateMachine != null)
            {
                children.Add(m_executingSubstateMachine);
            }

            if (m_getReadyMethod != null)
            {
                children.Add(m_getReadyMethod);
            }

            if (m_idleSubstateMachine != null)
            {
                children.Add(m_idleSubstateMachine);
            }

            if (m_standDownMethod != null)
            {
                children.Add(m_standDownMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_executingSubstateMachine, child))
            {
                m_executingSubstateMachine = null;
                return;
            }

            if (Object.ReferenceEquals(m_getReadyMethod, child))
            {
                m_getReadyMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_idleSubstateMachine, child))
            {
                m_idleSubstateMachine = null;
                return;
            }

            if (Object.ReferenceEquals(m_standDownMethod, child))
            {
                m_standDownMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.ExecutingSubstateMachine:
                {
                    if (createOrReplace)
                    {
                        if (ExecutingSubstateMachine == null)
                        {
                            if (replacement == null)
                            {
                                ExecutingSubstateMachine = new ExecutingSubstateMachineTypeState(this);
                            }
                            else
                            {
                                ExecutingSubstateMachine = (ExecutingSubstateMachineTypeState)replacement;
                            }
                        }
                    }

                    instance = ExecutingSubstateMachine;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.GetReady:
                {
                    if (createOrReplace)
                    {
                        if (GetReady == null)
                        {
                            if (replacement == null)
                            {
                                GetReady = new GetReadyMethodState(this);
                            }
                            else
                            {
                                GetReady = (GetReadyMethodState)replacement;
                            }
                        }
                    }

                    instance = GetReady;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.IdleSubstateMachine:
                {
                    if (createOrReplace)
                    {
                        if (IdleSubstateMachine == null)
                        {
                            if (replacement == null)
                            {
                                IdleSubstateMachine = new IdleSubstateMachineTypeState(this);
                            }
                            else
                            {
                                IdleSubstateMachine = (IdleSubstateMachineTypeState)replacement;
                            }
                        }
                    }

                    instance = IdleSubstateMachine;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.StandDown:
                {
                    if (createOrReplace)
                    {
                        if (StandDown == null)
                        {
                            if (replacement == null)
                            {
                                StandDown = new StandDownMethodState(this);
                            }
                            else
                            {
                                StandDown = (StandDownMethodState)replacement;
                            }
                        }
                    }

                    instance = StandDown;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private ExecutingSubstateMachineTypeState m_executingSubstateMachine;
        private GetReadyMethodState m_getReadyMethod;
        private IdleSubstateMachineTypeState m_idleSubstateMachine;
        private StandDownMethodState m_standDownMethod;
        #endregion
    }
    #endif
    #endregion

    #region TaskControlStateMachineTypeState Class
    #if (!OPCUA_EXCLUDE_TaskControlStateMachineTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class TaskControlStateMachineTypeState : OperationStateMachineTypeState
    {
        #region Constructors
        public TaskControlStateMachineTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.TaskControlStateMachineType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ConfiguredDefaultStopMode != null)
            {
                ConfiguredDefaultStopMode.Initialize(context, ConfiguredDefaultStopMode_InitializationString);
            }

            if (LoadByName != null)
            {
                LoadByName.Initialize(context, LoadByName_InitializationString);
            }

            if (LoadByNodeId != null)
            {
                LoadByNodeId.Initialize(context, LoadByNodeId_InitializationString);
            }

            if (PossibleStopModes != null)
            {
                PossibleStopModes.Initialize(context, PossibleStopModes_InitializationString);
            }

            if (ReadySubstateMachine != null)
            {
                ReadySubstateMachine.Initialize(context, ReadySubstateMachine_InitializationString);
            }

            if (Start != null)
            {
                Start.Initialize(context, Start_InitializationString);
            }

            if (Stop != null)
            {
                Stop.Initialize(context, Stop_InitializationString);
            }

            if (UnloadByName != null)
            {
                UnloadByName.Initialize(context, UnloadByName_InitializationString);
            }

            if (UnloadByNodeId != null)
            {
                UnloadByNodeId.Initialize(context, UnloadByNodeId_InitializationString);
            }

            if (UnloadProgram != null)
            {
                UnloadProgram.Initialize(context, UnloadProgram_InitializationString);
            }
        }

        #region Initialization String
        private const string ConfiguredDefaultStopMode_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAEAGQAAAENvbmZpZ3VyZWREZWZhdWx0U3RvcE1vZGUBAc4XAC8AP84XAAAABP////8DA///" +
           "//8AAAAA";

        private const string LoadByName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEACgAAAExvYWRCeU5hbWUBAWMbAC8BAWMbYxsAAAEBAQAAAAA1AQEBtBMCAAAAF2CpCgIA" +
           "AAAAAA4AAABJbnB1dEFyZ3VtZW50cwEB/xcALgBE/xcAAJYBAAAAAQAqAQFIAAAABAAAAE5hbWUADP//" +
           "//8AAAAAAjEAAABOYW1lIHRvIGlkZW50aWZ5IGEgdGFzayBjb250cm9sIHByb2dyYW0gb3IgbW9kdWxl" +
           "AQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBABgA" +
           "LgBEABgAAJYBAAAAAQAqAQHBAQAABgAAAFN0YXR1cwAG/////wAAAAACqAEAADAg4oCTIE9LIOKAkyBF" +
           "dmVyeXRoaW5nIGlzIE9LCjEg4oCTIEVfU3lzdGVtU3RhdGUg4oCTIFRoZSBzeXN0ZW0gaXMgbm90IGlu" +
           "IGNvcnJlY3Qgc3RhdGUgZm9yIHRoaXMgb3BlcmF0aW9uCjIg4oCTIEVfVW5leHBlY3RlZEVycm9yIOKA" +
           "kyBVbmV4cGVjdGVkIEVycm9yIGR1cmluZyB0aGUgbWV0aG9kIGNhbGwKMyDigJMgRV9BY3RpdmVBbGFy" +
           "bSDigJMgQW4gQWN0aXZlIEFsYXJtIHByZXZlbnRzIHRoZSBzeXN0ZW0gc3RhcnQKNCDigJMgRV9BY2tu" +
           "b3dsZWRnZVJlcXVpcmVkIOKAkyBDb25kaXRpb24gbmVlZHMgdG8gYmUgYWNrbm93bGVkZ2VkCjwwIOKA" +
           "kyBzaGFsbCBiZSB1c2VkIGZvciB2ZW5kb3Itc3BlY2lmaWMgZXJyb3JzCj4wIOKAkyBhcmUgcmVzZXJ2" +
           "ZWQgZm9yIGVycm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMBACgBAQAAAAEA" +
           "AAABAAAAAQH/////AAAAAA==";

        private const string LoadByNodeId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEADAAAAExvYWRCeU5vZGVJZAEBYhsALwEBYhtiGwAAAQEBAAAAADUBAQG0EwIAAAAXYKkK" +
           "AgAAAAAADgAAAElucHV0QXJndW1lbnRzAQH9FwAuAET9FwAAlgEAAAABACoBAXUAAAACAAAASWQAEv//" +
           "//8AAAAAAmAAAABFeHBhbmRlZE5vZGVJZCBwb2ludGluZyB0byBhbiBpbnN0YW5jZSBvZiBGaWxlVHlw" +
           "ZSByZXByZXNlbnRpbmcgYSB0YXNrIGNvbnRyb2wgcHJvZ3JhbSBvciBtb2R1bGUBACgBAQAAAAEAAAAB" +
           "AAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQH+FwAuAET+FwAAlgEAAAAB" +
           "ACoBAcEBAAAGAAAAU3RhdHVzAAb/////AAAAAAKoAQAAMCDigJMgT0sg4oCTIEV2ZXJ5dGhpbmcgaXMg" +
           "T0sKMSDigJMgRV9TeXN0ZW1TdGF0ZSDigJMgVGhlIHN5c3RlbSBpcyBub3QgaW4gY29ycmVjdCBzdGF0" +
           "ZSBmb3IgdGhpcyBvcGVyYXRpb24KMiDigJMgRV9VbmV4cGVjdGVkRXJyb3Ig4oCTIFVuZXhwZWN0ZWQg" +
           "RXJyb3IgZHVyaW5nIHRoZSBtZXRob2QgY2FsbAozIOKAkyBFX0FjdGl2ZUFsYXJtIOKAkyBBbiBBY3Rp" +
           "dmUgQWxhcm0gcHJldmVudHMgdGhlIHN5c3RlbSBzdGFydAo0IOKAkyBFX0Fja25vd2xlZGdlUmVxdWly" +
           "ZWQg4oCTIENvbmRpdGlvbiBuZWVkcyB0byBiZSBhY2tub3dsZWRnZWQKPDAg4oCTIHNoYWxsIGJlIHVz" +
           "ZWQgZm9yIHZlbmRvci1zcGVjaWZpYyBlcnJvcnMKPjAg4oCTIGFyZSByZXNlcnZlZCBmb3IgZXJyb3Jz" +
           "IGRlZmluZWQgYnkgdGhpcyBhbmQgZnV0dXJlIHN0YW5kYXJkcwEAKAEBAAAAAQAAAAEAAAABAf////8A" +
           "AAAA";

        private const string PossibleStopModes_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8X" +
           "YKkKAgAAAAEAEQAAAFBvc3NpYmxlU3RvcE1vZGVzAQHUFwAvAD/UFwAAlgUAAAABADsgAWsAAAABAAAA" +
           "AAAAAAMCAAAAZW4GAAAAT25QYXRoAwIAAABlbkcAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIGluIGEg" +
           "Y29udHJvbGxlZCBtYW5uZXIgYWxvbmcgdGhlIHByb2dyYW1tZWQgcGF0aAEAOyABcgAAAAIAAAAAAAAA" +
           "AwIAAABlbgoAAABFbmRPZkN5Y2xlAwIAAABlbkoAAABTdG9wIHByb2dyYW0gZXhlY3V0aW9uIHdoZW4g" +
           "dGhlIGN1cnJlbnQgcHJvZHVjdGlvbiBjeWNsZSBoYXMgYmVlbiBmaW5pc2hlZAEAOyAByQAAAAMAAAAA" +
           "AAAAAwIAAABlbgsAAABQcm9jZXNzU3RvcAMCAAAAZW6gAAAAQXBwbGljYXRpb24gZGVwZW5kZW50IHN0" +
           "b3AgaW5zdHJ1Y3Rpb24gdGhhdCBzdG9wcyBwcm9ncmFtIGV4ZWN1dGlvbiBhdCBhIGZhdm91cmFibGUg" +
           "cG9pbnQgZm9yIHRoZSBhcHBsaWNhdGlvbiwgZS5nLiBhdCB0aGUgZW5kIG9mIGEgcGFpbnQgc3Ryb2tl" +
           "IG9yIHNlYWxpbmcgYmVhZAEAOyABrAAAAAQAAAAAAAAAAwIAAABlbgkAAABRdWlja1N0b3ADAgAAAGVu" +
           "hQAAAFRoaXMgc3RvcCBpcyBwZXJmb3JtZWQgYnkgcmFtcGluZyBkb3duIG1vdGlvbiBhcyBmYXN0IGFz" +
           "IHBvc3NpYmxlIHVzaW5nIG9wdGltdW0gbW90b3IgcGVyZm9ybWFuY2UuIFRoZSByb2JvdCBtYXkgbm90" +
           "IHN0YXkgb24gdGhlIHBhdGgBADsgAYsAAAAFAAAAAAAAAAMCAAAAZW4QAAAARW5kT2ZJbnN0cnVjdGlv" +
           "bgMCAAAAZW5dAAAAVGhpcyBzdG9wIGNhbiBiZSB1c2VkIHRvIHN0b3AgdGhlIHByb2dyYW0gZXhlY3V0" +
           "aW9uIHdoZW4gdGhlIGN1cnJlbnQgaW5zdHJ1Y3Rpb24gaXMgY29tcGxldGVkAQCqHQEAAAABAAAAAAAA" +
           "AAMD/////wAAAAA=";

        private const string ReadySubstateMachine_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEAFAAAAFJlYWR5U3Vic3RhdGVNYWNoaW5lAQGvEwAvAQH0A68TAAACAAAAAHUBAQGxEwAp" +
           "AAEABwkDAAAAFWCJCgIAAAAAAAwAAABDdXJyZW50U3RhdGUBAd0XAC8BAMgK3RcAAAAV/////wEB////" +
           "/wEAAAAVYIkKAgAAAAAAAgAAAElkAQHfFwAuAETfFwAAABH/////AQH/////AAAAABVgiQoCAAAAAAAO" +
           "AAAATGFzdFRyYW5zaXRpb24BAdgXAC8BAM8K2BcAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAA" +
           "AElkAQHZFwAuAETZFwAAABH/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25S" +
           "ZWFzb24BAdoXAC8BAOYr2hcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMB" +
           "AdsXAC4ARNsXAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsA" +
           "AABDYXVzZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0" +
           "ZXJuYWwDAgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAA" +
           "AAMCAAAAZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyAB" +
           "RgAAAAMAAAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3Bl" +
           "Y2lmaWMgYmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENh" +
           "dXNlZCBieSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAA" +
           "ZW4rAAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAA" +
           "AAAAAAABAf////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEB3BcALgBE3BcAABUCBwAAAElu" +
           "dmFsaWQAFf////8BAf////8AAAAA";

        private const string Start_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEABQAAAFN0YXJ0AQFgGwAvAQFZG2AbAAABAQEAAAAANQEBAbYTAQAAABdgqQoCAAAAAAAP" +
           "AAAAT3V0cHV0QXJndW1lbnRzAQHVFwAuAETVFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////" +
           "AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVk" +
           "IGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3Ig" +
           "YXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";

        private const string Stop_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEABAAAAFN0b3ABAWEbAC8BAVobYRsAAAEBAQAAAAA1AQEBtxMCAAAAF2CpCgIAAAAAAA4A" +
           "AABJbnB1dEFyZ3VtZW50cwEB1hcALgBE1hcAAJYBAAAAAQAqAQGtAAAACAAAAFN0b3BNb2RlAAj/////" +
           "AAAAAAKSAAAAcHJvdmlkZXMgYSB3YXkgdG8gZGlmZmVyZW50aWF0ZSBiZXR3ZWVuIGRpZmZlcmVudCBz" +
           "dG9wIG1vZGVzLiBUaGlzIHBhcmFtZXRlciBzaG91bGQgY29ycmVzcG9uZCB0byBvbmUgb2YgdGhlIHZh" +
           "bHVlcyBpbiB0aGUgUG9zc2libGVTdG9wTW9kZXMgYXJyYXkBACgBAQAAAAEAAAABAAAAAQH/////AAAA" +
           "ABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQHXFwAuAETXFwAAlgEAAAABACoBAaMAAAAGAAAA" +
           "U3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMgT0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVy" +
           "cm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwg" +
           "YmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3BlY2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB////" +
           "/wAAAAA=";

        private const string UnloadByName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEADAAAAFVubG9hZEJ5TmFtZQEBZhsALwEBZhtmGwAAAQEBAAAAADUBAQG1EwIAAAAXYKkK" +
           "AgAAAAAADgAAAElucHV0QXJndW1lbnRzAQGzFwAuAESzFwAAlgEAAAABACoBARMAAAAEAAAATmFtZQAM" +
           "/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3Vt" +
           "ZW50cwEBtRcALgBEtRcAAJYBAAAAAQAqAQEVAAAABgAAAFN0YXR1cwAG/////wAAAAAAAQAoAQEAAAAB" +
           "AAAAAQAAAAEB/////wAAAAA=";

        private const string UnloadByNodeId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEADgAAAFVubG9hZEJ5Tm9kZUlkAQFlGwAvAQFlG2UbAAABAQEAAAAANQEBAbUTAgAAABdg" +
           "qQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAQIYAC4ARAIYAACWAQAAAAEAKgEBQQAAAAIAAABJZAAS" +
           "/////wAAAAACLAAAAEV4cGFuZGVkIE5vZGVJZCBvZiB0aGUgbW9kdWxlIHRvIGJlIHVubG9hZGVkAQAo" +
           "AQEAAAABAAAAAQAAAAEB/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBAxgALgBE" +
           "AxgAAJYBAAAAAQAqAQHBAQAABgAAAFN0YXR1cwAG/////wAAAAACqAEAADAg4oCTIE9LIOKAkyBFdmVy" +
           "eXRoaW5nIGlzIE9LCjEg4oCTIEVfU3lzdGVtU3RhdGUg4oCTIFRoZSBzeXN0ZW0gaXMgbm90IGluIGNv" +
           "cnJlY3Qgc3RhdGUgZm9yIHRoaXMgb3BlcmF0aW9uCjIg4oCTIEVfVW5leHBlY3RlZEVycm9yIOKAkyBV" +
           "bmV4cGVjdGVkIEVycm9yIGR1cmluZyB0aGUgbWV0aG9kIGNhbGwKMyDigJMgRV9BY3RpdmVBbGFybSDi" +
           "gJMgQW4gQWN0aXZlIEFsYXJtIHByZXZlbnRzIHRoZSBzeXN0ZW0gc3RhcnQKNCDigJMgRV9BY2tub3ds" +
           "ZWRnZVJlcXVpcmVkIOKAkyBDb25kaXRpb24gbmVlZHMgdG8gYmUgYWNrbm93bGVkZ2VkCjwwIOKAkyBz" +
           "aGFsbCBiZSB1c2VkIGZvciB2ZW5kb3Itc3BlY2lmaWMgZXJyb3JzCj4wIOKAkyBhcmUgcmVzZXJ2ZWQg" +
           "Zm9yIGVycm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMBACgBAQAAAAEAAAAB" +
           "AAAAAQH/////AAAAAA==";

        private const string UnloadProgram_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEADQAAAFVubG9hZFByb2dyYW0BAWQbAC8BAWQbZBsAAAEBAQAAAAA1AQEBtRMBAAAAF2Cp" +
           "CgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQEYAC4ARAEYAACWAQAAAAEAKgEBwQEAAAYAAABTdGF0" +
           "dXMABv////8AAAAAAqgBAAAwIOKAkyBPSyDigJMgRXZlcnl0aGluZyBpcyBPSwoxIOKAkyBFX1N5c3Rl" +
           "bVN0YXRlIOKAkyBUaGUgc3lzdGVtIGlzIG5vdCBpbiBjb3JyZWN0IHN0YXRlIGZvciB0aGlzIG9wZXJh" +
           "dGlvbgoyIOKAkyBFX1VuZXhwZWN0ZWRFcnJvciDigJMgVW5leHBlY3RlZCBFcnJvciBkdXJpbmcgdGhl" +
           "IG1ldGhvZCBjYWxsCjMg4oCTIEVfQWN0aXZlQWxhcm0g4oCTIEFuIEFjdGl2ZSBBbGFybSBwcmV2ZW50" +
           "cyB0aGUgc3lzdGVtIHN0YXJ0CjQg4oCTIEVfQWNrbm93bGVkZ2VSZXF1aXJlZCDigJMgQ29uZGl0aW9u" +
           "IG5lZWRzIHRvIGJlIGFja25vd2xlZGdlZAo8MCDigJMgc2hhbGwgYmUgdXNlZCBmb3IgdmVuZG9yLXNw" +
           "ZWNpZmljIGVycm9ycwo+MCDigJMgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5lZCBieSB0aGlz" +
           "IGFuZCBmdXR1cmUgc3RhbmRhcmRzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAIwAAAFRhc2tDb250cm9sU3RhdGVNYWNoaW5lVHlwZUluc3RhbmNlAQEBBAEBAQQBBAAA" +
           "AQAAAAApAAEABwkWAAAAFWCJCAIAAAAAAAwAAABDdXJyZW50U3RhdGUBAQAAAC8BAMgKABX/////AQH/" +
           "////AQAAABVgiQgCAAAAAAACAAAASWQBAQAAAC4ARAAR/////wEB/////wAAAAAVYIkKAgAAAAAADgAA" +
           "AExhc3RUcmFuc2l0aW9uAQHPFwAvAQDPCs8XAAAAFf////8BAf////8BAAAAFWCJCgIAAAAAAAIAAABJ" +
           "ZAEB0BcALgBE0BcAAAAR/////wEB/////wAAAAAVYIkKAgAAAAEAGQAAAENvbmZpZ3VyZWREZWZhdWx0" +
           "U3RvcE1vZGUBAc4XAC8AP84XAAAABP////8DA/////8AAAAAJGCACgEAAAABAAkAAABFeGVjdXRpbmcB" +
           "AbITAwAAAAAmAAAARW50aXR5IGlzIGluIGEgY29uZGl0aW9uIG9mIGV4ZWN1dGlvbi4ALwEAAwmyEwAA" +
           "AwAAAAAzAQEBuBMAMwEBAbcTADQBAQG2EwEAAAAVYKkKAgAAAAAACwAAAFN0YXRlTnVtYmVyAQHiFwAu" +
           "AETiFwAABwMAAAAAB/////8BAf////8AAAAAJGCACgEAAAABAA8AAABFeGVjdXRpbmdUb0lkbGUBAbgT" +
           "AwAAAAAeAAAAQ2hhbmdlcyBmcm9tIEV4ZWN1dGluZyB0byBJZGxlAC8BAAYJuBMAAAMAAAAAMwABAbIT" +
           "ADQAAQGwEwA2AAEABwkBAAAAFWCpCgIAAAAAABAAAABUcmFuc2l0aW9uTnVtYmVyAQHqFwAuAETqFwAA" +
           "BwYAAAAAB/////8BAf////8AAAAAJGCACgEAAAABABAAAABFeGVjdXRpbmdUb1JlYWR5AQG3EwMAAAAA" +
           "HwAAAENoYW5nZXMgZnJvbSBFeGVjdXRpbmcgdG8gUmVhZHkALwEABgm3EwAABAAAAAAzAAEBshMANAAB" +
           "AbETADUAAQFhGwA2AAEABwkBAAAAFWCpCgIAAAAAABAAAABUcmFuc2l0aW9uTnVtYmVyAQHpFwAuAETp" +
           "FwAABwUAAAAAB/////8BAf////8AAAAAJGCACgEAAAABAAQAAABJZGxlAQGwEwMAAAAAMAAAAEVudGl0" +
           "eSBpcyBub3QgaW4gYSBjb25kaXRpb24gdG8gc3RhcnQgZXhlY3V0aW9uLgAvAQADCbATAAAFAAAAADQB" +
           "AQG4EwAzAQEBsxMANAEBAbMTADMBAQG0EwA0AQEBtRMBAAAAFWCpCgIAAAAAAAsAAABTdGF0ZU51bWJl" +
           "cgEB4BcALgBE4BcAAAcBAAAAAAf/////AQH/////AAAAACRggAoBAAAAAQAKAAAASWRsZVRvSWRsZQEB" +
           "sxMDAAAAABoAAABDaGFuZ2VzIGZyb20gSWRsZSB0byBJZGxlLgAvAQAGCbMTAAADAAAAADMAAQGwEwA0" +
           "AAEBsBMANgABAAcJAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51bWJlcgEB4xcALgBE4xcAAAcB" +
           "AAAAAAf/////AQH/////AAAAACRggAoBAAAAAQALAAAASWRsZVRvUmVhZHkBAbQTAwAAAAAaAAAAQ2hh" +
           "bmdlcyBmcm9tIElkbGUgdG8gUmVhZHkALwEABgm0EwAABQAAAAAzAAEBsBMANAABAbETADYAAQAHCQA1" +
           "AAEBYxsANQABAWIbAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51bWJlcgEB5BcALgBE5BcAAAcC" +
           "AAAAAAf/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25SZWFzb24BAdEXAC8B" +
           "AOYr0RcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMBAdIXAC4ARNIXAACW" +
           "BgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsAAABDYXVzZWQgYnkg" +
           "YW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0ZXJuYWwDAgAAAGVu" +
           "HAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAAAAMCAAAAZW4GAAAA" +
           "RGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyABRgAAAAMAAAAAAAAA" +
           "AwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3BlY2lmaWMgYmVoYXZp" +
           "b3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENhdXNlZCBieSBhbiBl" +
           "cnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAAZW4rAAAAQ2F1c2Vk" +
           "IGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAAAAAAAAABAf////8A" +
           "AAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEB0xcALgBE0xcAABUCBwAAAEludmFsaWQAFf////8B" +
           "Af////8AAAAAF2CpCgIAAAABABEAAABQb3NzaWJsZVN0b3BNb2RlcwEB1BcALwA/1BcAAJYFAAAAAQA7" +
           "IAFrAAAAAQAAAAAAAAADAgAAAGVuBgAAAE9uUGF0aAMCAAAAZW5HAAAAU3RvcCBwcm9ncmFtIGV4ZWN1" +
           "dGlvbiBpbiBhIGNvbnRyb2xsZWQgbWFubmVyIGFsb25nIHRoZSBwcm9ncmFtbWVkIHBhdGgBADsgAXIA" +
           "AAACAAAAAAAAAAMCAAAAZW4KAAAARW5kT2ZDeWNsZQMCAAAAZW5KAAAAU3RvcCBwcm9ncmFtIGV4ZWN1" +
           "dGlvbiB3aGVuIHRoZSBjdXJyZW50IHByb2R1Y3Rpb24gY3ljbGUgaGFzIGJlZW4gZmluaXNoZWQBADsg" +
           "AckAAAADAAAAAAAAAAMCAAAAZW4LAAAAUHJvY2Vzc1N0b3ADAgAAAGVuoAAAAEFwcGxpY2F0aW9uIGRl" +
           "cGVuZGVudCBzdG9wIGluc3RydWN0aW9uIHRoYXQgc3RvcHMgcHJvZ3JhbSBleGVjdXRpb24gYXQgYSBm" +
           "YXZvdXJhYmxlIHBvaW50IGZvciB0aGUgYXBwbGljYXRpb24sIGUuZy4gYXQgdGhlIGVuZCBvZiBhIHBh" +
           "aW50IHN0cm9rZSBvciBzZWFsaW5nIGJlYWQBADsgAawAAAAEAAAAAAAAAAMCAAAAZW4JAAAAUXVpY2tT" +
           "dG9wAwIAAABlboUAAABUaGlzIHN0b3AgaXMgcGVyZm9ybWVkIGJ5IHJhbXBpbmcgZG93biBtb3Rpb24g" +
           "YXMgZmFzdCBhcyBwb3NzaWJsZSB1c2luZyBvcHRpbXVtIG1vdG9yIHBlcmZvcm1hbmNlLiBUaGUgcm9i" +
           "b3QgbWF5IG5vdCBzdGF5IG9uIHRoZSBwYXRoAQA7IAGLAAAABQAAAAAAAAADAgAAAGVuEAAAAEVuZE9m" +
           "SW5zdHJ1Y3Rpb24DAgAAAGVuXQAAAFRoaXMgc3RvcCBjYW4gYmUgdXNlZCB0byBzdG9wIHRoZSBwcm9n" +
           "cmFtIGV4ZWN1dGlvbiB3aGVuIHRoZSBjdXJyZW50IGluc3RydWN0aW9uIGlzIGNvbXBsZXRlZAEAqh0B" +
           "AAAAAQAAAAAAAAADA/////8AAAAAJGCACgEAAAABAAUAAABSZWFkeQEBsRMDAAAAACwAAABFbnRpdHkg" +
           "aXMgaW4gYSBjb25kaXRpb24gdG8gc3RhcnQgZXhlY3V0aW9uLgAvAQADCbETAAAFAAAAADQBAQG3EwA0" +
           "AQEBtBMAMwEBAbYTADMBAQG1EwB1AAEBrxMBAAAAFWCpCgIAAAAAAAsAAABTdGF0ZU51bWJlcgEB4RcA" +
           "LgBE4RcAAAcCAAAAAAf/////AQH/////AAAAACRggAoBAAAAAQAQAAAAUmVhZHlUb0V4ZWN1dGluZwEB" +
           "thMDAAAAAB8AAABDaGFuZ2VzIGZyb20gUmVhZHkgdG8gRXhlY3V0aW5nAC8BAAYJthMAAAQAAAAANAAB" +
           "AbITADMAAQGxEwA1AAEBYBsANgABAAcJAQAAABVgqQoCAAAAAAAQAAAAVHJhbnNpdGlvbk51bWJlcgEB" +
           "6BcALgBE6BcAAAcEAAAAAAf/////AQH/////AAAAACRggAoBAAAAAQALAAAAUmVhZHlUb0lkbGUBAbUT" +
           "AwAAAAAaAAAAQ2hhbmdlcyBmcm9tIFJlYWR5IHRvIElkbGUALwEABgm1EwAABgAAAAA0AAEBsBMAMwAB" +
           "AbETADYAAQAHCQA1AAEBZhsANQABAWUbADUAAQFkGwEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRpb25O" +
           "dW1iZXIBAecXAC4AROcXAAAHAwAAAAAH/////wEB/////wAAAAAEYYIKBAAAAAEABQAAAFN0YXJ0AQFg" +
           "GwAvAQFZG2AbAAABAQEAAAAANQEBAbYTAQAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQHV" +
           "FwAuAETVFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMgT0sKVmFs" +
           "dWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1dHVyZSBz" +
           "dGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3BlY2lmaWMg" +
           "ZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEABAAAAFN0b3ABAWEbAC8BAVob" +
           "YRsAAAEBAQAAAAA1AQEBtxMCAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEB1hcALgBE1hcA" +
           "AJYBAAAAAQAqAQGtAAAACAAAAFN0b3BNb2RlAAj/////AAAAAAKSAAAAcHJvdmlkZXMgYSB3YXkgdG8g" +
           "ZGlmZmVyZW50aWF0ZSBiZXR3ZWVuIGRpZmZlcmVudCBzdG9wIG1vZGVzLiBUaGlzIHBhcmFtZXRlciBz" +
           "aG91bGQgY29ycmVzcG9uZCB0byBvbmUgb2YgdGhlIHZhbHVlcyBpbiB0aGUgUG9zc2libGVTdG9wTW9k" +
           "ZXMgYXJyYXkBACgBAQAAAAEAAAABAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1l" +
           "bnRzAQHXFwAuAETXFwAAlgEAAAABACoBAaMAAAAGAAAAU3RhdHVzAAb/////AAAAAAKKAAAAMCDigJMg" +
           "T0sKVmFsdWVzID4gMCBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVkIGJ5IHRoaXMgYW5kIGZ1" +
           "dHVyZSBzdGFuZGFyZHMuClZhbHVlcyA8IDAgc2hhbGwgYmUgdXNlZCBmb3IgYXBwbGljYXRpb24tc3Bl" +
           "Y2lmaWMgZXJyb3JzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAEACgAAAExvYWRCeU5h" +
           "bWUBAWMbAC8BAWMbYxsAAAEBAQAAAAA1AQEBtBMCAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50" +
           "cwEB/xcALgBE/xcAAJYBAAAAAQAqAQFIAAAABAAAAE5hbWUADP////8AAAAAAjEAAABOYW1lIHRvIGlk" +
           "ZW50aWZ5IGEgdGFzayBjb250cm9sIHByb2dyYW0gb3IgbW9kdWxlAQAoAQEAAAABAAAAAQAAAAEB////" +
           "/wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBABgALgBEABgAAJYBAAAAAQAqAQHBAQAA" +
           "BgAAAFN0YXR1cwAG/////wAAAAACqAEAADAg4oCTIE9LIOKAkyBFdmVyeXRoaW5nIGlzIE9LCjEg4oCT" +
           "IEVfU3lzdGVtU3RhdGUg4oCTIFRoZSBzeXN0ZW0gaXMgbm90IGluIGNvcnJlY3Qgc3RhdGUgZm9yIHRo" +
           "aXMgb3BlcmF0aW9uCjIg4oCTIEVfVW5leHBlY3RlZEVycm9yIOKAkyBVbmV4cGVjdGVkIEVycm9yIGR1" +
           "cmluZyB0aGUgbWV0aG9kIGNhbGwKMyDigJMgRV9BY3RpdmVBbGFybSDigJMgQW4gQWN0aXZlIEFsYXJt" +
           "IHByZXZlbnRzIHRoZSBzeXN0ZW0gc3RhcnQKNCDigJMgRV9BY2tub3dsZWRnZVJlcXVpcmVkIOKAkyBD" +
           "b25kaXRpb24gbmVlZHMgdG8gYmUgYWNrbm93bGVkZ2VkCjwwIOKAkyBzaGFsbCBiZSB1c2VkIGZvciB2" +
           "ZW5kb3Itc3BlY2lmaWMgZXJyb3JzCj4wIOKAkyBhcmUgcmVzZXJ2ZWQgZm9yIGVycm9ycyBkZWZpbmVk" +
           "IGJ5IHRoaXMgYW5kIGZ1dHVyZSBzdGFuZGFyZHMBACgBAQAAAAEAAAABAAAAAQH/////AAAAAARhggoE" +
           "AAAAAQAMAAAATG9hZEJ5Tm9kZUlkAQFiGwAvAQFiG2IbAAABAQEAAAAANQEBAbQTAgAAABdgqQoCAAAA" +
           "AAAOAAAASW5wdXRBcmd1bWVudHMBAf0XAC4ARP0XAACWAQAAAAEAKgEBdQAAAAIAAABJZAAS/////wAA" +
           "AAACYAAAAEV4cGFuZGVkTm9kZUlkIHBvaW50aW5nIHRvIGFuIGluc3RhbmNlIG9mIEZpbGVUeXBlIHJl" +
           "cHJlc2VudGluZyBhIHRhc2sgY29udHJvbCBwcm9ncmFtIG9yIG1vZHVsZQEAKAEBAAAAAQAAAAEAAAAB" +
           "Af////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAf4XAC4ARP4XAACWAQAAAAEAKgEB" +
           "wQEAAAYAAABTdGF0dXMABv////8AAAAAAqgBAAAwIOKAkyBPSyDigJMgRXZlcnl0aGluZyBpcyBPSwox" +
           "IOKAkyBFX1N5c3RlbVN0YXRlIOKAkyBUaGUgc3lzdGVtIGlzIG5vdCBpbiBjb3JyZWN0IHN0YXRlIGZv" +
           "ciB0aGlzIG9wZXJhdGlvbgoyIOKAkyBFX1VuZXhwZWN0ZWRFcnJvciDigJMgVW5leHBlY3RlZCBFcnJv" +
           "ciBkdXJpbmcgdGhlIG1ldGhvZCBjYWxsCjMg4oCTIEVfQWN0aXZlQWxhcm0g4oCTIEFuIEFjdGl2ZSBB" +
           "bGFybSBwcmV2ZW50cyB0aGUgc3lzdGVtIHN0YXJ0CjQg4oCTIEVfQWNrbm93bGVkZ2VSZXF1aXJlZCDi" +
           "gJMgQ29uZGl0aW9uIG5lZWRzIHRvIGJlIGFja25vd2xlZGdlZAo8MCDigJMgc2hhbGwgYmUgdXNlZCBm" +
           "b3IgdmVuZG9yLXNwZWNpZmljIGVycm9ycwo+MCDigJMgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVm" +
           "aW5lZCBieSB0aGlzIGFuZCBmdXR1cmUgc3RhbmRhcmRzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAE" +
           "YIAKAQAAAAEAFAAAAFJlYWR5U3Vic3RhdGVNYWNoaW5lAQGvEwAvAQH0A68TAAACAAAAAHUBAQGxEwAp" +
           "AAEABwkDAAAAFWCJCgIAAAAAAAwAAABDdXJyZW50U3RhdGUBAd0XAC8BAMgK3RcAAAAV/////wEB////" +
           "/wEAAAAVYIkKAgAAAAAAAgAAAElkAQHfFwAuAETfFwAAABH/////AQH/////AAAAABVgiQoCAAAAAAAO" +
           "AAAATGFzdFRyYW5zaXRpb24BAdgXAC8BAM8K2BcAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAA" +
           "AElkAQHZFwAuAETZFwAAABH/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25S" +
           "ZWFzb24BAdoXAC8BAOYr2hcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMB" +
           "AdsXAC4ARNsXAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsA" +
           "AABDYXVzZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0" +
           "ZXJuYWwDAgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAA" +
           "AAMCAAAAZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyAB" +
           "RgAAAAMAAAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3Bl" +
           "Y2lmaWMgYmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENh" +
           "dXNlZCBieSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAA" +
           "ZW4rAAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAA" +
           "AAAAAAABAf////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEB3BcALgBE3BcAABUCBwAAAElu" +
           "dmFsaWQAFf////8BAf////8AAAAABGGCCgQAAAABAAwAAABVbmxvYWRCeU5hbWUBAWYbAC8BAWYbZhsA" +
           "AAEBAQAAAAA1AQEBtRMCAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBsxcALgBEsxcAAJYB" +
           "AAAAAQAqAQETAAAABAAAAE5hbWUADP////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAAF2Cp" +
           "CgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAbUXAC4ARLUXAACWAQAAAAEAKgEBFQAAAAYAAABTdGF0" +
           "dXMABv////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAABAA4AAABVbmxvYWRC" +
           "eU5vZGVJZAEBZRsALwEBZRtlGwAAAQEBAAAAADUBAQG1EwIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJn" +
           "dW1lbnRzAQECGAAuAEQCGAAAlgEAAAABACoBAUEAAAACAAAASWQAEv////8AAAAAAiwAAABFeHBhbmRl" +
           "ZCBOb2RlSWQgb2YgdGhlIG1vZHVsZSB0byBiZSB1bmxvYWRlZAEAKAEBAAAAAQAAAAEAAAABAf////8A" +
           "AAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQMYAC4ARAMYAACWAQAAAAEAKgEBwQEAAAYA" +
           "AABTdGF0dXMABv////8AAAAAAqgBAAAwIOKAkyBPSyDigJMgRXZlcnl0aGluZyBpcyBPSwoxIOKAkyBF" +
           "X1N5c3RlbVN0YXRlIOKAkyBUaGUgc3lzdGVtIGlzIG5vdCBpbiBjb3JyZWN0IHN0YXRlIGZvciB0aGlz" +
           "IG9wZXJhdGlvbgoyIOKAkyBFX1VuZXhwZWN0ZWRFcnJvciDigJMgVW5leHBlY3RlZCBFcnJvciBkdXJp" +
           "bmcgdGhlIG1ldGhvZCBjYWxsCjMg4oCTIEVfQWN0aXZlQWxhcm0g4oCTIEFuIEFjdGl2ZSBBbGFybSBw" +
           "cmV2ZW50cyB0aGUgc3lzdGVtIHN0YXJ0CjQg4oCTIEVfQWNrbm93bGVkZ2VSZXF1aXJlZCDigJMgQ29u" +
           "ZGl0aW9uIG5lZWRzIHRvIGJlIGFja25vd2xlZGdlZAo8MCDigJMgc2hhbGwgYmUgdXNlZCBmb3IgdmVu" +
           "ZG9yLXNwZWNpZmljIGVycm9ycwo+MCDigJMgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5lZCBi" +
           "eSB0aGlzIGFuZCBmdXR1cmUgc3RhbmRhcmRzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAA" +
           "AAEADQAAAFVubG9hZFByb2dyYW0BAWQbAC8BAWQbZBsAAAEBAQAAAAA1AQEBtRMBAAAAF2CpCgIAAAAA" +
           "AA8AAABPdXRwdXRBcmd1bWVudHMBAQEYAC4ARAEYAACWAQAAAAEAKgEBwQEAAAYAAABTdGF0dXMABv//" +
           "//8AAAAAAqgBAAAwIOKAkyBPSyDigJMgRXZlcnl0aGluZyBpcyBPSwoxIOKAkyBFX1N5c3RlbVN0YXRl" +
           "IOKAkyBUaGUgc3lzdGVtIGlzIG5vdCBpbiBjb3JyZWN0IHN0YXRlIGZvciB0aGlzIG9wZXJhdGlvbgoy" +
           "IOKAkyBFX1VuZXhwZWN0ZWRFcnJvciDigJMgVW5leHBlY3RlZCBFcnJvciBkdXJpbmcgdGhlIG1ldGhv" +
           "ZCBjYWxsCjMg4oCTIEVfQWN0aXZlQWxhcm0g4oCTIEFuIEFjdGl2ZSBBbGFybSBwcmV2ZW50cyB0aGUg" +
           "c3lzdGVtIHN0YXJ0CjQg4oCTIEVfQWNrbm93bGVkZ2VSZXF1aXJlZCDigJMgQ29uZGl0aW9uIG5lZWRz" +
           "IHRvIGJlIGFja25vd2xlZGdlZAo8MCDigJMgc2hhbGwgYmUgdXNlZCBmb3IgdmVuZG9yLXNwZWNpZmlj" +
           "IGVycm9ycwo+MCDigJMgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5lZCBieSB0aGlzIGFuZCBm" +
           "dXR1cmUgc3RhbmRhcmRzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public LoadByNameMethodState LoadByName
        {
            get => m_loadByNameMethod;

            set
            {
                if (!Object.ReferenceEquals(m_loadByNameMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_loadByNameMethod = value;
            }
        }

        public LoadByNodeIdMethodState LoadByNodeId
        {
            get => m_loadByNodeIdMethod;

            set
            {
                if (!Object.ReferenceEquals(m_loadByNodeIdMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_loadByNodeIdMethod = value;
            }
        }

        public ReadySubstateMachineTypeState ReadySubstateMachine
        {
            get => m_readySubstateMachine;

            set
            {
                if (!Object.ReferenceEquals(m_readySubstateMachine, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_readySubstateMachine = value;
            }
        }

        public UnloadByNameMethodState UnloadByName
        {
            get => m_unloadByNameMethod;

            set
            {
                if (!Object.ReferenceEquals(m_unloadByNameMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_unloadByNameMethod = value;
            }
        }

        public UnloadByNodeIdMethodState UnloadByNodeId
        {
            get => m_unloadByNodeIdMethod;

            set
            {
                if (!Object.ReferenceEquals(m_unloadByNodeIdMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_unloadByNodeIdMethod = value;
            }
        }

        public UnloadProgramMethodState UnloadProgram
        {
            get => m_unloadProgramMethod;

            set
            {
                if (!Object.ReferenceEquals(m_unloadProgramMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_unloadProgramMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_loadByNameMethod != null)
            {
                children.Add(m_loadByNameMethod);
            }

            if (m_loadByNodeIdMethod != null)
            {
                children.Add(m_loadByNodeIdMethod);
            }

            if (m_readySubstateMachine != null)
            {
                children.Add(m_readySubstateMachine);
            }

            if (m_unloadByNameMethod != null)
            {
                children.Add(m_unloadByNameMethod);
            }

            if (m_unloadByNodeIdMethod != null)
            {
                children.Add(m_unloadByNodeIdMethod);
            }

            if (m_unloadProgramMethod != null)
            {
                children.Add(m_unloadProgramMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_loadByNameMethod, child))
            {
                m_loadByNameMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_loadByNodeIdMethod, child))
            {
                m_loadByNodeIdMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_readySubstateMachine, child))
            {
                m_readySubstateMachine = null;
                return;
            }

            if (Object.ReferenceEquals(m_unloadByNameMethod, child))
            {
                m_unloadByNameMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_unloadByNodeIdMethod, child))
            {
                m_unloadByNodeIdMethod = null;
                return;
            }

            if (Object.ReferenceEquals(m_unloadProgramMethod, child))
            {
                m_unloadProgramMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.LoadByName:
                {
                    if (createOrReplace)
                    {
                        if (LoadByName == null)
                        {
                            if (replacement == null)
                            {
                                LoadByName = new LoadByNameMethodState(this);
                            }
                            else
                            {
                                LoadByName = (LoadByNameMethodState)replacement;
                            }
                        }
                    }

                    instance = LoadByName;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.LoadByNodeId:
                {
                    if (createOrReplace)
                    {
                        if (LoadByNodeId == null)
                        {
                            if (replacement == null)
                            {
                                LoadByNodeId = new LoadByNodeIdMethodState(this);
                            }
                            else
                            {
                                LoadByNodeId = (LoadByNodeIdMethodState)replacement;
                            }
                        }
                    }

                    instance = LoadByNodeId;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.ReadySubstateMachine:
                {
                    if (createOrReplace)
                    {
                        if (ReadySubstateMachine == null)
                        {
                            if (replacement == null)
                            {
                                ReadySubstateMachine = new ReadySubstateMachineTypeState(this);
                            }
                            else
                            {
                                ReadySubstateMachine = (ReadySubstateMachineTypeState)replacement;
                            }
                        }
                    }

                    instance = ReadySubstateMachine;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.UnloadByName:
                {
                    if (createOrReplace)
                    {
                        if (UnloadByName == null)
                        {
                            if (replacement == null)
                            {
                                UnloadByName = new UnloadByNameMethodState(this);
                            }
                            else
                            {
                                UnloadByName = (UnloadByNameMethodState)replacement;
                            }
                        }
                    }

                    instance = UnloadByName;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.UnloadByNodeId:
                {
                    if (createOrReplace)
                    {
                        if (UnloadByNodeId == null)
                        {
                            if (replacement == null)
                            {
                                UnloadByNodeId = new UnloadByNodeIdMethodState(this);
                            }
                            else
                            {
                                UnloadByNodeId = (UnloadByNodeIdMethodState)replacement;
                            }
                        }
                    }

                    instance = UnloadByNodeId;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.UnloadProgram:
                {
                    if (createOrReplace)
                    {
                        if (UnloadProgram == null)
                        {
                            if (replacement == null)
                            {
                                UnloadProgram = new UnloadProgramMethodState(this);
                            }
                            else
                            {
                                UnloadProgram = (UnloadProgramMethodState)replacement;
                            }
                        }
                    }

                    instance = UnloadProgram;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private LoadByNameMethodState m_loadByNameMethod;
        private LoadByNodeIdMethodState m_loadByNodeIdMethod;
        private ReadySubstateMachineTypeState m_readySubstateMachine;
        private UnloadByNameMethodState m_unloadByNameMethod;
        private UnloadByNodeIdMethodState m_unloadByNodeIdMethod;
        private UnloadProgramMethodState m_unloadProgramMethod;
        #endregion
    }
    #endif
    #endregion

    #region ReadySubstateMachineTypeState Class
    #if (!OPCUA_EXCLUDE_ReadySubstateMachineTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class ReadySubstateMachineTypeState : FiniteStateMachineState
    {
        #region Constructors
        public ReadySubstateMachineTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.ReadySubstateMachineType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ResetToProgramStart != null)
            {
                ResetToProgramStart.Initialize(context, ResetToProgramStart_InitializationString);
            }
        }

        #region Initialization String
        private const string ResetToProgramStart_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIKBAAAAAEAEwAAAFJlc2V0VG9Qcm9ncmFtU3RhcnQBAVsbAC8BAVsbWxsAAAEBAQAAAAA1AQEBohMB" +
           "AAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAaUXAC4ARKUXAACWAQAAAAEAKgEBwQEAAAYA" +
           "AABTdGF0dXMABv////8AAAAAAqgBAAAwIOKAkyBPSyDigJMgRXZlcnl0aGluZyBpcyBPSwoxIOKAkyBF" +
           "X1N5c3RlbVN0YXRlIOKAkyBUaGUgc3lzdGVtIGlzIG5vdCBpbiBjb3JyZWN0IHN0YXRlIGZvciB0aGlz" +
           "IG9wZXJhdGlvbgoyIOKAkyBFX1VuZXhwZWN0ZWRFcnJvciDigJMgVW5leHBlY3RlZCBFcnJvciBkdXJp" +
           "bmcgdGhlIG1ldGhvZCBjYWxsCjMg4oCTIEVfQWN0aXZlQWxhcm0g4oCTIEFuIEFjdGl2ZSBBbGFybSBw" +
           "cmV2ZW50cyB0aGUgc3lzdGVtIHN0YXJ0CjQg4oCTIEVfQWNrbm93bGVkZ2VSZXF1aXJlZCDigJMgQ29u" +
           "ZGl0aW9uIG5lZWRzIHRvIGJlIGFja25vd2xlZGdlZAo8MCDigJMgc2hhbGwgYmUgdXNlZCBmb3IgdmVu" +
           "ZG9yLXNwZWNpZmljIGVycm9ycwo+MCDigJMgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5lZCBi" +
           "eSB0aGlzIGFuZCBmdXR1cmUgc3RhbmRhcmRzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAIAAAAFJlYWR5U3Vic3RhdGVNYWNoaW5lVHlwZUluc3RhbmNlAQH0AwEB9AP0AwAAAQAA" +
           "AAApAAEABwkIAAAAFWCJCAIAAAAAAAwAAABDdXJyZW50U3RhdGUBAQAAAC8BAMgKABX/////AQH/////" +
           "AQAAABVgiQgCAAAAAAACAAAASWQBAQAAAC4ARAAR/////wEB/////wAAAAAVYIkKAgAAAAAADgAAAExh" +
           "c3RUcmFuc2l0aW9uAQGcFwAvAQDPCpwXAAAAFf////8BAf////8BAAAAFWCJCgIAAAAAAAIAAABJZAEB" +
           "nRcALgBEnRcAAAAR/////wEB/////wAAAAAEYIAKAQAAAAEADgAAAEF0UHJvZ3JhbVN0YXJ0AQGfEwAv" +
           "AQADCZ8TAAACAAAAADMBAQGhEwA0AQEBohMBAAAAFWCpCgIAAAAAAAsAAABTdGF0ZU51bWJlcgEBoRcA" +
           "LgBEoRcAAAcBAAAAAAf/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25SZWFz" +
           "b24BAZ4XAC8BAOYrnhcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMBAZ8X" +
           "AC4ARJ8XAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsAAABD" +
           "YXVzZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0ZXJu" +
           "YWwDAgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAAAAMC" +
           "AAAAZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyABRgAA" +
           "AAMAAAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3BlY2lm" +
           "aWMgYmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENhdXNl" +
           "ZCBieSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAAZW4r" +
           "AAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAAAAAA" +
           "AAABAf////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEBoBcALgBEoBcAABUCBwAAAEludmFs" +
           "aWQAFf////8BAf////8AAAAABGCACgEAAAABABcAAABQcm9ncmFtU3RhcnRUb1N1c3BlbmRlZAEBoRMA" +
           "LwEABgmhEwAAAwAAAAAzAAEBnxMANAABAaATADYAAQAHCQEAAAAVYKkKAgAAAAAAEAAAAFRyYW5zaXRp" +
           "b25OdW1iZXIBAaMXAC4ARKMXAAAHAQAAAAAH/////wEB/////wAAAAAEYYIKBAAAAAEAEwAAAFJlc2V0" +
           "VG9Qcm9ncmFtU3RhcnQBAVsbAC8BAVsbWxsAAAEBAQAAAAA1AQEBohMBAAAAF2CpCgIAAAAAAA8AAABP" +
           "dXRwdXRBcmd1bWVudHMBAaUXAC4ARKUXAACWAQAAAAEAKgEBwQEAAAYAAABTdGF0dXMABv////8AAAAA" +
           "AqgBAAAwIOKAkyBPSyDigJMgRXZlcnl0aGluZyBpcyBPSwoxIOKAkyBFX1N5c3RlbVN0YXRlIOKAkyBU" +
           "aGUgc3lzdGVtIGlzIG5vdCBpbiBjb3JyZWN0IHN0YXRlIGZvciB0aGlzIG9wZXJhdGlvbgoyIOKAkyBF" +
           "X1VuZXhwZWN0ZWRFcnJvciDigJMgVW5leHBlY3RlZCBFcnJvciBkdXJpbmcgdGhlIG1ldGhvZCBjYWxs" +
           "CjMg4oCTIEVfQWN0aXZlQWxhcm0g4oCTIEFuIEFjdGl2ZSBBbGFybSBwcmV2ZW50cyB0aGUgc3lzdGVt" +
           "IHN0YXJ0CjQg4oCTIEVfQWNrbm93bGVkZ2VSZXF1aXJlZCDigJMgQ29uZGl0aW9uIG5lZWRzIHRvIGJl" +
           "IGFja25vd2xlZGdlZAo8MCDigJMgc2hhbGwgYmUgdXNlZCBmb3IgdmVuZG9yLXNwZWNpZmljIGVycm9y" +
           "cwo+MCDigJMgYXJlIHJlc2VydmVkIGZvciBlcnJvcnMgZGVmaW5lZCBieSB0aGlzIGFuZCBmdXR1cmUg" +
           "c3RhbmRhcmRzAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAEYIAKAQAAAAEACQAAAFN1c3BlbmRlZAEB" +
           "oBMALwEAAwmgEwAAAgAAAAA0AQEBoRMAMwEBAaITAQAAABVgqQoCAAAAAAALAAAAU3RhdGVOdW1iZXIB" +
           "AaIXAC4ARKIXAAAHAgAAAAAH/////wEB/////wAAAAAEYIAKAQAAAAEAFwAAAFN1c3BlbmRlZFRvUHJv" +
           "Z3JhbVN0YXJ0AQGiEwAvAQAGCaITAAAEAAAAADQAAQGfEwA1AAEBWxsAMwABAaATADYAAQAHCQEAAAAV" +
           "YKkKAgAAAAAAEAAAAFRyYW5zaXRpb25OdW1iZXIBAaQXAC4ARKQXAAAHAgAAAAAH/////wEB/////wAA" +
           "AAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public MultiStateValueDiscreteState<short> LastTransitionReason
        {
            get => m_lastTransitionReason;

            set
            {
                if (!Object.ReferenceEquals(m_lastTransitionReason, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_lastTransitionReason = value;
            }
        }

        public ResetToProgramStartMethodState ResetToProgramStart
        {
            get => m_resetToProgramStartMethod;

            set
            {
                if (!Object.ReferenceEquals(m_resetToProgramStartMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_resetToProgramStartMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_lastTransitionReason != null)
            {
                children.Add(m_lastTransitionReason);
            }

            if (m_resetToProgramStartMethod != null)
            {
                children.Add(m_resetToProgramStartMethod);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_lastTransitionReason, child))
            {
                m_lastTransitionReason = null;
                return;
            }

            if (Object.ReferenceEquals(m_resetToProgramStartMethod, child))
            {
                m_resetToProgramStartMethod = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.LastTransitionReason:
                {
                    if (createOrReplace)
                    {
                        if (LastTransitionReason == null)
                        {
                            if (replacement == null)
                            {
                                LastTransitionReason = new MultiStateValueDiscreteState<short>(this);
                            }
                            else
                            {
                                LastTransitionReason = (MultiStateValueDiscreteState<short>)replacement;
                            }
                        }
                    }

                    instance = LastTransitionReason;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.ResetToProgramStart:
                {
                    if (createOrReplace)
                    {
                        if (ResetToProgramStart == null)
                        {
                            if (replacement == null)
                            {
                                ResetToProgramStart = new ResetToProgramStartMethodState(this);
                            }
                            else
                            {
                                ResetToProgramStart = (ResetToProgramStartMethodState)replacement;
                            }
                        }
                    }

                    instance = ResetToProgramStart;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private MultiStateValueDiscreteState<short> m_lastTransitionReason;
        private ResetToProgramStartMethodState m_resetToProgramStartMethod;
        #endregion
    }
    #endif
    #endregion

    #region SystemOperationTypeState Class
    #if (!OPCUA_EXCLUDE_SystemOperationTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class SystemOperationTypeState : BaseObjectState
    {
        #region Constructors
        public SystemOperationTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.SystemOperationType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (Conditions != null)
            {
                Conditions.Initialize(context, Conditions_InitializationString);
            }
        }

        #region Initialization String
        private const string Conditions_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEACgAAAENvbmRpdGlvbnMBAboTAC8APboTAAD/////AQAAAARgwAoBAAAAJAAAAEFja25v" +
           "d2xlZGdlYWJsZUNvbmRpdGlvbl9QbGFjZWhvbGRlcgEAGgAAADxBY2tub3dsZWRnZWFibGVDb25kaXRp" +
           "b24+AQHDEwAjAQBBC8MTAAD/////GQAAABVgiQoCAAAAAAAHAAAARXZlbnRJZAEBPRgALgBEPRgAAAAP" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAACQAAAEV2ZW50VHlwZQEBPhgALgBEPhgAAAAR/////wEB////" +
           "/wAAAAAVYIkKAgAAAAAACgAAAFNvdXJjZU5vZGUBAUMYAC4AREMYAAAAEf////8BAf////8AAAAAFWCJ" +
           "CgIAAAAAAAoAAABTb3VyY2VOYW1lAQFCGAAuAERCGAAAAAz/////AQH/////AAAAABVgiQoCAAAAAAAE" +
           "AAAAVGltZQEBRBgALgBERBgAAAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAALAAAAUmVjZWl2ZVRp" +
           "bWUBAUAYAC4AREAYAAABACYB/////wEB/////wAAAAAVYIkKAgAAAAAABwAAAE1lc3NhZ2UBAT8YAC4A" +
           "RD8YAAAAFf////8BAf////8AAAAAFWCJCgIAAAAAAAgAAABTZXZlcml0eQEBQRgALgBEQRgAAAAF////" +
           "/wEB/////wAAAAAVYIkKAgAAAAAAEAAAAENvbmRpdGlvbkNsYXNzSWQBATMYAC4ARDMYAAAAEf////8B" +
           "Af////8AAAAAFWCJCgIAAAAAABIAAABDb25kaXRpb25DbGFzc05hbWUBATQYAC4ARDQYAAAAFf////8B" +
           "Af////8AAAAAF2CJCgIAAAAAABMAAABDb25kaXRpb25TdWJDbGFzc0lkAQE2GAAuAEQ2GAAAABEBAAAA" +
           "AQAAAAAAAAABAf////8AAAAAF2CJCgIAAAAAABUAAABDb25kaXRpb25TdWJDbGFzc05hbWUBATcYAC4A" +
           "RDcYAAAAFQEAAAABAAAAAAAAAAEB/////wAAAAAVYIkKAgAAAAAADQAAAENvbmRpdGlvbk5hbWUBATUY" +
           "AC4ARDUYAAAADP////8BAf////8AAAAAFWCJCgIAAAAAAAgAAABCcmFuY2hJZAEBLxgALgBELxgAAAAR" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAABgAAAFJldGFpbgEBPBgALgBEPBgAAAAB/////wEB/////wAA" +
           "AAAVYIkKAgAAAAAADAAAAEVuYWJsZWRTdGF0ZQEBLBgALwEAIyMsGAAAABX/////AQECAAAAAQAsIwAB" +
           "ASkYAQAsIwADAQBSAAAAU3lzdGVtT3BlcmF0aW9uVHlwZV9Db25kaXRpb25zX0Fja25vd2xlZGdlYWJs" +
           "ZUNvbmRpdGlvbl9QbGFjZWhvbGRlcl9Db25maXJtZWRTdGF0ZQEAAAAVYIkKAgAAAAAAAgAAAElkAQEt" +
           "GAAuAEQtGAAAAAH/////AQH/////AAAAABVgiQoCAAAAAAAHAAAAUXVhbGl0eQEBOhgALwEAKiM6GAAA" +
           "ABP/////AQH/////AQAAABVgiQoCAAAAAAAPAAAAU291cmNlVGltZXN0YW1wAQE7GAAuAEQ7GAAAAQAm" +
           "Af////8BAf////8AAAAAFWCJCgIAAAAAAAwAAABMYXN0U2V2ZXJpdHkBATgYAC8BACojOBgAAAAF////" +
           "/wEB/////wEAAAAVYIkKAgAAAAAADwAAAFNvdXJjZVRpbWVzdGFtcAEBORgALgBEORgAAAEAJgH/////" +
           "AQH/////AAAAABVgiQoCAAAAAAAHAAAAQ29tbWVudAEBMRgALwEAKiMxGAAAABX/////AQH/////AQAA" +
           "ABVgiQoCAAAAAAAPAAAAU291cmNlVGltZXN0YW1wAQEyGAAuAEQyGAAAAQAmAf////8BAf////8AAAAA" +
           "FWCJCgIAAAAAAAwAAABDbGllbnRVc2VySWQBATAYAC4ARDAYAAAADP////8BAf////8AAAAABGGCCgQA" +
           "AAAAAAcAAABEaXNhYmxlAQFtGwAvAQBEI20bAAABAQEAAAABAPkLAAEA8woAAAAABGGCCgQAAAAAAAYA" +
           "AABFbmFibGUBAW4bAC8BAEMjbhsAAAEBAQAAAAEA+QsAAQDzCgAAAAAEYYIKBAAAAAAACgAAAEFkZENv" +
           "bW1lbnQBAWwbAC8BAEUjbBsAAAEBAQAAAAEA+QsAAQANCwEAAAAXYKkKAgAAAAAADgAAAElucHV0QXJn" +
           "dW1lbnRzAQEuGAAuAEQuGAAAlgIAAAABACoBAUIAAAAHAAAARXZlbnRJZAAP/////wAAAAACKAAAAFRo" +
           "ZSBpZGVudGlmaWVyIGZvciB0aGUgZXZlbnQgdG8gY29tbWVudC4BACoBAT4AAAAHAAAAQ29tbWVudAAV" +
           "/////wAAAAACJAAAAFRoZSBjb21tZW50IHRvIGFkZCB0byB0aGUgY29uZGl0aW9uLgEAKAEBAAAAAQAA" +
           "AAIAAAABAf////8AAAAAFWCJCgIAAAAAAAoAAABBY2tlZFN0YXRlAQEpGAAvAQAjIykYAAAAFf////8B" +
           "AQEAAAABACwjAQEBLBgBAAAAFWCJCgIAAAAAAAIAAABJZAEBKhgALgBEKhgAAAAB/////wEB/////wAA" +
           "AAAEYYIKBAAAAAAACwAAAEFja25vd2xlZGdlAQFrGwAvAQCXI2sbAAABAQEAAAABAPkLAAEA8CIBAAAA" +
           "F2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBKxgALgBEKxgAAJYCAAAAAQAqAQFCAAAABwAAAEV2" +
           "ZW50SWQAD/////8AAAAAAigAAABUaGUgaWRlbnRpZmllciBmb3IgdGhlIGV2ZW50IHRvIGNvbW1lbnQu" +
           "AQAqAQE+AAAABwAAAENvbW1lbnQAFf////8AAAAAAiQAAABUaGUgY29tbWVudCB0byBhZGQgdG8gdGhl" +
           "IGNvbmRpdGlvbi4BACgBAQAAAAEAAAACAAAAAQH/////AAAAAA==";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAGwAAAFN5c3RlbU9wZXJhdGlvblR5cGVJbnN0YW5jZQEBBAQBAQQEBAQAAP////8DAAAA" +
           "BGCACgEAAAABAAoAAABDb25kaXRpb25zAQG6EwAvAD26EwAA/////wEAAAAEYMAKAQAAACQAAABBY2tu" +
           "b3dsZWRnZWFibGVDb25kaXRpb25fUGxhY2Vob2xkZXIBABoAAAA8QWNrbm93bGVkZ2VhYmxlQ29uZGl0" +
           "aW9uPgEBwxMAIwEAQQvDEwAA/////xkAAAAVYIkKAgAAAAAABwAAAEV2ZW50SWQBAT0YAC4ARD0YAAAA" +
           "D/////8BAf////8AAAAAFWCJCgIAAAAAAAkAAABFdmVudFR5cGUBAT4YAC4ARD4YAAAAEf////8BAf//" +
           "//8AAAAAFWCJCgIAAAAAAAoAAABTb3VyY2VOb2RlAQFDGAAuAERDGAAAABH/////AQH/////AAAAABVg" +
           "iQoCAAAAAAAKAAAAU291cmNlTmFtZQEBQhgALgBEQhgAAAAM/////wEB/////wAAAAAVYIkKAgAAAAAA" +
           "BAAAAFRpbWUBAUQYAC4AREQYAAABACYB/////wEB/////wAAAAAVYIkKAgAAAAAACwAAAFJlY2VpdmVU" +
           "aW1lAQFAGAAuAERAGAAAAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAcAAABNZXNzYWdlAQE/GAAu" +
           "AEQ/GAAAABX/////AQH/////AAAAABVgiQoCAAAAAAAIAAAAU2V2ZXJpdHkBAUEYAC4AREEYAAAABf//" +
           "//8BAf////8AAAAAFWCJCgIAAAAAABAAAABDb25kaXRpb25DbGFzc0lkAQEzGAAuAEQzGAAAABH/////" +
           "AQH/////AAAAABVgiQoCAAAAAAASAAAAQ29uZGl0aW9uQ2xhc3NOYW1lAQE0GAAuAEQ0GAAAABX/////" +
           "AQH/////AAAAABdgiQoCAAAAAAATAAAAQ29uZGl0aW9uU3ViQ2xhc3NJZAEBNhgALgBENhgAAAARAQAA" +
           "AAEAAAAAAAAAAQH/////AAAAABdgiQoCAAAAAAAVAAAAQ29uZGl0aW9uU3ViQ2xhc3NOYW1lAQE3GAAu" +
           "AEQ3GAAAABUBAAAAAQAAAAAAAAABAf////8AAAAAFWCJCgIAAAAAAA0AAABDb25kaXRpb25OYW1lAQE1" +
           "GAAuAEQ1GAAAAAz/////AQH/////AAAAABVgiQoCAAAAAAAIAAAAQnJhbmNoSWQBAS8YAC4ARC8YAAAA" +
           "Ef////8BAf////8AAAAAFWCJCgIAAAAAAAYAAABSZXRhaW4BATwYAC4ARDwYAAAAAf////8BAf////8A" +
           "AAAAFWCJCgIAAAAAAAwAAABFbmFibGVkU3RhdGUBASwYAC8BACMjLBgAAAAV/////wEBAgAAAAEALCMA" +
           "AQEpGAEALCMAAwEAUgAAAFN5c3RlbU9wZXJhdGlvblR5cGVfQ29uZGl0aW9uc19BY2tub3dsZWRnZWFi" +
           "bGVDb25kaXRpb25fUGxhY2Vob2xkZXJfQ29uZmlybWVkU3RhdGUBAAAAFWCJCgIAAAAAAAIAAABJZAEB" +
           "LRgALgBELRgAAAAB/////wEB/////wAAAAAVYIkKAgAAAAAABwAAAFF1YWxpdHkBAToYAC8BACojOhgA" +
           "AAAT/////wEB/////wEAAAAVYIkKAgAAAAAADwAAAFNvdXJjZVRpbWVzdGFtcAEBOxgALgBEOxgAAAEA" +
           "JgH/////AQH/////AAAAABVgiQoCAAAAAAAMAAAATGFzdFNldmVyaXR5AQE4GAAvAQAqIzgYAAAABf//" +
           "//8BAf////8BAAAAFWCJCgIAAAAAAA8AAABTb3VyY2VUaW1lc3RhbXABATkYAC4ARDkYAAABACYB////" +
           "/wEB/////wAAAAAVYIkKAgAAAAAABwAAAENvbW1lbnQBATEYAC8BACojMRgAAAAV/////wEB/////wEA" +
           "AAAVYIkKAgAAAAAADwAAAFNvdXJjZVRpbWVzdGFtcAEBMhgALgBEMhgAAAEAJgH/////AQH/////AAAA" +
           "ABVgiQoCAAAAAAAMAAAAQ2xpZW50VXNlcklkAQEwGAAuAEQwGAAAAAz/////AQH/////AAAAAARhggoE" +
           "AAAAAAAHAAAARGlzYWJsZQEBbRsALwEARCNtGwAAAQEBAAAAAQD5CwABAPMKAAAAAARhggoEAAAAAAAG" +
           "AAAARW5hYmxlAQFuGwAvAQBDI24bAAABAQEAAAABAPkLAAEA8woAAAAABGGCCgQAAAAAAAoAAABBZGRD" +
           "b21tZW50AQFsGwAvAQBFI2wbAAABAQEAAAABAPkLAAEADQsBAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFy" +
           "Z3VtZW50cwEBLhgALgBELhgAAJYCAAAAAQAqAQFCAAAABwAAAEV2ZW50SWQAD/////8AAAAAAigAAABU" +
           "aGUgaWRlbnRpZmllciBmb3IgdGhlIGV2ZW50IHRvIGNvbW1lbnQuAQAqAQE+AAAABwAAAENvbW1lbnQA" +
           "Ff////8AAAAAAiQAAABUaGUgY29tbWVudCB0byBhZGQgdG8gdGhlIGNvbmRpdGlvbi4BACgBAQAAAAEA" +
           "AAACAAAAAQH/////AAAAABVgiQoCAAAAAAAKAAAAQWNrZWRTdGF0ZQEBKRgALwEAIyMpGAAAABX/////" +
           "AQEBAAAAAQAsIwEBASwYAQAAABVgiQoCAAAAAAACAAAASWQBASoYAC4ARCoYAAAAAf////8BAf////8A" +
           "AAAABGGCCgQAAAAAAAsAAABBY2tub3dsZWRnZQEBaxsALwEAlyNrGwAAAQEBAAAAAQD5CwABAPAiAQAA" +
           "ABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBASsYAC4ARCsYAACWAgAAAAEAKgEBQgAAAAcAAABF" +
           "dmVudElkAA//////AAAAAAIoAAAAVGhlIGlkZW50aWZpZXIgZm9yIHRoZSBldmVudCB0byBjb21tZW50" +
           "LgEAKgEBPgAAAAcAAABDb21tZW50ABX/////AAAAAAIkAAAAVGhlIGNvbW1lbnQgdG8gYWRkIHRvIHRo" +
           "ZSBjb25kaXRpb24uAQAoAQEAAAABAAAAAgAAAAEB/////wAAAAAVYKkKAgAAAAAAGQAAAERlZmF1bHRJ" +
           "bnN0YW5jZUJyb3dzZU5hbWUBAfIXAC4ARPIXAAAUAwAPAAAAU3lzdGVtT3BlcmF0aW9uABT/////AwP/" +
           "////AAAAAARggAoBAAAAAQAbAAAAU3lzdGVtT3BlcmF0aW9uU3RhdGVNYWNoaW5lAQG5EwAvAQH9A7kT" +
           "AAABAAAAACkAAQAHCQMAAAAVYIkKAgAAAAAADAAAAEN1cnJlbnRTdGF0ZQEB8BcALwEAyArwFwAAABX/" +
           "////AQH/////AQAAABVgiQoCAAAAAAACAAAASWQBAfEXAC4ARPEXAAAAEf////8BAf////8AAAAAFWCJ" +
           "CgIAAAAAAA4AAABMYXN0VHJhbnNpdGlvbgEB6xcALwEAzwrrFwAAABX/////AQH/////AQAAABVgiQoC" +
           "AAAAAAACAAAASWQBAewXAC4AROwXAAAAEf////8BAf////8AAAAAFWCJCgIAAAABABQAAABMYXN0VHJh" +
           "bnNpdGlvblJlYXNvbgEB7RcALwEA5ivtFwAAAAT/////AwP/////AgAAABdgqQoCAAAAAAAKAAAARW51" +
           "bVZhbHVlcwEB7hcALgBE7hcAAJYGAAAAAQA7IAFAAAAAAAAAAAAAAAADAgAAAGVuBwAAAFVua25vd24D" +
           "AgAAAGVuGwAAAENhdXNlZCBieSBhbiB1bmtub3duIHJlYXNvbgEAOyABQgAAAAEAAAAAAAAAAwIAAABl" +
           "bggAAABFeHRlcm5hbAMCAAAAZW4cAAAAQ2F1c2VkIGJ5IGV4dGVybmFsIG9wZXJhdGlvbgEAOyABPgAA" +
           "AAIAAAAAAAAAAwIAAABlbgYAAABEaXJlY3QDAgAAAGVuGgAAAENhdXNlZCBieSBkaXJlY3Qgb3BlcmF0" +
           "aW9uAQA7IAFGAAAAAwAAAAAAAAADAgAAAGVuBgAAAFN5c3RlbQMCAAAAZW4iAAAAQ2F1c2VkIGJ5IHN5" +
           "c3RlbSBzcGVjaWZpYyBiZWhhdmlvcgEAOyABNQAAAAQAAAAAAAAAAwIAAABlbgUAAABFcnJvcgMCAAAA" +
           "ZW4SAAAAQ2F1c2VkIGJ5IGFuIGVycm9yAQA7IAFUAAAABQAAAAAAAAADAgAAAGVuCwAAAEFwcGxpY2F0" +
           "aW9uAwIAAABlbisAAABDYXVzZWQgZXhwbGljaXRseSBieSBlbmQgdXNlciBwcm9ncmFtIGxvZ2ljAQCq" +
           "HQEAAAABAAAAAAAAAAEB/////wAAAAAVYKkKAgAAAAAACwAAAFZhbHVlQXNUZXh0AQHvFwAuAETvFwAA" +
           "FQIHAAAASW52YWxpZAAV/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public FolderState Conditions
        {
            get => m_conditions;

            set
            {
                if (!Object.ReferenceEquals(m_conditions, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_conditions = value;
            }
        }

        public SystemOperationStateMachineTypeState SystemOperationStateMachine
        {
            get => m_systemOperationStateMachine;

            set
            {
                if (!Object.ReferenceEquals(m_systemOperationStateMachine, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_systemOperationStateMachine = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_conditions != null)
            {
                children.Add(m_conditions);
            }

            if (m_systemOperationStateMachine != null)
            {
                children.Add(m_systemOperationStateMachine);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_conditions, child))
            {
                m_conditions = null;
                return;
            }

            if (Object.ReferenceEquals(m_systemOperationStateMachine, child))
            {
                m_systemOperationStateMachine = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.Conditions:
                {
                    if (createOrReplace)
                    {
                        if (Conditions == null)
                        {
                            if (replacement == null)
                            {
                                Conditions = new FolderState(this);
                            }
                            else
                            {
                                Conditions = (FolderState)replacement;
                            }
                        }
                    }

                    instance = Conditions;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.SystemOperationStateMachine:
                {
                    if (createOrReplace)
                    {
                        if (SystemOperationStateMachine == null)
                        {
                            if (replacement == null)
                            {
                                SystemOperationStateMachine = new SystemOperationStateMachineTypeState(this);
                            }
                            else
                            {
                                SystemOperationStateMachine = (SystemOperationStateMachineTypeState)replacement;
                            }
                        }
                    }

                    instance = SystemOperationStateMachine;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private FolderState m_conditions;
        private SystemOperationStateMachineTypeState m_systemOperationStateMachine;
        #endregion
    }
    #endif
    #endregion

    #region TaskControlOperationTypeState Class
    #if (!OPCUA_EXCLUDE_TaskControlOperationTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class TaskControlOperationTypeState : BaseObjectState
    {
        #region Constructors
        public TaskControlOperationTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.TaskControlOperationType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (MotionDevicesUnderControl != null)
            {
                MotionDevicesUnderControl.Initialize(context, MotionDevicesUnderControl_InitializationString);
            }
        }

        #region Initialization String
        private const string MotionDevicesUnderControl_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8X" +
           "YIkKAgAAAAEAGQAAAE1vdGlvbkRldmljZXNVbmRlckNvbnRyb2wBAfMXAC4ARPMXAAAAEQEAAAABAAAA" +
           "AAAAAAEB/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAIAAAAFRhc2tDb250cm9sT3BlcmF0aW9uVHlwZUluc3RhbmNlAQHwAwEB8APwAwAA////" +
           "/wMAAAAVYKkKAgAAAAAAGQAAAERlZmF1bHRJbnN0YW5jZUJyb3dzZU5hbWUBAfQXAC4ARPQXAAAUAQAU" +
           "AAAAVGFza0NvbnRyb2xPcGVyYXRpb24AFP////8DA/////8AAAAAF2CJCgIAAAABABkAAABNb3Rpb25E" +
           "ZXZpY2VzVW5kZXJDb250cm9sAQHzFwAuAETzFwAAABEBAAAAAQAAAAAAAAABAf////8AAAAABGCACgEA" +
           "AAABABcAAABUYXNrQ29udHJvbFN0YXRlTWFjaGluZQEBuxMALwEBAQS7EwAAAQAAAAApAAEABwkDAAAA" +
           "FWCJCgIAAAAAAAwAAABDdXJyZW50U3RhdGUBAfoXAC8BAMgK+hcAAAAV/////wEB/////wEAAAAVYIkK" +
           "AgAAAAAAAgAAAElkAQH7FwAuAET7FwAAABH/////AQH/////AAAAABVgiQoCAAAAAAAOAAAATGFzdFRy" +
           "YW5zaXRpb24BAfUXAC8BAM8K9RcAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQH2FwAu" +
           "AET2FwAAABH/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25SZWFzb24BAfcX" +
           "AC8BAOYr9xcAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMBAfgXAC4ARPgX" +
           "AACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsAAABDYXVzZWQg" +
           "YnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0ZXJuYWwDAgAA" +
           "AGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAAAAMCAAAAZW4G" +
           "AAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyABRgAAAAMAAAAA" +
           "AAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3BlY2lmaWMgYmVo" +
           "YXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENhdXNlZCBieSBh" +
           "biBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAAZW4rAAAAQ2F1" +
           "c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAAAAAAAAABAf//" +
           "//8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEB+RcALgBE+RcAABUCBwAAAEludmFsaWQAFf//" +
           "//8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<NodeId[]> MotionDevicesUnderControl
        {
            get => m_motionDevicesUnderControl;

            set
            {
                if (!Object.ReferenceEquals(m_motionDevicesUnderControl, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_motionDevicesUnderControl = value;
            }
        }

        public TaskControlStateMachineTypeState TaskControlStateMachine
        {
            get => m_taskControlStateMachine;

            set
            {
                if (!Object.ReferenceEquals(m_taskControlStateMachine, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_taskControlStateMachine = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_motionDevicesUnderControl != null)
            {
                children.Add(m_motionDevicesUnderControl);
            }

            if (m_taskControlStateMachine != null)
            {
                children.Add(m_taskControlStateMachine);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_motionDevicesUnderControl, child))
            {
                m_motionDevicesUnderControl = null;
                return;
            }

            if (Object.ReferenceEquals(m_taskControlStateMachine, child))
            {
                m_taskControlStateMachine = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.MotionDevicesUnderControl:
                {
                    if (createOrReplace)
                    {
                        if (MotionDevicesUnderControl == null)
                        {
                            if (replacement == null)
                            {
                                MotionDevicesUnderControl = new PropertyState<NodeId[]>(this);
                            }
                            else
                            {
                                MotionDevicesUnderControl = (PropertyState<NodeId[]>)replacement;
                            }
                        }
                    }

                    instance = MotionDevicesUnderControl;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.TaskControlStateMachine:
                {
                    if (createOrReplace)
                    {
                        if (TaskControlStateMachine == null)
                        {
                            if (replacement == null)
                            {
                                TaskControlStateMachine = new TaskControlStateMachineTypeState(this);
                            }
                            else
                            {
                                TaskControlStateMachine = (TaskControlStateMachineTypeState)replacement;
                            }
                        }
                    }

                    instance = TaskControlStateMachine;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<NodeId[]> m_motionDevicesUnderControl;
        private TaskControlStateMachineTypeState m_taskControlStateMachine;
        #endregion
    }
    #endif
    #endregion

    #region TaskModuleTypeState Class
    #if (!OPCUA_EXCLUDE_TaskModuleTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class TaskModuleTypeState : BaseObjectState
    {
        #region Constructors
        public TaskModuleTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.TaskModuleType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (IsReferenced != null)
            {
                IsReferenced.Initialize(context, IsReferenced_InitializationString);
            }

            if (Version != null)
            {
                Version.Initialize(context, Version_InitializationString);
            }
        }

        #region Initialization String
        private const string IsReferenced_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAEADAAAAElzUmVmZXJlbmNlZAEBqBcALgBEqBcAAAAB/////wMD/////wAAAAA=";

        private const string Version_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAEABwAAAFZlcnNpb24BAacXAC4ARKcXAAAADP////8DA/////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAFgAAAFRhc2tNb2R1bGVUeXBlSW5zdGFuY2UBAfgDAQH4A/gDAAD/////AwAAABVgiQoC" +
           "AAAAAQAMAAAASXNSZWZlcmVuY2VkAQGoFwAuAESoFwAAAAH/////AwP/////AAAAABVgiQoCAAAAAQAE" +
           "AAAATmFtZQEBphcALgBEphcAAAAM/////wMD/////wAAAAAVYIkKAgAAAAEABwAAAFZlcnNpb24BAacX" +
           "AC4ARKcXAAAADP////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<bool> IsReferenced
        {
            get => m_isReferenced;

            set
            {
                if (!Object.ReferenceEquals(m_isReferenced, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_isReferenced = value;
            }
        }

        public PropertyState<string> Name
        {
            get => m_name;

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }

        public PropertyState<string> Version
        {
            get => m_version;

            set
            {
                if (!Object.ReferenceEquals(m_version, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_version = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_isReferenced != null)
            {
                children.Add(m_isReferenced);
            }

            if (m_name != null)
            {
                children.Add(m_name);
            }

            if (m_version != null)
            {
                children.Add(m_version);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_isReferenced, child))
            {
                m_isReferenced = null;
                return;
            }

            if (Object.ReferenceEquals(m_name, child))
            {
                m_name = null;
                return;
            }

            if (Object.ReferenceEquals(m_version, child))
            {
                m_version = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.IsReferenced:
                {
                    if (createOrReplace)
                    {
                        if (IsReferenced == null)
                        {
                            if (replacement == null)
                            {
                                IsReferenced = new PropertyState<bool>(this);
                            }
                            else
                            {
                                IsReferenced = (PropertyState<bool>)replacement;
                            }
                        }
                    }

                    instance = IsReferenced;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new PropertyState<string>(this);
                            }
                            else
                            {
                                Name = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Version:
                {
                    if (createOrReplace)
                    {
                        if (Version == null)
                        {
                            if (replacement == null)
                            {
                                Version = new PropertyState<string>(this);
                            }
                            else
                            {
                                Version = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Version;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<bool> m_isReferenced;
        private PropertyState<string> m_name;
        private PropertyState<string> m_version;
        #endregion
    }
    #endif
    #endregion

    #region AuxiliaryComponentTypeState Class
    #if (!OPCUA_EXCLUDE_AuxiliaryComponentTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AuxiliaryComponentTypeState : ComponentTypeState
    {
        #region Constructors
        public AuxiliaryComponentTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.AuxiliaryComponentType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }
        }

        #region Initialization String
        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBAScYAC4ARCcYAAAADP////8BAf////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAHgAAAEF1eGlsaWFyeUNvbXBvbmVudFR5cGVJbnN0YW5jZQEBPUUBAT1FPUUAAP////8C" +
           "AAAANWCJCgIAAAACAAsAAABQcm9kdWN0Q29kZQEBXEUDAAAAALgAAABUaGUgUHJvZHVjdENvZGUgcHJv" +
           "cGVydHkgcHJvdmlkZXMgYSB1bmlxdWUgY29tYmluYXRpb24gb2YgbnVtYmVycyBhbmQgbGV0dGVycyB1" +
           "c2VkIHRvIGlkZW50aWZ5IHRoZSBwcm9kdWN0LiBJdCBtYXkgYmUgdGhlIG9yZGVyIGluZm9ybWF0aW9u" +
           "IGRpc3BsYXllZCBvbiB0eXBlIHNoaWVsZHMgb3IgaW4gRVJQIHN5c3RlbXMuAC4ARFxFAAAADP////8B" +
           "Af////8AAAAAFWCJCgIAAAACAAcAAABBc3NldElkAQEnGAAuAEQnGAAAAAz/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region AxisTypeState Class
    #if (!OPCUA_EXCLUDE_AxisTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AxisTypeState : ComponentTypeState
    {
        #region Constructors
        public AxisTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.AxisType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AdditionalLoad != null)
            {
                AdditionalLoad.Initialize(context, AdditionalLoad_InitializationString);
            }

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }
        }

        #region Initialization String
        private const string AdditionalLoad_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAEADgAAAEFkZGl0aW9uYWxMb2FkAQH+QAMAAAAAYwAAAFRoZSBhZGRpdGlvbmFsIGxvYWQg" +
           "d2hpY2ggaXMgbW91bnRlZCBvbiB0aGlzIGF4aXMuIEUuZy4gZm9yIHByb2Nlc3MtbmVlZCBhIHRyYW5z" +
           "Zm9ybWVyIGZvciB3ZWxkaW5nLgAvAQH6A/5AAAD/////AQAAADVgiQoCAAAAAQAEAAAATWFzcwEB/0AD" +
           "AAAAADUAAABUaGUgd2VpZ2h0IG9mIHRoZSBsb2FkIG1vdW50ZWQgb24gb25lIG1vdW50aW5nIHBvaW50" +
           "LgAvAQBZRP9AAAAAC/////8BAf////8BAAAAFWCJCgIAAAAAABAAAABFbmdpbmVlcmluZ1VuaXRzAQEE" +
           "QQAuAEQEQQAAAQB3A/////8BAf////8AAAAA";

        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBAR8YAC4ARB8YAAAADP////8BAf////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAEAAAAEF4aXNUeXBlSW5zdGFuY2UBAdlAAQHZQNlAAAD/////BQAAACRggAoBAAAAAgAM" +
           "AAAAUGFyYW1ldGVyU2V0AQHaQAMAAAAAFwAAAEZsYXQgbGlzdCBvZiBQYXJhbWV0ZXJzAC8AOtpAAAD/" +
           "////AwAAADVgiQoCAAAAAQASAAAAQWN0dWFsQWNjZWxlcmF0aW9uAQEiQQMAAAAArQAAADogVGhlIEFj" +
           "dHVhbEFjY2VsZXJhdGlvbiB2YXJpYWJsZSBwcm92aWRlcyB0aGUgYXhpcyBhY2NlbGVyYXRpb24uIEFw" +
           "cGxpY2FibGUgYWNjZWxlcmF0aW9uIGxpbWl0cyBvZiB0aGUgYXhpcyBzaGFsbCBiZSBwcm92aWRlZCBi" +
           "eSB0aGUgRVVSYW5nZSBwcm9wZXJ0eSBvZiB0aGUgQW5hbG9nVW5pdFR5cGUuAC8BAFlEIkEAAAAL////" +
           "/wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5pdHMBASdBAC4ARCdBAAABAHcD////" +
           "/wEB/////wAAAAA1YIkKAgAAAAEADgAAAEFjdHVhbFBvc2l0aW9uAQEWQQMAAAAAMwAAAFRoZSBheGlz" +
           "IHBvc2l0aW9uIGluY2x1c2l2ZSBVbml0IGFuZCBSYW5nZU9mTW90aW9uLgAvAQBZRBZBAAAAC/////8B" +
           "Af////8BAAAAFWCJCgIAAAAAABAAAABFbmdpbmVlcmluZ1VuaXRzAQEbQQAuAEQbQQAAAQB3A/////8B" +
           "Af////8AAAAANWCJCgIAAAABAAsAAABBY3R1YWxTcGVlZAEBHEEDAAAAAEAAAABUaGUgYXhpcyBzcGVl" +
           "ZCBvbiBsb2FkIHNpZGUgKGFmdGVyIGdlYXIvc3BpbmRsZSkgaW5jbHVzaXZlIFVuaXQuAC8BAFlEHEEA" +
           "AAAL/////wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5pdHMBASFBAC4ARCFBAAAB" +
           "AHcD/////wEB/////wAAAAAVYIkKAgAAAAIABwAAAEFzc2V0SWQBAR8YAC4ARB8YAAAADP////8BAf//" +
           "//8AAAAAJGDACgEAAAAgAAAAUG93ZXJUcmFpbklkZW50aWZpZXJfUGxhY2Vob2xkZXIBABYAAAA8UG93" +
           "ZXJUcmFpbklkZW50aWZpZXI+AQGoRwMAAAAAzAEAAFRoZSBSZXF1aXJlcyByZWZlcmVuY2UgcHJvdmlk" +
           "ZXMgdGhlIHJlbGF0aW9uc2hpcCBvZiBheGVzIHRvIHBvd2VydHJhaW5zLiBGb3IgY29tcGxleCBraW5l" +
           "bWF0aWNzIHRoaXMgZG9lcyBub3QgbmVlZCB0byBiZSBhIG9uZSB0byBvbmUgcmVsYXRpb25zaGlwLCBi" +
           "ZWNhdXNlIG1vcmUgdGhhbiBvbmUgcG93ZXIgdHJhaW4gbWlnaHQgaW5mbHVlbmNlIHRoZSBtb3Rpb24g" +
           "b2Ygb25lIGF4aXMuIFRoaXMgcmVmZXJlbmNlIGNvbm5lY3RzIGFsbCBwb3dlciB0cmFpbnMgdG8gYW4g" +
           "YXhpcyB0aGF0IG11c3QgYmUgYWN0aXZlbHkgZHJpdmVuIHdoZW4gb25seSB0aGlzIGF4aXMgc2hvdWxk" +
           "IG1vdmUgYW5kIGFsbCBvdGhlciBheGVzIHNob3VsZCBzdGFuZCBzdGlsbC4gVmlydHVhbCBheGVzIHRo" +
           "YXQgYXJlIG5vdCBhY3RpdmVseSBkcml2ZW4gYnkgYSBwb3dlciB0cmFpbiBkbyBub3QgaGF2ZSB0aGlz" +
           "IHJlZmVyZW5jZS4BAQNHAQGaQahHAAD/////AAAAACRggAoBAAAAAQAOAAAAQWRkaXRpb25hbExvYWQB" +
           "Af5AAwAAAABjAAAAVGhlIGFkZGl0aW9uYWwgbG9hZCB3aGljaCBpcyBtb3VudGVkIG9uIHRoaXMgYXhp" +
           "cy4gRS5nLiBmb3IgcHJvY2Vzcy1uZWVkIGEgdHJhbnNmb3JtZXIgZm9yIHdlbGRpbmcuAC8BAfoD/kAA" +
           "AP////8BAAAANWCJCgIAAAABAAQAAABNYXNzAQH/QAMAAAAANQAAAFRoZSB3ZWlnaHQgb2YgdGhlIGxv" +
           "YWQgbW91bnRlZCBvbiBvbmUgbW91bnRpbmcgcG9pbnQuAC8BAFlE/0AAAAAL/////wEB/////wEAAAAV" +
           "YIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5pdHMBAQRBAC4ARARBAAABAHcD/////wEB/////wAAAAA1" +
           "YIkKAgAAAAEADQAAAE1vdGlvblByb2ZpbGUBAf1AAwAAAABJAAAAVGhlIGtpbmQgb2YgYXhpcyBtb3Rp" +
           "b24gYXMgZGVmaW5lZCB3aXRoIHRoZSBBeGlzTW90aW9uUHJvZmlsZUVudW1lcmF0aW9uLgAuAET9QAAA" +
           "AQHAC/////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public LoadTypeState AdditionalLoad
        {
            get => m_additionalLoad;

            set
            {
                if (!Object.ReferenceEquals(m_additionalLoad, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_additionalLoad = value;
            }
        }

        public PropertyState<AxisMotionProfileEnumeration> MotionProfile
        {
            get => m_motionProfile;

            set
            {
                if (!Object.ReferenceEquals(m_motionProfile, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_motionProfile = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_additionalLoad != null)
            {
                children.Add(m_additionalLoad);
            }

            if (m_motionProfile != null)
            {
                children.Add(m_motionProfile);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_additionalLoad, child))
            {
                m_additionalLoad = null;
                return;
            }

            if (Object.ReferenceEquals(m_motionProfile, child))
            {
                m_motionProfile = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.AdditionalLoad:
                {
                    if (createOrReplace)
                    {
                        if (AdditionalLoad == null)
                        {
                            if (replacement == null)
                            {
                                AdditionalLoad = new LoadTypeState(this);
                            }
                            else
                            {
                                AdditionalLoad = (LoadTypeState)replacement;
                            }
                        }
                    }

                    instance = AdditionalLoad;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.MotionProfile:
                {
                    if (createOrReplace)
                    {
                        if (MotionProfile == null)
                        {
                            if (replacement == null)
                            {
                                MotionProfile = new PropertyState<AxisMotionProfileEnumeration>(this);
                            }
                            else
                            {
                                MotionProfile = (PropertyState<AxisMotionProfileEnumeration>)replacement;
                            }
                        }
                    }

                    instance = MotionProfile;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private LoadTypeState m_additionalLoad;
        private PropertyState<AxisMotionProfileEnumeration> m_motionProfile;
        #endregion
    }
    #endif
    #endregion

    #region ControllerTypeState Class
    #if (!OPCUA_EXCLUDE_ControllerTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class ControllerTypeState : ComponentTypeState
    {
        #region Constructors
        public ControllerTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.ControllerType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }

            if (ComponentName != null)
            {
                ComponentName.Initialize(context, ComponentName_InitializationString);
            }

            if (Components != null)
            {
                Components.Initialize(context, Components_InitializationString);
            }

            if (DeviceManual != null)
            {
                DeviceManual.Initialize(context, DeviceManual_InitializationString);
            }

            if (ParameterSet != null)
            {
                ParameterSet.Initialize(context, ParameterSet_InitializationString);
            }

            if (Programs != null)
            {
                Programs.Initialize(context, Programs_InitializationString);
            }

            if (SystemOperation != null)
            {
                SystemOperation.Initialize(context, SystemOperation_InitializationString);
            }
        }

        #region Initialization String
        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBASQYAC4ARCQYAAAADP////8BAf////8AAAAA";

        private const string ComponentName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADQAAAENvbXBvbmVudE5hbWUBASUYAC4ARCUYAAAAFf////8BAf////8AAAAA";

        private const string Components_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAEACgAAAENvbXBvbmVudHMBAWRDAwAAAAAbAQAAQ29tcG9uZW50cyBpcyBhIGNvbnRhaW5l" +
           "ciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9mIHN1YnR5cGVzIG9mIENvbXBvbmVudFR5cGUgZGVm" +
           "aW5lZCBpbiBPUEMgVUEgREkuIFRoZSBsaXN0ZWQgY29tcG9uZW50cyBhcmUgaW5zdGFsbGVkIGluIHRo" +
           "ZSBtb3Rpb24gZGV2aWNlIHN5c3RlbSwgZS5nLiBhIHByb2Nlc3NpbmctdW5pdCwgYSBwb3dlci1zdXBw" +
           "bHksIGFuIElPLWJvYXJkIG9yIGEgZHJpdmUsIGFuZCBoYXZlIGFuIGVsZWN0cmljYWwgaW50ZXJmYWNl" +
           "IHRvIHRoZSBjb250cm9sbGVyLgAvAD1kQwAA/////wEAAAAkYMAKAQAAAB8AAABDb21wb25lbnRJZGVu" +
           "dGlmaWVyX1BsYWNlaG9sZGVyAQAVAAAAPENvbXBvbmVudElkZW50aWZpZXI+AQF9SQMAAAAAeQAAAFRo" +
           "ZSBpbnRlbnRpb24gaXMgdG8gaW50ZWdyYXRlIGluc2lkZSB0aGlzIGNvbnRhaW5lciBkZXZpY2VzIHdo" +
           "aWNoIGFyZSBkZWZpbmVkIGluIG90aGVyIGNvbXBhbmlvbiBzcGVjaWZpY2F0aW9ucyB1c2luZyBESS4A" +
           "LwEC1zp9SQAAAgAAAAEAw0QAAQK7OgEAw0QAAQLIOgAAAAA=";

        private const string DeviceManual_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADAAAAERldmljZU1hbnVhbAEBJhgALgBEJhgAAAAM/////wEB/////wAAAAA=";

        private const string ParameterSet_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAIADAAAAFBhcmFtZXRlclNldAEBjBMDAAAAABcAAABGbGF0IGxpc3Qgb2YgUGFyYW1ldGVy" +
           "cwAvADqMEwAA/////wgAAAA1YIkKAgAAAAEADwAAAENhYmluZXRGYW5TcGVlZAEB1UMDAAAAAB0AAABU" +
           "aGUgc3BlZWQgb2YgdGhlIGNhYmluZXQgZmFuLgAvAQBZRNVDAAAAC/////8BAf////8BAAAAFWCJCgIA" +
           "AAAAABAAAABFbmdpbmVlcmluZ1VuaXRzAQHaQwAuAETaQwAAAQB3A/////8BAf////8AAAAANWCJCgIA" +
           "AAABAAsAAABDUFVGYW5TcGVlZAEB20MDAAAAABkAAABUaGUgc3BlZWQgb2YgdGhlIENQVSBmYW4uAC8B" +
           "AFlE20MAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5pdHMBAeBDAC4A" +
           "ROBDAAABAHcD/////wEB/////wAAAAA1YIkKAgAAAAEADAAAAElucHV0Vm9sdGFnZQEB4UMDAAAAALwA" +
           "AABUaGUgaW5wdXQgdm9sdGFnZSBvZiB0aGUgY29udHJvbGxlciB3aGljaCBjYW4gYmUgYSBjb25maWd1" +
           "cmVkIHZhbHVlLiBUbyBkaXN0aW5ndWlzaCBiZXR3ZWVuIGFuIEFDIG9yIERDIHN1cHBseSB0aGUgb3B0" +
           "aW9uYWwgcHJvcGVydHkgRGVmaW5pdGlvbiBvZiB0aGUgYmFzZSB0eXBlIERhdGFJdGVtVHlwZSBzaGFs" +
           "bCBiZSB1c2VkLgAvAQBZROFDAAAAC/////8BAf////8BAAAAFWCJCgIAAAAAABAAAABFbmdpbmVlcmlu" +
           "Z1VuaXRzAQHmQwAuAETmQwAAAQB3A/////8BAf////8AAAAANWCJCgIAAAABAAsAAABTdGFydFVwVGlt" +
           "ZQEBBjwDAAAAADkAAABUaGUgZGF0ZSBhbmQgdGltZSBvZiB0aGUgbGFzdCBzdGFydC11cCBvZiB0aGUg" +
           "Y29udHJvbGxlci4ALwA/BjwAAAAN/////wEB/////wAAAAA1YIkKAgAAAAEACwAAAFRlbXBlcmF0dXJl" +
           "AQHnQwMAAAAAUgAAAFRoZSBjb250cm9sbGVyIHRlbXBlcmF0dXJlIGdpdmVuIGJ5IGEgdGVtcGVyYXR1" +
           "cmUgc2Vuc29yIGluc2lkZSBvZiB0aGUgY29udHJvbGxlci4ALwEAWUTnQwAAAAv/////AQH/////AQAA" +
           "ABVgiQoCAAAAAAAQAAAARW5naW5lZXJpbmdVbml0cwEB7EMALgBE7EMAAAEAdwP/////AQH/////AAAA" +
           "ADVgiQoCAAAAAQAWAAAAVG90YWxFbmVyZ3lDb25zdW1wdGlvbgEBz0MDAAAAAGIAAABUaGUgdG90YWwg" +
           "YWNjdW11bGF0ZWQgZW5lcmd5IGNvbnN1bWVkIGJ5IHRoZSBtb3Rpb24gZGV2aWNlcyByZWxhdGVkIHdp" +
           "dGggdGhpcyBjb250cm9sbGVyIGluc3RhbmNlLgAvAQBZRM9DAAAAC/////8BAf////8BAAAAFWCJCgIA" +
           "AAAAABAAAABFbmdpbmVlcmluZ1VuaXRzAQHUQwAuAETUQwAAAQB3A/////8BAf////8AAAAANWCJCgIA" +
           "AAABABAAAABUb3RhbFBvd2VyT25UaW1lAQHOQwMAAAAAOQAAAFRoZSB0b3RhbCBhY2N1bXVsYXRlZCB0" +
           "aW1lIHRoZSBjb250cm9sbGVyIHdhcyBwb3dlcmVkIG9uLgAvAD/OQwAAAQBPMv////8BAf////8AAAAA" +
           "NWCJCgIAAAABAAgAAABVcHNTdGF0ZQEBBTwDAAAAAEYAAABUaGUgdmVuZG9yIHNwZWNpZmljIHN0YXR1" +
           "cyBvZiBhbiBpbnRlZ3JhdGVkIFVQUyBvciBhY2N1bXVsYXRvciBzeXN0ZW0uAC8APwU8AAAADP////8B" +
           "Af////8AAAAA";

        private const string Programs_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEACAAAAFByb2dyYW1zAQG+EwAvAQApNL4TAAD/////BQAAAARhggoEAAAAAAAPAAAAQ3Jl" +
           "YXRlRGlyZWN0b3J5AQFnGwAvAQBLNGcbAAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3Vt" +
           "ZW50cwEBCxgALgBECxgAAJYBAAAAAQAqAQEcAAAADQAAAERpcmVjdG9yeU5hbWUADP////8AAAAAAAEA" +
           "KAEBAAAAAQAAAAEAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQwYAC4A" +
           "RAwYAACWAQAAAAEAKgEBHgAAAA8AAABEaXJlY3RvcnlOb2RlSWQAEf////8AAAAAAAEAKAEBAAAAAQAA" +
           "AAEAAAABAf////8AAAAABGGCCgQAAAAAAAoAAABDcmVhdGVGaWxlAQFoGwAvAQBONGgbAAABAf////8C" +
           "AAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBDRgALgBEDRgAAJYCAAAAAQAqAQEXAAAACAAA" +
           "AEZpbGVOYW1lAAz/////AAAAAAABACoBAR4AAAAPAAAAUmVxdWVzdEZpbGVPcGVuAAH/////AAAAAAAB" +
           "ACgBAQAAAAEAAAACAAAAAQH/////AAAAABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQEOGAAu" +
           "AEQOGAAAlgIAAAABACoBARkAAAAKAAAARmlsZU5vZGVJZAAR/////wAAAAAAAQAqAQEZAAAACgAAAEZp" +
           "bGVIYW5kbGUAB/////8AAAAAAAEAKAEBAAAAAQAAAAIAAAABAf////8AAAAABGHCCAQAAAAWAAAARGVs" +
           "ZXRlRmlsZVN5c3RlbU9iamVjdAAABgAAAERlbGV0ZQEBAAAALwEAUTQBAf////8BAAAAF2CpCAIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwEBAAAALgBElgEAAAABACoBAR0AAAAOAAAAT2JqZWN0VG9EZWxldGUA" +
           "Ef////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAAAAAoAAABNb3ZlT3JDb3B5" +
           "AQFqGwAvAQBTNGobAAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBEBgALgBE" +
           "EBgAAJYEAAAAAQAqAQEhAAAAEgAAAE9iamVjdFRvTW92ZU9yQ29weQAR/////wAAAAAAAQAqAQEeAAAA" +
           "DwAAAFRhcmdldERpcmVjdG9yeQAR/////wAAAAAAAQAqAQEZAAAACgAAAENyZWF0ZUNvcHkAAf////8A" +
           "AAAAAAEAKgEBFgAAAAcAAABOZXdOYW1lAAz/////AAAAAAABACgBAQAAAAEAAAAEAAAAAQH/////AAAA" +
           "ABdgqQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQERGAAuAEQRGAAAlgEAAAABACoBARgAAAAJAAAA" +
           "TmV3Tm9kZUlkABH/////AAAAAAABACgBAQAAAAEAAAABAAAAAQH/////AAAAAARhggoEAAAAAAAGAAAA" +
           "RGVsZXRlAQFpGwAvAQFpG2kbAAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEB" +
           "DxgALgBEDxgAAJYBAAAAAQAqAQEdAAAADgAAAE9iamVjdFRvRGVsZXRlABH/////AAAAAAABACgBAQAA" +
           "AAEAAAABAAAAAQH/////AAAAAA==";

        private const string SystemOperation_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEADwAAAFN5c3RlbU9wZXJhdGlvbgEBvxMBAMREAQEEBL8TAAD/////AQAAAARggAoBAAAA" +
           "AQAbAAAAU3lzdGVtT3BlcmF0aW9uU3RhdGVNYWNoaW5lAQHAEwAvAQH9A8ATAAABAAAAACkAAQAHCQMA" +
           "AAAVYIkKAgAAAAAADAAAAEN1cnJlbnRTdGF0ZQEBEhgALwEAyAoSGAAAABX/////AQH/////AQAAABVg" +
           "iQoCAAAAAAACAAAASWQBARMYAC4ARBMYAAAAEf////8BAf////8AAAAAFWCJCgIAAAAAAA4AAABMYXN0" +
           "VHJhbnNpdGlvbgEBFBgALwEAzwoUGAAAABX/////AQH/////AQAAABVgiQoCAAAAAAACAAAASWQBARUY" +
           "AC4ARBUYAAAAEf////8BAf////8AAAAAFWCJCgIAAAABABQAAABMYXN0VHJhbnNpdGlvblJlYXNvbgEB" +
           "FhgALwEA5isWGAAAAAT/////AwP/////AgAAABdgqQoCAAAAAAAKAAAARW51bVZhbHVlcwEBFxgALgBE" +
           "FxgAAJYGAAAAAQA7IAFAAAAAAAAAAAAAAAADAgAAAGVuBwAAAFVua25vd24DAgAAAGVuGwAAAENhdXNl" +
           "ZCBieSBhbiB1bmtub3duIHJlYXNvbgEAOyABQgAAAAEAAAAAAAAAAwIAAABlbggAAABFeHRlcm5hbAMC" +
           "AAAAZW4cAAAAQ2F1c2VkIGJ5IGV4dGVybmFsIG9wZXJhdGlvbgEAOyABPgAAAAIAAAAAAAAAAwIAAABl" +
           "bgYAAABEaXJlY3QDAgAAAGVuGgAAAENhdXNlZCBieSBkaXJlY3Qgb3BlcmF0aW9uAQA7IAFGAAAAAwAA" +
           "AAAAAAADAgAAAGVuBgAAAFN5c3RlbQMCAAAAZW4iAAAAQ2F1c2VkIGJ5IHN5c3RlbSBzcGVjaWZpYyBi" +
           "ZWhhdmlvcgEAOyABNQAAAAQAAAAAAAAAAwIAAABlbgUAAABFcnJvcgMCAAAAZW4SAAAAQ2F1c2VkIGJ5" +
           "IGFuIGVycm9yAQA7IAFUAAAABQAAAAAAAAADAgAAAGVuCwAAAEFwcGxpY2F0aW9uAwIAAABlbisAAABD" +
           "YXVzZWQgZXhwbGljaXRseSBieSBlbmQgdXNlciBwcm9ncmFtIGxvZ2ljAQCqHQEAAAABAAAAAAAAAAEB" +
           "/////wAAAAAVYKkKAgAAAAAACwAAAFZhbHVlQXNUZXh0AQEYGAAuAEQYGAAAFQIHAAAASW52YWxpZAAV" +
           "/////wEB/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAFgAAAENvbnRyb2xsZXJUeXBlSW5zdGFuY2UBAesDAQHrA+sDAAD/////EAAAACRggAoB" +
           "AAAAAgAMAAAAUGFyYW1ldGVyU2V0AQGMEwMAAAAAFwAAAEZsYXQgbGlzdCBvZiBQYXJhbWV0ZXJzAC8A" +
           "OowTAAD/////CAAAADVgiQoCAAAAAQAPAAAAQ2FiaW5ldEZhblNwZWVkAQHVQwMAAAAAHQAAAFRoZSBz" +
           "cGVlZCBvZiB0aGUgY2FiaW5ldCBmYW4uAC8BAFlE1UMAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAA" +
           "EAAAAEVuZ2luZWVyaW5nVW5pdHMBAdpDAC4ARNpDAAABAHcD/////wEB/////wAAAAA1YIkKAgAAAAEA" +
           "CwAAAENQVUZhblNwZWVkAQHbQwMAAAAAGQAAAFRoZSBzcGVlZCBvZiB0aGUgQ1BVIGZhbi4ALwEAWUTb" +
           "QwAAAAv/////AQH/////AQAAABVgiQoCAAAAAAAQAAAARW5naW5lZXJpbmdVbml0cwEB4EMALgBE4EMA" +
           "AAEAdwP/////AQH/////AAAAADVgiQoCAAAAAQAMAAAASW5wdXRWb2x0YWdlAQHhQwMAAAAAvAAAAFRo" +
           "ZSBpbnB1dCB2b2x0YWdlIG9mIHRoZSBjb250cm9sbGVyIHdoaWNoIGNhbiBiZSBhIGNvbmZpZ3VyZWQg" +
           "dmFsdWUuIFRvIGRpc3Rpbmd1aXNoIGJldHdlZW4gYW4gQUMgb3IgREMgc3VwcGx5IHRoZSBvcHRpb25h" +
           "bCBwcm9wZXJ0eSBEZWZpbml0aW9uIG9mIHRoZSBiYXNlIHR5cGUgRGF0YUl0ZW1UeXBlIHNoYWxsIGJl" +
           "IHVzZWQuAC8BAFlE4UMAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5p" +
           "dHMBAeZDAC4AROZDAAABAHcD/////wEB/////wAAAAA1YIkKAgAAAAEACwAAAFN0YXJ0VXBUaW1lAQEG" +
           "PAMAAAAAOQAAAFRoZSBkYXRlIGFuZCB0aW1lIG9mIHRoZSBsYXN0IHN0YXJ0LXVwIG9mIHRoZSBjb250" +
           "cm9sbGVyLgAvAD8GPAAAAA3/////AQH/////AAAAADVgiQoCAAAAAQALAAAAVGVtcGVyYXR1cmUBAedD" +
           "AwAAAABSAAAAVGhlIGNvbnRyb2xsZXIgdGVtcGVyYXR1cmUgZ2l2ZW4gYnkgYSB0ZW1wZXJhdHVyZSBz" +
           "ZW5zb3IgaW5zaWRlIG9mIHRoZSBjb250cm9sbGVyLgAvAQBZROdDAAAAC/////8BAf////8BAAAAFWCJ" +
           "CgIAAAAAABAAAABFbmdpbmVlcmluZ1VuaXRzAQHsQwAuAETsQwAAAQB3A/////8BAf////8AAAAANWCJ" +
           "CgIAAAABABYAAABUb3RhbEVuZXJneUNvbnN1bXB0aW9uAQHPQwMAAAAAYgAAAFRoZSB0b3RhbCBhY2N1" +
           "bXVsYXRlZCBlbmVyZ3kgY29uc3VtZWQgYnkgdGhlIG1vdGlvbiBkZXZpY2VzIHJlbGF0ZWQgd2l0aCB0" +
           "aGlzIGNvbnRyb2xsZXIgaW5zdGFuY2UuAC8BAFlEz0MAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAA" +
           "EAAAAEVuZ2luZWVyaW5nVW5pdHMBAdRDAC4ARNRDAAABAHcD/////wEB/////wAAAAA1YIkKAgAAAAEA" +
           "EAAAAFRvdGFsUG93ZXJPblRpbWUBAc5DAwAAAAA5AAAAVGhlIHRvdGFsIGFjY3VtdWxhdGVkIHRpbWUg" +
           "dGhlIGNvbnRyb2xsZXIgd2FzIHBvd2VyZWQgb24uAC8AP85DAAABAE8y/////wEB/////wAAAAA1YIkK" +
           "AgAAAAEACAAAAFVwc1N0YXRlAQEFPAMAAAAARgAAAFRoZSB2ZW5kb3Igc3BlY2lmaWMgc3RhdHVzIG9m" +
           "IGFuIGludGVncmF0ZWQgVVBTIG9yIGFjY3VtdWxhdG9yIHN5c3RlbS4ALwA/BTwAAAAM/////wEB////" +
           "/wAAAAAVYIkKAgAAAAIADAAAAE1hbnVmYWN0dXJlcgEBVUMALgBEVUMAAAAV/////wEB/////wAAAAAV" +
           "YIkKAgAAAAIABQAAAE1vZGVsAQFXQwAuAERXQwAAABX/////AQH/////AAAAABVgiQoCAAAAAgALAAAA" +
           "UHJvZHVjdENvZGUBAV1DAC4ARF1DAAAADP////8BAf////8AAAAAFWCJCgIAAAACAAwAAABEZXZpY2VN" +
           "YW51YWwBASYYAC4ARCYYAAAADP////8BAf////8AAAAAFWCJCgIAAAACAAwAAABTZXJpYWxOdW1iZXIB" +
           "AVhDAC4ARFhDAAAADP////8BAf////8AAAAAFWCJCgIAAAACAAcAAABBc3NldElkAQEkGAAuAEQkGAAA" +
           "AAz/////AQH/////AAAAABVgiQoCAAAAAgANAAAAQ29tcG9uZW50TmFtZQEBJRgALgBEJRgAAAAV////" +
           "/wEB/////wAAAAAkYMAKAQAAACIAAABNb3Rpb25EZXZpY2VJZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAY" +
           "AAAAPE1vdGlvbkRldmljZUlkZW50aWZpZXI+AQEUSgMAAAAAMwAAAFRoZSByZWxhdGlvbnNoaXAgb2Yg" +
           "YSBtb3Rpb24gZGV2aWNlIGFuZCBjb250cm9sbGVyLgEBog8BAewDFEoAAP////8JAAAAJGCACgEAAAAC" +
           "AAwAAABQYXJhbWV0ZXJTZXQBARVKAwAAAAAXAAAARmxhdCBsaXN0IG9mIFBhcmFtZXRlcnMALwA6FUoA" +
           "AP////8BAAAANWCJCgIAAAABAA0AAABTcGVlZE92ZXJyaWRlAQE5SgMAAAAAWwAAAFNwZWVkT3ZlcnJp" +
           "ZGUgcHJvdmlkZXMgdGhlIGN1cnJlbnQgc3BlZWQgc2V0dGluZyBpbiBwZXJjZW50IG9mIHByb2dyYW1t" +
           "ZWQgc3BlZWQgKDAgLSAxMDAlKS4ALwA/OUoAAAAL/////wEB/////wAAAAAVYIkKAgAAAAIADAAAAE1h" +
           "bnVmYWN0dXJlcgEBKUoALgBEKUoAAAAV/////wEB/////wAAAAAVYIkKAgAAAAIABQAAAE1vZGVsAQEr" +
           "SgAuAEQrSgAAABX/////AQH/////AAAAABVgiQoCAAAAAgALAAAAUHJvZHVjdENvZGUBATFKAC4ARDFK" +
           "AAAADP////8BAf////8AAAAAFWCJCgIAAAACAAwAAABTZXJpYWxOdW1iZXIBASxKAC4ARCxKAAAADP//" +
           "//8BAf////8AAAAAJGCACgEAAAABAAQAAABBeGVzAQE6SgMAAAAAPgAAAEF4ZXMgaXMgYSBjb250YWlu" +
           "ZXIgZm9yIG9uZSBvciBtb3JlIGluc3RhbmNlcyBvZiB0aGUgQXhpc1R5cGUuAC8APTpKAAD/////AAAA" +
           "ADVgiQoCAAAAAQAUAAAATW90aW9uRGV2aWNlQ2F0ZWdvcnkBATZKAwAAAACCAAAAVGhlIHZhcmlhYmxl" +
           "IE1vdGlvbkRldmljZUNhdGVnb3J5IHByb3ZpZGVzIHRoZSBraW5kIG9mIG1vdGlvbiBkZXZpY2UgZGVm" +
           "aW5lZCBieSBNb3Rpb25EZXZpY2VDYXRlZ29yeUVudW1lcmF0aW9uIGJhc2VkIG9uIElTTyA4MzczLgAu" +
           "AEQ2SgAAAQERR/////8BAf////8AAAAAJGCACgEAAAABAAsAAABQb3dlclRyYWlucwEBiEoDAAAAAEsA" +
           "AABQb3dlclRyYWlucyBpcyBhIGNvbnRhaW5lciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9mIHRo" +
           "ZSBQb3dlclRyYWluVHlwZS4ALwA9iEoAAP////8AAAAAFWCJCgIAAAABABQAAABUYXNrQ29udHJvbFJl" +
           "ZmVyZW5jZQEBcxcALwA/cxcAAAAR/////wEB/////wAAAAAkYMAKAQAAACIAAABTYWZldHlTdGF0ZXNJ" +
           "ZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAYAAAAPFNhZmV0eVN0YXRlc0lkZW50aWZpZXI+AQHmSQMAAAAA" +
           "MgAAAFRoZSByZWxhdGlvbnNoaXAgb2Ygc2FmZXR5IHN0YXRlcyB0byBhIGNvbnRyb2xsZXIuAQEGRwEB" +
           "9QPmSQAA/////wEAAAAkYIAKAQAAAAIADAAAAFBhcmFtZXRlclNldAEB50kDAAAAABcAAABGbGF0IGxp" +
           "c3Qgb2YgUGFyYW1ldGVycwAvADrnSQAA/////wMAAAA1YIkKAgAAAAEADQAAAEVtZXJnZW5jeVN0b3AB" +
           "ARJKAwAAAAAeAQAAVGhlIEVtZXJnZW5jeVN0b3AgdmFyaWFibGUgaXMgVFJVRSBpZiBvbmUgb3IgbW9y" +
           "ZSBvZiB0aGUgZW1lcmdlbmN5IHN0b3AgZnVuY3Rpb25zIGluIHRoZSByb2JvdCBzeXN0ZW0gYXJlIGFj" +
           "dGl2ZSwgRkFMU0Ugb3RoZXJ3aXNlLiBJZiB0aGUgRW1lcmdlbmN5U3RvcEZ1bmN0aW9ucyBvYmplY3Qg" +
           "aXMgcHJvdmlkZWQsIHRoZW4gdGhlIHZhbHVlIG9mIHRoaXMgdmFyaWFibGUgaXMgVFJVRSBpZiBvbmUg" +
           "b3IgbW9yZSBvZiB0aGUgbGlzdGVkIGVtZXJnZW5jeSBzdG9wIGZ1bmN0aW9ucyBhcmUgYWN0aXZlLgAv" +
           "AD8SSgAAAAH/////AQH/////AAAAADVgiQoCAAAAAQAPAAAAT3BlcmF0aW9uYWxNb2RlAQERSgMAAAAA" +
           "wAAAAFRoZSBPcGVyYXRpb25hbE1vZGUgdmFyaWFibGUgcHJvdmlkZXMgaW5mb3JtYXRpb24gYWJvdXQg" +
           "dGhlIGN1cnJlbnQgb3BlcmF0aW9uYWwgbW9kZS4gQWxsb3dlZCB2YWx1ZXMgYXJlIGRlc2NyaWJlZCBp" +
           "biBPcGVyYXRpb25hbE1vZGVFbnVtZXJhdGlvbiwgc2VlIElTTyAxMDIxOC0xOjIwMTEgQ2guNS43IE9w" +
           "ZXJhdGlvbmFsIE1vZGVzLgAvAD8RSgAAAQG+C/////8BAf////8AAAAANWCJCgIAAAABAA4AAABQcm90" +
           "ZWN0aXZlU3RvcAEBE0oDAAAAADABAABUaGUgUHJvdGVjdGl2ZVN0b3AgdmFyaWFibGUgaXMgVFJVRSBp" +
           "ZiBvbmUgb3IgbW9yZSBvZiB0aGUgZW5hYmxlZCBwcm90ZWN0aXZlIHN0b3AgZnVuY3Rpb25zIGluIHRo" +
           "ZSBzeXN0ZW0gYXJlIGFjdGl2ZSwgRkFMU0Ugb3RoZXJ3aXNlLiBJZiB0aGUgUHJvdGVjdGl2ZVN0b3BG" +
           "dW5jdGlvbnMgb2JqZWN0IGlzIHByb3ZpZGVkLCB0aGVuIHRoZSB2YWx1ZSBvZiB0aGlzIHZhcmlhYmxl" +
           "IGlzIFRSVUUgaWYgb25lIG9yIG1vcmUgb2YgdGhlIGxpc3RlZCBwcm90ZWN0aXZlIHN0b3AgZnVuY3Rp" +
           "b25zIGFyZSBlbmFibGVkIGFuZCBhY3RpdmUuAC8APxNKAAAAAf////8BAf////8AAAAAJGCACgEAAAAB" +
           "AAoAAABDb21wb25lbnRzAQFkQwMAAAAAGwEAAENvbXBvbmVudHMgaXMgYSBjb250YWluZXIgZm9yIG9u" +
           "ZSBvciBtb3JlIGluc3RhbmNlcyBvZiBzdWJ0eXBlcyBvZiBDb21wb25lbnRUeXBlIGRlZmluZWQgaW4g" +
           "T1BDIFVBIERJLiBUaGUgbGlzdGVkIGNvbXBvbmVudHMgYXJlIGluc3RhbGxlZCBpbiB0aGUgbW90aW9u" +
           "IGRldmljZSBzeXN0ZW0sIGUuZy4gYSBwcm9jZXNzaW5nLXVuaXQsIGEgcG93ZXItc3VwcGx5LCBhbiBJ" +
           "Ty1ib2FyZCBvciBhIGRyaXZlLCBhbmQgaGF2ZSBhbiBlbGVjdHJpY2FsIGludGVyZmFjZSB0byB0aGUg" +
           "Y29udHJvbGxlci4ALwA9ZEMAAP////8BAAAAJGDACgEAAAAfAAAAQ29tcG9uZW50SWRlbnRpZmllcl9Q" +
           "bGFjZWhvbGRlcgEAFQAAADxDb21wb25lbnRJZGVudGlmaWVyPgEBfUkDAAAAAHkAAABUaGUgaW50ZW50" +
           "aW9uIGlzIHRvIGludGVncmF0ZSBpbnNpZGUgdGhpcyBjb250YWluZXIgZGV2aWNlcyB3aGljaCBhcmUg" +
           "ZGVmaW5lZCBpbiBvdGhlciBjb21wYW5pb24gc3BlY2lmaWNhdGlvbnMgdXNpbmcgREkuAC8BAtc6fUkA" +
           "AAIAAAABAMNEAAECuzoBAMNEAAECyDoAAAAAJGCACgEAAAABAAsAAABDdXJyZW50VXNlcgEBYUMDAAAA" +
           "AB8AAABUaGUgY3VycmVudCB1c2VyIG9mIHRoZSBzeXN0ZW0uAC8BAf9GYUMAAP////8BAAAANWCJCgIA" +
           "AAABAAUAAABMZXZlbAEBYkMDAAAAADUAAABUaGUgd2VpZ2h0IG9mIHRoZSBsb2FkIG1vdW50ZWQgb24g" +
           "b25lIG1vdW50aW5nIHBvaW50LgAuAERiQwAAAAz/////AQH/////AAAAAARggAoBAAAAAQAIAAAAUHJv" +
           "Z3JhbXMBAb4TAC8BACk0vhMAAP////8FAAAABGGCCgQAAAAAAA8AAABDcmVhdGVEaXJlY3RvcnkBAWcb" +
           "AC8BAEs0ZxsAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQELGAAuAEQLGAAA" +
           "lgEAAAABACoBARwAAAANAAAARGlyZWN0b3J5TmFtZQAM/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB" +
           "/////wAAAAAXYKkKAgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBDBgALgBEDBgAAJYBAAAAAQAqAQEe" +
           "AAAADwAAAERpcmVjdG9yeU5vZGVJZAAR/////wAAAAAAAQAoAQEAAAABAAAAAQAAAAEB/////wAAAAAE" +
           "YYIKBAAAAAAACgAAAENyZWF0ZUZpbGUBAWgbAC8BAE40aBsAAAEB/////wIAAAAXYKkKAgAAAAAADgAA" +
           "AElucHV0QXJndW1lbnRzAQENGAAuAEQNGAAAlgIAAAABACoBARcAAAAIAAAARmlsZU5hbWUADP////8A" +
           "AAAAAAEAKgEBHgAAAA8AAABSZXF1ZXN0RmlsZU9wZW4AAf////8AAAAAAAEAKAEBAAAAAQAAAAIAAAAB" +
           "Af////8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAQ4YAC4ARA4YAACWAgAAAAEAKgEB" +
           "GQAAAAoAAABGaWxlTm9kZUlkABH/////AAAAAAABACoBARkAAAAKAAAARmlsZUhhbmRsZQAH/////wAA" +
           "AAAAAQAoAQEAAAABAAAAAgAAAAEB/////wAAAAAEYcIIBAAAABYAAABEZWxldGVGaWxlU3lzdGVtT2Jq" +
           "ZWN0AAAGAAAARGVsZXRlAQEAAAAvAQBRNAEB/////wEAAAAXYKkIAgAAAAAADgAAAElucHV0QXJndW1l" +
           "bnRzAQEAAAAuAESWAQAAAAEAKgEBHQAAAA4AAABPYmplY3RUb0RlbGV0ZQAR/////wAAAAAAAQAoAQEA" +
           "AAABAAAAAQAAAAEB/////wAAAAAEYYIKBAAAAAAACgAAAE1vdmVPckNvcHkBAWobAC8BAFM0ahsAAAEB" +
           "/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQEQGAAuAEQQGAAAlgQAAAABACoBASEA" +
           "AAASAAAAT2JqZWN0VG9Nb3ZlT3JDb3B5ABH/////AAAAAAABACoBAR4AAAAPAAAAVGFyZ2V0RGlyZWN0" +
           "b3J5ABH/////AAAAAAABACoBARkAAAAKAAAAQ3JlYXRlQ29weQAB/////wAAAAAAAQAqAQEWAAAABwAA" +
           "AE5ld05hbWUADP////8AAAAAAAEAKAEBAAAAAQAAAAQAAAABAf////8AAAAAF2CpCgIAAAAAAA8AAABP" +
           "dXRwdXRBcmd1bWVudHMBAREYAC4ARBEYAACWAQAAAAEAKgEBGAAAAAkAAABOZXdOb2RlSWQAEf////8A" +
           "AAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8AAAAABGGCCgQAAAAAAAYAAABEZWxldGUBAWkbAC8BAWkb" +
           "aRsAAAEB/////wEAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQEPGAAuAEQPGAAAlgEAAAAB" +
           "ACoBAR0AAAAOAAAAT2JqZWN0VG9EZWxldGUAEf////8AAAAAAAEAKAEBAAAAAQAAAAEAAAABAf////8A" +
           "AAAAJGCACgEAAAABAAgAAABTb2Z0d2FyZQEBuD0DAAAAAFcAAABTb2Z0d2FyZSBpcyBhIGNvbnRhaW5l" +
           "ciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9mIFNvZnR3YXJlVHlwZSBkZWZpbmVkIGluIE9QQyBV" +
           "QSBESS4ALwA9uD0AAP////8BAAAABGDACgEAAAAeAAAAU29mdHdhcmVJZGVudGlmaWVyX1BsYWNlaG9s" +
           "ZGVyAQAUAAAAPFNvZnR3YXJlSWRlbnRpZmllcj4BAZ9JAC8BAgI7n0kAAP////8DAAAAFWCJCgIAAAAC" +
           "AAwAAABNYW51ZmFjdHVyZXIBAbRJAC4ARLRJAAAAFf////8BAf////8AAAAAFWCJCgIAAAACAAUAAABN" +
           "b2RlbAEBtkkALgBEtkkAAAAV/////wEB/////wAAAAAVYIkKAgAAAAIAEAAAAFNvZnR3YXJlUmV2aXNp" +
           "b24BAblJAC4ARLlJAAAADP////8BAf////8AAAAABGCACgEAAAABAA8AAABTeXN0ZW1PcGVyYXRpb24B" +
           "Ab8TAQDERAEBBAS/EwAA/////wEAAAAEYIAKAQAAAAEAGwAAAFN5c3RlbU9wZXJhdGlvblN0YXRlTWFj" +
           "aGluZQEBwBMALwEB/QPAEwAAAQAAAAApAAEABwkDAAAAFWCJCgIAAAAAAAwAAABDdXJyZW50U3RhdGUB" +
           "ARIYAC8BAMgKEhgAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQETGAAuAEQTGAAAABH/" +
           "////AQH/////AAAAABVgiQoCAAAAAAAOAAAATGFzdFRyYW5zaXRpb24BARQYAC8BAM8KFBgAAAAV////" +
           "/wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQEVGAAuAEQVGAAAABH/////AQH/////AAAAABVgiQoC" +
           "AAAAAQAUAAAATGFzdFRyYW5zaXRpb25SZWFzb24BARYYAC8BAOYrFhgAAAAE/////wMD/////wIAAAAX" +
           "YKkKAgAAAAAACgAAAEVudW1WYWx1ZXMBARcYAC4ARBcYAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIA" +
           "AABlbgcAAABVbmtub3duAwIAAABlbhsAAABDYXVzZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIA" +
           "AAABAAAAAAAAAAMCAAAAZW4IAAAARXh0ZXJuYWwDAgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBv" +
           "cGVyYXRpb24BADsgAT4AAAACAAAAAAAAAAMCAAAAZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQg" +
           "YnkgZGlyZWN0IG9wZXJhdGlvbgEAOyABRgAAAAMAAAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVu" +
           "IgAAAENhdXNlZCBieSBzeXN0ZW0gc3BlY2lmaWMgYmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAA" +
           "ZW4FAAAARXJyb3IDAgAAAGVuEgAAAENhdXNlZCBieSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIA" +
           "AABlbgsAAABBcHBsaWNhdGlvbgMCAAAAZW4rAAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIg" +
           "cHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAAAAAAAAABAf////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFz" +
           "VGV4dAEBGBgALgBEGBgAABUCBwAAAEludmFsaWQAFf////8BAf////8AAAAAJGCACgEAAAABAAwAAABU" +
           "YXNrQ29udHJvbHMBAdI9AwAAAABJAAAAVGFza0NvbnRyb2xzIGlzIGEgY29udGFpbmVyIGZvciBvbmUg" +
           "b3IgbW9yZSBpbnN0YW5jZXMgb2YgVGFza0NvbnRyb2xUeXBlLgAvAD3SPQAA/////wEAAAAEYMAKAQAA" +
           "ACEAAABUYXNrQ29udHJvbElkZW50aWZpZXJfUGxhY2Vob2xkZXIBABcAAAA8VGFza0NvbnRyb2xJZGVu" +
           "dGlmaWVyPgEBwUkALwEB8wPBSQAA/////wIAAAAkYIAKAQAAAAIADAAAAFBhcmFtZXRlclNldAEBwkkD" +
           "AAAAABcAAABGbGF0IGxpc3Qgb2YgUGFyYW1ldGVycwAvADrCSQAA/////wIAAAA1YIkKAgAAAAEAEQAA" +
           "AFRhc2tQcm9ncmFtTG9hZGVkAQHkSQMAAAAAaAAAAFRoZSBUYXNrUHJvZ3JhbUxvYWRlZCB2YXJpYWJs" +
           "ZSBpcyBUUlVFIGlmIGEgdGFzayBwcm9ncmFtIGlzIGxvYWRlZCBpbiB0aGUgdGFzayBjb250cm9sLCBG" +
           "QUxTRSBvdGhlcndpc2UuAC8AP+RJAAAAAf////8BAf////8AAAAANWCJCgIAAAABAA8AAABUYXNrUHJv" +
           "Z3JhbU5hbWUBAeNJAwAAAAAxAAAAQSBjdXN0b21lciBnaXZlbiBpZGVudGlmaWVyIGZvciB0aGUgdGFz" +
           "ayBwcm9ncmFtLgAvAD/jSQAAAAz/////AQH/////AAAAADVgiQoCAAAAAgANAAAAQ29tcG9uZW50TmFt" +
           "ZQEB4kkDAAAAAE4AAABBIHVzZXIgd3JpdGFibGUgbmFtZSBwcm92aWRlZCBieSB0aGUgdmVuZG9yLCBp" +
           "bnRlZ3JhdG9yIG9yIHVzZXIgb2YgdGhlIGRldmljZS4ALgBE4kkAAAAV/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public FolderState Components
        {
            get => m_components;

            set
            {
                if (!Object.ReferenceEquals(m_components, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_components = value;
            }
        }

        public UserTypeState CurrentUser
        {
            get => m_currentUser;

            set
            {
                if (!Object.ReferenceEquals(m_currentUser, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_currentUser = value;
            }
        }

        public FileDirectoryState Programs
        {
            get => m_programs;

            set
            {
                if (!Object.ReferenceEquals(m_programs, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_programs = value;
            }
        }

        public FolderState Software
        {
            get => m_software;

            set
            {
                if (!Object.ReferenceEquals(m_software, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_software = value;
            }
        }

        public SystemOperationTypeState SystemOperation
        {
            get => m_systemOperation;

            set
            {
                if (!Object.ReferenceEquals(m_systemOperation, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_systemOperation = value;
            }
        }

        public FolderState TaskControls
        {
            get => m_taskControls;

            set
            {
                if (!Object.ReferenceEquals(m_taskControls, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_taskControls = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_components != null)
            {
                children.Add(m_components);
            }

            if (m_currentUser != null)
            {
                children.Add(m_currentUser);
            }

            if (m_programs != null)
            {
                children.Add(m_programs);
            }

            if (m_software != null)
            {
                children.Add(m_software);
            }

            if (m_systemOperation != null)
            {
                children.Add(m_systemOperation);
            }

            if (m_taskControls != null)
            {
                children.Add(m_taskControls);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_components, child))
            {
                m_components = null;
                return;
            }

            if (Object.ReferenceEquals(m_currentUser, child))
            {
                m_currentUser = null;
                return;
            }

            if (Object.ReferenceEquals(m_programs, child))
            {
                m_programs = null;
                return;
            }

            if (Object.ReferenceEquals(m_software, child))
            {
                m_software = null;
                return;
            }

            if (Object.ReferenceEquals(m_systemOperation, child))
            {
                m_systemOperation = null;
                return;
            }

            if (Object.ReferenceEquals(m_taskControls, child))
            {
                m_taskControls = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.Components:
                {
                    if (createOrReplace)
                    {
                        if (Components == null)
                        {
                            if (replacement == null)
                            {
                                Components = new FolderState(this);
                            }
                            else
                            {
                                Components = (FolderState)replacement;
                            }
                        }
                    }

                    instance = Components;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.CurrentUser:
                {
                    if (createOrReplace)
                    {
                        if (CurrentUser == null)
                        {
                            if (replacement == null)
                            {
                                CurrentUser = new UserTypeState(this);
                            }
                            else
                            {
                                CurrentUser = (UserTypeState)replacement;
                            }
                        }
                    }

                    instance = CurrentUser;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Programs:
                {
                    if (createOrReplace)
                    {
                        if (Programs == null)
                        {
                            if (replacement == null)
                            {
                                Programs = new FileDirectoryState(this);
                            }
                            else
                            {
                                Programs = (FileDirectoryState)replacement;
                            }
                        }
                    }

                    instance = Programs;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Software:
                {
                    if (createOrReplace)
                    {
                        if (Software == null)
                        {
                            if (replacement == null)
                            {
                                Software = new FolderState(this);
                            }
                            else
                            {
                                Software = (FolderState)replacement;
                            }
                        }
                    }

                    instance = Software;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.SystemOperation:
                {
                    if (createOrReplace)
                    {
                        if (SystemOperation == null)
                        {
                            if (replacement == null)
                            {
                                SystemOperation = new SystemOperationTypeState(this);
                            }
                            else
                            {
                                SystemOperation = (SystemOperationTypeState)replacement;
                            }
                        }
                    }

                    instance = SystemOperation;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.TaskControls:
                {
                    if (createOrReplace)
                    {
                        if (TaskControls == null)
                        {
                            if (replacement == null)
                            {
                                TaskControls = new FolderState(this);
                            }
                            else
                            {
                                TaskControls = (FolderState)replacement;
                            }
                        }
                    }

                    instance = TaskControls;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private FolderState m_components;
        private UserTypeState m_currentUser;
        private FileDirectoryState m_programs;
        private FolderState m_software;
        private SystemOperationTypeState m_systemOperation;
        private FolderState m_taskControls;
        #endregion
    }
    #endif
    #endregion

    #region DriveTypeState Class
    #if (!OPCUA_EXCLUDE_DriveTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class DriveTypeState : ComponentTypeState
    {
        #region Constructors
        public DriveTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.DriveType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }
        }

        #region Initialization String
        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBASgYAC4ARCgYAAAADP////8BAf////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAEQAAAERyaXZlVHlwZUluc3RhbmNlAQGBRQEBgUWBRQAA/////wIAAAA1YIkKAgAAAAIA" +
           "CwAAAFByb2R1Y3RDb2RlAQGgRQMAAAAAuAAAAFRoZSBQcm9kdWN0Q29kZSBwcm9wZXJ0eSBwcm92aWRl" +
           "cyBhIHVuaXF1ZSBjb21iaW5hdGlvbiBvZiBudW1iZXJzIGFuZCBsZXR0ZXJzIHVzZWQgdG8gaWRlbnRp" +
           "ZnkgdGhlIHByb2R1Y3QuIEl0IG1heSBiZSB0aGUgb3JkZXIgaW5mb3JtYXRpb24gZGlzcGxheWVkIG9u" +
           "IHR5cGUgc2hpZWxkcyBvciBpbiBFUlAgc3lzdGVtcy4ALgBEoEUAAAAM/////wEB/////wAAAAAVYIkK" +
           "AgAAAAIABwAAAEFzc2V0SWQBASgYAC4ARCgYAAAADP////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region GearTypeState Class
    #if (!OPCUA_EXCLUDE_GearTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GearTypeState : ComponentTypeState
    {
        #region Constructors
        public GearTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.GearType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }

            if (Pitch != null)
            {
                Pitch.Initialize(context, Pitch_InitializationString);
            }
        }

        #region Initialization String
        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBASIYAC4ARCIYAAAADP////8BAf////8AAAAA";

        private const string Pitch_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////81" +
           "YIkKAgAAAAEABQAAAFBpdGNoAQENQwMAAAAA+AAAAFBpdGNoIGRlc2NyaWJlcyB0aGUgZGlzdGFuY2Ug" +
           "Y292ZXJlZCBpbiBtaWxsaW1ldGVycyAobW0pIGZvciBsaW5lYXIgbW90aW9uIHBlciBvbmUgcmV2b2x1" +
           "dGlvbiBvZiB0aGUgb3V0cHV0IHNpZGUgb2YgdGhlIGRyaXZpbmcgdW5pdC4gUGl0Y2ggaXMgdXNlZCBp" +
           "biBjb21iaW5hdGlvbiB3aXRoIEdlYXJSYXRpbyB0byBkZXNjcmliZSB0aGUgb3ZlcmFsbCB0cmFuc21p" +
           "c3Npb24gZnJvbSBpbnB1dCB0byBvdXRwdXQgb2YgdGhlIGdlYXIuAC8APw1DAAAAC/////8BAf////8A" +
           "AAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAEAAAAEdlYXJUeXBlSW5zdGFuY2UBAf4DAQH+A/4DAAD/////BwAAABVgiQoCAAAAAgAM" +
           "AAAATWFudWZhY3R1cmVyAQEAQwAuAEQAQwAAABX/////AQH/////AAAAABVgiQoCAAAAAgAFAAAATW9k" +
           "ZWwBAQJDAC4ARAJDAAAAFf////8BAf////8AAAAAFWCJCgIAAAACAAsAAABQcm9kdWN0Q29kZQEBCEMA" +
           "LgBECEMAAAAM/////wEB/////wAAAAAVYIkKAgAAAAIADAAAAFNlcmlhbE51bWJlcgEBA0MALgBEA0MA" +
           "AAAM/////wEB/////wAAAAAVYIkKAgAAAAIABwAAAEFzc2V0SWQBASIYAC4ARCIYAAAADP////8BAf//" +
           "//8AAAAANWCJCgIAAAABAAkAAABHZWFyUmF0aW8BAUU+AwAAAAB5AAAAVGhlIHRyYW5zbWlzc2lvbiBy" +
           "YXRpbyBvZiB0aGUgZ2VhciBleHByZXNzZWQgYXMgYSBmcmFjdGlvbiBhcyBpbnB1dCB2ZWxvY2l0eSAo" +
           "bW90b3Igc2lkZSkgYnkgb3V0cHV0IHZlbG9jaXR5IChsb2FkIHNpZGUpLgAvAQAtRUU+AAABAHZJ////" +
           "/wEB/////wIAAAAVYIkKAgAAAAAACQAAAE51bWVyYXRvcgEB/zwALwA//zwAAAAG/////wEB/////wAA" +
           "AAAVYIkKAgAAAAAACwAAAERlbm9taW5hdG9yAQEAPQAvAD8APQAAAAf/////AQH/////AAAAADVgiQoC" +
           "AAAAAQAFAAAAUGl0Y2gBAQ1DAwAAAAD4AAAAUGl0Y2ggZGVzY3JpYmVzIHRoZSBkaXN0YW5jZSBjb3Zl" +
           "cmVkIGluIG1pbGxpbWV0ZXJzIChtbSkgZm9yIGxpbmVhciBtb3Rpb24gcGVyIG9uZSByZXZvbHV0aW9u" +
           "IG9mIHRoZSBvdXRwdXQgc2lkZSBvZiB0aGUgZHJpdmluZyB1bml0LiBQaXRjaCBpcyB1c2VkIGluIGNv" +
           "bWJpbmF0aW9uIHdpdGggR2VhclJhdGlvIHRvIGRlc2NyaWJlIHRoZSBvdmVyYWxsIHRyYW5zbWlzc2lv" +
           "biBmcm9tIGlucHV0IHRvIG91dHB1dCBvZiB0aGUgZ2Vhci4ALwA/DUMAAAAL/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public RationalNumberState GearRatio
        {
            get => m_gearRatio;

            set
            {
                if (!Object.ReferenceEquals(m_gearRatio, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_gearRatio = value;
            }
        }

        public BaseDataVariableState<double> Pitch
        {
            get => m_pitch;

            set
            {
                if (!Object.ReferenceEquals(m_pitch, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_pitch = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_gearRatio != null)
            {
                children.Add(m_gearRatio);
            }

            if (m_pitch != null)
            {
                children.Add(m_pitch);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_gearRatio, child))
            {
                m_gearRatio = null;
                return;
            }

            if (Object.ReferenceEquals(m_pitch, child))
            {
                m_pitch = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.GearRatio:
                {
                    if (createOrReplace)
                    {
                        if (GearRatio == null)
                        {
                            if (replacement == null)
                            {
                                GearRatio = new RationalNumberState(this);
                            }
                            else
                            {
                                GearRatio = (RationalNumberState)replacement;
                            }
                        }
                    }

                    instance = GearRatio;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Pitch:
                {
                    if (createOrReplace)
                    {
                        if (Pitch == null)
                        {
                            if (replacement == null)
                            {
                                Pitch = new BaseDataVariableState<double>(this);
                            }
                            else
                            {
                                Pitch = (BaseDataVariableState<double>)replacement;
                            }
                        }
                    }

                    instance = Pitch;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private RationalNumberState m_gearRatio;
        private BaseDataVariableState<double> m_pitch;
        #endregion
    }
    #endif
    #endregion

    #region MotionDeviceSystemTypeState Class
    #if (!OPCUA_EXCLUDE_MotionDeviceSystemTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class MotionDeviceSystemTypeState : ComponentTypeState
    {
        #region Constructors
        public MotionDeviceSystemTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.MotionDeviceSystemType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ComponentName != null)
            {
                ComponentName.Initialize(context, ComponentName_InitializationString);
            }
        }

        #region Initialization String
        private const string ComponentName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADQAAAENvbXBvbmVudE5hbWUBARsYAC4ARBsYAAAAFf////8BAf////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAHgAAAE1vdGlvbkRldmljZVN5c3RlbVR5cGVJbnN0YW5jZQEB6gMBAeoD6gMAAP////8E" +
           "AAAAFWCJCgIAAAACAA0AAABDb21wb25lbnROYW1lAQEbGAAuAEQbGAAAABX/////AQH/////AAAAACRg" +
           "gAoBAAAAAQALAAAAQ29udHJvbGxlcnMBAYkTAwAAAAA8AAAAQ29udGFpbnMgdGhlIHNldCBvZiBjb250" +
           "cm9sbGVycyBpbiB0aGUgbW90aW9uIGRldmljZSBzeXN0ZW0uAC8APYkTAAD/////AQAAAARgwAoBAAAA" +
           "IAAAAENvbnRyb2xsZXJJZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAWAAAAPENvbnRyb2xsZXJJZGVudGlm" +
           "aWVyPgEBLTwALwEB6wMtPAAA/////wcAAAAVYIkKAgAAAAIADAAAAE1hbnVmYWN0dXJlcgEBQjwALgBE" +
           "QjwAAAAV/////wEB/////wAAAAAVYIkKAgAAAAIABQAAAE1vZGVsAQFEPAAuAEREPAAAABX/////AQH/" +
           "////AAAAABVgiQoCAAAAAgALAAAAUHJvZHVjdENvZGUBAUo8AC4AREo8AAAADP////8BAf////8AAAAA" +
           "FWCJCgIAAAACAAwAAABTZXJpYWxOdW1iZXIBAUU8AC4AREU8AAAADP////8BAf////8AAAAAJGCACgEA" +
           "AAABAAsAAABDdXJyZW50VXNlcgEBUDwDAAAAAB0AAABUaGUgZ2l2ZW4gbmFtZSBvZiB0aGUgZGV2aWNl" +
           "LgAvAQH/RlA8AAD/////AQAAADVgiQoCAAAAAQAFAAAATGV2ZWwBAVE8AwAAAAA1AAAAVGhlIHdlaWdo" +
           "dCBvZiB0aGUgbG9hZCBtb3VudGVkIG9uIG9uZSBtb3VudGluZyBwb2ludC4ALgBEUTwAAAAM/////wEB" +
           "/////wAAAAAkYIAKAQAAAAEACAAAAFNvZnR3YXJlAQF7PAMAAAAAVwAAAFNvZnR3YXJlIGlzIGEgY29u" +
           "dGFpbmVyIGZvciBvbmUgb3IgbW9yZSBpbnN0YW5jZXMgb2YgU29mdHdhcmVUeXBlIGRlZmluZWQgaW4g" +
           "T1BDIFVBIERJLgAvAD17PAAA/////wAAAAAkYIAKAQAAAAEADAAAAFRhc2tDb250cm9scwEBnjwDAAAA" +
           "AEkAAABUYXNrQ29udHJvbHMgaXMgYSBjb250YWluZXIgZm9yIG9uZSBvciBtb3JlIGluc3RhbmNlcyBv" +
           "ZiBUYXNrQ29udHJvbFR5cGUuAC8APZ48AAD/////AAAAACRggAoBAAAAAQANAAAATW90aW9uRGV2aWNl" +
           "cwEBihMDAAAAAFIAAABDb250YWlucyBhbnkga2luZW1hdGljIG9yIG1vdGlvbiBkZXZpY2Ugd2hpY2gg" +
           "aXMgcGFydCBvZiB0aGUgbW90aW9uIGRldmljZSBzeXN0ZW0uAC8APYoTAAD/////AQAAAARgwAoBAAAA" +
           "IgAAAE1vdGlvbkRldmljZUlkZW50aWZpZXJfUGxhY2Vob2xkZXIBABgAAAA8TW90aW9uRGV2aWNlSWRl" +
           "bnRpZmllcj4BAaA6AC8BAewDoDoAAP////8JAAAAJGCACgEAAAACAAwAAABQYXJhbWV0ZXJTZXQBAbA6" +
           "AwAAAAAXAAAARmxhdCBsaXN0IG9mIFBhcmFtZXRlcnMALwA6sDoAAP////8BAAAANWCJCgIAAAABAA0A" +
           "AABTcGVlZE92ZXJyaWRlAQHVOgMAAAAAWwAAAFNwZWVkT3ZlcnJpZGUgcHJvdmlkZXMgdGhlIGN1cnJl" +
           "bnQgc3BlZWQgc2V0dGluZyBpbiBwZXJjZW50IG9mIHByb2dyYW1tZWQgc3BlZWQgKDAgLSAxMDAlKS4A" +
           "LwA/1ToAAAAL/////wEB/////wAAAAAVYIkKAgAAAAIADAAAAE1hbnVmYWN0dXJlcgEBxToALgBExToA" +
           "AAAV/////wEB/////wAAAAAVYIkKAgAAAAIABQAAAE1vZGVsAQHHOgAuAETHOgAAABX/////AQH/////" +
           "AAAAABVgiQoCAAAAAgALAAAAUHJvZHVjdENvZGUBAc06AC4ARM06AAAADP////8BAf////8AAAAAFWCJ" +
           "CgIAAAACAAwAAABTZXJpYWxOdW1iZXIBAcg6AC4ARMg6AAAADP////8BAf////8AAAAAJGCACgEAAAAB" +
           "AAQAAABBeGVzAQHWOgMAAAAAPgAAAEF4ZXMgaXMgYSBjb250YWluZXIgZm9yIG9uZSBvciBtb3JlIGlu" +
           "c3RhbmNlcyBvZiB0aGUgQXhpc1R5cGUuAC8APdY6AAD/////AAAAADVgiQoCAAAAAQAUAAAATW90aW9u" +
           "RGV2aWNlQ2F0ZWdvcnkBAdI6AwAAAACCAAAAVGhlIHZhcmlhYmxlIE1vdGlvbkRldmljZUNhdGVnb3J5" +
           "IHByb3ZpZGVzIHRoZSBraW5kIG9mIG1vdGlvbiBkZXZpY2UgZGVmaW5lZCBieSBNb3Rpb25EZXZpY2VD" +
           "YXRlZ29yeUVudW1lcmF0aW9uIGJhc2VkIG9uIElTTyA4MzczLgAuAETSOgAAAQERR/////8BAf////8A" +
           "AAAAJGCACgEAAAABAAsAAABQb3dlclRyYWlucwEBaDsDAAAAAEsAAABQb3dlclRyYWlucyBpcyBhIGNv" +
           "bnRhaW5lciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9mIHRoZSBQb3dlclRyYWluVHlwZS4ALwA9" +
           "aDsAAP////8AAAAAFWCJCgIAAAABABQAAABUYXNrQ29udHJvbFJlZmVyZW5jZQEBchcALwA/chcAAAAR" +
           "/////wEB/////wAAAAAkYIAKAQAAAAEADAAAAFNhZmV0eVN0YXRlcwEBkhMDAAAAADcAAABDb250YWlu" +
           "cyBzYWZldHktcmVsYXRlZCBkYXRhIGZyb20gbW90aW9uIGRldmljZSBzeXN0ZW0uAC8APZITAAD/////" +
           "AQAAAARgwAoBAAAAIQAAAFNhZmV0eVN0YXRlSWRlbnRpZmllcl9QbGFjZWhvbGRlcgEAFwAAADxTYWZl" +
           "dHlTdGF0ZUlkZW50aWZpZXI+AQFRPQAvAQH1A1E9AAD/////AQAAACRggAoBAAAAAgAMAAAAUGFyYW1l" +
           "dGVyU2V0AQFSPQMAAAAAFwAAAEZsYXQgbGlzdCBvZiBQYXJhbWV0ZXJzAC8AOlI9AAD/////AwAAADVg" +
           "iQoCAAAAAQANAAAARW1lcmdlbmN5U3RvcAEBfT0DAAAAAB4BAABUaGUgRW1lcmdlbmN5U3RvcCB2YXJp" +
           "YWJsZSBpcyBUUlVFIGlmIG9uZSBvciBtb3JlIG9mIHRoZSBlbWVyZ2VuY3kgc3RvcCBmdW5jdGlvbnMg" +
           "aW4gdGhlIHJvYm90IHN5c3RlbSBhcmUgYWN0aXZlLCBGQUxTRSBvdGhlcndpc2UuIElmIHRoZSBFbWVy" +
           "Z2VuY3lTdG9wRnVuY3Rpb25zIG9iamVjdCBpcyBwcm92aWRlZCwgdGhlbiB0aGUgdmFsdWUgb2YgdGhp" +
           "cyB2YXJpYWJsZSBpcyBUUlVFIGlmIG9uZSBvciBtb3JlIG9mIHRoZSBsaXN0ZWQgZW1lcmdlbmN5IHN0" +
           "b3AgZnVuY3Rpb25zIGFyZSBhY3RpdmUuAC8AP309AAAAAf////8BAf////8AAAAANWCJCgIAAAABAA8A" +
           "AABPcGVyYXRpb25hbE1vZGUBAXw9AwAAAADAAAAAVGhlIE9wZXJhdGlvbmFsTW9kZSB2YXJpYWJsZSBw" +
           "cm92aWRlcyBpbmZvcm1hdGlvbiBhYm91dCB0aGUgY3VycmVudCBvcGVyYXRpb25hbCBtb2RlLiBBbGxv" +
           "d2VkIHZhbHVlcyBhcmUgZGVzY3JpYmVkIGluIE9wZXJhdGlvbmFsTW9kZUVudW1lcmF0aW9uLCBzZWUg" +
           "SVNPIDEwMjE4LTE6MjAxMSBDaC41LjcgT3BlcmF0aW9uYWwgTW9kZXMuAC8AP3w9AAABAb4L/////wEB" +
           "/////wAAAAA1YIkKAgAAAAEADgAAAFByb3RlY3RpdmVTdG9wAQF+PQMAAAAAMAEAAFRoZSBQcm90ZWN0" +
           "aXZlU3RvcCB2YXJpYWJsZSBpcyBUUlVFIGlmIG9uZSBvciBtb3JlIG9mIHRoZSBlbmFibGVkIHByb3Rl" +
           "Y3RpdmUgc3RvcCBmdW5jdGlvbnMgaW4gdGhlIHN5c3RlbSBhcmUgYWN0aXZlLCBGQUxTRSBvdGhlcndp" +
           "c2UuIElmIHRoZSBQcm90ZWN0aXZlU3RvcEZ1bmN0aW9ucyBvYmplY3QgaXMgcHJvdmlkZWQsIHRoZW4g" +
           "dGhlIHZhbHVlIG9mIHRoaXMgdmFyaWFibGUgaXMgVFJVRSBpZiBvbmUgb3IgbW9yZSBvZiB0aGUgbGlz" +
           "dGVkIHByb3RlY3RpdmUgc3RvcCBmdW5jdGlvbnMgYXJlIGVuYWJsZWQgYW5kIGFjdGl2ZS4ALwA/fj0A" +
           "AAAB/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public FolderState Controllers
        {
            get => m_controllers;

            set
            {
                if (!Object.ReferenceEquals(m_controllers, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_controllers = value;
            }
        }

        public FolderState MotionDevices
        {
            get => m_motionDevices;

            set
            {
                if (!Object.ReferenceEquals(m_motionDevices, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_motionDevices = value;
            }
        }

        public FolderState SafetyStates
        {
            get => m_safetyStates;

            set
            {
                if (!Object.ReferenceEquals(m_safetyStates, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_safetyStates = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_controllers != null)
            {
                children.Add(m_controllers);
            }

            if (m_motionDevices != null)
            {
                children.Add(m_motionDevices);
            }

            if (m_safetyStates != null)
            {
                children.Add(m_safetyStates);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_controllers, child))
            {
                m_controllers = null;
                return;
            }

            if (Object.ReferenceEquals(m_motionDevices, child))
            {
                m_motionDevices = null;
                return;
            }

            if (Object.ReferenceEquals(m_safetyStates, child))
            {
                m_safetyStates = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.Controllers:
                {
                    if (createOrReplace)
                    {
                        if (Controllers == null)
                        {
                            if (replacement == null)
                            {
                                Controllers = new FolderState(this);
                            }
                            else
                            {
                                Controllers = (FolderState)replacement;
                            }
                        }
                    }

                    instance = Controllers;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.MotionDevices:
                {
                    if (createOrReplace)
                    {
                        if (MotionDevices == null)
                        {
                            if (replacement == null)
                            {
                                MotionDevices = new FolderState(this);
                            }
                            else
                            {
                                MotionDevices = (FolderState)replacement;
                            }
                        }
                    }

                    instance = MotionDevices;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.SafetyStates:
                {
                    if (createOrReplace)
                    {
                        if (SafetyStates == null)
                        {
                            if (replacement == null)
                            {
                                SafetyStates = new FolderState(this);
                            }
                            else
                            {
                                SafetyStates = (FolderState)replacement;
                            }
                        }
                    }

                    instance = SafetyStates;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private FolderState m_controllers;
        private FolderState m_motionDevices;
        private FolderState m_safetyStates;
        #endregion
    }
    #endif
    #endregion

    #region MotionDeviceTypeState Class
    #if (!OPCUA_EXCLUDE_MotionDeviceTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class MotionDeviceTypeState : ComponentTypeState
    {
        #region Constructors
        public MotionDeviceTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.MotionDeviceType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AdditionalComponents != null)
            {
                AdditionalComponents.Initialize(context, AdditionalComponents_InitializationString);
            }

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }

            if (ComponentName != null)
            {
                ComponentName.Initialize(context, ComponentName_InitializationString);
            }

            if (DeviceManual != null)
            {
                DeviceManual.Initialize(context, DeviceManual_InitializationString);
            }

            if (FlangeLoad != null)
            {
                FlangeLoad.Initialize(context, FlangeLoad_InitializationString);
            }

            if (TaskControlReference != null)
            {
                TaskControlReference.Initialize(context, TaskControlReference_InitializationString);
            }
        }

        #region Initialization String
        private const string AdditionalComponents_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAEAFAAAAEFkZGl0aW9uYWxDb21wb25lbnRzAQG2QAMAAAAAvAAAAEFkZGl0aW9uYWxDb21w" +
           "b25lbnRzIGlzIGEgY29udGFpbmVyIGZvciBvbmUgb3IgbW9yZSBpbnN0YW5jZXMgb2Ygc3VidHlwZXMg" +
           "b2YgQ29tcG9uZW50VHlwZSBkZWZpbmVkIGluIE9QQyBVQSBESS4gVGhlIGxpc3RlZCBjb21wb25lbnRz" +
           "IGFyZSBpbnN0YWxsZWQgYXQgdGhlIG1vdGlvbiBkZXZpY2UsIGUuZy4gYW4gSU8tYm9hcmQuAC8APbZA" +
           "AAD/////AQAAAARgwAoBAAAAKQAAAEFkZGl0aW9uYWxDb21wb25lbnRJZGVudGlmaWVyX1BsYWNlaG9s" +
           "ZGVyAQAfAAAAPEFkZGl0aW9uYWxDb21wb25lbnRJZGVudGlmaWVyPgEBixMALwA6ixMAAP////8AAAAA";

        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBARwYAC4ARBwYAAAADP////8BAf////8AAAAA";

        private const string ComponentName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADQAAAENvbXBvbmVudE5hbWUBAR0YAC4ARB0YAAAAFf////8BAf////8AAAAA";

        private const string DeviceManual_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADAAAAERldmljZU1hbnVhbAEBHhgALgBEHhgAAAAM/////wEB/////wAAAAA=";

        private const string FlangeLoad_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAEACgAAAEZsYW5nZUxvYWQBAeMTAwAAAACIAAAAVGhlIEZsYW5nZUxvYWQgaXMgdGhlIGxv" +
           "YWQgb24gdGhlIGZsYW5nZSBvciBhdCB0aGUgbW91bnRpbmcgcG9pbnQgb2YgdGhlIE1vdGlvbkRldmlj" +
           "ZS4gVGhpcyBjYW4gYmUgdGhlIG1heGltdW0gbG9hZCBvZiB0aGUgTW90aW9uRGV2aWNlLgAvAQH6A+MT" +
           "AAD/////AQAAADVgiQoCAAAAAQAEAAAATWFzcwEB4BkDAAAAADUAAABUaGUgd2VpZ2h0IG9mIHRoZSBs" +
           "b2FkIG1vdW50ZWQgb24gb25lIG1vdW50aW5nIHBvaW50LgAvAQBZROAZAAAAC/////8BAf////8BAAAA" +
           "FWCJCgIAAAAAABAAAABFbmdpbmVlcmluZ1VuaXRzAQErPQAuAEQrPQAAAQB3A/////8BAf////8AAAAA";

        private const string TaskControlReference_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAEAFAAAAFRhc2tDb250cm9sUmVmZXJlbmNlAQFxFwAvAD9xFwAAABH/////AQH/////AAAA" +
           "AA==";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAGAAAAE1vdGlvbkRldmljZVR5cGVJbnN0YW5jZQEB7AMBAewD7AMAAP////8OAAAAJGCA" +
           "CgEAAAACAAwAAABQYXJhbWV0ZXJTZXQBAaUTAwAAAAAXAAAARmxhdCBsaXN0IG9mIFBhcmFtZXRlcnMA" +
           "LwA6pRMAAP////8DAAAANWCJCgIAAAABAAkAAABJbkNvbnRyb2wBAew/AwAAAACzAAAASW5Db250cm9s" +
           "IHByb3ZpZGVzIHRoZSBpbmZvcm1hdGlvbiBpZiB0aGUgYWN0dWF0b3JzIChpbiBtb3N0IGNhc2VzIGEg" +
           "bW90b3IpIG9mIHRoZSBtb3Rpb24gZGV2aWNlIGFyZSBwb3dlcmVkIHVwIGFuZCBpbiBjb250cm9sOiAi" +
           "dHJ1ZSIuIFRoZSBtb3Rpb24gZGV2aWNlIG1pZ2h0IGJlIGluIGEgc3RhbmRzdGlsbC4ALwA/7D8AAAAB" +
           "/////wEB/////wAAAAA1YIkKAgAAAAEABgAAAE9uUGF0aAEB6z8DAAAAAEsBAABPblBhdGggaXMgdHJ1" +
           "ZSBpZiB0aGUgbW90aW9uIGRldmljZSBpcyBvbiBvciBuZWFyIGVub3VnaCB0aGUgcGxhbm5lZCBwcm9n" +
           "cmFtIHBhdGggc3VjaCB0aGF0IHByb2dyYW0gZXhlY3V0aW9uIGNhbiBjb250aW51ZS4gSWYgdGhlIE1v" +
           "dGlvbkRldmljZSBkZXZpYXRlcyB0b28gbXVjaCBmcm9tIHRoaXMgcGF0aCBpbiBjYXNlIG9mIGVycm9y" +
           "cyBvciBhbiBlbWVyZ2VuY3kgc3RvcCwgdGhpcyB2YWx1ZSBiZWNvbWVzIGZhbHNlLiBJZiBPblBhdGgg" +
           "aXMgZmFsc2UsIHRoZSBtb3Rpb24gZGV2aWNlIG5lZWRzIHJlcG9zaXRpb25pbmcgdG8gY29udGludWUg" +
           "cHJvZ3JhbSBleGVjdXRpb24uAC8AP+s/AAAAAf////8BAf////8AAAAANWCJCgIAAAABAA0AAABTcGVl" +
           "ZE92ZXJyaWRlAQHtPwMAAAAAWwAAAFNwZWVkT3ZlcnJpZGUgcHJvdmlkZXMgdGhlIGN1cnJlbnQgc3Bl" +
           "ZWQgc2V0dGluZyBpbiBwZXJjZW50IG9mIHByb2dyYW1tZWQgc3BlZWQgKDAgLSAxMDAlKS4ALwA/7T8A" +
           "AAAL/////wEB/////wAAAAAVYIkKAgAAAAIADAAAAE1hbnVmYWN0dXJlcgEB3z8ALgBE3z8AAAAV////" +
           "/wEB/////wAAAAAVYIkKAgAAAAIABQAAAE1vZGVsAQHhPwAuAEThPwAAABX/////AQH/////AAAAABVg" +
           "iQoCAAAAAgALAAAAUHJvZHVjdENvZGUBAec/AC4AROc/AAAADP////8BAf////8AAAAAFWCJCgIAAAAC" +
           "AAwAAABEZXZpY2VNYW51YWwBAR4YAC4ARB4YAAAADP////8BAf////8AAAAAFWCJCgIAAAACAAwAAABT" +
           "ZXJpYWxOdW1iZXIBAeI/AC4AROI/AAAADP////8BAf////8AAAAAFWCJCgIAAAACAAcAAABBc3NldElk" +
           "AQEcGAAuAEQcGAAAAAz/////AQH/////AAAAABVgiQoCAAAAAgANAAAAQ29tcG9uZW50TmFtZQEBHRgA" +
           "LgBEHRgAAAAV/////wEB/////wAAAAAkYIAKAQAAAAEAFAAAAEFkZGl0aW9uYWxDb21wb25lbnRzAQG2" +
           "QAMAAAAAvAAAAEFkZGl0aW9uYWxDb21wb25lbnRzIGlzIGEgY29udGFpbmVyIGZvciBvbmUgb3IgbW9y" +
           "ZSBpbnN0YW5jZXMgb2Ygc3VidHlwZXMgb2YgQ29tcG9uZW50VHlwZSBkZWZpbmVkIGluIE9QQyBVQSBE" +
           "SS4gVGhlIGxpc3RlZCBjb21wb25lbnRzIGFyZSBpbnN0YWxsZWQgYXQgdGhlIG1vdGlvbiBkZXZpY2Us" +
           "IGUuZy4gYW4gSU8tYm9hcmQuAC8APbZAAAD/////AQAAAARgwAoBAAAAKQAAAEFkZGl0aW9uYWxDb21w" +
           "b25lbnRJZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAfAAAAPEFkZGl0aW9uYWxDb21wb25lbnRJZGVudGlm" +
           "aWVyPgEBixMALwA6ixMAAP////8AAAAAJGCACgEAAAABAAQAAABBeGVzAQHJOwMAAAAAPgAAAEF4ZXMg" +
           "aXMgYSBjb250YWluZXIgZm9yIG9uZSBvciBtb3JlIGluc3RhbmNlcyBvZiB0aGUgQXhpc1R5cGUuAC8A" +
           "Pck7AAD/////AQAAAARgwAoBAAAAGgAAAEF4aXNJZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAQAAAAPEF4" +
           "aXNJZGVudGlmaWVyPgEBfz0ALwEB2UB/PQAA/////wIAAAAkYIAKAQAAAAIADAAAAFBhcmFtZXRlclNl" +
           "dAEBgD0DAAAAABcAAABGbGF0IGxpc3Qgb2YgUGFyYW1ldGVycwAvADqAPQAA/////wEAAAA1YIkKAgAA" +
           "AAEADgAAAEFjdHVhbFBvc2l0aW9uAQH3PQMAAAAAMwAAAFRoZSBheGlzIHBvc2l0aW9uIGluY2x1c2l2" +
           "ZSBVbml0IGFuZCBSYW5nZU9mTW90aW9uLgAvAQBZRPc9AAAAC/////8BAf////8BAAAAFWCJCgIAAAAA" +
           "ABAAAABFbmdpbmVlcmluZ1VuaXRzAQH9PQAuAET9PQAAAQB3A/////8BAf////8AAAAANWCJCgIAAAAB" +
           "AA0AAABNb3Rpb25Qcm9maWxlAQHAPQMAAAAASQAAAFRoZSBraW5kIG9mIGF4aXMgbW90aW9uIGFzIGRl" +
           "ZmluZWQgd2l0aCB0aGUgQXhpc01vdGlvblByb2ZpbGVFbnVtZXJhdGlvbi4ALgBEwD0AAAEBwAv/////" +
           "AQH/////AAAAACRggAoBAAAAAQAKAAAARmxhbmdlTG9hZAEB4xMDAAAAAIgAAABUaGUgRmxhbmdlTG9h" +
           "ZCBpcyB0aGUgbG9hZCBvbiB0aGUgZmxhbmdlIG9yIGF0IHRoZSBtb3VudGluZyBwb2ludCBvZiB0aGUg" +
           "TW90aW9uRGV2aWNlLiBUaGlzIGNhbiBiZSB0aGUgbWF4aW11bSBsb2FkIG9mIHRoZSBNb3Rpb25EZXZp" +
           "Y2UuAC8BAfoD4xMAAP////8BAAAANWCJCgIAAAABAAQAAABNYXNzAQHgGQMAAAAANQAAAFRoZSB3ZWln" +
           "aHQgb2YgdGhlIGxvYWQgbW91bnRlZCBvbiBvbmUgbW91bnRpbmcgcG9pbnQuAC8BAFlE4BkAAAAL////" +
           "/wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5pdHMBASs9AC4ARCs9AAABAHcD////" +
           "/wEB/////wAAAAA1YIkKAgAAAAEAFAAAAE1vdGlvbkRldmljZUNhdGVnb3J5AQHqPwMAAAAAggAAAFRo" +
           "ZSB2YXJpYWJsZSBNb3Rpb25EZXZpY2VDYXRlZ29yeSBwcm92aWRlcyB0aGUga2luZCBvZiBtb3Rpb24g" +
           "ZGV2aWNlIGRlZmluZWQgYnkgTW90aW9uRGV2aWNlQ2F0ZWdvcnlFbnVtZXJhdGlvbiBiYXNlZCBvbiBJ" +
           "U08gODM3My4ALgBE6j8AAAEBEUf/////AQH/////AAAAACRggAoBAAAAAQALAAAAUG93ZXJUcmFpbnMB" +
           "ATtAAwAAAABLAAAAUG93ZXJUcmFpbnMgaXMgYSBjb250YWluZXIgZm9yIG9uZSBvciBtb3JlIGluc3Rh" +
           "bmNlcyBvZiB0aGUgUG93ZXJUcmFpblR5cGUuAC8APTtAAAD/////AQAAAARgwAoBAAAAIAAAAFBvd2Vy" +
           "VHJhaW5JZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAWAAAAPFBvd2VyVHJhaW5JZGVudGlmaWVyPgEBIT4A" +
           "LwEBmkEhPgAA/////wAAAAAVYIkKAgAAAAEAFAAAAFRhc2tDb250cm9sUmVmZXJlbmNlAQFxFwAvAD9x" +
           "FwAAABH/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public FolderState AdditionalComponents
        {
            get => m_additionalComponents;

            set
            {
                if (!Object.ReferenceEquals(m_additionalComponents, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_additionalComponents = value;
            }
        }

        public FolderState Axes
        {
            get => m_axes;

            set
            {
                if (!Object.ReferenceEquals(m_axes, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_axes = value;
            }
        }

        public LoadTypeState FlangeLoad
        {
            get => m_flangeLoad;

            set
            {
                if (!Object.ReferenceEquals(m_flangeLoad, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_flangeLoad = value;
            }
        }

        public PropertyState<MotionDeviceCategoryEnumeration> MotionDeviceCategory
        {
            get => m_motionDeviceCategory;

            set
            {
                if (!Object.ReferenceEquals(m_motionDeviceCategory, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_motionDeviceCategory = value;
            }
        }

        public FolderState PowerTrains
        {
            get => m_powerTrains;

            set
            {
                if (!Object.ReferenceEquals(m_powerTrains, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_powerTrains = value;
            }
        }

        public BaseDataVariableState<NodeId> TaskControlReference
        {
            get => m_taskControlReference;

            set
            {
                if (!Object.ReferenceEquals(m_taskControlReference, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_taskControlReference = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_additionalComponents != null)
            {
                children.Add(m_additionalComponents);
            }

            if (m_axes != null)
            {
                children.Add(m_axes);
            }

            if (m_flangeLoad != null)
            {
                children.Add(m_flangeLoad);
            }

            if (m_motionDeviceCategory != null)
            {
                children.Add(m_motionDeviceCategory);
            }

            if (m_powerTrains != null)
            {
                children.Add(m_powerTrains);
            }

            if (m_taskControlReference != null)
            {
                children.Add(m_taskControlReference);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_additionalComponents, child))
            {
                m_additionalComponents = null;
                return;
            }

            if (Object.ReferenceEquals(m_axes, child))
            {
                m_axes = null;
                return;
            }

            if (Object.ReferenceEquals(m_flangeLoad, child))
            {
                m_flangeLoad = null;
                return;
            }

            if (Object.ReferenceEquals(m_motionDeviceCategory, child))
            {
                m_motionDeviceCategory = null;
                return;
            }

            if (Object.ReferenceEquals(m_powerTrains, child))
            {
                m_powerTrains = null;
                return;
            }

            if (Object.ReferenceEquals(m_taskControlReference, child))
            {
                m_taskControlReference = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.AdditionalComponents:
                {
                    if (createOrReplace)
                    {
                        if (AdditionalComponents == null)
                        {
                            if (replacement == null)
                            {
                                AdditionalComponents = new FolderState(this);
                            }
                            else
                            {
                                AdditionalComponents = (FolderState)replacement;
                            }
                        }
                    }

                    instance = AdditionalComponents;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Axes:
                {
                    if (createOrReplace)
                    {
                        if (Axes == null)
                        {
                            if (replacement == null)
                            {
                                Axes = new FolderState(this);
                            }
                            else
                            {
                                Axes = (FolderState)replacement;
                            }
                        }
                    }

                    instance = Axes;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.FlangeLoad:
                {
                    if (createOrReplace)
                    {
                        if (FlangeLoad == null)
                        {
                            if (replacement == null)
                            {
                                FlangeLoad = new LoadTypeState(this);
                            }
                            else
                            {
                                FlangeLoad = (LoadTypeState)replacement;
                            }
                        }
                    }

                    instance = FlangeLoad;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.MotionDeviceCategory:
                {
                    if (createOrReplace)
                    {
                        if (MotionDeviceCategory == null)
                        {
                            if (replacement == null)
                            {
                                MotionDeviceCategory = new PropertyState<MotionDeviceCategoryEnumeration>(this);
                            }
                            else
                            {
                                MotionDeviceCategory = (PropertyState<MotionDeviceCategoryEnumeration>)replacement;
                            }
                        }
                    }

                    instance = MotionDeviceCategory;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.PowerTrains:
                {
                    if (createOrReplace)
                    {
                        if (PowerTrains == null)
                        {
                            if (replacement == null)
                            {
                                PowerTrains = new FolderState(this);
                            }
                            else
                            {
                                PowerTrains = (FolderState)replacement;
                            }
                        }
                    }

                    instance = PowerTrains;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.TaskControlReference:
                {
                    if (createOrReplace)
                    {
                        if (TaskControlReference == null)
                        {
                            if (replacement == null)
                            {
                                TaskControlReference = new BaseDataVariableState<NodeId>(this);
                            }
                            else
                            {
                                TaskControlReference = (BaseDataVariableState<NodeId>)replacement;
                            }
                        }
                    }

                    instance = TaskControlReference;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private FolderState m_additionalComponents;
        private FolderState m_axes;
        private LoadTypeState m_flangeLoad;
        private PropertyState<MotionDeviceCategoryEnumeration> m_motionDeviceCategory;
        private FolderState m_powerTrains;
        private BaseDataVariableState<NodeId> m_taskControlReference;
        #endregion
    }
    #endif
    #endregion

    #region MotorTypeState Class
    #if (!OPCUA_EXCLUDE_MotorTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class MotorTypeState : ComponentTypeState
    {
        #region Constructors
        public MotorTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.MotorType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (AssetId != null)
            {
                AssetId.Initialize(context, AssetId_InitializationString);
            }
        }

        #region Initialization String
        private const string AssetId_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIABwAAAEFzc2V0SWQBASEYAC4ARCEYAAAADP////8BAf////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAEQAAAE1vdG9yVHlwZUluc3RhbmNlAQH7AwEB+wP7AwAA/////wcAAAAkYIAKAQAAAAIA" +
           "DAAAAFBhcmFtZXRlclNldAEB8RMDAAAAABcAAABGbGF0IGxpc3Qgb2YgUGFyYW1ldGVycwAvADrxEwAA" +
           "/////wMAAAA1YIkKAgAAAAEADQAAAEJyYWtlUmVsZWFzZWQBAf5CAwAAAACsAAAASW5kaWNhdGVzIGFu" +
           "IG9wdGlvbmFsIHZhcmlhYmxlIHVzZWQgb25seSBmb3IgbW90b3JzIHdpdGggYnJha2VzLiBJZiBCcmFr" +
           "ZVJlbGVhc2VkIGlzIFRSVUUgdGhlIG1vdG9yIGlzIGZyZWUgdG8gcnVuLiBGQUxTRSBtZWFucyB0aGF0" +
           "IHRoZSBtb3RvciBzaGFmdCBpcyBsb2NrZWQgYnkgdGhlIGJyYWtlLgAvAD/+QgAAAAH/////AQH/////" +
           "AAAAADVgiQoCAAAAAQARAAAARWZmZWN0aXZlTG9hZFJhdGUBAXgaAwAAAADHAAAARWZmZWN0aXZlTG9h" +
           "ZFJhdGUgaXMgZXhwcmVzc2VkIGFzIGEgcGVyY2VudGFnZSBvZiBtYXhpbXVtIGNvbnRpbnVvdXMgbG9h" +
           "ZC4gVGhlIEpvdWxlIGludGVncmFsIGlzIHR5cGljYWxseSB1c2VkIHRvIGNhbGN1bGF0ZSB0aGUgY3Vy" +
           "cmVudCBsb2FkLiBEdXJhdGlvbiBzaG91bGQgYmUgZGVmaW5lZCBhbmQgZG9jdW1lbnRlZCBieSB0aGUg" +
           "dmVuZG9yLgAvAD94GgAAAAX/////AQH/////AAAAADVgiQoCAAAAAQAQAAAATW90b3JUZW1wZXJhdHVy" +
           "ZQEBZRoDAAAAAHwAAABUaGUgbW90b3IgdGVtcGVyYXR1cmUgcHJvdmlkZXMgdGhlIHRlbXBlcmF0dXJl" +
           "IG9mIHRoZSBtb3Rvci4gSWYgdGhlcmUgaXMgbm8gdGVtcGVyYXR1cmUgc2Vuc29yIHRoZSB2YWx1ZSBp" +
           "cyBzZXQgdG8gXCJudWxsXCIuAC8BAFlEZRoAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVu" +
           "Z2luZWVyaW5nVW5pdHMBAWoaAC4ARGoaAAABAHcD/////wEB/////wAAAAAVYIkKAgAAAAIADAAAAE1h" +
           "bnVmYWN0dXJlcgEBzUIALgBEzUIAAAAV/////wEB/////wAAAAAVYIkKAgAAAAIABQAAAE1vZGVsAQHP" +
           "QgAuAETPQgAAABX/////AQH/////AAAAABVgiQoCAAAAAgALAAAAUHJvZHVjdENvZGUBAdVCAC4ARNVC" +
           "AAAADP////8BAf////8AAAAAFWCJCgIAAAACAAwAAABTZXJpYWxOdW1iZXIBAdBCAC4ARNBCAAAADP//" +
           "//8BAf////8AAAAAFWCJCgIAAAACAAcAAABBc3NldElkAQEhGAAuAEQhGAAAAAz/////AQH/////AAAA" +
           "AARgwAoBAAAAGwAAAERyaXZlSWRlbnRpZmllcl9QbGFjZWhvbGRlcgEAEQAAADxEcml2ZUlkZW50aWZp" +
           "ZXI+AQHEEwEBBEcAOsQTAAD/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region PowerTrainTypeState Class
    #if (!OPCUA_EXCLUDE_PowerTrainTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class PowerTrainTypeState : ComponentTypeState
    {
        #region Constructors
        public PowerTrainTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.PowerTrainType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ComponentName != null)
            {
                ComponentName.Initialize(context, ComponentName_InitializationString);
            }
        }

        #region Initialization String
        private const string ComponentName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADQAAAENvbXBvbmVudE5hbWUBASAYAC4ARCAYAAAAFf////8BAf////8AAAAA";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAFgAAAFBvd2VyVHJhaW5UeXBlSW5zdGFuY2UBAZpBAQGaQZpBAAD/////BQAAABVgiQoC" +
           "AAAAAgANAAAAQ29tcG9uZW50TmFtZQEBIBgALgBEIBgAAAAV/////wEB/////wAAAAAkYMAKAQAAABoA" +
           "AABBeGlzSWRlbnRpZmllcl9QbGFjZWhvbGRlcgEAEAAAADxBeGlzSWRlbnRpZmllcj4BAWhIAwAAAABh" +
           "AQAATW92ZXMgaXMgYSByZWZlcmVuY2UgdG8gcHJvdmlkZSB0aGUgcmVsYXRpb25zaGlwIG9mIHBvd2Vy" +
           "dHJhaW5zIHRvIGF4ZXMuIEZvciBjb21wbGV4IGtpbmVtYXRpY3MgdGhpcyBkb2VzIG5vdCBuZWVkIHRv" +
           "IGJlIGEgb25lIHRvIG9uZSByZWxhdGlvbnNoaXAsIGJlY2F1c2UgYSBwb3dlcnRyYWluIG1pZ2h0IGlu" +
           "Zmx1ZW5jZSB0aGUgbW90aW9uIG9mIG1vcmUgdGhhbiBvbmUgYXhpcy4gVGhpcyByZWZlcmVuY2UgY29u" +
           "bmVjdHMgYWxsIGF4aXMgdG8gYSBwb3dlcnRyYWluIHRoYXQgdGhhdCBtb3ZlIHdoZW4gb25seSB0aGlz" +
           "IHBvd2VydHJhaW4gbW92ZXMgYW5kIGFsbCBvdGhlciBwb3dlcnRyYWlucyBzdGFuZCBzdGlsbC4BAQJH" +
           "AQHZQGhIAAD/////AgAAACRggAoBAAAAAgAMAAAAUGFyYW1ldGVyU2V0AQFpSAMAAAAAFwAAAEZsYXQg" +
           "bGlzdCBvZiBQYXJhbWV0ZXJzAC8AOmlIAAD/////AQAAADVgiQoCAAAAAQAOAAAAQWN0dWFsUG9zaXRp" +
           "b24BAaNIAwAAAAAzAAAAVGhlIGF4aXMgcG9zaXRpb24gaW5jbHVzaXZlIFVuaXQgYW5kIFJhbmdlT2ZN" +
           "b3Rpb24uAC8BAFlEo0gAAAAL/////wEB/////wEAAAAVYIkKAgAAAAAAEAAAAEVuZ2luZWVyaW5nVW5p" +
           "dHMBAahIAC4ARKhIAAABAHcD/////wEB/////wAAAAA1YIkKAgAAAAEADQAAAE1vdGlvblByb2ZpbGUB" +
           "AYpIAwAAAABJAAAAVGhlIGtpbmQgb2YgYXhpcyBtb3Rpb24gYXMgZGVmaW5lZCB3aXRoIHRoZSBBeGlz" +
           "TW90aW9uUHJvZmlsZUVudW1lcmF0aW9uLgAuAESKSAAAAQHAC/////8BAf////8AAAAABGDACgEAAAAa" +
           "AAAAR2VhcklkZW50aWZpZXJfUGxhY2Vob2xkZXIBABAAAAA8R2VhcklkZW50aWZpZXI+AQGpPgAvAQH+" +
           "A6k+AAD/////BQAAABVgiQoCAAAAAgAMAAAATWFudWZhY3R1cmVyAQG+PgAuAES+PgAAABX/////AQH/" +
           "////AAAAABVgiQoCAAAAAgAFAAAATW9kZWwBAcA+AC4ARMA+AAAAFf////8BAf////8AAAAAFWCJCgIA" +
           "AAACAAsAAABQcm9kdWN0Q29kZQEBxD4ALgBExD4AAAAM/////wEB/////wAAAAAVYIkKAgAAAAIADAAA" +
           "AFNlcmlhbE51bWJlcgEBxz4ALgBExz4AAAAM/////wEB/////wAAAAA1YIkKAgAAAAEACQAAAEdlYXJS" +
           "YXRpbwEBzD4DAAAAAHkAAABUaGUgdHJhbnNtaXNzaW9uIHJhdGlvIG9mIHRoZSBnZWFyIGV4cHJlc3Nl" +
           "ZCBhcyBhIGZyYWN0aW9uIGFzIGlucHV0IHZlbG9jaXR5IChtb3RvciBzaWRlKSBieSBvdXRwdXQgdmVs" +
           "b2NpdHkgKGxvYWQgc2lkZSkuAC8BAC1FzD4AAAEAdkn/////AQH/////AgAAABVgiQoCAAAAAAAJAAAA" +
           "TnVtZXJhdG9yAQHNPgAvAD/NPgAAAAb/////AQH/////AAAAABVgiQoCAAAAAAALAAAARGVub21pbmF0" +
           "b3IBAc4+AC8AP84+AAAAB/////8BAf////8AAAAABGDACgEAAAAbAAAATW90b3JJZGVudGlmaWVyX1Bs" +
           "YWNlaG9sZGVyAQARAAAAPE1vdG9ySWRlbnRpZmllcj4BAX4+AC8BAfsDfj4AAP////8FAAAAJGCACgEA" +
           "AAACAAwAAABQYXJhbWV0ZXJTZXQBAX8+AwAAAAAXAAAARmxhdCBsaXN0IG9mIFBhcmFtZXRlcnMALwA6" +
           "fz4AAP////8BAAAANWCJCgIAAAABABAAAABNb3RvclRlbXBlcmF0dXJlAQGiPgMAAAAAfAAAAFRoZSBt" +
           "b3RvciB0ZW1wZXJhdHVyZSBwcm92aWRlcyB0aGUgdGVtcGVyYXR1cmUgb2YgdGhlIG1vdG9yLiBJZiB0" +
           "aGVyZSBpcyBubyB0ZW1wZXJhdHVyZSBzZW5zb3IgdGhlIHZhbHVlIGlzIHNldCB0byBcIm51bGxcIi4A" +
           "LwEAWUSiPgAAAAv/////AQH/////AQAAABVgiQoCAAAAAAAQAAAARW5naW5lZXJpbmdVbml0cwEBpz4A" +
           "LgBEpz4AAAEAdwP/////AQH/////AAAAABVgiQoCAAAAAgAMAAAATWFudWZhY3R1cmVyAQGTPgAuAEST" +
           "PgAAABX/////AQH/////AAAAABVgiQoCAAAAAgAFAAAATW9kZWwBAZU+AC4ARJU+AAAAFf////8BAf//" +
           "//8AAAAAFWCJCgIAAAACAAsAAABQcm9kdWN0Q29kZQEBmT4ALgBEmT4AAAAM/////wEB/////wAAAAAV" +
           "YIkKAgAAAAIADAAAAFNlcmlhbE51bWJlcgEBnD4ALgBEnD4AAAAM/////wEB/////wAAAAAkYMAKAQAA" +
           "ACAAAABQb3dlclRyYWluSWRlbnRpZmllcl9QbGFjZWhvbGRlcgEAFgAAADxQb3dlclRyYWluSWRlbnRp" +
           "Zmllcj4BAbVIAwAAAACVAAAASGFzU2xhdmUgaXMgYSByZWZlcmVuY2UgdG8gcHJvdmlkZSB0aGUgbWFz" +
           "dGVyLXNsYXZlIHJlbGF0aW9uc2hpcCBvZiBwb3dlcnRyYWlucyB3aGljaCBwcm92aWRlIHRvcnF1ZSBm" +
           "b3IgYSBjb21tb24gYXhpcy4gVGhlIEludmVyc2VOYW1lIGlzIElzU2xhdmVPZi4BAQdHAQGaQbVIAAD/" +
           "////AQAAABVgiQoCAAAAAgANAAAAQ29tcG9uZW50TmFtZQEBIBgALgBEIBgAAAAV/////wEB/////wAA" +
           "AAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region SafetyStateTypeState Class
    #if (!OPCUA_EXCLUDE_SafetyStateTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class SafetyStateTypeState : ComponentTypeState
    {
        #region Constructors
        public SafetyStateTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.SafetyStateType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (ComponentName != null)
            {
                ComponentName.Initialize(context, ComponentName_InitializationString);
            }

            if (EmergencyStopFunctions != null)
            {
                EmergencyStopFunctions.Initialize(context, EmergencyStopFunctions_InitializationString);
            }

            if (ProtectiveStopFunctions != null)
            {
                ProtectiveStopFunctions.Initialize(context, ProtectiveStopFunctions_InitializationString);
            }
        }

        #region Initialization String
        private const string ComponentName_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8V" +
           "YIkKAgAAAAIADQAAAENvbXBvbmVudE5hbWUBASMYAC4ARCMYAAAAFf////8BAf////8AAAAA";

        private const string EmergencyStopFunctions_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAEAFgAAAEVtZXJnZW5jeVN0b3BGdW5jdGlvbnMBAUVDAwAAAABhAAAARW1lcmdlbmN5U3Rv" +
           "cEZ1bmN0aW9ucyBpcyBhIGNvbnRhaW5lciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9mIHRoZSBF" +
           "bWVyZ2VuY3lTdG9wRnVuY3Rpb25UeXBlLgAvAD1FQwAA/////wEAAAAEYMAKAQAAACsAAABFbWVyZ2Vu" +
           "Y3lTdG9wRnVuY3Rpb25JZGVudGlmaWVyX1BsYWNlaG9sZGVyAQAhAAAAPEVtZXJnZW5jeVN0b3BGdW5j" +
           "dGlvbklkZW50aWZpZXI+AQF2SQAvAQFOQ3ZJAAD/////AgAAADVgiQoCAAAAAQAGAAAAQWN0aXZlAQF4" +
           "SQMAAAAAkgAAAFRoZSBBY3RpdmUgdmFyaWFibGUgaXMgVFJVRSBpZiB0aGlzIHBhcnRpY3VsYXIgZW1l" +
           "cmdlbmN5IHN0b3AgZnVuY3Rpb24gaXMgYWN0aXZlLCBlLmcuIHRoYXQgdGhlIGVtZXJnZW5jeSBzdG9w" +
           "IGJ1dHRvbiBpcyBwcmVzc2VkLCBGQUxTRSBvdGhlcndpc2UuAC8AP3hJAAAAAf////8BAf////8AAAAA" +
           "NWCJCgIAAAABAAQAAABOYW1lAQF3SQMAAAAAhwAAAFRoZSBOYW1lIG9mIHRoZSBFbWVyZ2VuY3lTdG9w" +
           "RnVuY3Rpb25UeXBlIHByb3ZpZGVzIGEgbWFudWZhY3R1cmVyLXNwZWNpZmljIGVtZXJnZW5jeSBzdG9w" +
           "IGZ1bmN0aW9uIGlkZW50aWZpZXIgd2l0aGluIHRoZSBzYWZldHkgc3lzdGVtLgAuAER3SQAAAAz/////" +
           "AQH/////AAAAAA==";

        private const string ProtectiveStopFunctions_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8k" +
           "YIAKAQAAAAEAFwAAAFByb3RlY3RpdmVTdG9wRnVuY3Rpb25zAQFJQwMAAAAAYwAAAFByb3RlY3RpdmVT" +
           "dG9wRnVuY3Rpb25zIGlzIGEgY29udGFpbmVyIGZvciBvbmUgb3IgbW9yZSBpbnN0YW5jZXMgb2YgdGhl" +
           "IFByb3RlY3RpdmVTdG9wRnVuY3Rpb25UeXBlLgAvAD1JQwAA/////wEAAAAEYMAKAQAAACwAAABQcm90" +
           "ZWN0aXZlU3RvcEZ1bmN0aW9uSWRlbnRpZmllcl9QbGFjZWhvbGRlcgEAIgAAADxQcm90ZWN0aXZlU3Rv" +
           "cEZ1bmN0aW9uSWRlbnRpZmllcj4BAXlJAC8BAVFDeUkAAP////8DAAAANWCJCgIAAAABAAYAAABBY3Rp" +
           "dmUBAXxJAwAAAAC2AAAA4oCTCVRoZSBBY3RpdmUgdmFyaWFibGUgaXMgVFJVRSBpZiB0aGlzIHBhcnRp" +
           "Y3VsYXIgcHJvdGVjdGl2ZSBzdG9wIGZ1bmN0aW9uIGlzIGFjdGl2ZSwgaS5lLiB0aGF0IGEgc3RvcCBp" +
           "cyBpbml0aWF0ZWQsIEZBTFNFIG90aGVyd2lzZS4gSWYgRW5hYmxlZCBpcyBGQUxTRSB0aGVuIEFjdGl2" +
           "ZSBzaGFsbCBiZSBGQUxTRS4ALwA/fEkAAAAB/////wEB/////wAAAAA1YIkKAgAAAAEABwAAAEVuYWJs" +
           "ZWQBAXtJAwAAAADeAQAA4oCTCVRoZSBFbmFibGVkIHZhcmlhYmxlIGlzIFRSVUUgaWYgdGhpcyBwcm90" +
           "ZWN0aXZlIHN0b3AgZnVuY3Rpb24gaXMgY3VycmVudGx5IHN1cGVydmlzaW5nIHRoZSBzeXN0ZW0sIEZB" +
           "TFNFIG90aGVyd2lzZS4gQSBwcm90ZWN0aXZlIHN0b3AgZnVuY3Rpb24gbWF5IG9yIG1heSBub3QgYmUg" +
           "ZW5hYmxlZCBhdCBhbGwgdGltZXMsIGUuZy4gdGhlIHByb3RlY3RpdmUgc3RvcCBmdW5jdGlvbiBvZiB0" +
           "aGUgc2FmZXR5IGRvb3JzIGFyZSB0eXBpY2FsbHkgZW5hYmxlZCBpbiBhdXRvbWF0aWMgb3BlcmF0aW9u" +
           "YWwgbW9kZSBhbmQgZGlzYWJsZWQgaW4gbWFudWFsIG1vZGUuIE9uIHRoZSBvdGhlciBoYW5kIGZvciBl" +
           "eGFtcGxlLCB0aGUgcHJvdGVjdGl2ZSBzdG9wIGZ1bmN0aW9uIG9mIHRoZSB0ZWFjaCBwZW5kYW50IGVu" +
           "YWJsaW5nIGRldmljZSBpcyBlbmFibGVkIGluIG1hbnVhbCBtb2RlcyBhbmQgZGlzYWJsZWQgaW4gYXV0" +
           "b21hdGljIG1vZGVzLgAvAD97SQAAAAH/////AQH/////AAAAADVgiQoCAAAAAQAEAAAATmFtZQEBekkD" +
           "AAAAAIkAAABUaGUgTmFtZSBvZiB0aGUgUHJvdGVjdGl2ZVN0b3BGdW5jdGlvblR5cGUgcHJvdmlkZXMg" +
           "YSBtYW51ZmFjdHVyZXItc3BlY2lmaWMgcHJvdGVjdGl2ZSBzdG9wIGZ1bmN0aW9uIGlkZW50aWZpZXIg" +
           "d2l0aGluIHRoZSBzYWZldHkgc3lzdGVtLgAuAER6SQAAAAz/////AQH/////AAAAAA==";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAFwAAAFNhZmV0eVN0YXRlVHlwZUluc3RhbmNlAQH1AwEB9QP1AwAA/////wQAAAAkYIAK" +
           "AQAAAAIADAAAAFBhcmFtZXRlclNldAEBmBMDAAAAABcAAABGbGF0IGxpc3Qgb2YgUGFyYW1ldGVycwAv" +
           "ADqYEwAA/////wMAAAA1YIkKAgAAAAEADQAAAEVtZXJnZW5jeVN0b3ABAQo+AwAAAAAeAQAAVGhlIEVt" +
           "ZXJnZW5jeVN0b3AgdmFyaWFibGUgaXMgVFJVRSBpZiBvbmUgb3IgbW9yZSBvZiB0aGUgZW1lcmdlbmN5" +
           "IHN0b3AgZnVuY3Rpb25zIGluIHRoZSByb2JvdCBzeXN0ZW0gYXJlIGFjdGl2ZSwgRkFMU0Ugb3RoZXJ3" +
           "aXNlLiBJZiB0aGUgRW1lcmdlbmN5U3RvcEZ1bmN0aW9ucyBvYmplY3QgaXMgcHJvdmlkZWQsIHRoZW4g" +
           "dGhlIHZhbHVlIG9mIHRoaXMgdmFyaWFibGUgaXMgVFJVRSBpZiBvbmUgb3IgbW9yZSBvZiB0aGUgbGlz" +
           "dGVkIGVtZXJnZW5jeSBzdG9wIGZ1bmN0aW9ucyBhcmUgYWN0aXZlLgAvAD8KPgAAAAH/////AQH/////" +
           "AAAAADVgiQoCAAAAAQAPAAAAT3BlcmF0aW9uYWxNb2RlAQEoPgMAAAAAwAAAAFRoZSBPcGVyYXRpb25h" +
           "bE1vZGUgdmFyaWFibGUgcHJvdmlkZXMgaW5mb3JtYXRpb24gYWJvdXQgdGhlIGN1cnJlbnQgb3BlcmF0" +
           "aW9uYWwgbW9kZS4gQWxsb3dlZCB2YWx1ZXMgYXJlIGRlc2NyaWJlZCBpbiBPcGVyYXRpb25hbE1vZGVF" +
           "bnVtZXJhdGlvbiwgc2VlIElTTyAxMDIxOC0xOjIwMTEgQ2guNS43IE9wZXJhdGlvbmFsIE1vZGVzLgAv" +
           "AD8oPgAAAQG+C/////8BAf////8AAAAANWCJCgIAAAABAA4AAABQcm90ZWN0aXZlU3RvcAEBKT4DAAAA" +
           "ADABAABUaGUgUHJvdGVjdGl2ZVN0b3AgdmFyaWFibGUgaXMgVFJVRSBpZiBvbmUgb3IgbW9yZSBvZiB0" +
           "aGUgZW5hYmxlZCBwcm90ZWN0aXZlIHN0b3AgZnVuY3Rpb25zIGluIHRoZSBzeXN0ZW0gYXJlIGFjdGl2" +
           "ZSwgRkFMU0Ugb3RoZXJ3aXNlLiBJZiB0aGUgUHJvdGVjdGl2ZVN0b3BGdW5jdGlvbnMgb2JqZWN0IGlz" +
           "IHByb3ZpZGVkLCB0aGVuIHRoZSB2YWx1ZSBvZiB0aGlzIHZhcmlhYmxlIGlzIFRSVUUgaWYgb25lIG9y" +
           "IG1vcmUgb2YgdGhlIGxpc3RlZCBwcm90ZWN0aXZlIHN0b3AgZnVuY3Rpb25zIGFyZSBlbmFibGVkIGFu" +
           "ZCBhY3RpdmUuAC8APyk+AAAAAf////8BAf////8AAAAAFWCJCgIAAAACAA0AAABDb21wb25lbnROYW1l" +
           "AQEjGAAuAEQjGAAAABX/////AQH/////AAAAACRggAoBAAAAAQAWAAAARW1lcmdlbmN5U3RvcEZ1bmN0" +
           "aW9ucwEBRUMDAAAAAGEAAABFbWVyZ2VuY3lTdG9wRnVuY3Rpb25zIGlzIGEgY29udGFpbmVyIGZvciBv" +
           "bmUgb3IgbW9yZSBpbnN0YW5jZXMgb2YgdGhlIEVtZXJnZW5jeVN0b3BGdW5jdGlvblR5cGUuAC8APUVD" +
           "AAD/////AQAAAARgwAoBAAAAKwAAAEVtZXJnZW5jeVN0b3BGdW5jdGlvbklkZW50aWZpZXJfUGxhY2Vo" +
           "b2xkZXIBACEAAAA8RW1lcmdlbmN5U3RvcEZ1bmN0aW9uSWRlbnRpZmllcj4BAXZJAC8BAU5DdkkAAP//" +
           "//8CAAAANWCJCgIAAAABAAYAAABBY3RpdmUBAXhJAwAAAACSAAAAVGhlIEFjdGl2ZSB2YXJpYWJsZSBp" +
           "cyBUUlVFIGlmIHRoaXMgcGFydGljdWxhciBlbWVyZ2VuY3kgc3RvcCBmdW5jdGlvbiBpcyBhY3RpdmUs" +
           "IGUuZy4gdGhhdCB0aGUgZW1lcmdlbmN5IHN0b3AgYnV0dG9uIGlzIHByZXNzZWQsIEZBTFNFIG90aGVy" +
           "d2lzZS4ALwA/eEkAAAAB/////wEB/////wAAAAA1YIkKAgAAAAEABAAAAE5hbWUBAXdJAwAAAACHAAAA" +
           "VGhlIE5hbWUgb2YgdGhlIEVtZXJnZW5jeVN0b3BGdW5jdGlvblR5cGUgcHJvdmlkZXMgYSBtYW51ZmFj" +
           "dHVyZXItc3BlY2lmaWMgZW1lcmdlbmN5IHN0b3AgZnVuY3Rpb24gaWRlbnRpZmllciB3aXRoaW4gdGhl" +
           "IHNhZmV0eSBzeXN0ZW0uAC4ARHdJAAAADP////8BAf////8AAAAAJGCACgEAAAABABcAAABQcm90ZWN0" +
           "aXZlU3RvcEZ1bmN0aW9ucwEBSUMDAAAAAGMAAABQcm90ZWN0aXZlU3RvcEZ1bmN0aW9ucyBpcyBhIGNv" +
           "bnRhaW5lciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9mIHRoZSBQcm90ZWN0aXZlU3RvcEZ1bmN0" +
           "aW9uVHlwZS4ALwA9SUMAAP////8BAAAABGDACgEAAAAsAAAAUHJvdGVjdGl2ZVN0b3BGdW5jdGlvbklk" +
           "ZW50aWZpZXJfUGxhY2Vob2xkZXIBACIAAAA8UHJvdGVjdGl2ZVN0b3BGdW5jdGlvbklkZW50aWZpZXI+" +
           "AQF5SQAvAQFRQ3lJAAD/////AwAAADVgiQoCAAAAAQAGAAAAQWN0aXZlAQF8SQMAAAAAtgAAAOKAkwlU" +
           "aGUgQWN0aXZlIHZhcmlhYmxlIGlzIFRSVUUgaWYgdGhpcyBwYXJ0aWN1bGFyIHByb3RlY3RpdmUgc3Rv" +
           "cCBmdW5jdGlvbiBpcyBhY3RpdmUsIGkuZS4gdGhhdCBhIHN0b3AgaXMgaW5pdGlhdGVkLCBGQUxTRSBv" +
           "dGhlcndpc2UuIElmIEVuYWJsZWQgaXMgRkFMU0UgdGhlbiBBY3RpdmUgc2hhbGwgYmUgRkFMU0UuAC8A" +
           "P3xJAAAAAf////8BAf////8AAAAANWCJCgIAAAABAAcAAABFbmFibGVkAQF7SQMAAAAA3gEAAOKAkwlU" +
           "aGUgRW5hYmxlZCB2YXJpYWJsZSBpcyBUUlVFIGlmIHRoaXMgcHJvdGVjdGl2ZSBzdG9wIGZ1bmN0aW9u" +
           "IGlzIGN1cnJlbnRseSBzdXBlcnZpc2luZyB0aGUgc3lzdGVtLCBGQUxTRSBvdGhlcndpc2UuIEEgcHJv" +
           "dGVjdGl2ZSBzdG9wIGZ1bmN0aW9uIG1heSBvciBtYXkgbm90IGJlIGVuYWJsZWQgYXQgYWxsIHRpbWVz" +
           "LCBlLmcuIHRoZSBwcm90ZWN0aXZlIHN0b3AgZnVuY3Rpb24gb2YgdGhlIHNhZmV0eSBkb29ycyBhcmUg" +
           "dHlwaWNhbGx5IGVuYWJsZWQgaW4gYXV0b21hdGljIG9wZXJhdGlvbmFsIG1vZGUgYW5kIGRpc2FibGVk" +
           "IGluIG1hbnVhbCBtb2RlLiBPbiB0aGUgb3RoZXIgaGFuZCBmb3IgZXhhbXBsZSwgdGhlIHByb3RlY3Rp" +
           "dmUgc3RvcCBmdW5jdGlvbiBvZiB0aGUgdGVhY2ggcGVuZGFudCBlbmFibGluZyBkZXZpY2UgaXMgZW5h" +
           "YmxlZCBpbiBtYW51YWwgbW9kZXMgYW5kIGRpc2FibGVkIGluIGF1dG9tYXRpYyBtb2Rlcy4ALwA/e0kA" +
           "AAAB/////wEB/////wAAAAA1YIkKAgAAAAEABAAAAE5hbWUBAXpJAwAAAACJAAAAVGhlIE5hbWUgb2Yg" +
           "dGhlIFByb3RlY3RpdmVTdG9wRnVuY3Rpb25UeXBlIHByb3ZpZGVzIGEgbWFudWZhY3R1cmVyLXNwZWNp" +
           "ZmljIHByb3RlY3RpdmUgc3RvcCBmdW5jdGlvbiBpZGVudGlmaWVyIHdpdGhpbiB0aGUgc2FmZXR5IHN5" +
           "c3RlbS4ALgBEekkAAAAM/////wEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public FolderState EmergencyStopFunctions
        {
            get => m_emergencyStopFunctions;

            set
            {
                if (!Object.ReferenceEquals(m_emergencyStopFunctions, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_emergencyStopFunctions = value;
            }
        }

        public FolderState ProtectiveStopFunctions
        {
            get => m_protectiveStopFunctions;

            set
            {
                if (!Object.ReferenceEquals(m_protectiveStopFunctions, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_protectiveStopFunctions = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_emergencyStopFunctions != null)
            {
                children.Add(m_emergencyStopFunctions);
            }

            if (m_protectiveStopFunctions != null)
            {
                children.Add(m_protectiveStopFunctions);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_emergencyStopFunctions, child))
            {
                m_emergencyStopFunctions = null;
                return;
            }

            if (Object.ReferenceEquals(m_protectiveStopFunctions, child))
            {
                m_protectiveStopFunctions = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.EmergencyStopFunctions:
                {
                    if (createOrReplace)
                    {
                        if (EmergencyStopFunctions == null)
                        {
                            if (replacement == null)
                            {
                                EmergencyStopFunctions = new FolderState(this);
                            }
                            else
                            {
                                EmergencyStopFunctions = (FolderState)replacement;
                            }
                        }
                    }

                    instance = EmergencyStopFunctions;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.ProtectiveStopFunctions:
                {
                    if (createOrReplace)
                    {
                        if (ProtectiveStopFunctions == null)
                        {
                            if (replacement == null)
                            {
                                ProtectiveStopFunctions = new FolderState(this);
                            }
                            else
                            {
                                ProtectiveStopFunctions = (FolderState)replacement;
                            }
                        }
                    }

                    instance = ProtectiveStopFunctions;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private FolderState m_emergencyStopFunctions;
        private FolderState m_protectiveStopFunctions;
        #endregion
    }
    #endif
    #endregion

    #region TaskControlTypeState Class
    #if (!OPCUA_EXCLUDE_TaskControlTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class TaskControlTypeState : ComponentTypeState
    {
        #region Constructors
        public TaskControlTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.TaskControlType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (TaskControlOperation != null)
            {
                TaskControlOperation.Initialize(context, TaskControlOperation_InitializationString);
            }

            if (TaskModules != null)
            {
                TaskModules.Initialize(context, TaskModules_InitializationString);
            }
        }

        #region Initialization String
        private const string TaskControlOperation_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEAFAAAAFRhc2tDb250cm9sT3BlcmF0aW9uAQG8EwEAxEQBAfADvBMAAP////8BAAAABGCA" +
           "CgEAAAABABcAAABUYXNrQ29udHJvbFN0YXRlTWFjaGluZQEBvRMALwEBAQS9EwAAAQAAAAApAAEABwkD" +
           "AAAAFWCJCgIAAAAAAAwAAABDdXJyZW50U3RhdGUBAQQYAC8BAMgKBBgAAAAV/////wEB/////wEAAAAV" +
           "YIkKAgAAAAAAAgAAAElkAQEFGAAuAEQFGAAAABH/////AQH/////AAAAABVgiQoCAAAAAAAOAAAATGFz" +
           "dFRyYW5zaXRpb24BAQYYAC8BAM8KBhgAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQEH" +
           "GAAuAEQHGAAAABH/////AQH/////AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25SZWFzb24B" +
           "AQgYAC8BAOYrCBgAAAAE/////wMD/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMBAQkYAC4A" +
           "RAkYAACWBgAAAAEAOyABQAAAAAAAAAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsAAABDYXVz" +
           "ZWQgYnkgYW4gdW5rbm93biByZWFzb24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0ZXJuYWwD" +
           "AgAAAGVuHAAAAENhdXNlZCBieSBleHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAAAAMCAAAA" +
           "ZW4GAAAARGlyZWN0AwIAAABlbhoAAABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyABRgAAAAMA" +
           "AAAAAAAAAwIAAABlbgYAAABTeXN0ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3BlY2lmaWMg" +
           "YmVoYXZpb3IBADsgATUAAAAEAAAAAAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENhdXNlZCBi" +
           "eSBhbiBlcnJvcgEAOyABVAAAAAUAAAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAAZW4rAAAA" +
           "Q2F1c2VkIGV4cGxpY2l0bHkgYnkgZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAAAAAAAAAB" +
           "Af////8AAAAAFWCpCgIAAAAAAAsAAABWYWx1ZUFzVGV4dAEBChgALgBEChgAABUCBwAAAEludmFsaWQA" +
           "Ff////8BAf////8AAAAA";

        private const string TaskModules_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIAKAQAAAAEACwAAAFRhc2tNb2R1bGVzAQHBEwAvAD3BEwAA/////wEAAAAEYMAKAQAAABYAAABUYXNr" +
           "TW9kdWxlX1BsYWNlaG9sZGVyAQAMAAAAPFRhc2tNb2R1bGU+AQHCEwAjAQH4A8ITAAD/////AQAAABVg" +
           "iQoCAAAAAQAEAAAATmFtZQEBGRgALgBEGRgAAAAM/////wMD/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAFwAAAFRhc2tDb250cm9sVHlwZUluc3RhbmNlAQHzAwEB8wPzAwAA/////wUAAAAkYIAK" +
           "AQAAAAIADAAAAFBhcmFtZXRlclNldAEBCz4DAAAAABcAAABGbGF0IGxpc3Qgb2YgUGFyYW1ldGVycwAv" +
           "ADoLPgAA/////wMAAAA1YIkKAgAAAAEADQAAAEV4ZWN1dGlvbk1vZGUBAdRFAwAAAAA9AAAARXhlY3V0" +
           "aW9uIG1vZGUgb2YgdGhlIHRhc2sgY29udHJvbCAoY29udGludW91cyBvciBzdGVwLXdpc2UpLgAvAD/U" +
           "RQAAAQEPR/////8BAf////8AAAAANWCJCgIAAAABABEAAABUYXNrUHJvZ3JhbUxvYWRlZAEB00UDAAAA" +
           "AGgAAABUaGUgVGFza1Byb2dyYW1Mb2FkZWQgdmFyaWFibGUgaXMgVFJVRSBpZiBhIHRhc2sgcHJvZ3Jh" +
           "bSBpcyBsb2FkZWQgaW4gdGhlIHRhc2sgY29udHJvbCwgRkFMU0Ugb3RoZXJ3aXNlLgAvAD/TRQAAAAH/" +
           "////AQH/////AAAAADVgiQoCAAAAAQAPAAAAVGFza1Byb2dyYW1OYW1lAQHSRQMAAAAAMQAAAEEgY3Vz" +
           "dG9tZXIgZ2l2ZW4gaWRlbnRpZmllciBmb3IgdGhlIHRhc2sgcHJvZ3JhbS4ALwA/0kUAAAAM/////wEB" +
           "/////wAAAAA1YIkKAgAAAAIADQAAAENvbXBvbmVudE5hbWUBAdFFAwAAAABOAAAAQSB1c2VyIHdyaXRh" +
           "YmxlIG5hbWUgcHJvdmlkZWQgYnkgdGhlIHZlbmRvciwgaW50ZWdyYXRvciBvciB1c2VyIG9mIHRoZSBk" +
           "ZXZpY2UuAC4ARNFFAAAAFf////8BAf////8AAAAAJGDACgEAAAAiAAAATW90aW9uRGV2aWNlSWRlbnRp" +
           "Zmllcl9QbGFjZWhvbGRlcgEAGAAAADxNb3Rpb25EZXZpY2VJZGVudGlmaWVyPgEBNksDAAAAAF8AAABD" +
           "b250cm9scyBpcyBhIHJlZmVyZW5jZSB0byBwcm92aWRlIHRoZSByZWxhdGlvbnNoaXAgYmV0d2VlbiBh" +
           "IHRhc2sgY29udHJvbCBhbmQgYSBtb3Rpb24gZGV2aWNlLgEBog8BAewDNksAAP////8JAAAAJGCACgEA" +
           "AAACAAwAAABQYXJhbWV0ZXJTZXQBATdLAwAAAAAXAAAARmxhdCBsaXN0IG9mIFBhcmFtZXRlcnMALwA6" +
           "N0sAAP////8BAAAANWCJCgIAAAABAA0AAABTcGVlZE92ZXJyaWRlAQFbSwMAAAAAWwAAAFNwZWVkT3Zl" +
           "cnJpZGUgcHJvdmlkZXMgdGhlIGN1cnJlbnQgc3BlZWQgc2V0dGluZyBpbiBwZXJjZW50IG9mIHByb2dy" +
           "YW1tZWQgc3BlZWQgKDAgLSAxMDAlKS4ALwA/W0sAAAAL/////wEB/////wAAAAAVYIkKAgAAAAIADAAA" +
           "AE1hbnVmYWN0dXJlcgEBS0sALgBES0sAAAAV/////wEB/////wAAAAAVYIkKAgAAAAIABQAAAE1vZGVs" +
           "AQFNSwAuAERNSwAAABX/////AQH/////AAAAABVgiQoCAAAAAgALAAAAUHJvZHVjdENvZGUBAVNLAC4A" +
           "RFNLAAAADP////8BAf////8AAAAAFWCJCgIAAAACAAwAAABTZXJpYWxOdW1iZXIBAU5LAC4ARE5LAAAA" +
           "DP////8BAf////8AAAAAJGCACgEAAAABAAQAAABBeGVzAQFcSwMAAAAAPgAAAEF4ZXMgaXMgYSBjb250" +
           "YWluZXIgZm9yIG9uZSBvciBtb3JlIGluc3RhbmNlcyBvZiB0aGUgQXhpc1R5cGUuAC8APVxLAAD/////" +
           "AAAAADVgiQoCAAAAAQAUAAAATW90aW9uRGV2aWNlQ2F0ZWdvcnkBAVhLAwAAAACCAAAAVGhlIHZhcmlh" +
           "YmxlIE1vdGlvbkRldmljZUNhdGVnb3J5IHByb3ZpZGVzIHRoZSBraW5kIG9mIG1vdGlvbiBkZXZpY2Ug" +
           "ZGVmaW5lZCBieSBNb3Rpb25EZXZpY2VDYXRlZ29yeUVudW1lcmF0aW9uIGJhc2VkIG9uIElTTyA4Mzcz" +
           "LgAuAERYSwAAAQERR/////8BAf////8AAAAAJGCACgEAAAABAAsAAABQb3dlclRyYWlucwEBqksDAAAA" +
           "AEsAAABQb3dlclRyYWlucyBpcyBhIGNvbnRhaW5lciBmb3Igb25lIG9yIG1vcmUgaW5zdGFuY2VzIG9m" +
           "IHRoZSBQb3dlclRyYWluVHlwZS4ALwA9qksAAP////8AAAAAFWCJCgIAAAABABQAAABUYXNrQ29udHJv" +
           "bFJlZmVyZW5jZQEBdBcALwA/dBcAAAAR/////wEB/////wAAAAAEYIAKAQAAAAEAFAAAAFRhc2tDb250" +
           "cm9sT3BlcmF0aW9uAQG8EwEAxEQBAfADvBMAAP////8BAAAABGCACgEAAAABABcAAABUYXNrQ29udHJv" +
           "bFN0YXRlTWFjaGluZQEBvRMALwEBAQS9EwAAAQAAAAApAAEABwkDAAAAFWCJCgIAAAAAAAwAAABDdXJy" +
           "ZW50U3RhdGUBAQQYAC8BAMgKBBgAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQEFGAAu" +
           "AEQFGAAAABH/////AQH/////AAAAABVgiQoCAAAAAAAOAAAATGFzdFRyYW5zaXRpb24BAQYYAC8BAM8K" +
           "BhgAAAAV/////wEB/////wEAAAAVYIkKAgAAAAAAAgAAAElkAQEHGAAuAEQHGAAAABH/////AQH/////" +
           "AAAAABVgiQoCAAAAAQAUAAAATGFzdFRyYW5zaXRpb25SZWFzb24BAQgYAC8BAOYrCBgAAAAE/////wMD" +
           "/////wIAAAAXYKkKAgAAAAAACgAAAEVudW1WYWx1ZXMBAQkYAC4ARAkYAACWBgAAAAEAOyABQAAAAAAA" +
           "AAAAAAAAAwIAAABlbgcAAABVbmtub3duAwIAAABlbhsAAABDYXVzZWQgYnkgYW4gdW5rbm93biByZWFz" +
           "b24BADsgAUIAAAABAAAAAAAAAAMCAAAAZW4IAAAARXh0ZXJuYWwDAgAAAGVuHAAAAENhdXNlZCBieSBl" +
           "eHRlcm5hbCBvcGVyYXRpb24BADsgAT4AAAACAAAAAAAAAAMCAAAAZW4GAAAARGlyZWN0AwIAAABlbhoA" +
           "AABDYXVzZWQgYnkgZGlyZWN0IG9wZXJhdGlvbgEAOyABRgAAAAMAAAAAAAAAAwIAAABlbgYAAABTeXN0" +
           "ZW0DAgAAAGVuIgAAAENhdXNlZCBieSBzeXN0ZW0gc3BlY2lmaWMgYmVoYXZpb3IBADsgATUAAAAEAAAA" +
           "AAAAAAMCAAAAZW4FAAAARXJyb3IDAgAAAGVuEgAAAENhdXNlZCBieSBhbiBlcnJvcgEAOyABVAAAAAUA" +
           "AAAAAAAAAwIAAABlbgsAAABBcHBsaWNhdGlvbgMCAAAAZW4rAAAAQ2F1c2VkIGV4cGxpY2l0bHkgYnkg" +
           "ZW5kIHVzZXIgcHJvZ3JhbSBsb2dpYwEAqh0BAAAAAQAAAAAAAAABAf////8AAAAAFWCpCgIAAAAAAAsA" +
           "AABWYWx1ZUFzVGV4dAEBChgALgBEChgAABUCBwAAAEludmFsaWQAFf////8BAf////8AAAAABGCACgEA" +
           "AAABAAsAAABUYXNrTW9kdWxlcwEBwRMALwA9wRMAAP////8BAAAABGDACgEAAAAWAAAAVGFza01vZHVs" +
           "ZV9QbGFjZWhvbGRlcgEADAAAADxUYXNrTW9kdWxlPgEBwhMAIwEB+APCEwAA/////wEAAAAVYIkKAgAA" +
           "AAEABAAAAE5hbWUBARkYAC4ARBkYAAAADP////8DA/////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public TaskControlOperationTypeState TaskControlOperation
        {
            get => m_taskControlOperation;

            set
            {
                if (!Object.ReferenceEquals(m_taskControlOperation, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_taskControlOperation = value;
            }
        }

        public FolderState TaskModules
        {
            get => m_taskModules;

            set
            {
                if (!Object.ReferenceEquals(m_taskModules, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_taskModules = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_taskControlOperation != null)
            {
                children.Add(m_taskControlOperation);
            }

            if (m_taskModules != null)
            {
                children.Add(m_taskModules);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_taskControlOperation, child))
            {
                m_taskControlOperation = null;
                return;
            }

            if (Object.ReferenceEquals(m_taskModules, child))
            {
                m_taskModules = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.TaskControlOperation:
                {
                    if (createOrReplace)
                    {
                        if (TaskControlOperation == null)
                        {
                            if (replacement == null)
                            {
                                TaskControlOperation = new TaskControlOperationTypeState(this);
                            }
                            else
                            {
                                TaskControlOperation = (TaskControlOperationTypeState)replacement;
                            }
                        }
                    }

                    instance = TaskControlOperation;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.TaskModules:
                {
                    if (createOrReplace)
                    {
                        if (TaskModules == null)
                        {
                            if (replacement == null)
                            {
                                TaskModules = new FolderState(this);
                            }
                            else
                            {
                                TaskModules = (FolderState)replacement;
                            }
                        }
                    }

                    instance = TaskModules;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private TaskControlOperationTypeState m_taskControlOperation;
        private FolderState m_taskModules;
        #endregion
    }
    #endif
    #endregion

    #region UserTypeState Class
    #if (!OPCUA_EXCLUDE_UserTypeState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class UserTypeState : BaseObjectState
    {
        #region Constructors
        public UserTypeState(NodeState parent) : base(parent)
        {
        }

        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(Opc.Ua.Robotics.ObjectTypes.UserType, Opc.Ua.Robotics.Namespaces.Robotics, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);

            if (Name != null)
            {
                Name.Initialize(context, Name_InitializationString);
            }
        }

        #region Initialization String
        private const string Name_InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////81" +
           "YIkKAgAAAAEABAAAAE5hbWUBAQFHAwAAAAA4AAAAVGhlIG5hbWUgZm9yIHRoZSBjdXJyZW50IHVzZXIg" +
           "d2l0aGluIHRoZSBjb250cm9sIHN5c3RlbS4ALgBEAUcAAAAM/////wEB/////wAAAAA=";

        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YIACAQAAAAEAEAAAAFVzZXJUeXBlSW5zdGFuY2UBAf9GAQH/Rv9GAAD/////AgAAADVgiQoCAAAAAQAF" +
           "AAAATGV2ZWwBAQBHAwAAAABtAAAAUHJvdmlkZXMgaW5mb3JtYXRpb24gYWJvdXQgdGhlIGFjY2VzcyBy" +
           "aWdodHMgYW5kIGRldGVybWluZXMgd2hhdCBjYW4gYmUgdmlld2VkLCB1cGRhdGVkLCBvciBkZWxldGVk" +
           "IGJ5IGEgdXNlcgAuAEQARwAAAAz/////AQH/////AAAAADVgiQoCAAAAAQAEAAAATmFtZQEBAUcDAAAA" +
           "ADgAAABUaGUgbmFtZSBmb3IgdGhlIGN1cnJlbnQgdXNlciB3aXRoaW4gdGhlIGNvbnRyb2wgc3lzdGVt" +
           "LgAuAEQBRwAAAAz/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        public PropertyState<string> Level
        {
            get => m_level;

            set
            {
                if (!Object.ReferenceEquals(m_level, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_level = value;
            }
        }

        public PropertyState<string> Name
        {
            get => m_name;

            set
            {
                if (!Object.ReferenceEquals(m_name, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_name = value;
            }
        }
        #endregion

        #region Overridden Methods
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_level != null)
            {
                children.Add(m_level);
            }

            if (m_name != null)
            {
                children.Add(m_name);
            }

            base.GetChildren(context, children);
        }
            
        protected override void RemoveExplicitlyDefinedChild(BaseInstanceState child)
        {
            if (Object.ReferenceEquals(m_level, child))
            {
                m_level = null;
                return;
            }

            if (Object.ReferenceEquals(m_name, child))
            {
                m_name = null;
                return;
            }

            base.RemoveExplicitlyDefinedChild(child);
        }

        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case Opc.Ua.Robotics.BrowseNames.Level:
                {
                    if (createOrReplace)
                    {
                        if (Level == null)
                        {
                            if (replacement == null)
                            {
                                Level = new PropertyState<string>(this);
                            }
                            else
                            {
                                Level = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Level;
                    break;
                }

                case Opc.Ua.Robotics.BrowseNames.Name:
                {
                    if (createOrReplace)
                    {
                        if (Name == null)
                        {
                            if (replacement == null)
                            {
                                Name = new PropertyState<string>(this);
                            }
                            else
                            {
                                Name = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = Name;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<string> m_level;
        private PropertyState<string> m_name;
        #endregion
    }
    #endif
    #endregion

    #region StartMethodState Class
    #if (!OPCUA_EXCLUDE_StartMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class StartMethodState : MethodState
    {
        #region Constructors
        public StartMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new StartMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEADwAAAFN0YXJ0TWV0aG9kVHlwZQEBAAABAQAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public StartMethodStateMethodCallHandler OnCall;

        public StartMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            StartMethodStateResult _result = null;

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult StartMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ref int status);

    /// <exclude />
    public partial class StartMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<StartMethodStateResult> StartMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region StopMethodState Class
    #if (!OPCUA_EXCLUDE_StopMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class StopMethodState : MethodState
    {
        #region Constructors
        public StopMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new StopMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEADgAAAFN0b3BNZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public StopMethodStateMethodCallHandler OnCall;

        public StopMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            long stopMode = (long)_inputArguments[0];

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    stopMode,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            StopMethodStateResult _result = null;

            long stopMode = (long)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    stopMode,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult StopMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        long stopMode,
        ref int status);

    /// <exclude />
    public partial class StopMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<StopMethodStateResult> StopMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        long stopMode,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region GetReadyMethodState Class
    #if (!OPCUA_EXCLUDE_GetReadyMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class GetReadyMethodState : MethodState
    {
        #region Constructors
        public GetReadyMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new GetReadyMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAEgAAAEdldFJlYWR5TWV0aG9kVHlwZQEBAAABAQAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public GetReadyMethodStateMethodCallHandler OnCall;

        public GetReadyMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            GetReadyMethodStateResult _result = null;

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult GetReadyMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ref int status);

    /// <exclude />
    public partial class GetReadyMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<GetReadyMethodStateResult> GetReadyMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region StandDownMethodState Class
    #if (!OPCUA_EXCLUDE_StandDownMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class StandDownMethodState : MethodState
    {
        #region Constructors
        public StandDownMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new StandDownMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAEwAAAFN0YW5kRG93bk1ldGhvZFR5cGUBAQAAAQEAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public StandDownMethodStateMethodCallHandler OnCall;

        public StandDownMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            StandDownMethodStateResult _result = null;

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult StandDownMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ref int status);

    /// <exclude />
    public partial class StandDownMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<StandDownMethodStateResult> StandDownMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region LoadByNameMethodState Class
    #if (!OPCUA_EXCLUDE_LoadByNameMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class LoadByNameMethodState : MethodState
    {
        #region Constructors
        public LoadByNameMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new LoadByNameMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFAAAAExvYWRCeU5hbWVNZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public LoadByNameMethodStateMethodCallHandler OnCall;

        public LoadByNameMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string name = (string)_inputArguments[0];

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    name,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            LoadByNameMethodStateResult _result = null;

            string name = (string)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    name,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult LoadByNameMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string name,
        ref int status);

    /// <exclude />
    public partial class LoadByNameMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<LoadByNameMethodStateResult> LoadByNameMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string name,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region LoadByNodeIdMethodState Class
    #if (!OPCUA_EXCLUDE_LoadByNodeIdMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class LoadByNodeIdMethodState : MethodState
    {
        #region Constructors
        public LoadByNodeIdMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new LoadByNodeIdMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFgAAAExvYWRCeU5vZGVJZE1ldGhvZFR5cGUBAQAAAQEAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public LoadByNodeIdMethodStateMethodCallHandler OnCall;

        public LoadByNodeIdMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            ExpandedNodeId id = (ExpandedNodeId)_inputArguments[0];

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    id,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            LoadByNodeIdMethodStateResult _result = null;

            ExpandedNodeId id = (ExpandedNodeId)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    id,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult LoadByNodeIdMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ExpandedNodeId id,
        ref int status);

    /// <exclude />
    public partial class LoadByNodeIdMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<LoadByNodeIdMethodStateResult> LoadByNodeIdMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ExpandedNodeId id,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region UnloadByNameMethodState Class
    #if (!OPCUA_EXCLUDE_UnloadByNameMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class UnloadByNameMethodState : MethodState
    {
        #region Constructors
        public UnloadByNameMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new UnloadByNameMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFgAAAFVubG9hZEJ5TmFtZU1ldGhvZFR5cGUBAQAAAQEAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public UnloadByNameMethodStateMethodCallHandler OnCall;

        public UnloadByNameMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string name = (string)_inputArguments[0];

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    name,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            UnloadByNameMethodStateResult _result = null;

            string name = (string)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    name,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult UnloadByNameMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string name,
        ref int status);

    /// <exclude />
    public partial class UnloadByNameMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<UnloadByNameMethodStateResult> UnloadByNameMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string name,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region UnloadByNodeIdMethodState Class
    #if (!OPCUA_EXCLUDE_UnloadByNodeIdMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class UnloadByNodeIdMethodState : MethodState
    {
        #region Constructors
        public UnloadByNodeIdMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new UnloadByNodeIdMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAGAAAAFVubG9hZEJ5Tm9kZUlkTWV0aG9kVHlwZQEBAAABAQAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public UnloadByNodeIdMethodStateMethodCallHandler OnCall;

        public UnloadByNodeIdMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            ExpandedNodeId id = (ExpandedNodeId)_inputArguments[0];

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    id,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            UnloadByNodeIdMethodStateResult _result = null;

            ExpandedNodeId id = (ExpandedNodeId)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    id,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult UnloadByNodeIdMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ExpandedNodeId id,
        ref int status);

    /// <exclude />
    public partial class UnloadByNodeIdMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<UnloadByNodeIdMethodStateResult> UnloadByNodeIdMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ExpandedNodeId id,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region UnloadProgramMethodState Class
    #if (!OPCUA_EXCLUDE_UnloadProgramMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class UnloadProgramMethodState : MethodState
    {
        #region Constructors
        public UnloadProgramMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new UnloadProgramMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFwAAAFVubG9hZFByb2dyYW1NZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public UnloadProgramMethodStateMethodCallHandler OnCall;

        public UnloadProgramMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            UnloadProgramMethodStateResult _result = null;

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult UnloadProgramMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ref int status);

    /// <exclude />
    public partial class UnloadProgramMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<UnloadProgramMethodStateResult> UnloadProgramMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region ResetToProgramStartMethodState Class
    #if (!OPCUA_EXCLUDE_ResetToProgramStartMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class ResetToProgramStartMethodState : MethodState
    {
        #region Constructors
        public ResetToProgramStartMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new ResetToProgramStartMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAHQAAAFJlc2V0VG9Qcm9ncmFtU3RhcnRNZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public ResetToProgramStartMethodStateMethodCallHandler OnCall;

        public ResetToProgramStartMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            int status = (int)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    ref status);
            }

            _outputArguments[0] = status;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            ResetToProgramStartMethodStateResult _result = null;

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.Status;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult ResetToProgramStartMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        ref int status);

    /// <exclude />
    public partial class ResetToProgramStartMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public int Status { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<ResetToProgramStartMethodStateResult> ResetToProgramStartMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region AcknowledgeMethodState Class
    #if (!OPCUA_EXCLUDE_AcknowledgeMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AcknowledgeMethodState : MethodState
    {
        #region Constructors
        public AcknowledgeMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new AcknowledgeMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFQAAAEFja25vd2xlZGdlTWV0aG9kVHlwZQEBAAABAQAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public AcknowledgeMethodStateMethodCallHandler OnCall;

        public AcknowledgeMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            byte[] eventId = (byte[])_inputArguments[0];
            LocalizedText comment = (LocalizedText)_inputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    eventId,
                    comment);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            AcknowledgeMethodStateResult _result = null;

            byte[] eventId = (byte[])_inputArguments[0];
            LocalizedText comment = (LocalizedText)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    eventId,
                    comment,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult AcknowledgeMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        byte[] eventId,
        LocalizedText comment);

    /// <exclude />
    public partial class AcknowledgeMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<AcknowledgeMethodStateResult> AcknowledgeMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        byte[] eventId,
        LocalizedText comment,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region AddCommentMethodState Class
    #if (!OPCUA_EXCLUDE_AddCommentMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class AddCommentMethodState : MethodState
    {
        #region Constructors
        public AddCommentMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new AddCommentMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFAAAAEFkZENvbW1lbnRNZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public AddCommentMethodStateMethodCallHandler OnCall;

        public AddCommentMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            byte[] eventId = (byte[])_inputArguments[0];
            LocalizedText comment = (LocalizedText)_inputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    eventId,
                    comment);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            AddCommentMethodStateResult _result = null;

            byte[] eventId = (byte[])_inputArguments[0];
            LocalizedText comment = (LocalizedText)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    eventId,
                    comment,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult AddCommentMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        byte[] eventId,
        LocalizedText comment);

    /// <exclude />
    public partial class AddCommentMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<AddCommentMethodStateResult> AddCommentMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        byte[] eventId,
        LocalizedText comment,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region CreateDirectoryMethodState Class
    #if (!OPCUA_EXCLUDE_CreateDirectoryMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CreateDirectoryMethodState : MethodState
    {
        #region Constructors
        public CreateDirectoryMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new CreateDirectoryMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAGQAAAENyZWF0ZURpcmVjdG9yeU1ldGhvZFR5cGUBAQAAAQEAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public CreateDirectoryMethodStateMethodCallHandler OnCall;

        public CreateDirectoryMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string directoryName = (string)_inputArguments[0];

            NodeId directoryNodeId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    directoryName,
                    ref directoryNodeId);
            }

            _outputArguments[0] = directoryNodeId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            CreateDirectoryMethodStateResult _result = null;

            string directoryName = (string)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    directoryName,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.DirectoryNodeId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult CreateDirectoryMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string directoryName,
        ref NodeId directoryNodeId);

    /// <exclude />
    public partial class CreateDirectoryMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId DirectoryNodeId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<CreateDirectoryMethodStateResult> CreateDirectoryMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string directoryName,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region CreateFileMethodState Class
    #if (!OPCUA_EXCLUDE_CreateFileMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class CreateFileMethodState : MethodState
    {
        #region Constructors
        public CreateFileMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new CreateFileMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFAAAAENyZWF0ZUZpbGVNZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public CreateFileMethodStateMethodCallHandler OnCall;

        public CreateFileMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            string fileName = (string)_inputArguments[0];
            bool requestFileOpen = (bool)_inputArguments[1];

            NodeId fileNodeId = (NodeId)_outputArguments[0];
            uint fileHandle = (uint)_outputArguments[1];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    fileName,
                    requestFileOpen,
                    ref fileNodeId,
                    ref fileHandle);
            }

            _outputArguments[0] = fileNodeId;
            _outputArguments[1] = fileHandle;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            CreateFileMethodStateResult _result = null;

            string fileName = (string)_inputArguments[0];
            bool requestFileOpen = (bool)_inputArguments[1];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    fileName,
                    requestFileOpen,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.FileNodeId;
            _outputArguments[1] = _result.FileHandle;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult CreateFileMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string fileName,
        bool requestFileOpen,
        ref NodeId fileNodeId,
        ref uint fileHandle);

    /// <exclude />
    public partial class CreateFileMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId FileNodeId { get; set; }
        public uint FileHandle { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<CreateFileMethodStateResult> CreateFileMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        string fileName,
        bool requestFileOpen,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region DeleteMethodState Class
    #if (!OPCUA_EXCLUDE_DeleteMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class DeleteMethodState : MethodState
    {
        #region Constructors
        public DeleteMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new DeleteMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAEAAAAERlbGV0ZU1ldGhvZFR5cGUBAQAAAQEAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public DeleteMethodStateMethodCallHandler OnCall;

        public DeleteMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId objectToDelete = (NodeId)_inputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    objectToDelete);
            }

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            DeleteMethodStateResult _result = null;

            NodeId objectToDelete = (NodeId)_inputArguments[0];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    objectToDelete,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult DeleteMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId objectToDelete);

    /// <exclude />
    public partial class DeleteMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<DeleteMethodStateResult> DeleteMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId objectToDelete,
        CancellationToken cancellationToken);
    #endif
    #endregion

    #region MoveOrCopyMethodState Class
    #if (!OPCUA_EXCLUDE_MoveOrCopyMethodState)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    public partial class MoveOrCopyMethodState : MethodState
    {
        #region Constructors
        public MoveOrCopyMethodState(NodeState parent) : base(parent)
        {
        }

        public new static NodeState Construct(NodeState parent)
        {
            return new MoveOrCopyMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        protected override void Initialize(ISystemContext context)
        {
            base.Initialize(context);
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAACUAAABodHRwOi8vb3BjZm91bmRhdGlvbi5vcmcvVUEvUm9ib3RpY3MvHwAAAGh0dHA6Ly9vcGNm" +
           "b3VuZGF0aW9uLm9yZy9VQS9ESS8fAAAAaHR0cDovL29wY2ZvdW5kYXRpb24ub3JnL1VBL0lBL/////8E" +
           "YYIABAAAAAEAFAAAAE1vdmVPckNvcHlNZXRob2RUeXBlAQEAAAEBAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        public MoveOrCopyMethodStateMethodCallHandler OnCall;

        public MoveOrCopyMethodStateMethodAsyncCallHandler OnCallAsync;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult _result = null;

            NodeId objectToMoveOrCopy = (NodeId)_inputArguments[0];
            NodeId targetDirectory = (NodeId)_inputArguments[1];
            bool createCopy = (bool)_inputArguments[2];
            string newName = (string)_inputArguments[3];

            NodeId newNodeId = (NodeId)_outputArguments[0];

            if (OnCall != null)
            {
                _result = OnCall(
                    _context,
                    this,
                    _objectId,
                    objectToMoveOrCopy,
                    targetDirectory,
                    createCopy,
                    newName,
                    ref newNodeId);
            }

            _outputArguments[0] = newNodeId;

            return _result;
        }

        #if (OPCUA_INCLUDE_ASYNC)
        protected override async ValueTask<ServiceResult> CallAsync(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments,
            CancellationToken cancellationToken = default)
        {
            if (OnCall == null && OnCallAsync == null)
            {
                return await base.CallAsync(_context, _objectId, _inputArguments, _outputArguments, cancellationToken).ConfigureAwait(false);
            }

            MoveOrCopyMethodStateResult _result = null;

            NodeId objectToMoveOrCopy = (NodeId)_inputArguments[0];
            NodeId targetDirectory = (NodeId)_inputArguments[1];
            bool createCopy = (bool)_inputArguments[2];
            string newName = (string)_inputArguments[3];

            if (OnCallAsync != null)
            {
                _result = await OnCallAsync(
                    _context,
                    this,
                    _objectId,
                    objectToMoveOrCopy,
                    targetDirectory,
                    createCopy,
                    newName,
                    cancellationToken).ConfigureAwait(false);
            }
            else if (OnCall != null)
            {
                return Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            _outputArguments[0] = _result.NewNodeId;

            return _result.ServiceResult;
        }
        #endif

        #endregion

        #region Private Fields
        #endregion
    }

    /// <exclude />
    public delegate ServiceResult MoveOrCopyMethodStateMethodCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId objectToMoveOrCopy,
        NodeId targetDirectory,
        bool createCopy,
        string newName,
        ref NodeId newNodeId);

    /// <exclude />
    public partial class MoveOrCopyMethodStateResult
    {
        public ServiceResult ServiceResult { get; set; }
        public NodeId NewNodeId { get; set; }
    }

    /// <exclude />
    public delegate ValueTask<MoveOrCopyMethodStateResult> MoveOrCopyMethodStateMethodAsyncCallHandler(
        ISystemContext _context,
        MethodState _method,
        NodeId _objectId,
        NodeId objectToMoveOrCopy,
        NodeId targetDirectory,
        bool createCopy,
        string newName,
        CancellationToken cancellationToken);
    #endif
    #endregion
}