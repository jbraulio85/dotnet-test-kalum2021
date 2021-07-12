using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class ClaseView : MetroWindow
    {
        public ClaseView(ClasesViewModel ClasesViewModel)
        {
            InitializeComponent();
            ClaseViewModel Modelo = new ClaseViewModel(ClasesViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}