namespace RoboterDatenverwaltung;

public class Roboter : ISerializable
{
    public Roboter(string name, string typ, int energielevel)
    {
        Name = name;
        Typ = typ;
        Energielevel = energielevel;
    }
    public Roboter()
    {
        Name = "Unbekannt";
        Typ = "Unbekannt";
    }
    public string Name { get; set; }
    public string Typ { get; set; } // z. B. "Lieferroboter", "Schwimmroboter", etc.
    public int Energielevel { get; set; }


    public Dictionary<string, string> zuDictionary()
    {
        if (this is Lieferroboter lieferroboter)
            return lieferroboter.zuDictionary();
        return new Dictionary<string, string>
        {
            { "Name", Name },
            { "Typ", Typ },
            { "Energielevel", Energielevel.ToString() }
        };
    }
    public static ISerializable vonDictionary(Dictionary<string, string> dict)
    {
        string typ = dict.GetValueOrDefault("Typ", "Unbekannt");
        if (typ == "Lieferroboter")
            return Lieferroboter.vonDictionary(dict);
        string name = dict.GetValueOrDefault("Name", "Unbekannt");
        int energieLevel = int.TryParse(dict.GetValueOrDefault("Energielevel", "0"), out int el) ? el : 0;
        return new Roboter(
            name,
            typ,
            energieLevel
        );
    }

    public virtual string GetStatus()
    {
        return $"Roboter - Name: {Name}, Typ: {Typ}, Energielevel: {Energielevel}";
    }

    public virtual void Activate()
    {
        if (Energielevel > 0)
        {
            Console.WriteLine("activated");
            Energielevel--;
            return;
        }

        Console.WriteLine("energy depleted");
    }
}

public class Lieferroboter : Roboter
{
    public int Lieferkapazität { get; set; }
    public Lieferroboter() : base()
    {
        Typ = "Lieferroboter";
    }
    public Lieferroboter(string name, int energielevel, int lieferkapazität) : base(name, "Lieferroboter", energielevel)
    {
        Lieferkapazität = lieferkapazität;
    }

    public new Dictionary<string, string> zuDictionary()
    {
        var result = new Dictionary<string, string>
        {
            { "Name", Name },
            { "Typ", Typ },
            { "Energielevel", Energielevel.ToString() }
        };
        if (this is Lieferroboter lieferroboter)
            result["Lieferkapazität"] = lieferroboter.Lieferkapazität.ToString();    
        return result;
    }
    public static new ISerializable vonDictionary(Dictionary<string, string> dict)
    {
        string typ = dict.GetValueOrDefault("Typ", "Unbekannt");
        if (typ != "Lieferroboter")
            throw new ArgumentException("Der übergebene Dictionary entspricht nicht einem Lieferroboter.");
        string name = dict.GetValueOrDefault("Name", "Unbekannt");
        int energieLevel = int.TryParse(dict.GetValueOrDefault("Energielevel", "0"), out int el) ? el : 0;
        int lieferkapazität = int.TryParse(dict.GetValueOrDefault("Lieferkapazität", "0"), out int lk) ? lk : 0;
        return new Lieferroboter(name, energieLevel, lieferkapazität);
    }

    public override string GetStatus()
    {
        return $"Lieferroboter - Name: {Name}, Typ: {Typ}, Energielevel: {Energielevel}, Lieferkapazität: {Lieferkapazität}";
    }
}