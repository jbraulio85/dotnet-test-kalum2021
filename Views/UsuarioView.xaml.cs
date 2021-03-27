using System.Windows;
using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class UsuarioView : MetroWindow
    {
        public UsuarioView(UsuariosViewModel UsuariosViewModel)
        {
            InitializeComponent();
            UsuarioViewModel Modelo = new UsuarioViewModel(UsuariosViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}