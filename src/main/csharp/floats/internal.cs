/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = floats.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace floats
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
                    poolByName.TryGetValue("doubletest", out p);
                    DoubleTestsField = (null == p) ? (P0)Parser.newPool("doubletest", null, types) : (P0) p;
                    poolByName.TryGetValue("floattest", out p);
                    FloatTestsField = (null == p) ? (P1)Parser.newPool("floattest", null, types) : (P1) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 DoubleTestsField;

            public override P0 DoubleTests() {
                return DoubleTestsField;
            }
        
            internal readonly P1 FloatTestsField;

            public override P1 FloatTests() {
                return FloatTestsField;
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
                        case "doubletest":
                            return (superPool = new P0(types.Count));
        

                        case "floattest":
                            return (superPool = new P1(types.Count));
        
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
    ///  check some double values.
    /// </summary>
    public sealed class P0 : BasePool<floats.DoubleTest> {
        
            protected override floats.DoubleTest[] newArray(int size) {
                return new floats.DoubleTest[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "doubletest", new string[] { "minuszero", "nan", "pi", "two", "zero" }, NoAutoFields) {

            }

            internal floats.DoubleTest[] Data {
                get
                {
                    return (floats.DoubleTest[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new floats.DoubleTest(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "minuszero":
                    unchecked{new f0(F64.get(), this);}
                    return;

                case "nan":
                    unchecked{new f1(F64.get(), this);}
                    return;

                case "pi":
                    unchecked{new f2(F64.get(), this);}
                    return;

                case "two":
                    unchecked{new f3(F64.get(), this);}
                    return;

                case "zero":
                    unchecked{new f4(F64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "minuszero":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "nan":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "pi":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "two":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "zero":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new DoubleTest instance with default field values </returns>
            public override object make() {
                floats.DoubleTest rval = new floats.DoubleTest();
                add(rval);
                return rval;
            }
        
            /// <returns> a new floats.DoubleTest instance with the argument field values </returns>
            public floats.DoubleTest make(double minusZZero, double NaN, double pi, double two, double zero) {
                floats.DoubleTest rval = new floats.DoubleTest(-1, minusZZero, NaN, pi, two, zero);
                add(rval);
                return rval;
            }

            public DoubleTestBuilder build() {
                return new DoubleTestBuilder(this, new floats.DoubleTest());
            }

            /// <summary>
            /// Builder for new DoubleTest instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class DoubleTestBuilder : Builder<floats.DoubleTest> {

                public DoubleTestBuilder(AbstractStoragePool pool, floats.DoubleTest instance) : base(pool, instance) {

                }

                public DoubleTestBuilder minusZZero(double minusZZero) {
                    instance.minusZZero = minusZZero;
                    return this;
                }

                public DoubleTestBuilder NaN(double NaN) {
                    instance.NaN = NaN;
                    return this;
                }

                public DoubleTestBuilder pi(double pi) {
                    instance.pi = pi;
                    return this;
                }

                public DoubleTestBuilder two(double two) {
                    instance.two = two;
                    return this;
                }

                public DoubleTestBuilder zero(double zero) {
                    instance.zero = zero;
                    return this;
                }

                public override floats.DoubleTest make() {
                    pool.add(instance);
                    floats.DoubleTest rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<floats.DoubleTest.SubType, floats.DoubleTest> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new floats.DoubleTest.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  check some float values.
    /// </summary>
    public sealed class P1 : BasePool<floats.FloatTest> {
        
            protected override floats.FloatTest[] newArray(int size) {
                return new floats.FloatTest[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "floattest", new string[] { "minuszero", "nan", "pi", "two", "zero" }, NoAutoFields) {

            }

            internal floats.FloatTest[] Data {
                get
                {
                    return (floats.FloatTest[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new floats.FloatTest(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "minuszero":
                    unchecked{new f5(F32.get(), this);}
                    return;

                case "nan":
                    unchecked{new f6(F32.get(), this);}
                    return;

                case "pi":
                    unchecked{new f7(F32.get(), this);}
                    return;

                case "two":
                    unchecked{new f8(F32.get(), this);}
                    return;

                case "zero":
                    unchecked{new f9(F32.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "minuszero":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "nan":
                    return new f6((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "pi":
                    return new f7((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "two":
                    return new f8((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "zero":
                    return new f9((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new FloatTest instance with default field values </returns>
            public override object make() {
                floats.FloatTest rval = new floats.FloatTest();
                add(rval);
                return rval;
            }
        
            /// <returns> a new floats.FloatTest instance with the argument field values </returns>
            public floats.FloatTest make(float minusZZero, float NaN, float pi, float two, float zero) {
                floats.FloatTest rval = new floats.FloatTest(-1, minusZZero, NaN, pi, two, zero);
                add(rval);
                return rval;
            }

            public FloatTestBuilder build() {
                return new FloatTestBuilder(this, new floats.FloatTest());
            }

            /// <summary>
            /// Builder for new FloatTest instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class FloatTestBuilder : Builder<floats.FloatTest> {

                public FloatTestBuilder(AbstractStoragePool pool, floats.FloatTest instance) : base(pool, instance) {

                }

                public FloatTestBuilder minusZZero(float minusZZero) {
                    instance.minusZZero = minusZZero;
                    return this;
                }

                public FloatTestBuilder NaN(float NaN) {
                    instance.NaN = NaN;
                    return this;
                }

                public FloatTestBuilder pi(float pi) {
                    instance.pi = pi;
                    return this;
                }

                public FloatTestBuilder two(float two) {
                    instance.two = two;
                    return this;
                }

                public FloatTestBuilder zero(float zero) {
                    instance.zero = zero;
                    return this;
                }

                public override floats.FloatTest make() {
                    pool.add(instance);
                    floats.FloatTest rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<floats.FloatTest.SubType, floats.FloatTest> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new floats.FloatTest.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// f64 DoubleTest.minusZero
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Double, floats.DoubleTest> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "minuszero", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in DoubleTest.minusZero but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].minusZZero = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].minusZZero);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.DoubleTest) @ref).minusZZero;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.DoubleTest) @ref).minusZZero = (System.Double)value;
            }
        }

        /// <summary>
        /// f64 DoubleTest.NaN
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Double, floats.DoubleTest> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "nan", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in DoubleTest.NaN but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].NaN = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].NaN);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.DoubleTest) @ref).NaN;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.DoubleTest) @ref).NaN = (System.Double)value;
            }
        }

        /// <summary>
        /// f64 DoubleTest.pi
        /// </summary>
        internal sealed class f2 : KnownDataField<System.Double, floats.DoubleTest> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "pi", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in DoubleTest.pi but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].pi = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].pi);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.DoubleTest) @ref).pi;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.DoubleTest) @ref).pi = (System.Double)value;
            }
        }

        /// <summary>
        /// f64 DoubleTest.two
        /// </summary>
        internal sealed class f3 : KnownDataField<System.Double, floats.DoubleTest> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "two", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in DoubleTest.two but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].two = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].two);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.DoubleTest) @ref).two;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.DoubleTest) @ref).two = (System.Double)value;
            }
        }

        /// <summary>
        /// f64 DoubleTest.zero
        /// </summary>
        internal sealed class f4 : KnownDataField<System.Double, floats.DoubleTest> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "zero", owner) {
                
                if (type.TypeID != 13)
                    throw new SkillException("Expected field type f64 in DoubleTest.zero but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].zero = @in.f64();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 3;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.DoubleTest[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.f64(d[i].zero);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.DoubleTest) @ref).zero;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.DoubleTest) @ref).zero = (System.Double)value;
            }
        }

        /// <summary>
        /// f32 FloatTest.minusZero
        /// </summary>
        internal sealed class f5 : KnownDataField<System.Single, floats.FloatTest> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "minuszero", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in FloatTest.minusZero but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].minusZZero = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].minusZZero);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.FloatTest) @ref).minusZZero;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.FloatTest) @ref).minusZZero = (System.Single)value;
            }
        }

        /// <summary>
        /// f32 FloatTest.NaN
        /// </summary>
        internal sealed class f6 : KnownDataField<System.Single, floats.FloatTest> {

            public f6(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "nan", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in FloatTest.NaN but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].NaN = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].NaN);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.FloatTest) @ref).NaN;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.FloatTest) @ref).NaN = (System.Single)value;
            }
        }

        /// <summary>
        /// f32 FloatTest.pi
        /// </summary>
        internal sealed class f7 : KnownDataField<System.Single, floats.FloatTest> {

            public f7(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "pi", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in FloatTest.pi but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].pi = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].pi);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.FloatTest) @ref).pi;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.FloatTest) @ref).pi = (System.Single)value;
            }
        }

        /// <summary>
        /// f32 FloatTest.two
        /// </summary>
        internal sealed class f8 : KnownDataField<System.Single, floats.FloatTest> {

            public f8(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "two", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in FloatTest.two but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].two = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].two);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.FloatTest) @ref).two;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.FloatTest) @ref).two = (System.Single)value;
            }
        }

        /// <summary>
        /// f32 FloatTest.zero
        /// </summary>
        internal sealed class f9 : KnownDataField<System.Single, floats.FloatTest> {

            public f9(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "zero", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in FloatTest.zero but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
            d[i].zero = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                floats.FloatTest[] d = ((P1) owner).Data;
                for (; i != h; i++) {
                    @out.f32(d[i].zero);
                }

            }


            public override object get(SkillObject @ref) {
                return ((floats.FloatTest) @ref).zero;
            }

            public override void set(SkillObject @ref, object value) {
                ((floats.FloatTest) @ref).zero = (System.Single)value;
            }
        }

    }
}
