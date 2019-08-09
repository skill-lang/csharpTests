/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = enums.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace enums
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
                    poolByName.TryGetValue("testenum", out p);
                    TestEnumsField = (null == p) ? (P0)Parser.newPool("testenum", null, types) : (P0) p;
                    poolByName.TryGetValue("testenum:default", out p);
                    Testenum_defaultsField = (null == p) ? (P1)Parser.newPool("testenum:default", TestEnumsField, types) : (P1) p;
                    poolByName.TryGetValue("testenum:second", out p);
                    Testenum_secondsField = (null == p) ? (P2)Parser.newPool("testenum:second", TestEnumsField, types) : (P2) p;
                    poolByName.TryGetValue("testenum:third", out p);
                    Testenum_thirdsField = (null == p) ? (P3)Parser.newPool("testenum:third", TestEnumsField, types) : (P3) p;
                    poolByName.TryGetValue("testenum:last", out p);
                    Testenum_lastsField = (null == p) ? (P4)Parser.newPool("testenum:last", TestEnumsField, types) : (P4) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 TestEnumsField;

            public override P0 TestEnums() {
                return TestEnumsField;
            }
        
            internal readonly P1 Testenum_defaultsField;

            public override P1 Testenum_defaults() {
                return Testenum_defaultsField;
            }
        
            internal readonly P2 Testenum_secondsField;

            public override P2 Testenum_seconds() {
                return Testenum_secondsField;
            }
        
            internal readonly P3 Testenum_thirdsField;

            public override P3 Testenum_thirds() {
                return Testenum_thirdsField;
            }
        
            internal readonly P4 Testenum_lastsField;

            public override P4 Testenum_lasts() {
                return Testenum_lastsField;
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
                        case "testenum":
                            return (superPool = new P0(types.Count));
        

                        case "testenum:default":
                            return (superPool = new P1(types.Count, (P0)superPool));


                        case "testenum:second":
                            return (superPool = new P2(types.Count, (P0)superPool));


                        case "testenum:third":
                            return (superPool = new P3(types.Count, (P0)superPool));


                        case "testenum:last":
                            return (superPool = new P4(types.Count, (P0)superPool));

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
    ///  Test of mapping of enums.
    ///  @author  Timm Felden
    /// </summary>
    public sealed class P0 : BasePool<enums.TestEnum> {
        
            protected override enums.TestEnum[] newArray(int size) {
                return new enums.TestEnum[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "testenum", new string[] { "name", "next" }, new IAutoField[1]) {

            }

            internal enums.TestEnum[] Data {
                get
                {
                    return (enums.TestEnum[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new enums.TestEnum(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "name":
                    unchecked{new f0(@string, this);}
                    return;

                case "next":
                    unchecked{new f1((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).TestEnums()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "next":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "name":
                    throw new SkillException(String.Format(
                            "The file contains a field declaration %s.%s, but there is an auto field of similar name!",
                            this.Name, name));

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new TestEnum instance with default field values </returns>
            public override object make() {
                enums.TestEnum rval = new enums.TestEnum();
                add(rval);
                return rval;
            }
        
            /// <returns> a new enums.TestEnum instance with the argument field values </returns>
            public enums.TestEnum make(string name, enums.TestEnum next) {
                enums.TestEnum rval = new enums.TestEnum(-1, name, next);
                add(rval);
                return rval;
            }

            public TestEnumBuilder build() {
                return new TestEnumBuilder(this, new enums.TestEnum());
            }

            /// <summary>
            /// Builder for new TestEnum instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class TestEnumBuilder : Builder<enums.TestEnum> {

                public TestEnumBuilder(AbstractStoragePool pool, enums.TestEnum instance) : base(pool, instance) {

                }

                public TestEnumBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public TestEnumBuilder next(enums.TestEnum next) {
                    instance.next = next;
                    return this;
                }

                public override enums.TestEnum make() {
                    pool.add(instance);
                    enums.TestEnum rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<enums.TestEnum.SubType, enums.TestEnum> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new enums.TestEnum.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P1 : StoragePool<enums.Testenum_default, enums.TestEnum> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex, P0 superPool) : base(poolIndex, "testenum:default", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new enums.Testenum_default(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Testenum_default instance with default field values </returns>
            public override object make() {
                enums.Testenum_default rval = new enums.Testenum_default();
                add(rval);
                return rval;
            }
        
            public Testenum_defaultBuilder build() {
                return new Testenum_defaultBuilder(this, new enums.Testenum_default());
            }

            /// <summary>
            /// Builder for new Testenum_default instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class Testenum_defaultBuilder : Builder<enums.Testenum_default> {

                public Testenum_defaultBuilder(AbstractStoragePool pool, enums.Testenum_default instance) : base(pool, instance) {

                }

                public Testenum_defaultBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public Testenum_defaultBuilder next(enums.TestEnum next) {
                    instance.next = next;
                    return this;
                }

                public override enums.Testenum_default make() {
                    pool.add(instance);
                    enums.Testenum_default rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<enums.Testenum_default.SubType, enums.TestEnum> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new enums.Testenum_default.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P2 : StoragePool<enums.Testenum_second, enums.TestEnum> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex, P0 superPool) : base(poolIndex, "testenum:second", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new enums.Testenum_second(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Testenum_second instance with default field values </returns>
            public override object make() {
                enums.Testenum_second rval = new enums.Testenum_second();
                add(rval);
                return rval;
            }
        
            public Testenum_secondBuilder build() {
                return new Testenum_secondBuilder(this, new enums.Testenum_second());
            }

            /// <summary>
            /// Builder for new Testenum_second instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class Testenum_secondBuilder : Builder<enums.Testenum_second> {

                public Testenum_secondBuilder(AbstractStoragePool pool, enums.Testenum_second instance) : base(pool, instance) {

                }

                public Testenum_secondBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public Testenum_secondBuilder next(enums.TestEnum next) {
                    instance.next = next;
                    return this;
                }

                public override enums.Testenum_second make() {
                    pool.add(instance);
                    enums.Testenum_second rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<enums.Testenum_second.SubType, enums.TestEnum> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new enums.Testenum_second.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P3 : StoragePool<enums.Testenum_third, enums.TestEnum> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex, P0 superPool) : base(poolIndex, "testenum:third", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new enums.Testenum_third(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Testenum_third instance with default field values </returns>
            public override object make() {
                enums.Testenum_third rval = new enums.Testenum_third();
                add(rval);
                return rval;
            }
        
            public Testenum_thirdBuilder build() {
                return new Testenum_thirdBuilder(this, new enums.Testenum_third());
            }

            /// <summary>
            /// Builder for new Testenum_third instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class Testenum_thirdBuilder : Builder<enums.Testenum_third> {

                public Testenum_thirdBuilder(AbstractStoragePool pool, enums.Testenum_third instance) : base(pool, instance) {

                }

                public Testenum_thirdBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public Testenum_thirdBuilder next(enums.TestEnum next) {
                    instance.next = next;
                    return this;
                }

                public override enums.Testenum_third make() {
                    pool.add(instance);
                    enums.Testenum_third rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<enums.Testenum_third.SubType, enums.TestEnum> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new enums.Testenum_third.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        public sealed class P4 : StoragePool<enums.Testenum_last, enums.TestEnum> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P4(int poolIndex, P0 superPool) : base(poolIndex, "testenum:last", superPool, noKnownFields, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new enums.Testenum_last(i + 1);
                    i += 1;
                }
            }
        

            /// <returns> a new Testenum_last instance with default field values </returns>
            public override object make() {
                enums.Testenum_last rval = new enums.Testenum_last();
                add(rval);
                return rval;
            }
        
            public Testenum_lastBuilder build() {
                return new Testenum_lastBuilder(this, new enums.Testenum_last());
            }

            /// <summary>
            /// Builder for new Testenum_last instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class Testenum_lastBuilder : Builder<enums.Testenum_last> {

                public Testenum_lastBuilder(AbstractStoragePool pool, enums.Testenum_last instance) : base(pool, instance) {

                }

                public Testenum_lastBuilder name(string name) {
                    instance.name = name;
                    return this;
                }

                public Testenum_lastBuilder next(enums.TestEnum next) {
                    instance.next = next;
                    return this;
                }

                public override enums.Testenum_last make() {
                    pool.add(instance);
                    enums.Testenum_last rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<enums.Testenum_last.SubType, enums.TestEnum> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new enums.Testenum_last.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// string TestEnum.name
        /// </summary>
        internal sealed class f0 : AutoField<System.String, enums.TestEnum> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "name", 0, owner) {
                
                // TODO insert known restrictions?
            }
        

            public override object get(SkillObject @ref) {
                return ((enums.TestEnum) @ref).name;
            }

            public override void set(SkillObject @ref, object value) {
                ((enums.TestEnum) @ref).name = (System.String)value;
            }
        }

        /// <summary>
        /// testenum TestEnum.next
        /// </summary>
        internal sealed class f1 : KnownDataField<enums.TestEnum, enums.TestEnum> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "next", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("testenum"))
                    throw new SkillException("Expected field type testenum in TestEnum.next but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                enums.TestEnum[] d = ((P0) owner).Data;
                P0 t = ((P0)(object)this.type);
                for (; i != h; i++) {
            d[i].next = (enums.TestEnum)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                enums.TestEnum[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    enums.TestEnum instance = d[i].next;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                enums.TestEnum[] d = ((P0) owner).Data;
                for (; i != h; i++) {
                    enums.TestEnum v = d[i].next;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((enums.TestEnum) @ref).next;
            }

            public override void set(SkillObject @ref, object value) {
                ((enums.TestEnum) @ref).next = (enums.TestEnum)value;
            }
        }

    }
}
