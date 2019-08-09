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
using SkillState = fancy.@internal.SkillState;

namespace fancy
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

            /// <returns> an access for all As in this state </returns>
            public abstract @internal.P0 As();

            /// <returns> an access for all Bs in this state </returns>
            public abstract @internal.P1 Bs();

            /// <returns> an access for all Cs in this state </returns>
            public abstract @internal.P2 Cs();

            /// <returns> an access for all Ds in this state </returns>
            public abstract @internal.P3 Ds();

            /// <returns> an access for all Gs in this state </returns>
            public abstract @internal.P4 Gs();

            /// <returns> an access for all Hs in this state </returns>
            public abstract @internal.P5 Hs();

            /// <returns> an access for all Is in this state </returns>
            public abstract @internal.P6 Is();

            /// <returns> an access for all Js in this state </returns>
            public abstract @internal.P7 Js();

            /// <returns> an access for all Es in this state </returns>
            public abstract de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A> Es();

            /// <returns> an access for all Fs in this state </returns>
            public abstract de.ust.skill.common.csharp.@internal.InterfacePool<SkillObject, fancy.A> Fs();
        }
    }
}
