using System.Collections.Generic;
using Lab2;

namespace ProductServiceWpf.Services;

public interface IDataService
{
    IReadOnlyList<ProdusServiciuAbstract> GetItems();
    IReadOnlyList<Produs> GetProduse();
    IReadOnlyList<Serviciu> GetServicii();
    IReadOnlyList<Pachet> GetPachete();

    void ClearAll();
    void LoadFromXml(string xmlPath);
    void AddProdus(Produs produs);
    void AddServiciu(Serviciu serviciu);
    void AddPachet(Pachet pachet);
    void AddProdusToPachet(Pachet pachet, Produs produs);
    void AddServiciuToPachet(Pachet pachet, Serviciu serviciu);
    void RemoveItem(ProdusServiciuAbstract item);
    void RemovePachet(Pachet pachet);
}
