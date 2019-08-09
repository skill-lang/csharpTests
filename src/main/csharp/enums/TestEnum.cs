/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace enums
{

    /// <summary>
    ///  Test of mapping of enums.
    ///  @author  Timm Felden
    /// </summary>
    public class TestEnum : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("testenum".GetHashCode()) << 32;

        public override string skillName() {
            return "testenum";
        }

        /// <summary>
        /// Create a new unmanaged TestEnum. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public TestEnum() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public TestEnum(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public TestEnum(int skillID, string name, enums.TestEnum next) : base(skillID) {
            this.name = name;
          this.next = next;
        }

        [NonSerialized]
        protected string _name = null;

        /// <summary>
        ///  an application may store a name for each enum value
        /// </summary>
        public string name {
            get {return _name;}
            set {_name = value;}
        }

        
        protected enums.TestEnum _next = null;

        /// <summary>
        ///  a real data field to test (compilable) mapping of fields in the generated code
        /// </summary>
        public enums.TestEnum next {
            get {return _next;}
            set {_next = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : TestEnum , NamedType {
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
