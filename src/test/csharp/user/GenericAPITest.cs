using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = user.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace user
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_user_acc_bernd() {
            string path = tmpFile("bernd");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                user.User usr = (user.User)sf.Users().make();
            // set fields
            usr.name = (string)"Bernd das Brot";
            usr.age = (long)44L;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Users().staticSize());
                // create objects from file
                    user.User usr_2 = (user.User)sf2.Users().getByID(usr.SkillID);
                // assert fields
                    Assert.IsTrue(usr_2.name != null && usr_2.name.Equals("Bernd das Brot"));
                    Assert.IsTrue(usr_2.age == 44L);
            }
            File.Delete(path);
        }

    }
}
