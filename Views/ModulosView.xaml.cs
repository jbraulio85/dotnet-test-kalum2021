using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class ModulosView : MetroWindow
    {
        public ModulosView()
        {
            InitializeComponent();
            ModulosViewModel ModeloDatos = new ModulosViewModel (DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}