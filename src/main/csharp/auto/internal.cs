/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = auto.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace auto
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
                    poolByName.TryGetValue("c", out p);
                    CsField = (null == p) ? (P2)Parser.newPool("c", BsField, types) : (P2) p;
                    poolByName.TryGetValue("d", out p);
                    DsField = (null == p) ? (P3)Parser.newPool("d", BsField, types) : (P3) p;
                    poolByName.TryGetValue("noserializeddata", out p);
                    NoSerializedDatasField = (null == p) ? (P4)Parser.newPool("noserializeddata", null, types) : (P4) p;
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
        
            internal readonly P2 CsField;

            public override P2 Cs() {
                return CsField;
            }
        
            internal readonly P3 DsField;

            public override P3 Ds() {
                return DsField;
            }
        
            internal readonly P4 NoSerializedDatasField;

            public override P4 NoSerializedDatas() {
                return NoSerializedDatasField;
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


                        case "c":
                            return (superPool = new P2(types.Count, (P1)superPool));


                        case "d":
                            return (superPool = new P3(types.Count, (P1)superPool));


                        case "noserializeddata":
                            return (superPool = new P4(types.Count));
        
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
    ///  Check subtyping; use single fields only, because otherwise field IDs are underspecified
    /// </summary>
    public sealed class P0 : BasePool<auto.A> {
        
            protected override auto.A[] newArray(int size) {
                return new auto.A[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "a", new string[] { "a" }, new IAutoField[1]) {

            }

            internal auto.A[] Data {
                get
                {
                    return (auto.A[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new auto.A(i + 1);
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
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new A instance with default field values </returns>
            public override object make() {
                auto.A rval = new auto.A();
                add(rval);
                return rval;
            }
        
            /// <returns> a new auto.A instance with the argument field values </returns>
            public auto.A make(auto.A a) {
                auto.A rval = new auto.A(-1, a);
                add(rval);
                return rval;
            }

            public ABuilder build() {
                return new ABuilder(this, new auto.A());
            }

            /// <summary>
            /// Builder for new A instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ABuilder : Builder<auto.A> {

                public ABuilder(AbstractStoragePool pool, auto.A instance) : base(pool, instance) {

                }

                public ABuilder a(auto.A a) {
                    instance.a = a;
                    return this;
                }

                public override auto.A make() {
                    pool.add(instance);
                    auto.A rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<auto.A.SubType, auto.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new auto.A.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : StoragePool<auto.B, auto.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex, P0 superPool) : base(poolIndex, "b", superPool, new string[] { "b" }, new IAutoField[1]) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new auto.B(i + 1);
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
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new B instance with default field values </returns>
            public override object make() {
                auto.B rval = new auto.B();
                add(rval);
                return rval;
            }
        
            /// <returns> a new auto.B instance with the argument field values </returns>
            public auto.B make(auto.B b, auto.A a) {
                auto.B rval = new auto.B(-1, b, a);
                add(rval);
                return rval;
            }

            public BBuilder build() {
                return new BBuilder(this, new auto.B());
            }

            /// <summary>
            /// Builder for new B instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BBuilder : Builder<auto.B> {

                public BBuilder(AbstractStoragePool pool, auto.B instance) : base(pool, instance) {

                }

                public BBuilder b(auto.B b) {
                    instance.b = b;
                    return this;
                }

                public BBuilder a(auto.A a) {
                    instance.a = a;
                    return this;
                }

                public override auto.B make() {
                    pool.add(instance);
                    auto.B rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<auto.B.SubType, auto.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new auto.B.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P2 : StoragePool<auto.C, auto.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex, P1 superPool) : base(poolIndex, "c", superPool, new string[] { "c" }, new IAutoField[1]) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new auto.C(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "c":
                    unchecked{new f2((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Cs()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "c":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new C instance with default field values </returns>
            public override object make() {
                auto.C rval = new auto.C();
                add(rval);
                return rval;
            }
        
            /// <returns> a new auto.C instance with the argument field values </returns>
            public auto.C make(auto.C c, auto.B b, auto.A a) {
                auto.C rval = new auto.C(-1, c, b, a);
                add(rval);
                return rval;
            }

            public CBuilder build() {
                return new CBuilder(this, new auto.C());
            }

            /// <summary>
            /// Builder for new C instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class CBuilder : Builder<auto.C> {

                public CBuilder(AbstractStoragePool pool, auto.C instance) : base(pool, instance) {

                }

                public CBuilder c(auto.C c) {
                    instance.c = c;
                    return this;
                }

                public CBuilder b(auto.B b) {
                    instance.b = b;
                    return this;
                }

                public CBuilder a(auto.A a) {
                    instance.a = a;
                    return this;
                }

                public override auto.C make() {
                    pool.add(instance);
                    auto.C rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<auto.C.SubType, auto.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new auto.C.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P3 : StoragePool<auto.D, auto.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex, P1 superPool) : base(poolIndex, "d", superPool, new string[] { "d" }, new IAutoField[1]) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new auto.D(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "d":
                    unchecked{new f3((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Ds()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "d":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new D instance with default field values </returns>
            public override object make() {
                auto.D rval = new auto.D();
                add(rval);
                return rval;
            }
        
            /// <returns> a new auto.D instance with the argument field values </returns>
            public auto.D make(auto.D d, auto.B b, auto.A a) {
                auto.D rval = new auto.D(-1, d, b, a);
                add(rval);
                return rval;
            }

            public DBuilder build() {
                return new DBuilder(this, new auto.D());
            }

            /// <summary>
            /// Builder for new D instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class DBuilder : Builder<auto.D> {

                public DBuilder(AbstractStoragePool pool, auto.D instance) : base(pool, instance) {

                }

                public DBuilder d(auto.D d) {
                    instance.d = d;
                    return this;
                }

                public DBuilder b(auto.B b) {
                    instance.b = b;
                    return this;
                }

                public DBuilder a(auto.A a) {
                    instance.a = a;
                    return this;
                }

                public override auto.D make() {
                    pool.add(instance);
                    auto.D rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<auto.D.SubType, auto.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new auto.D.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  All fields of this type are auto.
    ///  @author  Timm Felden
    /// </summary>
    public sealed class P4 : BasePool<auto.NoSerializedData> {
        
            protected override auto.NoSerializedData[] newArray(int size) {
                return new auto.NoSerializedData[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P4(int poolIndex) : base(poolIndex, "noserializeddata", new string[] { "age", "name", "seen", "someintegersinalist", "somemap", "somereference" }, new IAutoField[6]) {

            }

            internal auto.NoSerializedData[] Data {
                get
                {
                    return (auto.NoSerializedData[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new auto.NoSerializedData(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "age":
                    unchecked{new f4(V64.get(), this);}
                    return;

                case "name":
                    unchecked{new f5(@string, this);}
                    return;

                case "seen":
                    unchecked{new f6(BoolType.get(), this);}
                    return;

                case "someintegersinalist":
                    unchecked{new f7(new ListType<int>(I32.get()), this);}
                    return;

                case "somemap":
                    unchecked{new f8(new MapType<string, string>(@string, @string), this);}
                    return;

                case "somereference":
                    unchecked{new f9((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).NoSerializedDatas()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "age":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                case "name":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                case "seen":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                case "someintegersinalist":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                case "somemap":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                case "somereference":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new NoSerializedData instance with default field values </returns>
            public override object make() {
                auto.NoSerializedData rval = new auto.NoSerializedData();
                add(rval);
                return rval;
            }
        
            /// <returns> a new auto.NoSerializedData instance with the argument field values </returns>
            public auto.NoSerializedData make(long age, string name, bool seen, System.Collections.Generic.List<System.Int32> someIntegersInAList, System.Collections.Generic.Dictionary<System.String, System.String> someMap, auto.NoSerializedData someReference) {
                auto.NoSerializedData rval = new auto.NoSerializedData(-1, age, name, seen, someIntegersInAList, someMap, someReference);
                add(rval);
                return rval;
            }

            public NoSerializedDataBuilder build() {
                return new NoSerializedDataBuilder(this, new auto.NoSerializedData());
            }

            /// <summary>
            /// Builder for new NoSerializedData instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class NoSerializedDataBuilder : Builder<auto.NoSerializedData> {

                public NoSerializedDataBuilder(AbstractStoragePool pool, auto.NoSerializedData instance) : base(pool, instance) {

                }

                public NoSerializedDataBuilder age(long age) {
                    instance.age = age;
                    return this;
                }

                public NoSerializedDataBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public NoSerializedDataBuilder seen(bool seen) {
                    instance.seen = seen;
                    return this;
                }

                public NoSerializedDataBuilder someIntegersInAList(System.Collections.Generic.List<System.Int32> someIntegersInAList) {
                    instance.someIntegersInAList = someIntegersInAList;
                    return this;
                }

                public NoSerializedDataBuilder someMap(System.Collections.Generic.Dictionary<System.String, System.String> someMap) {
                    instance.someMap = someMap;
                    return this;
                }

                public NoSerializedDataBuilder someReference(auto.NoSerializedData someReference) {
                    instance.someReference = someReference;
                    return this;
                }

                public override auto.NoSerializedData make() {
                    pool.add(instance);
                    auto.NoSerializedData rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<auto.NoSerializedData.SubType, auto.NoSerializedData> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new auto.NoSerializedData.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// a A.a
        /// </summary>
        internal sealed class f0 : AutoField<auto.A, auto.A> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "a", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.A) @ref).a;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.A) @ref).a = (auto.A)value;
            }
        }

        /// <summary>
        /// b B.b
        /// </summary>
        internal sealed class f1 : AutoField<auto.B, auto.B> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "b", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.B) @ref).b;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.B) @ref).b = (auto.B)value;
            }
        }

        /// <summary>
        /// c C.c
        /// </summary>
        internal sealed class f2 : AutoField<auto.C, auto.C> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "c", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.C) @ref).c;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.C) @ref).c = (auto.C)value;
            }
        }

        /// <summary>
        /// d D.d
        /// </summary>
        internal sealed class f3 : AutoField<auto.D, auto.D> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "d", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.D) @ref).d;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.D) @ref).d = (auto.D)value;
            }
        }

        /// <summary>
        /// v64 NoSerializedData.age
        /// </summary>
        internal sealed class f4 : AutoField<System.Int64, auto.NoSerializedData> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "age", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.NoSerializedData) @ref).age;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.NoSerializedData) @ref).age = (System.Int64)value;
            }
        }

        /// <summary>
        /// string NoSerializedData.name
        /// </summary>
        internal sealed class f5 : AutoField<System.String, auto.NoSerializedData> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "name", -1, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.NoSerializedData) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.NoSerializedData) @ref).name = (System.String)value;
            }
        }

        /// <summary>
        /// bool NoSerializedData.seen
        /// </summary>
        internal sealed class f6 : AutoField<System.Boolean, auto.NoSerializedData> {

            public f6(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "seen", -2, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.NoSerializedData) @ref).seen;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.NoSerializedData) @ref).seen = (System.Boolean)value;
            }
        }

        /// <summary>
        /// list<i32> NoSerializedData.someIntegersInAList
        /// </summary>
        internal sealed class f7 : AutoField<System.Collections.Generic.List<System.Int32>, auto.NoSerializedData> {

            public f7(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "someintegersinalist", -3, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.NoSerializedData) @ref).someIntegersInAList;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.NoSerializedData) @ref).someIntegersInAList = ((System.Collections.Generic.List<object>)value).Cast<int>().ToList();
            }
        }

        /// <summary>
        /// map<string,string> NoSerializedData.someMap
        /// </summary>
        internal sealed class f8 : AutoField<System.Collections.Generic.Dictionary<System.String, System.String>, auto.NoSerializedData> {

            public f8(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "somemap", -4, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.NoSerializedData) @ref).someMap;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.NoSerializedData) @ref).someMap = castMap<string, string, object, object>((System.Collections.Generic.Dictionary<object, object>)value);
            }
        }

        /// <summary>
        /// noserializeddata NoSerializedData.someReference
        /// </summary>
        internal sealed class f9 : AutoField<auto.NoSerializedData, auto.NoSerializedData> {

            public f9(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "somereference", -5, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((auto.NoSerializedData) @ref).someReference;
            }

            public override void set(SkillObject @ref, object value) {
                ((auto.NoSerializedData) @ref).someReference = (auto.NoSerializedData)value;
            }
        }

    }
}
