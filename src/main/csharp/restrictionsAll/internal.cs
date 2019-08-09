/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = restrictionsAll.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace restrictionsAll
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
                    poolByName.TryGetValue("comment", out p);
                    CommentsField = (null == p) ? (P0)Parser.newPool("comment", null, types) : (P0) p;
                    poolByName.TryGetValue("defaultboardercases", out p);
                    DefaultBoarderCasessField = (null == p) ? (P1)Parser.newPool("defaultboardercases", null, types) : (P1) p;
                    poolByName.TryGetValue("operator", out p);
                    OperatorsField = (null == p) ? (P2)Parser.newPool("operator", null, types) : (P2) p;
                    poolByName.TryGetValue("properties", out p);
                    PropertiessField = (null == p) ? (P3)Parser.newPool("properties", null, types) : (P3) p;
                    poolByName.TryGetValue("none", out p);
                    NonesField = (null == p) ? (P4)Parser.newPool("none", PropertiessField, types) : (P4) p;
                    poolByName.TryGetValue("regularproperty", out p);
                    RegularPropertysField = (null == p) ? (P5)Parser.newPool("regularproperty", PropertiessField, types) : (P5) p;
                    poolByName.TryGetValue("system", out p);
                    ZSystemsField = (null == p) ? (P6)Parser.newPool("system", PropertiessField, types) : (P6) p;
                    poolByName.TryGetValue("rangeboardercases", out p);
                    RangeBoarderCasessField = (null == p) ? (P7)Parser.newPool("rangeboardercases", null, types) : (P7) p;
                    poolByName.TryGetValue("term", out p);
                    TermsField = (null == p) ? (P8)Parser.newPool("term", null, types) : (P8) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 CommentsField;

            public override P0 Comments() {
                return CommentsField;
            }
        
            internal readonly P1 DefaultBoarderCasessField;

            public override P1 DefaultBoarderCasess() {
                return DefaultBoarderCasessField;
            }
        
            internal readonly P2 OperatorsField;

            public override P2 Operators() {
                return OperatorsField;
            }
        
            internal readonly P3 PropertiessField;

            public override P3 Propertiess() {
                return PropertiessField;
            }
        
            internal readonly P4 NonesField;

            public override P4 Nones() {
                return NonesField;
            }
        
            internal readonly P5 RegularPropertysField;

            public override P5 RegularPropertys() {
                return RegularPropertysField;
            }
        
            internal readonly P6 ZSystemsField;

            public override P6 ZSystems() {
                return ZSystemsField;
            }
        
            internal readonly P7 RangeBoarderCasessField;

            public override P7 RangeBoarderCasess() {
                return RangeBoarderCasessField;
            }
        
            internal readonly P8 TermsField;

            public override P8 Terms() {
                return TermsField;
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
                        case "comment":
                            return (superPool = new P0(types.Count));
        

                        case "defaultboardercases":
                            return (superPool = new P1(types.Count));
        

                        case "operator":
                            return (superPool = new P2(types.Count));
        

                        case "properties":
                            return (superPool = new P3(types.Count));
        

                        case "none":
                            return (superPool = new P4(types.Count, (P3)superPool));


                        case "regularproperty":
                            return (superPool = new P5(types.Count, (P3)superPool));


                        case "system":
                            return (superPool = new P6(types.Count, (P3)superPool));


                        case "rangeboardercases":
                            return (superPool = new P7(types.Count));
        

                        case "term":
                            return (superPool = new P8(types.Count));
        
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

        public sealed class P0 : BasePool<restrictionsAll.Comment> {
        
            protected override restrictionsAll.Comment[] newArray(int size) {
                return new restrictionsAll.Comment[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "comment", new string[] { "property", "target", "text" }, NoAutoFields) {

            }

            internal restrictionsAll.Comment[] Data {
                get
                {
                    return (restrictionsAll.Comment[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.Comment(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "property":
                    unchecked{new f0((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Propertiess()), this);}
                    return;

                case "target":
                    unchecked{new f1(annotation, this);}
                    return;

                case "text":
                    unchecked{new f2(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "property":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "target":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "text":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Comment instance with default field values </returns>
            public override object make() {
                restrictionsAll.Comment rval = new restrictionsAll.Comment();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsAll.Comment instance with the argument field values </returns>
            public restrictionsAll.Comment make(restrictionsAll.Properties property, de.ust.skill.common.csharp.@internal.SkillObject target, string text) {
                restrictionsAll.Comment rval = new restrictionsAll.Comment(-1, property, target, text);
                add(rval);
                return rval;
            }

            public CommentBuilder build() {
                return new CommentBuilder(this, new restrictionsAll.Comment());
            }

            /// <summary>
            /// Builder for new Comment instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class CommentBuilder : Builder<restrictionsAll.Comment> {

                public CommentBuilder(AbstractStoragePool pool, restrictionsAll.Comment instance) : base(pool, instance) {

                }

                public CommentBuilder property(restrictionsAll.Properties property) {
                    instance.property = property;
                    return this;
                }

                public CommentBuilder target(de.ust.skill.common.csharp.@internal.SkillObject target) {
                    instance.target = target;
                    return this;
                }

                public CommentBuilder text(string text) {
                    instance.text = text;
                    return this;
                }

                public override restrictionsAll.Comment make() {
                    pool.add(instance);
                    restrictionsAll.Comment rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.Comment.SubType, restrictionsAll.Comment> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.Comment.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : BasePool<restrictionsAll.DefaultBoarderCases> {
        
            protected override restrictionsAll.DefaultBoarderCases[] newArray(int size) {
                return new restrictionsAll.DefaultBoarderCases[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "defaultboardercases", new string[] { "float", "message", "none", "nopdefault", "system" }, new IAutoField[1]) {

            }

            internal restrictionsAll.DefaultBoarderCases[] Data {
                get
                {
                    return (restrictionsAll.DefaultBoarderCases[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.DefaultBoarderCases(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "float":
                    unchecked{new f3(F32.get(), this);}
                    return;

                case "message":
                    unchecked{new f4(@string, this);}
                    return;

                case "none":
                    unchecked{new f5((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Propertiess()), this);}
                    return;

                case "nopdefault":
                    unchecked{new f6(V64.get(), this);}
                    return;

                case "system":
                    unchecked{new f7(annotation, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "float":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "message":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "none":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "nopdefault":
                    return new f6((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "system":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new DefaultBoarderCases instance with default field values </returns>
            public override object make() {
                restrictionsAll.DefaultBoarderCases rval = new restrictionsAll.DefaultBoarderCases();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsAll.DefaultBoarderCases instance with the argument field values </returns>
            public restrictionsAll.DefaultBoarderCases make(float Zfloat, string message, restrictionsAll.Properties none, long nopDefault, de.ust.skill.common.csharp.@internal.SkillObject system) {
                restrictionsAll.DefaultBoarderCases rval = new restrictionsAll.DefaultBoarderCases(-1, Zfloat, message, none, nopDefault, system);
                add(rval);
                return rval;
            }

            public DefaultBoarderCasesBuilder build() {
                return new DefaultBoarderCasesBuilder(this, new restrictionsAll.DefaultBoarderCases());
            }

            /// <summary>
            /// Builder for new DefaultBoarderCases instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class DefaultBoarderCasesBuilder : Builder<restrictionsAll.DefaultBoarderCases> {

                public DefaultBoarderCasesBuilder(AbstractStoragePool pool, restrictionsAll.DefaultBoarderCases instance) : base(pool, instance) {

                }

                public DefaultBoarderCasesBuilder Zfloat(float Zfloat) {
                    instance.Zfloat = Zfloat;
                    return this;
                }

                public DefaultBoarderCasesBuilder message(string message) {
                    instance.message = message;
                    return this;
                }

                public DefaultBoarderCasesBuilder none(restrictionsAll.Properties none) {
                    instance.none = none;
                    return this;
                }

                public DefaultBoarderCasesBuilder nopDefault(long nopDefault) {
                    instance.nopDefault = nopDefault;
                    return this;
                }

                public DefaultBoarderCasesBuilder system(de.ust.skill.common.csharp.@internal.SkillObject system) {
                    instance.system = system;
                    return this;
                }

                public override restrictionsAll.DefaultBoarderCases make() {
                    pool.add(instance);
                    restrictionsAll.DefaultBoarderCases rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.DefaultBoarderCases.SubType, restrictionsAll.DefaultBoarderCases> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.DefaultBoarderCases.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P2 : BasePool<restrictionsAll.Operator> {
        
            protected override restrictionsAll.Operator[] newArray(int size) {
                return new restrictionsAll.Operator[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex) : base(poolIndex, "operator", new string[] { "name" }, NoAutoFields) {

            }

            internal restrictionsAll.Operator[] Data {
                get
                {
                    return (restrictionsAll.Operator[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.Operator(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "name":
                    unchecked{new f8(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "name":
                    return new f8((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Operator instance with default field values </returns>
            public override object make() {
                restrictionsAll.Operator rval = new restrictionsAll.Operator();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsAll.Operator instance with the argument field values </returns>
            public restrictionsAll.Operator make(string name) {
                restrictionsAll.Operator rval = new restrictionsAll.Operator(-1, name);
                add(rval);
                return rval;
            }

            public OperatorBuilder build() {
                return new OperatorBuilder(this, new restrictionsAll.Operator());
            }

            /// <summary>
            /// Builder for new Operator instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class OperatorBuilder : Builder<restrictionsAll.Operator> {

                public OperatorBuilder(AbstractStoragePool pool, restrictionsAll.Operator instance) : base(pool, instance) {

                }

                public OperatorBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public override restrictionsAll.Operator make() {
                    pool.add(instance);
                    restrictionsAll.Operator rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.Operator.SubType, restrictionsAll.Operator> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.Operator.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P3 : BasePool<restrictionsAll.Properties> {
        
            protected override restrictionsAll.Properties[] newArray(int size) {
                return new restrictionsAll.Properties[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex) : base(poolIndex, "properties", noKnownFields, NoAutoFields) {

            }

            internal restrictionsAll.Properties[] Data {
                get
                {
                    return (restrictionsAll.Properties[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.Properties(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Properties instance with default field values </returns>
            public override object make() {
                restrictionsAll.Properties rval = new restrictionsAll.Properties();
                add(rval);
                return rval;
            }
        
            public PropertiesBuilder build() {
                return new PropertiesBuilder(this, new restrictionsAll.Properties());
            }

            /// <summary>
            /// Builder for new Properties instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class PropertiesBuilder : Builder<restrictionsAll.Properties> {

                public PropertiesBuilder(AbstractStoragePool pool, restrictionsAll.Properties instance) : base(pool, instance) {

                }

                public override restrictionsAll.Properties make() {
                    pool.add(instance);
                    restrictionsAll.Properties rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.Properties.SubType, restrictionsAll.Properties> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.Properties.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P4 : StoragePool<restrictionsAll.None, restrictionsAll.Properties> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P4(int poolIndex, P3 superPool) : base(poolIndex, "none", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.None(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new None instance with default field values </returns>
            public override object make() {
                restrictionsAll.None rval = new restrictionsAll.None();
                add(rval);
                return rval;
            }
        
            public NoneBuilder build() {
                return new NoneBuilder(this, new restrictionsAll.None());
            }

            /// <summary>
            /// Builder for new None instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class NoneBuilder : Builder<restrictionsAll.None> {

                public NoneBuilder(AbstractStoragePool pool, restrictionsAll.None instance) : base(pool, instance) {

                }

                public override restrictionsAll.None make() {
                    pool.add(instance);
                    restrictionsAll.None rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.None.SubType, restrictionsAll.Properties> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.None.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  some regular property
    /// </summary>
    public sealed class P5 : StoragePool<restrictionsAll.RegularProperty, restrictionsAll.Properties> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P5(int poolIndex, P3 superPool) : base(poolIndex, "regularproperty", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.RegularProperty(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new RegularProperty instance with default field values </returns>
            public override object make() {
                restrictionsAll.RegularProperty rval = new restrictionsAll.RegularProperty();
                add(rval);
                return rval;
            }
        
            public RegularPropertyBuilder build() {
                return new RegularPropertyBuilder(this, new restrictionsAll.RegularProperty());
            }

            /// <summary>
            /// Builder for new RegularProperty instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class RegularPropertyBuilder : Builder<restrictionsAll.RegularProperty> {

                public RegularPropertyBuilder(AbstractStoragePool pool, restrictionsAll.RegularProperty instance) : base(pool, instance) {

                }

                public override restrictionsAll.RegularProperty make() {
                    pool.add(instance);
                    restrictionsAll.RegularProperty rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.RegularProperty.SubType, restrictionsAll.Properties> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.RegularProperty.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  some properties of the target system
    /// </summary>
    public sealed class P6 : StoragePool<restrictionsAll.ZSystem, restrictionsAll.Properties> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P6(int poolIndex, P3 superPool) : base(poolIndex, "system", superPool, new string[] { "name", "version" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.ZSystem(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "name":
                    unchecked{new f9(@string, this);}
                    return;

                case "version":
                    unchecked{new f10(F32.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "name":
                    return new f9((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "version":
                    return new f10((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new ZSystem instance with default field values </returns>
            public override object make() {
                restrictionsAll.ZSystem rval = new restrictionsAll.ZSystem();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsAll.ZSystem instance with the argument field values </returns>
            public restrictionsAll.ZSystem make(string name, float version) {
                restrictionsAll.ZSystem rval = new restrictionsAll.ZSystem(-1, name, version);
                add(rval);
                return rval;
            }

            public ZSystemBuilder build() {
                return new ZSystemBuilder(this, new restrictionsAll.ZSystem());
            }

            /// <summary>
            /// Builder for new ZSystem instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ZSystemBuilder : Builder<restrictionsAll.ZSystem> {

                public ZSystemBuilder(AbstractStoragePool pool, restrictionsAll.ZSystem instance) : base(pool, instance) {

                }

                public ZSystemBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public ZSystemBuilder version(float version) {
                    instance.version = version;
                    return this;
                }

                public override restrictionsAll.ZSystem make() {
                    pool.add(instance);
                    restrictionsAll.ZSystem rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.ZSystem.SubType, restrictionsAll.Properties> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.ZSystem.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P7 : BasePool<restrictionsAll.RangeBoarderCases> {
        
            protected override restrictionsAll.RangeBoarderCases[] newArray(int size) {
                return new restrictionsAll.RangeBoarderCases[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P7(int poolIndex) : base(poolIndex, "rangeboardercases", new string[] { "degrees", "degrees2", "negative", "negative2", "positive", "positive2" }, NoAutoFields) {

            }

            internal restrictionsAll.RangeBoarderCases[] Data {
                get
                {
                    return (restrictionsAll.RangeBoarderCases[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.RangeBoarderCases(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "degrees":
                    unchecked{new f11(F32.get(), this);}
                    return;

                case "degrees2":
                    unchecked{new f12(F64.get(), this);}
                    return;

                case "negative":
                    unchecked{new f13(I32.get(), this);}
                    return;

                case "negative2":
                    unchecked{new f14(V64.get(), this);}
                    return;

                case "positive":
                    unchecked{new f15(I8.get(), this);}
                    return;

                case "positive2":
                    unchecked{new f16(I16.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "degrees":
                    return new f11((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "degrees2":
                    return new f12((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "negative":
                    return new f13((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "negative2":
                    return new f14((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "positive":
                    return new f15((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "positive2":
                    return new f16((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new RangeBoarderCases instance with default field values </returns>
            public override object make() {
                restrictionsAll.RangeBoarderCases rval = new restrictionsAll.RangeBoarderCases();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsAll.RangeBoarderCases instance with the argument field values </returns>
            public restrictionsAll.RangeBoarderCases make(float degrees, double degrees2, int negative, long negative2, sbyte positive, short positive2) {
                restrictionsAll.RangeBoarderCases rval = new restrictionsAll.RangeBoarderCases(-1, degrees, degrees2, negative, negative2, positive, positive2);
                add(rval);
                return rval;
            }

            public RangeBoarderCasesBuilder build() {
                return new RangeBoarderCasesBuilder(this, new restrictionsAll.RangeBoarderCases());
            }

            /// <summary>
            /// Builder for new RangeBoarderCases instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class RangeBoarderCasesBuilder : Builder<restrictionsAll.RangeBoarderCases> {

                public RangeBoarderCasesBuilder(AbstractStoragePool pool, restrictionsAll.RangeBoarderCases instance) : base(pool, instance) {

                }

                public RangeBoarderCasesBuilder degrees(float degrees) {
                    instance.degrees = degrees;
                    return this;
                }

                public RangeBoarderCasesBuilder degrees2(double degrees2) {
                    instance.degrees2 = degrees2;
                    return this;
                }

                public RangeBoarderCasesBuilder negative(int negative) {
                    instance.negative = negative;
                    return this;
                }

                public RangeBoarderCasesBuilder negative2(long negative2) {
                    instance.negative2 = negative2;
                    return this;
                }

                public RangeBoarderCasesBuilder positive(sbyte positive) {
                    instance.positive = positive;
                    return this;
                }

                public RangeBoarderCasesBuilder positive2(short positive2) {
                    instance.positive2 = positive2;
                    return this;
                }

                public override restrictionsAll.RangeBoarderCases make() {
                    pool.add(instance);
                    restrictionsAll.RangeBoarderCases rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.RangeBoarderCases.SubType, restrictionsAll.RangeBoarderCases> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.RangeBoarderCases.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P8 : BasePool<restrictionsAll.Term> {
        
            protected override restrictionsAll.Term[] newArray(int size) {
                return new restrictionsAll.Term[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P8(int poolIndex) : base(poolIndex, "term", new string[] { "arguments", "operator" }, NoAutoFields) {

            }

            internal restrictionsAll.Term[] Data {
                get
                {
                    return (restrictionsAll.Term[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsAll.Term(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "arguments":
                    unchecked{new f17(new VariableLengthArray<restrictionsAll.Term>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Terms())), this);}
                    return;

                case "operator":
                    unchecked{new f18((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Operators()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "arguments":
                    return new f17((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "operator":
                    return new f18((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Term instance with default field values </returns>
            public override object make() {
                restrictionsAll.Term rval = new restrictionsAll.Term();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsAll.Term instance with the argument field values </returns>
            public restrictionsAll.Term make(System.Collections.ArrayList arguments, restrictionsAll.Operator Zoperator) {
                restrictionsAll.Term rval = new restrictionsAll.Term(-1, arguments, Zoperator);
                add(rval);
                return rval;
            }

            public TermBuilder build() {
                return new TermBuilder(this, new restrictionsAll.Term());
            }

            /// <summary>
            /// Builder for new Term instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class TermBuilder : Builder<restrictionsAll.Term> {

                public TermBuilder(AbstractStoragePool pool, restrictionsAll.Term instance) : base(pool, instance) {

                }

                public TermBuilder arguments(System.Collections.ArrayList arguments) {
                    instance.arguments = arguments;
                    return this;
                }

                public TermBuilder Zoperator(restrictionsAll.Operator Zoperator) {
                    instance.Zoperator = Zoperator;
                    return this;
                }

                public override restrictionsAll.Term make() {
                    pool.add(instance);
                    restrictionsAll.Term rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsAll.Term.SubType, restrictionsAll.Term> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsAll.Term.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// properties Comment.property
        /// </summary>
        internal sealed class f0 : KnownDataField<restrictionsAll.Properties, restrictionsAll.Comment> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "property", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("properties"))
                    throw new SkillException("Expected field type properties in Comment.property but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Comment[] d = ((P0) owner).Data;
                P3 t = ((P3)(object)this.type);
                for (; i != h; i++) {
            d[i].property = (restrictionsAll.Properties)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                restrictionsAll.Comment[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    restrictionsAll.Properties instance = d[i].property;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Comment[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    restrictionsAll.Properties v = d[i].property;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.Comment) @ref).property;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.Comment) @ref).property = (restrictionsAll.Properties)value;
            }
        }

        /// <summary>
        /// annotation Comment.target
        /// </summary>
        internal sealed class f1 : KnownDataField<de.ust.skill.common.csharp.@internal.SkillObject, restrictionsAll.Comment> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "target", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in Comment.target but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Comment[] d = ((P0) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
            d[i].target = (de.ust.skill.common.csharp.@internal.SkillObject)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t = (Annotation)this.type;
                restrictionsAll.Comment[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    de.ust.skill.common.csharp.@internal.SkillObject v = d[i].target;
                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Comment[] d = ((P0) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].target, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.Comment) @ref).target;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.Comment) @ref).target = (de.ust.skill.common.csharp.@internal.SkillObject)value;
            }
        }

        /// <summary>
        /// string Comment.text
        /// </summary>
        internal sealed class f2 : KnownDataField<System.String, restrictionsAll.Comment> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "text", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Comment.text but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Comment[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].text = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                restrictionsAll.Comment[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].text;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Comment[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].text, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.Comment) @ref).text;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.Comment) @ref).text = (System.String)value;
            }
        }

        /// <summary>
        /// f32 DefaultBoarderCases.float
        /// </summary>
        internal sealed class f3 : KnownDataField<System.Single, restrictionsAll.DefaultBoarderCases> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "float", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in DefaultBoarderCases.float but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].Zfloat = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].Zfloat);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.DefaultBoarderCases) @ref).Zfloat;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.DefaultBoarderCases) @ref).Zfloat = (System.Single)value;
            }
        }

        /// <summary>
        /// string DefaultBoarderCases.message
        /// </summary>
        internal sealed class f4 : KnownDataField<System.String, restrictionsAll.DefaultBoarderCases> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "message", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in DefaultBoarderCases.message but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].message = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].message;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].message, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.DefaultBoarderCases) @ref).message;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.DefaultBoarderCases) @ref).message = (System.String)value;
            }
        }

        /// <summary>
        /// properties DefaultBoarderCases.none
        /// </summary>
        internal sealed class f5 : KnownDataField<restrictionsAll.Properties, restrictionsAll.DefaultBoarderCases> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "none", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("properties"))
                    throw new SkillException("Expected field type properties in DefaultBoarderCases.none but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                P3 t = ((P3)(object)this.type);
                for (; i != h; i++) {
            d[i].none = (restrictionsAll.Properties)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    restrictionsAll.Properties instance = d[i].none;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    restrictionsAll.Properties v = d[i].none;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.DefaultBoarderCases) @ref).none;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.DefaultBoarderCases) @ref).none = (restrictionsAll.Properties)value;
            }
        }

        /// <summary>
        /// v64 DefaultBoarderCases.nopDefault
        /// </summary>
        internal sealed class f6 : KnownDataField<System.Int64, restrictionsAll.DefaultBoarderCases> {

            public f6(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "nopdefault", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in DefaultBoarderCases.nopDefault but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].nopDefault = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].nopDefault);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.DefaultBoarderCases[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].nopDefault);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.DefaultBoarderCases) @ref).nopDefault;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.DefaultBoarderCases) @ref).nopDefault = (System.Int64)value;
            }
        }

        /// <summary>
        /// annotation DefaultBoarderCases.system
        /// </summary>
        internal sealed class f7 : AutoField<de.ust.skill.common.csharp.@internal.SkillObject, restrictionsAll.DefaultBoarderCases> {

            public f7(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "system", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((restrictionsAll.DefaultBoarderCases) @ref).system;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.DefaultBoarderCases) @ref).system = (de.ust.skill.common.csharp.@internal.SkillObject)value;
            }
        }

        /// <summary>
        /// string Operator.name
        /// </summary>
        internal sealed class f8 : KnownDataField<System.String, restrictionsAll.Operator> {

            public f8(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "name", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Operator.name but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Operator[] d = ((P2) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].name = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                restrictionsAll.Operator[] d = ((P2) owner.basePool).Data;
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
                restrictionsAll.Operator[] d = ((P2) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].name, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.Operator) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.Operator) @ref).name = (System.String)value;
            }
        }

        /// <summary>
        /// string System.name
        /// </summary>
        internal sealed class f9 : KnownDataField<System.String, restrictionsAll.ZSystem> {

            public f9(de.ust.skill.common.csharp.@internal.FieldType type, P6 owner) : base(type, "name", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in System.name but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Properties[] d = ((P3) owner.basePool).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            ((restrictionsAll.ZSystem)d[i]).name = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                restrictionsAll.Properties[] d = ((P3) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = ((restrictionsAll.ZSystem)d[i]).name;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Properties[] d = ((P3) owner.basePool).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(((restrictionsAll.ZSystem)d[i]).name, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.ZSystem) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.ZSystem) @ref).name = (System.String)value;
            }
        }

        /// <summary>
        /// f32 System.version
        /// </summary>
        internal sealed class f10 : KnownDataField<System.Single, restrictionsAll.ZSystem> {

            public f10(de.ust.skill.common.csharp.@internal.FieldType type, P6 owner) : base(type, "version", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in System.version but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Properties[] d = ((P3) owner.basePool).Data;
                for (; i != h; i++) {
            ((restrictionsAll.ZSystem)d[i]).version = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Properties[] d = ((P3) owner.basePool).Data;
                for (; i != h; i++) {
                    @out.f32(((restrictionsAll.ZSystem)d[i]).version);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.ZSystem) @ref).version;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.ZSystem) @ref).version = (System.Single)value;
            }
        }

        /// <summary>
        /// f32 RangeBoarderCases.degrees
        /// </summary>
        internal sealed class f11 : KnownDataField<System.Single, restrictionsAll.RangeBoarderCases> {

            public f11(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "degrees", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in RangeBoarderCases.degrees but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].degrees = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].degrees);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.RangeBoarderCases) @ref).degrees;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.RangeBoarderCases) @ref).degrees = (System.Single)value;
            }
        }

        /// <summary>
        /// f64 RangeBoarderCases.degrees2
        /// </summary>
        internal sealed class f12 : KnownDataField<System.Double, restrictionsAll.RangeBoarderCases> {

            public f12(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "degrees2", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in RangeBoarderCases.degrees2 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].degrees2 = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].degrees2);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.RangeBoarderCases) @ref).degrees2;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.RangeBoarderCases) @ref).degrees2 = (System.Double)value;
            }
        }

        /// <summary>
        /// i32 RangeBoarderCases.negative
        /// </summary>
        internal sealed class f13 : KnownDataField<System.Int32, restrictionsAll.RangeBoarderCases> {

            public f13(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "negative", owner) {
                
                if (type.TypeID != 9)
                    throw new SkillException("Expected field type i32 in RangeBoarderCases.negative but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].negative = @in.i32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.i32(d[i].negative);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.RangeBoarderCases) @ref).negative;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.RangeBoarderCases) @ref).negative = (System.Int32)value;
            }
        }

        /// <summary>
        /// v64 RangeBoarderCases.negative2
        /// </summary>
        internal sealed class f14 : KnownDataField<System.Int64, restrictionsAll.RangeBoarderCases> {

            public f14(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "negative2", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in RangeBoarderCases.negative2 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].negative2 = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].negative2);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].negative2);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.RangeBoarderCases) @ref).negative2;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.RangeBoarderCases) @ref).negative2 = (System.Int64)value;
            }
        }

        /// <summary>
        /// i8 RangeBoarderCases.positive
        /// </summary>
        internal sealed class f15 : KnownDataField<System.SByte, restrictionsAll.RangeBoarderCases> {

            public f15(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "positive", owner) {
                
                if (type.TypeID != 7)
                    throw new SkillException("Expected field type i8 in RangeBoarderCases.positive but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].positive = @in.i8();
                }

            }
            public override void osc(int i, int h) {offset += (h-i);
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.i8(d[i].positive);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.RangeBoarderCases) @ref).positive;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.RangeBoarderCases) @ref).positive = (System.SByte)value;
            }
        }

        /// <summary>
        /// i16 RangeBoarderCases.positive2
        /// </summary>
        internal sealed class f16 : KnownDataField<System.Int16, restrictionsAll.RangeBoarderCases> {

            public f16(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "positive2", owner) {
                
                if (type.TypeID != 8)
                    throw new SkillException("Expected field type i16 in RangeBoarderCases.positive2 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].positive2 = @in.i16();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 1;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.RangeBoarderCases[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.i16(d[i].positive2);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.RangeBoarderCases) @ref).positive2;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.RangeBoarderCases) @ref).positive2 = (System.Int16)value;
            }
        }

        /// <summary>
        /// term[] Term.arguments
        /// </summary>
        internal sealed class f17 : KnownDataField<System.Collections.ArrayList, restrictionsAll.Term> {

            public f17(de.ust.skill.common.csharp.@internal.FieldType type, P8 owner) : base(type, "arguments", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type term[] in Term.arguments but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Term[] d = ((P8) owner).Data;
                VariableLengthArray<restrictionsAll.Term> type = (VariableLengthArray<restrictionsAll.Term>) this.type.cast<restrictionsAll.Term, System.Object>();
                P8 t = ((P8)(object)type.groundType);
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.ArrayList v = new ArrayList(size);
            while (size-- > 0) {
                v.Add((restrictionsAll.Term)t.getByID(@in.v32()));
            }
            d[i].arguments = v;
                }

            }
            public override void osc(int i, int h) {
                VariableLengthArray<restrictionsAll.Term> type = (VariableLengthArray<restrictionsAll.Term>) this.type.cast<restrictionsAll.Term, System.Object>();
                restrictionsAll.Term[] d = ((P8) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.ArrayList v = null == d[i].arguments ? null : (System.Collections.ArrayList)d[i].arguments;

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(restrictionsAll.Term x in v)
                    result += null==x?1:V64.singleV64Offset(x.SkillID);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Term[] d = ((P8) owner).Data;
                VariableLengthArray<restrictionsAll.Term> type = (VariableLengthArray<restrictionsAll.Term>) this.type.cast<restrictionsAll.Term, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.ArrayList x = d[i].arguments;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (restrictionsAll.Term e in x){
                restrictionsAll.Term v = e;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.Term) @ref).arguments;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.Term) @ref).arguments = (System.Collections.ArrayList)value;
            }
        }

        /// <summary>
        /// operator Term.operator
        /// </summary>
        internal sealed class f18 : KnownDataField<restrictionsAll.Operator, restrictionsAll.Term> {

            public f18(de.ust.skill.common.csharp.@internal.FieldType type, P8 owner) : base(type, "operator", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("operator"))
                    throw new SkillException("Expected field type operator in Term.operator but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsAll.Term[] d = ((P8) owner).Data;
                P2 t = ((P2)(object)this.type);
                for (; i != h; i++) {
            d[i].Zoperator = (restrictionsAll.Operator)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                restrictionsAll.Term[] d = ((P8) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    restrictionsAll.Operator instance = d[i].Zoperator;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsAll.Term[] d = ((P8) owner).Data;
                for (; i != h; i++) {
                    restrictionsAll.Operator v = d[i].Zoperator;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsAll.Term) @ref).Zoperator;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsAll.Term) @ref).Zoperator = (restrictionsAll.Operator)value;
            }
        }

    }
}
