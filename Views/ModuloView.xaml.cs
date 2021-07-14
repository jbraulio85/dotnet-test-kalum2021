using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class ModuloView : MetroWindow
    {
        public ModuloView(ModulosViewModel ModulosViewModel)
        {
            InitializeComponent();
            ModuloViewModel Modelo = new ModuloViewModel(ModulosViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}