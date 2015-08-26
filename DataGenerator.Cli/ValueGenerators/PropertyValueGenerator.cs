using System;
using System.Reflection;

namespace DataGenerator.Cli.ValueGenerators
{
    public abstract class PropertyValueGenerator
    {
        private Random _rand;

        public PropertyValueGenerator(PropertyInfo property)
        {
            Property = property;

            _rand = new Random((int)DateTime.Now.Ticks);
        }

        public PropertyInfo Property { get; private set; }

        protected Random Random { get { return _rand; } }

        public void GenerateValue(Object obj)
        {
            Object value = GenerateValue();
            if (value != null)
            {
                Property.SetValue(obj, value);
            }
        }

        public PropertyValueGenerator WithFixValue(Object value)
        {
            return this;
        }

        protected virtual void Reset()
        {
            _rand = new Random((int)DateTime.Now.Ticks);
        }

        protected virtual Object GenerateValue()
        {
            return null;
        }
    }
}
