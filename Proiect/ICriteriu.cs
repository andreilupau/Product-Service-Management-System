namespace Lab2;

public interface ICriteriu<T>
{
    bool EsteIndeplinit(T element);
}

public class CriteriuCategorie : ICriteriu<Pachet>
{
    private string categorieCautata;

    public CriteriuCategorie(string categorie)
    {
        categorieCautata = categorie;
    }

    public bool EsteIndeplinit(Pachet p)
    {
        return p.Categorie == categorieCautata;
    }
}

public class CriteriuPret : ICriteriu<Pachet>
{
    private uint pretMaxim;

    public CriteriuPret(uint pret)
    {
        pretMaxim = pret;
    }

    public bool EsteIndeplinit(Pachet p)
    {
        return p.Pret <= pretMaxim;
    }
}