using System.Windows;
using kalum2021.ModelsView;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.Views
{
    public partial class RoleView : MetroWindow
    {
        public RoleView(RolesViewModel rolesViewModel)
        {
            InitializeComponent();
            RoleViewModel Modelo = new RoleViewModel(rolesViewModel, DialogCoordinator.Instance);
            this.DataContext= Modelo;
        }
    }
}