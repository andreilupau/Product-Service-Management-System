namespace Lab2;

public class Serviciu : ProdusServiciuAbstract, IEquatable<Serviciu>, IComparable<Serviciu>
{
    public Serviciu(uint id, string nume, string codIntern, uint pret, string? categorie)
        : base(id, nume, codIntern, pret, categorie)
    {
       
    }
    
    
    //afisari
    public override string Descriere()
    {
        return $"[SERVICIU] Id:{Id} Nume:{Nume} CodI:[{CodIntern}] Pret:{Pret} Categorie:{Categorie}";
    }
    public void Afisare()
    {
        Console.WriteLine($"Id: {Id}, Nume: {Nume}, CodIntern: {CodIntern}, Pret: {Pret}, Categorie: {Categorie}");
    }
    public override string ToString()
    {
        return base.ToString();
    }

    
    //clasa Object
    public override bool Equals(object? obj)
{
    if (obj is not Serviciu other) return false;
    return this.Id == other.Id && this.Nume == other.Nume && this.CodIntern == other.CodIntern && Pret == other.Pret && Categorie == other.Categorie;
}
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode());
    } 
    public static bool operator ==(Serviciu? a, Serviciu? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }
    public static bool operator !=(Serviciu a, Serviciu b)
    {
        return !(a == b);
    }

    
    //comparare obiecte Lab4
    public bool Equals(Serviciu? other)
    {
        if (other == null) return false;
        return this.Nume == other.Nume && this.CodIntern == other.CodIntern;
    } //IEquatable
    public int CompareTo(Serviciu? other)
    {
        if (other == null) return 1;
        return this.Pret.CompareTo(other.Pret);
    } //IComparable

    
    //Interfata Pachet
    public override bool CanAddToPackage(Pachet pachet)
    {
        return true;
    }

    
}