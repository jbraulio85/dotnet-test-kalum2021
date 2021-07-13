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
    public class SeminariosViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public SeminariosViewModel Instancia {get;set;}
        private Seminarios _Seleccionado;
        public Seminarios Seleccionado
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
        private ObservableCollection<Seminarios> _Seminarios;
        public ObservableCollection<Seminarios> seminarios
        {
            get
            {
                if(this._Seminarios == null)
                {
                    this._Seminarios = new ObservableCollection<Seminarios>(dBContext.Seminarios
                                                                            .Include(m => m.Modulos)
                                                                            .ToList());
                }
                return this._Seminarios;
            }
            set
            {
                this._Seminarios = value;
            }
        }

        public SeminariosViewModel(IDialogCoordinator dialogCoordinator)
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
                SeminarioView ventanaSeminario = new SeminarioView(this.Instancia);
                ventanaSeminario.ShowDialog();
            }
            else if(parametro.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Seminarios","Debe de Seleccionar un registro");
                }
                else
                {
                    SeminarioView ventanaSeminario = new SeminarioView(this.Instancia);
                    ventanaSeminario.ShowDialog();
                }
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Seminarios","Debe de Seleccionar un registro");
                }
                else
                {
                    MessageDialogResult resultado = await this.dialogCoordinator.ShowMessageAsync(this,"Eliminar",
                            "¿Está seguro de eliminar este registro?",MessageDialogStyle.AffirmativeAndNegative);
                    if(resultado == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            int posicion = this.seminarios.IndexOf(this.Seleccionado);
                            this.dBContext.Remove(this.Seleccionado);
                            this.dBContext.SaveChanges();
                            this.seminarios.RemoveAt(posicion);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Seminarios","El registro fue eliminado");
                        }
                        catch (Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                        }
                    }
                }
            }
        }
    }
}