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
    ///  Includes all basic types
    /// </summary>
    public class BasicTypes : SkillObject {
        private static readonly long serialVersionUID = 0x5c11L + ("basictypes".GetHashCode()) << 32;

        public override string skillName() {
            return "basictypes";
        }

        /// <summary>
        /// Create a new unmanaged BasicTypes. Allocation of objects without using the
        /// access factory method is discouraged.
        /// </summary>
        public BasicTypes() : base(-1) {

        }

        /// <summary>
        /// Used for internal construction only!
        /// </summary>
        /// <param id=skillID></param>
        public BasicTypes(int skillID) : base(skillID) {
        }

        /// <summary>
        /// Used for internal construction, full allocation.
        /// </summary>
        public BasicTypes(int skillID, basicTypes.BasicBool aBool, System.Collections.Generic.List<System.Single> aList, System.Collections.Generic.Dictionary<System.Int16, System.SByte> aMap, de.ust.skill.common.csharp.@internal.SkillObject anAnnotation, System.Collections.ArrayList anArray, basicTypes.BasicFloats anotherUserType, System.Collections.Generic.HashSet<System.SByte> aSet, basicTypes.BasicString aString, basicTypes.BasicIntegers aUserType) : base(skillID) {
            this.aBool = aBool;
          this.aList = aList;
          this.aMap = aMap;
          this.anAnnotation = anAnnotation;
          this.anArray = anArray;
          this.anotherUserType = anotherUserType;
          this.aSet = aSet;
          this.aString = aString;
          this.aUserType = aUserType;
        }

        
        protected basicTypes.BasicBool _aBool = null;

        public basicTypes.BasicBool aBool {
            get {return _aBool;}
            set {_aBool = value;}
        }

        
        protected System.Collections.Generic.List<System.Single> _aList = null;

        public System.Collections.Generic.List<System.Single> aList {
            get {return _aList;}
            set {_aList = value;}
        }

        
        protected System.Collections.Generic.Dictionary<System.Int16, System.SByte> _aMap = null;

        public System.Collections.Generic.Dictionary<System.Int16, System.SByte> aMap {
            get {return _aMap;}
            set {_aMap = value;}
        }

        
        protected de.ust.skill.common.csharp.@internal.SkillObject _anAnnotation = null;

        public de.ust.skill.common.csharp.@internal.SkillObject anAnnotation {
            get {return _anAnnotation;}
            set {_anAnnotation = value;}
        }

        
        protected System.Collections.ArrayList _anArray = null;

        public System.Collections.ArrayList anArray {
            get {return _anArray;}
            set {_anArray = value;}
        }

        
        protected basicTypes.BasicFloats _anotherUserType = null;

        public basicTypes.BasicFloats anotherUserType {
            get {return _anotherUserType;}
            set {_anotherUserType = value;}
        }

        
        protected System.Collections.Generic.HashSet<System.SByte> _aSet = null;

        public System.Collections.Generic.HashSet<System.SByte> aSet {
            get {return _aSet;}
            set {_aSet = value;}
        }

        
        protected basicTypes.BasicString _aString = null;

        public basicTypes.BasicString aString {
            get {return _aString;}
            set {_aString = value;}
        }

        
        protected basicTypes.BasicIntegers _aUserType = null;

        public basicTypes.BasicIntegers aUserType {
            get {return _aUserType;}
            set {_aUserType = value;}
        }

        /// <summary>
        /// Generic sub types of this type.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public new sealed class SubType : BasicTypes , NamedType {
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
