using System.Windows;
using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class UsuarioAppView : MetroWindow
    {
        public UsuarioAppView(UsuariosAppViewModel UsuariosAppViewModel)
        {
            InitializeComponent();
            UsuarioAppViewModel Modelo = new UsuarioAppViewModel(UsuariosAppViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}