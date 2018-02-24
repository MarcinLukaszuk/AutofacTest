using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IOutput
{
    void Write(string content);
}


public class ConsoleOutput : IOutput
{
    public void Write(string content)
    {
        Console.WriteLine(content);
    }
}


public interface IDateWriter
{
    void WriteDate();
}


public class TodayWriter : IDateWriter
{
    private IOutput _output;
    public TodayWriter(IOutput output)
    {
        this._output = output;
    }

    public void WriteDate()
    {
        this._output.Write(DateTime.Today.ToShortDateString());
    }
}

namespace AutofacTest
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            WriteData();
            Console.ReadLine();
        }


        public static void WriteData()
        {
            using (var scope=Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IOutput>();
                writer.Write("elo");
            }



        }



    }
}
