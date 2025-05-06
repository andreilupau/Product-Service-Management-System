namespace Lab2;
using System.Xml;

public class ProduseManager : ProduseServiciiManagerAbstract
{
    
    ////citire produs/produse + afisare
    public Produs ReadProdus()
    {
        //citire produs 
        Console.Write("Numele produsului: "); string? nume = Console.ReadLine();
        Console.Write("Codul intern: "); string? codIntern = Console.ReadLine();
        Console.Write("Producător: "); string? producator = Console.ReadLine(); //unic
        Console.Write("Pret: "); string? input = Console.ReadLine();
        int pret; while (!int.TryParse(input, out pret))
        {
            Console.Write("Valoare invalida. Reintrodu pretul: ");
            input = Console.ReadLine();
        }
        Console.Write("Categorie: "); string? categorie = Console.ReadLine();

        
        Produs produs = new Produs((uint)(Array100.Count), nume, codIntern, producator, (uint) pret, categorie);
        AddElement(produs);
        return produs;
    }
    public void ReadProduse(uint nr)
    {
        for (int i = 0; i < nr; i++)
        {
            Console.WriteLine($"\nIntroduceti produse {i + 1}/{nr}:");
            ReadProdus();
        }
    }
    public void WriteProduse(uint nr)
    {
        Console.WriteLine("\nLista de Produse:"); int count = 0;
        
        for (int i = 0; i < Array100.Count && count < nr; i++)
        {
            if (Array100[i] is Produs)
            {
                Console.WriteLine($"Produs #{count + 1}: {Array100[i].Descriere()}");
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Nu exista produse de afisat.");
        }
    }
    
    
    //metode de gasire
    public bool Contine(Produs p)
    {
        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Equals(p)) return true;
        }
        return false;
    }
    public bool Contine(string? numeProdus)
    {
        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Nume == numeProdus) return true;
        }
        return false;
    }
    public List<Produs> GasesteDupaNume(string nume)
    {
        List<Produs> rezultate = new List<Produs>();

        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Nume == nume && Array100[i] is Produs p)
            {
                rezultate.Add(p);
            }
        }

        return rezultate;
    }

    //citire xml
    public void CitesteProduseDinXml(string caleFisier)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(caleFisier);
        XmlNodeList produse = doc.SelectNodes("//Produs");

        foreach (XmlNode produsNode in produse)
        {
            uint id = uint.Parse(produsNode["Id"]!.InnerText);
            string nume = produsNode["Nume"]!.InnerText;
            string codIntern = produsNode["CodIntern"]!.InnerText;
            string producator = produsNode["Producator"]!.InnerText;
            uint pret = uint.Parse(produsNode["Pret"]!.InnerText);
            string categorie = produsNode["Categorie"]!.InnerText;

            var produs = new Produs(id, nume, codIntern, producator, pret, categorie);
            AddElement(produs);
        }
    }

    
}