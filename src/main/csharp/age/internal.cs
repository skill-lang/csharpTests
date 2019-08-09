/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = age.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace age
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
                    poolByName.TryGetValue("age", out p);
                    AgesField = (null == p) ? (P0)Parser.newPool("age", null, types) : (P0) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 AgesField;

            public override P0 Ages() {
                return AgesField;
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
                        case "age":
                            return (superPool = new P0(types.Count));
        
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
    ///  The age of a person.
    ///  @author  Timm Felden
    /// </summary>
    public sealed class P0 : BasePool<age.Age> {
        
            protected override age.Age[] newArray(int size) {
                return new age.Age[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "age", new string[] { "age" }, NoAutoFields) {

            }

            internal age.Age[] Data {
                get
                {
                    return (age.Age[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new age.Age(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "age":
                    unchecked{new f0(V64.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "age":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Age instance with default field values </returns>
            public override object make() {
                age.Age rval = new age.Age();
                add(rval);
                return rval;
            }
        
            /// <returns> a new age.Age instance with the argument field values </returns>
            public age.Age make(long age) {
                age.Age rval = new age.Age(-1, age);
                add(rval);
                return rval;
            }

            public AgeBuilder build() {
                return new AgeBuilder(this, new age.Age());
            }

            /// <summary>
            /// Builder for new Age instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class AgeBuilder : Builder<age.Age> {

                public AgeBuilder(AbstractStoragePool pool, age.Age instance) : base(pool, instance) {

                }

                public AgeBuilder age(long age) {
                    instance.age = age;
                    return this;
                }

                public override age.Age make() {
                    pool.add(instance);
                    age.Age rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<age.Age.SubType, age.Age> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new age.Age.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// v64 Age.age
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Int64, age.Age> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "age", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in Age.age but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                age.Age[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].age = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                age.Age[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].age);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                age.Age[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].age);
                }

            }


            public override object get(SkillObject @ref) {
                return ((age.Age) @ref).age;
            }

            public override void set(SkillObject @ref, object value) {
                ((age.Age) @ref).age = (System.Int64)value;
            }
        }

    }
}
