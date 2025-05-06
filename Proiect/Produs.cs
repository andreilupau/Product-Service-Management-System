namespace Lab2;

public class Produs : ProdusServiciuAbstract, IEquatable<Produs>, IComparable<Produs>
{
    public string? Producator { get; set; }
    public Produs(uint id, string? nume, string? codIntern, string? producator, uint pret, string? categorie)
        : base(id, nume, codIntern, pret, categorie)
    {
        Producator = producator;
    }

    
    //afisari
    public override string Descriere() //din clasa de baza
    {
        return $"[PRODUS] Id:{Id} Nume:{Nume} CodI:[{CodIntern}] Producator:{Producator} Pret:{Pret} Categorie:{Categorie}";
    }
    public void Afisare() //proprie
    {
        Console.WriteLine($"Id: {Id}, Nume: {Nume}, CodIntern: {CodIntern}, Producator: {Producator}, Pret: {Pret}, Categorie: {Categorie}");
    }
    public override string ToString()
    {
        return base.ToString() + $", Producător: {Producator}";
    }
    
    
    //clasa Object
    public override bool Equals(object? obj)
    {
        if (obj is not Produs other) return false;
        return this.Nume == other.Nume && this.CodIntern == other.CodIntern && this.Producator == other.Producator && Pret == other.Pret && Categorie == other.Categorie;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Producator);
    }
    public static bool operator ==(Produs? a, Produs? b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }
    public static bool operator !=(Produs a, Produs b) => !(a == b);

    
    //comparare obiecte Lab4
    public bool Equals(Produs? other)
    {
        if (other == null) return false;
        return this.Nume == other.Nume && this.CodIntern == other.CodIntern;
    } //IEquatable
    public int CompareTo(Produs? other)
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