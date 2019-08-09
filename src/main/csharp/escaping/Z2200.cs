/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace escaping
{

    /// <summary>
    ///  non-printable unicode characters
    /// </summary>
    public class Z2200 : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("∀".GetHashCode()) << 32;

        public override string skillName() {
            return "∀";
        }

        /// <summary>
        /// Create a new unmanaged ∀. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Z2200() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Z2200(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Z2200(int skillID, escaping.Z2200 Z20ac, string Z2622) : base(skillID) {
            this.Z20ac = Z20ac;
          this.Z2622 = Z2622;
        }

        
        protected escaping.Z2200 _Z20ac = null;

        public escaping.Z2200 Z20ac {
            get {return _Z20ac;}
            set {_Z20ac = value;}
        }

        
        protected string _Z2622 = null;

        public string Z2622 {
            get {return _Z2622;}
            set {_Z2622 = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Z2200 , NamedType {
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
