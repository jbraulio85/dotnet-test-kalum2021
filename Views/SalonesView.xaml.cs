using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class SalonesView : MetroWindow
    {
        public SalonesView()
        {
            InitializeComponent();
            SalonesViewModel ModeloDatos = new SalonesViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}