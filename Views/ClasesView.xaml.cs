using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views

{
    public partial class ClasesView : MetroWindow
    {
        public ClasesView()
        {
            InitializeComponent();
            ClasesViewModel Modelo = new ClasesViewModel(DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}