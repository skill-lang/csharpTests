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
    ///  A user has a name and an age.
    /// </summary>
    public class User : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("user".GetHashCode()) << 32;

        public override string skillName() {
            return "user";
        }

        /// <summary>
        /// Create a new unmanaged User. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public User() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public User(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public User(int skillID, long age, string name, string reflectivelyVisible) : base(skillID) {
            this.age = age;
          this.name = name;
          this.reflectivelyVisible = reflectivelyVisible;
        }

        
        protected long _age = 0;

        /// <summary>
        ///  The current age of the user.
        ///  @note  nobody cares for your age
        /// </summary>
        public long age {
            get {return _age;}
            set {_age = value;}
        }

        
        protected string _name = null;

        /// <summary>
        ///  Full name of the user ignoring regional specifics like give or family names.
        ///  @note  no one wants to know your name, until you are in trouble
        /// </summary>
        public string name {
            get {return _name;}
            set {_name = value;}
        }

        
        protected string _reflectivelyVisible = null;

        /// <summary>
        ///  this field can only be seen by reflection
        /// </summary>
        public string reflectivelyVisible {
            get {return _reflectivelyVisible;}
            set {_reflectivelyVisible = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : User , NamedType {
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
