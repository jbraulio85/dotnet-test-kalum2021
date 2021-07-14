using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;

namespace kalum2021.ModelsView
{
    public class UsuariosAppViewModel : INotifyPropertyChanged, ICommand
    {
        private ObservableCollection<UsuariosApp> _Usuarios;
        public ObservableCollection<UsuariosApp> usuarios
        {
            get
            {
                if(this._Usuarios == null)
                {
                    this._Usuarios = new ObservableCollection<UsuariosApp>(dBContext.UsuariosApp.ToList());
                }
                return this._Usuarios;
            }
            set
            {
                this._Usuarios = value;
            }
        }

        public UsuariosAppViewModel Instancia {get;set;}
        public UsuariosApp Seleccionado {get;set;}
        private IDialogCoordinator dialogCoordinator;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private KalumDBContext dBContext = new KalumDBContext();

        public UsuariosAppViewModel(IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(UsuariosApp nuevo)
        {
            this.usuarios.Add(nuevo);
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
                UsuarioAppView nuevoUsuario = new UsuarioAppView(this.Instancia);
                nuevoUsuario.Show();
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Usuarios", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar Usuario", "¿Está seguro de eliminar el registro?", MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            int posicion = this.usuarios.IndexOf(this.Seleccionado);
                            this.dBContext.Remove(this.Seleccionado);
                            this.dBContext.SaveChanges();
                            this.usuarios.RemoveAt(posicion);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Usuarios","El registro fue eliminado exitosamente");
                        }catch (Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                        }
                    }
                }
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Usuarios", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    UsuarioAppView modificarUsuario = new UsuarioAppView(this.Instancia);
                    modificarUsuario.ShowDialog();
                }
            }
        }
    }
}
