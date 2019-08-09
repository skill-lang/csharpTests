/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace unknown
{

    public class A : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("a".GetHashCode()) << 32;

        public override string skillName() {
            return "a";
        }

        /// <summary>
        /// Create a new unmanaged A. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public A() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public A(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public A(int skillID, unknown.A a) : base(skillID) {
            this.a = a;
        }

        
        protected unknown.A _a = null;

        public unknown.A a {
            get {return _a;}
            set {_a = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : A , NamedType {
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
