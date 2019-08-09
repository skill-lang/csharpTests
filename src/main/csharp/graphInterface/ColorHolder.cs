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
    ///  check that abstract colors are in fact annotations
    /// </summary>
    public class ColorHolder : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("colorholder".GetHashCode()) << 32;

        public override string skillName() {
            return "colorholder";
        }

        /// <summary>
        /// Create a new unmanaged ColorHolder. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public ColorHolder() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public ColorHolder(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public ColorHolder(int skillID, graphInterface.ColoredNode anAbstractNode, graphInterface.Colored anAnnotation) : base(skillID) {
            this.anAbstractNode = anAbstractNode;
          this.anAnnotation = anAnnotation;
        }

        
        protected graphInterface.ColoredNode _anAbstractNode = null;

        public graphInterface.ColoredNode anAbstractNode {
            get {return _anAbstractNode;}
            set {_anAbstractNode = value;}
        }

        
        protected graphInterface.Colored _anAnnotation = null;

        public graphInterface.Colored anAnnotation {
            get {return _anAnnotation;}
            set {_anAnnotation = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : ColorHolder , NamedType {
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
