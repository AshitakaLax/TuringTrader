using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TuringTraderWin.Algorithm;
using TuringTraderWin.DataSource;
using TuringTraderWin.Instruments;
using TuringTraderWin.Optimizer;
using TuringTraderWin.Orders;
using TuringTraderWin.Simulator;

namespace TuringTraderWin
{
    internal static class Program
  {
    public static IServiceProvider ServiceProvider { get; set; }
    static void ConfigureServices(ServiceCollection services)
    {
      services.AddSingleton<MainWindow>()
        .AddLogging()
        .AddSingleton<ISimulatorManager, SimulatorManager>()
        .AddSingleton<IAlgorithmManager, AlgorithmManager>()
        .AddTransient<ITransactionHistory, TransactionHistory>()
        .AddTransient<IInstrumentManager, InstrumentManager>()
        .AddTransient<ISimulatorCore, SimulatorCore>()
        .AddSingleton<IDataSourceManager, DataSourceManager>()
      .AddSingleton<IOptimizerManager, OptimizerManager>();
    }

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Log.Logger = new LoggerConfiguration()
         .WriteTo.File($"TuringTraderWin.log")
         .CreateLogger();
      ApplicationConfiguration.Initialize();
      //Application.Run(new MainWindow());
      ServiceCollection services = new ServiceCollection(); 
      ConfigureServices(services);
      using (ServiceProvider serviceProvider = services.BuildServiceProvider())
      {
        var form1 = serviceProvider.GetRequiredService<MainWindow>();
        Application.Run(form1);
      }
    }
  }
}