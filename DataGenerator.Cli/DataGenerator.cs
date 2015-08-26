using DataGenerator.Cli.ValueGenerators;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataGenerator.Cli
{
    public abstract class DataGenerator
    {

        protected DataGenerator(Type dataType)
        {
            DataType = dataType;
            Generators = new List<PropertyValueGenerator>(50);
        }

        public void GenerateValues(Object obj)
        {
            foreach(PropertyValueGenerator generator in Generators)
            {
                generator.GenerateValue(obj);
            }
        }

        protected ICollection<PropertyValueGenerator> Generators { get; private set; }

        public Type DataType { get; private set; }
    }

    public class DataGenerator<T> : DataGenerator
    {
        public DataGenerator()
            : base(typeof(T)) { }

        public BooleanValueGenerator Property(Expression<Func<T, bool>> expr)
        {
            var pu = ExpressionUtil.Property(expr);
            BooleanValueGenerator gen = Generators.OfType<BooleanValueGenerator>().SingleOrDefault(f => f.Property.Name == pu.Name);
            if (gen == null)
            {
                gen = new BooleanValueGenerator(pu);
                Generators.Add(gen);
            }

            return gen;
        }

        public DateTimeValueGenerator Property(Expression<Func<T, DateTime>> expr)
        {
            var pu = ExpressionUtil.Property(expr);
            DateTimeValueGenerator gen = Generators.OfType<DateTimeValueGenerator>().SingleOrDefault(f => f.Property.Name == pu.Name);
            if (gen == null)
            {
                gen = new DateTimeValueGenerator(pu);
                Generators.Add(gen);
            }

            return gen;
        }

        public IntegerValueGenerator Property(Expression<Func<T, int>> expr)
        {
            var pu = ExpressionUtil.Property(expr);
            IntegerValueGenerator gen = Generators.OfType<IntegerValueGenerator>().SingleOrDefault(f => f.Property.Name == pu.Name);
            if (gen == null)
            {
                gen = new IntegerValueGenerator(pu);
                Generators.Add(gen);
            }

            return gen;
        }

        public StringValueGenerator Property(Expression<Func<T, String>> expr)
        {
            var pu = ExpressionUtil.Property(expr);
            StringValueGenerator gen = Generators.OfType<StringValueGenerator>().SingleOrDefault(f => f.Property.Name == pu.Name);
            if (gen == null)
            {
                gen = new StringValueGenerator(pu);
                Generators.Add(gen);
            }

            return gen;
        }
    }
}
