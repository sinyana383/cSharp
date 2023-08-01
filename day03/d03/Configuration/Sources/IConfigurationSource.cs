using System.Collections;

namespace d03.Configuration.Sources
{
    public interface IConfigurationSource
    {
        int Priority { get; }
    
        Hashtable LoadData();
        Hashtable GetParameters();
    }
}