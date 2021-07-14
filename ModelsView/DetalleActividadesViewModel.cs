using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.DataContext;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using kalum2021.Views;

namespace kalum2021.ModelsView
{
    public class DetalleActividadesViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public DetalleActividadesViewModel Instancia {get;set;}
        private DetalleActividades _Seleccionado;
        public DetalleActividades Seleccionado
        {
            get
            {
                return this._Seleccionado;
            }
            set
            {
                this._Seleccionado = value;
                NotifificarCambio("Seleccionado");
            }
        }
        private IDialogCoordinator dialogCoordinator {get;set;}
        private KalumDBContext dBContext = new KalumDBContext();
        private ObservableCollection<DetalleActividades> _Actividades;
        public ObservableCollection<DetalleActividades> actividades
        {
            get
            {
                if(this._Actividades == null)
                {
                    this._Actividades = new ObservableCollection<DetalleActividades>(dBContext.DetalleActividades
                                                                                    .Include(s => s.Seminarios)
                                                                                    .ToList());
                }
                return this._Actividades;
            }
            set
            {
                this._Actividades = value;
            }
        }

        public DetalleActividadesViewModel(IDialogCoordinator dialogCoordinator)
        {
            this.dialogCoordinator = dialogCoordinator;
            this.Instancia = this;
        }

        public void NotifificarCambio(string propiedad)
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
                DetalleActividadView nuevaActividad = new DetalleActividadView(Instancia);
                nuevaActividad.ShowDialog();
            }
            else if(parametro.Equals("Modificar"))
            {
                if(this.Seleccionado ==  null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Actividades","Debe de Seleccionar un registro");
                }
                else
                {
                    DetalleActividadView modificarActividad = new DetalleActividadView(Instancia);
                    modificarActividad.ShowDialog();
                }
            }
            else if(parametro.Equals("Eliminar"))
            {
                if(this.Seleccionado ==  null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Actividades","Debe de Seleccionar un registro");
                }
                else
                {
                    try
                    {
                        int posicion = this.actividades.IndexOf(this.Seleccionado);
                        this.dBContext.Remove(this.Seleccionado);
                        this.dBContext.SaveChanges();
                        this.actividades.RemoveAt(posicion);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Actividades", "El registro fue eliminado exitosamente");
                    }catch(Exception e)
                    {
                        await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                    }
                }
            }
        }
    }
}