using System;
using System.Configuration;

namespace PowerShellRunnerService.Config
{
    public class ScriptsCollection : ConfigurationElementCollection
    {
        public ScriptsCollection()
        {
            ScriptConfigElement script = (ScriptConfigElement)CreateNewElement();
            Add(script);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ScriptConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ScriptConfigElement)element).Name;
        }

        public ScriptConfigElement this[int index]
        {
            get { return (ScriptConfigElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new ScriptConfigElement this[string Name]
        {
            get { return (ScriptConfigElement)BaseGet(Name); }
        }

        public int IndexOf(ScriptConfigElement script)
        {
            return BaseIndexOf(script);
        }

        public void Add(ScriptConfigElement script)
        {
            BaseAdd(script);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(ScriptConfigElement script)
        {
            if (BaseIndexOf(script) >= 0)
                BaseRemove(script.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
