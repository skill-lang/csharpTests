/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = unknown.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace unknown
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
                    poolByName.TryGetValue("c", out p);
                    CsField = (null == p) ? (P1)Parser.newPool("c", AsField, types) : (P1) p;
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
        
            internal readonly P1 CsField;

            public override P1 Cs() {
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
        

                        case "c":
                            return (superPool = new P1(types.Count, (P0)superPool));

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

        public sealed class P0 : BasePool<unknown.A> {
        
            protected override unknown.A[] newArray(int size) {
                return new unknown.A[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "a", new string[] { "a" }, NoAutoFields) {

            }

            internal unknown.A[] Data {
                get
                {
                    return (unknown.A[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new unknown.A(i + 1);
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
                unknown.A rval = new unknown.A();
                add(rval);
                return rval;
            }
        
            /// <returns> a new unknown.A instance with the argument field values </returns>
            public unknown.A make(unknown.A a) {
                unknown.A rval = new unknown.A(-1, a);
                add(rval);
                return rval;
            }

            public ABuilder build() {
                return new ABuilder(this, new unknown.A());
            }

            /// <summary>
            /// Builder for new A instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ABuilder : Builder<unknown.A> {

                public ABuilder(AbstractStoragePool pool, unknown.A instance) : base(pool, instance) {

                }

                public ABuilder a(unknown.A a) {
                    instance.a = a;
                    return this;
                }

                public override unknown.A make() {
                    pool.add(instance);
                    unknown.A rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<unknown.A.SubType, unknown.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new unknown.A.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : StoragePool<unknown.C, unknown.A> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex, P0 superPool) : base(poolIndex, "c", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new unknown.C(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new C instance with default field values </returns>
            public override object make() {
                unknown.C rval = new unknown.C();
                add(rval);
                return rval;
            }
        
            public CBuilder build() {
                return new CBuilder(this, new unknown.C());
            }

            /// <summary>
            /// Builder for new C instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class CBuilder : Builder<unknown.C> {

                public CBuilder(AbstractStoragePool pool, unknown.C instance) : base(pool, instance) {

                }

                public CBuilder a(unknown.A a) {
                    instance.a = a;
                    return this;
                }

                public override unknown.C make() {
                    pool.add(instance);
                    unknown.C rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<unknown.C.SubType, unknown.A> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new unknown.C.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// a A.a
        /// </summary>
        internal sealed class f0 : KnownDataField<unknown.A, unknown.A> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "a", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("a"))
                    throw new SkillException("Expected field type a in A.a but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                unknown.A[] d = ((P0) owner).Data;
                P0 t = ((P0)(object)this.type);
                for (; i != h; i++) {
            d[i].a = (unknown.A)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                unknown.A[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    unknown.A instance = d[i].a;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                unknown.A[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    unknown.A v = d[i].a;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((unknown.A) @ref).a;
            }

            public override void set(SkillObject @ref, object value) {
                ((unknown.A) @ref).a = (unknown.A)value;
            }
        }

    }
}
