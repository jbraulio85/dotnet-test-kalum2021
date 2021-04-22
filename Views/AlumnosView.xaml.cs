using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class AlumnosView : MetroWindow
    {
        public AlumnosView()
        {
            InitializeComponent();
            AlumnosViewModel ModeloDatos = new AlumnosViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}