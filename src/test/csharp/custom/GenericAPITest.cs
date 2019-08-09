using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = custom.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace custom
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_custom_acc_customFields__succ__1() {
            string path = tmpFile("customFields_succ_1");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                custom.Custom c = (custom.Custom)sf.Customs().make();
            // set fields
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Customs().staticSize());
                // create objects from file
                    custom.Custom c_2 = (custom.Custom)sf2.Customs().getByID(c.SkillID);
                // assert fields
            }
            File.Delete(path);
        }

    }
}
