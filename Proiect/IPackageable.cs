namespace Lab2;

//baza pentru ProdusServiciuAbstract
public interface IPackageable //cine vrea să intre în pachet trebuie să respecte metodele
{
    bool CanAddToPackage(Pachet pachet);
    uint Pret { get; }
}