using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using de.ust.skill.common.csharp.api;
using de.ust.skill.common.csharp.@internal;
using de.ust.skill.common.csharp.@internal.fieldDeclarations;
using de.ust.skill.common.csharp.@internal.fieldTypes;

namespace common
{

    /// <summary>
    /// Some test code commonly used by all tests.
    ///
    /// @author Timm Felden
    /// </summary>
    public abstract class CommonTest
    {

        /**
         * This constant is used to guide reflective init
         */
        private static readonly int reflectiveInitSize = 10;

        public static readonly string basePath = "C:/Users/Simon/GitLab/Bachelorarbeit/skill/";

        public CommonTest() : base()
        {
        }

        protected static string createFile(string packagePath, string name)
        {
            DirectoryInfo dir = new DirectoryInfo(basePath + "src/test/resources/serializedTestfiles/" + packagePath);
            if (!dir.Exists)
            {
                dir.Create();
            }
            FileInfo file = new FileInfo(basePath + "src/test/resources/serializedTestfiles/" + packagePath + name + ".sf");
            if (file.Exists)
            {
                file.Delete();
            }
            return file.FullName;
        }

        protected static string tmpFile(string name)
        {
            string tmpPath = Path.GetTempFileName();
            File.Delete(tmpPath);
            FileInfo r = new FileInfo(tmpPath.Replace(".tmp", name + ".sf"));
            r.Create().Close();
            //r.deleteOnExit();
            return r.FullName;
        }

        protected static string sha256(string name)
        {
            return sha256(new FileInfo(basePath + "src/test/resources/" + name));
        }

        protected static string sha256(FileInfo path)
        {
            FileStream file = path.OpenRead();
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, (int)file.Length);
            string sb = "";
            foreach (byte b in new SHA256Managed().ComputeHash(bytes))
            {
                sb += string.Format("%02X", b);
            }
            return sb;
        }

        protected static void reflectiveInit(SkillFile sf)
        {
            // create instances
            foreach (AbstractStoragePool t in sf.allTypesStream())
            {
                try
                {
                    for (int j = reflectiveInitSize; j != 0; j--)
                    {
                        t.make();
                    }
                }
                catch (SkillException e)
                {
                    // the type can not have more instances
                }
            }

            // set fields
            foreach (AbstractStoragePool t in sf.allTypesStream())
            {
                foreach (SkillObject o in t)
                {
                    StaticFieldIterator it = t.fields();
                    while (it.hasNext())
                    {
                        AbstractFieldDeclaration f = it.next();
                        if (!(f is IAutoField) && !(f.Type is IConstantIntegerType) && !(f is InterfaceField))
                        {
                            set<object, SkillObject>(sf, o, f);
                        }
                    }
                }
            }
        }

        private static void set<T, Obj>(SkillFile sf, Obj o, AbstractFieldDeclaration f) where Obj : SkillObject
        {
            T v = value<T>(sf, (de.ust.skill.common.csharp.@internal.FieldType)f.Type);
            // System.out.printf("%s#%d.%s = %s\n", o.getClass().getName(),
            // o.getSkillID(), f.name(), v.toString());
            o.set(f, v);
        }

        /**
         * unchecked, because the insane amount of casts is necessary to reflect the
         * implicit value based type system
         */
        private static T value<T>(SkillFile sf, de.ust.skill.common.csharp.@internal.FieldType type)
        {
            if (type is IGeneralAccess)
            {
                // get a random object
                IEnumerator @is = ((IGeneralAccess)type).GetEnumerator();
                for (int i = new Random().Next(reflectiveInitSize) % 200; i != 0; i--)
                {
                    @is.MoveNext();
                }
                return (T)@is.Current;
            }

            switch (type.TypeID)
            {
                case 5:
                    // random type
                    IEnumerator ts = sf.allTypes().GetEnumerator();
                    ts.MoveNext();
                    IAccess t = (IAccess)ts.Current;
                    for (int i = new Random().Next(200); i != 0 && ts.MoveNext(); i--)
                    {
                        t = (IAccess)ts.Current;
                    }

                    // random object
                    IEnumerator @is = t.GetEnumerator();
                    for (int i = new Random().Next(Math.Min(200, reflectiveInitSize)); i != 0; i--)
                    {
                        @is.MoveNext();
                    }
                    @is.MoveNext();
                    return (T)@is.Current;

                case 6:
                    return (T)(object)(Boolean)(new Random().Next(1) != 0);
                case 7:
                    return (T)(object)(SByte)new Random().Next(reflectiveInitSize);
                case 8:
                    return (T)(object)(Int16)new Random().Next(reflectiveInitSize);
                case 9:
                    return (T)(object)(Int32)new Random().Next(reflectiveInitSize);
                case 10:
                case 11:
                    return (T)(object)(Int64)(new Random().Next() % reflectiveInitSize);
                case 12:
                    return (T)(object)(Single)new Random().NextDouble();
                case 13:
                    return (T)(object)(Double)new Random().NextDouble();
                case 14:
                    return (T)(object)"☢☢☢";

                case 15:
                    {
                        IConstantLengthArray cla = (IConstantLengthArray)type;
                        ArrayList rval = new ArrayList(cla.GetLength());
                        for (int i = cla.GetLength(); i != 0; i--)
                        {
                            rval.Add(value<object>(sf, cla.GetGroundType()));
                        }
                        return (T)(object)rval;
                    }
                case 17:
                    {
                        ISingleArgumentType cla = (ISingleArgumentType)type;
                        int length = (int)Math.Sqrt(reflectiveInitSize);
                        ArrayList rval = new ArrayList(length);
                        while (0 != length--)
                        {
                            rval.Add(value<object>(sf, cla.GetGroundType()));
                        }
                        return (T)(object)rval;
                    }
                case 18:
                    {
                        ISingleArgumentType cla = (ISingleArgumentType)type;
                        int length = (int)Math.Sqrt(reflectiveInitSize);
                        List<Object> rval = new List<Object>();
                        while (0 != length--)
                        {
                            rval.Add(value<object>(sf, cla.GetGroundType()));
                        }
                        return (T)(object)rval;
                    }
                case 19:
                    {
                        ISingleArgumentType cla = (ISingleArgumentType)type;
                        int length = (int)Math.Sqrt(reflectiveInitSize);
                        HashSet<Object> rval = new HashSet<Object>();
                        while (0 != length--)
                        {
                            rval.Add(value<object>(sf, cla.GetGroundType()));
                        }
                        return (T)(object)rval;
                    }
                case 20:
                    return (T)(object)new Dictionary<Object, Object>();
                default:
                    throw new Exception();
            }
        }

        protected ArrayList array<T>(params T[] ts)
        {
            ArrayList rval = new ArrayList();
            foreach (T t in ts)
            {
                rval.Add(t);
            }
            return rval;
        }

        protected List<T> list<T>(params T[] ts)
        {
            List<T> rval = new List<T>();
            foreach (T t in ts)
            {
                rval.Add(t);
            }
            return rval;
        }

        protected HashSet<T> set<T>(params T[] ts)
        {
            HashSet<T> rval = new HashSet<T>();
            foreach (T t in ts)
            {
                rval.Add(t);
            }
            return rval;
        }

        protected Dictionary<K, V> map<K, V>()
        {
            return new Dictionary<K, V>();
        }

        protected Dictionary<K, V> put<K, V>(Dictionary<K, V> m, K key, V value)
        {
            m[key] = value;
            return m;
        }

        protected bool ArrayListEqual(ArrayList l1, ArrayList l2)
        {
            if (l1 == null && l2 == null)
            {
                return true;
            }
            else if (l1 == null || l2 == null)
            {
                return false;
            }

            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                if (!(l1[i]==l2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
