/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = escaping.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace escaping
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
                    poolByName.TryGetValue("boolean", out p);
                    ZBooleansField = (null == p) ? (P0)Parser.newPool("boolean", null, types) : (P0) p;
                    poolByName.TryGetValue("if", out p);
                    IfsField = (null == p) ? (P1)Parser.newPool("if", null, types) : (P1) p;
                    poolByName.TryGetValue("int", out p);
                    IntsField = (null == p) ? (P2)Parser.newPool("int", null, types) : (P2) p;
                    poolByName.TryGetValue("∀", out p);
                    Z2200sField = (null == p) ? (P3)Parser.newPool("∀", null, types) : (P3) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 ZBooleansField;

            public override P0 ZBooleans() {
                return ZBooleansField;
            }
        
            internal readonly P1 IfsField;

            public override P1 Ifs() {
                return IfsField;
            }
        
            internal readonly P2 IntsField;

            public override P2 Ints() {
                return IntsField;
            }
        
            internal readonly P3 Z2200sField;

            public override P3 Z2200s() {
                return Z2200sField;
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
                        case "boolean":
                            return (superPool = new P0(types.Count));
        

                        case "if":
                            return (superPool = new P1(types.Count));
        

                        case "int":
                            return (superPool = new P2(types.Count));
        

                        case "∀":
                            return (superPool = new P3(types.Count));
        
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
    ///  Representation of another type.
    ///  @note  Caused by a Bug in the C generator.
    /// </summary>
    public sealed class P0 : BasePool<escaping.ZBoolean> {
        
            protected override escaping.ZBoolean[] newArray(int size) {
                return new escaping.ZBoolean[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "boolean", new string[] { "bool", "boolean" }, NoAutoFields) {

            }

            internal escaping.ZBoolean[] Data {
                get
                {
                    return (escaping.ZBoolean[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new escaping.ZBoolean(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "bool":
                    unchecked{new f0((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).ZBooleans()), this);}
                    return;

                case "boolean":
                    unchecked{new f1(BoolType.get(), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "bool":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "boolean":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new ZBoolean instance with default field values </returns>
            public override object make() {
                escaping.ZBoolean rval = new escaping.ZBoolean();
                add(rval);
                return rval;
            }
        
            /// <returns> a new escaping.ZBoolean instance with the argument field values </returns>
            public escaping.ZBoolean make(escaping.ZBoolean Zbool, bool boolean) {
                escaping.ZBoolean rval = new escaping.ZBoolean(-1, Zbool, boolean);
                add(rval);
                return rval;
            }

            public ZBooleanBuilder build() {
                return new ZBooleanBuilder(this, new escaping.ZBoolean());
            }

            /// <summary>
            /// Builder for new ZBoolean instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ZBooleanBuilder : Builder<escaping.ZBoolean> {

                public ZBooleanBuilder(AbstractStoragePool pool, escaping.ZBoolean instance) : base(pool, instance) {

                }

                public ZBooleanBuilder Zbool(escaping.ZBoolean Zbool) {
                    instance.Zbool = Zbool;
                    return this;
                }

                public ZBooleanBuilder boolean(bool boolean) {
                    instance.boolean = boolean;
                    return this;
                }

                public override escaping.ZBoolean make() {
                    pool.add(instance);
                    escaping.ZBoolean rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<escaping.ZBoolean.SubType, escaping.ZBoolean> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new escaping.ZBoolean.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Another stupid typename
    /// </summary>
    public sealed class P1 : BasePool<escaping.If> {
        
            protected override escaping.If[] newArray(int size) {
                return new escaping.If[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "if", noKnownFields, NoAutoFields) {

            }

            internal escaping.If[] Data {
                get
                {
                    return (escaping.If[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new escaping.If(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new If instance with default field values </returns>
            public override object make() {
                escaping.If rval = new escaping.If();
                add(rval);
                return rval;
            }
        
            public IfBuilder build() {
                return new IfBuilder(this, new escaping.If());
            }

            /// <summary>
            /// Builder for new If instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class IfBuilder : Builder<escaping.If> {

                public IfBuilder(AbstractStoragePool pool, escaping.If instance) : base(pool, instance) {

                }

                public override escaping.If make() {
                    pool.add(instance);
                    escaping.If rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<escaping.If.SubType, escaping.If> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new escaping.If.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  Stupid typename
    /// </summary>
    public sealed class P2 : BasePool<escaping.Int> {
        
            protected override escaping.Int[] newArray(int size) {
                return new escaping.Int[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex) : base(poolIndex, "int", new string[] { "for", "if" }, NoAutoFields) {

            }

            internal escaping.Int[] Data {
                get
                {
                    return (escaping.Int[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new escaping.Int(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "for":
                    unchecked{new f2((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Ifs()), this);}
                    return;

                case "if":
                    unchecked{new f3((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Ints()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "for":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "if":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Int instance with default field values </returns>
            public override object make() {
                escaping.Int rval = new escaping.Int();
                add(rval);
                return rval;
            }
        
            /// <returns> a new escaping.Int instance with the argument field values </returns>
            public escaping.Int make(escaping.If Zfor, escaping.Int Zif) {
                escaping.Int rval = new escaping.Int(-1, Zfor, Zif);
                add(rval);
                return rval;
            }

            public IntBuilder build() {
                return new IntBuilder(this, new escaping.Int());
            }

            /// <summary>
            /// Builder for new Int instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class IntBuilder : Builder<escaping.Int> {

                public IntBuilder(AbstractStoragePool pool, escaping.Int instance) : base(pool, instance) {

                }

                public IntBuilder Zfor(escaping.If Zfor) {
                    instance.Zfor = Zfor;
                    return this;
                }

                public IntBuilder Zif(escaping.Int Zif) {
                    instance.Zif = Zif;
                    return this;
                }

                public override escaping.Int make() {
                    pool.add(instance);
                    escaping.Int rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<escaping.Int.SubType, escaping.Int> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new escaping.Int.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  non-printable unicode characters
    /// </summary>
    public sealed class P3 : BasePool<escaping.Z2200> {
        
            protected override escaping.Z2200[] newArray(int size) {
                return new escaping.Z2200[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex) : base(poolIndex, "∀", new string[] { "€", "☢" }, NoAutoFields) {

            }

            internal escaping.Z2200[] Data {
                get
                {
                    return (escaping.Z2200[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new escaping.Z2200(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "€":
                    unchecked{new f4((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Z2200s()), this);}
                    return;

                case "☢":
                    unchecked{new f5(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "€":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "☢":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Z2200 instance with default field values </returns>
            public override object make() {
                escaping.Z2200 rval = new escaping.Z2200();
                add(rval);
                return rval;
            }
        
            /// <returns> a new escaping.Z2200 instance with the argument field values </returns>
            public escaping.Z2200 make(escaping.Z2200 Z20ac, string Z2622) {
                escaping.Z2200 rval = new escaping.Z2200(-1, Z20ac, Z2622);
                add(rval);
                return rval;
            }

            public Z2200Builder build() {
                return new Z2200Builder(this, new escaping.Z2200());
            }

            /// <summary>
            /// Builder for new Z2200 instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class Z2200Builder : Builder<escaping.Z2200> {

                public Z2200Builder(AbstractStoragePool pool, escaping.Z2200 instance) : base(pool, instance) {

                }

                public Z2200Builder Z20ac(escaping.Z2200 Z20ac) {
                    instance.Z20ac = Z20ac;
                    return this;
                }

                public Z2200Builder Z2622(string Z2622) {
                    instance.Z2622 = Z2622;
                    return this;
                }

                public override escaping.Z2200 make() {
                    pool.add(instance);
                    escaping.Z2200 rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<escaping.Z2200.SubType, escaping.Z2200> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new escaping.Z2200.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// boolean Boolean.bool
        /// </summary>
        internal sealed class f0 : KnownDataField<escaping.ZBoolean, escaping.ZBoolean> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "bool", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("boolean"))
                    throw new SkillException("Expected field type boolean in Boolean.bool but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                escaping.ZBoolean[] d = ((P0) owner).Data;
                P0 t = ((P0)(object)this.type);
                for (; i != h; i++) {
            d[i].Zbool = (escaping.ZBoolean)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                escaping.ZBoolean[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    escaping.ZBoolean instance = d[i].Zbool;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                escaping.ZBoolean[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    escaping.ZBoolean v = d[i].Zbool;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((escaping.ZBoolean) @ref).Zbool;
            }

            public override void set(SkillObject @ref, object value) {
                ((escaping.ZBoolean) @ref).Zbool = (escaping.ZBoolean)value;
            }
        }

        /// <summary>
        /// bool Boolean.boolean
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Boolean, escaping.ZBoolean> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "boolean", owner) {
                
                if (type.TypeID != 6)
                    throw new SkillException("Expected field type bool in Boolean.boolean but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                escaping.ZBoolean[] d = ((P0) owner).Data;
                for (; i != h; i++) {
            d[i].boolean = @in.@bool();
                }

            }
            public override void osc(int i, int h) {offset += (h-i);
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                escaping.ZBoolean[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    @out.@bool(d[i].boolean);
                }

            }


            public override object get(SkillObject @ref) {
                return ((escaping.ZBoolean) @ref).boolean;
            }

            public override void set(SkillObject @ref, object value) {
                ((escaping.ZBoolean) @ref).boolean = (System.Boolean)value;
            }
        }

        /// <summary>
        /// if Int.for
        /// </summary>
        internal sealed class f2 : KnownDataField<escaping.If, escaping.Int> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "for", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("if"))
                    throw new SkillException("Expected field type if in Int.for but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                escaping.Int[] d = ((P2) owner).Data;
                P1 t = ((P1)(object)this.type);
                for (; i != h; i++) {
            d[i].Zfor = (escaping.If)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                escaping.Int[] d = ((P2) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    escaping.If instance = d[i].Zfor;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                escaping.Int[] d = ((P2) owner).Data;
                for (; i != h; i++) {
                    escaping.If v = d[i].Zfor;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((escaping.Int) @ref).Zfor;
            }

            public override void set(SkillObject @ref, object value) {
                ((escaping.Int) @ref).Zfor = (escaping.If)value;
            }
        }

        /// <summary>
        /// int Int.if
        /// </summary>
        internal sealed class f3 : KnownDataField<escaping.Int, escaping.Int> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "if", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("int"))
                    throw new SkillException("Expected field type int in Int.if but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                escaping.Int[] d = ((P2) owner).Data;
                P2 t = ((P2)(object)this.type);
                for (; i != h; i++) {
            d[i].Zif = (escaping.Int)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                escaping.Int[] d = ((P2) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    escaping.Int instance = d[i].Zif;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                escaping.Int[] d = ((P2) owner).Data;
                for (; i != h; i++) {
                    escaping.Int v = d[i].Zif;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((escaping.Int) @ref).Zif;
            }

            public override void set(SkillObject @ref, object value) {
                ((escaping.Int) @ref).Zif = (escaping.Int)value;
            }
        }

        /// <summary>
        /// ∀ ∀.€
        /// </summary>
        internal sealed class f4 : KnownDataField<escaping.Z2200, escaping.Z2200> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "€", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("∀"))
                    throw new SkillException("Expected field type ∀ in ∀.€ but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                escaping.Z2200[] d = ((P3) owner).Data;
                P3 t = ((P3)(object)this.type);
                for (; i != h; i++) {
            d[i].Z20ac = (escaping.Z2200)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                escaping.Z2200[] d = ((P3) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    escaping.Z2200 instance = d[i].Z20ac;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                escaping.Z2200[] d = ((P3) owner).Data;
                for (; i != h; i++) {
                    escaping.Z2200 v = d[i].Z20ac;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((escaping.Z2200) @ref).Z20ac;
            }

            public override void set(SkillObject @ref, object value) {
                ((escaping.Z2200) @ref).Z20ac = (escaping.Z2200)value;
            }
        }

        /// <summary>
        /// string ∀.☢
        /// </summary>
        internal sealed class f5 : KnownDataField<System.String, escaping.Z2200> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "☢", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in ∀.☢ but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                escaping.Z2200[] d = ((P3) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].Z2622 = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                escaping.Z2200[] d = ((P3) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].Z2622;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                escaping.Z2200[] d = ((P3) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].Z2622, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((escaping.Z2200) @ref).Z2622;
            }

            public override void set(SkillObject @ref, object value) {
                ((escaping.Z2200) @ref).Z2622 = (System.String)value;
            }
        }

    }
}
