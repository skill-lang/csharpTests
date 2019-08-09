using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = graphInterface.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace graphInterface
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_interfaces_graphInterface_acc_succ__1() {
            string path = tmpFile("succ_1");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                graphInterface.ColorHolder ch = (graphInterface.ColorHolder)sf.ColorHolders().make();
                graphInterface.SubNode sn = (graphInterface.SubNode)sf.SubNodes().make();
                graphInterface.Node n = (graphInterface.Node)sf.Nodes().make();
            // set fields
            ch.anAbstractNode = (ColoredNode)n;
            ch.anAnnotation = (Colored)(Colored) null;

            sn.next = (Marker)(Marker) null;
            sn.color = (string)"red";
            sn.f = (Marker)(Marker) null;
            sn.edges = (System.Collections.Generic.HashSet<ColoredNode>)set<ColoredNode>(n);
            sn.map = (System.Collections.Generic.Dictionary<Node, System.Collections.Generic.Dictionary<ColoredNode, Marker>>)put(map<Node, Dictionary< ColoredNode, Marker > >(), n, put(map<ColoredNode, Marker >(), n, (Marker) null));
            sn.mark = (string)"Cirlce";
            sn.n = (Node)n;

            n.next = (Marker)(Marker) null;
            n.color = (string)"blue";
            n.edges = (System.Collections.Generic.HashSet<ColoredNode>)set<ColoredNode>();
            n.map = (System.Collections.Generic.Dictionary<Node, System.Collections.Generic.Dictionary<ColoredNode, Marker>>)put(map<Node, Dictionary< ColoredNode, Marker > >(), n, put(map<ColoredNode, Marker >(), n, (Marker) null));
            n.mark = (string)"Circle";
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.SubNodes().staticSize());
                    Assert.AreEqual(1, sf.Nodes().staticSize());
                    Assert.AreEqual(1, sf.ColorHolders().staticSize());
                // create objects from file
                    graphInterface.ColorHolder ch_2 = (graphInterface.ColorHolder)sf2.ColorHolders().getByID(ch.SkillID);
                    graphInterface.SubNode sn_2 = (graphInterface.SubNode)sf2.SubNodes().getByID(sn.SkillID);
                    graphInterface.Node n_2 = (graphInterface.Node)sf2.Nodes().getByID(n.SkillID);
                // assert fields
                    Assert.IsTrue(ch_2.anAbstractNode == n_2);
                    Assert.IsTrue(ch_2.anAnnotation == (Colored) null);

                    Assert.IsTrue(sn_2.next == (Marker) null);
                    Assert.IsTrue(sn_2.color != null && sn_2.color.Equals("red"));
                    Assert.IsTrue(sn_2.f == (Marker) null);
                    Assert.IsTrue(sn_2.edges != null && Enumerable.SequenceEqual(sn_2.edges, set<ColoredNode>(n_2 )));
                    Assert.IsTrue(sn_2.map != null && Enumerable.SequenceEqual(sn_2.map, put(map<Node, Dictionary< ColoredNode, Marker > >(), n_2, put(map<ColoredNode, Marker >(), n_2, (Marker) null))));
                    Assert.IsTrue(sn_2.mark != null && sn_2.mark.Equals("Cirlce"));
                    Assert.IsTrue(sn_2.n == n_2);

                    Assert.IsTrue(n_2.next == (Marker) null);
                    Assert.IsTrue(n_2.color != null && n_2.color.Equals("blue"));
                    Assert.IsTrue(n_2.edges != null && Enumerable.SequenceEqual(n_2.edges, set<ColoredNode>( )));
                    Assert.IsTrue(n_2.map != null && Enumerable.SequenceEqual(n_2.map, put(map<Node, Dictionary< ColoredNode, Marker > >(), n_2, put(map<ColoredNode, Marker >(), n_2, (Marker) null))));
                    Assert.IsTrue(n_2.mark != null && n_2.mark.Equals("Circle"));
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_interfaces_graphInterface_skipped_fail__1() {
        }

    }
}
