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

    public class Comment : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("comment".GetHashCode()) << 32;

        public override string skillName() {
            return "comment";
        }

        /// <summary>
        /// Create a new unmanaged Comment. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Comment() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Comment(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Comment(int skillID, restrictionsAll.Properties property, de.ust.skill.common.csharp.@internal.SkillObject target, string text) : base(skillID) {
            this.property = property;
          this.target = target;
          this.text = text;
        }

        
        protected restrictionsAll.Properties _property = null;

        public restrictionsAll.Properties property {
            get {return _property;}
            set {_property = value;}
        }

        
        protected de.ust.skill.common.csharp.@internal.SkillObject _target = null;

        public de.ust.skill.common.csharp.@internal.SkillObject target {
            get {return _target;}
            set {_target = value;}
        }

        
        protected string _text = null;

        public string text {
            get {return _text;}
            set {_text = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Comment , NamedType {
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
