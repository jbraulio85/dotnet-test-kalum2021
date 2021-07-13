using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class UsuariosAppView : MetroWindow
    {
        public UsuariosAppView()
        {
            InitializeComponent();
            UsuariosAppViewModel ModeloDatos = new UsuariosAppViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}