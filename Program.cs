namespace Lab2;

class Program
{
    public static void Main(string[] args)
    {
                //Mesaj introducere program
        Console.WriteLine("Bun venit in programul de gestiune a serviciilor si produselor!");
                                         
        
                //PREMADE
        ProduseManager pm = new ProduseManager(); ServiciiManager sm = new ServiciiManager();
        PachetManager packManager = new PachetManager();
            
        Produs p1 = new Produs(7, "Motocultor", "SV94", "BOSCH", 700, "Agricole");
        Serviciu s1 = new Serviciu(6, "Cositor", "UJ93", 200, "SRL");
        pm.AddElement(p1); sm.AddElement(s1); //adaugare in Array100
        
        
        //Creare si Adaugare Pachet in lista de pahcete
        Pachet pachet1 = new Pachet(100, "Pachet Promo", "PCK001", 3500, "Promotii");
        packManager.AddPachet(pachet1);
        
        //adaugam continut pachetului (pachet1), maxim 1 produs
        pachet1.AdaugaLaPachet(p1); pachet1.AdaugaLaPachet(s1);
        
        //AFISARE CONTINUT
        pm.Write2Console(); //afisare Array100 (prod si serv)
        packManager.WriteSortedPachete(); //Ce pachete exista
        
        
                //Incepere Program
                
        packManager.ReadPachet(); //creare pachet by user A-Z
        packManager.WriteSortedPachete();
        
        //TODO: mai sunt in PachetManager
        //TODO: afisare continutul unui pachet ales de tine
        
        
        

        
        //Sortari (interogari linq) TODO: ar trebui schimbate ca-s cringe
        // bool ruleaza = true; 
        // while (ruleaza)
        // {
        //     Console.WriteLine("\n----- Interogări LINQ -----");
        //     Console.WriteLine("1. Afisează toate produsele din categoria 'Agricole'");
        //     Console.WriteLine("2. Afisează toate serviciile cu pret sub 300");
        //     Console.WriteLine("3. Grupeaza elementele dupa categorie");
        //     Console.WriteLine("0. Iesire din interogări");
        //     Console.Write("Alege o optiune: ");
        //     string? optiuneInterogare = Console.ReadLine();
        //
        //     var elemente = ProduseServiciiManagerAbstract.GetArray();
        //
        //
        //     switch (optiuneInterogare)
        //     {
        //         case "1":
        //             var produseAgricole = from elem in elemente
        //                 where elem.Categorie == "Agricole" && elem is Produs
        //                 select elem;
        //             foreach (var p in produseAgricole)
        //                 Console.WriteLine(p.Descriere());
        //             break;
        //
        //         case "2":
        //             var serviciiIeftine = from elem in elemente
        //                 where elem is Serviciu && elem.Pret < 300
        //                 select elem;
        //             foreach (var s in serviciiIeftine)
        //                 Console.WriteLine(s.Descriere());
        //             break;
        //
        //         case "3":
        //             var grupare = from elem in elemente
        //                 group elem by elem.Categorie into gr
        //                 select gr;
        //             foreach (var grup in grupare)
        //             {
        //                 Console.WriteLine($"\nCategoria: {grup.Key}");
        //                 foreach (var elem in grup)
        //                     Console.WriteLine(elem.Descriere());
        //             }
        //             break;
        //
        //         case "0":
        //             ruleaza = false;
        //             break;
        //
        //         default:
        //             Console.WriteLine("Opțiune invalidă.");
        //             break;
        //     }
        // }
        //
        //
        //         
        //         
        // //FILTRARI PACHETE (dupa pret[prod. sau serv. din ea]) TODO: the code is alien
        // Console.WriteLine("\n----- Sortare pachete -----");
        //
        // //NEW todo, vezi why it works
        // var pachete = packManager.GetPachete().ToList(); // <-- asta înlocuiește lista locală veche
        // pachete.Sort();
        //
        // for (int i = 0; i < pachete.Count; i++)
        // {
        //     Console.WriteLine($"[{i + 1}] {pachete[i].Descriere()}");
        // }
        //
        //         Console.WriteLine("\n----- Filtrare pachete -----");
        //         Console.WriteLine("1. Filtrare dupa categorie");
        //         Console.WriteLine("2. Filtrare după pret maxim");
        //         Console.WriteLine("0. Iesire din filtrare");
        //         Console.Write("Alege optiunea de filtrare: ");
        //         string? optFiltrare = Console.ReadLine();
        //
        //         ICriteriu<Pachet>? criteriu = null;
        //
        //         switch (optFiltrare)
        //         {
        //             case "1":
        //                 Console.Write("Introdu categoria dorita: ");
        //                 string? cat = Console.ReadLine();
        //                 criteriu = new CriteriuCategorie(cat ?? "");
        //                 break;
        //
        //             case "2":
        //                 Console.Write("Introdu pretul maxim: ");
        //                 if (uint.TryParse(Console.ReadLine(), out uint pretMax))
        //                 {
        //                     criteriu = new CriteriuPret(pretMax);
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine("Pret invalid.");
        //                 }
        //                 break;
        //             case "0":
        //                 break;
        //
        //             default:
        //                 Console.WriteLine("Optiune invalida.");
        //                 break;
        //         }
        //         
        //         if (criteriu != null)
        //         {
        //             var filtrare = new FiltrareCriteriu<Pachet>();
        //             var rezultate = filtrare.Filtreaza(pachete, criteriu);
        //
        //             Console.WriteLine("\nRezultatele filtrarii:");
        //             foreach (var p in rezultate)
        //             {
        //                 Console.WriteLine(p.Descriere());
        //             }
        //         }

                
        //Mesaj de incheiere
        Console.WriteLine("\nProgramul a ajuns la sfarsit.");

    }
    
}