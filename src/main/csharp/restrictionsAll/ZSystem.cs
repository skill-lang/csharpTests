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

    /// <summary>
    ///  some properties of the target system
    /// </summary>
    public class ZSystem : Properties {
        private static readonly long serialVersionUID = 0x5c11L + ("system".GetHashCode()) << 32;

        public override string skillName() {
            return "system";
        }

        /// <summary>
        /// Create a new unmanaged System. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public ZSystem() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public ZSystem(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public ZSystem(int skillID, string name, float version) : base(skillID) {
            this.name = name;
          this.version = version;
        }

        
        protected string _name = null;

        public string name {
            get {return _name;}
            set {_name = value;}
        }

        
        protected float _version = 0.0f;

        public float version {
            get {return _version;}
            set {_version = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : ZSystem , NamedType {
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
