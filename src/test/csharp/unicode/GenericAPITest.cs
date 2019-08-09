using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = unicode.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace unicode
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_unicode_acc_example() {
            string path = tmpFile("example");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                unicode.Unicode uc = (unicode.Unicode)sf.Unicodes().make();
            // set fields
            uc.one = (string)"1";
            uc.two = (string)"ö";
            uc.three = (string)"☢";
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Unicodes().staticSize());
                // create objects from file
                    unicode.Unicode uc_2 = (unicode.Unicode)sf2.Unicodes().getByID(uc.SkillID);
                // assert fields
                    Assert.IsTrue(uc_2.one != null && uc_2.one.Equals("1"));
                    Assert.IsTrue(uc_2.two != null && uc_2.two.Equals("ö"));
                    Assert.IsTrue(uc_2.three != null && uc_2.three.Equals("☢"));
            }
            File.Delete(path);
        }

    }
}
