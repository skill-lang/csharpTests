/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = constants.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace constants
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
                    poolByName.TryGetValue("constant", out p);
                    ConstantsField = (null == p) ? (P0)Parser.newPool("constant", null, types) : (P0) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 ConstantsField;

            public override P0 Constants() {
                return ConstantsField;
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
                        case "constant":
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
    ///  Check for constant integerers.
    ///  @author  Dennis Przytarski
    /// </summary>
    public sealed class P0 : BasePool<constants.Constant> {
        
            protected override constants.Constant[] newArray(int size) {
                return new constants.Constant[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "constant", new string[] { "a", "b", "c", "d", "e" }, NoAutoFields) {

            }

            internal constants.Constant[] Data {
                get
                {
                    return (constants.Constant[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new constants.Constant(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "a":
                    unchecked{new f0(new ConstantI8((sbyte)8), this);}
                    return;

                case "b":
                    unchecked{new f1(new ConstantI16((short)16), this);}
                    return;

                case "c":
                    unchecked{new f2(new ConstantI32(32), this);}
                    return;

                case "d":
                    unchecked{new f3(new ConstantI64(64L), this);}
                    return;

                case "e":
                    unchecked{new f4(new ConstantV64(46L), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "a":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "b":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "c":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "d":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "e":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Constant instance with default field values </returns>
            public override object make() {
                constants.Constant rval = new constants.Constant();
                add(rval);
                return rval;
            }
        
            public ConstantBuilder build() {
                return new ConstantBuilder(this, new constants.Constant());
            }

            /// <summary>
            /// Builder for new Constant instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ConstantBuilder : Builder<constants.Constant> {

                public ConstantBuilder(AbstractStoragePool pool, constants.Constant instance) : base(pool, instance) {

                }

                public override constants.Constant make() {
                    pool.add(instance);
                    constants.Constant rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<constants.Constant.SubType, constants.Constant> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new constants.Constant.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// i8 Constant.a
        /// </summary>
        internal sealed class f0 : KnownDataField<System.SByte, constants.Constant> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "a", owner) {
                
                if (type.TypeID != 0)
                    throw new SkillException("Expected field type i8 in Constant.a but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
            }
            public override void osc(int i, int h) {
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
            }


            public override object get(SkillObject @ref) {
                return constants.Constant.a;
            }

            public override void set(SkillObject @ref, object value) {
                throw new Exception("a is a constant!");
            }
        }

        /// <summary>
        /// i16 Constant.b
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Int16, constants.Constant> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "b", owner) {
                
                if (type.TypeID != 1)
                    throw new SkillException("Expected field type i16 in Constant.b but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
            }
            public override void osc(int i, int h) {
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
            }


            public override object get(SkillObject @ref) {
                return constants.Constant.b;
            }

            public override void set(SkillObject @ref, object value) {
                throw new Exception("b is a constant!");
            }
        }

        /// <summary>
        /// i32 Constant.c
        /// </summary>
        internal sealed class f2 : KnownDataField<System.Int32, constants.Constant> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "c", owner) {
                
                if (type.TypeID != 2)
                    throw new SkillException("Expected field type i32 in Constant.c but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
            }
            public override void osc(int i, int h) {
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
            }


            public override object get(SkillObject @ref) {
                return constants.Constant.c;
            }

            public override void set(SkillObject @ref, object value) {
                throw new Exception("c is a constant!");
            }
        }

        /// <summary>
        /// i64 Constant.d
        /// </summary>
        internal sealed class f3 : KnownDataField<System.Int64, constants.Constant> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "d", owner) {
                
                if (type.TypeID != 3)
                    throw new SkillException("Expected field type i64 in Constant.d but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
            }
            public override void osc(int i, int h) {
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
            }


            public override object get(SkillObject @ref) {
                return constants.Constant.d;
            }

            public override void set(SkillObject @ref, object value) {
                throw new Exception("d is a constant!");
            }
        }

        /// <summary>
        /// v64 Constant.e
        /// </summary>
        internal sealed class f4 : KnownDataField<System.Int64, constants.Constant> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "e", owner) {
                
                if (type.TypeID != 4)
                    throw new SkillException("Expected field type v64 in Constant.e but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
            }
            public override void osc(int i, int h) {
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
            }


            public override object get(SkillObject @ref) {
                return constants.Constant.e;
            }

            public override void set(SkillObject @ref, object value) {
                throw new Exception("e is a constant!");
            }
        }

    }
}
