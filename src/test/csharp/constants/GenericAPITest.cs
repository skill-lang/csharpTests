using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = constants.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace constants
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_constants_acc_make() {
            string path = tmpFile("make");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                constants.Constant consts = (constants.Constant)sf.Constants().make();
            // set fields
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Constants().staticSize());
                // create objects from file
                    constants.Constant consts_2 = (constants.Constant)sf2.Constants().getByID(consts.SkillID);
                // assert fields
            }
            File.Delete(path);
        }

    }
}
