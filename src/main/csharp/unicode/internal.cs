/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = unicode.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace unicode
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
                    poolByName.TryGetValue("unicode", out p);
                    UnicodesField = (null == p) ? (P0)Parser.newPool("unicode", null, types) : (P0) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 UnicodesField;

            public override P0 Unicodes() {
                return UnicodesField;
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
                        case "unicode":
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
    ///  this test is used to check unicode handling inside of strings; only one instance but no
    ///  @singleton  to keep things simple; all fields contain one character.
    /// </summary>
    public sealed class P0 : BasePool<unicode.Unicode> {
        
            protected override unicode.Unicode[] newArray(int size) {
                return new unicode.Unicode[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "unicode", new string[] { "one", "three", "two" }, NoAutoFields) {

            }

            internal unicode.Unicode[] Data {
                get
                {
                    return (unicode.Unicode[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new unicode.Unicode(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "one":
                    unchecked{new f0(@string, this);}
                    return;

                case "three":
                    unchecked{new f1(@string, this);}
                    return;

                case "two":
                    unchecked{new f2(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "one":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "three":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "two":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Unicode instance with default field values </returns>
            public override object make() {
                unicode.Unicode rval = new unicode.Unicode();
                add(rval);
                return rval;
            }
        
            /// <returns> a new unicode.Unicode instance with the argument field values </returns>
            public unicode.Unicode make(string one, string three, string two) {
                unicode.Unicode rval = new unicode.Unicode(-1, one, three, two);
                add(rval);
                return rval;
            }

            public UnicodeBuilder build() {
                return new UnicodeBuilder(this, new unicode.Unicode());
            }

            /// <summary>
            /// Builder for new Unicode instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class UnicodeBuilder : Builder<unicode.Unicode> {

                public UnicodeBuilder(AbstractStoragePool pool, unicode.Unicode instance) : base(pool, instance) {

                }

                public UnicodeBuilder one(string one) {
                    instance.one = one;
                    return this;
                }

                public UnicodeBuilder three(string three) {
                    instance.three = three;
                    return this;
                }

                public UnicodeBuilder two(string two) {
                    instance.two = two;
                    return this;
                }

                public override unicode.Unicode make() {
                    pool.add(instance);
                    unicode.Unicode rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<unicode.Unicode.SubType, unicode.Unicode> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new unicode.Unicode.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// string Unicode.one
        /// </summary>
        internal sealed class f0 : KnownDataField<System.String, unicode.Unicode> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "one", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Unicode.one but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                unicode.Unicode[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].one = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                unicode.Unicode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].one;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                unicode.Unicode[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].one, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((unicode.Unicode) @ref).one;
            }

            public override void set(SkillObject @ref, object value) {
                ((unicode.Unicode) @ref).one = (System.String)value;
            }
        }

        /// <summary>
        /// string Unicode.three
        /// </summary>
        internal sealed class f1 : KnownDataField<System.String, unicode.Unicode> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "three", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Unicode.three but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                unicode.Unicode[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].three = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                unicode.Unicode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].three;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                unicode.Unicode[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].three, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((unicode.Unicode) @ref).three;
            }

            public override void set(SkillObject @ref, object value) {
                ((unicode.Unicode) @ref).three = (System.String)value;
            }
        }

        /// <summary>
        /// string Unicode.two
        /// </summary>
        internal sealed class f2 : KnownDataField<System.String, unicode.Unicode> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "two", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Unicode.two but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                unicode.Unicode[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].two = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                unicode.Unicode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].two;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                unicode.Unicode[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].two, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((unicode.Unicode) @ref).two;
            }

            public override void set(SkillObject @ref, object value) {
                ((unicode.Unicode) @ref).two = (System.String)value;
            }
        }

    }
}
