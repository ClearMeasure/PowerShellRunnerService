using System.Diagnostics.Eventing;
using System.Linq;
using Topshelf;

namespace PowerShellRunnerService
{
    class Program
    {
        
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<PowerShellRunner>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => new PowerShellRunner());
                    serviceConfigurator.WhenStarted(myService => myService.Start());
                    serviceConfigurator.WhenStopped(myService => myService.Stop());
                });

                hostConfigurator.RunAsLocalSystem();

                hostConfigurator.SetDisplayName("PowerShellRunnerService");
                hostConfigurator.SetDescription("This service runs powershell scripts as needed");
                hostConfigurator.SetServiceName("PowerShellRunnerService");
            });
        }
    }
}
