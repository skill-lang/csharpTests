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

    public class DefaultBoarderCases : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("defaultboardercases".GetHashCode()) << 32;

        public override string skillName() {
            return "defaultboardercases";
        }

        /// <summary>
        /// Create a new unmanaged DefaultBoarderCases. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public DefaultBoarderCases() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public DefaultBoarderCases(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public DefaultBoarderCases(int skillID, float Zfloat, string message, restrictionsAll.Properties none, long nopDefault, de.ust.skill.common.csharp.@internal.SkillObject system) : base(skillID) {
            this.Zfloat = Zfloat;
          this.message = message;
          this.none = none;
          this.nopDefault = nopDefault;
          this.system = system;
        }

        
        protected float _Zfloat = 0.0f;

        public float Zfloat {
            get {return _Zfloat;}
            set {_Zfloat = value;}
        }

        
        protected string _message = null;

        public string message {
            get {return _message;}
            set {_message = value;}
        }

        
        protected restrictionsAll.Properties _none = null;

        public restrictionsAll.Properties none {
            get {return _none;}
            set {_none = value;}
        }

        
        protected long _nopDefault = 0;

        public long nopDefault {
            get {return _nopDefault;}
            set {_nopDefault = value;}
        }

        [NonSerialized]
        protected de.ust.skill.common.csharp.@internal.SkillObject _system = null;

        public de.ust.skill.common.csharp.@internal.SkillObject system {
            get {return _system;}
            set {_system = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : DefaultBoarderCases , NamedType {
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
