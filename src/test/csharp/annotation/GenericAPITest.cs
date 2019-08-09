using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = annotation.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace annotation
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_annotation_acc_Znull() {
            string path = tmpFile("null");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                annotation.Test tst = (annotation.Test)sf.Tests().make();
            // set fields
            tst.f = (de.ust.skill.common.csharp.@internal.SkillObject)null;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Tests().staticSize());
                // create objects from file
                    annotation.Test tst_2 = (annotation.Test)sf2.Tests().getByID(tst.SkillID);
                // assert fields
                    Assert.IsTrue(tst_2.f == null);
            }
            File.Delete(path);
        }

    }
}
