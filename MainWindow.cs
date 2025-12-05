using System.Windows;
using ConsoleApp1.ViewModels;

namespace ConsoleApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // DataContext jest ustawiony w XAML, ale mo¿na te¿ przypisaæ tutaj:
            // DataContext = new MainViewModel();
        }
    }
}