/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace container
{

    public class Container : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("container".GetHashCode()) << 32;

        public override string skillName() {
            return "container";
        }

        /// <summary>
        /// Create a new unmanaged Container. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Container() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Container(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Container(int skillID, System.Collections.ArrayList arr, System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> f, System.Collections.Generic.List<System.Int64> l, System.Collections.Generic.HashSet<System.Int64> s, System.Collections.Generic.HashSet<container.SomethingElse> someSet, System.Collections.ArrayList varr) : base(skillID) {
            this.arr = arr;
          this.f = f;
          this.l = l;
          this.s = s;
          this.someSet = someSet;
          this.varr = varr;
        }

        
        protected System.Collections.ArrayList _arr = null;

        public System.Collections.ArrayList arr {
            get {return _arr;}
            set {_arr = value;}
        }

        
        protected System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> _f = null;

        public System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> f {
            get {return _f;}
            set {_f = value;}
        }

        
        protected System.Collections.Generic.List<System.Int64> _l = null;

        public System.Collections.Generic.List<System.Int64> l {
            get {return _l;}
            set {_l = value;}
        }

        
        protected System.Collections.Generic.HashSet<System.Int64> _s = null;

        public System.Collections.Generic.HashSet<System.Int64> s {
            get {return _s;}
            set {_s = value;}
        }

        
        protected System.Collections.Generic.HashSet<container.SomethingElse> _someSet = null;

        public System.Collections.Generic.HashSet<container.SomethingElse> someSet {
            get {return _someSet;}
            set {_someSet = value;}
        }

        
        protected System.Collections.ArrayList _varr = null;

        public System.Collections.ArrayList varr {
            get {return _varr;}
            set {_varr = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Container , NamedType {
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
