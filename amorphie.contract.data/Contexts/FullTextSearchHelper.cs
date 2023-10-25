using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.data.Contexts
{
    public static class FullTextSearchHelper
    {
        //Can throw error if argument is null or empty. May be database-specific (although all ought to support double quotes with the right settings).
    public static string GetIdentifierSql(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("The name must not be null or empty.");
        }
        return "\"" + name.Replace("\"", "") + "\"";
    }
 
    //PostgreSQL-specific. The columns must be text columns (which can be nullable). In general, config should be "english".
    public static string GetTsVectorComputedColumnSql(string config, IEnumerable<string> columns)
    {
        var filteredColumns = columns?.Where(item => !string.IsNullOrEmpty(item.ToString()));
        if (filteredColumns == null || !filteredColumns.Any())
        {
            return "";
        }
        return "to_tsvector('" + config.Replace("\'", "") + "', " + string.Join(" || ' ' || ", filteredColumns.Select(item => "coalesce(" + GetIdentifierSql(item.ToString()) + ", '')")) + ")";
    }
        
    }
}