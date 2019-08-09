/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using FieldDeclaration = de.ust.skill.common.csharp.api.FieldDeclaration;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;

namespace fancy
{

    public interface F  {

        
            System.Collections.Generic.Dictionary<fancy.E, fancy.F> aMap{ get;set; }

            fancy.C Value{ get;set; }

            /// <summary>
        ///  this type makes the annotation test incompatible with subtypes
        /// </summary>
        de.ust.skill.common.csharp.@internal.SkillObject a{ get;set; }

            fancy.A Parent{ get;set; }

    }
}
