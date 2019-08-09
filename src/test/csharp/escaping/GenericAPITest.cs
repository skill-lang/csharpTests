using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = escaping.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace escaping
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_escaped_escaping_acc_instances() {
            string path = tmpFile("instances");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                escaping.Int esc_1 = (escaping.Int)sf.Ints().make();
                escaping.ZBoolean esc_3 = (escaping.ZBoolean)sf.ZBooleans().make();
                escaping.If esc_2 = (escaping.If)sf.Ifs().make();
                escaping.Z2200 esc_4 = (escaping.Z2200)sf.Z2200s().make();
            // set fields
            esc_1.Zfor = (If)esc_2;
            esc_1.Zif = (Int)esc_1;

            esc_3.boolean = (bool)true;
            esc_3.Zbool = (ZBoolean)esc_3;


            esc_4.Z2622 = (string)"Hello, World!";
            esc_4.Z20ac = (Z2200)esc_4;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ints().staticSize());
                    Assert.AreEqual(1, sf.Ifs().staticSize());
                    Assert.AreEqual(1, sf.ZBooleans().staticSize());
                    Assert.AreEqual(1, sf.Z2200s().staticSize());
                // create objects from file
                    escaping.Int esc_1_2 = (escaping.Int)sf2.Ints().getByID(esc_1.SkillID);
                    escaping.ZBoolean esc_3_2 = (escaping.ZBoolean)sf2.ZBooleans().getByID(esc_3.SkillID);
                    escaping.If esc_2_2 = (escaping.If)sf2.Ifs().getByID(esc_2.SkillID);
                    escaping.Z2200 esc_4_2 = (escaping.Z2200)sf2.Z2200s().getByID(esc_4.SkillID);
                // assert fields
                    Assert.IsTrue(esc_1_2.Zfor == esc_2_2);
                    Assert.IsTrue(esc_1_2.Zif == esc_1_2);

                    Assert.IsTrue(esc_3_2.boolean == true);
                    Assert.IsTrue(esc_3_2.Zbool == esc_3_2);


                    Assert.IsTrue(esc_4_2.Z2622 != null && esc_4_2.Z2622.Equals("Hello, World!"));
                    Assert.IsTrue(esc_4_2.Z20ac == esc_4_2);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_escaped_escaping_skipped_ZBoolean() {
        }

        [Test]
        public void APITest_escaped_escaping_acc_escaping__succ__1() {
            string path = tmpFile("escaping_succ_1");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                escaping.Int esc_1 = (escaping.Int)sf.Ints().make();
                escaping.ZBoolean esc_3 = (escaping.ZBoolean)sf.ZBooleans().make();
                escaping.If esc_2 = (escaping.If)sf.Ifs().make();
                escaping.Z2200 esc_4 = (escaping.Z2200)sf.Z2200s().make();
            // set fields
            esc_1.Zfor = (If)esc_2;
            esc_1.Zif = (Int)esc_1;

            esc_3.boolean = (bool)true;
            esc_3.Zbool = (ZBoolean)esc_3;


            esc_4.Z2622 = (string)"Hello, World!";
            esc_4.Z20ac = (Z2200)esc_4;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ints().staticSize());
                    Assert.AreEqual(1, sf.Ifs().staticSize());
                    Assert.AreEqual(1, sf.ZBooleans().staticSize());
                    Assert.AreEqual(1, sf.Z2200s().staticSize());
                // create objects from file
                    escaping.Int esc_1_2 = (escaping.Int)sf2.Ints().getByID(esc_1.SkillID);
                    escaping.ZBoolean esc_3_2 = (escaping.ZBoolean)sf2.ZBooleans().getByID(esc_3.SkillID);
                    escaping.If esc_2_2 = (escaping.If)sf2.Ifs().getByID(esc_2.SkillID);
                    escaping.Z2200 esc_4_2 = (escaping.Z2200)sf2.Z2200s().getByID(esc_4.SkillID);
                // assert fields
                    Assert.IsTrue(esc_1_2.Zfor == esc_2_2);
                    Assert.IsTrue(esc_1_2.Zif == esc_1_2);

                    Assert.IsTrue(esc_3_2.boolean == true);
                    Assert.IsTrue(esc_3_2.Zbool == esc_3_2);


                    Assert.IsTrue(esc_4_2.Z2622 != null && esc_4_2.Z2622.Equals("Hello, World!"));
                    Assert.IsTrue(esc_4_2.Z20ac == esc_4_2);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_escaped_escaping_skipped_Zbool() {
        }

    }
}
