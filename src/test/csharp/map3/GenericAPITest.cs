using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = map3.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace map3
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_map3_acc_simple() {
            string path = tmpFile("simple");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                map3.T T = (map3.T)sf.Ts().make();
            // set fields
            T.Zref = (System.Collections.Generic.Dictionary<System.String, System.Collections.Generic.Dictionary<L, System.String>>)put(map<string, Dictionary< L, string > >(), "hallo", put(map<L, string >(), (L) null, "welt"));
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ts().staticSize());
                // create objects from file
                    map3.T T_2 = (map3.T)sf2.Ts().getByID(T.SkillID);
                // assert fields
                    Assert.IsTrue(T_2.Zref != null && Enumerable.SequenceEqual(T_2.Zref, put(map<string, Dictionary< L, string > >(), "hallo", put(map<L, string >(), (L) null, "welt"))));
            }
            File.Delete(path);
        }

    }
}
