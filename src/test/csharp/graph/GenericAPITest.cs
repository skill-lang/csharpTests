using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = graph.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace graph
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_graph_acc_penta() {
            string path = tmpFile("penta");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                graph.Node n1 = (graph.Node)sf.Nodes().make();
                graph.Node n2 = (graph.Node)sf.Nodes().make();
                graph.Node n3 = (graph.Node)sf.Nodes().make();
                graph.Node n4 = (graph.Node)sf.Nodes().make();
                graph.Node n5 = (graph.Node)sf.Nodes().make();
            // set fields
            n1.color = (string)"black";
            n1.edges = (System.Collections.Generic.HashSet<Node>)set<Node>(n1, n2, n3, n4, n5);

            n2.color = (string)"schwarz";
            n2.edges = (System.Collections.Generic.HashSet<Node>)set<Node>(n1, n2, n3, n4, n5);

            n3.color = (string)"niger";
            n3.edges = (System.Collections.Generic.HashSet<Node>)set<Node>(n1, n2, n3, n4, n5);

            n4.color = (string)"noir";
            n4.edges = (System.Collections.Generic.HashSet<Node>)set<Node>(n1, n2, n3, n4, n5);

            n5.color = (string)"negro";
            n5.edges = (System.Collections.Generic.HashSet<Node>)set<Node>(n1, n2, n3, n4, n5);
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(5, sf.Nodes().staticSize());
                // create objects from file
                    graph.Node n1_2 = (graph.Node)sf2.Nodes().getByID(n1.SkillID);
                    graph.Node n2_2 = (graph.Node)sf2.Nodes().getByID(n2.SkillID);
                    graph.Node n3_2 = (graph.Node)sf2.Nodes().getByID(n3.SkillID);
                    graph.Node n4_2 = (graph.Node)sf2.Nodes().getByID(n4.SkillID);
                    graph.Node n5_2 = (graph.Node)sf2.Nodes().getByID(n5.SkillID);
                // assert fields
                    Assert.IsTrue(n1_2.color != null && n1_2.color.Equals("black"));
                    Assert.IsTrue(n1_2.edges != null && Enumerable.SequenceEqual(n1_2.edges, set<Node>(n1_2, n2_2, n3_2, n4_2, n5_2 )));

                    Assert.IsTrue(n2_2.color != null && n2_2.color.Equals("schwarz"));
                    Assert.IsTrue(n2_2.edges != null && Enumerable.SequenceEqual(n2_2.edges, set<Node>(n1_2, n2_2, n3_2, n4_2, n5_2 )));

                    Assert.IsTrue(n3_2.color != null && n3_2.color.Equals("niger"));
                    Assert.IsTrue(n3_2.edges != null && Enumerable.SequenceEqual(n3_2.edges, set<Node>(n1_2, n2_2, n3_2, n4_2, n5_2 )));

                    Assert.IsTrue(n4_2.color != null && n4_2.color.Equals("noir"));
                    Assert.IsTrue(n4_2.edges != null && Enumerable.SequenceEqual(n4_2.edges, set<Node>(n1_2, n2_2, n3_2, n4_2, n5_2 )));

                    Assert.IsTrue(n5_2.color != null && n5_2.color.Equals("negro"));
                    Assert.IsTrue(n5_2.edges != null && Enumerable.SequenceEqual(n5_2.edges, set<Node>(n1_2, n2_2, n3_2, n4_2, n5_2 )));
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_graph_acc_small() {
            string path = tmpFile("small");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                graph.Node nd_2 = (graph.Node)sf.Nodes().make();
                graph.Node nd_1 = (graph.Node)sf.Nodes().make();
            // set fields
            nd_2.color = (string)"blue";
            nd_2.edges = (System.Collections.Generic.HashSet<Node>)set<Node>();

            nd_1.color = (string)"red";
            nd_1.edges = (System.Collections.Generic.HashSet<Node>)set<Node>(nd_2);
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(2, sf.Nodes().staticSize());
                // create objects from file
                    graph.Node nd_2_2 = (graph.Node)sf2.Nodes().getByID(nd_2.SkillID);
                    graph.Node nd_1_2 = (graph.Node)sf2.Nodes().getByID(nd_1.SkillID);
                // assert fields
                    Assert.IsTrue(nd_2_2.color != null && nd_2_2.color.Equals("blue"));
                    Assert.IsTrue(nd_2_2.edges != null && Enumerable.SequenceEqual(nd_2_2.edges, set<Node>( )));

                    Assert.IsTrue(nd_1_2.color != null && nd_1_2.color.Equals("red"));
                    Assert.IsTrue(nd_1_2.edges != null && Enumerable.SequenceEqual(nd_1_2.edges, set<Node>(nd_2_2 )));
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_graph_acc_Znull() {
            string path = tmpFile("null");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                graph.Node nd = (graph.Node)sf.Nodes().make();
            // set fields
            nd.color = (string)"null";
            nd.edges = (System.Collections.Generic.HashSet<Node>)set<Node>((Node) null);
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Nodes().staticSize());
                // create objects from file
                    graph.Node nd_2 = (graph.Node)sf2.Nodes().getByID(nd.SkillID);
                // assert fields
                    Assert.IsTrue(nd_2.color != null && nd_2.color.Equals("null"));
                    Assert.IsTrue(nd_2.edges != null && Enumerable.SequenceEqual(nd_2.edges, set<Node>((Node) null )));
            }
            File.Delete(path);
        }

    }
}
