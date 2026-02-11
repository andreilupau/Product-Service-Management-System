using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lab2;


namespace ProductServiceWpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowVm();
    }

    private MainWindowVm Vm => (MainWindowVm)DataContext;

    private void LoadSample_Click(object sender, RoutedEventArgs e)
    {
        Vm.Items.Clear();
        Vm.Items.Add(new { Tip = "Produs", Nume = "Exemplu", Pret = 100, Categorie = "Demo" });
        Vm.Items.Add(new { Tip = "Serviciu", Nume = "Exemplu 2", Pret = 200, Categorie = "Demo" });
        Vm.Status = "Loaded sample rows.";
    }

    private void LoadFromXml_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "p_s.xml");

            var pm = new ProduseManager();
            var sm = new ServiciiManager();

            pm.CitesteProduseDinXml(xmlPath);
            sm.CitesteServiciiDinXml(xmlPath);

            var items = ProduseServiciiManagerAbstract.GetArray();

            Vm.Items.Clear();
            foreach (var item in items)
                Vm.Items.Add(item);

            Vm.Status = $"Loaded {items.Count} items from XML.";
        }
        catch (System.Exception ex)
        {
            Vm.Status = "XML load failed: " + ex.Message;
            MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        Vm.Items.Clear();
        Vm.Status = "Cleared.";
    }

    private sealed class MainWindowVm : INotifyPropertyChanged
    {
        public ObservableCollection<object> Items { get; } = new();

        private string _status = "Ready.";
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}