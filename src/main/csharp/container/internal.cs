/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = container.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace container
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
                    poolByName.TryGetValue("container", out p);
                    ContainersField = (null == p) ? (P0)Parser.newPool("container", null, types) : (P0) p;
                    poolByName.TryGetValue("somethingelse", out p);
                    SomethingElsesField = (null == p) ? (P1)Parser.newPool("somethingelse", null, types) : (P1) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 ContainersField;

            public override P0 Containers() {
                return ContainersField;
            }
        
            internal readonly P1 SomethingElsesField;

            public override P1 SomethingElses() {
                return SomethingElsesField;
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
                        case "container":
                            return (superPool = new P0(types.Count));
        

                        case "somethingelse":
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

        public sealed class P0 : BasePool<container.Container> {
        
            protected override container.Container[] newArray(int size) {
                return new container.Container[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "container", new string[] { "arr", "f", "l", "s", "someset", "varr" }, NoAutoFields) {

            }

            internal container.Container[] Data {
                get
                {
                    return (container.Container[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new container.Container(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "arr":
                    unchecked{new f0(new ConstantLengthArray<long>(3, V64.get()), this);}
                    return;

                case "f":
                    unchecked{new f1(new MapType<string, System.Collections.Generic.Dictionary<long,long>>(@string, new MapType<long, long>(V64.get(), V64.get())), this);}
                    return;

                case "l":
                    unchecked{new f2(new ListType<long>(V64.get()), this);}
                    return;

                case "s":
                    unchecked{new f3(new SetType<long>(V64.get()), this);}
                    return;

                case "someset":
                    unchecked{new f4(new SetType<container.SomethingElse>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).SomethingElses())), this);}
                    return;

                case "varr":
                    unchecked{new f5(new VariableLengthArray<long>(V64.get()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "arr":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "f":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "l":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "s":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "someset":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "varr":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Container instance with default field values </returns>
            public override object make() {
                container.Container rval = new container.Container();
                add(rval);
                return rval;
            }
        
            /// <returns> a new container.Container instance with the argument field values </returns>
            public container.Container make(System.Collections.ArrayList arr, System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> f, System.Collections.Generic.List<System.Int64> l, System.Collections.Generic.HashSet<System.Int64> s, System.Collections.Generic.HashSet<container.SomethingElse> someSet, System.Collections.ArrayList varr) {
                container.Container rval = new container.Container(-1, arr, f, l, s, someSet, varr);
                add(rval);
                return rval;
            }

            public ContainerBuilder build() {
                return new ContainerBuilder(this, new container.Container());
            }

            /// <summary>
            /// Builder for new Container instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ContainerBuilder : Builder<container.Container> {

                public ContainerBuilder(AbstractStoragePool pool, container.Container instance) : base(pool, instance) {

                }

                public ContainerBuilder arr(System.Collections.ArrayList arr) {
                    instance.arr = arr;
                    return this;
                }

                public ContainerBuilder f(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> f) {
                    instance.f = f;
                    return this;
                }

                public ContainerBuilder l(System.Collections.Generic.List<System.Int64> l) {
                    instance.l = l;
                    return this;
                }

                public ContainerBuilder s(System.Collections.Generic.HashSet<System.Int64> s) {
                    instance.s = s;
                    return this;
                }

                public ContainerBuilder someSet(System.Collections.Generic.HashSet<container.SomethingElse> someSet) {
                    instance.someSet = someSet;
                    return this;
                }

                public ContainerBuilder varr(System.Collections.ArrayList varr) {
                    instance.varr = varr;
                    return this;
                }

                public override container.Container make() {
                    pool.add(instance);
                    container.Container rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<container.Container.SubType, container.Container> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new container.Container.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  no instance of this is required
    /// </summary>
    public sealed class P1 : BasePool<container.SomethingElse> {
        
            protected override container.SomethingElse[] newArray(int size) {
                return new container.SomethingElse[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "somethingelse", noKnownFields, NoAutoFields) {

            }

            internal container.SomethingElse[] Data {
                get
                {
                    return (container.SomethingElse[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new container.SomethingElse(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new SomethingElse instance with default field values </returns>
            public override object make() {
                container.SomethingElse rval = new container.SomethingElse();
                add(rval);
                return rval;
            }
        
            public SomethingElseBuilder build() {
                return new SomethingElseBuilder(this, new container.SomethingElse());
            }

            /// <summary>
            /// Builder for new SomethingElse instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class SomethingElseBuilder : Builder<container.SomethingElse> {

                public SomethingElseBuilder(AbstractStoragePool pool, container.SomethingElse instance) : base(pool, instance) {

                }

                public override container.SomethingElse make() {
                    pool.add(instance);
                    container.SomethingElse rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<container.SomethingElse.SubType, container.SomethingElse> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new container.SomethingElse.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// v64[3] Container.arr
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Collections.ArrayList, container.Container> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "arr", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type v64[3] in Container.arr but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                container.Container[] d = ((P0) owner).Data;
                ConstantLengthArray<System.Int64> type = (ConstantLengthArray<System.Int64>) this.type.cast<System.Int64, System.Object>();
                int size = type.length;
                for (; i != h; i++) {
            int s = size;
            System.Collections.ArrayList v = new ArrayList(size);
            while (s-- > 0) {
                v.Add(@in.v64());
            }
            d[i].arr = v;
                }

            }
            public override void osc(int i, int h) {
                ConstantLengthArray<System.Int64> type = (ConstantLengthArray<System.Int64>) this.type.cast<System.Int64, System.Object>();
                int size = type.length;
                container.Container[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.ArrayList v = null == d[i].arr ? null : v = (System.Collections.ArrayList)d[i].arr;
                    if (v.Count != type.length)
                        throw new Exception("constant length array has wrong size");

                    foreach(long x in v)
                    result += V64.singleV64Offset(x);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                container.Container[] d = ((P0) owner).Data;
                ConstantLengthArray<System.Int64> type = (ConstantLengthArray<System.Int64>) this.type.cast<System.Int64, System.Object>();
                int size = type.length;
                for (; i != h; i++) {
                    
        System.Collections.ArrayList x = d[i].arr;
        foreach (long e in x){
            @out.v64(e);
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((container.Container) @ref).arr;
            }

            public override void set(SkillObject @ref, object value) {
                ((container.Container) @ref).arr = (System.Collections.ArrayList)value;
            }
        }

        /// <summary>
        /// map<string,v64,v64> Container.f
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>, container.Container> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "f", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type map<string,v64,v64> in Container.f but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                container.Container[] d = ((P0) owner).Data;
                MapType<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> type = (MapType<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>) this.type.cast<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
            d[i].f = castMap<string, System.Collections.Generic.Dictionary<long, long>, System.Object, System.Object>((Dictionary<System.Object, System.Object>)((de.ust.skill.common.csharp.@internal.FieldType)this.type).readSingleField(@in));
                }

            }
            public override void osc(int i, int h) {
                MapType<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> type = (MapType<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>) this.type.cast<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                container.Container[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> v = castMap<string, System.Collections.Generic.Dictionary<long, long>, string, System.Collections.Generic.Dictionary<long, long>>(d[i].f);
                    if(null==v || v.Count == 0)
                        result++;
                    else {

                        string[] keysArray = new string[v.Keys.Count];
                        v.Keys.CopyTo(keysArray, 0);
                        ICollection keysList = new List<object>();
                        foreach (object key in keysArray)
                        {
                            ((List<object>)keysList).Add(key);
                        }

                        System.Collections.Generic.Dictionary<long, long>[] valuesArray = new System.Collections.Generic.Dictionary<long, long>[v.Values.Count];
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
                container.Container[] d = ((P0) owner).Data;
                MapType<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>> type = (MapType<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>) this.type.cast<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
                    ((de.ust.skill.common.csharp.@internal.FieldType)this.type).writeSingleField(d[i].f, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((container.Container) @ref).f;
            }

            public override void set(SkillObject @ref, object value) {
                ((container.Container) @ref).f = castMap<string, System.Collections.Generic.Dictionary<long, long>, object, object>((System.Collections.Generic.Dictionary<object, object>)value);
            }
        }

        /// <summary>
        /// list<v64> Container.l
        /// </summary>
        internal sealed class f2 : KnownDataField<System.Collections.Generic.List<System.Int64>, container.Container> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "l", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type list<v64> in Container.l but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                container.Container[] d = ((P0) owner).Data;
                ListType<System.Int64> type = (ListType<System.Int64>) this.type.cast<System.Int64, System.Object>();
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.List<System.Int64> v = new List<long>();
            while (size-- > 0) {
                v.Add(@in.v64());
            }
            d[i].l = v;
                }

            }
            public override void osc(int i, int h) {
                ListType<System.Int64> type = (ListType<System.Int64>) this.type.cast<System.Int64, System.Object>();
                container.Container[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.List<System.Int64> v = null == d[i].l ? null : ((System.Collections.Generic.List<System.Int64>)d[i].l).Cast<long>().ToList();

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(long x in v)
                    result += V64.singleV64Offset(x);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                container.Container[] d = ((P0) owner).Data;
                ListType<System.Int64> type = (ListType<System.Int64>) this.type.cast<System.Int64, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.Generic.List<System.Int64> x = d[i].l;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (long e in x){
                @out.v64(e);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((container.Container) @ref).l;
            }

            public override void set(SkillObject @ref, object value) {
                ((container.Container) @ref).l = ((System.Collections.Generic.List<object>)value).Cast<long>().ToList();
            }
        }

        /// <summary>
        /// set<v64> Container.s
        /// </summary>
        internal sealed class f3 : KnownDataField<System.Collections.Generic.HashSet<System.Int64>, container.Container> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "s", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type set<v64> in Container.s but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                container.Container[] d = ((P0) owner).Data;
                SetType<System.Int64> type = (SetType<System.Int64>) this.type.cast<System.Int64, System.Object>();
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.HashSet<System.Int64> v = new HashSet<long>();
            while (size-- > 0) {
                v.Add(@in.v64());
            }
            d[i].s = v;
                }

            }
            public override void osc(int i, int h) {
                SetType<System.Int64> type = (SetType<System.Int64>) this.type.cast<System.Int64, System.Object>();
                container.Container[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.HashSet<System.Int64> v = null == d[i].s ? null : new System.Collections.Generic.HashSet<System.Int64>(((System.Collections.Generic.HashSet<System.Int64>)d[i].s).Cast<long>());

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(long x in v)
                    result += V64.singleV64Offset(x);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                container.Container[] d = ((P0) owner).Data;
                SetType<System.Int64> type = (SetType<System.Int64>) this.type.cast<System.Int64, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.Generic.HashSet<System.Int64> x = d[i].s;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (long e in x){
                @out.v64(e);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((container.Container) @ref).s;
            }

            public override void set(SkillObject @ref, object value) {
                ((container.Container) @ref).s = new System.Collections.Generic.HashSet<long>(((System.Collections.Generic.HashSet<object>)value).Cast<long>());
            }
        }

        /// <summary>
        /// set<somethingelse> Container.someSet
        /// </summary>
        internal sealed class f4 : KnownDataField<System.Collections.Generic.HashSet<container.SomethingElse>, container.Container> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "someset", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type set<somethingelse> in Container.someSet but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                container.Container[] d = ((P0) owner).Data;
                SetType<container.SomethingElse> type = (SetType<container.SomethingElse>) this.type.cast<container.SomethingElse, System.Object>();
                P1 t = ((P1)(object)type.groundType);
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.HashSet<container.SomethingElse> v = new HashSet<container.SomethingElse>();
            while (size-- > 0) {
                v.Add((container.SomethingElse)t.getByID(@in.v32()));
            }
            d[i].someSet = v;
                }

            }
            public override void osc(int i, int h) {
                SetType<container.SomethingElse> type = (SetType<container.SomethingElse>) this.type.cast<container.SomethingElse, System.Object>();
                container.Container[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.HashSet<container.SomethingElse> v = null == d[i].someSet ? null : new System.Collections.Generic.HashSet<container.SomethingElse>(((System.Collections.Generic.HashSet<container.SomethingElse>)d[i].someSet).Cast<container.SomethingElse>());

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(container.SomethingElse x in v)
                    result += null==x?1:V64.singleV64Offset(x.SkillID);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                container.Container[] d = ((P0) owner).Data;
                SetType<container.SomethingElse> type = (SetType<container.SomethingElse>) this.type.cast<container.SomethingElse, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.Generic.HashSet<container.SomethingElse> x = d[i].someSet;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (container.SomethingElse e in x){
                container.SomethingElse v = e;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((container.Container) @ref).someSet;
            }

            public override void set(SkillObject @ref, object value) {
                ((container.Container) @ref).someSet = new System.Collections.Generic.HashSet<container.SomethingElse>(((System.Collections.Generic.HashSet<object>)value).Cast<container.SomethingElse>());
            }
        }

        /// <summary>
        /// v64[] Container.varr
        /// </summary>
        internal sealed class f5 : KnownDataField<System.Collections.ArrayList, container.Container> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "varr", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type v64[] in Container.varr but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                container.Container[] d = ((P0) owner).Data;
                VariableLengthArray<System.Int64> type = (VariableLengthArray<System.Int64>) this.type.cast<System.Int64, System.Object>();
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.ArrayList v = new ArrayList(size);
            while (size-- > 0) {
                v.Add(@in.v64());
            }
            d[i].varr = v;
                }

            }
            public override void osc(int i, int h) {
                VariableLengthArray<System.Int64> type = (VariableLengthArray<System.Int64>) this.type.cast<System.Int64, System.Object>();
                container.Container[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.ArrayList v = null == d[i].varr ? null : (System.Collections.ArrayList)d[i].varr;

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(long x in v)
                    result += V64.singleV64Offset(x);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                container.Container[] d = ((P0) owner).Data;
                VariableLengthArray<System.Int64> type = (VariableLengthArray<System.Int64>) this.type.cast<System.Int64, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.ArrayList x = d[i].varr;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (long e in x){
                @out.v64(e);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((container.Container) @ref).varr;
            }

            public override void set(SkillObject @ref, object value) {
                ((container.Container) @ref).varr = (System.Collections.ArrayList)value;
            }
        }

    }
}
