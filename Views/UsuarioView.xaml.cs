using System.Windows;
using kalum2021.ModelsView;

namespace kalum2021.Views
{
    public partial class UsuarioView : Window
    {
        public UsuarioView(UsuariosViewModel UsuariosViewModel)
        {
            InitializeComponent();
            UsuarioViewModel Modelo = new UsuarioViewModel(UsuariosViewModel);
            this.DataContext = Modelo;
        }
    }
}