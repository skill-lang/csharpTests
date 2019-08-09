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
    ///  Unique Identifiers are unique and appear as if they were longs
    /// </summary>
    public class UID : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("uid".GetHashCode()) << 32;

        public override string skillName() {
            return "uid";
        }

        /// <summary>
        /// Create a new unmanaged UID. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public UID() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public UID(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public UID(int skillID, long identifier) : base(skillID) {
            this.identifier = identifier;
        }

        
        protected long _identifier = 0;

        public long identifier {
            get {return _identifier;}
            set {_identifier = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : UID , NamedType {
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
