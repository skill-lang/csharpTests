/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using fancy.api;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace fancy
{

    public class G : C, E, F {
        private static readonly long serialVersionUID = 0x5c11L + ("g".GetHashCode()) << 32;

        public override string skillName() {
            return "g";
        }

        /// <summary>
        /// Create a new unmanaged G. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public G() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public G(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public G(int skillID, fancy.C Value, de.ust.skill.common.csharp.@internal.SkillObject a, fancy.A Parent, System.Collections.Generic.Dictionary<fancy.E, fancy.F> aMap) : base(skillID) {
            this.Value = Value;
          this.a = a;
          this.Parent = Parent;
          this.aMap = aMap;
        }

        public _R accept<_R, _A, _E>(Visitor<_R, _A, _E> v, _A arg) where _E : Exception {
            return v.visit(this, arg);
        }

        
        protected System.Collections.Generic.Dictionary<fancy.E, fancy.F> _aMap = null;

        public System.Collections.Generic.Dictionary<fancy.E, fancy.F> aMap {
            get {return _aMap;}
            set {_aMap = value;}
        }

        public G self() {
            return this;
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : G , NamedType {
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
