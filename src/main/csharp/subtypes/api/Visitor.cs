/*  ___ _  ___ _ _                                                                                                   *\
 * / __| |/ (_) | |     Your SKilL csharp Binding                                                                    * 
 * \__ \ ' <| | | |__   <<debug>>                                                                                    * 
 * |___/_|\_\_|_|____|  by: <<some developer>>                                                                       * 
\*                                                                                                                    */

using System;

namespace subtypes
{
    namespace api
    {

        /// <summary>
        /// Base class of a distributed dispatching function ranging over specified types
        /// implemented by the visitor pattern.
        ///
        /// @author Simon Glaub, Timm Felden
        /// </summary>
        /// <param id =_R> the result type </param>
        /// <param id =_A> the argument type </param>
        /// <param id =_E> the type of throws exception; use RuntimeException for nothrow </param>
        public abstract class Visitor<_R, _A, _E> where _E : Exception{
            public abstract _R visit(subtypes.A self, _A arg);
            public abstract _R visit(subtypes.B self, _A arg);
            public abstract _R visit(subtypes.D self, _A arg);
            public abstract _R visit(subtypes.C self, _A arg);
        }
    }
}
