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
    ///  A simple date test with known Translation
    /// </summary>
    public class Date : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("date".GetHashCode()) << 32;

        public override string skillName() {
            return "date";
        }

        /// <summary>
        /// Create a new unmanaged Date. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Date() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Date(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Date(int skillID, long date) : base(skillID) {
            this.date = date;
        }

        
        protected long _date = 0;

        /// <summary>
        ///  seconds since 1.1.1970 UTC
        /// </summary>
        public long date {
            get {return _date;}
            set {_date = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Date , NamedType {
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
