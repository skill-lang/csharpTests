/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace hintsAll
{

    /// <summary>
    ///  what ever it was before, now it is a singleton
    ///  @todo  provide a test binary to check this hint (where it should be abstract; and a fail, where it has a
    ///  subclass, because it can not be a singleton in that case)
    ///  @note  this is readOnly; should not matter, because it has no mutable state
    /// </summary>
    public class NowASingleton : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("nowasingleton".GetHashCode()) << 32;

        public override string skillName() {
            return "nowasingleton";
        }

        /// <summary>
        /// Create a new unmanaged NowASingleton. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public NowASingleton() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public NowASingleton(int skillID) : base(skillID) {
        }

          /// <summary>
        /// 
        ///  @todo  provide test files with guards 0, 1 and 0xCAFE
        /// </summary>
        static public short guard {
              get {
                  unchecked {
                      return (short)43981;
                  }
              }
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : NowASingleton , NamedType {
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
