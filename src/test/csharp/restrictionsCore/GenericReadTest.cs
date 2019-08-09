using System.Collections;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

using IAccess = de.ust.skill.common.csharp.api.IAccess;
using SkillException = de.ust.skill.common.csharp.api.SkillException;
using Mode = de.ust.skill.common.csharp.api.Mode;
using ForceLazyFields = de.ust.skill.common.csharp.@internal.ForceLazyFields;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using SkillFile = restrictionsCore.api.SkillFile;

namespace restrictionsCore
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
        public void test_restrictionsCore_read_accept_age_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/age.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_age16_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/age16.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_ageUnrestricted_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/ageUnrestricted.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_aircraft_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/aircraft.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_annotationNull_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/annotationNull.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_annotationString_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/annotationString.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_annotationTest_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/annotationTest.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_coloredNodes_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/coloredNodes.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_container_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/container.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_crossNodes_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/crossNodes.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_date_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/date.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_emptyBlocks_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/emptyBlocks.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_emptyFile_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/[[all]]/accept/emptyFile.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_fourColoredNodes_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/fourColoredNodes.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_localBasePoolOffset_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/localBasePoolOffset.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_noFieldRegressionTest_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/noFieldRegressionTest.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_nodeFirstBlockOnly_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/nodeFirstBlockOnly.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_restrictionsAll_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/restrictionsAll.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_trivialType_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/trivialType.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_twoNodeBlocks_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/twoNodeBlocks.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_twoTypes_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/twoTypes.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_accept_unicode_reference_sf() {
            Assert.IsNotNull(read("src/test/resources/genbinary/restrictionsCore/accept/unicode-reference.sf"));
        }

        [Test]
        public void test_restrictionsCore_read_reject_duplicateDefinition_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/duplicateDefinition.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_duplicateDefinitionMixed_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/duplicateDefinitionMixed.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_duplicateDefinitionSecondBlock_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/duplicateDefinitionSecondBlock.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_illegalStringPoolOffsets_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/illegalStringPoolOffsets.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_illegalTypeID_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/illegalTypeID.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_missingUserType_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/[[all]]/fail/missingUserType.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_nullAsFieldName_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/restrictionsCore/fail/nullAsFieldName.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_nullInNonNullNode_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/restrictionsCore/fail/nullInNonNullNode.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

        [Test]
        public void test_restrictionsCore_read_reject_partial_sf() {
            try {
                ForceLazyFields.forceFullCheck(read("src/test/resources/genbinary/restrictionsCore/fail/partial.sf"));
                Assert.Fail("Expected ParseException to be thrown");
            } catch (SkillException e) {
                return; // success
            }
        }

    }
}
