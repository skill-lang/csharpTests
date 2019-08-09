/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = fancy.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace fancy
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
                    DsField = (null == p) ? (P3)Parser.newPool("d", CsField, types) : (P3) p;
                    poolByName.TryGetValue("g", out p);
                    GsField = (null == p) ? (P4)Parser.newPool("g", CsField, types) : (P4) p;
                    poolByName.TryGetValue("h", out p);
                    HsField = (null == p) ? (P5)Parser.newPool("h", AsField, types) : (P5) p;
                    poolByName.TryGetValue("i", out p);
                    IsField = (null == p) ? (P6)Parser.newPool("i", AsField, types) : (P6) p;
                    poolByName.TryGetValue("j", out p);
                    JsField = (null == p) ? (P7)Parser.newPool("j", AsField, types) : (P7) p;
                    EsField = new de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A>("e", CsField, GsField);
                    FsField = new de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A>("f", CsField, GsField);
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
        
            internal readonly P4 GsField;

            public override P4 Gs() {
                return GsField;
            }
        
            internal readonly P5 HsField;

            public override P5 Hs() {
                return HsField;
            }
        
            internal readonly P6 IsField;

            public override P6 Is() {
                return IsField;
            }
        
            internal readonly P7 JsField;

            public override P7 Js() {
                return JsField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A> EsField;

            public override de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A> Es() {
                return EsField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A> FsField;

            public override de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A> Fs() {
                return FsField;
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
                            return (superPool = new P3(types.Count, (P2)superPool));


                        case "g":
                            return (superPool = new P4(types.Count, (P2)superPool));


                        case "h":
                            return (superPool = new P5(types.Count, (P0)superPool));


                        case "i":
                            return (superPool = new P6(types.Count, (P0)superPool));


                        case "j":
                            return (superPool = new P7(types.Count, (P0)superPool));

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

        public sealed class P0 : BasePool<fancy.A> {
        
            protected override fancy.A[] newArray(int size) {
                return new fancy.A[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "a", new string[] { "a", "parent" }, NoAutoFields) {

            }

            internal fancy.A[] Data {
                get
                {
                    return (fancy.A[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.A(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "a":
                    unchecked{new f0(annotation, this);}
                    return;

                case "parent":
                    unchecked{new f1((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).As()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "a":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "parent":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new A instance with default field values </returns>
            public override object make() {
                fancy.A rval = new fancy.A();
                add(rval);
                return rval;
            }
        
            /// <returns> a new fancy.A instance with the argument field values </returns>
            public fancy.A make(de.ust.skill.common.csharp.@internal.SkillObject a, fancy.A Parent) {
                fancy.A rval = new fancy.A(-1, a, Parent);
                add(rval);
                return rval;
            }

            public ABuilder build() {
                return new ABuilder(this, new fancy.A());
            }

            /// <summary>
            /// Builder for new A instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ABuilder : Builder<fancy.A> {

                public ABuilder(AbstractStoragePool pool, fancy.A instance) : base(pool, instance) {

                }

                public ABuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public ABuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.A make() {
                    pool.add(instance);
                    fancy.A rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.A.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.A.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : StoragePool<fancy.B, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex, P0 superPool) : base(poolIndex, "b", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.B(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new B instance with default field values </returns>
            public override object make() {
                fancy.B rval = new fancy.B();
                add(rval);
                return rval;
            }
        
            public BBuilder build() {
                return new BBuilder(this, new fancy.B());
            }

            /// <summary>
            /// Builder for new B instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class BBuilder : Builder<fancy.B> {

                public BBuilder(AbstractStoragePool pool, fancy.B instance) : base(pool, instance) {

                }

                public BBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public BBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.B make() {
                    pool.add(instance);
                    fancy.B rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.B.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.B.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P2 : StoragePool<fancy.C, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex, P1 superPool) : base(poolIndex, "c", superPool, new string[] { "value" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.C(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "value":
                    unchecked{new f2((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Cs()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "value":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new C instance with default field values </returns>
            public override object make() {
                fancy.C rval = new fancy.C();
                add(rval);
                return rval;
            }
        
            /// <returns> a new fancy.C instance with the argument field values </returns>
            public fancy.C make(fancy.C Value, de.ust.skill.common.csharp.@internal.SkillObject a, fancy.A Parent) {
                fancy.C rval = new fancy.C(-1, Value, a, Parent);
                add(rval);
                return rval;
            }

            public CBuilder build() {
                return new CBuilder(this, new fancy.C());
            }

            /// <summary>
            /// Builder for new C instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class CBuilder : Builder<fancy.C> {

                public CBuilder(AbstractStoragePool pool, fancy.C instance) : base(pool, instance) {

                }

                public CBuilder Value(fancy.C Value) {
                    instance.Value = Value;
                    return this;
                }

                public CBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public CBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.C make() {
                    pool.add(instance);
                    fancy.C rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.C.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.C.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P3 : StoragePool<fancy.D, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex, P2 superPool) : base(poolIndex, "d", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.D(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new D instance with default field values </returns>
            public override object make() {
                fancy.D rval = new fancy.D();
                add(rval);
                return rval;
            }
        
            public DBuilder build() {
                return new DBuilder(this, new fancy.D());
            }

            /// <summary>
            /// Builder for new D instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class DBuilder : Builder<fancy.D> {

                public DBuilder(AbstractStoragePool pool, fancy.D instance) : base(pool, instance) {

                }

                public DBuilder Value(fancy.C Value) {
                    instance.Value = Value;
                    return this;
                }

                public DBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public DBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.D make() {
                    pool.add(instance);
                    fancy.D rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.D.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.D.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P4 : StoragePool<fancy.G, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P4(int poolIndex, P2 superPool) : base(poolIndex, "g", superPool, new string[] { "amap" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.G(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "amap":
                    unchecked{new f3(new MapType<fancy.E, fancy.F>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Es()), (de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Fs())), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "amap":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new G instance with default field values </returns>
            public override object make() {
                fancy.G rval = new fancy.G();
                add(rval);
                return rval;
            }
        
            /// <returns> a new fancy.G instance with the argument field values </returns>
            public fancy.G make(fancy.C Value, de.ust.skill.common.csharp.@internal.SkillObject a, fancy.A Parent, System.Collections.Generic.Dictionary<fancy.E, fancy.F> aMap) {
                fancy.G rval = new fancy.G(-1, Value, a, Parent, aMap);
                add(rval);
                return rval;
            }

            public GBuilder build() {
                return new GBuilder(this, new fancy.G());
            }

            /// <summary>
            /// Builder for new G instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class GBuilder : Builder<fancy.G> {

                public GBuilder(AbstractStoragePool pool, fancy.G instance) : base(pool, instance) {

                }

                public GBuilder Value(fancy.C Value) {
                    instance.Value = Value;
                    return this;
                }

                public GBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public GBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public GBuilder aMap(System.Collections.Generic.Dictionary<fancy.E, fancy.F> aMap) {
                    instance.aMap = aMap;
                    return this;
                }

                public override fancy.G make() {
                    pool.add(instance);
                    fancy.G rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.G.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.G.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P5 : StoragePool<fancy.H, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P5(int poolIndex, P0 superPool) : base(poolIndex, "h", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.H(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new H instance with default field values </returns>
            public override object make() {
                fancy.H rval = new fancy.H();
                add(rval);
                return rval;
            }
        
            public HBuilder build() {
                return new HBuilder(this, new fancy.H());
            }

            /// <summary>
            /// Builder for new H instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class HBuilder : Builder<fancy.H> {

                public HBuilder(AbstractStoragePool pool, fancy.H instance) : base(pool, instance) {

                }

                public HBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public HBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.H make() {
                    pool.add(instance);
                    fancy.H rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.H.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.H.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P6 : StoragePool<fancy.I, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P6(int poolIndex, P0 superPool) : base(poolIndex, "i", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.I(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new I instance with default field values </returns>
            public override object make() {
                fancy.I rval = new fancy.I();
                add(rval);
                return rval;
            }
        
            public IBuilder build() {
                return new IBuilder(this, new fancy.I());
            }

            /// <summary>
            /// Builder for new I instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class IBuilder : Builder<fancy.I> {

                public IBuilder(AbstractStoragePool pool, fancy.I instance) : base(pool, instance) {

                }

                public IBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public IBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.I make() {
                    pool.add(instance);
                    fancy.I rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.I.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.I.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P7 : StoragePool<fancy.J, fancy.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P7(int poolIndex, P0 superPool) : base(poolIndex, "j", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new fancy.J(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new J instance with default field values </returns>
            public override object make() {
                fancy.J rval = new fancy.J();
                add(rval);
                return rval;
            }
        
            public JBuilder build() {
                return new JBuilder(this, new fancy.J());
            }

            /// <summary>
            /// Builder for new J instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class JBuilder : Builder<fancy.J> {

                public JBuilder(AbstractStoragePool pool, fancy.J instance) : base(pool, instance) {

                }

                public JBuilder a(de.ust.skill.common.csharp.@internal.SkillObject a) {
                    instance.a = a;
                    return this;
                }

                public JBuilder Parent(fancy.A Parent) {
                    instance.Parent = Parent;
                    return this;
                }

                public override fancy.J make() {
                    pool.add(instance);
                    fancy.J rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<fancy.J.SubType, fancy.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new fancy.J.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// annotation A.a
        /// </summary>
        internal sealed class f0 : KnownDataField<de.ust.skill.common.csharp.@internal.SkillObject, fancy.A> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "a", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in A.a but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                fancy.A[] d = ((P0) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
            d[i].a = (de.ust.skill.common.csharp.@internal.SkillObject)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t = (Annotation)this.type;
                fancy.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    de.ust.skill.common.csharp.@internal.SkillObject v = d[i].a;
                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                fancy.A[] d = ((P0) owner).Data;
                Annotation t = (Annotation)this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].a, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((fancy.A) @ref).a;
            }

            public override void set(SkillObject @ref, object value) {
                ((fancy.A) @ref).a = (de.ust.skill.common.csharp.@internal.SkillObject)value;
            }
        }

        /// <summary>
        /// a A.Parent
        /// </summary>
        internal sealed class f1 : KnownDataField<fancy.A, fancy.A> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "parent", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("a"))
                    throw new SkillException("Expected field type a in A.Parent but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                fancy.A[] d = ((P0) owner).Data;
                P0 t = ((P0)(object)this.type);
                for (; i != h; i++) {
            d[i].Parent = (fancy.A)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                fancy.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    fancy.A instance = d[i].Parent;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                fancy.A[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    fancy.A v = d[i].Parent;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((fancy.A) @ref).Parent;
            }

            public override void set(SkillObject @ref, object value) {
                ((fancy.A) @ref).Parent = (fancy.A)value;
            }
        }

        /// <summary>
        /// c C.Value
        /// </summary>
        internal sealed class f2 : KnownDataField<fancy.C, fancy.C> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "value", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("c"))
                    throw new SkillException("Expected field type c in C.Value but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                fancy.A[] d = ((P0) owner.basePool).Data;
                P2 t = ((P2)(object)this.type);
                for (; i != h; i++) {
            ((fancy.C)d[i]).Value = (fancy.C)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                fancy.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    fancy.C instance = ((fancy.C)d[i]).Value;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                fancy.A[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
                    fancy.C v = ((fancy.C)d[i]).Value;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((fancy.C) @ref).Value;
            }

            public override void set(SkillObject @ref, object value) {
                ((fancy.C) @ref).Value = (fancy.C)value;
            }
        }

        /// <summary>
        /// map<c,c> G.aMap
        /// </summary>
        internal sealed class f3 : KnownDataField<System.Collections.Generic.Dictionary<fancy.E, fancy.F>, fancy.G> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P4 owner) : base(type, "amap", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type map<c,c> in G.aMap but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                fancy.A[] d = ((P0) owner.basePool).Data;
                MapType<fancy.E, fancy.F> type = (MapType<fancy.E, fancy.F>) this.type.cast<fancy.E, fancy.F>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
            ((fancy.G)d[i]).aMap = castMap<fancy.E, fancy.F, System.Object, System.Object>((Dictionary<System.Object, System.Object>)((de.ust.skill.common.csharp.@internal.FieldType)this.type).readSingleField(@in));
                }

            }
            public override void osc(int i, int h) {
                MapType<fancy.E, fancy.F> type = (MapType<fancy.E, fancy.F>) this.type.cast<fancy.E, fancy.F>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                fancy.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.Dictionary<fancy.C, fancy.C> v = castMap<fancy.C, fancy.C, fancy.E, fancy.F>(((fancy.G)d[i]).aMap);
                    if(null==v || v.Count == 0)
                        result++;
                    else {

                        fancy.C[] keysArray = new fancy.C[v.Keys.Count];
                        v.Keys.CopyTo(keysArray, 0);
                        ICollection keysList = new List<object>();
                        foreach (object key in keysArray)
                        {
                            ((List<object>)keysList).Add(key);
                        }

                        fancy.C[] valuesArray = new fancy.C[v.Values.Count];
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
                fancy.A[] d = ((P0) owner.basePool).Data;
                MapType<fancy.E, fancy.F> type = (MapType<fancy.E, fancy.F>) this.type.cast<fancy.E, fancy.F>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
                    ((de.ust.skill.common.csharp.@internal.FieldType)this.type).writeSingleField(((fancy.G)d[i]).aMap, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((fancy.G) @ref).aMap;
            }

            public override void set(SkillObject @ref, object value) {
                ((fancy.G) @ref).aMap = castMap<fancy.E, fancy.F, object, object>((System.Collections.Generic.Dictionary<object, object>)value);
            }
        }

    }
}
