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
        Pachet pachet1 = new Pachet(100, "Pachet Promo", "PCK001", "Promotii");
        packManager.AddPachet(pachet1);
        
        //adaugam continut pachetului (pachet1), maxim 1 produs
        pachet1.AdaugaLaPachet(p1); pachet1.AdaugaLaPachet(s1);
        
        //AFISARE CONTINUT
        //pm.Write2Console(); //afisare Array100 (prod si serv)
        //packManager.WriteSortedPachete(); //Ce pachete exista
        
        
                //Incepere Program
                
        packManager.ReadPachet(); //creare pachet by user A-Z TODO: Este un bug la incarcare continut cu xml, zice asta cand puneam id-ul corect.
        packManager.WriteSortedPachete(); //afisare pachete din pachet100
        packManager.WritePacheteContinut(); //afisare continut pachete in Pachet100
        
        
        

                
        //Mesaj de incheiere
        Console.WriteLine("\nProgramul a ajuns la sfarsit.");

    }
    
}
