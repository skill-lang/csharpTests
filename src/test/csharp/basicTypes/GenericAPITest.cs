using System.Collections.Generic;
using System.Linq;
using System.IO;

using NUnit.Framework;

using SkillFile = basicTypes.api.SkillFile;
using SkillException= de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;

namespace basicTypes
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericAPITest : common.CommonTest {

        [Test]
        public void APITest_core_basicTypes_acc_all() {
            string path = tmpFile("all");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                basicTypes.BasicTypes all = (basicTypes.BasicTypes)sf.BasicTypess().make();
                basicTypes.BasicInt64I all_aUserType_obj_int64I_obj = (basicTypes.BasicInt64I)sf.BasicInt64Is().make();
                basicTypes.BasicFloats all_anotherUserType_obj = (basicTypes.BasicFloats)sf.BasicFloatss().make();
                basicTypes.BasicIntegers all_aUserType_obj = (basicTypes.BasicIntegers)sf.BasicIntegerss().make();
                basicTypes.BasicInt32 all_aUserType_obj_int32_obj = (basicTypes.BasicInt32)sf.BasicInt32s().make();
                basicTypes.BasicFloat32 all_anotherUserType_obj_float32_obj = (basicTypes.BasicFloat32)sf.BasicFloat32s().make();
                basicTypes.BasicInt64V all_aUserType_obj_int64V_obj = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicFloat64 all_anotherUserType_obj_float64_obj = (basicTypes.BasicFloat64)sf.BasicFloat64s().make();
                basicTypes.BasicInt8 all_aUserType_obj_int8_obj = (basicTypes.BasicInt8)sf.BasicInt8s().make();
                basicTypes.BasicInt16 all_aUserType_obj_int16_obj = (basicTypes.BasicInt16)sf.BasicInt16s().make();
                basicTypes.BasicBool all_aBool_obj = (basicTypes.BasicBool)sf.BasicBools().make();
                basicTypes.BasicString all_aString_obj = (basicTypes.BasicString)sf.BasicStrings().make();
            // set fields
            all.aUserType = (BasicIntegers)all_aUserType_obj;
            all.aString = (BasicString)all_aString_obj;
            all.aList = (System.Collections.Generic.List<System.Single>)list<float>((float)3, (float)4);
            all.aMap = (System.Collections.Generic.Dictionary<System.Int16, System.SByte>)put(map<short, sbyte >(), (short)5, (sbyte)6);
            all.anArray = (System.Collections.ArrayList)array<BasicIntegers>(all_aUserType_obj);
            all.anAnnotation = (de.ust.skill.common.csharp.@internal.SkillObject)all_aBool_obj;
            all.anotherUserType = (BasicFloats)all_anotherUserType_obj;
            all.aSet = (System.Collections.Generic.HashSet<System.SByte>)set<sbyte>((sbyte)2);
            all.aBool = (BasicBool)all_aBool_obj;

            all_aUserType_obj_int64I_obj.basicInt = (long)0L;

            all_anotherUserType_obj.float32 = (BasicFloat32)all_anotherUserType_obj_float32_obj;
            all_anotherUserType_obj.float64 = (BasicFloat64)all_anotherUserType_obj_float64_obj;

            all_aUserType_obj.int32 = (BasicInt32)all_aUserType_obj_int32_obj;
            all_aUserType_obj.int8 = (BasicInt8)all_aUserType_obj_int8_obj;
            all_aUserType_obj.int64V = (BasicInt64V)all_aUserType_obj_int64V_obj;
            all_aUserType_obj.int64I = (BasicInt64I)all_aUserType_obj_int64I_obj;
            all_aUserType_obj.int16 = (BasicInt16)all_aUserType_obj_int16_obj;

            all_aUserType_obj_int32_obj.basicInt = (int)-1;

            all_anotherUserType_obj_float32_obj.basicFloat = (float)(float)1;

            all_aUserType_obj_int64V_obj.basicInt = (long)1L;

            all_anotherUserType_obj_float64_obj.basicFloat = (double)(double)2;

            all_aUserType_obj_int8_obj.basicInt = (sbyte)(sbyte)-3;

            all_aUserType_obj_int16_obj.basicInt = (short)(short)-2;

            all_aBool_obj.basicBool = (bool)true;

            all_aString_obj.basicString = (string)"Hello World!";
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(1, sf.BasicInt32s().staticSize());
                    Assert.AreEqual(1, sf.BasicIntegerss().staticSize());
                    Assert.AreEqual(1, sf.BasicFloat64s().staticSize());
                    Assert.AreEqual(1, sf.BasicFloatss().staticSize());
                    Assert.AreEqual(1, sf.BasicBools().staticSize());
                    Assert.AreEqual(1, sf.BasicInt64Vs().staticSize());
                    Assert.AreEqual(1, sf.BasicInt16s().staticSize());
                    Assert.AreEqual(1, sf.BasicStrings().staticSize());
                    Assert.AreEqual(1, sf.BasicInt64Is().staticSize());
                    Assert.AreEqual(1, sf.BasicTypess().staticSize());
                    Assert.AreEqual(1, sf.BasicInt8s().staticSize());
                    Assert.AreEqual(1, sf.BasicFloat32s().staticSize());
                // create objects from file
                    basicTypes.BasicTypes all_2 = (basicTypes.BasicTypes)sf2.BasicTypess().getByID(all.SkillID);
                    basicTypes.BasicInt64I all_aUserType_obj_int64I_obj_2 = (basicTypes.BasicInt64I)sf2.BasicInt64Is().getByID(all_aUserType_obj_int64I_obj.SkillID);
                    basicTypes.BasicFloats all_anotherUserType_obj_2 = (basicTypes.BasicFloats)sf2.BasicFloatss().getByID(all_anotherUserType_obj.SkillID);
                    basicTypes.BasicIntegers all_aUserType_obj_2 = (basicTypes.BasicIntegers)sf2.BasicIntegerss().getByID(all_aUserType_obj.SkillID);
                    basicTypes.BasicInt32 all_aUserType_obj_int32_obj_2 = (basicTypes.BasicInt32)sf2.BasicInt32s().getByID(all_aUserType_obj_int32_obj.SkillID);
                    basicTypes.BasicFloat32 all_anotherUserType_obj_float32_obj_2 = (basicTypes.BasicFloat32)sf2.BasicFloat32s().getByID(all_anotherUserType_obj_float32_obj.SkillID);
                    basicTypes.BasicInt64V all_aUserType_obj_int64V_obj_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(all_aUserType_obj_int64V_obj.SkillID);
                    basicTypes.BasicFloat64 all_anotherUserType_obj_float64_obj_2 = (basicTypes.BasicFloat64)sf2.BasicFloat64s().getByID(all_anotherUserType_obj_float64_obj.SkillID);
                    basicTypes.BasicInt8 all_aUserType_obj_int8_obj_2 = (basicTypes.BasicInt8)sf2.BasicInt8s().getByID(all_aUserType_obj_int8_obj.SkillID);
                    basicTypes.BasicInt16 all_aUserType_obj_int16_obj_2 = (basicTypes.BasicInt16)sf2.BasicInt16s().getByID(all_aUserType_obj_int16_obj.SkillID);
                    basicTypes.BasicBool all_aBool_obj_2 = (basicTypes.BasicBool)sf2.BasicBools().getByID(all_aBool_obj.SkillID);
                    basicTypes.BasicString all_aString_obj_2 = (basicTypes.BasicString)sf2.BasicStrings().getByID(all_aString_obj.SkillID);
                // assert fields
                    Assert.IsTrue(all_2.aUserType == all_aUserType_obj_2);
                    Assert.IsTrue(all_2.aString == all_aString_obj_2);
                    Assert.IsTrue(all_2.aList != null && Enumerable.SequenceEqual(all_2.aList, list<float>((float)3, (float)4 )));
                    Assert.IsTrue(all_2.aMap != null && Enumerable.SequenceEqual(all_2.aMap, put(map<short, sbyte >(), (short)5, (sbyte)6)));
                    Assert.IsTrue(all_2.anArray != null && ArrayListEqual(all_2.anArray, array<BasicIntegers>(all_aUserType_obj_2 )));
                    Assert.IsTrue(all_2.anAnnotation == all_aBool_obj_2);
                    Assert.IsTrue(all_2.anotherUserType == all_anotherUserType_obj_2);
                    Assert.IsTrue(all_2.aSet != null && Enumerable.SequenceEqual(all_2.aSet, set<sbyte>((sbyte)2 )));
                    Assert.IsTrue(all_2.aBool == all_aBool_obj_2);

                    Assert.IsTrue(all_aUserType_obj_int64I_obj_2.basicInt == 0L);

                    Assert.IsTrue(all_anotherUserType_obj_2.float32 == all_anotherUserType_obj_float32_obj_2);
                    Assert.IsTrue(all_anotherUserType_obj_2.float64 == all_anotherUserType_obj_float64_obj_2);

                    Assert.IsTrue(all_aUserType_obj_2.int32 == all_aUserType_obj_int32_obj_2);
                    Assert.IsTrue(all_aUserType_obj_2.int8 == all_aUserType_obj_int8_obj_2);
                    Assert.IsTrue(all_aUserType_obj_2.int64V == all_aUserType_obj_int64V_obj_2);
                    Assert.IsTrue(all_aUserType_obj_2.int64I == all_aUserType_obj_int64I_obj_2);
                    Assert.IsTrue(all_aUserType_obj_2.int16 == all_aUserType_obj_int16_obj_2);

                    Assert.IsTrue(all_aUserType_obj_int32_obj_2.basicInt == -1);

                    Assert.IsTrue(all_anotherUserType_obj_float32_obj_2.basicFloat == (float)1);

                    Assert.IsTrue(all_aUserType_obj_int64V_obj_2.basicInt == 1L);

                    Assert.IsTrue(all_anotherUserType_obj_float64_obj_2.basicFloat == (double)2);

                    Assert.IsTrue(all_aUserType_obj_int8_obj_2.basicInt == (sbyte)-3);

                    Assert.IsTrue(all_aUserType_obj_int16_obj_2.basicInt == (short)-2);

                    Assert.IsTrue(all_aBool_obj_2.basicBool == true);

                    Assert.IsTrue(all_aString_obj_2.basicString != null && all_aString_obj_2.basicString.Equals("Hello World!"));
            }
            File.Delete(path);
        }

        [Test]
        public void APITest_core_basicTypes_acc_roland() {
            string path = tmpFile("roland");
            SkillFile sf = SkillFile.open(path, Mode.Create, Mode.Write);

            // create objects
                basicTypes.BasicInt64V v_0_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_1_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_0_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_1_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_2_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_0_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_3_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_4_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_5_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_4_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_5_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_6_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_1_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_2_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_3_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_2_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_3_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_4_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_7_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_8_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_9_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_8_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_9_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_5_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_6_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_7_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_6_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_7_b = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_8_a = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
                basicTypes.BasicInt64V v_9_c = (basicTypes.BasicInt64V)sf.BasicInt64Vs().make();
            // set fields
            v_0_b.basicInt = (long)0L;

            v_1_a.basicInt = (long)129L;

            v_0_c.basicInt = (long)-1L;

            v_1_b.basicInt = (long)128L;

            v_2_a.basicInt = (long)16385L;

            v_0_a.basicInt = (long)1L;

            v_3_c.basicInt = (long)2097151L;

            v_4_b.basicInt = (long)268435456L;

            v_5_a.basicInt = (long)34359738369L;

            v_4_c.basicInt = (long)268435455L;

            v_5_b.basicInt = (long)34359738368L;

            v_6_a.basicInt = (long)4398046511105L;

            v_1_c.basicInt = (long)127L;

            v_2_b.basicInt = (long)16384L;

            v_3_a.basicInt = (long)2097153L;

            v_2_c.basicInt = (long)16383L;

            v_3_b.basicInt = (long)2097152L;

            v_4_a.basicInt = (long)268435457L;

            v_7_c.basicInt = (long)562949953421311L;

            v_8_b.basicInt = (long)72057594037927936L;

            v_9_a.basicInt = (long)-9223372036854775807L;

            v_8_c.basicInt = (long)72057594037927935L;

            v_9_b.basicInt = (long)-9223372036854775808L;

            v_5_c.basicInt = (long)34359738367L;

            v_6_b.basicInt = (long)4398046511104L;

            v_7_a.basicInt = (long)562949953421313L;

            v_6_c.basicInt = (long)4398046511103L;

            v_7_b.basicInt = (long)562949953421312L;

            v_8_a.basicInt = (long)72057594037927937L;

            v_9_c.basicInt = (long)9223372036854775807L;
            sf.close();

            { // read back and assert correctness
                SkillFile sf2 = SkillFile.open(sf.currentPath(), Mode.Read, Mode.ReadOnly);
                // check count per Type
                    Assert.AreEqual(30, sf.BasicInt64Vs().staticSize());
                // create objects from file
                    basicTypes.BasicInt64V v_0_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_0_b.SkillID);
                    basicTypes.BasicInt64V v_1_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_1_a.SkillID);
                    basicTypes.BasicInt64V v_0_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_0_c.SkillID);
                    basicTypes.BasicInt64V v_1_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_1_b.SkillID);
                    basicTypes.BasicInt64V v_2_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_2_a.SkillID);
                    basicTypes.BasicInt64V v_0_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_0_a.SkillID);
                    basicTypes.BasicInt64V v_3_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_3_c.SkillID);
                    basicTypes.BasicInt64V v_4_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_4_b.SkillID);
                    basicTypes.BasicInt64V v_5_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_5_a.SkillID);
                    basicTypes.BasicInt64V v_4_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_4_c.SkillID);
                    basicTypes.BasicInt64V v_5_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_5_b.SkillID);
                    basicTypes.BasicInt64V v_6_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_6_a.SkillID);
                    basicTypes.BasicInt64V v_1_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_1_c.SkillID);
                    basicTypes.BasicInt64V v_2_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_2_b.SkillID);
                    basicTypes.BasicInt64V v_3_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_3_a.SkillID);
                    basicTypes.BasicInt64V v_2_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_2_c.SkillID);
                    basicTypes.BasicInt64V v_3_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_3_b.SkillID);
                    basicTypes.BasicInt64V v_4_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_4_a.SkillID);
                    basicTypes.BasicInt64V v_7_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_7_c.SkillID);
                    basicTypes.BasicInt64V v_8_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_8_b.SkillID);
                    basicTypes.BasicInt64V v_9_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_9_a.SkillID);
                    basicTypes.BasicInt64V v_8_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_8_c.SkillID);
                    basicTypes.BasicInt64V v_9_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_9_b.SkillID);
                    basicTypes.BasicInt64V v_5_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_5_c.SkillID);
                    basicTypes.BasicInt64V v_6_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_6_b.SkillID);
                    basicTypes.BasicInt64V v_7_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_7_a.SkillID);
                    basicTypes.BasicInt64V v_6_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_6_c.SkillID);
                    basicTypes.BasicInt64V v_7_b_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_7_b.SkillID);
                    basicTypes.BasicInt64V v_8_a_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_8_a.SkillID);
                    basicTypes.BasicInt64V v_9_c_2 = (basicTypes.BasicInt64V)sf2.BasicInt64Vs().getByID(v_9_c.SkillID);
                // assert fields
                    Assert.IsTrue(v_0_b_2.basicInt == 0L);

                    Assert.IsTrue(v_1_a_2.basicInt == 129L);

                    Assert.IsTrue(v_0_c_2.basicInt == -1L);

                    Assert.IsTrue(v_1_b_2.basicInt == 128L);

                    Assert.IsTrue(v_2_a_2.basicInt == 16385L);

                    Assert.IsTrue(v_0_a_2.basicInt == 1L);

                    Assert.IsTrue(v_3_c_2.basicInt == 2097151L);

                    Assert.IsTrue(v_4_b_2.basicInt == 268435456L);

                    Assert.IsTrue(v_5_a_2.basicInt == 34359738369L);

                    Assert.IsTrue(v_4_c_2.basicInt == 268435455L);

                    Assert.IsTrue(v_5_b_2.basicInt == 34359738368L);

                    Assert.IsTrue(v_6_a_2.basicInt == 4398046511105L);

                    Assert.IsTrue(v_1_c_2.basicInt == 127L);

                    Assert.IsTrue(v_2_b_2.basicInt == 16384L);

                    Assert.IsTrue(v_3_a_2.basicInt == 2097153L);

                    Assert.IsTrue(v_2_c_2.basicInt == 16383L);

                    Assert.IsTrue(v_3_b_2.basicInt == 2097152L);

                    Assert.IsTrue(v_4_a_2.basicInt == 268435457L);

                    Assert.IsTrue(v_7_c_2.basicInt == 562949953421311L);

                    Assert.IsTrue(v_8_b_2.basicInt == 72057594037927936L);

                    Assert.IsTrue(v_9_a_2.basicInt == -9223372036854775807L);

                    Assert.IsTrue(v_8_c_2.basicInt == 72057594037927935L);

                    Assert.IsTrue(v_9_b_2.basicInt == -9223372036854775808L);

                    Assert.IsTrue(v_5_c_2.basicInt == 34359738367L);

                    Assert.IsTrue(v_6_b_2.basicInt == 4398046511104L);

                    Assert.IsTrue(v_7_a_2.basicInt == 562949953421313L);

                    Assert.IsTrue(v_6_c_2.basicInt == 4398046511103L);

                    Assert.IsTrue(v_7_b_2.basicInt == 562949953421312L);

                    Assert.IsTrue(v_8_a_2.basicInt == 72057594037927937L);

                    Assert.IsTrue(v_9_c_2.basicInt == 9223372036854775807L);
            }
            File.Delete(path);
        }

    }
}
