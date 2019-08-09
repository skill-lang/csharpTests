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
    ///  Just for fun
    /// </summary>
    public class Abuser : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("abuser".GetHashCode()) << 32;

        public override string skillName() {
            return "abuser";
        }

        /// <summary>
        /// Create a new unmanaged Abuser. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Abuser() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Abuser(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Abuser(int skillID, string abuseDescription) : base(skillID) {
            this.abuseDescription = abuseDescription;
        }

        
        protected string _abuseDescription = null;

        /// <summary>
        ///  provided by debug, case insensitity check
        /// </summary>
        public string abuseDescription {
            get {return _abuseDescription;}
            set {_abuseDescription = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Abuser , NamedType {
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
