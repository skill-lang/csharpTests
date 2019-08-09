/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace map3
{

    public class T : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("t".GetHashCode()) << 32;

        public override string skillName() {
            return "t";
        }

        /// <summary>
        /// Create a new unmanaged T. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public T() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public T(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public T(int skillID, System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> Zref) : base(skillID) {
            this.Zref = Zref;
        }

        
        protected System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> _Zref = null;

        public System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> Zref {
            get {return _Zref;}
            set {_Zref = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : T , NamedType {
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
