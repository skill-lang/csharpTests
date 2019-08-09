/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace graphInterface
{

    /// <summary>
    ///  check that projection wont interfere with regular subtyping
    /// </summary>
    public class SubNode : Node {
        private static readonly long serialVersionUID = 0x5c11L + ("subnode".GetHashCode()) << 32;

        public override string skillName() {
            return "subnode";
        }

        /// <summary>
        /// Create a new unmanaged SubNode. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public SubNode() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public SubNode(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public SubNode(int skillID, graphInterface.Marker f, graphInterface.Node n, System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges, System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map, graphInterface.Marker next, string color, string mark) : base(skillID) {
            this.f = f;
          this.n = n;
          this.edges = edges;
          this.map = map;
          this.next = next;
          this.color = color;
          this.mark = mark;
        }

        
        protected graphInterface.Marker _f = null;

        public graphInterface.Marker f {
            get {return _f;}
            set {_f = value;}
        }

        
        protected graphInterface.Node _n = null;

        public graphInterface.Node n {
            get {return _n;}
            set {_n = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : SubNode , NamedType {
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
