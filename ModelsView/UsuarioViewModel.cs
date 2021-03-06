using System.Collections.ObjectModel;
using System.ComponentModel;
using kalum2021.Models;

namespace kalum2021.ModelsView
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Usuarios> usuarios {get;set;}

        public UsuarioViewModel()
        {
            this.usuarios = new ObservableCollection<Usuarios>();
            this.usuarios.Add(new Usuarios(1,"becheverria",true,"Braulio","Echeverria","braulioecheverria@kinal.org.gt"));
            this.usuarios.Add(new Usuarios(2,"jmontufar",true,"José","Montufar","jbmontufar85@gmail.com"));
            this.usuarios.Add(new Usuarios(3,"smejia",true,"Silvana","Mejia","smejia@gmail.com"));
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}