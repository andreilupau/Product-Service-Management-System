namespace Lab2;

class Program
{
    public static void Main(string[] args)
    {
        //cream o instanta a fiecarui manager, pt. a putea accesa metodele
        ProduseManager pm = new ProduseManager();
        ServiciiManager sm = new ServiciiManager();
        
        //citire din consola / info. din xml
        while (true)
        {
            //alegerea optiunii (1. consola, 2. fisier xml)
            Console.Write("Alege metoda de introducere: 1.Consolă 2.Fişier XML 0.Iesire\nAlegeti: ");
            string? optiune = Console.ReadLine(); 
            
            if (optiune == "1")
            {
                pm.ReadProdus();
                sm.ReadServiciu();
                break;
            }
            else if (optiune == "2")
            {
                pm.CitesteProduseDinXml("p_s.xml");
                sm.CitesteServiciiDinXml("p_s.xml");
                break;
            }
            else if (optiune == "0")
            {
                Console.WriteLine("Ieșire din program.");
                Environment.Exit(0);
            }
            else
            {
                Console.Write("\nOptiunea nu exista!");
            }
        }
        
        
        
        Produs p1 = new Produs(7, "Motocultor", "SV94", "BOSCH", 700, "Agricole");
        Serviciu s1 = new Serviciu(6, "Cositor", "UJ93", 200, "SRL");
        //adaugam obiecte in Array100 si le afisam
        pm.AddElement(p1); sm.AddElement(s1); pm.Write2Console();

        
        //interogari afisare linq
        bool ruleaza = true; 
        while (ruleaza)
        {
            Console.WriteLine("\n----- Interogări LINQ -----");
            Console.WriteLine("1. Afisează toate produsele din categoria 'Agricole'");
            Console.WriteLine("2. Afisează toate serviciile cu pret sub 300");
            Console.WriteLine("3. Grupeaza elementele dupa categorie");
            Console.WriteLine("0. Iesire din interogări");
            Console.Write("Alege o optiune: ");
            string? optiuneInterogare = Console.ReadLine();

            var elemente = ProduseServiciiManagerAbstract.GetArray();


            switch (optiuneInterogare)
            {
                case "1":
                    var produseAgricole = from elem in elemente
                        where elem.Categorie == "Agricole" && elem is Produs
                        select elem;
                    foreach (var p in produseAgricole)
                        Console.WriteLine(p.Descriere());
                    break;

                case "2":
                    var serviciiIeftine = from elem in elemente
                        where elem is Serviciu && elem.Pret < 300
                        select elem;
                    foreach (var s in serviciiIeftine)
                        Console.WriteLine(s.Descriere());
                    break;

                case "3":
                    var grupare = from elem in elemente
                        group elem by elem.Categorie into gr
                        select gr;
                    foreach (var grup in grupare)
                    {
                        Console.WriteLine($"\nCategoria: {grup.Key}");
                        foreach (var elem in grup)
                            Console.WriteLine(elem.Descriere());
                    }
                    break;

                case "0":
                    ruleaza = false;
                    break;

                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }
        } //schimba while-ul cu for-uri sau ceva mai ez

        
        //pachet, mai multe produse/servicii intr-un loc
        Pachet pachet1 = new Pachet(100, "Pachet Promo", "PCK001", 3500, "Promotii");
        Pachet pachet2 = new Pachet(99, "Pachet Smeker", "PCK002", 1500, "Jucarii");
        List<Pachet> pachete = new List<Pachet> { pachet1, pachet2 };
        
                //adaugare obiecte in primul pachet
                pachet1.AdaugaLaPachet(p1);
                pachet1.AdaugaLaPachet(s1);
                
        //sortare(e dupa pret[prod. sau serv. din ea] si afisare, ex4
        Console.WriteLine("\n----- Sortare pachete -----");
        pachete.Sort(); 
        for (int i = 0; i < pachete.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {pachete[i].Descriere()}");
        }
        
        
        
                //EX5
                Console.WriteLine("\n----- Filtrare pachete -----");
                Console.WriteLine("1. Filtrare dupa categorie");
                Console.WriteLine("2. Filtrare după pret maxim");
                Console.Write("Alege optiunea de filtrare: ");
                string? optFiltrare = Console.ReadLine();

                ICriteriu<Pachet>? criteriu = null;

                switch (optFiltrare)
                {
                    case "1":
                        Console.Write("Introdu categoria dorita: ");
                        string? cat = Console.ReadLine();
                        criteriu = new CriteriuCategorie(cat ?? "");
                        break;

                    case "2":
                        Console.Write("Introdu pretul maxim: ");
                        if (uint.TryParse(Console.ReadLine(), out uint pretMax))
                        {
                            criteriu = new CriteriuPret(pretMax);
                        }
                        else
                        {
                            Console.WriteLine("Pret invalid.");
                        }
                        break;

                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }
                
                if (criteriu != null)
                {
                    var filtrare = new FiltrareCriteriu<Pachet>();
                    var rezultate = filtrare.Filtreaza(pachete, criteriu);

                    Console.WriteLine("\nRezultatele filtrarii:");
                    foreach (var p in rezultate)
                    {
                        Console.WriteLine(p.Descriere());
                    }
                }

        


    }
}