using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class RolesViewModel : INotifyPropertyChanged, ICommand 
    {
        public ObservableCollection<Roles> roles {get;set;}
        public RolesViewModel Instancia {get;set;}
        public Roles Seleccionado {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public RolesViewModel(IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
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

        public void agregarElemento(Roles nuevo)
        {
            this.roles.Add(nuevo);
        }
        public bool CanExecute(object parametro)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if(parametro.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                RoleView nuevoRole = new RoleView(Instancia);
                nuevoRole.Show();
            }
            else if (parametro.Equals("Eliminar"))
            {
                 if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Roles","Debe de seleccional un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar role","¿Está seguro de eliminar el registro?",
                    MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.roles.Remove(Seleccionado);
                    }
                }
            }
            else if (parametro.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Usuarios","Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    RoleView modificarRole = new RoleView(Instancia);
                    modificarRole.ShowDialog();
                }
            }
        }


    }
}