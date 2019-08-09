/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace unicode
{

    /// <summary>
    ///  this test is used to check unicode handling inside of strings; only one instance but no
    ///  @singleton  to keep things simple; all fields contain one character.
    /// </summary>
    public class Unicode : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("unicode".GetHashCode()) << 32;

        public override string skillName() {
            return "unicode";
        }

        /// <summary>
        /// Create a new unmanaged Unicode. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public Unicode() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public Unicode(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public Unicode(int skillID, string one, string three, string two) : base(skillID) {
            this.one = one;
          this.three = three;
          this.two = two;
        }

        
        protected string _one = null;

        /// <summary>
        ///  contains "1", a one byte string
        /// </summary>
        public string one {
            get {return _one;}
            set {_one = value;}
        }

        
        protected string _three = null;

        /// <summary>
        ///  contains "☢", a three byte string
        /// </summary>
        public string three {
            get {return _three;}
            set {_three = value;}
        }

        
        protected string _two = null;

        /// <summary>
        ///  contains "ö", a two byte string
        /// </summary>
        public string two {
            get {return _two;}
            set {_two = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : Unicode , NamedType {
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
