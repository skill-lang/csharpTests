/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = map3.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace map3
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
                    poolByName.TryGetValue("l", out p);
                    LsField = (null == p) ? (P0)Parser.newPool("l", null, types) : (P0) p;
                    poolByName.TryGetValue("t", out p);
                    TsField = (null == p) ? (P1)Parser.newPool("t", null, types) : (P1) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 LsField;

            public override P0 Ls() {
                return LsField;
            }
        
            internal readonly P1 TsField;

            public override P1 Ts() {
                return TsField;
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
                        case "l":
                            return (superPool = new P0(types.Count));
        

                        case "t":
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

        public sealed class P0 : BasePool<map3.L> {
        
            protected override map3.L[] newArray(int size) {
                return new map3.L[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "l", noKnownFields, NoAutoFields) {

            }

            internal map3.L[] Data {
                get
                {
                    return (map3.L[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new map3.L(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new L instance with default field values </returns>
            public override object make() {
                map3.L rval = new map3.L();
                add(rval);
                return rval;
            }
        
            public LBuilder build() {
                return new LBuilder(this, new map3.L());
            }

            /// <summary>
            /// Builder for new L instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class LBuilder : Builder<map3.L> {

                public LBuilder(AbstractStoragePool pool, map3.L instance) : base(pool, instance) {

                }

                public override map3.L make() {
                    pool.add(instance);
                    map3.L rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<map3.L.SubType, map3.L> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new map3.L.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : BasePool<map3.T> {
        
            protected override map3.T[] newArray(int size) {
                return new map3.T[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "t", new string[] { "ref" }, NoAutoFields) {

            }

            internal map3.T[] Data {
                get
                {
                    return (map3.T[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new map3.T(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "ref":
                    unchecked{new f0(new MapType<string, System.Collections.Generic.Dictionary<map3.L,string>>(@string, new MapType<map3.L, string>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Ls()), @string)), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "ref":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new T instance with default field values </returns>
            public override object make() {
                map3.T rval = new map3.T();
                add(rval);
                return rval;
            }
        
            /// <returns> a new map3.T instance with the argument field values </returns>
            public map3.T make(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> Zref) {
                map3.T rval = new map3.T(-1, Zref);
                add(rval);
                return rval;
            }

            public TBuilder build() {
                return new TBuilder(this, new map3.T());
            }

            /// <summary>
            /// Builder for new T instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class TBuilder : Builder<map3.T> {

                public TBuilder(AbstractStoragePool pool, map3.T instance) : base(pool, instance) {

                }

                public TBuilder Zref(System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> Zref) {
                    instance.Zref = Zref;
                    return this;
                }

                public override map3.T make() {
                    pool.add(instance);
                    map3.T rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<map3.T.SubType, map3.T> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new map3.T.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// map<string,l,string> T.ref
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>, map3.T> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "ref", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type map<string,l,string> in T.ref but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                map3.T[] d = ((P1) owner).Data;
                MapType<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> type = (MapType<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>) this.type.cast<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
            d[i].Zref = castMap<string, System.Collections.Generic.Dictionary<map3.L, string>, System.Object, System.Object>((Dictionary<System.Object, System.Object>)((de.ust.skill.common.csharp.@internal.FieldType)this.type).readSingleField(@in));
                }

            }
            public override void osc(int i, int h) {
                MapType<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> type = (MapType<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>) this.type.cast<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                map3.T[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> v = castMap<string, System.Collections.Generic.Dictionary<map3.L, string>, string, System.Collections.Generic.Dictionary<map3.L, string>>(d[i].Zref);
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

                        System.Collections.Generic.Dictionary<map3.L, string>[] valuesArray = new System.Collections.Generic.Dictionary<map3.L, string>[v.Values.Count];
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
                map3.T[] d = ((P1) owner).Data;
                MapType<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>> type = (MapType<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>) this.type.cast<System.String, System.Collections.Generic.Dictionary<map3.L, System.String>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
                    ((de.ust.skill.common.csharp.@internal.FieldType)this.type).writeSingleField(d[i].Zref, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((map3.T) @ref).Zref;
            }

            public override void set(SkillObject @ref, object value) {
                ((map3.T) @ref).Zref = castMap<string, System.Collections.Generic.Dictionary<map3.L, string>, object, object>((System.Collections.Generic.Dictionary<object, object>)value);
            }
        }

    }
}
