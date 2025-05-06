namespace Lab2;

//baza pentru [Produs], [Serviciu] si [Pachet]
public abstract class ProdusServiciuAbstract : IPackageable //acum produs si serviciu pot fi puse intr-un pachet
{
    //constructor de baza
    public ProdusServiciuAbstract(uint id, string? nume, string? codIntern, uint pret, string? categorie)
    {
        Id = id;
        Nume = nume;
        CodIntern = codIntern;
        Pret = pret;
        Categorie = categorie;
    }
    
    public uint Id { get; set; } public string? Nume { get; set; } public string? CodIntern { get; set; }
    public uint Pret { get; set; } public string? Categorie { get; set; }
    

    //descrieri [Produs] si [Serviciu]
    public abstract string Descriere(); //abstract obliga subclasele sa aiba o astfel de metoda
    public virtual string AltaDescriere() //subclasele o au deja, poate fi modificata
    {
        return $"AltaDesc-> Id:[{Id}], Nume:{Nume}";
    }
    
    
    //metode din Object
    public override string ToString()
    {
        return $"Id: {Id}, Nume: {Nume}, Cod Intern: {CodIntern}, Pret: {Pret}, Categorie: {Categorie}";
    }
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (ProdusServiciuAbstract)obj;
        return Nume == other.Nume && CodIntern == other.CodIntern && Pret == other.Pret && Categorie == other.Categorie;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Nume, CodIntern, Pret, Categorie);
    }
    
    
    //pentru pachete L6
    public abstract bool CanAddToPackage(Pachet pachet);
    
}