using System.Windows;
using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class RolesView : MetroWindow
    {
        public RolesView()
        {
            InitializeComponent();
            RolesViewModel ModeloDatos = new RolesViewModel(DialogCoordinator.Instance);
            this.DataContext = ModeloDatos;
        }
    }
}