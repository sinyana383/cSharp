using System.Collections;
using System.Text;
using d03.Configuration.Sources;

namespace d03.Configuration
{
    public class Configuration
    {
        private Hashtable _setOfParams;
        private int _curPriority;

        public Configuration(IConfigurationSource source)
        {
            _setOfParams = source.GetParameters();
            _curPriority = source.Priority;
        }

        public void LoadMoreConfigData(IConfigurationSource newSource)
        {
            foreach (DictionaryEntry param in newSource.GetParameters())
            {
                if (!_setOfParams.ContainsKey(param.Key))
                {
                    _setOfParams.Add(param.Key, param.Value);
                    continue;
                }

                if (_curPriority < newSource.Priority)
                    _setOfParams[param.Key] = param.Value;
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if (_setOfParams.Count == 0)
                return stringBuilder.ToString();
        
            stringBuilder.Append("Configuration\n");
            foreach (DictionaryEntry param in _setOfParams)
            {
                stringBuilder.Append($"{param.Key}: {param.Value}");
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}