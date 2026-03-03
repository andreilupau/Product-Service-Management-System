namespace Lab2;

public class PachetManager : ProduseServiciiManagerAbstract
{
    //Lista strict de Pachete
    protected static List<Pachet> Pachet100 = new List<Pachet>();
    
    public IReadOnlyList<Pachet> GetPachete()
    {
        return Pachet100.AsReadOnly();
    }
    
    public void AddPachet(Pachet pachet)
    {
        if (pachet == null) throw new ArgumentNullException(nameof(pachet));
        Pachet100.Add(pachet);
    }

    public void ClearPachete()
    {
        Pachet100.Clear();
    }

    public bool RemovePachet(Pachet pachet)
    {
        return Pachet100.Remove(pachet);
    }
    
    //Userul creaza pachetul si continutul din el dupa plac
    public Pachet ReadPachet()
    {
        Console.WriteLine("\nBun venit in meniu creare Pachet (A-Z)!");

        uint nextPachetId = (uint)(Pachet100.Count + 1);

        Console.Write("Alege numele pachetlui: ");
        string nume = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(nume))
            nume = $"Pachet_{nextPachetId}";

        Console.Write("Cod intern (ENTER pentru auto): ");
        string codIntern = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(codIntern))
            codIntern = $"PKT{nextPachetId:D3}";

        Console.Write("Categorie (ENTER pentru 'Pachet'): ");
        string categorie = Console.ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(categorie))
            categorie = "Pachet";

        // pachetul pornește cu pret 0; se va recalcula din componente
        Pachet pachet = new Pachet(nextPachetId, nume, codIntern, categorie);

        
        // Acum userul alege ce adauga
        var produseManager = new ProduseManager();
        var serviciiManager = new ServiciiManager();
        while (true)
        {
            Console.WriteLine("\nAdaugare in pachet:");
            Console.WriteLine("1. Adauga produs (maxim 1)");
            Console.WriteLine("2. Adauga serviciu (nelimitat)");
            Console.WriteLine("3. Incarca din XML si alege elemente");
            Console.WriteLine("0. Gata (salveaza pachetul)");
            Console.Write("Alege: ");
            string? opt = Console.ReadLine();

            if (opt == "1")
            {
                Console.WriteLine("Introduceti detaliile produsului:");
                Produs produs = produseManager.ReadProdus();
                pachet.AdaugaLaPachet(produs);
            }
            else if (opt == "2")
            {
                Console.WriteLine("Introduceti detaliile serviciului:");
                Serviciu serviciu = serviciiManager.ReadServiciu();
                pachet.AdaugaLaPachet(serviciu);
            }
            else if (opt == "3")
            {
                produseManager.CitesteProduseDinXml("Data/p_s.xml");
                serviciiManager.CitesteServiciiDinXml("Data/p_s.xml");
                Console.WriteLine("Datele au fost incarcate din XML.");

                var produse = ProduseServiciiManagerAbstract.GetArray().OfType<Produs>().ToList();
                var servicii = ProduseServiciiManagerAbstract.GetArray().OfType<Serviciu>().ToList();

                bool hasProdusInPachet = pachet.GetElements().Any(e => e is Produs);
                if (!hasProdusInPachet && produse.Count > 0)
                {
                    Console.WriteLine("\nProduse disponibile din XML:");
                    foreach (var produsXml in produse)
                    {
                        Console.WriteLine($"[Produs] ID: {produsXml.Id}, Nume: {produsXml.Nume}, Pret: {produsXml.Pret}");
                    }

                    Console.Write("Introdu ID produs pentru adaugare (ENTER pentru skip): ");
                    string? prodIdInput = Console.ReadLine();
                    if (uint.TryParse(prodIdInput, out uint prodId))
                    {
                        var produsSelectat = produse.FirstOrDefault(p => p.Id == prodId);
                        if (produsSelectat != null)
                        {
                            pachet.AdaugaLaPachet(produsSelectat);
                        }
                        else
                        {
                            Console.WriteLine("ID produs invalid.");
                        }
                    }
                }
                else if (hasProdusInPachet)
                {
                    Console.WriteLine("Pachetul are deja un produs. Nu se mai poate adauga altul.");
                }
                else
                {
                    Console.WriteLine("Nu exista produse disponibile in XML.");
                }

                if (servicii.Count > 0)
                {
                    Console.WriteLine("\nServicii disponibile din XML:");
                    foreach (var serviciuXml in servicii)
                    {
                        Console.WriteLine($"[Serviciu] ID: {serviciuXml.Id}, Nume: {serviciuXml.Nume}, Pret: {serviciuXml.Pret}");
                    }

                    while (true)
                    {
                        Console.Write("Introdu ID serviciu pentru adaugare (0 pentru stop): ");
                        string? servIdInput = Console.ReadLine();
                        if (!uint.TryParse(servIdInput, out uint servId))
                        {
                            Console.WriteLine("ID invalid.");
                            continue;
                        }

                        if (servId == 0)
                        {
                            break;
                        }

                        var serviciuSelectat = servicii.FirstOrDefault(s => s.Id == servId);
                        if (serviciuSelectat != null)
                        {
                            pachet.AdaugaLaPachet(serviciuSelectat);
                        }
                        else
                        {
                            Console.WriteLine("ID serviciu invalid.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nu exista servicii disponibile in XML.");
                }
            }
            
            
            else if (opt == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Optiune invalida.");
            }
        }

        Pachet100.Add(pachet);
        Console.WriteLine("Pachetul a fost creat cu succes!");
        return pachet;
        
    }
    
    public void ReadPachete(int nrPachete, int current = 0)
    {
        if (current >= nrPachete)
        {
            return; // Cazul de bază: am citit toate pachetele
        }

        Console.WriteLine($"\nCitire pachet {current + 1}/{nrPachete}:");
        ReadPachet(); // Apel corect al metodei
        ReadPachete(nrPachete, current + 1); // Apel recursiv
    } 
    
    
    
    public void WriteSortedPachete()
    {
        Console.WriteLine("\nAfisare lista pachete existente:"); //din Pachet100

        if (Pachet100.Count == 0)
        {
            Console.WriteLine("Nu există pachete de afisat!");
            return;
        }

        var pachete = Pachet100.ToList();
        pachete.Sort((p1, p2) => p1.Pret.CompareTo(p2.Pret));

        foreach (var pachet in pachete)
        {
            Console.WriteLine(pachet.Descriere());
        }
    }

    public void WritePacheteContinut()
    {
        Console.WriteLine("\nAfisarea continutului din pachetele existente:"); //din Pachet100

        if (Pachet100.Count == 0)
        {
            Console.WriteLine("Nu exista pachete de afisat!");
            return;
        }

        foreach (var pachet in Pachet100)
        {
            Console.WriteLine(pachet.Descriere());

            var elements = pachet.GetElements();
            if (elements.Count == 0)
            {
                Console.WriteLine("  (pachet gol)");
                continue;
            }

            foreach (var element in elements)
            {
                if (element is ProdusServiciuAbstract psa)
                {
                    Console.WriteLine($"  - {psa.Descriere()}");
                }
                else
                {
                    Console.WriteLine($"  - Element (Pret: {element.Pret})");
                }
            }
        }
    }
    
    
    
}
