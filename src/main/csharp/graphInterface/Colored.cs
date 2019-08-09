/*  ___ _  ___ _ _      
 * / __| |/ (_) | |     
 * \__ \ ' <| | | |__   
 * |___/_|\_\_|_|____|  
\*                                                                                                                    */

using FieldDeclaration = de.ust.skill.common.csharp.api.FieldDeclaration;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;

namespace graphInterface
{

    /// <summary>
    ///  anything that has a color is colored
    /// </summary>
    public interface Colored  {

        
            string color{ get;set; }

    }
}