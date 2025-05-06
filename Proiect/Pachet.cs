namespace Lab2;

public class Pachet : ProdusServiciuAbstract, IComparable<Pachet>//ex4L6 si codul de jos compareto
{
    public Pachet(uint id, string nume, string codIntern, uint pret, string? categorie)
        : base(id, nume, codIntern, pret, categorie)
    {
    }
    
    //Conținutul intern al unui singur pachet
    //listă care poate conține orice obiect care implementează IPackageable
    private List<IPackageable> elem_pachet = new List<IPackageable>();


    
    public void AdaugaLaPachet(IPackageable item) //Într-un pachet să putem avea maxim un produs și un număr nelimitat de servic
    {
        if (CanAddElement(item))
        {
            elem_pachet.Add(item);
            UpdatePret(); // recalculăm prețul total al pachetului
        }
        else
        {
            Console.WriteLine("\nElementul nu poate fi adaugat in pachet!");
        }
    }

    public override bool CanAddToPackage(Pachet pachet)
    {
        return false;
    }
    public override string Descriere()
    {
        return $"[Pachet] ID: {Id}, Nume: {Nume}, CodIntern: {CodIntern}, " +
               $"Pret: {Pret}, Categorie: {Categorie}, Componente: {elem_pachet.Count}";
    }
    
    
    //ex4
    public IReadOnlyList<IPackageable> GetElements()
    {
        return elem_pachet.AsReadOnly();
    }
    private bool CanAddElement(IPackageable item)
    {
        if (item is Produs)
        {
            foreach (var element in elem_pachet)
            {
                if (element is Produs)
                {
                    return false; // Deja există un produs
                }
            }
            return true; // Nu există produs, se poate adăuga
        }
        else if (item is Serviciu)
        {
            return true; // Serviciile sunt nelimitate
        }
        return false; // Alte tipuri nu sunt permise
    }
    private void UpdatePret()
    {
        Pret = (uint)elem_pachet.Sum(e => (int)e.Pret);
    }
    
    public int CompareTo(Pachet? other)
    {
        if (other == null) return 1;
        return this.Pret.CompareTo(other.Pret);
    }
    
}