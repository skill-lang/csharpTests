using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = unknown.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace unknown
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_unknown_acc_full() {
            string path = tmpFile("full");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                unknown.A a = (unknown.A)sf.As().make();
                unknown.C c = (unknown.C)sf.Cs().make();
            // set fields
            a.a = (A)a;

            c.a = (A)a;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.As().staticSize());
                    Assert.AreEqual(1, sf.Cs().staticSize());
                // create objects from file
                    unknown.A a_2 = (unknown.A)sf2.As().getByID(a.SkillID);
                    unknown.C c_2 = (unknown.C)sf2.Cs().getByID(c.SkillID);
                // assert fields
                    Assert.IsTrue(a_2.a == a_2);

                    Assert.IsTrue(c_2.a == a_2);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_unknown_acc_partial() {
            string path = tmpFile("partial");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                unknown.A a = (unknown.A)sf.As().make();
                unknown.C c = (unknown.C)sf.Cs().make();
            // set fields
            a.a = (A)a;

            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.As().staticSize());
                    Assert.AreEqual(1, sf.Cs().staticSize());
                // create objects from file
                    unknown.A a_2 = (unknown.A)sf2.As().getByID(a.SkillID);
                    unknown.C c_2 = (unknown.C)sf2.Cs().getByID(c.SkillID);
                // assert fields
                    Assert.IsTrue(a_2.a == a_2);

            }
            File.Delete(path);
        }

    }
}
