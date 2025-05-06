namespace Lab2;

public interface IFiltrare<T>
{
    List<T> Filtreaza(List<T> lista, ICriteriu<T> criteriu);
}

public class FiltrareCriteriu<T> : IFiltrare<T>
{
    public List<T> Filtreaza(List<T> lista, ICriteriu<T> criteriu)
    {
        List<T> rezultat = new List<T>();
        foreach (var element in lista)
        {
            if (criteriu.EsteIndeplinit(element))
            {
                rezultat.Add(element);
            }
        }
        return rezultat;
    }
}