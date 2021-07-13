using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class SeminariosView : MetroWindow
    {
        public SeminariosView()
        {
            InitializeComponent();
            SeminariosViewModel ModeloDatos = new SeminariosViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}