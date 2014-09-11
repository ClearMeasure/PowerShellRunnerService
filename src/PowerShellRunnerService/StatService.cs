using System.Collections.Generic;
using PowerShellRunnerService.Config;

namespace PowerShellRunnerService
{
    public class StatService
    {
        private static Dictionary<string, ScriptStat> _stats = new Dictionary<string, ScriptStat>();

        public StatService()
        {
            
        }

        public static void UpdateStats(ScriptStat stat)
        {
            if (!_stats.ContainsKey(stat.Script))
            {
                _stats.Add(stat.Script, stat);
            }
            else
            {
                _stats[stat.Script] = stat;
            }
        }

        public static ScriptStat InitScriptStat(ScriptConfigElement e)
        {
            ScriptStat ss;

            if (_stats.ContainsKey(e.PathToScript))
            {
                ss = _stats[e.PathToScript];
            }
            else
            {
                ss = new ScriptStat();
                ss.Script = e.PathToScript;
                ss.Type = e.Type;
            }

            return ss;
        }
    }
}
