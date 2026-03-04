using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Lab2;
using ProductServiceWpf.Commands;
using ProductServiceWpf.Services;

namespace ProductServiceWpf.ViewModels;

public sealed class MainWindowVm : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private enum UiLanguage
    {
        Ro,
        En
    }

    private readonly IDataService _dataService;
    private readonly Dictionary<string, List<string>> _errors = new();
    private UiLanguage _language = UiLanguage.Ro;

    public ObservableCollection<ProdusServiciuAbstract> Items { get; } = new();
    public ObservableCollection<Produs> Produse { get; } = new();
    public ObservableCollection<Serviciu> Servicii { get; } = new();
    public ObservableCollection<Pachet> Pachete { get; } = new();
    public ObservableCollection<string> SelectedPachetContinut { get; } = new();

    private ProdusServiciuAbstract? _selectedItem;
    public ProdusServiciuAbstract? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem == value)
                return;
            _selectedItem = value;
            OnPropertyChanged();
            DeleteItemCommand.RaiseCanExecuteChanged();
        }
    }

    private Pachet? _selectedPachet;
    public Pachet? SelectedPachet
    {
        get => _selectedPachet;
        set
        {
            if (_selectedPachet == value)
                return;
            _selectedPachet = value;
            OnPropertyChanged();
            UpdateSelectedPachetContinut();
            AddProdusToPachetCommand.RaiseCanExecuteChanged();
            AddServiciuToPachetCommand.RaiseCanExecuteChanged();
            DeletePachetCommand.RaiseCanExecuteChanged();
        }
    }

    private Produs? _selectedProdus;
    public Produs? SelectedProdus
    {
        get => _selectedProdus;
        set
        {
            if (_selectedProdus == value)
                return;
            _selectedProdus = value;
            OnPropertyChanged();
            AddProdusToPachetCommand.RaiseCanExecuteChanged();
        }
    }

    private Serviciu? _selectedServiciu;
    public Serviciu? SelectedServiciu
    {
        get => _selectedServiciu;
        set
        {
            if (_selectedServiciu == value)
                return;
            _selectedServiciu = value;
            OnPropertyChanged();
            AddServiciuToPachetCommand.RaiseCanExecuteChanged();
        }
    }

    private string _status = string.Empty;
    public string Status
    {
        get => _status;
        set { _status = value; OnPropertyChanged(); }
    }

    private string? _produsNume;
    public string? ProdusNume
    {
        get => _produsNume;
        set { _produsNume = value; OnPropertyChanged(); ValidateProdus(); }
    }

    private string? _produsCod;
    public string? ProdusCod
    {
        get => _produsCod;
        set { _produsCod = value; OnPropertyChanged(); }
    }

    private string? _produsProducator;
    public string? ProdusProducator
    {
        get => _produsProducator;
        set { _produsProducator = value; OnPropertyChanged(); }
    }

    private string? _produsPret;
    public string? ProdusPret
    {
        get => _produsPret;
        set { _produsPret = value; OnPropertyChanged(); ValidateProdus(); }
    }

    private string? _produsCategorie;
    public string? ProdusCategorie
    {
        get => _produsCategorie;
        set { _produsCategorie = value; OnPropertyChanged(); }
    }

    private string? _serviciuNume;
    public string? ServiciuNume
    {
        get => _serviciuNume;
        set { _serviciuNume = value; OnPropertyChanged(); ValidateServiciu(); }
    }

    private string? _serviciuCod;
    public string? ServiciuCod
    {
        get => _serviciuCod;
        set { _serviciuCod = value; OnPropertyChanged(); }
    }

    private string? _serviciuPret;
    public string? ServiciuPret
    {
        get => _serviciuPret;
        set { _serviciuPret = value; OnPropertyChanged(); ValidateServiciu(); }
    }

    private string? _serviciuCategorie;
    public string? ServiciuCategorie
    {
        get => _serviciuCategorie;
        set { _serviciuCategorie = value; OnPropertyChanged(); }
    }

    private string? _pachetNume;
    public string? PachetNume
    {
        get => _pachetNume;
        set { _pachetNume = value; OnPropertyChanged(); }
    }

    private string? _pachetCod;
    public string? PachetCod
    {
        get => _pachetCod;
        set { _pachetCod = value; OnPropertyChanged(); }
    }

    private string? _pachetCategorie;
    public string? PachetCategorie
    {
        get => _pachetCategorie;
        set { _pachetCategorie = value; OnPropertyChanged(); }
    }

    public RelayCommand LoadFromXmlCommand { get; }
    public RelayCommand ClearCommand { get; }
    public RelayCommand AddProdusCommand { get; }
    public RelayCommand AddServiciuCommand { get; }
    public RelayCommand CreatePachetCommand { get; }
    public RelayCommand AddProdusToPachetCommand { get; }
    public RelayCommand AddServiciuToPachetCommand { get; }
    public RelayCommand DeleteItemCommand { get; }
    public RelayCommand DeletePachetCommand { get; }
    public RelayCommand ToggleLanguageCommand { get; }

    public MainWindowVm(IDataService dataService)
    {
        _dataService = dataService;

        LoadFromXmlCommand = new RelayCommand(LoadFromXml);
        ClearCommand = new RelayCommand(ClearAllData);
        AddProdusCommand = new RelayCommand(AddProdus, CanAddProdus);
        AddServiciuCommand = new RelayCommand(AddServiciu, CanAddServiciu);
        CreatePachetCommand = new RelayCommand(CreatePachet);
        AddProdusToPachetCommand = new RelayCommand(AddProdusToPachet, CanAddProdusToPachet);
        AddServiciuToPachetCommand = new RelayCommand(AddServiciuToPachet, CanAddServiciuToPachet);
        DeleteItemCommand = new RelayCommand(DeleteItem, () => SelectedItem != null);
        DeletePachetCommand = new RelayCommand(DeletePachet, () => SelectedPachet != null);
        ToggleLanguageCommand = new RelayCommand(ToggleLanguage);

        ApplyLanguage(_language);
        ValidateProdus();
        ValidateServiciu();
    }

    private void ToggleLanguage()
    {
        _language = _language == UiLanguage.Ro ? UiLanguage.En : UiLanguage.Ro;
        ApplyLanguage(_language);
    }

    private void LoadFromXml()
    {
        try
        {
            ClearAllData();
            var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "p_s.xml");
            _dataService.LoadFromXml(xmlPath);
            RefreshItemsFromCore();
            Status = string.Format(_msgLoadedXml, Items.Count);
        }
        catch (Exception ex)
        {
            Status = _msgXmlFailed + ex.Message;
        }
    }

    private void ClearAllData()
    {
        _dataService.ClearAll();
        RefreshItemsFromCore();
        RefreshPachete();
        Status = _msgCleared;
    }

    private void AddProdus()
    {
        if (!TryParsePrice(ProdusPret, out var pret))
        {
            Status = _msgInvalidProductPrice;
            return;
        }

        var id = NextItemId();
        var produs = new Produs(id,
            SafeText(ProdusNume),
            SafeText(ProdusCod),
            SafeText(ProdusProducator),
            pret,
            SafeText(ProdusCategorie));

        _dataService.AddProdus(produs);
        RefreshItemsFromCore();
        Status = _msgProductAdded;
    }

    private void AddServiciu()
    {
        if (!TryParsePrice(ServiciuPret, out var pret))
        {
            Status = _msgInvalidServicePrice;
            return;
        }

        var id = NextItemId();
        var serviciu = new Serviciu(id,
            SafeText(ServiciuNume),
            SafeText(ServiciuCod),
            pret,
            SafeText(ServiciuCategorie));

        _dataService.AddServiciu(serviciu);
        RefreshItemsFromCore();
        Status = _msgServiceAdded;
    }

    private void CreatePachet()
    {
        var nextId = NextPachetId();
        var nume = SafeText(PachetNume, $"Pachet_{nextId}");
        var cod = SafeText(PachetCod, $"PKT{nextId:D3}");
        var categorie = SafeText(PachetCategorie, "Pachet");

        var pachet = new Pachet(nextId, nume, cod, categorie);
        _dataService.AddPachet(pachet);
        RefreshPachete();
        Status = _msgPackageCreated;
    }

    private void AddProdusToPachet()
    {
        if (SelectedPachet == null || SelectedProdus == null)
        {
            Status = _msgSelectPackageProduct;
            return;
        }

        _dataService.AddProdusToPachet(SelectedPachet, SelectedProdus);
        RefreshPachete();
        Status = _msgProductAddedToPackage;
    }

    private void AddServiciuToPachet()
    {
        if (SelectedPachet == null || SelectedServiciu == null)
        {
            Status = _msgSelectPackageService;
            return;
        }

        _dataService.AddServiciuToPachet(SelectedPachet, SelectedServiciu);
        RefreshPachete();
        Status = _msgServiceAddedToPackage;
    }

    private void DeleteItem()
    {
        if (SelectedItem == null)
            return;

        _dataService.RemoveItem(SelectedItem);
        SelectedItem = null;
        RefreshItemsFromCore();
        Status = _msgItemDeleted;
    }

    private void DeletePachet()
    {
        if (SelectedPachet == null)
            return;

        _dataService.RemovePachet(SelectedPachet);
        SelectedPachet = null;
        RefreshPachete();
        Status = _msgPackageDeleted;
    }

    private void RefreshItemsFromCore()
    {
        Items.Clear();
        Produse.Clear();
        Servicii.Clear();

        foreach (var item in _dataService.GetItems())
        {
            Items.Add(item);
            if (item is Produs produs)
                Produse.Add(produs);
            else if (item is Serviciu serviciu)
                Servicii.Add(serviciu);
        }
    }

    private void RefreshPachete()
    {
        var selectedId = SelectedPachet?.Id;
        Pachete.Clear();
        foreach (var p in _dataService.GetPachete().OrderBy(p => p.Pret))
            Pachete.Add(p);

        if (selectedId.HasValue)
            SelectedPachet = Pachete.FirstOrDefault(p => p.Id == selectedId.Value);
        else
            UpdateSelectedPachetContinut();

        // SelectedPachet can be the same reference, so setter might not run.
        // Ensure UI content list reflects newly added package elements.
        if (SelectedPachet != null)
            UpdateSelectedPachetContinut();
    }

    private void UpdateSelectedPachetContinut()
    {
        SelectedPachetContinut.Clear();
        if (SelectedPachet == null)
            return;

        foreach (var elem in SelectedPachet.GetElements())
        {
            if (elem is ProdusServiciuAbstract psa)
                SelectedPachetContinut.Add(psa.Descriere());
            else
                SelectedPachetContinut.Add($"Element (Pret: {elem.Pret})");
        }
    }

    private uint NextItemId()
    {
        var array = _dataService.GetItems();
        if (array.Count == 0)
            return 1;
        return array.Max(x => x.Id) + 1;
    }

    private uint NextPachetId()
    {
        var pachete = _dataService.GetPachete();
        if (pachete.Count == 0)
            return 1;
        return pachete.Max(x => x.Id) + 1;
    }

    private static bool TryParsePrice(string? text, out uint value)
    {
        if (uint.TryParse(text, out value))
            return true;
        value = 0;
        return false;
    }

    private static string SafeText(string? text, string? fallback = "")
    {
        if (string.IsNullOrWhiteSpace(text))
            return fallback ?? string.Empty;
        return text.Trim();
    }

    private bool CanAddProdus() => !HasErrorsFor(nameof(ProdusNume)) && !HasErrorsFor(nameof(ProdusPret));
    private bool CanAddServiciu() => !HasErrorsFor(nameof(ServiciuNume)) && !HasErrorsFor(nameof(ServiciuPret));
    private bool CanAddProdusToPachet() => SelectedPachet != null && SelectedProdus != null;
    private bool CanAddServiciuToPachet() => SelectedPachet != null && SelectedServiciu != null;

    private void ValidateProdus()
    {
        ClearErrors(nameof(ProdusNume));
        if (string.IsNullOrWhiteSpace(ProdusNume))
            AddError(nameof(ProdusNume), _errProductNameRequired);

        ClearErrors(nameof(ProdusPret));
        if (string.IsNullOrWhiteSpace(ProdusPret))
        {
            AddError(nameof(ProdusPret), _errProductPriceRequired);
        }
        else if (!uint.TryParse(ProdusPret, out var pret) || pret == 0)
        {
            AddError(nameof(ProdusPret), _errProductPricePositive);
        }

        AddProdusCommand.RaiseCanExecuteChanged();
    }

    private void ValidateServiciu()
    {
        ClearErrors(nameof(ServiciuNume));
        if (string.IsNullOrWhiteSpace(ServiciuNume))
            AddError(nameof(ServiciuNume), _errServiceNameRequired);

        ClearErrors(nameof(ServiciuPret));
        if (string.IsNullOrWhiteSpace(ServiciuPret))
        {
            AddError(nameof(ServiciuPret), _errServicePriceRequired);
        }
        else if (!uint.TryParse(ServiciuPret, out var pret) || pret == 0)
        {
            AddError(nameof(ServiciuPret), _errServicePricePositive);
        }

        AddServiciuCommand.RaiseCanExecuteChanged();
    }

    private void AddError(string propertyName, string error)
    {
        if (!_errors.TryGetValue(propertyName, out var list))
        {
            list = new List<string>();
            _errors[propertyName] = list;
        }

        if (!list.Contains(error))
        {
            list.Add(error);
            OnErrorsChanged(propertyName);
        }
    }

    private void ClearErrors(string propertyName)
    {
        if (_errors.Remove(propertyName))
            OnErrorsChanged(propertyName);
    }

    private bool HasErrorsFor(string propertyName) =>
        _errors.TryGetValue(propertyName, out var list) && list.Count > 0;

    public bool HasErrors => _errors.Count > 0;

    public System.Collections.IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName == null)
            return _errors.SelectMany(kv => kv.Value);
        return _errors.TryGetValue(propertyName, out var list) ? list : Array.Empty<string>();
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
    private void OnErrorsChanged(string propertyName) =>
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private string _uiTitle = string.Empty;
    public string UiTitle { get => _uiTitle; private set => SetUi(ref _uiTitle, value); }

    private string _uiTabProductsServices = string.Empty;
    public string UiTabProductsServices { get => _uiTabProductsServices; private set => SetUi(ref _uiTabProductsServices, value); }

    private string _uiTabPackages = string.Empty;
    public string UiTabPackages { get => _uiTabPackages; private set => SetUi(ref _uiTabPackages, value); }

    private string _uiActions = string.Empty;
    public string UiActions { get => _uiActions; private set => SetUi(ref _uiActions, value); }

    private string _uiLoadXml = string.Empty;
    public string UiLoadXml { get => _uiLoadXml; private set => SetUi(ref _uiLoadXml, value); }

    private string _uiClear = string.Empty;
    public string UiClear { get => _uiClear; private set => SetUi(ref _uiClear, value); }

    private string _uiProduct = string.Empty;
    public string UiProduct { get => _uiProduct; private set => SetUi(ref _uiProduct, value); }

    private string _uiService = string.Empty;
    public string UiService { get => _uiService; private set => SetUi(ref _uiService, value); }

    private string _uiPackage = string.Empty;
    public string UiPackage { get => _uiPackage; private set => SetUi(ref _uiPackage, value); }

    private string _uiAddProduct = string.Empty;
    public string UiAddProduct { get => _uiAddProduct; private set => SetUi(ref _uiAddProduct, value); }

    private string _uiAddService = string.Empty;
    public string UiAddService { get => _uiAddService; private set => SetUi(ref _uiAddService, value); }

    private string _uiCreatePackage = string.Empty;
    public string UiCreatePackage { get => _uiCreatePackage; private set => SetUi(ref _uiCreatePackage, value); }

    private string _uiAddContent = string.Empty;
    public string UiAddContent { get => _uiAddContent; private set => SetUi(ref _uiAddContent, value); }

    private string _uiAddProductToPackage = string.Empty;
    public string UiAddProductToPackage { get => _uiAddProductToPackage; private set => SetUi(ref _uiAddProductToPackage, value); }

    private string _uiAddServiceToPackage = string.Empty;
    public string UiAddServiceToPackage { get => _uiAddServiceToPackage; private set => SetUi(ref _uiAddServiceToPackage, value); }

    private string _uiPackageContent = string.Empty;
    public string UiPackageContent { get => _uiPackageContent; private set => SetUi(ref _uiPackageContent, value); }

    private string _uiDeleteItem = string.Empty;
    public string UiDeleteItem { get => _uiDeleteItem; private set => SetUi(ref _uiDeleteItem, value); }

    private string _uiDeletePackage = string.Empty;
    public string UiDeletePackage { get => _uiDeletePackage; private set => SetUi(ref _uiDeletePackage, value); }

    private string _uiName = string.Empty;
    public string UiName { get => _uiName; private set => SetUi(ref _uiName, value); }

    private string _uiInternalCode = string.Empty;
    public string UiInternalCode { get => _uiInternalCode; private set => SetUi(ref _uiInternalCode, value); }

    private string _uiProducer = string.Empty;
    public string UiProducer { get => _uiProducer; private set => SetUi(ref _uiProducer, value); }

    private string _uiPrice = string.Empty;
    public string UiPrice { get => _uiPrice; private set => SetUi(ref _uiPrice, value); }

    private string _uiCategory = string.Empty;
    public string UiCategory { get => _uiCategory; private set => SetUi(ref _uiCategory, value); }

    private string _uiLanguageButton = string.Empty;
    public string UiLanguageButton { get => _uiLanguageButton; private set => SetUi(ref _uiLanguageButton, value); }

    private string _statusReady = string.Empty;
    private string _msgLoadedXml = string.Empty;
    private string _msgXmlFailed = string.Empty;
    private string _msgCleared = string.Empty;
    private string _msgInvalidProductPrice = string.Empty;
    private string _msgInvalidServicePrice = string.Empty;
    private string _msgProductAdded = string.Empty;
    private string _msgServiceAdded = string.Empty;
    private string _msgPackageCreated = string.Empty;
    private string _msgProductAddedToPackage = string.Empty;
    private string _msgServiceAddedToPackage = string.Empty;
    private string _msgSelectPackageProduct = string.Empty;
    private string _msgSelectPackageService = string.Empty;
    private string _msgItemDeleted = string.Empty;
    private string _msgPackageDeleted = string.Empty;

    private string _errProductNameRequired = string.Empty;
    private string _errProductPriceRequired = string.Empty;
    private string _errProductPricePositive = string.Empty;
    private string _errServiceNameRequired = string.Empty;
    private string _errServicePriceRequired = string.Empty;
    private string _errServicePricePositive = string.Empty;

    private void ApplyLanguage(UiLanguage language)
    {
        var oldReady = _statusReady;

        if (language == UiLanguage.Ro)
        {
            UiTitle = "Product Service Management";
            UiTabProductsServices = "Produse si Servicii";
            UiTabPackages = "Pachete";
            UiActions = "Actiuni";
            UiLoadXml = "Incarca XML";
            UiClear = "Goleste";
            UiProduct = "Produs";
            UiService = "Serviciu";
            UiPackage = "Pachet";
            UiAddProduct = "Adauga produs";
            UiAddService = "Adauga serviciu";
            UiCreatePackage = "Creeaza pachet";
            UiAddContent = "Adauga continut";
            UiAddProductToPackage = "Adauga produs in pachet";
            UiAddServiceToPackage = "Adauga serviciu in pachet";
            UiPackageContent = "Continut pachet";
            UiDeleteItem = "Sterge element";
            UiDeletePackage = "Sterge pachet";
            UiName = "Nume";
            UiInternalCode = "Cod intern";
            UiProducer = "Producator";
            UiPrice = "Pret";
            UiCategory = "Categorie";
            UiLanguageButton = "RO/EN";

            _statusReady = "Gata.";
            _msgLoadedXml = "Incarcate {0} elemente din XML.";
            _msgXmlFailed = "Eroare XML: ";
            _msgCleared = "Goleste.";
            _msgInvalidProductPrice = "Pret invalid pentru produs.";
            _msgInvalidServicePrice = "Pret invalid pentru serviciu.";
            _msgProductAdded = "Produs adaugat.";
            _msgServiceAdded = "Serviciu adaugat.";
            _msgPackageCreated = "Pachet creat.";
            _msgProductAddedToPackage = "Produs adaugat in pachet.";
            _msgServiceAddedToPackage = "Serviciu adaugat in pachet.";
            _msgSelectPackageProduct = "Selecteaza pachet si produs.";
            _msgSelectPackageService = "Selecteaza pachet si serviciu.";
            _msgItemDeleted = "Element sters.";
            _msgPackageDeleted = "Pachet sters.";

            _errProductNameRequired = "Numele produsului este obligatoriu.";
            _errProductPriceRequired = "Pretul produsului este obligatoriu.";
            _errProductPricePositive = "Pretul produsului trebuie sa fie un numar pozitiv.";
            _errServiceNameRequired = "Numele serviciului este obligatoriu.";
            _errServicePriceRequired = "Pretul serviciului este obligatoriu.";
            _errServicePricePositive = "Pretul serviciului trebuie sa fie un numar pozitiv.";
        }
        else
        {
            UiTitle = "Product Service Management";
            UiTabProductsServices = "Products and Services";
            UiTabPackages = "Packages";
            UiActions = "Actions";
            UiLoadXml = "Load XML";
            UiClear = "Clear";
            UiProduct = "Product";
            UiService = "Service";
            UiPackage = "Package";
            UiAddProduct = "Add product";
            UiAddService = "Add service";
            UiCreatePackage = "Create package";
            UiAddContent = "Add content";
            UiAddProductToPackage = "Add product to package";
            UiAddServiceToPackage = "Add service to package";
            UiPackageContent = "Package content";
            UiDeleteItem = "Delete item";
            UiDeletePackage = "Delete package";
            UiName = "Name";
            UiInternalCode = "Internal code";
            UiProducer = "Producer";
            UiPrice = "Price";
            UiCategory = "Category";
            UiLanguageButton = "EN/RO";

            _statusReady = "Ready.";
            _msgLoadedXml = "Loaded {0} items from XML.";
            _msgXmlFailed = "XML error: ";
            _msgCleared = "Cleared.";
            _msgInvalidProductPrice = "Invalid product price.";
            _msgInvalidServicePrice = "Invalid service price.";
            _msgProductAdded = "Product added.";
            _msgServiceAdded = "Service added.";
            _msgPackageCreated = "Package created.";
            _msgProductAddedToPackage = "Product added to package.";
            _msgServiceAddedToPackage = "Service added to package.";
            _msgSelectPackageProduct = "Select a package and a product.";
            _msgSelectPackageService = "Select a package and a service.";
            _msgItemDeleted = "Item deleted.";
            _msgPackageDeleted = "Package deleted.";

            _errProductNameRequired = "Product name is required.";
            _errProductPriceRequired = "Product price is required.";
            _errProductPricePositive = "Product price must be a positive number.";
            _errServiceNameRequired = "Service name is required.";
            _errServicePriceRequired = "Service price is required.";
            _errServicePricePositive = "Service price must be a positive number.";
        }

        if (string.IsNullOrWhiteSpace(Status) || Status == oldReady)
            Status = _statusReady;

        ValidateProdus();
        ValidateServiciu();
    }

    private void SetUi(ref string field, string value, [CallerMemberName] string? name = null)
    {
        if (field == value)
            return;
        field = value;
        OnPropertyChanged(name);
    }
}
