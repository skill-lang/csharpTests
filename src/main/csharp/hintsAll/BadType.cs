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

    public class BadType : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("badtype".GetHashCode()) << 32;

        public override string skillName() {
            return "badtype";
        }

        /// <summary>
        /// Create a new unmanaged BadType. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BadType() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BadType(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BadType(int skillID, string ignoredData, string reflectivelyInVisible) : base(skillID) {
            this.ignoredData = ignoredData;
          this.reflectivelyInVisible = reflectivelyInVisible;
        }

        
        protected string _ignoredData = null;

        public string ignoredData {
            get {return _ignoredData;}
            set {_ignoredData = value;}
        }

        
        protected string _reflectivelyInVisible = null;

        /// <summary>
        ///  this field should not be accessible, because the type itself is ignored
        /// </summary>
        public string reflectivelyInVisible {
            get {return _reflectivelyInVisible;}
            set {_reflectivelyInVisible = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BadType , NamedType {
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
