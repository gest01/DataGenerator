using System;
using System.Linq;
using System.Reflection;

namespace DataGenerator.Cli.ValueGenerators
{

    public class StringValueGenerator : PropertyValueGenerator
    {
        private int _min;
        private int _max;

        public StringValueGenerator(PropertyInfo property)
            :base(property) 
        {
        }

        public StringValueGenerator UseMaxLength(int maxLength)
        {
            if (maxLength <= 0)
                throw new ArgumentException();

            _max = maxLength;
            return this;
        }

        public StringValueGenerator UseMinLength(int minLength)
        {
            if (minLength < 0)
                throw new ArgumentException();

            _min = minLength;
            return this;
        }

        protected override object GenerateValue()
        {
            int len = Random.Next(_min, _max);

            String chars = "qwertzuiopasdfghjklyxcvbnmQWERTZUIOPSDFGHJKLYXCVBNM;:123<>0§456789ç%&/()=?`!èà£à-.¨,$äöé_:;";
            var result = new string(
                Enumerable.Repeat(chars, len)
                          .Select(s => s[Random.Next(chars.Length)])
                          .ToArray());


            return result;
        }
    }
}
