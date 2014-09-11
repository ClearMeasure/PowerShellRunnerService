using System;
using System.Configuration;

namespace PowerShellRunnerService.Config
{
    public class ScriptConfigElement : ConfigurationElement
    {
        public ScriptConfigElement(String name, String words)
        {
            this.Name = name;
            this.PathToScript = words;
        }

        public ScriptConfigElement()
        {

            //this.Name = "Testing";
            //this.PathToScript = "Foo Bar";
            //this.Weight = 0;
            //this.Type = "HIT";
        }

        [ConfigurationProperty("name", DefaultValue = "Default Name",
            IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("caseSensitive", DefaultValue = "false",
            IsRequired = false, IsKey = true)]
        [RegexStringValidator(@"true|TRUE|1|false|FALSE|0")]
        public string CaseSensitive
        {
            get { return (string)this["caseSensitive"]; }
            set { this["caseSensitive"] = value; }
        }

        [ConfigurationProperty("pathToScript", DefaultValue = "",
            IsRequired = true)]
        public string PathToScript
        {
            get { return (string)this["pathToScript"]; }
            set { this["pathToScript"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = "COUNT",
            IsRequired = true)]
        [RegexStringValidator(@"LOOP|COUNT|SCHEDULED")]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("hour", DefaultValue = (int)1, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 24, ExcludeRange = false)]
        public int Hour
        {
            get { return (int)this["hour"]; }
            set { this["hour"] = value; }
        }
        
        [ConfigurationProperty("minute", DefaultValue = (int)0, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 59, ExcludeRange = false)]
        public int Minute
        {
            get { return (int)this["minute"]; }
            set { this["minute"] = value; }
        }

        [ConfigurationProperty("day", DefaultValue = (int)1, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 7, ExcludeRange = false)]
        public int Day
        {
            get { return (int)this["day"]; }
            set { this["day"] = value; }
        }

        [ConfigurationProperty("count", DefaultValue = (int)1, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 100, ExcludeRange = false)]
        public int Count
        {
            get { return (int)this["count"]; }
            set { this["count"] = value; }
        }
    }
}
