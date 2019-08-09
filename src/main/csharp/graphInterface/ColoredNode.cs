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
    ///  The property of being a colored Node
    /// </summary>
    public interface ColoredNode : Colored, Marker {

        
            graphInterface.Marker next{ get;set; }

            System.Collections.Generic.HashSet<graphInterface.ColoredNode> edges{ get;set; }

            System.Collections.Generic.Dictionary<graphInterface.Node, System.Collections.Generic.Dictionary<graphInterface.ColoredNode, graphInterface.Marker>> map{ get;set; }

            string color{ get;set; }

            string mark{ get;set; }

    }
}
