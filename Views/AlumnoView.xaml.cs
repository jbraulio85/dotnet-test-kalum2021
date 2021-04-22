using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class AlumnoView : MetroWindow
    {
        public AlumnoView(AlumnosViewModel AlumnosViewModel)
        {
            InitializeComponent();
            AlumnoViewModel Modelo = new AlumnoViewModel(AlumnosViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}