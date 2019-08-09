/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace basicTypes
{

    /// <summary>
    ///  Contains a basic Int64 with v64
    /// </summary>
    public class BasicInt64V : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("basicint64v".GetHashCode()) << 32;

        public override string skillName() {
            return "basicint64v";
        }

        /// <summary>
        /// Create a new unmanaged BasicInt64V. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BasicInt64V() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BasicInt64V(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BasicInt64V(int skillID, long basicInt) : base(skillID) {
            this.basicInt = basicInt;
        }

        
        protected long _basicInt = 0;

        public long basicInt {
            get {return _basicInt;}
            set {_basicInt = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BasicInt64V , NamedType {
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
