using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = floats.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace floats
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_floats_acc_values() {
            string path = tmpFile("values");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                floats.FloatTest flts = (floats.FloatTest)sf.FloatTests().make();
                floats.DoubleTest dbls = (floats.DoubleTest)sf.DoubleTests().make();
            // set fields
            flts.zero = (float)(float)0;
            flts.pi = (float)(float)3.141592653589793;
            flts.minusZZero = (float)(float)-0.0;
            flts.NaN = (float)(float)13;
            flts.two = (float)(float)2;

            dbls.zero = (double)(double)0;
            dbls.pi = (double)(double)3.141592653589793;
            dbls.minusZZero = (double)(double)-0.0;
            dbls.NaN = (double)(double)13;
            dbls.two = (double)(double)2;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.DoubleTests().staticSize());
                    Assert.AreEqual(1, sf.FloatTests().staticSize());
                // create objects from file
                    floats.FloatTest flts_2 = (floats.FloatTest)sf2.FloatTests().getByID(flts.SkillID);
                    floats.DoubleTest dbls_2 = (floats.DoubleTest)sf2.DoubleTests().getByID(dbls.SkillID);
                // assert fields
                    Assert.IsTrue(flts_2.zero == (float)0);
                    Assert.IsTrue(flts_2.pi == (float)3.141592653589793);
                    Assert.IsTrue(flts_2.minusZZero == (float)-0.0);
                    Assert.IsTrue(flts_2.NaN == (float)13);
                    Assert.IsTrue(flts_2.two == (float)2);

                    Assert.IsTrue(dbls_2.zero == (double)0);
                    Assert.IsTrue(dbls_2.pi == (double)3.141592653589793);
                    Assert.IsTrue(dbls_2.minusZZero == (double)-0.0);
                    Assert.IsTrue(dbls_2.NaN == (double)13);
                    Assert.IsTrue(dbls_2.two == (double)2);
            }
            File.Delete(path);
        }

    }
}
