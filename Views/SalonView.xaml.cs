using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class SalonView : MetroWindow
    {
        public SalonView(SalonesViewModel SalonesViewModel)
        {
            InitializeComponent();
            SalonViewModel Modelo = new SalonViewModel(SalonesViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}