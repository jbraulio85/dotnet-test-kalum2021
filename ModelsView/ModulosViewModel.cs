using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using kalum2021.Views;

namespace kalum2021.ModelsView
{
    public class ModulosViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ModulosViewModel Instancia {get;set;}
        private Modulos _Seleccionado;
        public Modulos Seleccionado
        {
            get
            {
                return this._Seleccionado;
            }
            set
            {
                this._Seleccionado = value;
                NotificarCambio("Seleccionado");
            }
        }
        private IDialogCoordinator dialogCoordinator {get;set;}
        private KalumDBContext dBContext = new KalumDBContext();
        private ObservableCollection<Modulos> _Modulos;
        public ObservableCollection<Modulos> modulos
        {
            get
            {
                if(this._Modulos == null)
                {
                    this._Modulos = new ObservableCollection<Modulos>(dBContext.Modulos
                                                                        .Include(c => c.CarrerasTecnicas)
                                                                        .ToList());
                }
                return this._Modulos;
            }
            set
            {
                this._Modulos = value;
            }
        }

        public ModulosViewModel(IDialogCoordinator dialogCoordinator)
        {
            this.dialogCoordinator = dialogCoordinator;
            this.Instancia = this;
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }
        public bool CanExecute(object parametro)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if(parametro.Equals("Nuevo"))
            {
                if(this.Seleccionado != null)
                {
                    this.Seleccionado = null;
                }
                ModuloView ventanaModulo = new ModuloView(this.Instancia);
                ventanaModulo.ShowDialog();
            }
            else if(parametro.Equals("Modificar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Modulos", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    ModuloView ventanaModulo = new ModuloView(this.Instancia);
                    ventanaModulo.ShowDialog();
                }
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Modulos", "Debe de seleccionar un registro",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult resultado = await this.dialogCoordinator.
                    ShowMessageAsync(this, "Eliminar", "¿está seguro que desea eliminar el registro?", MessageDialogStyle.
                    AffirmativeAndNegative);
                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            int posicion = this.modulos.IndexOf(this.Seleccionado);
                            this.dBContext.Remove(this.Seleccionado);
                            this.dBContext.SaveChanges();
                            this.modulos.RemoveAt(posicion);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Modulos","El registo fue eliminado exitosamente");
                        }catch (Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                        }
                    }
                }
            }
        }
    }
}