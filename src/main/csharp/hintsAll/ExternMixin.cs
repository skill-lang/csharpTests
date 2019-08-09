/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace hintsAll
{

    /// <summary>
    ///  A type mixed into our hirarchy.
    ///  @todo  provide tests for programming languages using actual user defined implementations
    /// </summary>
    public class ExternMixin : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("externmixin".GetHashCode()) << 32;

        public override string skillName() {
            return "externmixin";
        }

        /// <summary>
        /// Create a new unmanaged ExternMixin. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public ExternMixin() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public ExternMixin(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public ExternMixin(int skillID, de.ust.skill.common.csharp.@internal.SkillObject unknownStuff) : base(skillID) {
            this.unknownStuff = unknownStuff;
        }

        
        protected de.ust.skill.common.csharp.@internal.SkillObject _unknownStuff = null;

        /// <summary>
        ///  what ever it is
        /// </summary>
        public de.ust.skill.common.csharp.@internal.SkillObject unknownStuff {
            get {return _unknownStuff;}
            set {_unknownStuff = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : ExternMixin , NamedType {
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
