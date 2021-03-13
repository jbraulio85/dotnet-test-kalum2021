using kalum2021.ModelsView;
using System.Windows;

namespace kalum2021.Views
{
    public partial class UsuariosView : Window
    {
        public UsuariosView()
        {
            InitializeComponent();
            UsuariosViewModel ModeloDatos = new UsuariosViewModel();
            this.DataContext = ModeloDatos;
        }
    }
}