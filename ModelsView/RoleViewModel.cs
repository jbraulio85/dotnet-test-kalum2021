using System.Collections.ObjectModel;
using System.ComponentModel;
using kalum2021.Models;
namespace kalum2021.ModelsView
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Roles> roles {get;set;}

        public RoleViewModel()
        {
            this.roles = new ObservableCollection<Roles>();
            this.roles.Add(new Roles(1,"ROLE_ADMIN"));   
            this.roles.Add(new Roles(2,"ROLE_USER")); 
            this.roles.Add(new Roles(3,"ROLE_SUPERV"));        
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