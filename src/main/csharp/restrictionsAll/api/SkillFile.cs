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
using SkillState = restrictionsAll.@internal.SkillState;

namespace restrictionsAll
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

            /// <returns> an access for all Comments in this state </returns>
            public abstract @internal.P0 Comments();

            /// <returns> an access for all DefaultBoarderCasess in this state </returns>
            public abstract @internal.P1 DefaultBoarderCasess();

            /// <returns> an access for all Operators in this state </returns>
            public abstract @internal.P2 Operators();

            /// <returns> an access for all Propertiess in this state </returns>
            public abstract @internal.P3 Propertiess();

            /// <returns> an access for all Nones in this state </returns>
            public abstract @internal.P4 Nones();

            /// <returns> an access for all RegularPropertys in this state </returns>
            public abstract @internal.P5 RegularPropertys();

            /// <returns> an access for all ZSystems in this state </returns>
            public abstract @internal.P6 ZSystems();

            /// <returns> an access for all RangeBoarderCasess in this state </returns>
            public abstract @internal.P7 RangeBoarderCasess();

            /// <returns> an access for all Terms in this state </returns>
            public abstract @internal.P8 Terms();
        }
    }
}
