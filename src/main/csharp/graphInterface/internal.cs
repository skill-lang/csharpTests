/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using SkillFile = graphInterface.api.SkillFile;
using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.exceptions;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using de.ust.skill.common.csharp.@internal.parts;
using de.ust.skill.common.csharp.restrictions;
using de.ust.skill.common.csharp.streams;

namespace graphInterface
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
                    poolByName.TryGetValue("abstractnode", out p);
                    AbstractNodesField = (null == p) ? (P0)Parser.newPool("abstractnode", null, types) : (P0) p;
                    poolByName.TryGetValue("colorholder", out p);
                    ColorHoldersField = (null == p) ? (P1)Parser.newPool("colorholder", null, types) : (P1) p;
                    poolByName.TryGetValue("node", out p);
                    NodesField = (null == p) ? (P2)Parser.newPool("node", AbstractNodesField, types) : (P2) p;
                    poolByName.TryGetValue("subnode", out p);
                    SubNodesField = (null == p) ? (P3)Parser.newPool("subnode", NodesField, types) : (P3) p;
                    ColoredsField = new de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.Colored>("colored", annotationType, NodesField);
                    MarkersField = new de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.Marker>("marker", annotationType, NodesField);
                    ColoredNodesField = new de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, graphInterface.AbstractNode>("colorednode", AbstractNodesField, NodesField);
                    UnusedsField = new de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, graphInterface.AbstractNode>("unused", NodesField);
                    UnusedRootlesssField = new de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.UnusedRootless>("unusedrootless", annotationType);
                } catch (System.InvalidCastException e) {
                    throw new ParseException(@in, -1, e,
                            "A super type does not match the specification; see cause for details.");
                }
                foreach (AbstractStoragePool t in types)
                    poolByName[t.Name] = t;

                finalizePools(@in);
                @in.close();
            }
        
            internal readonly P0 AbstractNodesField;

            public override P0 AbstractNodes() {
                return AbstractNodesField;
            }
        
            internal readonly P1 ColorHoldersField;

            public override P1 ColorHolders() {
                return ColorHoldersField;
            }
        
            internal readonly P2 NodesField;

            public override P2 Nodes() {
                return NodesField;
            }
        
            internal readonly P3 SubNodesField;

            public override P3 SubNodes() {
                return SubNodesField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.Colored> ColoredsField;

            public override de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.Colored> Coloreds() {
                return ColoredsField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.Marker> MarkersField;

            public override de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.Marker> Markers() {
                return MarkersField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, graphInterface.AbstractNode> ColoredNodesField;

            public override de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, graphInterface.AbstractNode> ColoredNodes() {
                return ColoredNodesField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, graphInterface.AbstractNode> UnusedsField;

            public override de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, graphInterface.AbstractNode> Unuseds() {
                return UnusedsField;
            }
        
            internal readonly de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.UnusedRootless> UnusedRootlesssField;

            public override de.ust.skill.common.csharp.@internal.UnrootedInterfacePool<graphInterface.UnusedRootless> UnusedRootlesss() {
                return UnusedRootlesssField;
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
                        case "abstractnode":
                            return (superPool = new P0(types.Count));
        

                        case "colorholder":
                            return (superPool = new P1(types.Count));
        

                        case "node":
                            return (superPool = new P2(types.Count, (P0)superPool));


                        case "subnode":
                            return (superPool = new P3(types.Count, (P2)superPool));

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

        public sealed class P0 : BasePool<graphInterface.AbstractNode> {
        
            protected override graphInterface.AbstractNode[] newArray(int size) {
                return new graphInterface.AbstractNode[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P0(int poolIndex) : base(poolIndex, "abstractnode", new string[] { "edges", "map" }, NoAutoFields) {

            }

            internal graphInterface.AbstractNode[] Data {
                get
                {
                    return (graphInterface.AbstractNode[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new graphInterface.AbstractNode(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "edges":
                    unchecked{new f0(new SetType<graphInterface.ColoredNode>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).ColoredNodes())), this);}
                    return;

                case "map":
                    unchecked{new f1(new MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode,graphInterface.Marker>>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Nodes()), new MapType<graphInterface.ColoredNode, graphInterface.Marker>((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).ColoredNodes()), (de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Markers()))), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "edges":
                    return new f0((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "map":
                    return new f1((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new AbstractNode instance with default field values </returns>
            public override object make() {
                graphInterface.AbstractNode rval = new graphInterface.AbstractNode();
                add(rval);
                return rval;
            }
        
            /// <returns> a new graphInterface.AbstractNode instance with the argument field values </returns>
            public graphInterface.AbstractNode make(System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges, System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map) {
                graphInterface.AbstractNode rval = new graphInterface.AbstractNode(-1, edges, map);
                add(rval);
                return rval;
            }

            public AbstractNodeBuilder build() {
                return new AbstractNodeBuilder(this, new graphInterface.AbstractNode());
            }

            /// <summary>
            /// Builder for new AbstractNode instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class AbstractNodeBuilder : Builder<graphInterface.AbstractNode> {

                public AbstractNodeBuilder(AbstractStoragePool pool, graphInterface.AbstractNode instance) : base(pool, instance) {

                }

                public AbstractNodeBuilder edges(System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges) {
                    instance.edges = edges;
                    return this;
                }

                public AbstractNodeBuilder map(System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map) {
                    instance.map = map;
                    return this;
                }

                public override graphInterface.AbstractNode make() {
                    pool.add(instance);
                    graphInterface.AbstractNode rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<graphInterface.AbstractNode.SubType, graphInterface.AbstractNode> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new graphInterface.AbstractNode.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  check that abstract colors are in fact annotations
    /// </summary>
    public sealed class P1 : BasePool<graphInterface.ColorHolder> {
        
            protected override graphInterface.ColorHolder[] newArray(int size) {
                return new graphInterface.ColorHolder[size];
            }

            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P1(int poolIndex) : base(poolIndex, "colorholder", new string[] { "anabstractnode", "anannotation" }, NoAutoFields) {

            }

            internal graphInterface.ColorHolder[] Data {
                get
                {
                    return (graphInterface.ColorHolder[])data;
                }
            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new graphInterface.ColorHolder(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "anabstractnode":
                    unchecked{new f2((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).ColoredNodes()), this);}
                    return;

                case "anannotation":
                    unchecked{new f3((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Coloreds()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "anabstractnode":
                    return new f2((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "anannotation":
                    return new f3((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new ColorHolder instance with default field values </returns>
            public override object make() {
                graphInterface.ColorHolder rval = new graphInterface.ColorHolder();
                add(rval);
                return rval;
            }
        
            /// <returns> a new graphInterface.ColorHolder instance with the argument field values </returns>
            public graphInterface.ColorHolder make(graphInterface.ColoredNode anAbstractNode, graphInterface.Colored anAnnotation) {
                graphInterface.ColorHolder rval = new graphInterface.ColorHolder(-1, anAbstractNode, anAnnotation);
                add(rval);
                return rval;
            }

            public ColorHolderBuilder build() {
                return new ColorHolderBuilder(this, new graphInterface.ColorHolder());
            }

            /// <summary>
            /// Builder for new ColorHolder instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class ColorHolderBuilder : Builder<graphInterface.ColorHolder> {

                public ColorHolderBuilder(AbstractStoragePool pool, graphInterface.ColorHolder instance) : base(pool, instance) {

                }

                public ColorHolderBuilder anAbstractNode(graphInterface.ColoredNode anAbstractNode) {
                    instance.anAbstractNode = anAbstractNode;
                    return this;
                }

                public ColorHolderBuilder anAnnotation(graphInterface.Colored anAnnotation) {
                    instance.anAnnotation = anAnnotation;
                    return this;
                }

                public override graphInterface.ColorHolder make() {
                    pool.add(instance);
                    graphInterface.ColorHolder rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<graphInterface.ColorHolder.SubType, graphInterface.ColorHolder> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new graphInterface.ColorHolder.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  a graph of colored nodes
    /// </summary>
    public sealed class P2 : StoragePool<graphInterface.Node, graphInterface.AbstractNode> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P2(int poolIndex, P0 superPool) : base(poolIndex, "node", superPool, new string[] { "next", "color", "mark" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new graphInterface.Node(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "next":
                    unchecked{new f4((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Markers()), this);}
                    return;

                case "color":
                    unchecked{new f5(@string, this);}
                    return;

                case "mark":
                    unchecked{new f6(@string, this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "next":
                    return new f4((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "color":
                    return new f5((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "mark":
                    return new f6((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new Node instance with default field values </returns>
            public override object make() {
                graphInterface.Node rval = new graphInterface.Node();
                add(rval);
                return rval;
            }
        
            /// <returns> a new graphInterface.Node instance with the argument field values </returns>
            public graphInterface.Node make(System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges, System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map, graphInterface.Marker next, string color, string mark) {
                graphInterface.Node rval = new graphInterface.Node(-1, edges, map, next, color, mark);
                add(rval);
                return rval;
            }

            public NodeBuilder build() {
                return new NodeBuilder(this, new graphInterface.Node());
            }

            /// <summary>
            /// Builder for new Node instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class NodeBuilder : Builder<graphInterface.Node> {

                public NodeBuilder(AbstractStoragePool pool, graphInterface.Node instance) : base(pool, instance) {

                }

                public NodeBuilder edges(System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges) {
                    instance.edges = edges;
                    return this;
                }

                public NodeBuilder map(System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map) {
                    instance.map = map;
                    return this;
                }

                public NodeBuilder next(graphInterface.Marker next) {
                    instance.next = next;
                    return this;
                }

                public NodeBuilder color(string color) {
                    instance.color = color;
                    return this;
                }

                public NodeBuilder mark(string mark) {
                    instance.mark = mark;
                    return this;
                }

                public override graphInterface.Node make() {
                    pool.add(instance);
                    graphInterface.Node rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<graphInterface.Node.SubType, graphInterface.AbstractNode> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new graphInterface.Node.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
    ///  check that projection wont interfere with regular subtyping
    /// </summary>
    public sealed class P3 : StoragePool<graphInterface.SubNode, graphInterface.AbstractNode> {
        
            /// <summary>
            /// Can only be constructed by the SkillFile in this package.
            /// </summary>
            internal P3(int poolIndex, P2 superPool) : base(poolIndex, "subnode", superPool, new string[] { "f", "n" }, NoAutoFields) {

            }

            public override void allocateInstances(Block last) {
                int i = (int) last.bpo;
                int high = (int) (i + last.staticCount);
                while (i < high) {
                    data[i] = new graphInterface.SubNode(i + 1);
                    i += 1;
                }
            }
        
            public override void addKnownField(string name, StringType @string, Annotation annotation) {

                switch (name) {
                case "f":
                    unchecked{new f7((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Markers()), this);}
                    return;

                case "n":
                    unchecked{new f8((de.ust.skill.common.csharp.@internal.FieldType)(((SkillState)Owner).Nodes()), this);}
                    return;

                }
            }

            public override AbstractFieldDeclaration addField<R> (de.ust.skill.common.csharp.@internal.FieldType type, string name) {
                switch (name) {
                case "f":
                    return new f7((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                case "n":
                    return new f8((de.ust.skill.common.csharp.@internal.FieldType) type, this);

                default:
                    return base.addField<R>(type, name);
                }
            }

            /// <returns> a new SubNode instance with default field values </returns>
            public override object make() {
                graphInterface.SubNode rval = new graphInterface.SubNode();
                add(rval);
                return rval;
            }
        
            /// <returns> a new graphInterface.SubNode instance with the argument field values </returns>
            public graphInterface.SubNode make(graphInterface.Marker f, graphInterface.Node n, System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges, System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map, graphInterface.Marker next, string color, string mark) {
                graphInterface.SubNode rval = new graphInterface.SubNode(-1, f, n, edges, map, next, color, mark);
                add(rval);
                return rval;
            }

            public SubNodeBuilder build() {
                return new SubNodeBuilder(this, new graphInterface.SubNode());
            }

            /// <summary>
            /// Builder for new SubNode instances.
            ///
            /// @author Simon Glaub, Timm Felden
            /// </summary>
            public sealed class SubNodeBuilder : Builder<graphInterface.SubNode> {

                public SubNodeBuilder(AbstractStoragePool pool, graphInterface.SubNode instance) : base(pool, instance) {

                }

                public SubNodeBuilder f(graphInterface.Marker f) {
                    instance.f = f;
                    return this;
                }

                public SubNodeBuilder n(graphInterface.Node n) {
                    instance.n = n;
                    return this;
                }

                public SubNodeBuilder edges(System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges) {
                    instance.edges = edges;
                    return this;
                }

                public SubNodeBuilder map(System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map) {
                    instance.map = map;
                    return this;
                }

                public SubNodeBuilder next(graphInterface.Marker next) {
                    instance.next = next;
                    return this;
                }

                public SubNodeBuilder color(string color) {
                    instance.color = color;
                    return this;
                }

                public SubNodeBuilder mark(string mark) {
                    instance.mark = mark;
                    return this;
                }

                public override graphInterface.SubNode make() {
                    pool.add(instance);
                    graphInterface.SubNode rval = instance;
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

            private sealed class UnknownSubPool : StoragePool<graphInterface.SubNode.SubType, graphInterface.AbstractNode> {
                internal UnknownSubPool(int poolIndex, string name, AbstractStoragePool superPool) : base(poolIndex, name, superPool, noKnownFields, NoAutoFields){

                }

                public override AbstractStoragePool makeSubPool(int index, string name) {
                    return new UnknownSubPool(index, name, this);
                }

                public override void allocateInstances(Block last) {
                    int i = (int) last.bpo;
                    int high = (int)(i + last.staticCount);
                    while (i < high) {
                        data[i] = new graphInterface.SubNode.SubType(this, i + 1);
                        i += 1;
                    }
                }
            }
        }

        /// <summary>
        /// set<abstractnode> AbstractNode.edges
        /// </summary>
        internal sealed class f0 : KnownDataField<System.Collections.Generic.HashSet<graphInterface.ColoredNode>, graphInterface.AbstractNode> {

            public f0(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "edges", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type set<abstractnode> in AbstractNode.edges but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner).Data;
                SetType<graphInterface.ColoredNode> type = (SetType<graphInterface.ColoredNode>) this.type.cast<graphInterface.ColoredNode, System.Object>();
        P0 t;
                if((de.ust.skill.common.csharp.@internal.FieldType)type.groundType is P0)
                    t = (P0)(object)(type.groundType);
                else
                    t = (P0)((IInterfacePool)type.groundType).getSuperPool();
                for (; i != h; i++) {
            int size = @in.v32();
            System.Collections.Generic.HashSet<graphInterface.ColoredNode> v = new HashSet<graphInterface.ColoredNode>();
            while (size-- > 0) {
                v.Add((graphInterface.ColoredNode)t.getByID(@in.v32()));
            }
            d[i].edges = v;
                }

            }
            public override void osc(int i, int h) {
                SetType<graphInterface.ColoredNode> type = (SetType<graphInterface.ColoredNode>) this.type.cast<graphInterface.ColoredNode, System.Object>();
        P0 t;
                if((de.ust.skill.common.csharp.@internal.FieldType)type.groundType is P0)
                    t = (P0)(object)(type.groundType);
                else
                    t = (P0)((IInterfacePool)type.groundType).getSuperPool();
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.HashSet<graphInterface.AbstractNode> v = null == d[i].edges ? null : new System.Collections.Generic.HashSet<graphInterface.AbstractNode>(((System.Collections.Generic.HashSet<graphInterface.ColoredNode>)d[i].edges).Cast<graphInterface.AbstractNode>());

                    int size = null == v ? 0 : v.Count;
                    if (0 == size)
                        result++;
                    else {
                        result += V64.singleV64Offset(size);
                        foreach(graphInterface.AbstractNode x in v)
                    result += null==x?1:V64.singleV64Offset(x.SkillID);
                    }
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.AbstractNode[] d = ((P0) owner).Data;
                SetType<graphInterface.ColoredNode> type = (SetType<graphInterface.ColoredNode>) this.type.cast<graphInterface.ColoredNode, System.Object>();
        P0 t;
                if((de.ust.skill.common.csharp.@internal.FieldType)type.groundType is P0)
                    t = (P0)(object)(type.groundType);
                else
                    t = (P0)((IInterfacePool)type.groundType).getSuperPool();
                for (; i != h; i++) {
                    
        System.Collections.Generic.HashSet<graphInterface.ColoredNode> x = d[i].edges;
        int size = null == x ? 0 : x.Count;
        if (0 == size) {
            @out.i8((sbyte) 0);
        } else {
            @out.v64(size);
            foreach (graphInterface.ColoredNode e in x){
                SkillObject v = (SkillObject)e;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
            }
        };
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.AbstractNode) @ref).edges;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.AbstractNode) @ref).edges = new System.Collections.Generic.HashSet<graphInterface.ColoredNode>(((System.Collections.Generic.HashSet<object>)value).Cast<graphInterface.ColoredNode>());
            }
        }

        /// <summary>
        /// map<node,abstractnode,annotation> AbstractNode.map
        /// </summary>
        internal sealed class f1 : KnownDataField<System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>, graphInterface.AbstractNode> {

            public f1(de.ust.skill.common.csharp.@internal.FieldType type, P0 owner) : base(type, "map", owner) {
                
                if (false)//TODO type check!)
                    throw new SkillException("Expected field type map<node,abstractnode,annotation> in AbstractNode.map but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner).Data;
                MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> type = (MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>) this.type.cast<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
            d[i].map = castMap<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>, System.Object, System.Object>((Dictionary<System.Object, System.Object>)((de.ust.skill.common.csharp.@internal.FieldType)this.type).readSingleField(@in));
                }

            }
            public override void osc(int i, int h) {
                MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> type = (MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>) this.type.cast<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.AbstractNode, de.ust.skill.common.csharp.@internal.SkillObject>> v = castMap<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.AbstractNode, de.ust.skill.common.csharp.@internal.SkillObject>, graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>(d[i].map);
                    if(null==v || v.Count == 0)
                        result++;
                    else {

                        graphInterface.Node[] keysArray = new graphInterface.Node[v.Keys.Count];
                        v.Keys.CopyTo(keysArray, 0);
                        ICollection keysList = new List<object>();
                        foreach (object key in keysArray)
                        {
                            ((List<object>)keysList).Add(key);
                        }

                        System.Collections.Generic.Dictionary<graphInterface.AbstractNode, de.ust.skill.common.csharp.@internal.SkillObject>[] valuesArray = new System.Collections.Generic.Dictionary<graphInterface.AbstractNode, de.ust.skill.common.csharp.@internal.SkillObject>[v.Values.Count];
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
                graphInterface.AbstractNode[] d = ((P0) owner).Data;
                MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> type = (MapType<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>) this.type.cast<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>>();
                de.ust.skill.common.csharp.api.FieldType keyType = type.keyType;
                de.ust.skill.common.csharp.api.FieldType valueType = type.valueType;
                for (; i != h; i++) {
                    ((de.ust.skill.common.csharp.@internal.FieldType)this.type).writeSingleField(d[i].map, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.AbstractNode) @ref).map;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.AbstractNode) @ref).map = castMap<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>, object, object>((System.Collections.Generic.Dictionary<object, object>)value);
            }
        }

        /// <summary>
        /// abstractnode ColorHolder.anAbstractNode
        /// </summary>
        internal sealed class f2 : KnownDataField<graphInterface.ColoredNode, graphInterface.ColorHolder> {

            public f2(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "anabstractnode", owner) {
                
                if (!(type is IInterfacePool?((IInterfacePool)type).Name.Equals("colorednode"):((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("abstractnode")))
                    throw new SkillException("Expected field type abstractnode in ColorHolder.anAbstractNode but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.ColorHolder[] d = ((P1) owner).Data;
        P0 t;
                if((de.ust.skill.common.csharp.@internal.FieldType)this.type is P0)
                    t = (P0)(object)(this.type);
                else
                    t = (P0)((IInterfacePool)this.type).getSuperPool();
                for (; i != h; i++) {
            d[i].anAbstractNode = (graphInterface.ColoredNode)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
        P0 t;
                if((de.ust.skill.common.csharp.@internal.FieldType)this.type is P0)
                    t = (P0)(object)(this.type);
                else
                    t = (P0)((IInterfacePool)this.type).getSuperPool();
                graphInterface.ColorHolder[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    graphInterface.ColoredNode instance = d[i].anAbstractNode;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(((SkillObject) instance).SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.ColorHolder[] d = ((P1) owner).Data;
        P0 t;
                if((de.ust.skill.common.csharp.@internal.FieldType)this.type is P0)
                    t = (P0)(object)(this.type);
                else
                    t = (P0)((IInterfacePool)this.type).getSuperPool();
                for (; i != h; i++) {
                    SkillObject v = (SkillObject)d[i].anAbstractNode;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.ColorHolder) @ref).anAbstractNode;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.ColorHolder) @ref).anAbstractNode = (graphInterface.ColoredNode)value;
            }
        }

        /// <summary>
        /// annotation ColorHolder.anAnnotation
        /// </summary>
        internal sealed class f3 : KnownDataField<graphInterface.Colored, graphInterface.ColorHolder> {

            public f3(de.ust.skill.common.csharp.@internal.FieldType type, P1 owner) : base(type, "anannotation", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in ColorHolder.anAnnotation but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.ColorHolder[] d = ((P1) owner).Data;
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                for (; i != h; i++) {
            d[i].anAnnotation = (graphInterface.Colored)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                graphInterface.ColorHolder[] d = ((P1) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    graphInterface.Colored v = d[i].anAnnotation;

                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset((SkillObject)v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.ColorHolder[] d = ((P1) owner).Data;
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                for (; i != h; i++) {
                    t.writeSingleField((SkillObject)d[i].anAnnotation, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.ColorHolder) @ref).anAnnotation;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.ColorHolder) @ref).anAnnotation = (graphInterface.Colored)value;
            }
        }

        /// <summary>
        /// annotation Node.next
        /// </summary>
        internal sealed class f4 : KnownDataField<graphInterface.Marker, graphInterface.Node> {

            public f4(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "next", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in Node.next but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                for (; i != h; i++) {
            ((graphInterface.Node)d[i]).next = (graphInterface.Marker)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    graphInterface.Marker v = ((graphInterface.Node)d[i]).next;

                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset((SkillObject)v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                for (; i != h; i++) {
                    t.writeSingleField((SkillObject)((graphInterface.Node)d[i]).next, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.Node) @ref).next;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.Node) @ref).next = (graphInterface.Marker)value;
            }
        }

        /// <summary>
        /// string Node.color
        /// </summary>
        internal sealed class f5 : KnownDataField<System.String, graphInterface.Node> {

            public f5(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "color", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Node.color but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            ((graphInterface.Node)d[i]).color = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = ((graphInterface.Node)d[i]).color;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(((graphInterface.Node)d[i]).color, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.Node) @ref).color;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.Node) @ref).color = (System.String)value;
            }
        }

        /// <summary>
        /// string Node.mark
        /// </summary>
        internal sealed class f6 : KnownDataField<System.String, graphInterface.Node> {

            public f6(de.ust.skill.common.csharp.@internal.FieldType type, P2 owner) : base(type, "mark", owner) {
                
                if (type.TypeID != 14)
                    throw new SkillException("Expected field type string in Node.mark but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                StringPool t = (StringPool) owner.Owner.Strings();
                for (; i != h; i++) {
            ((graphInterface.Node)d[i]).mark = t.get(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                StringType t = (StringType) this.type;
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    string v = ((graphInterface.Node)d[i]).mark;
                    if(null==v)
                        result++;
                    else
                        result += t.singleOffset(v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                StringType t = (StringType) this.type;
                for (; i != h; i++) {
                    t.writeSingleField(((graphInterface.Node)d[i]).mark, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.Node) @ref).mark;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.Node) @ref).mark = (System.String)value;
            }
        }

        /// <summary>
        /// annotation SubNode.f
        /// </summary>
        internal sealed class f7 : KnownDataField<graphInterface.Marker, graphInterface.SubNode> {

            public f7(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "f", owner) {
                
                if (type.TypeID != 5)
                    throw new SkillException("Expected field type annotation in SubNode.f but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                for (; i != h; i++) {
            ((graphInterface.SubNode)d[i]).f = (graphInterface.Marker)t.readSingleField(@in);
                }

            }
            public override void osc(int i, int h) {
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    graphInterface.Marker v = ((graphInterface.SubNode)d[i]).f;

                    if(null==v)
                        result += 2;
                    else
                        result += t.singleOffset((SkillObject)v);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                Annotation t;
                // TODO we have to replace Annotation by the respective unrooted pool upon field creation to get rid of this distinction
                if ((de.ust.skill.common.csharp.@internal.FieldType)this.type is Annotation)
                    t = (Annotation) (de.ust.skill.common.csharp.@internal.FieldType) this.type;
                else
                    t = ((IUnrootedInterfacePool) this.type).Type;
                for (; i != h; i++) {
                    t.writeSingleField((SkillObject)((graphInterface.SubNode)d[i]).f, @out);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.SubNode) @ref).f;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.SubNode) @ref).f = (graphInterface.Marker)value;
            }
        }

        /// <summary>
        /// node SubNode.n
        /// </summary>
        internal sealed class f8 : KnownDataField<graphInterface.Node, graphInterface.SubNode> {

            public f8(de.ust.skill.common.csharp.@internal.FieldType type, P3 owner) : base(type, "n", owner) {
                
                if (!((AbstractStoragePool)(de.ust.skill.common.csharp.api.FieldType)type).Name.Equals("node"))
                    throw new SkillException("Expected field type node in SubNode.n but found " + type);
        
                // TODO insert known restrictions?
            }
        
            public override void rsc(int i, int h, MappedInStream @in) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                P2 t = ((P2)(object)this.type);
                for (; i != h; i++) {
            ((graphInterface.SubNode)d[i]).n = (graphInterface.Node)t.getByID(@in.v32());
                }

            }
            public override void osc(int i, int h) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                long result = 0L;
                for (; i != h; i++) {
                    graphInterface.Node instance = ((graphInterface.SubNode)d[i]).n;
                    if (null == instance) {
                        result += 1;
                        continue;
                    }
                    result += V64.singleV64Offset(instance.SkillID);
                }
                offset += result;
            }
            public override void wsc(int i, int h, MappedOutStream @out) {
                graphInterface.AbstractNode[] d = ((P0) owner.basePool).Data;
                for (; i != h; i++) {
                    graphInterface.Node v = ((graphInterface.SubNode)d[i]).n;
            if(null == v)
                @out.i8((sbyte)0);
            else
                @out.v64(v.SkillID);
                }

            }


            public override object get(SkillObject @ref) {
                return ((graphInterface.SubNode) @ref).n;
            }

            public override void set(SkillObject @ref, object value) {
                ((graphInterface.SubNode) @ref).n = (graphInterface.Node)value;
            }
        }

    }
}
