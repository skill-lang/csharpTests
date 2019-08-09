/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;


namespace basicTypes
{

    /// <summary>
    ///  Contains all basic int types
    /// </summary>
    public class BasicIntegers : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("basicintegers".GetHashCode()) << 32;

        public override string skillName() {
            return "basicintegers";
        }

        /// <summary>
        /// Create a new unmanaged BasicIntegers. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BasicIntegers() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BasicIntegers(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BasicIntegers(int skillID, basicTypes.BasicInt16 int16, basicTypes.BasicInt32 int32, basicTypes.BasicInt64I int64I, basicTypes.BasicInt64V int64V, basicTypes.BasicInt8 int8) : base(skillID) {
            this.int16 = int16;
          this.int32 = int32;
          this.int64I = int64I;
          this.int64V = int64V;
          this.int8 = int8;
        }

        
        protected basicTypes.BasicInt16 _int16 = null;

        public basicTypes.BasicInt16 int16 {
            get {return _int16;}
            set {_int16 = value;}
        }

        
        protected basicTypes.BasicInt32 _int32 = null;

        public basicTypes.BasicInt32 int32 {
            get {return _int32;}
            set {_int32 = value;}
        }

        
        protected basicTypes.BasicInt64I _int64I = null;

        public basicTypes.BasicInt64I int64I {
            get {return _int64I;}
            set {_int64I = value;}
        }

        
        protected basicTypes.BasicInt64V _int64V = null;

        public basicTypes.BasicInt64V int64V {
            get {return _int64V;}
            set {_int64V = value;}
        }

        
        protected basicTypes.BasicInt8 _int8 = null;

        public basicTypes.BasicInt8 int8 {
            get {return _int8;}
            set {_int8 = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BasicIntegers , NamedType {
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
