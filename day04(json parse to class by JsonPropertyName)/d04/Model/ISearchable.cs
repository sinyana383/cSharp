using System;
using System.Collections.Generic;
using System.Linq;

namespace d04.Model
{
    public interface ISearchable
    {
        string Title { get; }
        bool IsBest { get; }
    }
    
    static class Extension
    {
        public static T[] Search<T>(this IEnumerable<T> list, string search)
            where T : ISearchable
        {
            T[] res = list
                .Where(el => el.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();
            return res;
        }
        
        public static T Best<T>(this IEnumerable<T> list)
            where T : ISearchable
        {
            T res = list.FirstOrDefault(el => el.IsBest);
            return res;
        }
    }
    
}