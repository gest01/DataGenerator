using System;
using System.Reflection;

namespace DataGenerator.Cli.ValueGenerators
{
    public class DateTimeValueGenerator : PropertyValueGenerator
    {
        public DateTimeValueGenerator(PropertyInfo property)
            : base(property) { }


        private DateTime _from;
        private DateTime _to;
        public DateTimeValueGenerator Range(DateTime from, DateTime to)
        {
            _to = to;
            _from = from;
            return this;
        }

        private bool _dateOnly;
        public DateTimeValueGenerator DateOnly(bool dateOnly)
        {
            _dateOnly = dateOnly;
            return this;
        }

        protected override object GenerateValue()
        {
            return GetNextValue();
        }

        private DateTime GetNextValue()
        {
            var span =  TimeSpan.FromTicks(_to.Ticks - _from.Ticks);

            int dsf = Random.Next(1, (int)span.TotalDays);

            var rsult = _from.AddDays(dsf);

            if (_dateOnly)
                return rsult.Date;

            return rsult;
        }
    }
}
