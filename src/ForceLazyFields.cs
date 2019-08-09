using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal.exceptions;

namespace de.ust.skill.common.csharp
{
    namespace @internal
    {

        public abstract class ForceLazyFields
        {
            private ForceLazyFields()
            {
                // no instance
            }

            public static void forceFullCheck(SkillFile skillFile)
            {
                loadAll(skillFile);
                try
                {
                    skillFile.check();
                }
                catch (SkillException e)
                {
                    // convert to parse exception
                    throw new ParseException(e, "a check failed");
                }
            }

            public static void loadAll(SkillFile skillFile)
            {
                foreach (IAccess t in skillFile.allTypes())
                {
                    StaticFieldIterator fs = t.fields();
                    while (fs.hasNext())
                    {
                        AbstractFieldDeclaration f = fs.next();
                        if (f is ILazyField)
                            ((ILazyField)f).ensureLoaded();
                    }
                }
            }
        }
    }
}
