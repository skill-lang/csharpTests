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

    public class AbstractNode : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("abstractnode".GetHashCode()) << 32;

        public override string skillName() {
            return "abstractnode";
        }

        /// <summary>
        /// Create a new unmanaged AbstractNode. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public AbstractNode() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public AbstractNode(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public AbstractNode(int skillID, System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges, System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map) : base(skillID) {
            this.edges = edges;
          this.map = map;
        }

        
        protected System.Collections.Generic.HashSet<graphInterface.ColoredNode> _edges = null;

        public System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges {
            get {return _edges;}
            set {_edges = value;}
        }

        
        protected System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> _map = null;

        public System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map {
            get {return _map;}
            set {_map = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : AbstractNode , NamedType {
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
