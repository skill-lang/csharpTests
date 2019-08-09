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

    public class Term : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("term".GetHashCode()) << 32;

        public override string skillName() {
            return "term";
        }

        /// <summary>
        /// Create a new unmanaged Term. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Term() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Term(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Term(int skillID, System.Collections.ArrayList arguments, restrictionsAll.Operator Zoperator) : base(skillID) {
            this.arguments = arguments;
          this.Zoperator = Zoperator;
        }

        
        protected System.Collections.ArrayList _arguments = null;

        public System.Collections.ArrayList arguments {
            get {return _arguments;}
            set {_arguments = value;}
        }

        
        protected restrictionsAll.Operator _Zoperator = null;

        public restrictionsAll.Operator Zoperator {
            get {return _Zoperator;}
            set {_Zoperator = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Term , NamedType {
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
