/*  ___ _  ___ _ _      
 * / __| |/ (_) | |     
 * \__ \ ' <| | | |__   
 * |___/_|\_\_|_|____|  
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace enums
{

    public class Testenum_third : TestEnum {
        private static readonly long serialVersionUID = 0x5c11L + ("testenum:third".GetHashCode()) << 32;

        public override string skillName() {
            return "testenum:third";
        }

        /// <summary>
        /// Create a new unmanaged Testenum:third. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Testenum_third() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Testenum_third(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Testenum_third(int skillID, string name, enums.TestEnum next) : base(skillID) {
            this.name = name;
          this.next = next;
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Testenum_third , NamedType {
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