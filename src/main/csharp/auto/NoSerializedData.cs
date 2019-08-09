/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace auto
{

    /// <summary>
    ///  All fields of this type are auto.
    ///  @author  Timm Felden
    /// </summary>
    public class NoSerializedData : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("noserializeddata".GetHashCode()) << 32;

        public override string skillName() {
            return "noserializeddata";
        }

        /// <summary>
        /// Create a new unmanaged NoSerializedData. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public NoSerializedData() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public NoSerializedData(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public NoSerializedData(int skillID, long age, string name, bool seen, System.Collections.Generic.List<System.Int32> someIntegersInAList, System.Collections.Generic.Dictionary<System.String, System.String> someMap, auto.NoSerializedData someReference) : base(skillID) {
            this.age = age;
          this.name = name;
          this.seen = seen;
          this.someIntegersInAList = someIntegersInAList;
          this.someMap = someMap;
          this.someReference = someReference;
        }

        [NonSerialized]
        protected long _age = 0;

        public long age {
            get {return _age;}
            set {_age = value;}
        }

        [NonSerialized]
        protected string _name = null;

        public string name {
            get {return _name;}
            set {_name = value;}
        }

        [NonSerialized]
        protected bool _seen = false;

        public bool seen {
            get {return _seen;}
            set {_seen = value;}
        }

        [NonSerialized]
        protected System.Collections.Generic.List<System.Int32> _someIntegersInAList = null;

        public System.Collections.Generic.List<System.Int32> someIntegersInAList {
            get {return _someIntegersInAList;}
            set {_someIntegersInAList = value;}
        }

        [NonSerialized]
        protected System.Collections.Generic.Dictionary<System.String, System.String> _someMap = null;

        public System.Collections.Generic.Dictionary<System.String, System.String> someMap {
            get {return _someMap;}
            set {_someMap = value;}
        }

        [NonSerialized]
        protected auto.NoSerializedData _someReference = null;

        public auto.NoSerializedData someReference {
            get {return _someReference;}
            set {_someReference = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : NoSerializedData , NamedType {
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
