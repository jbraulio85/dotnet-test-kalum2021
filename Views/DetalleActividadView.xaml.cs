using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class DetalleActividadView : MetroWindow
    {
        public DetalleActividadView(DetalleActividadesViewModel DetalleActividadesViewModel)
        {
            InitializeComponent();
            DetalleActividadViewModel Modelo = new DetalleActividadViewModel(DetalleActividadesViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}