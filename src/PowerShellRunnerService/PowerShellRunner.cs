using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Management.Automation;
using System.Threading;
using PowerShellRunnerService.Config;

namespace PowerShellRunnerService
{
    public class PowerShellRunner
    {
        private static bool _doWork;
        private static PowerShell _instance;
        public void Start()
        {
            _doWork = true;

            Console.WriteLine("Executing scripts defined in the configuration file:");
            while (_doWork)
            {
                try
                {
                    ProcessScripts();
                }
                catch (ConfigurationErrorsException err)
                {
                    Console.WriteLine("ReadCustomSection(string): {0}", err.ToString());
                }
            }
        }

        public void Stop()
        {
            _doWork = false;
        }

        public void ProcessScripts()
        {
            PSRunnerSection psRunnerSection =
                ConfigurationManager.GetSection("PSRunner") as PSRunnerSection;

            if (psRunnerSection == null)
                throw new ApplicationException("Failed to load PSRunnerSection.");
            else
            {
                for (int i = 0; i < psRunnerSection.Scripts.Count; i++)
                {
                    var scriptElement = psRunnerSection.Scripts[i];
                    if (File.Exists(scriptElement.PathToScript))
                    {
                        ScriptStat ss = StatService.InitScriptStat(psRunnerSection.Scripts[i]);

                        switch (ss.Type)
                        {
                            case "LOOP":
                                ExecuteScript(scriptElement);
                                break;

                            case "COUNT":
                                if(ss.Count < scriptElement.Count)
                                    ExecuteScript(scriptElement);
                                break;

                            case "SCHEDULED":
                                throw new NotImplementedException();
                                break;
                        }

                        ss.Count = ss.Count + 1;
                        ss.LastRan = DateTime.Now;
                        StatService.UpdateStats(ss);
                    }
                }
            }
        }

        public void ExecuteScript(ScriptConfigElement e)
        {
            using (_instance = PowerShell.Create())
            {
                _instance.AddScript(File.ReadAllText(e.PathToScript));

                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
                outputCollection.DataAdded += outputCollection_DataAdded;
                _instance.Streams.Progress.DataAdded += Progress_DataAdded;
                _instance.Streams.Error.DataAdded += Error_DataAdded;
                _instance.Streams.Verbose.DataAdded += Verbose_DataAdded;
                _instance.Streams.Debug.DataAdded += Debug_DataAdded;
                _instance.Streams.Warning.DataAdded += Warning_DataAdded;

                IAsyncResult result = _instance.BeginInvoke<PSObject, PSObject>(null,
                    outputCollection);

                while (result.IsCompleted == false)
                {
                    Thread.Sleep(500);
                }

                foreach (PSObject o in outputCollection)
                {
                    Console.WriteLine(o.GetType());
                    Console.WriteLine(o);
                }
            }
        }

        
        void Warning_DataAdded(object sender, DataAddedEventArgs e)
        {
            Console.Write("Warning:");
            var record = _instance.Streams.Warning[e.Index];
            Console.WriteLine(record.Message);
        }

        void Debug_DataAdded(object sender, DataAddedEventArgs e)
        {
            Console.Write("Debug: ");
            var record = _instance.Streams.Debug[e.Index];
            Console.WriteLine(record.Message);
        }

        void Progress_DataAdded(object sender, DataAddedEventArgs e)
        {
            Console.Write("Progress: ");
            var record = _instance.Streams.Progress[e.Index];
            Console.WriteLine(record.SecondsRemaining);
        }

        void Verbose_DataAdded(object sender, DataAddedEventArgs e)
        {
            Console.Write("Verbose: ");
            var record = _instance.Streams.Verbose[e.Index];
            Console.WriteLine(record.Message);
        }

        void outputCollection_DataAdded(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<PSObject> myp = (PSDataCollection<PSObject>)sender;

            Collection<PSObject> results = myp.ReadAll();
            foreach (PSObject result in results)
            {
                Console.WriteLine(result.ToString());
            }
        }

        void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            Console.Write("Error: ");
            var record = _instance.Streams.Error[e.Index];
            Console.WriteLine(record.ErrorDetails);
            Console.WriteLine(record.Exception);
        }
    }
}