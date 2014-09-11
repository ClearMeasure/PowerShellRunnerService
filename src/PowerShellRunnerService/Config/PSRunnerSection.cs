using System.Configuration;

namespace PowerShellRunnerService.Config
{
    public class PSRunnerSection : ConfigurationSection
    {

        [ConfigurationProperty("Scripts", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ScriptsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ScriptsCollection Scripts
        {
            get
            {
                ScriptsCollection scriptsCollection =
                    (ScriptsCollection)base["Scripts"];
                return scriptsCollection;
            }
        }

    }
}
