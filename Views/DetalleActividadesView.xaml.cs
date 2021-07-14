using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class DetalleActividadesView : MetroWindow
    {
        public DetalleActividadesView()
        {
            InitializeComponent();
            DetalleActividadesViewModel Modelo = new DetalleActividadesViewModel(DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}