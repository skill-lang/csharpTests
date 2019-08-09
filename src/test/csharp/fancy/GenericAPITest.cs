using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = fancy.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace fancy
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_fancy_acc_fancy() {
            string path = tmpFile("fancy");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                fancy.D d = (fancy.D)sf.Ds().make();
                fancy.G g = (fancy.G)sf.Gs().make();
            // set fields
            d.Parent = (A)d;
            d.Value = (C)d;

            g.Parent = (A)d;
            g.aMap = (System.Collections.Generic.Dictionary<E, F>)put(map<E, F >(), g, g);
            g.Value = (C)g;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ds().staticSize());
                    Assert.AreEqual(1, sf.Gs().staticSize());
                // create objects from file
                    fancy.D d_2 = (fancy.D)sf2.Ds().getByID(d.SkillID);
                    fancy.G g_2 = (fancy.G)sf2.Gs().getByID(g.SkillID);
                // assert fields
                    Assert.IsTrue(d_2.Parent == d_2);
                    Assert.IsTrue(d_2.Value == d_2);

                    Assert.IsTrue(g_2.Parent == d_2);
                    Assert.IsTrue(g_2.aMap != null && Enumerable.SequenceEqual(g_2.aMap, put(map<E, F >(), g_2, g_2)));
                    Assert.IsTrue(g_2.Value == g_2);
            }
            File.Delete(path);
        }

    }
}
