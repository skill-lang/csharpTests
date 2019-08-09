/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = annotation.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace annotation
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
                    poolByName.TryGetValue("date", out p);
                    DatesField = (null == p) ? (P0)Parser.newPool("date", null, types) : (P0) p;
                    poolByName.TryGetValue("test", out p);
                    TestsField = (null == p) ? (P1)Parser.newPool("test", null, types) : (P1) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 DatesField;

            public override P0 Dates() {
                return DatesField;
            }
        
            internal readonly P1 TestsField;

            public override P1 Tests() {
                return TestsField;
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
                        case "date":
                            return (superPool = new P0(types.Count));
        

                        case "test":
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
    ///  A simple date test with known Translation
    /// </summary>
    public sealed class P0 : BasePool<annotation.Date> {
        
            protected override annotation.Date[] newArray(int size) {
                return new annotation.Date[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "date", new string[] { "date" }, NoAutoFields) {

            }

            internal annotation.Date[] Data {
                get
                {
                    return (annotation.Date[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new annotation.Date(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "date":
                    unchecked{new f0(V64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "date":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Date instance with default field values </returns>
            public override object make() {
                annotation.Date rval = new annotation.Date();
                add(rval);
                return rval;
            }
        
            /// <returns> a new annotation.Date instance with the argument field values </returns>
            public annotation.Date make(long date) {
                annotation.Date rval = new annotation.Date(-1, date);
                add(rval);
                return rval;
            }

            public DateBuilder build() {
                return new DateBuilder(this, new annotation.Date());
            }

            /// <summary>
            /// Builder for new Date instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class DateBuilder : Builder<annotation.Date> {

                public DateBuilder(AbstractStoragePool pool, annotation.Date instance) : base(pool, instance) {

                }

                public DateBuilder date(long date) {
                    instance.date = date;
                    return this;
                }

                public override annotation.Date make() {
                    pool.add(instance);
                    annotation.Date rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<annotation.Date.SubType, annotation.Date> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new annotation.Date.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Test the implementation of annotations.
    /// </summary>
    public sealed class P1 : BasePool<annotation.Test> {
        
            protected override annotation.Test[] newArray(int size) {
                return new annotation.Test[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "test", new string[] { "f" }, NoAutoFields) {

            }

            internal annotation.Test[] Data {
                get
                {
                    return (annotation.Test[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new annotation.Test(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "f":
                    unchecked{new f1(annotation, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "f":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Test instance with default field values </returns>
            public override object make() {
                annotation.Test rval = new annotation.Test();
                add(rval);
                return rval;
            }
        
            /// <returns> a new annotation.Test instance with the argument field values </returns>
            public annotation.Test make(de.ust.skill.common.csharp.@internal.SkillObject f) {
                annotation.Test rval = new annotation.Test(-1, f);
                add(rval);
                return rval;
            }

            public TestBuilder build() {
                return new TestBuilder(this, new annotation.Test());
            }

            /// <summary>
            /// Builder for new Test instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class TestBuilder : Builder<annotation.Test> {

                public TestBuilder(AbstractStoragePool pool, annotation.Test instance) : base(pool, instance) {

                }

                public TestBuilder f(de.ust.skill.common.csharp.@internal.SkillObject f) {
                    instance.f = f;
                    return this;
                }

                public override annotation.Test make() {
                    pool.add(instance);
                    annotation.Test rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<annotation.Test.SubType, annotation.Test> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new annotation.Test.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// v64 Date.date
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Int64, annotation.Date> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "date", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in Date.date but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                annotation.Date[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].date = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                annotation.Date[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].date);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                annotation.Date[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].date);
                }

            }


            public override object get(SkillObject @ref) {
                return ((annotation.Date) @ref).date;
            }

            public override void set(SkillObject @ref, object value) {
                ((annotation.Date) @ref).date = (System.Int64)value;
            }
        }

        /// <summary>
        /// annotation Test.f
        /// </summary>
        internal sealed class f1 : KnownDataField<de.ust.skill.common.csharp.@internal.SkillObject, annotation.Test> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "f", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in Test.f but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                annotation.Test[] d = ((P1) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
            d[i].f = (de.ust.skill.common.csharp.@internal.SkillObject)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t = (Annotation)this.type;
                annotation.Test[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    de.ust.skill.common.csharp.@internal.SkillObject v = d[i].f;
                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                annotation.Test[] d = ((P1) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].f, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((annotation.Test) @ref).f;
            }

            public override void set(SkillObject @ref, object value) {
                ((annotation.Test) @ref).f = (de.ust.skill.common.csharp.@internal.SkillObject)value;
            }
        }

    }
}
