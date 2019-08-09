/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = hintsAll.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace hintsAll
{

    public sealed class @internal {
        private @internal() {}


        /**
         * Internal implementation of SkillFile.
         *
         * @author Simon Glaub, Timm Felden
         * @note type access fields start with a capital letter to avoid collisions
         */
        public sealed class SkillState : SkillFile {

            /**
             * Create a new skill file based on argument path and mode.
             *
             * @throws IOException
             *             on IO and mode related errors
             * @throws SkillException
             *             on file or specification consistency errors
             * @note suppress unused warnings, because sometimes type declarations are
             *       created, although nobody is using them
             */
            public static new SkillFile open(string path, params Mode[] mode) {
                ActualMode actualMode = new ActualMode(mode);
                try {
                    switch (actualMode.open) {
                    case Mode.Create:
                        // initialization order of type information has to match file
                        // parser
                        // and can not be done in place
                        StringPool strings = new StringPool(null);
                        List<AbstractStoragePool> types = new List<AbstractStoragePool>(1);
                        StringType stringType = new StringType(strings);
                        Annotation annotation = new Annotation(types);

                        return new SkillState(new Dictionary<string, AbstractStoragePool>(), strings, stringType, annotation,
                                types, FileInputStream.open(path, false), actualMode.close);

                    case Mode.Read:
                        Parser p = new Parser(FileInputStream.open(path, actualMode.close == Mode.ReadOnly));
                        return p.read<SkillState>(typeof(SkillState), actualMode.close);

                    default:
                        throw new System.InvalidOperationException("should never happen");
                    }
                } catch (SkillException e) {
                    // rethrow all skill exceptions
                    throw e;
                } catch (Exception e) {
                    throw new SkillException(e);
                }
            }

            public SkillState(Dictionary<string, AbstractStoragePool> poolByName, StringPool strings, StringType stringType,
                    Annotation annotationType, List<AbstractStoragePool> types, FileInputStream @in, Mode mode) : base(strings, @in.Path, mode, types, poolByName, stringType, annotationType) {

                try {
                    AbstractStoragePool p = null;
                    poolByName.TryGetValue("abuser", out p);
                    AbusersField = (null == p) ? (P0)Parser.newPool("abuser", null, types) : (P0) p;
                    poolByName.TryGetValue("badtype", out p);
                    BadTypesField = (null == p) ? (P1)Parser.newPool("badtype", null, types) : (P1) p;
                    poolByName.TryGetValue("expression", out p);
                    ExpressionsField = (null == p) ? (P2)Parser.newPool("expression", null, types) : (P2) p;
                    poolByName.TryGetValue("externmixin", out p);
                    ExternMixinsField = (null == p) ? (P3)Parser.newPool("externmixin", null, types) : (P3) p;
                    poolByName.TryGetValue("nowasingleton", out p);
                    NowASingletonsField = (null == p) ? (P4)Parser.newPool("nowasingleton", null, types) : (P4) p;
                    poolByName.TryGetValue("uid", out p);
                    UIDsField = (null == p) ? (P5)Parser.newPool("uid", null, types) : (P5) p;
                    poolByName.TryGetValue("user", out p);
                    UsersField = (null == p) ? (P6)Parser.newPool("user", null, types) : (P6) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 AbusersField;

            public override P0 Abusers() {
                return AbusersField;
            }
        
            internal readonly P1 BadTypesField;

            public override P1 BadTypes() {
                return BadTypesField;
            }
        
            internal readonly P2 ExpressionsField;

            public override P2 Expressions() {
                return ExpressionsField;
            }
        
            internal readonly P3 ExternMixinsField;

            public override P3 ExternMixins() {
                return ExternMixinsField;
            }
        
            internal readonly P4 NowASingletonsField;

            public override P4 NowASingletons() {
                return NowASingletonsField;
            }
        
            internal readonly P5 UIDsField;

            public override P5 UIDs() {
                return UIDsField;
            }
        
            internal readonly P6 UsersField;

            public override P6 Users() {
                return UsersField;
            }
        }

        public sealed class Parser : FileParser {

            public Parser(FileInputStream @in) : base(@in, 1) {
            }

            /// <summary>
            /// allocate correct pool type and add it to types
            /// </summary>
            internal static AbstractStoragePool newPool (string name, AbstractStoragePool superPool, List<AbstractStoragePool> types)
            {
                try {
                    switch (name) {
                        case "abuser":
                            return (superPool = new P0(types.Count));
        

                        case "badtype":
                            return (superPool = new P1(types.Count));
        

                        case "expression":
                            return (superPool = new P2(types.Count));
        

                        case "externmixin":
                            return (superPool = new P3(types.Count));
        

                        case "nowasingleton":
                            return (superPool = new P4(types.Count));
        

                        case "uid":
                            return (superPool = new P5(types.Count));
        

                        case "user":
                            return (superPool = new P6(types.Count));
        
                    default:
                        if (null == superPool)
                            return (superPool = new BasePool<SkillObject>(types.Count, name, AbstractStoragePool.noKnownFields, AbstractStoragePool.NoAutoFields));
                        else
                            return (superPool = superPool.makeSubPool(types.Count, name));
                    }
                } finally {
                    types.Add(superPool);
                }
            }

            protected override AbstractStoragePool newPool(string name, AbstractStoragePool superPool, HashSet<TypeRestriction> restrictions) {
                return newPool(name, superPool, types);
            }
        }

        /// <summary>
    ///  Just for fun
    /// </summary>
    public sealed class P0 : BasePool<hintsAll.Abuser> {
        
            protected override hintsAll.Abuser[] newArray(int size) {
                return new hintsAll.Abuser[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "abuser", new string[] { "abusedescription" }, NoAutoFields) {

            }

            internal hintsAll.Abuser[] Data {
                get
                {
                    return (hintsAll.Abuser[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.Abuser(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "abusedescription":
                    unchecked{new f0(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "abusedescription":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Abuser instance with default field values </returns>
            public override object make() {
                hintsAll.Abuser rval = new hintsAll.Abuser();
                add(rval);
                return rval;
            }
        
            /// <returns> a new hintsAll.Abuser instance with the argument field values </returns>
            public hintsAll.Abuser make(string abuseDescription) {
                hintsAll.Abuser rval = new hintsAll.Abuser(-1, abuseDescription);
                add(rval);
                return rval;
            }

            public AbuserBuilder build() {
                return new AbuserBuilder(this, new hintsAll.Abuser());
            }

            /// <summary>
            /// Builder for new Abuser instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class AbuserBuilder : Builder<hintsAll.Abuser> {

                public AbuserBuilder(AbstractStoragePool pool, hintsAll.Abuser instance) : base(pool, instance) {

                }

                public AbuserBuilder abuseDescription(string abuseDescription) {
                    instance.abuseDescription = abuseDescription;
                    return this;
                }

                public override hintsAll.Abuser make() {
                    pool.add(instance);
                    hintsAll.Abuser rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.Abuser.SubType, hintsAll.Abuser> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.Abuser.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : BasePool<hintsAll.BadType> {
        
            protected override hintsAll.BadType[] newArray(int size) {
                return new hintsAll.BadType[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "badtype", new string[] { "ignoreddata", "reflectivelyinvisible" }, NoAutoFields) {

            }

            internal hintsAll.BadType[] Data {
                get
                {
                    return (hintsAll.BadType[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.BadType(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "ignoreddata":
                    unchecked{new f1(@string, this);}
                    return;

                case "reflectivelyinvisible":
                    unchecked{new f2(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "ignoreddata":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "reflectivelyinvisible":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BadType instance with default field values </returns>
            public override object make() {
                hintsAll.BadType rval = new hintsAll.BadType();
                add(rval);
                return rval;
            }
        
            /// <returns> a new hintsAll.BadType instance with the argument field values </returns>
            public hintsAll.BadType make(string ignoredData, string reflectivelyInVisible) {
                hintsAll.BadType rval = new hintsAll.BadType(-1, ignoredData, reflectivelyInVisible);
                add(rval);
                return rval;
            }

            public BadTypeBuilder build() {
                return new BadTypeBuilder(this, new hintsAll.BadType());
            }

            /// <summary>
            /// Builder for new BadType instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BadTypeBuilder : Builder<hintsAll.BadType> {

                public BadTypeBuilder(AbstractStoragePool pool, hintsAll.BadType instance) : base(pool, instance) {

                }

                public BadTypeBuilder ignoredData(string ignoredData) {
                    instance.ignoredData = ignoredData;
                    return this;
                }

                public BadTypeBuilder reflectivelyInVisible(string reflectivelyInVisible) {
                    instance.reflectivelyInVisible = reflectivelyInVisible;
                    return this;
                }

                public override hintsAll.BadType make() {
                    pool.add(instance);
                    hintsAll.BadType rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.BadType.SubType, hintsAll.BadType> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.BadType.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  all expressions are pure
    /// </summary>
    public sealed class P2 : BasePool<hintsAll.Expression> {
        
            protected override hintsAll.Expression[] newArray(int size) {
                return new hintsAll.Expression[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex) : base(poolIndex, "expression", noKnownFields, NoAutoFields) {

            }

            internal hintsAll.Expression[] Data {
                get
                {
                    return (hintsAll.Expression[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.Expression(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Expression instance with default field values </returns>
            public override object make() {
                hintsAll.Expression rval = new hintsAll.Expression();
                add(rval);
                return rval;
            }
        
            public ExpressionBuilder build() {
                return new ExpressionBuilder(this, new hintsAll.Expression());
            }

            /// <summary>
            /// Builder for new Expression instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ExpressionBuilder : Builder<hintsAll.Expression> {

                public ExpressionBuilder(AbstractStoragePool pool, hintsAll.Expression instance) : base(pool, instance) {

                }

                public override hintsAll.Expression make() {
                    pool.add(instance);
                    hintsAll.Expression rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.Expression.SubType, hintsAll.Expression> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.Expression.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  A type mixed into our hirarchy.
    ///  @todo  provide tests for programming languages using actual user defined implementations
    /// </summary>
    public sealed class P3 : BasePool<hintsAll.ExternMixin> {
        
            protected override hintsAll.ExternMixin[] newArray(int size) {
                return new hintsAll.ExternMixin[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex) : base(poolIndex, "externmixin", new string[] { "unknownstuff" }, NoAutoFields) {

            }

            internal hintsAll.ExternMixin[] Data {
                get
                {
                    return (hintsAll.ExternMixin[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.ExternMixin(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "unknownstuff":
                    unchecked{new f3(annotation, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "unknownstuff":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new ExternMixin instance with default field values </returns>
            public override object make() {
                hintsAll.ExternMixin rval = new hintsAll.ExternMixin();
                add(rval);
                return rval;
            }
        
            /// <returns> a new hintsAll.ExternMixin instance with the argument field values </returns>
            public hintsAll.ExternMixin make(de.ust.skill.common.csharp.@internal.SkillObject unknownStuff) {
                hintsAll.ExternMixin rval = new hintsAll.ExternMixin(-1, unknownStuff);
                add(rval);
                return rval;
            }

            public ExternMixinBuilder build() {
                return new ExternMixinBuilder(this, new hintsAll.ExternMixin());
            }

            /// <summary>
            /// Builder for new ExternMixin instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ExternMixinBuilder : Builder<hintsAll.ExternMixin> {

                public ExternMixinBuilder(AbstractStoragePool pool, hintsAll.ExternMixin instance) : base(pool, instance) {

                }

                public ExternMixinBuilder unknownStuff(de.ust.skill.common.csharp.@internal.SkillObject unknownStuff) {
                    instance.unknownStuff = unknownStuff;
                    return this;
                }

                public override hintsAll.ExternMixin make() {
                    pool.add(instance);
                    hintsAll.ExternMixin rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.ExternMixin.SubType, hintsAll.ExternMixin> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.ExternMixin.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  what ever it was before, now it is a singleton
    ///  @todo  provide a test binary to check this hint (where it should be abstract; and a fail, where it has a
    ///  subclass, because it can not be a singleton in that case)
    ///  @note  this is readOnly; should not matter, because it has no mutable state
    /// </summary>
    public sealed class P4 : BasePool<hintsAll.NowASingleton> {
        
            protected override hintsAll.NowASingleton[] newArray(int size) {
                return new hintsAll.NowASingleton[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P4(int poolIndex) : base(poolIndex, "nowasingleton", new string[] { "guard" }, NoAutoFields) {

            }

            internal hintsAll.NowASingleton[] Data {
                get
                {
                    return (hintsAll.NowASingleton[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.NowASingleton(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "guard":
                    unchecked{new f4(new ConstantI16((short)43981), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "guard":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new NowASingleton instance with default field values </returns>
            public override object make() {
                hintsAll.NowASingleton rval = new hintsAll.NowASingleton();
                add(rval);
                return rval;
            }
        
            public NowASingletonBuilder build() {
                return new NowASingletonBuilder(this, new hintsAll.NowASingleton());
            }

            /// <summary>
            /// Builder for new NowASingleton instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class NowASingletonBuilder : Builder<hintsAll.NowASingleton> {

                public NowASingletonBuilder(AbstractStoragePool pool, hintsAll.NowASingleton instance) : base(pool, instance) {

                }

                public override hintsAll.NowASingleton make() {
                    pool.add(instance);
                    hintsAll.NowASingleton rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.NowASingleton.SubType, hintsAll.NowASingleton> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.NowASingleton.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Unique Identifiers are unique and appear as if they were longs
    /// </summary>
    public sealed class P5 : BasePool<hintsAll.UID> {
        
            protected override hintsAll.UID[] newArray(int size) {
                return new hintsAll.UID[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P5(int poolIndex) : base(poolIndex, "uid", new string[] { "identifier" }, NoAutoFields) {

            }

            internal hintsAll.UID[] Data {
                get
                {
                    return (hintsAll.UID[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.UID(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "identifier":
                    unchecked{new f5(I64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "identifier":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new UID instance with default field values </returns>
            public override object make() {
                hintsAll.UID rval = new hintsAll.UID();
                add(rval);
                return rval;
            }
        
            /// <returns> a new hintsAll.UID instance with the argument field values </returns>
            public hintsAll.UID make(long identifier) {
                hintsAll.UID rval = new hintsAll.UID(-1, identifier);
                add(rval);
                return rval;
            }

            public UIDBuilder build() {
                return new UIDBuilder(this, new hintsAll.UID());
            }

            /// <summary>
            /// Builder for new UID instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class UIDBuilder : Builder<hintsAll.UID> {

                public UIDBuilder(AbstractStoragePool pool, hintsAll.UID instance) : base(pool, instance) {

                }

                public UIDBuilder identifier(long identifier) {
                    instance.identifier = identifier;
                    return this;
                }

                public override hintsAll.UID make() {
                    pool.add(instance);
                    hintsAll.UID rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.UID.SubType, hintsAll.UID> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.UID.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  A user has a name and an age.
    /// </summary>
    public sealed class P6 : BasePool<hintsAll.User> {
        
            protected override hintsAll.User[] newArray(int size) {
                return new hintsAll.User[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P6(int poolIndex) : base(poolIndex, "user", new string[] { "age", "name", "reflectivelyvisible" }, NoAutoFields) {

            }

            internal hintsAll.User[] Data {
                get
                {
                    return (hintsAll.User[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new hintsAll.User(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "age":
                    unchecked{new f6(V64.get(), this);}
                    return;

                case "name":
                    unchecked{new f7(@string, this);}
                    return;

                case "reflectivelyvisible":
                    unchecked{new f8(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "age":
                    return new f6((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "name":
                    return new f7((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "reflectivelyvisible":
                    return new f8((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new User instance with default field values </returns>
            public override object make() {
                hintsAll.User rval = new hintsAll.User();
                add(rval);
                return rval;
            }
        
            /// <returns> a new hintsAll.User instance with the argument field values </returns>
            public hintsAll.User make(long age, string name, string reflectivelyVisible) {
                hintsAll.User rval = new hintsAll.User(-1, age, name, reflectivelyVisible);
                add(rval);
                return rval;
            }

            public UserBuilder build() {
                return new UserBuilder(this, new hintsAll.User());
            }

            /// <summary>
            /// Builder for new User instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class UserBuilder : Builder<hintsAll.User> {

                public UserBuilder(AbstractStoragePool pool, hintsAll.User instance) : base(pool, instance) {

                }

                public UserBuilder age(long age) {
                    instance.age = age;
                    return this;
                }

                public UserBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public UserBuilder reflectivelyVisible(string reflectivelyVisible) {
                    instance.reflectivelyVisible = reflectivelyVisible;
                    return this;
                }

                public override hintsAll.User make() {
                    pool.add(instance);
                    hintsAll.User rval = instance;
                    instance = null;
                    return rval;
                }
            }

            /// <summary>
            /// used internally for type forest construction
            /// </summary>
            public override AbstractStoragePool makeSubPool(int index, string name) {
                return new UnknownSubPool(index, name, this);
            }

            private sealed class UnknownSubPool : StoragePool<hintsAll.User.SubType, hintsAll.User> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new hintsAll.User.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// string Abuser.abuseDescription
        /// </summary>
        internal sealed class f0 : KnownDataField<System.String, hintsAll.Abuser> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "abusedescription", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Abuser.abuseDescription but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.Abuser[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].abuseDescription = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                hintsAll.Abuser[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].abuseDescription;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.Abuser[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].abuseDescription, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.Abuser) @ref).abuseDescription;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.Abuser) @ref).abuseDescription = (System.String)value;
            }
        }

        /// <summary>
        /// string BadType.ignoredData
        /// </summary>
        internal sealed class f1 : KnownDataField<System.String, hintsAll.BadType> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "ignoreddata", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in BadType.ignoredData but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.BadType[] d = ((P1) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].ignoredData = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                hintsAll.BadType[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].ignoredData;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.BadType[] d = ((P1) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].ignoredData, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.BadType) @ref).ignoredData;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.BadType) @ref).ignoredData = (System.String)value;
            }
        }

        /// <summary>
        /// string BadType.reflectivelyInVisible
        /// </summary>
        internal sealed class f2 : KnownDataField<System.String, hintsAll.BadType> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "reflectivelyinvisible", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in BadType.reflectivelyInVisible but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.BadType[] d = ((P1) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].reflectivelyInVisible = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                hintsAll.BadType[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].reflectivelyInVisible;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.BadType[] d = ((P1) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].reflectivelyInVisible, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.BadType) @ref).reflectivelyInVisible;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.BadType) @ref).reflectivelyInVisible = (System.String)value;
            }
        }

        /// <summary>
        /// annotation ExternMixin.unknownStuff
        /// </summary>
        internal sealed class f3 : KnownDataField<de.ust.skill.common.csharp.@internal.SkillObject, hintsAll.ExternMixin> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "unknownstuff", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in ExternMixin.unknownStuff but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.ExternMixin[] d = ((P3) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
            d[i].unknownStuff = (de.ust.skill.common.csharp.@internal.SkillObject)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t = (Annotation)this.type;
                hintsAll.ExternMixin[] d = ((P3) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    de.ust.skill.common.csharp.@internal.SkillObject v = d[i].unknownStuff;
                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.ExternMixin[] d = ((P3) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].unknownStuff, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.ExternMixin) @ref).unknownStuff;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.ExternMixin) @ref).unknownStuff = (de.ust.skill.common.csharp.@internal.SkillObject)value;
            }
        }

        /// <summary>
        /// i16 NowASingleton.guard
        /// </summary>
        internal sealed class f4 : KnownDataField<System.Int16, hintsAll.NowASingleton> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "guard", owner) {
                
                if (type.TypeID != 1)
                    throw new SkillException("Expected field type i16 in NowASingleton.guard but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
            }
            public override void osc(int i, int h) {
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
            }


            public override object get(SkillObject @ref) {
                return hintsAll.NowASingleton.guard;
            }

            public override void set(SkillObject @ref, object value) {
                throw new Exception("guard is a constant!");
            }
        }

        /// <summary>
        /// i64 UID.identifier
        /// </summary>
        internal sealed class f5 : KnownDataField<System.Int64, hintsAll.UID> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P5 owner) : base(type, "identifier", owner) {
                
                if (type.TypeID != 10)
                    throw new SkillException("Expected field type i64 in UID.identifier but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.UID[] d = ((P5) owner).Data;
                for (; i != h; i++) {
            d[i].identifier = @in.i64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.UID[] d = ((P5) owner).Data;
                for (; i != h; i++) {
                    @out.i64(d[i].identifier);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.UID) @ref).identifier;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.UID) @ref).identifier = (System.Int64)value;
            }
        }

        /// <summary>
        /// v64 User.age
        /// </summary>
        internal sealed class f6 : KnownDataField<System.Int64, hintsAll.User> {

            public f6(de.ust.skill.common.csharp.@internal.FieldType type, P6 owner) : base(type, "age", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in User.age but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.User[] d = ((P6) owner).Data;
                for (; i != h; i++) {
            d[i].age = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                hintsAll.User[] d = ((P6) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].age);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.User[] d = ((P6) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].age);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.User) @ref).age;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.User) @ref).age = (System.Int64)value;
            }
        }

        /// <summary>
        /// string User.name
        /// </summary>
        internal sealed class f7 : KnownDataField<System.String, hintsAll.User> {

            public f7(de.ust.skill.common.csharp.@internal.FieldType type, P6 owner) : base(type, "name", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in User.name but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.User[] d = ((P6) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].name = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                hintsAll.User[] d = ((P6) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].name;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.User[] d = ((P6) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].name, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.User) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.User) @ref).name = (System.String)value;
            }
        }

        /// <summary>
        /// string User.reflectivelyVisible
        /// </summary>
        internal sealed class f8 : KnownDataField<System.String, hintsAll.User> {

            public f8(de.ust.skill.common.csharp.@internal.FieldType type, P6 owner) : base(type, "reflectivelyvisible", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in User.reflectivelyVisible but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                hintsAll.User[] d = ((P6) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].reflectivelyVisible = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                hintsAll.User[] d = ((P6) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].reflectivelyVisible;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                hintsAll.User[] d = ((P6) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].reflectivelyVisible, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((hintsAll.User) @ref).reflectivelyVisible;
            }

            public override void set(SkillObject @ref, object value) {
                ((hintsAll.User) @ref).reflectivelyVisible = (System.String)value;
            }
        }

    }
}
