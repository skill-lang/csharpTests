using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = subtypes.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace subtypes
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_subtypes_acc_simple() {
            string path = tmpFile("simple");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                subtypes.A a = (subtypes.A)sf.As().make();
                subtypes.B b = (subtypes.B)sf.Bs().make();
                subtypes.C c = (subtypes.C)sf.Cs().make();
                subtypes.D d = (subtypes.D)sf.Ds().make();
            // set fields
            a.a = (A)a;

            b.a = (A)a;
            b.b = (B)b;

            c.a = (A)a;
            c.c = (C)c;

            d.a = (A)a;
            d.b = (B)b;
            d.d = (D)d;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ds().staticSize());
                    Assert.AreEqual(1, sf.As().staticSize());
                    Assert.AreEqual(1, sf.Cs().staticSize());
                    Assert.AreEqual(1, sf.Bs().staticSize());
                // create objects from file
                    subtypes.A a_2 = (subtypes.A)sf2.As().getByID(a.SkillID);
                    subtypes.B b_2 = (subtypes.B)sf2.Bs().getByID(b.SkillID);
                    subtypes.C c_2 = (subtypes.C)sf2.Cs().getByID(c.SkillID);
                    subtypes.D d_2 = (subtypes.D)sf2.Ds().getByID(d.SkillID);
                // assert fields
                    Assert.IsTrue(a_2.a == a_2);

                    Assert.IsTrue(b_2.a == a_2);
                    Assert.IsTrue(b_2.b == b_2);

                    Assert.IsTrue(c_2.a == a_2);
                    Assert.IsTrue(c_2.c == c_2);

                    Assert.IsTrue(d_2.a == a_2);
                    Assert.IsTrue(d_2.b == b_2);
                    Assert.IsTrue(d_2.d == d_2);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_subtypes_acc_poly() {
            string path = tmpFile("poly");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                subtypes.A a = (subtypes.A)sf.As().make();
                subtypes.B b = (subtypes.B)sf.Bs().make();
                subtypes.C c = (subtypes.C)sf.Cs().make();
                subtypes.D d = (subtypes.D)sf.Ds().make();
            // set fields
            a.a = (A)d;

            b.a = (A)d;
            b.b = (B)d;

            c.a = (A)d;
            c.c = (C)c;

            d.a = (A)d;
            d.b = (B)d;
            d.d = (D)d;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.Ds().staticSize());
                    Assert.AreEqual(1, sf.As().staticSize());
                    Assert.AreEqual(1, sf.Cs().staticSize());
                    Assert.AreEqual(1, sf.Bs().staticSize());
                // create objects from file
                    subtypes.A a_2 = (subtypes.A)sf2.As().getByID(a.SkillID);
                    subtypes.B b_2 = (subtypes.B)sf2.Bs().getByID(b.SkillID);
                    subtypes.C c_2 = (subtypes.C)sf2.Cs().getByID(c.SkillID);
                    subtypes.D d_2 = (subtypes.D)sf2.Ds().getByID(d.SkillID);
                // assert fields
                    Assert.IsTrue(a_2.a == d_2);

                    Assert.IsTrue(b_2.a == d_2);
                    Assert.IsTrue(b_2.b == d_2);

                    Assert.IsTrue(c_2.a == d_2);
                    Assert.IsTrue(c_2.c == c_2);

                    Assert.IsTrue(d_2.a == d_2);
                    Assert.IsTrue(d_2.b == d_2);
                    Assert.IsTrue(d_2.d == d_2);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_subtypes_skipped_poly__fail__1() {
        }

        [Test]
        public void APITest_core_subtypes_skipped_poly__fail__2() {
        }

        [Test]
        public void APITest_core_subtypes_skipped_polyFail() {
        }

    }
}
