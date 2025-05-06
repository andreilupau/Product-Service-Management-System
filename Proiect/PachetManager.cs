namespace Lab2;

public class PachetManager : ProduseServiciiManagerAbstract
{
    
    public Pachet ReadPachet()
    {
        Console.WriteLine("Citire pachet (produs si serviciu):");
        
        //refolosim metodele ReadProdus și ReadServiciu
        ProduseManager produseManager = new ProduseManager();
        ServiciiManager serviciiManager = new ServiciiManager();

        // Citim un produs
        Console.WriteLine("Introduceti detaliile produsului:");
        Produs produs = produseManager.ReadProdus();

        // Citim un serviciu
        Console.WriteLine("Introduceti detaliile serviciului:");
        Serviciu serviciu = serviciiManager.ReadServiciu();

        // Generăm datele generale pentru pachet
        string nume = $"Pachet_{Array100.Count + 1}";
        string codIntern = $"PKT{(Array100.Count + 1):D3}";
        string categorie = produs.Categorie ?? "Pachet";
        uint pret = produs.Pret + serviciu.Pret;

        // Creăm pachetul
        Pachet pachet = new Pachet((uint)(Array100.Count + 1), nume, codIntern, pret, categorie);

        // Adăugăm elementele în pachet
        pachet.AdaugaLaPachet(produs);
        pachet.AdaugaLaPachet(serviciu);

        // Adăugăm pachetul în lista Array100
        AddElement(pachet);

        Console.WriteLine("Pachetul a fost creat cu succes!");
        return pachet;
        

    } //ex1
    public void ReadPachete(int nrPachete, int current = 0)
    {
        if (current >= nrPachete)
        {
            return; // Cazul de bază: am citit toate pachetele
        }

        Console.WriteLine($"\nCitire pachet {current + 1}/{nrPachete}:");
        ReadPachet(); // Apel corect al metodei
        ReadPachete(nrPachete, current + 1); // Apel recursiv
    } //ex2
    
    //ex4L6
    public void WriteSortedPachete()
    {
        Console.WriteLine("\nLista pachete sortate după preț:");
        var pachete = Array100.OfType<Pachet>().ToList();
        if (pachete.Count == 0)
        {
            Console.WriteLine("Nu există pachete de afișat!");
            return;
        }
        pachete.Sort((p1, p2) => p1.Pret.CompareTo(p2.Pret));
        foreach (var pachet in pachete)
        {
            Console.WriteLine(pachet.Descriere());
        }
    }
}