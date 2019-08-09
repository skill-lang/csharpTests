/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = user.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace user
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
                    poolByName.TryGetValue("user", out p);
                    UsersField = (null == p) ? (P0)Parser.newPool("user", null, types) : (P0) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 UsersField;

            public override P0 Users() {
                return UsersField;
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
                        case "user":
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
    ///  A user has a name and an age.
    /// </summary>
    public sealed class P0 : BasePool<user.User> {
        
            protected override user.User[] newArray(int size) {
                return new user.User[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "user", new string[] { "age", "name" }, NoAutoFields) {

            }

            internal user.User[] Data {
                get
                {
                    return (user.User[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new user.User(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "age":
                    unchecked{new f0(V64.get(), this);}
                    return;

                case "name":
                    unchecked{new f1(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "age":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "name":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new User instance with default field values </returns>
            public override object make() {
                user.User rval = new user.User();
                add(rval);
                return rval;
            }
        
            /// <returns> a new user.User instance with the argument field values </returns>
            public user.User make(long age, string name) {
                user.User rval = new user.User(-1, age, name);
                add(rval);
                return rval;
            }

            public UserBuilder build() {
                return new UserBuilder(this, new user.User());
            }

            /// <summary>
            /// Builder for new User instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class UserBuilder : Builder<user.User> {

                public UserBuilder(AbstractStoragePool pool, user.User instance) : base(pool, instance) {

                }

                public UserBuilder age(long age) {
                    instance.age = age;
                    return this;
                }

                public UserBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public override user.User make() {
                    pool.add(instance);
                    user.User rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<user.User.SubType, user.User> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new user.User.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// v64 User.age
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Int64, user.User> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "age", owner) {
                
                if (type.TypeID != 11)
                    throw new SkillException("Expected field type v64 in User.age but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                user.User[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].age = @in.v64();
                }

            }
            public override void osc(int i, int h) {
                user.User[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    result += V64.singleV64Offset(d[i].age);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                user.User[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.v64(d[i].age);
                }

            }


            public override object get(SkillObject @ref) {
                return ((user.User) @ref).age;
            }

            public override void set(SkillObject @ref, object value) {
                ((user.User) @ref).age = (System.Int64)value;
            }
        }

        /// <summary>
        /// string User.name
        /// </summary>
        internal sealed class f1 : KnownDataField<System.String, user.User> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "name", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in User.name but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                user.User[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].name = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                user.User[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].name;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                user.User[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].name, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((user.User) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((user.User) @ref).name = (System.String)value;
            }
        }

    }
}
