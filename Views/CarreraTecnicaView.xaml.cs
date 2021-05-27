using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class CarreraTecnicaView : MetroWindow
    {
        public CarreraTecnicaView(CarrerasTecnicasViewModel CarrerasTecnicasViewModel)
        {
            InitializeComponent();
            CarreraTecnicaViewModel Modelo = new CarreraTecnicaViewModel(CarrerasTecnicasViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}