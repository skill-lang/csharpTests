/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = basicTypes.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace basicTypes
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
                    poolByName.TryGetValue("basicbool", out p);
                    BasicBoolsField = (null == p) ? (P0)Parser.newPool("basicbool", null, types) : (P0) p;
                    poolByName.TryGetValue("basicfloat32", out p);
                    BasicFloat32sField = (null == p) ? (P1)Parser.newPool("basicfloat32", null, types) : (P1) p;
                    poolByName.TryGetValue("basicfloat64", out p);
                    BasicFloat64sField = (null == p) ? (P2)Parser.newPool("basicfloat64", null, types) : (P2) p;
                    poolByName.TryGetValue("basicfloats", out p);
                    BasicFloatssField = (null == p) ? (P3)Parser.newPool("basicfloats", null, types) : (P3) p;
                    poolByName.TryGetValue("basicint16", out p);
                    BasicInt16sField = (null == p) ? (P4)Parser.newPool("basicint16", null, types) : (P4) p;
                    poolByName.TryGetValue("basicint32", out p);
                    BasicInt32sField = (null == p) ? (P5)Parser.newPool("basicint32", null, types) : (P5) p;
                    poolByName.TryGetValue("basicint64i", out p);
                    BasicInt64IsField = (null == p) ? (P6)Parser.newPool("basicint64i", null, types) : (P6) p;
                    poolByName.TryGetValue("basicint64v", out p);
                    BasicInt64VsField = (null == p) ? (P7)Parser.newPool("basicint64v", null, types) : (P7) p;
                    poolByName.TryGetValue("basicint8", out p);
                    BasicInt8sField = (null == p) ? (P8)Parser.newPool("basicint8", null, types) : (P8) p;
                    poolByName.TryGetValue("basicintegers", out p);
                    BasicIntegerssField = (null == p) ? (P9)Parser.newPool("basicintegers", null, types) : (P9) p;
                    poolByName.TryGetValue("basicstring", out p);
                    BasicStringsField = (null == p) ? (P10)Parser.newPool("basicstring", null, types) : (P10) p;
                    poolByName.TryGetValue("basictypes", out p);
                    BasicTypessField = (null == p) ? (P11)Parser.newPool("basictypes", null, types) : (P11) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 BasicBoolsField;

            public override P0 BasicBools() {
                return BasicBoolsField;
            }
        
            internal readonly P1 BasicFloat32sField;

            public override P1 BasicFloat32s() {
                return BasicFloat32sField;
            }
        
            internal readonly P2 BasicFloat64sField;

            public override P2 BasicFloat64s() {
                return BasicFloat64sField;
            }
        
            internal readonly P3 BasicFloatssField;

            public override P3 BasicFloatss() {
                return BasicFloatssField;
            }
        
            internal readonly P4 BasicInt16sField;

            public override P4 BasicInt16s() {
                return BasicInt16sField;
            }
        
            internal readonly P5 BasicInt32sField;

            public override P5 BasicInt32s() {
                return BasicInt32sField;
            }
        
            internal readonly P6 BasicInt64IsField;

            public override P6 BasicInt64Is() {
                return BasicInt64IsField;
            }
        
            internal readonly P7 BasicInt64VsField;

            public override P7 BasicInt64Vs() {
                return BasicInt64VsField;
            }
        
            internal readonly P8 BasicInt8sField;

            public override P8 BasicInt8s() {
                return BasicInt8sField;
            }
        
            internal readonly P9 BasicIntegerssField;

            public override P9 BasicIntegerss() {
                return BasicIntegerssField;
            }
        
            internal readonly P10 BasicStringsField;

            public override P10 BasicStrings() {
                return BasicStringsField;
            }
        
            internal readonly P11 BasicTypessField;

            public override P11 BasicTypess() {
                return BasicTypessField;
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
                        case "basicbool":
                            return (superPool = new P0(types.Count));
        

                        case "basicfloat32":
                            return (superPool = new P1(types.Count));
        

                        case "basicfloat64":
                            return (superPool = new P2(types.Count));
        

                        case "basicfloats":
                            return (superPool = new P3(types.Count));
        

                        case "basicint16":
                            return (superPool = new P4(types.Count));
        

                        case "basicint32":
                            return (superPool = new P5(types.Count));
        

                        case "basicint64i":
                            return (superPool = new P6(types.Count));
        

                        case "basicint64v":
                            return (superPool = new P7(types.Count));
        

                        case "basicint8":
                            return (superPool = new P8(types.Count));
        

                        case "basicintegers":
                            return (superPool = new P9(types.Count));
        

                        case "basicstring":
                            return (superPool = new P10(types.Count));
        

                        case "basictypes":
                            return (superPool = new P11(types.Count));
        
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
    ///  Contains a basic bool
    /// </summary>
    public sealed class P0 : BasePool<basicTypes.BasicBool> {
        
            protected override basicTypes.BasicBool[] newArray(int size) {
                return new basicTypes.BasicBool[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "basicbool", new string[] { "basicbool" }, NoAutoFields) {

            }

            internal basicTypes.BasicBool[] Data {
                get
                {
                    return (basicTypes.BasicBool[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicBool(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicbool":
                    unchecked{new f0(BoolType.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicbool":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicBool instance with default field values </returns>
            public override object make() {
                basicTypes.BasicBool rval = new basicTypes.BasicBool();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicBool instance with the argument field values </returns>
            public basicTypes.BasicBool make(bool basicBool) {
                basicTypes.BasicBool rval = new basicTypes.BasicBool(-1, basicBool);
                add(rval);
                return rval;
            }

            public BasicBoolBuilder build() {
                return new BasicBoolBuilder(this, new basicTypes.BasicBool());
            }

            /// <summary>
            /// Builder for new BasicBool instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicBoolBuilder : Builder<basicTypes.BasicBool> {

                public BasicBoolBuilder(AbstractStoragePool pool, basicTypes.BasicBool instance) : base(pool, instance) {

                }

                public BasicBoolBuilder basicBool(bool basicBool) {
                    instance.basicBool = basicBool;
                    return this;
                }

                public override basicTypes.BasicBool make() {
                    pool.add(instance);
                    basicTypes.BasicBool rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicBool.SubType, basicTypes.BasicBool> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicBool.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Float32
    /// </summary>
    public sealed class P1 : BasePool<basicTypes.BasicFloat32> {
        
            protected override basicTypes.BasicFloat32[] newArray(int size) {
                return new basicTypes.BasicFloat32[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "basicfloat32", new string[] { "basicfloat" }, NoAutoFields) {

            }

            internal basicTypes.BasicFloat32[] Data {
                get
                {
                    return (basicTypes.BasicFloat32[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicFloat32(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicfloat":
                    unchecked{new f1(F32.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicfloat":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicFloat32 instance with default field values </returns>
            public override object make() {
                basicTypes.BasicFloat32 rval = new basicTypes.BasicFloat32();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicFloat32 instance with the argument field values </returns>
            public basicTypes.BasicFloat32 make(float basicFloat) {
                basicTypes.BasicFloat32 rval = new basicTypes.BasicFloat32(-1, basicFloat);
                add(rval);
                return rval;
            }

            public BasicFloat32Builder build() {
                return new BasicFloat32Builder(this, new basicTypes.BasicFloat32());
            }

            /// <summary>
            /// Builder for new BasicFloat32 instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicFloat32Builder : Builder<basicTypes.BasicFloat32> {

                public BasicFloat32Builder(AbstractStoragePool pool, basicTypes.BasicFloat32 instance) : base(pool, instance) {

                }

                public BasicFloat32Builder basicFloat(float basicFloat) {
                    instance.basicFloat = basicFloat;
                    return this;
                }

                public override basicTypes.BasicFloat32 make() {
                    pool.add(instance);
                    basicTypes.BasicFloat32 rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicFloat32.SubType, basicTypes.BasicFloat32> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicFloat32.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Float64
    /// </summary>
    public sealed class P2 : BasePool<basicTypes.BasicFloat64> {
        
            protected override basicTypes.BasicFloat64[] newArray(int size) {
                return new basicTypes.BasicFloat64[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex) : base(poolIndex, "basicfloat64", new string[] { "basicfloat" }, NoAutoFields) {

            }

            internal basicTypes.BasicFloat64[] Data {
                get
                {
                    return (basicTypes.BasicFloat64[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicFloat64(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicfloat":
                    unchecked{new f2(F64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicfloat":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicFloat64 instance with default field values </returns>
            public override object make() {
                basicTypes.BasicFloat64 rval = new basicTypes.BasicFloat64();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicFloat64 instance with the argument field values </returns>
            public basicTypes.BasicFloat64 make(double basicFloat) {
                basicTypes.BasicFloat64 rval = new basicTypes.BasicFloat64(-1, basicFloat);
                add(rval);
                return rval;
            }

            public BasicFloat64Builder build() {
                return new BasicFloat64Builder(this, new basicTypes.BasicFloat64());
            }

            /// <summary>
            /// Builder for new BasicFloat64 instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicFloat64Builder : Builder<basicTypes.BasicFloat64> {

                public BasicFloat64Builder(AbstractStoragePool pool, basicTypes.BasicFloat64 instance) : base(pool, instance) {

                }

                public BasicFloat64Builder basicFloat(double basicFloat) {
                    instance.basicFloat = basicFloat;
                    return this;
                }

                public override basicTypes.BasicFloat64 make() {
                    pool.add(instance);
                    basicTypes.BasicFloat64 rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicFloat64.SubType, basicTypes.BasicFloat64> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicFloat64.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains all basic float types
    /// </summary>
    public sealed class P3 : BasePool<basicTypes.BasicFloats> {
        
            protected override basicTypes.BasicFloats[] newArray(int size) {
                return new basicTypes.BasicFloats[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex) : base(poolIndex, "basicfloats", new string[] { "float32", "float64" }, NoAutoFields) {

            }

            internal basicTypes.BasicFloats[] Data {
                get
                {
                    return (basicTypes.BasicFloats[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicFloats(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "float32":
                    unchecked{new f3((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicFloat32s()), this);}
                    return;

                case "float64":
                    unchecked{new f4((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicFloat64s()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "float32":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "float64":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicFloats instance with default field values </returns>
            public override object make() {
                basicTypes.BasicFloats rval = new basicTypes.BasicFloats();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicFloats instance with the argument field values </returns>
            public basicTypes.BasicFloats make(basicTypes.BasicFloat32 float32, basicTypes.BasicFloat64 float64) {
                basicTypes.BasicFloats rval = new basicTypes.BasicFloats(-1, float32, float64);
                add(rval);
                return rval;
            }

            public BasicFloatsBuilder build() {
                return new BasicFloatsBuilder(this, new basicTypes.BasicFloats());
            }

            /// <summary>
            /// Builder for new BasicFloats instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicFloatsBuilder : Builder<basicTypes.BasicFloats> {

                public BasicFloatsBuilder(AbstractStoragePool pool, basicTypes.BasicFloats instance) : base(pool, instance) {

                }

                public BasicFloatsBuilder float32(basicTypes.BasicFloat32 float32) {
                    instance.float32 = float32;
                    return this;
                }

                public BasicFloatsBuilder float64(basicTypes.BasicFloat64 float64) {
                    instance.float64 = float64;
                    return this;
                }

                public override basicTypes.BasicFloats make() {
                    pool.add(instance);
                    basicTypes.BasicFloats rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicFloats.SubType, basicTypes.BasicFloats> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicFloats.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Int16
    /// </summary>
    public sealed class P4 : BasePool<basicTypes.BasicInt16> {
        
            protected override basicTypes.BasicInt16[] newArray(int size) {
                return new basicTypes.BasicInt16[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P4(int poolIndex) : base(poolIndex, "basicint16", new string[] { "basicint" }, NoAutoFields) {

            }

            internal basicTypes.BasicInt16[] Data {
                get
                {
                    return (basicTypes.BasicInt16[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicInt16(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicint":
                    unchecked{new f5(I16.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicint":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicInt16 instance with default field values </returns>
            public override object make() {
                basicTypes.BasicInt16 rval = new basicTypes.BasicInt16();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicInt16 instance with the argument field values </returns>
            public basicTypes.BasicInt16 make(short basicInt) {
                basicTypes.BasicInt16 rval = new basicTypes.BasicInt16(-1, basicInt);
                add(rval);
                return rval;
            }

            public BasicInt16Builder build() {
                return new BasicInt16Builder(this, new basicTypes.BasicInt16());
            }

            /// <summary>
            /// Builder for new BasicInt16 instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicInt16Builder : Builder<basicTypes.BasicInt16> {

                public BasicInt16Builder(AbstractStoragePool pool, basicTypes.BasicInt16 instance) : base(pool, instance) {

                }

                public BasicInt16Builder basicInt(short basicInt) {
                    instance.basicInt = basicInt;
                    return this;
                }

                public override basicTypes.BasicInt16 make() {
                    pool.add(instance);
                    basicTypes.BasicInt16 rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicInt16.SubType, basicTypes.BasicInt16> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicInt16.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Int32
    /// </summary>
    public sealed class P5 : BasePool<basicTypes.BasicInt32> {
        
            protected override basicTypes.BasicInt32[] newArray(int size) {
                return new basicTypes.BasicInt32[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P5(int poolIndex) : base(poolIndex, "basicint32", new string[] { "basicint" }, NoAutoFields) {

            }

            internal basicTypes.BasicInt32[] Data {
                get
                {
                    return (basicTypes.BasicInt32[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicInt32(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicint":
                    unchecked{new f6(I32.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicint":
                    return new f6((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicInt32 instance with default field values </returns>
            public override object make() {
                basicTypes.BasicInt32 rval = new basicTypes.BasicInt32();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicInt32 instance with the argument field values </returns>
            public basicTypes.BasicInt32 make(int basicInt) {
                basicTypes.BasicInt32 rval = new basicTypes.BasicInt32(-1, basicInt);
                add(rval);
                return rval;
            }

            public BasicInt32Builder build() {
                return new BasicInt32Builder(this, new basicTypes.BasicInt32());
            }

            /// <summary>
            /// Builder for new BasicInt32 instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicInt32Builder : Builder<basicTypes.BasicInt32> {

                public BasicInt32Builder(AbstractStoragePool pool, basicTypes.BasicInt32 instance) : base(pool, instance) {

                }

                public BasicInt32Builder basicInt(int basicInt) {
                    instance.basicInt = basicInt;
                    return this;
                }

                public override basicTypes.BasicInt32 make() {
                    pool.add(instance);
                    basicTypes.BasicInt32 rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicInt32.SubType, basicTypes.BasicInt32> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicInt32.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Int64 with i64
    /// </summary>
    public sealed class P6 : BasePool<basicTypes.BasicInt64I> {
        
            protected override basicTypes.BasicInt64I[] newArray(int size) {
                return new basicTypes.BasicInt64I[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P6(int poolIndex) : base(poolIndex, "basicint64i", new string[] { "basicint" }, NoAutoFields) {

            }

            internal basicTypes.BasicInt64I[] Data {
                get
                {
                    return (basicTypes.BasicInt64I[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicInt64I(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicint":
                    unchecked{new f7(I64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicint":
                    return new f7((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicInt64I instance with default field values </returns>
            public override object make() {
                basicTypes.BasicInt64I rval = new basicTypes.BasicInt64I();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicInt64I instance with the argument field values </returns>
            public basicTypes.BasicInt64I make(long basicInt) {
                basicTypes.BasicInt64I rval = new basicTypes.BasicInt64I(-1, basicInt);
                add(rval);
                return rval;
            }

            public BasicInt64IBuilder build() {
                return new BasicInt64IBuilder(this, new basicTypes.BasicInt64I());
            }

            /// <summary>
            /// Builder for new BasicInt64I instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicInt64IBuilder : Builder<basicTypes.BasicInt64I> {

                public BasicInt64IBuilder(AbstractStoragePool pool, basicTypes.BasicInt64I instance) : base(pool, instance) {

                }

                public BasicInt64IBuilder basicInt(long basicInt) {
                    instance.basicInt = basicInt;
                    return this;
                }

                public override basicTypes.BasicInt64I make() {
                    pool.add(instance);
                    basicTypes.BasicInt64I rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicInt64I.SubType, basicTypes.BasicInt64I> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicInt64I.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Int64 with v64
    /// </summary>
    public sealed class P7 : BasePool<basicTypes.BasicInt64V> {
        
            protected override basicTypes.BasicInt64V[] newArray(int size) {
                return new basicTypes.BasicInt64V[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P7(int poolIndex) : base(poolIndex, "basicint64v", new string[] { "basicint" }, NoAutoFields) {

            }

            internal basicTypes.BasicInt64V[] Data {
                get
                {
                    return (basicTypes.BasicInt64V[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicInt64V(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicint":
                    unchecked{new f8(V64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicint":
                    return new f8((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicInt64V instance with default field values </returns>
            public override object make() {
                basicTypes.BasicInt64V rval = new basicTypes.BasicInt64V();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicInt64V instance with the argument field values </returns>
            public basicTypes.BasicInt64V make(long basicInt) {
                basicTypes.BasicInt64V rval = new basicTypes.BasicInt64V(-1, basicInt);
                add(rval);
                return rval;
            }

            public BasicInt64VBuilder build() {
                return new BasicInt64VBuilder(this, new basicTypes.BasicInt64V());
            }

            /// <summary>
            /// Builder for new BasicInt64V instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicInt64VBuilder : Builder<basicTypes.BasicInt64V> {

                public BasicInt64VBuilder(AbstractStoragePool pool, basicTypes.BasicInt64V instance) : base(pool, instance) {

                }

                public BasicInt64VBuilder basicInt(long basicInt) {
                    instance.basicInt = basicInt;
                    return this;
                }

                public override basicTypes.BasicInt64V make() {
                    pool.add(instance);
                    basicTypes.BasicInt64V rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicInt64V.SubType, basicTypes.BasicInt64V> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicInt64V.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic Int8
    /// </summary>
    public sealed class P8 : BasePool<basicTypes.BasicInt8> {
        
            protected override basicTypes.BasicInt8[] newArray(int size) {
                return new basicTypes.BasicInt8[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P8(int poolIndex) : base(poolIndex, "basicint8", new string[] { "basicint" }, NoAutoFields) {

            }

            internal basicTypes.BasicInt8[] Data {
                get
                {
                    return (basicTypes.BasicInt8[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicInt8(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicint":
                    unchecked{new f9(I8.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicint":
                    return new f9((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicInt8 instance with default field values </returns>
            public override object make() {
                basicTypes.BasicInt8 rval = new basicTypes.BasicInt8();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicInt8 instance with the argument field values </returns>
            public basicTypes.BasicInt8 make(sbyte basicInt) {
                basicTypes.BasicInt8 rval = new basicTypes.BasicInt8(-1, basicInt);
                add(rval);
                return rval;
            }

            public BasicInt8Builder build() {
                return new BasicInt8Builder(this, new basicTypes.BasicInt8());
            }

            /// <summary>
            /// Builder for new BasicInt8 instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicInt8Builder : Builder<basicTypes.BasicInt8> {

                public BasicInt8Builder(AbstractStoragePool pool, basicTypes.BasicInt8 instance) : base(pool, instance) {

                }

                public BasicInt8Builder basicInt(sbyte basicInt) {
                    instance.basicInt = basicInt;
                    return this;
                }

                public override basicTypes.BasicInt8 make() {
                    pool.add(instance);
                    basicTypes.BasicInt8 rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicInt8.SubType, basicTypes.BasicInt8> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicInt8.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains all basic int types
    /// </summary>
    public sealed class P9 : BasePool<basicTypes.BasicIntegers> {
        
            protected override basicTypes.BasicIntegers[] newArray(int size) {
                return new basicTypes.BasicIntegers[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P9(int poolIndex) : base(poolIndex, "basicintegers", new string[] { "int16", "int32", "int64i", "int64v", "int8" }, NoAutoFields) {

            }

            internal basicTypes.BasicIntegers[] Data {
                get
                {
                    return (basicTypes.BasicIntegers[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicIntegers(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "int16":
                    unchecked{new f10((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicInt16s()), this);}
                    return;

                case "int32":
                    unchecked{new f11((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicInt32s()), this);}
                    return;

                case "int64i":
                    unchecked{new f12((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicInt64Is()), this);}
                    return;

                case "int64v":
                    unchecked{new f13((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicInt64Vs()), this);}
                    return;

                case "int8":
                    unchecked{new f14((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicInt8s()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "int16":
                    return new f10((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "int32":
                    return new f11((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "int64i":
                    return new f12((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "int64v":
                    return new f13((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "int8":
                    return new f14((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicIntegers instance with default field values </returns>
            public override object make() {
                basicTypes.BasicIntegers rval = new basicTypes.BasicIntegers();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicIntegers instance with the argument field values </returns>
            public basicTypes.BasicIntegers make(basicTypes.BasicInt16 int16, basicTypes.BasicInt32 int32, basicTypes.BasicInt64I int64I, basicTypes.BasicInt64V int64V, basicTypes.BasicInt8 int8) {
                basicTypes.BasicIntegers rval = new basicTypes.BasicIntegers(-1, int16, int32, int64I, int64V, int8);
                add(rval);
                return rval;
            }

            public BasicIntegersBuilder build() {
                return new BasicIntegersBuilder(this, new basicTypes.BasicIntegers());
            }

            /// <summary>
            /// Builder for new BasicIntegers instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicIntegersBuilder : Builder<basicTypes.BasicIntegers> {

                public BasicIntegersBuilder(AbstractStoragePool pool, basicTypes.BasicIntegers instance) : base(pool, instance) {

                }

                public BasicIntegersBuilder int16(basicTypes.BasicInt16 int16) {
                    instance.int16 = int16;
                    return this;
                }

                public BasicIntegersBuilder int32(basicTypes.BasicInt32 int32) {
                    instance.int32 = int32;
                    return this;
                }

                public BasicIntegersBuilder int64I(basicTypes.BasicInt64I int64I) {
                    instance.int64I = int64I;
                    return this;
                }

                public BasicIntegersBuilder int64V(basicTypes.BasicInt64V int64V) {
                    instance.int64V = int64V;
                    return this;
                }

                public BasicIntegersBuilder int8(basicTypes.BasicInt8 int8) {
                    instance.int8 = int8;
                    return this;
                }

                public override basicTypes.BasicIntegers make() {
                    pool.add(instance);
                    basicTypes.BasicIntegers rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicIntegers.SubType, basicTypes.BasicIntegers> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicIntegers.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Contains a basic String
    /// </summary>
    public sealed class P10 : BasePool<basicTypes.BasicString> {
        
            protected override basicTypes.BasicString[] newArray(int size) {
                return new basicTypes.BasicString[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P10(int poolIndex) : base(poolIndex, "basicstring", new string[] { "basicstring" }, NoAutoFields) {

            }

            internal basicTypes.BasicString[] Data {
                get
                {
                    return (basicTypes.BasicString[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicString(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "basicstring":
                    unchecked{new f15(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "basicstring":
                    return new f15((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicString instance with default field values </returns>
            public override object make() {
                basicTypes.BasicString rval = new basicTypes.BasicString();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicString instance with the argument field values </returns>
            public basicTypes.BasicString make(string basicString) {
                basicTypes.BasicString rval = new basicTypes.BasicString(-1, basicString);
                add(rval);
                return rval;
            }

            public BasicStringBuilder build() {
                return new BasicStringBuilder(this, new basicTypes.BasicString());
            }

            /// <summary>
            /// Builder for new BasicString instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicStringBuilder : Builder<basicTypes.BasicString> {

                public BasicStringBuilder(AbstractStoragePool pool, basicTypes.BasicString instance) : base(pool, instance) {

                }

                public BasicStringBuilder basicString(string basicString) {
                    instance.basicString = basicString;
                    return this;
                }

                public override basicTypes.BasicString make() {
                    pool.add(instance);
                    basicTypes.BasicString rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicString.SubType, basicTypes.BasicString> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicString.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Includes all basic types
    /// </summary>
    public sealed class P11 : BasePool<basicTypes.BasicTypes> {
        
            protected override basicTypes.BasicTypes[] newArray(int size) {
                return new basicTypes.BasicTypes[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P11(int poolIndex) : base(poolIndex, "basictypes", new string[] { "abool", "alist", "amap", "anannotation", "anarray", "anotherusertype", "aset", "astring", "ausertype" }, NoAutoFields) {

            }

            internal basicTypes.BasicTypes[] Data {
                get
                {
                    return (basicTypes.BasicTypes[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new basicTypes.BasicTypes(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "abool":
                    unchecked{new f16((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicBools()), this);}
                    return;

                case "alist":
                    unchecked{new f17(new ListType<float>(F32.get()), this);}
                    return;

                case "amap":
                    unchecked{new f18(new MapType<short, sbyte>(I16.get(), I8.get()), this);}
                    return;

                case "anannotation":
                    unchecked{new f19(annotation, this);}
                    return;

                case "anarray":
                    unchecked{new f20(new VariableLengthArray<basicTypes.BasicIntegers>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicIntegerss())), this);}
                    return;

                case "anotherusertype":
                    unchecked{new f21((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicFloatss()), this);}
                    return;

                case "aset":
                    unchecked{new f22(new SetType<sbyte>(I8.get()), this);}
                    return;

                case "astring":
                    unchecked{new f23((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicStrings()), this);}
                    return;

                case "ausertype":
                    unchecked{new f24((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).BasicIntegerss()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "abool":
                    return new f16((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "alist":
                    return new f17((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "amap":
                    return new f18((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "anannotation":
                    return new f19((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "anarray":
                    return new f20((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "anotherusertype":
                    return new f21((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "aset":
                    return new f22((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "astring":
                    return new f23((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "ausertype":
                    return new f24((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new BasicTypes instance with default field values </returns>
            public override object make() {
                basicTypes.BasicTypes rval = new basicTypes.BasicTypes();
                add(rval);
                return rval;
            }
        
            /// <returns> a new basicTypes.BasicTypes instance with the argument field values </returns>
            public basicTypes.BasicTypes make(basicTypes.BasicBool aBool, System.Collections.Generic.List<System.Single> aList, System.Collections.Generic.Dictionary<System.Int16, System.SByte> aMap, de.ust.skill.common.csharp.@internal.SkillObject anAnnotation, System.Collections.ArrayList anArray, basicTypes.BasicFloats anotherUserType, System.Collections.Generic.HashSet<System.SByte> aSet, basicTypes.BasicString aString, basicTypes.BasicIntegers aUserType) {
                basicTypes.BasicTypes rval = new basicTypes.BasicTypes(-1, aBool, aList, aMap, anAnnotation, anArray, anotherUserType, aSet, aString, aUserType);
                add(rval);
                return rval;
            }

            public BasicTypesBuilder build() {
                return new BasicTypesBuilder(this, new basicTypes.BasicTypes());
            }

            /// <summary>
            /// Builder for new BasicTypes instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BasicTypesBuilder : Builder<basicTypes.BasicTypes> {

                public BasicTypesBuilder(AbstractStoragePool pool, basicTypes.BasicTypes instance) : base(pool, instance) {

                }

                public BasicTypesBuilder aBool(basicTypes.BasicBool aBool) {
                    instance.aBool = aBool;
                    return this;
                }

                public BasicTypesBuilder aList(System.Collections.Generic.List<System.Single> aList) {
                    instance.aList = aList;
                    return this;
                }

                public BasicTypesBuilder aMap(System.Collections.Generic.Dictionary<System.Int16, System.SByte> aMap) {
                    instance.aMap = aMap;
                    return this;
                }

                public BasicTypesBuilder anAnnotation(de.ust.skill.common.csharp.@internal.SkillObject anAnnotation) {
                    instance.anAnnotation = anAnnotation;
                    return this;
                }

                public BasicTypesBuilder anArray(System.Collections.ArrayList anArray) {
                    instance.anArray = anArray;
                    return this;
                }

                public BasicTypesBuilder anotherUserType(basicTypes.BasicFloats anotherUserType) {
                    instance.anotherUserType = anotherUserType;
                    return this;
                }

                public BasicTypesBuilder aSet(System.Collections.Generic.HashSet<System.SByte> aSet) {
                    instance.aSet = aSet;
                    return this;
                }

                public BasicTypesBuilder aString(basicTypes.BasicString aString) {
                    instance.aString = aString;
                    return this;
                }

                public BasicTypesBuilder aUserType(basicTypes.BasicIntegers aUserType) {
                    instance.aUserType = aUserType;
                    return this;
                }

                public override basicTypes.BasicTypes make() {
                    pool.add(instance);
                    basicTypes.BasicTypes rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<basicTypes.BasicTypes.SubType, basicTypes.BasicTypes> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new basicTypes.BasicTypes.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// bool BasicBool.basicBool
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Boolean, basicTypes.BasicBool> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "basicbool", owner) {
                
                if (type.TypeID != 6)
                    throw new SkillException("Expected field type bool in BasicBool.basicBool but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicBool[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].basicBool = @in.@bool();
                }

            }
            public override void osc(int i, int h) {offset += (h-i);
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicBool[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.@bool(d[i].basicBool);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicBool) @ref).basicBool;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicBool) @ref).basicBool = (System.Boolean)value;
            }
        }

        /// <summary>
        /// f32 BasicFloat32.basicFloat
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Single, basicTypes.BasicFloat32> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "basicfloat", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in BasicFloat32.basicFloat but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicFloat32[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].basicFloat = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicFloat32[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].basicFloat);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicFloat32) @ref).basicFloat;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicFloat32) @ref).basicFloat = (System.Single)value;
            }
        }

        /// <summary>
        /// f64 BasicFloat64.basicFloat
        /// </summary>
        internal sealed class f2 : KnownDataField<System.Double, basicTypes.BasicFloat64> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "basicfloat", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in BasicFloat64.basicFloat but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicFloat64[] d = ((P2) owner).Data;
                for (; i != h; i++) {
            d[i].basicFloat = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicFloat64[] d = ((P2) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].basicFloat);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicFloat64) @ref).basicFloat;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicFloat64) @ref).basicFloat = (System.Double)value;
            }
        }

        /// <summary>
        /// basicfloat32 BasicFloats.float32
        /// </summary>
        internal sealed class f3 : KnownDataField<basicTypes.BasicFloat32, basicTypes.BasicFloats> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "float32", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicfloat32"))
                    throw new SkillException("Expected field type basicfloat32 in BasicFloats.float32 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicFloats[] d = ((P3) owner).Data;
                P1 t = ((P1)(object)this.type);
                for (; i != h; i++) {
            d[i].float32 = (basicTypes.BasicFloat32)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicFloats[] d = ((P3) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicFloat32 instance = d[i].float32;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicFloats[] d = ((P3) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicFloat32 v = d[i].float32;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicFloats) @ref).float32;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicFloats) @ref).float32 = (basicTypes.BasicFloat32)value;
            }
        }

        /// <summary>
        /// basicfloat64 BasicFloats.float64
        /// </summary>
        internal sealed class f4 : KnownDataField<basicTypes.BasicFloat64, basicTypes.BasicFloats> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "float64", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicfloat64"))
                    throw new SkillException("Expected field type basicfloat64 in BasicFloats.float64 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicFloats[] d = ((P3) owner).Data;
                P2 t = ((P2)(object)this.type);
                for (; i != h; i++) {
            d[i].float64 = (basicTypes.BasicFloat64)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicFloats[] d = ((P3) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicFloat64 instance = d[i].float64;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicFloats[] d = ((P3) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicFloat64 v = d[i].float64;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicFloats) @ref).float64;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicFloats) @ref).float64 = (basicTypes.BasicFloat64)value;
            }
        }

        /// <summary>
        /// i16 BasicInt16.basicInt
        /// </summary>
        internal sealed class f5 : KnownDataField<System.Int16, basicTypes.BasicInt16> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "basicint", owner) {
                
                if (type.TypeID != 8)
                    throw new SkillException("Expected field type i16 in BasicInt16.basicInt but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicInt16[] d = ((P4) owner).Data;
                for (; i != h; i++) {
            d[i].basicInt = @in.i16();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 1;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicInt16[] d = ((P4) owner).Data;
                for (; i != h; i++) {
                    @out.i16(d[i].basicInt);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicInt16) @ref).basicInt;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicInt16) @ref).basicInt = (System.Int16)value;
            }
        }

        /// <summary>
        /// i32 BasicInt32.basicInt
        /// </summary>
        internal sealed class f6 : KnownDataField<System.Int32, basicTypes.BasicInt32> {

            public f6(de.ust.skill.common.csharp.@internal.FieldType type, P5 owner) : base(type, "basicint", owner) {
                
                if (type.TypeID != 9)
                    throw new SkillException("Expected field type i32 in BasicInt32.basicInt but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicInt32[] d = ((P5) owner).Data;
                for (; i != h; i++) {
            d[i].basicInt = @in.i32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicInt32[] d = ((P5) owner).Data;
                for (; i != h; i++) {
                    @out.i32(d[i].basicInt);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicInt32) @ref).basicInt;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicInt32) @ref).basicInt = (System.Int32)value;
            }
        }

        /// <summary>
        /// i64 BasicInt64I.basicInt
        /// </summary>
        internal sealed class f7 : KnownDataField<System.Int64, basicTypes.BasicInt64I> {

            public f7(de.ust.skill.common.csharp.@internal.FieldType type, P6 owner) : base(type, "basicint", owner) {
                
                if (type.TypeID != 10)
                    throw new SkillException("Expected field type i64 in BasicInt64I.basicInt but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicInt64I[] d = ((P6) owner).Data;
                for (; i != h; i++) {
            d[i].basicInt = @in.i64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicInt64I[] d = ((P6) owner).Data;
                for (; i != h; i++) {
                    @out.i64(d[i].basicInt);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicInt64I) @ref).basicInt;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicInt64I) @ref).basicInt = (System.Int64)value;
            }
        }

        /// <summary>
        /// v64 BasicInt64V.basicInt
        /// </summary>
        internal sealed class f8 : KnownDataField<System.Int64, basicTypes.BasicInt64V> {

            public f8(de.ust.skill.common.csharp.@internal.FieldType type, P7 owner) : base(type, "basicint", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in BasicInt64V.basicInt but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicInt64V[] d = ((P7) owner).Data;
                for (; i != h; i++) {
            d[i].basicInt = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicInt64V[] d = ((P7) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].basicInt);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicInt64V[] d = ((P7) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].basicInt);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicInt64V) @ref).basicInt;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicInt64V) @ref).basicInt = (System.Int64)value;
            }
        }

        /// <summary>
        /// i8 BasicInt8.basicInt
        /// </summary>
        internal sealed class f9 : KnownDataField<System.SByte, basicTypes.BasicInt8> {

            public f9(de.ust.skill.common.csharp.@internal.FieldType type, P8 owner) : base(type, "basicint", owner) {
                
                if (type.TypeID != 7)
                    throw new SkillException("Expected field type i8 in BasicInt8.basicInt but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicInt8[] d = ((P8) owner).Data;
                for (; i != h; i++) {
            d[i].basicInt = @in.i8();
                }

            }
            public override void osc(int i, int h) {offset += (h-i);
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicInt8[] d = ((P8) owner).Data;
                for (; i != h; i++) {
                    @out.i8(d[i].basicInt);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicInt8) @ref).basicInt;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicInt8) @ref).basicInt = (System.SByte)value;
            }
        }

        /// <summary>
        /// basicint16 BasicIntegers.int16
        /// </summary>
        internal sealed class f10 : KnownDataField<basicTypes.BasicInt16, basicTypes.BasicIntegers> {

            public f10(de.ust.skill.common.csharp.@internal.FieldType type, P9 owner) : base(type, "int16", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicint16"))
                    throw new SkillException("Expected field type basicint16 in BasicIntegers.int16 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                P4 t = ((P4)(object)this.type);
                for (; i != h; i++) {
            d[i].int16 = (basicTypes.BasicInt16)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicIntegers[] d = ((P9) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicInt16 instance = d[i].int16;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicInt16 v = d[i].int16;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicIntegers) @ref).int16;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicIntegers) @ref).int16 = (basicTypes.BasicInt16)value;
            }
        }

        /// <summary>
        /// basicint32 BasicIntegers.int32
        /// </summary>
        internal sealed class f11 : KnownDataField<basicTypes.BasicInt32, basicTypes.BasicIntegers> {

            public f11(de.ust.skill.common.csharp.@internal.FieldType type, P9 owner) : base(type, "int32", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicint32"))
                    throw new SkillException("Expected field type basicint32 in BasicIntegers.int32 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                P5 t = ((P5)(object)this.type);
                for (; i != h; i++) {
            d[i].int32 = (basicTypes.BasicInt32)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicIntegers[] d = ((P9) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicInt32 instance = d[i].int32;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicInt32 v = d[i].int32;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicIntegers) @ref).int32;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicIntegers) @ref).int32 = (basicTypes.BasicInt32)value;
            }
        }

        /// <summary>
        /// basicint64i BasicIntegers.int64I
        /// </summary>
        internal sealed class f12 : KnownDataField<basicTypes.BasicInt64I, basicTypes.BasicIntegers> {

            public f12(de.ust.skill.common.csharp.@internal.FieldType type, P9 owner) : base(type, "int64i", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicint64i"))
                    throw new SkillException("Expected field type basicint64i in BasicIntegers.int64I but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                P6 t = ((P6)(object)this.type);
                for (; i != h; i++) {
            d[i].int64I = (basicTypes.BasicInt64I)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicIntegers[] d = ((P9) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicInt64I instance = d[i].int64I;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicInt64I v = d[i].int64I;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicIntegers) @ref).int64I;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicIntegers) @ref).int64I = (basicTypes.BasicInt64I)value;
            }
        }

        /// <summary>
        /// basicint64v BasicIntegers.int64V
        /// </summary>
        internal sealed class f13 : KnownDataField<basicTypes.BasicInt64V, basicTypes.BasicIntegers> {

            public f13(de.ust.skill.common.csharp.@internal.FieldType type, P9 owner) : base(type, "int64v", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicint64v"))
                    throw new SkillException("Expected field type basicint64v in BasicIntegers.int64V but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                P7 t = ((P7)(object)this.type);
                for (; i != h; i++) {
            d[i].int64V = (basicTypes.BasicInt64V)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicIntegers[] d = ((P9) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicInt64V instance = d[i].int64V;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicInt64V v = d[i].int64V;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicIntegers) @ref).int64V;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicIntegers) @ref).int64V = (basicTypes.BasicInt64V)value;
            }
        }

        /// <summary>
        /// basicint8 BasicIntegers.int8
        /// </summary>
        internal sealed class f14 : KnownDataField<basicTypes.BasicInt8, basicTypes.BasicIntegers> {

            public f14(de.ust.skill.common.csharp.@internal.FieldType type, P9 owner) : base(type, "int8", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicint8"))
                    throw new SkillException("Expected field type basicint8 in BasicIntegers.int8 but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                P8 t = ((P8)(object)this.type);
                for (; i != h; i++) {
            d[i].int8 = (basicTypes.BasicInt8)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicIntegers[] d = ((P9) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicInt8 instance = d[i].int8;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicIntegers[] d = ((P9) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicInt8 v = d[i].int8;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicIntegers) @ref).int8;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicIntegers) @ref).int8 = (basicTypes.BasicInt8)value;
            }
        }

        /// <summary>
        /// string BasicString.basicString
        /// </summary>
        internal sealed class f15 : KnownDataField<System.String, basicTypes.BasicString> {

            public f15(de.ust.skill.common.csharp.@internal.FieldType type, P10 owner) : base(type, "basicstring", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in BasicString.basicString but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicString[] d = ((P10) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].basicString = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                basicTypes.BasicString[] d = ((P10) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].basicString;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicString[] d = ((P10) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].basicString, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicString) @ref).basicString;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicString) @ref).basicString = (System.String)value;
            }
        }

        /// <summary>
        /// basicbool BasicTypes.aBool
        /// </summary>
        internal sealed class f16 : KnownDataField<basicTypes.BasicBool, basicTypes.BasicTypes> {

            public f16(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "abool", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicbool"))
                    throw new SkillException("Expected field type basicbool in BasicTypes.aBool but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                P0 t = ((P0)(object)this.type);
                for (; i != h; i++) {
            d[i].aBool = (basicTypes.BasicBool)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicBool instance = d[i].aBool;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicBool v = d[i].aBool;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).aBool;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).aBool = (basicTypes.BasicBool)value;
            }
        }

        /// <summary>
        /// list<f32> BasicTypes.aList
        /// </summary>
        internal sealed class f17 : KnownDataField<System.Collections.Generic.List<System.Single>, basicTypes.BasicTypes> {

            public f17(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "alist", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type list<f32> in BasicTypes.aList but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                ListType<System.Single> type = (ListType<System.Single>) this.type.cast<System.Single, System.Object>();
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.List<System.Single> v = new List<float>();
            while (size-- > 0) {
                v.Add(@in.f32());
            }
            d[i].aList = v;
                }

            }
            public override void osc(int i, int h) {
                ListType<System.Single> type = (ListType<System.Single>) this.type.cast<System.Single, System.Object>();
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.List<System.Single> v = null == d[i].aList ? null : ((System.Collections.Generic.List<System.Single>)d[i].aList).Cast<float>().ToList();

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        result += (size<<2);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                ListType<System.Single> type = (ListType<System.Single>) this.type.cast<System.Single, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.Generic.List<System.Single> x = d[i].aList;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (float e in x){
                @out.f32(e);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).aList;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).aList = ((System.Collections.Generic.List<object>)value).Cast<float>().ToList();
            }
        }

        /// <summary>
        /// map<i16,i8> BasicTypes.aMap
        /// </summary>
        internal sealed class f18 : KnownDataField<System.Collections.Generic.Dictionary<System.Int16, System.SByte>, basicTypes.BasicTypes> {

            public f18(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "amap", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type map<i16,i8> in BasicTypes.aMap but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                MapType<System.Int16, System.SByte> type = (MapType<System.Int16, System.SByte>) this.type.cast<System.Int16, System.SByte>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
            d[i].aMap = castMap<short, sbyte, System.Object, System.Object>((Dictionary<System.Object, System.Object>)((de.ust.skill.common.csharp.@internal.FieldType)this.type).readSingleField(@in));
                }

            }
            public override void osc(int i, int h) {
                MapType<System.Int16, System.SByte> type = (MapType<System.Int16, System.SByte>) this.type.cast<System.Int16, System.SByte>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.Dictionary<System.Int16, System.SByte> v = castMap<short, sbyte, short, sbyte>(d[i].aMap);
                    if(null==v || v.Count == 0)
                        result++;
                    else {

                        short[] keysArray = new short[v.Keys.Count];
                        v.Keys.CopyTo(keysArray, 0);
                        ICollection keysList = new List<object>();
                        foreach (object key in keysArray)
                        {
                            ((List<object>)keysList).Add(key);
                        }

                        sbyte[] valuesArray = new sbyte[v.Values.Count];
                        v.Values.CopyTo(valuesArray, 0);
                        ICollection valuesList = new List<object>();
                        foreach (object value in valuesArray)
                        {
                            ((List<object>)valuesList).Add(value);
                        }

                        result += V64.singleV64Offset(v.Count);
                        result += ((de.ust.skill.common.csharp.@internal.FieldType)keyType).calculateOffset(keysList);
                        result += ((de.ust.skill.common.csharp.@internal.FieldType)valueType).calculateOffset(valuesList);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                MapType<System.Int16, System.SByte> type = (MapType<System.Int16, System.SByte>) this.type.cast<System.Int16, System.SByte>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
                    ((de.ust.skill.common.csharp.@internal.FieldType)this.type).writeSingleField(d[i].aMap, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).aMap;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).aMap = castMap<short, sbyte, object, object>((System.Collections.Generic.Dictionary<object, object>)value);
            }
        }

        /// <summary>
        /// annotation BasicTypes.anAnnotation
        /// </summary>
        internal sealed class f19 : KnownDataField<de.ust.skill.common.csharp.@internal.SkillObject, basicTypes.BasicTypes> {

            public f19(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "anannotation", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in BasicTypes.anAnnotation but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
            d[i].anAnnotation = (de.ust.skill.common.csharp.@internal.SkillObject)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t = (Annotation)this.type;
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    de.ust.skill.common.csharp.@internal.SkillObject v = d[i].anAnnotation;
                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].anAnnotation, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).anAnnotation;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).anAnnotation = (de.ust.skill.common.csharp.@internal.SkillObject)value;
            }
        }

        /// <summary>
        /// basicintegers[] BasicTypes.anArray
        /// </summary>
        internal sealed class f20 : KnownDataField<System.Collections.ArrayList, basicTypes.BasicTypes> {

            public f20(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "anarray", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type basicintegers[] in BasicTypes.anArray but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                VariableLengthArray<basicTypes.BasicIntegers> type = (VariableLengthArray<basicTypes.BasicIntegers>) this.type.cast<basicTypes.BasicIntegers, System.Object>();
                P9 t = ((P9)(object)type.groundType);
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.ArrayList v = new ArrayList(size);
            while (size-- > 0) {
                v.Add((basicTypes.BasicIntegers)t.getByID(@in.v32()));
            }
            d[i].anArray = v;
                }

            }
            public override void osc(int i, int h) {
                VariableLengthArray<basicTypes.BasicIntegers> type = (VariableLengthArray<basicTypes.BasicIntegers>) this.type.cast<basicTypes.BasicIntegers, System.Object>();
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.ArrayList v = null == d[i].anArray ? null : (System.Collections.ArrayList)d[i].anArray;

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(basicTypes.BasicIntegers x in v)
                    result += null==x?1:V64.singleV64Offset(x.SkillID);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                VariableLengthArray<basicTypes.BasicIntegers> type = (VariableLengthArray<basicTypes.BasicIntegers>) this.type.cast<basicTypes.BasicIntegers, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.ArrayList x = d[i].anArray;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (basicTypes.BasicIntegers e in x){
                basicTypes.BasicIntegers v = e;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).anArray;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).anArray = (System.Collections.ArrayList)value;
            }
        }

        /// <summary>
        /// basicfloats BasicTypes.anotherUserType
        /// </summary>
        internal sealed class f21 : KnownDataField<basicTypes.BasicFloats, basicTypes.BasicTypes> {

            public f21(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "anotherusertype", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicfloats"))
                    throw new SkillException("Expected field type basicfloats in BasicTypes.anotherUserType but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                P3 t = ((P3)(object)this.type);
                for (; i != h; i++) {
            d[i].anotherUserType = (basicTypes.BasicFloats)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicFloats instance = d[i].anotherUserType;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicFloats v = d[i].anotherUserType;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).anotherUserType;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).anotherUserType = (basicTypes.BasicFloats)value;
            }
        }

        /// <summary>
        /// set<i8> BasicTypes.aSet
        /// </summary>
        internal sealed class f22 : KnownDataField<System.Collections.Generic.HashSet<System.SByte>, basicTypes.BasicTypes> {

            public f22(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "aset", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type set<i8> in BasicTypes.aSet but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                SetType<System.SByte> type = (SetType<System.SByte>) this.type.cast<System.SByte, System.Object>();
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.HashSet<System.SByte> v = new HashSet<sbyte>();
            while (size-- > 0) {
                v.Add(@in.i8());
            }
            d[i].aSet = v;
                }

            }
            public override void osc(int i, int h) {
                SetType<System.SByte> type = (SetType<System.SByte>) this.type.cast<System.SByte, System.Object>();
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.HashSet<System.SByte> v = null == d[i].aSet ? null : new System.Collections.Generic.HashSet<System.SByte>(((System.Collections.Generic.HashSet<System.SByte>)d[i].aSet).Cast<sbyte>());

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        result += size;
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                SetType<System.SByte> type = (SetType<System.SByte>) this.type.cast<System.SByte, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.Generic.HashSet<System.SByte> x = d[i].aSet;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (sbyte e in x){
                @out.i8(e);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).aSet;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).aSet = new System.Collections.Generic.HashSet<sbyte>(((System.Collections.Generic.HashSet<object>)value).Cast<sbyte>());
            }
        }

        /// <summary>
        /// basicstring BasicTypes.aString
        /// </summary>
        internal sealed class f23 : KnownDataField<basicTypes.BasicString, basicTypes.BasicTypes> {

            public f23(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "astring", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicstring"))
                    throw new SkillException("Expected field type basicstring in BasicTypes.aString but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                P10 t = ((P10)(object)this.type);
                for (; i != h; i++) {
            d[i].aString = (basicTypes.BasicString)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicString instance = d[i].aString;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicString v = d[i].aString;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).aString;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).aString = (basicTypes.BasicString)value;
            }
        }

        /// <summary>
        /// basicintegers BasicTypes.aUserType
        /// </summary>
        internal sealed class f24 : KnownDataField<basicTypes.BasicIntegers, basicTypes.BasicTypes> {

            public f24(de.ust.skill.common.csharp.@internal.FieldType type, P11 owner) : base(type, "ausertype", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("basicintegers"))
                    throw new SkillException("Expected field type basicintegers in BasicTypes.aUserType but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                P9 t = ((P9)(object)this.type);
                for (; i != h; i++) {
            d[i].aUserType = (basicTypes.BasicIntegers)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                basicTypes.BasicTypes[] d = ((P11) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    basicTypes.BasicIntegers instance = d[i].aUserType;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                basicTypes.BasicTypes[] d = ((P11) owner).Data;
                for (; i != h; i++) {
                    basicTypes.BasicIntegers v = d[i].aUserType;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((basicTypes.BasicTypes) @ref).aUserType;
            }

            public override void set(SkillObject @ref, object value) {
                ((basicTypes.BasicTypes) @ref).aUserType = (basicTypes.BasicIntegers)value;
            }
        }

    }
}
