using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Cli.ValueGenerators
{
    public class BooleanValueGenerator : PropertyValueGenerator
    {
        public BooleanValueGenerator(PropertyInfo pi)
            :base(pi) { }

        protected override object GenerateValue()
        {
            return Random.Next(0, 10) % 2 == 0;
        }
    }
}
