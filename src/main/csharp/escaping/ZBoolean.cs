/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace escaping
{

    /// <summary>
    ///  Representation of another type.
    ///  @note  Caused by a Bug in the C generator.
    /// </summary>
    public class ZBoolean : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("boolean".GetHashCode()) << 32;

        public override string skillName() {
            return "boolean";
        }

        /// <summary>
        /// Create a new unmanaged Boolean. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public ZBoolean() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public ZBoolean(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public ZBoolean(int skillID, escaping.ZBoolean Zbool, bool boolean) : base(skillID) {
            this.Zbool = Zbool;
          this.boolean = boolean;
        }

        
        protected escaping.ZBoolean _Zbool = null;

        /// <summary>
        ///  reference to a boolean
        /// </summary>
        public escaping.ZBoolean Zbool {
            get {return _Zbool;}
            set {_Zbool = value;}
        }

        
        protected bool _boolean = false;

        /// <summary>
        ///  a flag
        /// </summary>
        public bool boolean {
            get {return _boolean;}
            set {_boolean = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : ZBoolean , NamedType {
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
