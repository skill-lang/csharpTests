/*  ___ _  ___ _ _      
 * / __| |/ (_) | |     
 * \__ \ ' <| | | |__   
 * |___/_|\_\_|_|____|  
\*                                                                                                                    */

using FieldDeclaration = de.ust.skill.common.csharp.api.FieldDeclaration;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;

namespace fancy
{

    public interface E  {

        
            fancy.C Value{ get;set; }

            /// <summary>
        ///  this type makes the annotation test incompatible with subtypes
        /// </summary>
        de.ust.skill.common.csharp.@internal.SkillObject a{ get;set; }

            fancy.A Parent{ get;set; }

    }
}