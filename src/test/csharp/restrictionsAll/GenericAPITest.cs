using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = restrictionsAll.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace restrictionsAll
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range2() {
            string path = tmpFile("range2");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)-1;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)-1);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_oneOf2() {
            string path = tmpFile("oneOf2");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RegularProperty rp = (restrictionsAll.RegularProperty)sf.RegularPropertys().make();
                restrictionsAll.Comment cmnt = (restrictionsAll.Comment)sf.Comments().make();
                // set fields

            cmnt.target = (de.ust.skill.common.csharp.@internal.SkillObject)rp;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Comments().staticSize());
                    Assert.AreEqual(1, sf.RegularPropertys().staticSize());
                    // create objects from file
                    restrictionsAll.RegularProperty rp_2 = (restrictionsAll.RegularProperty)sf2.RegularPropertys().getByID(rp.SkillID);
                    restrictionsAll.Comment cmnt_2 = (restrictionsAll.Comment)sf2.Comments().getByID(cmnt.SkillID);
                    // assert fields

                    Assert.IsTrue(cmnt_2.target == rp_2);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range5() {
            string path = tmpFile("range5");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)360;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)360);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_acc_range7() {
            string path = tmpFile("range7");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
            // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0.001;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0.001);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range9() {
            string path = tmpFile("range9");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)0;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0.001;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)0);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0.001);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_nonnull2() {
            string path = tmpFile("nonnull2");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.Comment cmnt = (restrictionsAll.Comment)sf.Comments().make();
                // set fields
            cmnt.text = (string)"null";
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Comments().staticSize());
                    // create objects from file
                    restrictionsAll.Comment cmnt_2 = (restrictionsAll.Comment)sf2.Comments().getByID(cmnt.SkillID);
                    // assert fields
                    Assert.IsTrue(cmnt_2.text != null && cmnt_2.text.Equals("null"));
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range8() {
            string path = tmpFile("range8");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360.1;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0.001;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360.1);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0.001);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range4() {
            string path = tmpFile("range4");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)1L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 1L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_restrictionsAll_acc_example() {
            string path = tmpFile("example");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                restrictionsAll.Operator op = (restrictionsAll.Operator)sf.Operators().make();
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                restrictionsAll.DefaultBoarderCases dbc = (restrictionsAll.DefaultBoarderCases)sf.DefaultBoarderCasess().make();
                restrictionsAll.None none_obj = (restrictionsAll.None)sf.Nones().make();
                restrictionsAll.ZSystem sys_obj = (restrictionsAll.ZSystem)sf.ZSystems().make();
                restrictionsAll.Term trm = (restrictionsAll.Term)sf.Terms().make();
                restrictionsAll.RegularProperty rp = (restrictionsAll.RegularProperty)sf.RegularPropertys().make();
                restrictionsAll.Comment cmnt = (restrictionsAll.Comment)sf.Comments().make();
            // set fields
            op.name = (string)"Minus";

            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0;

            dbc.system = (de.ust.skill.common.csharp.@internal.SkillObject)sys_obj;
            dbc.nopDefault = (long)0L;
            dbc.none = (Properties)none_obj;
            dbc.Zfloat = (float)(float)-1;
            dbc.message = (string)"Hello World!";


            sys_obj.name = (string)"Hexadecimal";
            sys_obj.version = (float)(float)1.1;

            trm.arguments = (System.Collections.ArrayList)array<Term>(trm, (Term) null);
            trm.Zoperator = (Operator)op;


            cmnt.property = (Properties)sys_obj;
            cmnt.text = (string)"A comment";
            cmnt.target = (de.ust.skill.common.csharp.@internal.SkillObject)op;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.DefaultBoarderCasess().staticSize());
                    Assert.AreEqual(1, sf.ZSystems().staticSize());
                    Assert.AreEqual(1, sf.Operators().staticSize());
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    Assert.AreEqual(1, sf.Comments().staticSize());
                    Assert.AreEqual(1, sf.Nones().staticSize());
                    Assert.AreEqual(1, sf.RegularPropertys().staticSize());
                    Assert.AreEqual(1, sf.Terms().staticSize());
                // create objects from file
                    restrictionsAll.Operator op_2 = (restrictionsAll.Operator)sf2.Operators().getByID(op.SkillID);
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    restrictionsAll.DefaultBoarderCases dbc_2 = (restrictionsAll.DefaultBoarderCases)sf2.DefaultBoarderCasess().getByID(dbc.SkillID);
                    restrictionsAll.None none_obj_2 = (restrictionsAll.None)sf2.Nones().getByID(none_obj.SkillID);
                    restrictionsAll.ZSystem sys_obj_2 = (restrictionsAll.ZSystem)sf2.ZSystems().getByID(sys_obj.SkillID);
                    restrictionsAll.Term trm_2 = (restrictionsAll.Term)sf2.Terms().getByID(trm.SkillID);
                    restrictionsAll.RegularProperty rp_2 = (restrictionsAll.RegularProperty)sf2.RegularPropertys().getByID(rp.SkillID);
                    restrictionsAll.Comment cmnt_2 = (restrictionsAll.Comment)sf2.Comments().getByID(cmnt.SkillID);
                // assert fields
                    Assert.IsTrue(op_2.name != null && op_2.name.Equals("Minus"));

                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0);

                    Assert.IsTrue(dbc_2.nopDefault == 0L);
                    Assert.IsTrue(dbc_2.none == none_obj_2);
                    Assert.IsTrue(dbc_2.Zfloat == (float)-1);
                    Assert.IsTrue(dbc_2.message != null && dbc_2.message.Equals("Hello World!"));


                    Assert.IsTrue(sys_obj_2.name != null && sys_obj_2.name.Equals("Hexadecimal"));
                    Assert.IsTrue(sys_obj_2.version == (float)1.1);

                    Assert.IsTrue(trm_2.arguments != null && ArrayListEqual(trm_2.arguments, array<Term>(trm_2, (Term) null )));
                    Assert.IsTrue(trm_2.Zoperator == op_2);


                    Assert.IsTrue(cmnt_2.property == sys_obj_2);
                    Assert.IsTrue(cmnt_2.text != null && cmnt_2.text.Equals("A comment"));
                    Assert.IsTrue(cmnt_2.target == op_2);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_acc_range7b() {
            string path = tmpFile("range7b");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
            // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)0.001;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0.001;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)0.001);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0.001);
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range3() {
            string path = tmpFile("range3");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)1;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)0;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 1);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)0);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_restrictionsAll_acc_twoSingletons() {
            string path = tmpFile("twoSingletons");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                restrictionsAll.None none_1 = (restrictionsAll.None)sf.Nones().make();
                restrictionsAll.None none_2 = (restrictionsAll.None)sf.Nones().make();
            // set fields

            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(2, sf.Nones().staticSize());
                // create objects from file
                    restrictionsAll.None none_1_2 = (restrictionsAll.None)sf2.Nones().getByID(none_1.SkillID);
                    restrictionsAll.None none_2_2 = (restrictionsAll.None)sf2.Nones().getByID(none_2.SkillID);
                // assert fields

            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_unique() {
            string path = tmpFile("unique");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.Operator op_2 = (restrictionsAll.Operator)sf.Operators().make();
                restrictionsAll.Operator op_1 = (restrictionsAll.Operator)sf.Operators().make();
                // set fields
            op_2.name = (string)"Minus";

            op_1.name = (string)"Minus";
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(2, sf.Operators().staticSize());
                    // create objects from file
                    restrictionsAll.Operator op_2_2 = (restrictionsAll.Operator)sf2.Operators().getByID(op_2.SkillID);
                    restrictionsAll.Operator op_1_2 = (restrictionsAll.Operator)sf2.Operators().getByID(op_1.SkillID);
                    // assert fields
                    Assert.IsTrue(op_2_2.name != null && op_2_2.name.Equals("Minus"));

                    Assert.IsTrue(op_1_2.name != null && op_1_2.name.Equals("Minus"));
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_oneOf() {
            string path = tmpFile("oneOf");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RegularProperty rp = (restrictionsAll.RegularProperty)sf.RegularPropertys().make();
                restrictionsAll.Comment cmnt = (restrictionsAll.Comment)sf.Comments().make();
                // set fields

            cmnt.property = (Properties)rp;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Comments().staticSize());
                    Assert.AreEqual(1, sf.RegularPropertys().staticSize());
                    // create objects from file
                    restrictionsAll.RegularProperty rp_2 = (restrictionsAll.RegularProperty)sf2.RegularPropertys().getByID(rp.SkillID);
                    restrictionsAll.Comment cmnt_2 = (restrictionsAll.Comment)sf2.Comments().getByID(cmnt.SkillID);
                    // assert fields

                    Assert.IsTrue(cmnt_2.property == rp_2);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range6() {
            string path = tmpFile("range6");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)0;
            rbc.degrees = (float)(float)-0.001;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)0);
                    Assert.IsTrue(rbc_2.degrees == (float)-0.001);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_nonnull() {
            string path = tmpFile("nonnull");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.Term op = (restrictionsAll.Term)sf.Terms().make();
                // set fields
            op.Zoperator = (Operator)(Operator) null;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.Terms().staticSize());
                    // create objects from file
                    restrictionsAll.Term op_2 = (restrictionsAll.Term)sf2.Terms().getByID(op.SkillID);
                    // assert fields
                    Assert.IsTrue(op_2.Zoperator == (Operator) null);
                }
            }
            catch (SkillException)
            {
                return;
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_restrictions_restrictionsAll_fail_range1() {
            string path = tmpFile("range1");
            try
            {
                SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

                // create objects
                restrictionsAll.RangeBoarderCases rbc = (restrictionsAll.RangeBoarderCases)sf.RangeBoarderCasess().make();
                // set fields
            rbc.negative = (int)0;
            rbc.negative2 = (long)0L;
            rbc.degrees2 = (double)(double)360;
            rbc.positive2 = (short)(short)0;
            rbc.positive = (sbyte)(sbyte)-1;
            rbc.degrees = (float)(float)0;
                sf.close();

                { // read back and assert correctness
                    SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                    // check count per Type
                    Assert.AreEqual(1, sf.RangeBoarderCasess().staticSize());
                    // create objects from file
                    restrictionsAll.RangeBoarderCases rbc_2 = (restrictionsAll.RangeBoarderCases)sf2.RangeBoarderCasess().getByID(rbc.SkillID);
                    // assert fields
                    Assert.IsTrue(rbc_2.negative == 0);
                    Assert.IsTrue(rbc_2.negative2 == 0L);
                    Assert.IsTrue(rbc_2.degrees2 == (double)360);
                    Assert.IsTrue(rbc_2.positive2 == (short)0);
                    Assert.IsTrue(rbc_2.positive == (sbyte)-1);
                    Assert.IsTrue(rbc_2.degrees == (float)0);
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
