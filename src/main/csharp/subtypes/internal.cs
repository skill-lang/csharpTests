/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = subtypes.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace subtypes
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
                    poolByName.TryGetValue("a", out p);
                    AsField = (null == p) ? (P0)Parser.newPool("a", null, types) : (P0) p;
                    poolByName.TryGetValue("b", out p);
                    BsField = (null == p) ? (P1)Parser.newPool("b", AsField, types) : (P1) p;
                    poolByName.TryGetValue("d", out p);
                    DsField = (null == p) ? (P2)Parser.newPool("d", BsField, types) : (P2) p;
                    poolByName.TryGetValue("c", out p);
                    CsField = (null == p) ? (P3)Parser.newPool("c", AsField, types) : (P3) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 AsField;

            public override P0 As() {
                return AsField;
            }
        
            internal readonly P1 BsField;

            public override P1 Bs() {
                return BsField;
            }
        
            internal readonly P2 DsField;

            public override P2 Ds() {
                return DsField;
            }
        
            internal readonly P3 CsField;

            public override P3 Cs() {
                return CsField;
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
                        case "a":
                            return (superPool = new P0(types.Count));
        

                        case "b":
                            return (superPool = new P1(types.Count, (P0)superPool));


                        case "d":
                            return (superPool = new P2(types.Count, (P1)superPool));


                        case "c":
                            return (superPool = new P3(types.Count, (P0)superPool));

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

        public sealed class P0 : BasePool<subtypes.A> {
        
            protected override subtypes.A[] newArray(int size) {
                return new subtypes.A[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "a", new string[] { "a" }, NoAutoFields) {

            }

            internal subtypes.A[] Data {
                get
                {
                    return (subtypes.A[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new subtypes.A(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "a":
                    unchecked{new f0((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).As()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "a":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new A instance with default field values </returns>
            public override object make() {
                subtypes.A rval = new subtypes.A();
                add(rval);
                return rval;
            }
        
            /// <returns> a new subtypes.A instance with the argument field values </returns>
            public subtypes.A make(subtypes.A a) {
                subtypes.A rval = new subtypes.A(-1, a);
                add(rval);
                return rval;
            }

            public ABuilder build() {
                return new ABuilder(this, new subtypes.A());
            }

            /// <summary>
            /// Builder for new A instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ABuilder : Builder<subtypes.A> {

                public ABuilder(AbstractStoragePool pool, subtypes.A instance) : base(pool, instance) {

                }

                public ABuilder a(subtypes.A a) {
                    instance.a = a;
                    return this;
                }

                public override subtypes.A make() {
                    pool.add(instance);
                    subtypes.A rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<subtypes.A.SubType, subtypes.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new subtypes.A.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : StoragePool<subtypes.B, subtypes.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex, P0 superPool) : base(poolIndex, "b", superPool, new string[] { "b" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new subtypes.B(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "b":
                    unchecked{new f1((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Bs()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "b":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new B instance with default field values </returns>
            public override object make() {
                subtypes.B rval = new subtypes.B();
                add(rval);
                return rval;
            }
        
            /// <returns> a new subtypes.B instance with the argument field values </returns>
            public subtypes.B make(subtypes.B b, subtypes.A a) {
                subtypes.B rval = new subtypes.B(-1, b, a);
                add(rval);
                return rval;
            }

            public BBuilder build() {
                return new BBuilder(this, new subtypes.B());
            }

            /// <summary>
            /// Builder for new B instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BBuilder : Builder<subtypes.B> {

                public BBuilder(AbstractStoragePool pool, subtypes.B instance) : base(pool, instance) {

                }

                public BBuilder b(subtypes.B b) {
                    instance.b = b;
                    return this;
                }

                public BBuilder a(subtypes.A a) {
                    instance.a = a;
                    return this;
                }

                public override subtypes.B make() {
                    pool.add(instance);
                    subtypes.B rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<subtypes.B.SubType, subtypes.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new subtypes.B.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P2 : StoragePool<subtypes.D, subtypes.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex, P1 superPool) : base(poolIndex, "d", superPool, new string[] { "d" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new subtypes.D(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "d":
                    unchecked{new f2((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Ds()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "d":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new D instance with default field values </returns>
            public override object make() {
                subtypes.D rval = new subtypes.D();
                add(rval);
                return rval;
            }
        
            /// <returns> a new subtypes.D instance with the argument field values </returns>
            public subtypes.D make(subtypes.D d, subtypes.B b, subtypes.A a) {
                subtypes.D rval = new subtypes.D(-1, d, b, a);
                add(rval);
                return rval;
            }

            public DBuilder build() {
                return new DBuilder(this, new subtypes.D());
            }

            /// <summary>
            /// Builder for new D instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class DBuilder : Builder<subtypes.D> {

                public DBuilder(AbstractStoragePool pool, subtypes.D instance) : base(pool, instance) {

                }

                public DBuilder d(subtypes.D d) {
                    instance.d = d;
                    return this;
                }

                public DBuilder b(subtypes.B b) {
                    instance.b = b;
                    return this;
                }

                public DBuilder a(subtypes.A a) {
                    instance.a = a;
                    return this;
                }

                public override subtypes.D make() {
                    pool.add(instance);
                    subtypes.D rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<subtypes.D.SubType, subtypes.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new subtypes.D.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P3 : StoragePool<subtypes.C, subtypes.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex, P0 superPool) : base(poolIndex, "c", superPool, new string[] { "c" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new subtypes.C(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "c":
                    unchecked{new f3((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Cs()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "c":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new C instance with default field values </returns>
            public override object make() {
                subtypes.C rval = new subtypes.C();
                add(rval);
                return rval;
            }
        
            /// <returns> a new subtypes.C instance with the argument field values </returns>
            public subtypes.C make(subtypes.C c, subtypes.A a) {
                subtypes.C rval = new subtypes.C(-1, c, a);
                add(rval);
                return rval;
            }

            public CBuilder build() {
                return new CBuilder(this, new subtypes.C());
            }

            /// <summary>
            /// Builder for new C instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class CBuilder : Builder<subtypes.C> {

                public CBuilder(AbstractStoragePool pool, subtypes.C instance) : base(pool, instance) {

                }

                public CBuilder c(subtypes.C c) {
                    instance.c = c;
                    return this;
                }

                public CBuilder a(subtypes.A a) {
                    instance.a = a;
                    return this;
                }

                public override subtypes.C make() {
                    pool.add(instance);
                    subtypes.C rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<subtypes.C.SubType, subtypes.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new subtypes.C.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// a A.a
        /// </summary>
        internal sealed class f0 : KnownDataField<subtypes.A, subtypes.A> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "a", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("a"))
                    throw new SkillException("Expected field type a in A.a but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                subtypes.A[] d = ((P0) owner).Data;
                P0 t = ((P0)(object)this.type);
                for (; i != h; i++) {
            d[i].a = (subtypes.A)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    subtypes.A instance = d[i].a;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                subtypes.A[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    subtypes.A v = d[i].a;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((subtypes.A) @ref).a;
            }

            public override void set(SkillObject @ref, object value) {
                ((subtypes.A) @ref).a = (subtypes.A)value;
            }
        }

        /// <summary>
        /// b B.b
        /// </summary>
        internal sealed class f1 : KnownDataField<subtypes.B, subtypes.B> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "b", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("b"))
                    throw new SkillException("Expected field type b in B.b but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                P1 t = ((P1)(object)this.type);
                for (; i != h; i++) {
            ((subtypes.B)d[i]).b = (subtypes.B)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    subtypes.B instance = ((subtypes.B)d[i]).b;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
                    subtypes.B v = ((subtypes.B)d[i]).b;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((subtypes.B) @ref).b;
            }

            public override void set(SkillObject @ref, object value) {
                ((subtypes.B) @ref).b = (subtypes.B)value;
            }
        }

        /// <summary>
        /// d D.d
        /// </summary>
        internal sealed class f2 : KnownDataField<subtypes.D, subtypes.D> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "d", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("d"))
                    throw new SkillException("Expected field type d in D.d but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                P2 t = ((P2)(object)this.type);
                for (; i != h; i++) {
            ((subtypes.D)d[i]).d = (subtypes.D)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    subtypes.D instance = ((subtypes.D)d[i]).d;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
                    subtypes.D v = ((subtypes.D)d[i]).d;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((subtypes.D) @ref).d;
            }

            public override void set(SkillObject @ref, object value) {
                ((subtypes.D) @ref).d = (subtypes.D)value;
            }
        }

        /// <summary>
        /// c C.c
        /// </summary>
        internal sealed class f3 : KnownDataField<subtypes.C, subtypes.C> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "c", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("c"))
                    throw new SkillException("Expected field type c in C.c but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                P3 t = ((P3)(object)this.type);
                for (; i != h; i++) {
            ((subtypes.C)d[i]).c = (subtypes.C)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    subtypes.C instance = ((subtypes.C)d[i]).c;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                subtypes.A[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
                    subtypes.C v = ((subtypes.C)d[i]).c;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((subtypes.C) @ref).c;
            }

            public override void set(SkillObject @ref, object value) {
                ((subtypes.C) @ref).c = (subtypes.C)value;
            }
        }

    }
}
