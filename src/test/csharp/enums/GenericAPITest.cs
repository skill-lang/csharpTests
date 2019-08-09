using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = enums.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace enums
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_restrictions_enums_fail_instances() {
            string path = tmpFile("instances");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                enums.TestEnum enm_last = (enums.TestEnum)sf.TestEnums().make();
                enums.TestEnum enm_second = (enums.TestEnum)sf.TestEnums().make();
                enums.TestEnum enm_third = (enums.TestEnum)sf.TestEnums().make();
                enums.TestEnum enm_default = (enums.TestEnum)sf.TestEnums().make();
                // set fields
            enm_last.next = (TestEnum)enm_default;
            enm_last.name = (string)"last";

            enm_second.next = (TestEnum)enm_third;
            enm_second.name = (string)"second";

            enm_third.next = (TestEnum)enm_last;
            enm_third.name = (string)"third";

            enm_default.next = (TestEnum)enm_second;
            enm_default.name = (string)"default";
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(4, sf.TestEnums().staticSize());
                    // create objects from file
                    enums.TestEnum enm_last_2 = (enums.TestEnum)sf2.TestEnums().getByID(enm_last.SkillID);
                    enums.TestEnum enm_second_2 = (enums.TestEnum)sf2.TestEnums().getByID(enm_second.SkillID);
                    enums.TestEnum enm_third_2 = (enums.TestEnum)sf2.TestEnums().getByID(enm_third.SkillID);
                    enums.TestEnum enm_default_2 = (enums.TestEnum)sf2.TestEnums().getByID(enm_default.SkillID);
                    // assert fields
                    Assert.IsTrue(enm_last_2.next == enm_default_2);

                    Assert.IsTrue(enm_second_2.next == enm_third_2);

                    Assert.IsTrue(enm_third_2.next == enm_last_2);

                    Assert.IsTrue(enm_default_2.next == enm_second_2);
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
