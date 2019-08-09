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
    ///  Contains a basic Int32
    /// </summary>
    public class BasicInt32 : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("basicint32".GetHashCode()) << 32;

        public override string skillName() {
            return "basicint32";
        }

        /// <summary>
        /// Create a new unmanaged BasicInt32. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BasicInt32() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BasicInt32(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BasicInt32(int skillID, int basicInt) : base(skillID) {
            this.basicInt = basicInt;
        }

        
        protected int _basicInt = 0;

        public int basicInt {
            get {return _basicInt;}
            set {_basicInt = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BasicInt32 , NamedType {
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
