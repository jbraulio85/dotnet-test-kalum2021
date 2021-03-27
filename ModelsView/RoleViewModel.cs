using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class RoleViewModel : INotifyPropertyChanged, ICommand
    {
        public RoleViewModel Instancia { get; set; }
        public RolesViewModel RolesViewModel { get; set; }
        public string RoleNombre { get; set; }
        public Roles Role { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        private IDialogCoordinator dialogCoordinator;

        public RoleViewModel(RolesViewModel RolesViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.RolesViewModel = RolesViewModel;
            if (this.RolesViewModel.Seleccionado != null)
            {
                this.Role = new Roles();
                this.Role.RoleId = this.RolesViewModel.Seleccionado.RoleId;
                this.RoleNombre = this.RolesViewModel.Seleccionado.RoleNombre;
            }
        }

        public bool CanExecute(object parametro)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if (parametro is Window)
            {
                if (this.RolesViewModel.Seleccionado == null)
                {
                    Roles nuevo = new Roles(105, RoleNombre);
                    this.RolesViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar roles","¡El role fue creado exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    Role.RoleNombre = this.RoleNombre;
                    int posicion = this.RolesViewModel.roles.IndexOf(this.RolesViewModel.Seleccionado);
                    this.RolesViewModel.roles.RemoveAt(posicion);
                    this.RolesViewModel.roles.Insert(posicion, Role);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificador de roles","¡El role fue modificado exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}