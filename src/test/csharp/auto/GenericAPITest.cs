using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = auto.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace auto
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_auto_acc_some() {
            string path = tmpFile("some");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                auto.A a = (auto.A)sf.As().make();
                auto.B b = (auto.B)sf.Bs().make();
                auto.C c = (auto.C)sf.Cs().make();
                auto.D d = (auto.D)sf.Ds().make();
            // set fields
            a.a = (A)a;

            b.b = (B)b;

            c.c = (C)c;

            d.d = (D)(D) null;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ds().staticSize());
                    Assert.AreEqual(1, sf.As().staticSize());
                    Assert.AreEqual(1, sf.Cs().staticSize());
                    Assert.AreEqual(1, sf.Bs().staticSize());
                // create objects from file
                    auto.A a_2 = (auto.A)sf2.As().getByID(a.SkillID);
                    auto.B b_2 = (auto.B)sf2.Bs().getByID(b.SkillID);
                    auto.C c_2 = (auto.C)sf2.Cs().getByID(c.SkillID);
                    auto.D d_2 = (auto.D)sf2.Ds().getByID(d.SkillID);
                // assert fields



            }
            File.Delete(path);
        }

    }
}
