using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = age.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace age
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_age_acc_two() {
            string path = tmpFile("two");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                age.Age one = (age.Age)sf.Ages().make();
                age.Age two = (age.Age)sf.Ages().make();
            // set fields
            one.age = (long)30L;

            two.age = (long)2L;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(2, sf.Ages().staticSize());
                // create objects from file
                    age.Age one_2 = (age.Age)sf2.Ages().getByID(one.SkillID);
                    age.Age two_2 = (age.Age)sf2.Ages().getByID(two.SkillID);
                // assert fields
                    Assert.IsTrue(one_2.age == 30L);

                    Assert.IsTrue(two_2.age == 2L);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_age_fail_restr() {
            string path = tmpFile("restr");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                age.Age one = (age.Age)sf.Ages().make();
                // set fields
            one.age = (long)-1L;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Ages().staticSize());
                    // create objects from file
                    age.Age one_2 = (age.Age)sf2.Ages().getByID(one.SkillID);
                    // assert fields
                    Assert.IsTrue(one_2.age == -1L);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_age_acc_one() {
            string path = tmpFile("one");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                age.Age one = (age.Age)sf.Ages().make();
            // set fields
            one.age = (long)30L;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ages().staticSize());
                // create objects from file
                    age.Age one_2 = (age.Age)sf2.Ages().getByID(one.SkillID);
                // assert fields
                    Assert.IsTrue(one_2.age == 30L);
            }
            File.Delete(path);
        }

    }
}
