using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = hintsAll.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace hintsAll
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_hintsAll_acc_basic() {
            string path = tmpFile("basic");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                hintsAll.Abuser a = (hintsAll.Abuser)sf.Abusers().make();
                hintsAll.NowASingleton nas = (hintsAll.NowASingleton)sf.NowASingletons().make();
                hintsAll.UID uid = (hintsAll.UID)sf.UIDs().make();
                hintsAll.BadType bt = (hintsAll.BadType)sf.BadTypes().make();
                hintsAll.User u = (hintsAll.User)sf.Users().make();
                hintsAll.ExternMixin em = (hintsAll.ExternMixin)sf.ExternMixins().make();
                hintsAll.Expression expr = (hintsAll.Expression)sf.Expressions().make();
            // set fields
            a.abuseDescription = (string)"I am a absue description";


            uid.identifier = (long)7L;

            bt.reflectivelyInVisible = (string)"I am reflectively visible";
            bt.ignoredData = (string)"Ignore me";

            u.name = (string)"herb";
            u.reflectivelyVisible = (string)"I am not visible";
            u.age = (long)43L;

            em.unknownStuff = (de.ust.skill.common.csharp.@internal.SkillObject)null;

            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.BadTypes().staticSize());
                    Assert.AreEqual(1, sf.NowASingletons().staticSize());
                    Assert.AreEqual(1, sf.Abusers().staticSize());
                    Assert.AreEqual(1, sf.ExternMixins().staticSize());
                    Assert.AreEqual(1, sf.Users().staticSize());
                    Assert.AreEqual(1, sf.UIDs().staticSize());
                    Assert.AreEqual(1, sf.Expressions().staticSize());
                // create objects from file
                    hintsAll.Abuser a_2 = (hintsAll.Abuser)sf2.Abusers().getByID(a.SkillID);
                    hintsAll.NowASingleton nas_2 = (hintsAll.NowASingleton)sf2.NowASingletons().getByID(nas.SkillID);
                    hintsAll.UID uid_2 = (hintsAll.UID)sf2.UIDs().getByID(uid.SkillID);
                    hintsAll.BadType bt_2 = (hintsAll.BadType)sf2.BadTypes().getByID(bt.SkillID);
                    hintsAll.User u_2 = (hintsAll.User)sf2.Users().getByID(u.SkillID);
                    hintsAll.ExternMixin em_2 = (hintsAll.ExternMixin)sf2.ExternMixins().getByID(em.SkillID);
                    hintsAll.Expression expr_2 = (hintsAll.Expression)sf2.Expressions().getByID(expr.SkillID);
                // assert fields
                    Assert.IsTrue(a_2.abuseDescription != null && a_2.abuseDescription.Equals("I am a absue description"));


                    Assert.IsTrue(uid_2.identifier == 7L);

                    Assert.IsTrue(bt_2.reflectivelyInVisible != null && bt_2.reflectivelyInVisible.Equals("I am reflectively visible"));
                    Assert.IsTrue(bt_2.ignoredData != null && bt_2.ignoredData.Equals("Ignore me"));

                    Assert.IsTrue(u_2.name != null && u_2.name.Equals("herb"));
                    Assert.IsTrue(u_2.reflectivelyVisible != null && u_2.reflectivelyVisible.Equals("I am not visible"));
                    Assert.IsTrue(u_2.age == 43L);

                    Assert.IsTrue(em_2.unknownStuff == null);

            }
            File.Delete(path);
        }

    }
}
