using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class InstructoresView : MetroWindow
    {
        public InstructoresView()
        {
            InitializeComponent();
            InstructoresViewModel ModeloDatos = new InstructoresViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}