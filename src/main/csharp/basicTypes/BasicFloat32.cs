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
    ///  Contains a basic Float32
    /// </summary>
    public class BasicFloat32 : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("basicfloat32".GetHashCode()) << 32;

        public override string skillName() {
            return "basicfloat32";
        }

        /// <summary>
        /// Create a new unmanaged BasicFloat32. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BasicFloat32() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BasicFloat32(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BasicFloat32(int skillID, float basicFloat) : base(skillID) {
            this.basicFloat = basicFloat;
        }

        
        protected float _basicFloat = 0.0f;

        public float basicFloat {
            get {return _basicFloat;}
            set {_basicFloat = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BasicFloat32 , NamedType {
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
