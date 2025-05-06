namespace Lab2;
using System.Xml;

public class ServiciiManager : ProduseServiciiManagerAbstract
{
    
    //citire serviciu/servicii + afisare
    public Serviciu ReadServiciu()
    {
        
        Console.Write("Numele serviciului: "); string? nume = Console.ReadLine();
        Console.Write("Codul intern: "); string? codIntern = Console.ReadLine();
        Console.Write("Pret: "); string? input = Console.ReadLine();
        int pret; while (!int.TryParse(input, out pret))
        {
            Console.Write("Valoare invalida. Reintrodu pretul: ");
            input = Console.ReadLine();
        }
        Console.Write("Categorie: "); string? categorie = Console.ReadLine();
        
        
        Serviciu serviciu = new Serviciu((uint)(Array100.Count + 1), nume, codIntern, (uint)pret, categorie);
        AddElement(serviciu);
        return serviciu;
    }
    public void ReadServicii(uint nr)
    {
        for (uint i = 0; i < nr; i++)
        {
            Console.WriteLine($"\nIntroducere servicii {i + 1}/{nr}:");
            ReadServiciu();
        }
    }
    public void WriteServicii(uint nr)
    {
        Console.WriteLine("\nLista de Servicii:"); int count = 0;
        
        for (int i = 0; i < Array100.Count && count < nr; i++)
        {
            if (Array100[i] is Serviciu)
            {
                Console.WriteLine($"Serviciu #{count + 1}: {Array100[i].Descriere()}");
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Nu exista servicii de afisat.");
        }
    }
    
    
    //metode de gasire
    public bool Contine(Serviciu s)
    {
        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Equals(s)) return true;
        }
        return false;
    }
    public bool Contine(string? numeServiciu)
    {
        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Nume == numeServiciu) return true;
        }
        return false;
    }
    public List<Serviciu> GasesteDupaNume(string nume)
    {
        List<Serviciu> rezultate = new List<Serviciu>();

        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Nume == nume && Array100[i] is Serviciu s)
            {
                rezultate.Add(s);
            }
        }

        return rezultate;
    }
    
    
    //citire xml 
    public void CitesteServiciiDinXml(string caleFisier)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(caleFisier);
        XmlNodeList servicii = doc.SelectNodes("//Serviciu");

        foreach (XmlNode serviciuNode in servicii)
        {
            uint id = uint.Parse(serviciuNode["Id"]!.InnerText);
            string nume = serviciuNode["Nume"]!.InnerText;
            string codIntern = serviciuNode["CodIntern"]!.InnerText;
            uint pret = uint.Parse(serviciuNode["Pret"]!.InnerText);
            string categorie = serviciuNode["Categorie"]!.InnerText;

            var serviciu = new Serviciu(id, nume, codIntern, pret, categorie);
            AddElement(serviciu);
        }
        
    }

    
    
}