/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using FieldDeclaration = de.ust.skill.common.csharp.api.FieldDeclaration;
using NamedType = de.ust.skill.common.csharp.@internal.NamedType;
using SkillObject = de.ust.skill.common.csharp.@internal.SkillObject;
using AbstractStoragePool = de.ust.skill.common.csharp.@internal.AbstractStoragePool;

namespace graphInterface
{

    /// <summary>
    ///  a marker interface intended to check propper multiple inheritance translation
    /// </summary>
    public interface Marker  {

        
            string mark{ get;set; }

    }
}
