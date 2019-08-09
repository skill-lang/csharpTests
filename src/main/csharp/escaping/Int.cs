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
    ///  Stupid typename
    /// </summary>
    public class Int : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("int".GetHashCode()) << 32;

        public override string skillName() {
            return "int";
        }

        /// <summary>
        /// Create a new unmanaged Int. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Int() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Int(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Int(int skillID, escaping.If Zfor, escaping.Int Zif) : base(skillID) {
            this.Zfor = Zfor;
          this.Zif = Zif;
        }

        
        protected escaping.If _Zfor = null;

        /// <summary>
        ///  Another potential name clash
        /// </summary>
        public escaping.If Zfor {
            get {return _Zfor;}
            set {_Zfor = value;}
        }

        
        protected escaping.Int _Zif = null;

        /// <summary>
        ///  A keyword in most languages
        /// </summary>
        public escaping.Int Zif {
            get {return _Zif;}
            set {_Zif = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Int , NamedType {
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
