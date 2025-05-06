namespace Lab2;
//baza pentru [ProduseManager], [ServiciiManager] si [PachetManager]
public abstract class ProduseServiciiManagerAbstract
{
    //Boss-ul, array complet! intra toate: produs, serviciu, pachet. Fiecare pe un rand.
    protected static List<ProdusServiciuAbstract> Array100 = new List<ProdusServiciuAbstract>();
    
    
    //ADAUGARE, AFISARE SI CAUTAREA UNUI OBIECT
    public void AddElement(ProdusServiciuAbstract obiect)
    {
        for (int i = 0; i < Array100.Count; i++)
        {
            //daca obiectul exista deja, nu mai il adaugam si iesim din program
            if (Array100[i].Equals(obiect))
            {
                Console.WriteLine("Obiectul exista deja în lista!");
                return;
            }
        }

        Array100.Add(obiect);
    }
    public int CautaElement(uint id) //cautare dupa id
    {
        for (int i = 0; i < Array100.Count; i++)
        {
            if (Array100[i].Id == id)
            {
                Console.WriteLine($"\nElement gasit!: {Array100[i].Descriere()}");
                return i;
            }
        }
        Console.WriteLine($"\nNu s-a găsit niciun element cu ID-ul:{id}.");
        return -1;
    }
    
    public void Write2Console() //afisam tot ce avem in Array100
     {
        Console.WriteLine("\nLista cu produse/servicii:");
        if (Array100.Count == 0)
        {
            Console.WriteLine("Array-ul este gol!");
            return;
        }
        for (int i = 0; i < Array100.Count; i++)
        {
            Console.WriteLine(Array100[i].Descriere());
        }
    }
    public static List<ProdusServiciuAbstract> GetArray() //aici returnam doar, tot ce avem in Array100
    {
        return Array100;
    }
    
    
    
}