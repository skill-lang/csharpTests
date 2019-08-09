/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace restrictionsAll
{

    public class Operator : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("operator".GetHashCode()) << 32;

        public override string skillName() {
            return "operator";
        }

        /// <summary>
        /// Create a new unmanaged Operator. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Operator() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Operator(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Operator(int skillID, string name) : base(skillID) {
            this.name = name;
        }

        
        protected string _name = null;

        public string name {
            get {return _name;}
            set {_name = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Operator , NamedType {
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
