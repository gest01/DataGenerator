using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Cli
{
    public static class Generate
    {
        static readonly Dictionary<Type, DataGenerator> _generators = new Dictionary<Type, DataGenerator>();

        public static DataGenerator<T> ForClass<T>()
        {
            Type t = typeof(T);
            if (!_generators.ContainsKey(t))
            {
                _generators.Add(t, new DataGenerator<T>());
            }


            return (DataGenerator<T>)_generators[t];
        }

        public static IEnumerable<T> Items<T>(int count) where T : new()
        {
            List<T> items = new List<T>(count);

            Type type = typeof(T);
            if (_generators.ContainsKey(type))
            {
                DataGenerator generator = _generators[type];
                for (int i = 0; i < count; i++)
                {
                    T instance = (T)Activator.CreateInstance(type);

                    generator.GenerateValues(instance);

                    items.Add(instance);
                }
            }


            return items;
        }
    }

}
