using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataGenerator.Cli.ValueGenerators
{
    public class IntegerValueGenerator : PropertyValueGenerator
    {
        private int _min;
        private int _max;

        public IntegerValueGenerator(PropertyInfo property)
            :base(property) 
        {
        }

        public IntegerValueGenerator UseMax(int value)
        {
            _max = value;
            return this;
        }

        public IntegerValueGenerator UseMin(int value)
        {
            _min = value;
            return this;
        }

        private bool _isUnique;
        public IntegerValueGenerator IsUnique(bool isUnique)
        {
            _isUnique = isUnique;
            return this;
        }

        protected override object GenerateValue()
        {
            return GetNextValue();
        }

        private readonly HashSet<int> _set = new HashSet<int>();
        private int GetNextValue()
        {
            var value = Random.Next(_min, _max);
            if (_isUnique)
            {
                if (_set.Contains(value))
                {
                    return GetNextValue();
                }

                _set.Add(value);
            }

            return value;
        }

        protected override void Reset()
        {
            base.Reset();

            _set.Clear();
        }

    }
}
