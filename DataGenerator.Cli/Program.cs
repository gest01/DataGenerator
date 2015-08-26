using System;

namespace DataGenerator.Cli
{
    class Program
    {
        public int Bla { get; set; }
        public int Id { get; set; }
        public String Name {get;set; }

        public bool TestFlag { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateOnly { get; set; }

        static void Main(string[] args)
        {
            Generate.ForClass<Program>().Property(f => f.TestFlag);
            Generate.ForClass<Program>().Property(f => f.Bla).IsUnique(false).UseMin(12).UseMax(123333);
            Generate.ForClass<Program>().Property(f => f.Id).IsUnique(true).UseMin(12).UseMax(550);
            Generate.ForClass<Program>().Property(f => f.Name).UseMaxLength(123).UseMinLength(22);
            Generate.ForClass<Program>().Property(f => f.Date).Range(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(150));
            Generate.ForClass<Program>().Property(f => f.DateOnly).Range(DateTime.Now.AddDays(-100), DateTime.Now.AddDays(150)).DateOnly(true);

            var items = Generate.Items<Program>(500);
        }

    }
}
