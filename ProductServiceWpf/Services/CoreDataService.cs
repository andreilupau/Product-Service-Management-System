using System.Collections.Generic;
using System.Linq;
using Lab2;

namespace ProductServiceWpf.Services;

public sealed class CoreDataService : IDataService
{
    private readonly ProduseManager _produseManager = new();
    private readonly ServiciiManager _serviciiManager = new();
    private readonly PachetManager _pachetManager = new();

    public IReadOnlyList<ProdusServiciuAbstract> GetItems() =>
        ProduseServiciiManagerAbstract.GetArray();

    public IReadOnlyList<Produs> GetProduse() =>
        ProduseServiciiManagerAbstract.GetArray().OfType<Produs>().ToList();

    public IReadOnlyList<Serviciu> GetServicii() =>
        ProduseServiciiManagerAbstract.GetArray().OfType<Serviciu>().ToList();

    public IReadOnlyList<Pachet> GetPachete() =>
        _pachetManager.GetPachete();

    public void ClearAll()
    {
        ProduseServiciiManagerAbstract.GetArray().Clear();
        _pachetManager.ClearPachete();
    }

    public void LoadFromXml(string xmlPath)
    {
        _produseManager.CitesteProduseDinXml(xmlPath);
        _serviciiManager.CitesteServiciiDinXml(xmlPath);
    }

    public void AddProdus(Produs produs) => _produseManager.AddElement(produs);
    public void AddServiciu(Serviciu serviciu) => _serviciiManager.AddElement(serviciu);
    public void AddPachet(Pachet pachet) => _pachetManager.AddPachet(pachet);

    public void AddProdusToPachet(Pachet pachet, Produs produs) => pachet.AdaugaLaPachet(produs);
    public void AddServiciuToPachet(Pachet pachet, Serviciu serviciu) => pachet.AdaugaLaPachet(serviciu);

    public void RemoveItem(ProdusServiciuAbstract item) =>
        ProduseServiciiManagerAbstract.RemoveElement(item);

    public void RemovePachet(Pachet pachet) =>
        _pachetManager.RemovePachet(pachet);
}
