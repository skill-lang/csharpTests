/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using age.api;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace age
{

    /// <summary>
    ///  The age of a person.
    ///  @author  Timm Felden
    /// </summary>
    public class Age : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("age".GetHashCode()) << 32;

        public override string skillName() {
            return "age";
        }

        /// <summary>
        /// Create a new unmanaged Age. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Age() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Age(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Age(int skillID, long age) : base(skillID) {
            this.age = age;
        }

        public _R accept<_R, _A, _E>(Visitor<_R, _A, _E> v, _A arg) where _E : Exception {
            return v.visit(this, arg);
        }

        
        protected long _age = 0;

        /// <summary>
        ///  People have a small positive age, but maybe they will start to live longer in the future, who knows
        /// </summary>
        public long age {
            get {return _age;}
            set {_age = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Age , NamedType {
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
