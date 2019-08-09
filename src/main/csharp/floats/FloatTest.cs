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
    ///  check some float values.
    /// </summary>
    public class FloatTest : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("floattest".GetHashCode()) << 32;

        public override string skillName() {
            return "floattest";
        }

        /// <summary>
        /// Create a new unmanaged FloatTest. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public FloatTest() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public FloatTest(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public FloatTest(int skillID, float minusZZero, float NaN, float pi, float two, float zero) : base(skillID) {
            this.minusZZero = minusZZero;
          this.NaN = NaN;
          this.pi = pi;
          this.two = two;
          this.zero = zero;
        }

        
        protected float _minusZZero = 0.0f;

        public float minusZZero {
            get {return _minusZZero;}
            set {_minusZZero = value;}
        }

        
        protected float _NaN = 0.0f;

        public float NaN {
            get {return _NaN;}
            set {_NaN = value;}
        }

        
        protected float _pi = 0.0f;

        public float pi {
            get {return _pi;}
            set {_pi = value;}
        }

        
        protected float _two = 0.0f;

        public float two {
            get {return _two;}
            set {_two = value;}
        }

        
        protected float _zero = 0.0f;

        public float zero {
            get {return _zero;}
            set {_zero = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : FloatTest , NamedType {
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
