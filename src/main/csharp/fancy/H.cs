/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using fancy.api;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace fancy
{

    public class H : A {
        private static readonly long serialVersionUID = 0x5c11L + ("h".GetHashCode()) << 32;

        public override string skillName() {
            return "h";
        }

        /// <summary>
        /// Create a new unmanaged H. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public H() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public H(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public H(int skillID, de.ust.skill.common.csharp.@internal.SkillObject a, fancy.A Parent) : base(skillID) {
            this.a = a;
          this.Parent = Parent;
        }

        public _R accept<_R, _A, _E>(Visitor<_R, _A, _E> v, _A arg) where _E : Exception {
            return v.visit(this, arg);
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : H , NamedType {
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
