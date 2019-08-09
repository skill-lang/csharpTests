/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace annotation
{

    /// <summary>
    ///  Test the implementation of annotations.
    /// </summary>
    public class Test : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("test".GetHashCode()) << 32;

        public override string skillName() {
            return "test";
        }

        /// <summary>
        /// Create a new unmanaged Test. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Test() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Test(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Test(int skillID, de.ust.skill.common.csharp.@internal.SkillObject f) : base(skillID) {
            this.f = f;
        }

        
        protected de.ust.skill.common.csharp.@internal.SkillObject _f = null;

        /// <summary>
        ///  can point to anything, there are binary files exlpoiting this property.
        /// </summary>
        public de.ust.skill.common.csharp.@internal.SkillObject f {
            get {return _f;}
            set {_f = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Test , NamedType {
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
