using System;

namespace PowerShellRunnerService
{
    public struct ScriptStat
    {
        public string Script { get; set; }
        public DateTime LastRan { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}