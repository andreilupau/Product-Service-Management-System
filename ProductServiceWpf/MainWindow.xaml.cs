using System.Windows;
using ProductServiceWpf.Services;
using ProductServiceWpf.ViewModels;

namespace ProductServiceWpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowVm(new CoreDataService());
    }
}

