using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class SeminarioView : MetroWindow
    {
        public SeminarioView(SeminariosViewModel SeminariosViewModel)
        {
            InitializeComponent();
            SeminarioViewModel Modelo = new SeminarioViewModel(SeminariosViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}