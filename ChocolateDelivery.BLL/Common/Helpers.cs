using System.Globalization;
using System.Text.RegularExpressions;

namespace ChocolateDelivery.BLL;

public static class Helpers
{
    public static void WriteToFile(string p_str_path, string p_str_data, bool p_bln_append = true)
    {
        if (!File.Exists(p_str_path))
        {
            var dinfo = Directory.CreateDirectory(p_str_path);
            dinfo.Create();
        }
        if (!File.Exists(p_str_path + FormatDate(DateTime.Today.Date) + ".txt"))
        {
            var fi = new FileInfo(p_str_path + FormatDate(DateTime.Today.Date) + ".txt");
            var fstr = fi.Create();
            fstr.Close();
        }
        var obj = new StreamWriter(p_str_path + FormatDate(DateTime.Today.Date) + ".txt", p_bln_append);
        obj.WriteLine("=========================================================================");
        obj.WriteLine("Date Time : " + DateTime.Now.ToString());
        obj.WriteLine("-------------------------------------------------------------------------");
        obj.Write(p_str_data + "\n");
        obj.WriteLine("=========================================================================");
        obj.Close();
    }

    private static string FormatDate(DateTime Date)
    {
        //declare as constant.
        var format = "dd-MMM-yyyy";
        return Convert.ToDateTime(Date).ToString(format);
    }

    public static string ConvertToPgsqlQuery(this string mySqlQuery)
    {
        var pattern = @"as\s+'([^']+)'";
        var replacement = @"as ""$1""";

        var resultQuery = Regex.Replace(mySqlQuery, pattern, replacement);
        
        var arr = resultQuery.Split(";").ToList();
        var selectQuery = arr.Last();
		
        arr.Remove(selectQuery);
	
        var dict = new Dictionary<string, string>();
        foreach (var element in arr)
        {
            var split = element.Split("=");
            var val = split.Last();
            var key = "@" + split.First().Split("@").Last();
            dict.Add(key, val);
        }

        foreach (var (k,v) in dict)
        {
            selectQuery = selectQuery.Replace(k,v, ignoreCase : true, new CultureInfo("en"));
        }

        return selectQuery;
    }
}