/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = restrictionsCore.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace restrictionsCore
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
                    poolByName.TryGetValue("properties", out p);
                    PropertiessField = (null == p) ? (P0)Parser.newPool("properties", null, types) : (P0) p;
                    poolByName.TryGetValue("system", out p);
                    ZSystemsField = (null == p) ? (P1)Parser.newPool("system", PropertiessField, types) : (P1) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 PropertiessField;

            public override P0 Propertiess() {
                return PropertiessField;
            }
        
            internal readonly P1 ZSystemsField;

            public override P1 ZSystems() {
                return ZSystemsField;
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
                        case "properties":
                            return (superPool = new P0(types.Count));
        

                        case "system":
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

        public sealed class P0 : BasePool<restrictionsCore.Properties> {
        
            protected override restrictionsCore.Properties[] newArray(int size) {
                return new restrictionsCore.Properties[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "properties", noKnownFields, NoAutoFields) {

            }

            internal restrictionsCore.Properties[] Data {
                get
                {
                    return (restrictionsCore.Properties[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsCore.Properties(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Properties instance with default field values </returns>
            public override object make() {
                restrictionsCore.Properties rval = new restrictionsCore.Properties();
                add(rval);
                return rval;
            }
        
            public PropertiesBuilder build() {
                return new PropertiesBuilder(this, new restrictionsCore.Properties());
            }

            /// <summary>
            /// Builder for new Properties instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class PropertiesBuilder : Builder<restrictionsCore.Properties> {

                public PropertiesBuilder(AbstractStoragePool pool, restrictionsCore.Properties instance) : base(pool, instance) {

                }

                public override restrictionsCore.Properties make() {
                    pool.add(instance);
                    restrictionsCore.Properties rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsCore.Properties.SubType, restrictionsCore.Properties> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsCore.Properties.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  some properties of the target system
    /// </summary>
    public sealed class P1 : StoragePool<restrictionsCore.ZSystem, restrictionsCore.Properties> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex, P0 superPool) : base(poolIndex, "system", superPool, new string[] { "name", "version" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new restrictionsCore.ZSystem(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "name":
                    unchecked{new f0(@string, this);}
                    return;

                case "version":
                    unchecked{new f1(F32.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "name":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "version":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new ZSystem instance with default field values </returns>
            public override object make() {
                restrictionsCore.ZSystem rval = new restrictionsCore.ZSystem();
                add(rval);
                return rval;
            }
        
            /// <returns> a new restrictionsCore.ZSystem instance with the argument field values </returns>
            public restrictionsCore.ZSystem make(string name, float version) {
                restrictionsCore.ZSystem rval = new restrictionsCore.ZSystem(-1, name, version);
                add(rval);
                return rval;
            }

            public ZSystemBuilder build() {
                return new ZSystemBuilder(this, new restrictionsCore.ZSystem());
            }

            /// <summary>
            /// Builder for new ZSystem instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ZSystemBuilder : Builder<restrictionsCore.ZSystem> {

                public ZSystemBuilder(AbstractStoragePool pool, restrictionsCore.ZSystem instance) : base(pool, instance) {

                }

                public ZSystemBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public ZSystemBuilder version(float version) {
                    instance.version = version;
                    return this;
                }

                public override restrictionsCore.ZSystem make() {
                    pool.add(instance);
                    restrictionsCore.ZSystem rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<restrictionsCore.ZSystem.SubType, restrictionsCore.Properties> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new restrictionsCore.ZSystem.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// string System.name
        /// </summary>
        internal sealed class f0 : KnownDataField<System.String, restrictionsCore.ZSystem> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "name", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in System.name but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsCore.Properties[] d = ((P0) owner.basePool).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            ((restrictionsCore.ZSystem)d[i]).name = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                restrictionsCore.Properties[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = ((restrictionsCore.ZSystem)d[i]).name;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsCore.Properties[] d = ((P0) owner.basePool).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(((restrictionsCore.ZSystem)d[i]).name, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsCore.ZSystem) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsCore.ZSystem) @ref).name = (System.String)value;
            }
        }

        /// <summary>
        /// f32 System.version
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Single, restrictionsCore.ZSystem> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "version", owner) {
                
                if (type.TypeID != 12)
                    throw new SkillException("Expected field type f32 in System.version but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                restrictionsCore.Properties[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
            ((restrictionsCore.ZSystem)d[i]).version = @in.f32();
                }

            }
            public override void osc(int i, int h) {offset += (h-i) << 2;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                restrictionsCore.Properties[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
                    @out.f32(((restrictionsCore.ZSystem)d[i]).version);
                }

            }


            public override object get(SkillObject @ref) {
                return ((restrictionsCore.ZSystem) @ref).version;
            }

            public override void set(SkillObject @ref, object value) {
                ((restrictionsCore.ZSystem) @ref).version = (System.Single)value;
            }
        }

    }
}
