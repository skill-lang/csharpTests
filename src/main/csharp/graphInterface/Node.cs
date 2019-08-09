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
    ///  a graph of colored nodes
    /// </summary>
    public class Node : AbstractNode, ColoredNode {
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
        public Node(int skillID, System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges, System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map, graphInterface.Marker next, string color, string mark) : base(skillID) {
            this.edges = edges;
          this.map = map;
          this.next = next;
          this.color = color;
          this.mark = mark;
        }

        
        protected graphInterface.Marker _next = null;

        public graphInterface.Marker next {
            get {return _next;}
            set {_next = value;}
        }

        
        protected string _color = null;

        public string color {
            get {return _color;}
            set {_color = value;}
        }

        
        protected string _mark = null;

        public string mark {
            get {return _mark;}
            set {_mark = value;}
        }

        public Node self() {
            return this;
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
