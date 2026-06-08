namespace RoboterDatenverwaltung;

public interface ISerializable
{
    Dictionary<string, string> zuDictionary();
    static abstract ISerializable vonDictionary(Dictionary<string, string> dict);
    static string zuJson(Dictionary<string, string> dict) => System.Text.Json.JsonSerializer.Serialize(dict);
    static Dictionary<string, string> vonJson(string json) => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
    static string zuCsv(Dictionary<string, string> dict)
    {
        string keys = string.Join(",", dict.Keys);
        string values = string.Join(",", dict.Values);
        return keys + "\n" + values;
    }
    static Dictionary<string, string> vonCsv(string csv)
    {
        string[] zeilen = csv.Split('\n');
        if (zeilen.Length < 2) return new Dictionary<string, string>();

        string[] keys = zeilen[0].Split(',');
        string[] values = zeilen[1].Split(',');

        var dict = new Dictionary<string, string>();
        for (int i = 0; i < keys.Length && i < values.Length; i++)
        {
            dict[keys[i]] = values[i];
        }
        return dict;
    }
}