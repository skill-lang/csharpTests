using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = number.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace number
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_number_acc_numbers() {
            string path = tmpFile("numbers");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                number.Number n = (number.Number)sf.Numbers().make();
            // set fields
            n.number = (long)1234567L;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Numbers().staticSize());
                // create objects from file
                    number.Number n_2 = (number.Number)sf2.Numbers().getByID(n.SkillID);
                // assert fields
                    Assert.IsTrue(n_2.number == 1234567L);
            }
            File.Delete(path);
        }

    }
}
