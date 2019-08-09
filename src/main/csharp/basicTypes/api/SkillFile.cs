/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System.IO;
using System.Collections.Generic;

using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldTypes;
using SkillState = basicTypes.@internal.SkillState;

namespace basicTypes
{
    namespace api
    {

        /// <summary>
        /// An abstract skill file that is hiding all the dirty implementation details from you.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        public abstract class SkillFile : de.ust.skill.common.csharp.@internal.SkillState, de.ust.skill.common.csharp.api.SkillFile {

            public SkillFile(StringPool strings, string path, Mode mode, List<AbstractStoragePool> types,
                Dictionary<string, AbstractStoragePool> poolByName, StringType stringType, Annotation annotationType)
                : base(strings, path, mode, types, poolByName, stringType, annotationType)
            { }

            /// <summary>
            /// Create a new skill file based on argument path and mode.
            /// </summary>
            public static SkillFile open(string path, params Mode[] mode) {
                FileInfo f = new FileInfo(path);
                return open(f, mode);
            }

            /// <summary>
            /// Create a new skill file based on argument path and mode.
            /// </summary>
            public static SkillFile open(FileInfo path, params Mode[] mode) {
                foreach (Mode m in mode) {
                    if (m == Mode.Create && !path.Exists)
                        path.Create().Close();
                }
                return SkillState.open(path.FullName, mode);
            }

            /// <returns> an access for all BasicBools in this state </returns>
            public abstract @internal.P0 BasicBools();

            /// <returns> an access for all BasicFloat32s in this state </returns>
            public abstract @internal.P1 BasicFloat32s();

            /// <returns> an access for all BasicFloat64s in this state </returns>
            public abstract @internal.P2 BasicFloat64s();

            /// <returns> an access for all BasicFloatss in this state </returns>
            public abstract @internal.P3 BasicFloatss();

            /// <returns> an access for all BasicInt16s in this state </returns>
            public abstract @internal.P4 BasicInt16s();

            /// <returns> an access for all BasicInt32s in this state </returns>
            public abstract @internal.P5 BasicInt32s();

            /// <returns> an access for all BasicInt64Is in this state </returns>
            public abstract @internal.P6 BasicInt64Is();

            /// <returns> an access for all BasicInt64Vs in this state </returns>
            public abstract @internal.P7 BasicInt64Vs();

            /// <returns> an access for all BasicInt8s in this state </returns>
            public abstract @internal.P8 BasicInt8s();

            /// <returns> an access for all BasicIntegerss in this state </returns>
            public abstract @internal.P9 BasicIntegerss();

            /// <returns> an access for all BasicStrings in this state </returns>
            public abstract @internal.P10 BasicStrings();

            /// <returns> an access for all BasicTypess in this state </returns>
            public abstract @internal.P11 BasicTypess();
        }
    }
}
