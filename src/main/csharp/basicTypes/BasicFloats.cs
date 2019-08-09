/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace basicTypes
{

    /// <summary>
    ///  Contains all basic float types
    /// </summary>
    public class BasicFloats : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("basicfloats".GetHashCode()) << 32;

        public override string skillName() {
            return "basicfloats";
        }

        /// <summary>
        /// Create a new unmanaged BasicFloats. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BasicFloats() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BasicFloats(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BasicFloats(int skillID, basicTypes.BasicFloat32 float32, basicTypes.BasicFloat64 float64) : base(skillID) {
            this.float32 = float32;
          this.float64 = float64;
        }

        
        protected basicTypes.BasicFloat32 _float32 = null;

        public basicTypes.BasicFloat32 float32 {
            get {return _float32;}
            set {_float32 = value;}
        }

        
        protected basicTypes.BasicFloat64 _float64 = null;

        public basicTypes.BasicFloat64 float64 {
            get {return _float64;}
            set {_float64 = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BasicFloats , NamedType {
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
