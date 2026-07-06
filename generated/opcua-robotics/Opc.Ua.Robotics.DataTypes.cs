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
    #region AxisMotionProfileEnumeration Enumeration
    #if (!OPCUA_EXCLUDE_AxisMotionProfileEnumeration)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Opc.Ua.Robotics.Namespaces.Robotics)]
    
    public enum AxisMotionProfileEnumeration
    {
        [EnumMember(Value = "OTHER_0")]
        OTHER = 0,

        [EnumMember(Value = "ROTARY_1")]
        ROTARY = 1,

        [EnumMember(Value = "ROTARY_ENDLESS_2")]
        ROTARY_ENDLESS = 2,

        [EnumMember(Value = "LINEAR_3")]
        LINEAR = 3,

        [EnumMember(Value = "LINEAR_ENDLESS_4")]
        LINEAR_ENDLESS = 4,
    }

    #region AxisMotionProfileEnumerationCollection Class
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [CollectionDataContract(Name = "ListOfAxisMotionProfileEnumeration", Namespace = Opc.Ua.Robotics.Namespaces.Robotics, ItemName = "AxisMotionProfileEnumeration")]
    public partial class AxisMotionProfileEnumerationCollection : List<AxisMotionProfileEnumeration>, ICloneable
    {
        #region Constructors
        public AxisMotionProfileEnumerationCollection() {}

        public AxisMotionProfileEnumerationCollection(int capacity) : base(capacity) {}

        public AxisMotionProfileEnumerationCollection(IEnumerable<AxisMotionProfileEnumeration> collection) : base(collection) {}
        #endregion

        #region Static Operators
        public static implicit operator AxisMotionProfileEnumerationCollection(AxisMotionProfileEnumeration[] values)
        {
            if (values != null)
            {
                return new AxisMotionProfileEnumerationCollection(values);
            }

            return new AxisMotionProfileEnumerationCollection();
        }

        public static explicit operator AxisMotionProfileEnumeration[](AxisMotionProfileEnumerationCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #region ICloneable Methods
        public object Clone()
        {
            return (AxisMotionProfileEnumerationCollection)this.MemberwiseClone();
        }
        #endregion

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            AxisMotionProfileEnumerationCollection clone = new AxisMotionProfileEnumerationCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((AxisMotionProfileEnumeration)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region ExecutionModeEnumeration Enumeration
    #if (!OPCUA_EXCLUDE_ExecutionModeEnumeration)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Opc.Ua.Robotics.Namespaces.Robotics)]
    
    public enum ExecutionModeEnumeration
    {
        [EnumMember(Value = "CYCLE_0")]
        CYCLE = 0,

        [EnumMember(Value = "CONTINUOUS_1")]
        CONTINUOUS = 1,

        [EnumMember(Value = "STEP_2")]
        STEP = 2,
    }

    #region ExecutionModeEnumerationCollection Class
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [CollectionDataContract(Name = "ListOfExecutionModeEnumeration", Namespace = Opc.Ua.Robotics.Namespaces.Robotics, ItemName = "ExecutionModeEnumeration")]
    public partial class ExecutionModeEnumerationCollection : List<ExecutionModeEnumeration>, ICloneable
    {
        #region Constructors
        public ExecutionModeEnumerationCollection() {}

        public ExecutionModeEnumerationCollection(int capacity) : base(capacity) {}

        public ExecutionModeEnumerationCollection(IEnumerable<ExecutionModeEnumeration> collection) : base(collection) {}
        #endregion

        #region Static Operators
        public static implicit operator ExecutionModeEnumerationCollection(ExecutionModeEnumeration[] values)
        {
            if (values != null)
            {
                return new ExecutionModeEnumerationCollection(values);
            }

            return new ExecutionModeEnumerationCollection();
        }

        public static explicit operator ExecutionModeEnumeration[](ExecutionModeEnumerationCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #region ICloneable Methods
        public object Clone()
        {
            return (ExecutionModeEnumerationCollection)this.MemberwiseClone();
        }
        #endregion

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            ExecutionModeEnumerationCollection clone = new ExecutionModeEnumerationCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((ExecutionModeEnumeration)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region MotionDeviceCategoryEnumeration Enumeration
    #if (!OPCUA_EXCLUDE_MotionDeviceCategoryEnumeration)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Opc.Ua.Robotics.Namespaces.Robotics)]
    
    public enum MotionDeviceCategoryEnumeration
    {
        [EnumMember(Value = "OTHER_0")]
        OTHER = 0,

        [EnumMember(Value = "ARTICULATED_ROBOT_1")]
        ARTICULATED_ROBOT = 1,

        [EnumMember(Value = "SCARA_ROBOT_2")]
        SCARA_ROBOT = 2,

        [EnumMember(Value = "CARTESIAN_ROBOT_3")]
        CARTESIAN_ROBOT = 3,

        [EnumMember(Value = "SPHERICAL_ROBOT_4")]
        SPHERICAL_ROBOT = 4,

        [EnumMember(Value = "PARALLEL_ROBOT_5")]
        PARALLEL_ROBOT = 5,

        [EnumMember(Value = "CYLINDRICAL_ROBOT_6")]
        CYLINDRICAL_ROBOT = 6,
    }

    #region MotionDeviceCategoryEnumerationCollection Class
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [CollectionDataContract(Name = "ListOfMotionDeviceCategoryEnumeration", Namespace = Opc.Ua.Robotics.Namespaces.Robotics, ItemName = "MotionDeviceCategoryEnumeration")]
    public partial class MotionDeviceCategoryEnumerationCollection : List<MotionDeviceCategoryEnumeration>, ICloneable
    {
        #region Constructors
        public MotionDeviceCategoryEnumerationCollection() {}

        public MotionDeviceCategoryEnumerationCollection(int capacity) : base(capacity) {}

        public MotionDeviceCategoryEnumerationCollection(IEnumerable<MotionDeviceCategoryEnumeration> collection) : base(collection) {}
        #endregion

        #region Static Operators
        public static implicit operator MotionDeviceCategoryEnumerationCollection(MotionDeviceCategoryEnumeration[] values)
        {
            if (values != null)
            {
                return new MotionDeviceCategoryEnumerationCollection(values);
            }

            return new MotionDeviceCategoryEnumerationCollection();
        }

        public static explicit operator MotionDeviceCategoryEnumeration[](MotionDeviceCategoryEnumerationCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #region ICloneable Methods
        public object Clone()
        {
            return (MotionDeviceCategoryEnumerationCollection)this.MemberwiseClone();
        }
        #endregion

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            MotionDeviceCategoryEnumerationCollection clone = new MotionDeviceCategoryEnumerationCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((MotionDeviceCategoryEnumeration)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region OperationalModeEnumeration Enumeration
    #if (!OPCUA_EXCLUDE_OperationalModeEnumeration)
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Opc.Ua.Robotics.Namespaces.Robotics)]
    
    public enum OperationalModeEnumeration
    {
        [EnumMember(Value = "OTHER_0")]
        OTHER = 0,

        [EnumMember(Value = "MANUAL_REDUCED_SPEED_1")]
        MANUAL_REDUCED_SPEED = 1,

        [EnumMember(Value = "MANUAL_HIGH_SPEED_2")]
        MANUAL_HIGH_SPEED = 2,

        [EnumMember(Value = "AUTOMATIC_3")]
        AUTOMATIC = 3,

        [EnumMember(Value = "AUTOMATIC_EXTERNAL_4")]
        AUTOMATIC_EXTERNAL = 4,
    }

    #region OperationalModeEnumerationCollection Class
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute()]
    [CollectionDataContract(Name = "ListOfOperationalModeEnumeration", Namespace = Opc.Ua.Robotics.Namespaces.Robotics, ItemName = "OperationalModeEnumeration")]
    public partial class OperationalModeEnumerationCollection : List<OperationalModeEnumeration>, ICloneable
    {
        #region Constructors
        public OperationalModeEnumerationCollection() {}

        public OperationalModeEnumerationCollection(int capacity) : base(capacity) {}

        public OperationalModeEnumerationCollection(IEnumerable<OperationalModeEnumeration> collection) : base(collection) {}
        #endregion

        #region Static Operators
        public static implicit operator OperationalModeEnumerationCollection(OperationalModeEnumeration[] values)
        {
            if (values != null)
            {
                return new OperationalModeEnumerationCollection(values);
            }

            return new OperationalModeEnumerationCollection();
        }

        public static explicit operator OperationalModeEnumeration[](OperationalModeEnumerationCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #region ICloneable Methods
        public object Clone()
        {
            return (OperationalModeEnumerationCollection)this.MemberwiseClone();
        }
        #endregion

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            OperationalModeEnumerationCollection clone = new OperationalModeEnumerationCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((OperationalModeEnumeration)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion
}