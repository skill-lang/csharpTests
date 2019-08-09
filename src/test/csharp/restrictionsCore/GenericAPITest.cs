using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = restrictionsCore.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace restrictionsCore
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_restrictions_restrictionsCore_fail_duplicate() {
            string path = tmpFile("duplicate");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsCore.ZSystem sys_1 = (restrictionsCore.ZSystem)sf.ZSystems().make();
                restrictionsCore.ZSystem sys_2 = (restrictionsCore.ZSystem)sf.ZSystems().make();
                // set fields
            sys_1.name = (string)"Hexadecimal";
            sys_1.version = (float)(float)1.1;

            sys_2.name = (string)"Octal";
            sys_2.version = (float)(float)1.2;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(2, sf.ZSystems().staticSize());
                    // create objects from file
                    restrictionsCore.ZSystem sys_1_2 = (restrictionsCore.ZSystem)sf2.ZSystems().getByID(sys_1.SkillID);
                    restrictionsCore.ZSystem sys_2_2 = (restrictionsCore.ZSystem)sf2.ZSystems().getByID(sys_2.SkillID);
                    // assert fields
                    Assert.IsTrue(sys_1_2.name != null && sys_1_2.name.Equals("Hexadecimal"));
                    Assert.IsTrue(sys_1_2.version == (float)1.1);

                    Assert.IsTrue(sys_2_2.name != null && sys_2_2.name.Equals("Octal"));
                    Assert.IsTrue(sys_2_2.version == (float)1.2);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_restrictionsCore_acc_make() {
            string path = tmpFile("make");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                restrictionsCore.ZSystem sys = (restrictionsCore.ZSystem)sf.ZSystems().make();
            // set fields
            sys.name = (string)"Hexadecimal";
            sys.version = (float)(float)1.1;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.ZSystems().staticSize());
                // create objects from file
                    restrictionsCore.ZSystem sys_2 = (restrictionsCore.ZSystem)sf2.ZSystems().getByID(sys.SkillID);
                // assert fields
                    Assert.IsTrue(sys_2.name != null && sys_2.name.Equals("Hexadecimal"));
                    Assert.IsTrue(sys_2.version == (float)1.1);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsCore_fail_restrictionsCore__fail__2() {
            string path = tmpFile("restrictionsCore_fail_2");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsCore.ZSystem sys = (restrictionsCore.ZSystem)sf.ZSystems().make();
                // set fields
            sys.name = (string)"null";
            sys.version = (float)(float)1.1;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.ZSystems().staticSize());
                    // create objects from file
                    restrictionsCore.ZSystem sys_2 = (restrictionsCore.ZSystem)sf2.ZSystems().getByID(sys.SkillID);
                    // assert fields
                    Assert.IsTrue(sys_2.name != null && sys_2.name.Equals("null"));
                    Assert.IsTrue(sys_2.version == (float)1.1);
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
