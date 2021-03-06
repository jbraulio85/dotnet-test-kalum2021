using System.Windows;
using kalum2021.ModelsView;
namespace kalum2021.Views
{
    public partial class RolesView : Window
    {
        public RolesView()
        {
            InitializeComponent();
            RoleViewModel ModeloDatos = new RoleViewModel();
            this.DataContext = ModeloDatos;
        }
    }
}