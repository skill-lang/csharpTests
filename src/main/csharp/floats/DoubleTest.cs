/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace floats
{

    /// <summary>
    ///  check some double values.
    /// </summary>
    public class DoubleTest : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("doubletest".GetHashCode()) << 32;

        public override string skillName() {
            return "doubletest";
        }

        /// <summary>
        /// Create a new unmanaged DoubleTest. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public DoubleTest() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public DoubleTest(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public DoubleTest(int skillID, double minusZZero, double NaN, double pi, double two, double zero) : base(skillID) {
            this.minusZZero = minusZZero;
          this.NaN = NaN;
          this.pi = pi;
          this.two = two;
          this.zero = zero;
        }

        
        protected double _minusZZero = 0.0f;

        public double minusZZero {
            get {return _minusZZero;}
            set {_minusZZero = value;}
        }

        
        protected double _NaN = 0.0f;

        public double NaN {
            get {return _NaN;}
            set {_NaN = value;}
        }

        
        protected double _pi = 0.0f;

        public double pi {
            get {return _pi;}
            set {_pi = value;}
        }

        
        protected double _two = 0.0f;

        public double two {
            get {return _two;}
            set {_two = value;}
        }

        
        protected double _zero = 0.0f;

        public double zero {
            get {return _zero;}
            set {_zero = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : DoubleTest , NamedType {
            private readonly AbstractStoragePool τPool;

            /// internal use only!!!
            public SubType(AbstractStoragePool τPool, int skillID) : base(skillID) {
                this.τPool = τPool;
            }

            public AbstractStoragePool ΤPool {
                get
                {
                    return τPool;
                }
            }

            public override string skillName() {
                return τPool.Name;
            }

            public override string ToString() {
                return skillName() + "#" + skillID;
            }
        }
    }
}
