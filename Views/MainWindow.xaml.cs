using System.Windows;
using WpfApplicationData.ViewModels;

namespace WpfApplicationData.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new DateViewModel();
    }
}