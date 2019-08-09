/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace constants
{

    /// <summary>
    ///  Check for constant integerers.
    ///  @author  Dennis Przytarski
    /// </summary>
    public class Constant : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("constant".GetHashCode()) << 32;

        public override string skillName() {
            return "constant";
        }

        /// <summary>
        /// Create a new unmanaged Constant. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Constant() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Constant(int skillID) : base(skillID) {
        }

          static public sbyte a {
              get {
                  unchecked {
                      return (sbyte)8;
                  }
              }
        }

          static public short b {
              get {
                  unchecked {
                      return (short)16;
                  }
              }
        }

          static public int c {
              get {
                  unchecked {
                      return 32;
                  }
              }
        }

          static public long d {
              get {
                  unchecked {
                      return 64L;
                  }
              }
        }

          static public long e {
              get {
                  unchecked {
                      return 46L;
                  }
              }
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Constant , NamedType {
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
