using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class InstructorView : MetroWindow
    {
        public InstructorView (InstructoresViewModel InstructoresViewModel)
        {
            InitializeComponent();
            InstructorViewModel Modelo = new InstructorViewModel(InstructoresViewModel, DialogCoordinator.Instance);
            this.DataContext = Modelo;
        }
    }
}