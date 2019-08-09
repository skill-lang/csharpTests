using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = container.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace container
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_container_acc_make() {
            string path = tmpFile("make");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                container.Container cont = (container.Container)sf.Containers().make();
            // set fields
            cont.arr = (System.Collections.ArrayList)array<long>(-1L, 0L, 1L);
            cont.s = (System.Collections.Generic.HashSet<System.Int64>)set<long>(9L, 9L, 9L);
            cont.f = (System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<System.Int64, System.Int64>>)put(map<string, Dictionary< long, long > >(), "String", put(put(map<long, long >(), 2L, 1L), 3L, 1L));
            cont.someSet = (System.Collections.Generic.HashSet<SomethingElse>)set<SomethingElse>();
            cont.varr = (System.Collections.ArrayList)array<long>(-2L, -1L, 0L, 1L);
            cont.l = (System.Collections.Generic.List<System.Int64>)list<long>(0L, 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L);
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Containers().staticSize());
                // create objects from file
                    container.Container cont_2 = (container.Container)sf2.Containers().getByID(cont.SkillID);
                // assert fields
                    Assert.IsTrue(cont_2.arr != null && ArrayListEqual(cont_2.arr, array<long>(-1L, 0L, 1L )));
                    Assert.IsTrue(cont_2.s != null && Enumerable.SequenceEqual(cont_2.s, set<long>(9L, 9L, 9L )));
                    Assert.IsTrue(cont_2.f != null && Enumerable.SequenceEqual(cont_2.f, put(map<string, Dictionary< long, long > >(), "String", put(put(map<long, long >(), 2L, 1L), 3L, 1L))));
                    Assert.IsTrue(cont_2.someSet != null && Enumerable.SequenceEqual(cont_2.someSet, set<SomethingElse>( )));
                    Assert.IsTrue(cont_2.varr != null && ArrayListEqual(cont_2.varr, array<long>(-2L, -1L, 0L, 1L )));
                    Assert.IsTrue(cont_2.l != null && Enumerable.SequenceEqual(cont_2.l, list<long>(0L, 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L )));
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_container_fail_fail__short__array() {
            string path = tmpFile("fail_short_array");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                container.Container cont = (container.Container)sf.Containers().make();
                // set fields
            cont.arr = (System.Collections.ArrayList)array<long>(-1L, 0L);
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Containers().staticSize());
                    // create objects from file
                    container.Container cont_2 = (container.Container)sf2.Containers().getByID(cont.SkillID);
                    // assert fields
                    Assert.IsTrue(cont_2.arr != null && ArrayListEqual(cont_2.arr, array<long>(-1L, 0L )));
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_container_fail_fail__long__array() {
            string path = tmpFile("fail_long_array");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                container.Container cont = (container.Container)sf.Containers().make();
                // set fields
            cont.arr = (System.Collections.ArrayList)array<long>(-1L, 0L, 1L, 2L);
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Containers().staticSize());
                    // create objects from file
                    container.Container cont_2 = (container.Container)sf2.Containers().getByID(cont.SkillID);
                    // assert fields
                    Assert.IsTrue(cont_2.arr != null && ArrayListEqual(cont_2.arr, array<long>(-1L, 0L, 1L, 2L )));
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

    }
}
