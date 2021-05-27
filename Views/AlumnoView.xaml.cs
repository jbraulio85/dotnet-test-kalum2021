using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class AlumnoView : MetroWindow
    {
        private AlumnoViewModel Modelo;
        private AlumnosViewModel AlumnosViewModel { get; set; }
        public AlumnoView(AlumnosViewModel ModelAlumnos)
        {
            InitializeComponent();
            this.AlumnosViewModel = ModelAlumnos;
            Modelo = new AlumnoViewModel(AlumnosViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}