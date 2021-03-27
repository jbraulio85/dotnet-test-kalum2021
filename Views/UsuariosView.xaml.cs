using kalum2021.ModelsView;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class UsuariosView : MetroWindow
    {
        public UsuariosView()
        {
            InitializeComponent();
            UsuariosViewModel ModeloDatos = new UsuariosViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}