/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = graph.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace graph
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
                    poolByName.TryGetValue("node", out p);
                    NodesField = (null == p) ? (P0)Parser.newPool("node", null, types) : (P0) p;
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 NodesField;

            public override P0 Nodes() {
                return NodesField;
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
                        case "node":
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
    ///  a graph of colored nodes
    /// </summary>
    public sealed class P0 : BasePool<graph.Node> {
        
            protected override graph.Node[] newArray(int size) {
                return new graph.Node[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "node", new string[] { "color", "edges" }, NoAutoFields) {

            }

            internal graph.Node[] Data {
                get
                {
                    return (graph.Node[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new graph.Node(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "color":
                    unchecked{new f0(@string, this);}
                    return;

                case "edges":
                    unchecked{new f1(new SetType<graph.Node>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Nodes())), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "color":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "edges":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Node instance with default field values </returns>
            public override object make() {
                graph.Node rval = new graph.Node();
                add(rval);
                return rval;
            }
        
            /// <returns> a new graph.Node instance with the argument field values </returns>
            public graph.Node make(string color, System.Collections.Generic.HashSet<graph.Node> edges) {
                graph.Node rval = new graph.Node(-1, color, edges);
                add(rval);
                return rval;
            }

            public NodeBuilder build() {
                return new NodeBuilder(this, new graph.Node());
            }

            /// <summary>
            /// Builder for new Node instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class NodeBuilder : Builder<graph.Node> {

                public NodeBuilder(AbstractStoragePool pool, graph.Node instance) : base(pool, instance) {

                }

                public NodeBuilder color(string color) {
                    instance.color = color;
                    return this;
                }

                public NodeBuilder edges(System.Collections.Generic.HashSet<graph.Node> edges) {
                    instance.edges = edges;
                    return this;
                }

                public override graph.Node make() {
                    pool.add(instance);
                    graph.Node rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<graph.Node.SubType, graph.Node> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new graph.Node.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// string Node.color
        /// </summary>
        internal sealed class f0 : KnownDataField<System.String, graph.Node> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "color", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Node.color but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graph.Node[] d = ((P0) owner).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            d[i].color = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                graph.Node[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = d[i].color;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graph.Node[] d = ((P0) owner).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(d[i].color, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graph.Node) @ref).color;
            }

            public override void set(SkillObject @ref, object value) {
                ((graph.Node) @ref).color = (System.String)value;
            }
        }

        /// <summary>
        /// set<node> Node.edges
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Collections.Generic.HashSet<graph.Node>, graph.Node> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "edges", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type set<node> in Node.edges but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graph.Node[] d = ((P0) owner).Data;
                SetType<graph.Node> type = (SetType<graph.Node>) this.type.cast<graph.Node, System.Object>();
                P0 t = ((P0)(object)type.groundType);
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.HashSet<graph.Node> v = new HashSet<graph.Node>();
            while (size-- > 0) {
                v.Add((graph.Node)t.getByID(@in.v32()));
            }
            d[i].edges = v;
                }

            }
            public override void osc(int i, int h) {
                SetType<graph.Node> type = (SetType<graph.Node>) this.type.cast<graph.Node, System.Object>();
                graph.Node[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.HashSet<graph.Node> v = null == d[i].edges ? null : new System.Collections.Generic.HashSet<graph.Node>(((System.Collections.Generic.HashSet<graph.Node>)d[i].edges).Cast<graph.Node>());

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(graph.Node x in v)
                    result += null==x?1:V64.singleV64Offset(x.SkillID);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graph.Node[] d = ((P0) owner).Data;
                SetType<graph.Node> type = (SetType<graph.Node>) this.type.cast<graph.Node, System.Object>();
                for (; i != h; i++) {
                    
        System.Collections.Generic.HashSet<graph.Node> x = d[i].edges;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (graph.Node e in x){
                graph.Node v = e;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((graph.Node) @ref).edges;
            }

            public override void set(SkillObject @ref, object value) {
                ((graph.Node) @ref).edges = new System.Collections.Generic.HashSet<graph.Node>(((System.Collections.Generic.HashSet<object>)value).Cast<graph.Node>());
            }
        }

    }
}
