using System.Collections;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

using IAccess = de.ust.skill.common.csharp.api.IAccess;
using SkillException = de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;
using ForceLazyFields = de.ust.skill.common.csharp.@internal.ForceLazyFields;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using SkillFile = subtypes.api.SkillFile;

namespace subtypes
{

    /// <summary>
    /// Tests the file reading capabilities.
    /// </summary>
    [TestFixture]
    public class GenericReadTest : common.CommonTest {
        public SkillFile read(string s) {
            return SkillFile.open(basePath + s, Mode.Read, Mode.ReadOnly);
        }

        [Test]
        public void writeGeneric() {
            string path = tmpFile("write.generic");
            SkillFile sf = SkillFile.open(path);
            reflectiveInit(sf);
            sf.close();
            File.Delete(path);
        }

        [Test]
        public void writeGenericChecked() {
            string path = tmpFile("write.generic.checked");
            SkillFile sf = SkillFile.open(path);
            reflectiveInit(sf);
            // write file
            sf.flush();

            // create a name -> type map
            Dictionary<string, IAccess> types = new Dictionary<string, IAccess>();
            foreach (IAccess t in sf.allTypes())
                types[t.Name] = t;

            // read file and check skill IDs
            SkillFile sf2 = SkillFile.open(path, Mode.Read);
            foreach (IAccess t in sf2.allTypes()) {
                IEnumerator os = types[t.Name].GetEnumerator();
                foreach (SkillObject o in t) {
                    Assert.IsTrue(os.MoveNext(), "to few instances in read state");
                    Assert.AreEqual(o.SkillID, ((SkillObject)os.Current).SkillID);
                }
                Assert.IsFalse(os.MoveNext(), "to many instances in read state");
            }
            File.Delete(path);
        }

        [Test]
        public void test_subtypes_read_accept_age_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/age.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_ageUnrestricted_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/ageUnrestricted.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_coloredNodes_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/coloredNodes.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_crossNodes_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/crossNodes.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_date_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/date.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_emptyBlocks_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/emptyBlocks.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_emptyFile_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/[[all]]/accept/emptyFile.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_localBasePoolOffset_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/localBasePoolOffset.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_noFieldRegressionTest_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/noFieldRegressionTest.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_nodeFirstBlockOnly_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/nodeFirstBlockOnly.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_trivialType_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/trivialType.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_twoNodeBlocks_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/twoNodeBlocks.sf"));
        }

        [Test]
        public void test_subtypes_read_accept_unicode_reference_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/subtypes/accept/unicode-reference.sf"));
        }

        [Test]
        public void test_subtypes_read_reject_duplicateDefinition_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/duplicateDefinition.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_subtypes_read_reject_duplicateDefinitionMixed_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/duplicateDefinitionMixed.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_subtypes_read_reject_duplicateDefinitionSecondBlock_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/duplicateDefinitionSecondBlock.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_subtypes_read_reject_illegalStringPoolOffsets_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/illegalStringPoolOffsets.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_subtypes_read_reject_illegalTypeID_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/illegalTypeID.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_subtypes_read_reject_missingUserType_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/missingUserType.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_subtypes_read_reject_twoTypes_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/subtypes/fail/twoTypes.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

    }
}
