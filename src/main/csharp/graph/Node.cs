/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace graph
{

    /// <summary>
    ///  a graph of colored nodes
    /// </summary>
    public class Node : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("node".GetHashCode()) << 32;

        public override string skillName() {
            return "node";
        }

        /// <summary>
        /// Create a new unmanaged Node. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Node() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Node(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Node(int skillID, string color, System.Collections.Generic.HashSet<graph.Node> edges) : base(skillID) {
            this.color = color;
          this.edges = edges;
        }

        
        protected string _color = null;

        public string color {
            get {return _color;}
            set {_color = value;}
        }

        
        protected System.Collections.Generic.HashSet<graph.Node> _edges = null;

        public System.Collections.Generic.HashSet<graph.Node> edges {
            get {return _edges;}
            set {_edges = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Node , NamedType {
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
