/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace restrictionsAll
{

    public class RangeBoarderCases : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("rangeboardercases".GetHashCode()) << 32;

        public override string skillName() {
            return "rangeboardercases";
        }

        /// <summary>
        /// Create a new unmanaged RangeBoarderCases. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public RangeBoarderCases() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public RangeBoarderCases(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public RangeBoarderCases(int skillID, float degrees, double degrees2, int negative, long negative2, sbyte positive, short positive2) : base(skillID) {
            this.degrees = degrees;
          this.degrees2 = degrees2;
          this.negative = negative;
          this.negative2 = negative2;
          this.positive = positive;
          this.positive2 = positive2;
        }

        
        protected float _degrees = 0.0f;

        public float degrees {
            get {return _degrees;}
            set {_degrees = value;}
        }

        
        protected double _degrees2 = 0.0f;

        /// <summary>
        /// 
        ///  @note  very hard to use
        /// </summary>
        public double degrees2 {
            get {return _degrees2;}
            set {_degrees2 = value;}
        }

        
        protected int _negative = 0;

        public int negative {
            get {return _negative;}
            set {_negative = value;}
        }

        
        protected long _negative2 = 0;

        public long negative2 {
            get {return _negative2;}
            set {_negative2 = value;}
        }

        
        protected sbyte _positive = 0;

        public sbyte positive {
            get {return _positive;}
            set {_positive = value;}
        }

        
        protected short _positive2 = 0;

        public short positive2 {
            get {return _positive2;}
            set {_positive2 = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : RangeBoarderCases , NamedType {
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
