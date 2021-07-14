using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.DataContext;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Collections.ObjectModel;
using kalum2021.Models;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace kalum2021.ModelsView
{
    public class DetalleActividadViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public DetalleActividadViewModel Instancia {get;set;}
        public DetalleActividadesViewModel DetalleActividadesViewModel {get;set;}
        public string Titulo {get;set;}
        public string ValorNombreActividad {get;set;}
        public int ValorNotaActividad {get;set;}
        public DateTime ValorFechaEntrega {get;set;}
        public DateTime ValorFechaPostergacion {get;set;}
        public string ValorEstado {get;set;}
        public string SeminarioDefinido {get;set;} = "SELECCIONAR";
        private IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();
        public ObservableCollection<Seminarios> Seminarios {get;set;}
        public Seminarios SeminarioSeleccionado {get;set;}

        public DetalleActividadViewModel(DetalleActividadesViewModel DetalleActividadesViewModel, IDialogCoordinator dialogCoordinator)
        {
            this.Instancia = this;
            this.dialogCoordinator = dialogCoordinator;
            this.DetalleActividadesViewModel = DetalleActividadesViewModel;
            this.Seminarios = new ObservableCollection<Seminarios>(this.dBContext.Seminarios.ToList());
            if(this.DetalleActividadesViewModel.Seleccionado != null)
            {
                this.Titulo = "Modificar Registro";
                this.ValorNombreActividad = this.DetalleActividadesViewModel.Seleccionado.NombreActividad;
                this.ValorNotaActividad = this.DetalleActividadesViewModel.Seleccionado.NotaActividad;
                this.ValorFechaEntrega = this.DetalleActividadesViewModel.Seleccionado.FechaEntrega;
                this.ValorFechaPostergacion = this.DetalleActividadesViewModel.Seleccionado.FechaPostergacion;
                this.ValorEstado = this.DetalleActividadesViewModel.Seleccionado.Estado;
                this.SeminarioSeleccionado = this.DetalleActividadesViewModel.Seleccionado.Seminarios;
            }
            else
            {
                this.Titulo ="Nuevo registro";
            }
        }

        public void NotifificarCambio (string propiedad)
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
                if(parametro is Window)
                {
                    try
                    {
                        if(this.DetalleActividadesViewModel.Seleccionado == null)
                        {
                            DetalleActividades nuevoDetalle = new DetalleActividades();
                            nuevoDetalle.DetalleActividadId = Guid.NewGuid().ToString();
                            nuevoDetalle.NombreActividad = ValorNombreActividad;
                            nuevoDetalle.NotaActividad = ValorNotaActividad;
                            nuevoDetalle.FechaCreacion = DateTime.Today;
                            nuevoDetalle.FechaEntrega = Convert.ToDateTime(ValorFechaEntrega);
                            nuevoDetalle.FechaPostergacion = Convert.ToDateTime(ValorFechaPostergacion);
                            nuevoDetalle.Estado = ValorEstado;
                            nuevoDetalle.Seminarios = SeminarioSeleccionado;
                            dBContext.DetalleActividades.Add(nuevoDetalle);
                            dBContext.SaveChanges();
                            this.DetalleActividadesViewModel.actividades.Add(nuevoDetalle);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Detalle de actividades","Nuevo regitro agregado exitosamente");
                        }
                        else
                        {
                            int posicion = this.DetalleActividadesViewModel.actividades.IndexOf(this.DetalleActividadesViewModel.Seleccionado);
                            DetalleActividades temporal = new DetalleActividades();
                            temporal.DetalleActividadId = this.DetalleActividadesViewModel.Seleccionado.DetalleActividadId;
                            temporal.NombreActividad = this.ValorNombreActividad;
                            temporal.NotaActividad = this.ValorNotaActividad;
                            temporal.FechaCreacion = this.DetalleActividadesViewModel.Seleccionado.FechaCreacion;
                            temporal.FechaEntrega = Convert.ToDateTime(this.ValorFechaEntrega);
                            temporal.FechaPostergacion = Convert.ToDateTime(this.ValorFechaPostergacion);
                            temporal.Estado = this.ValorEstado;
                            temporal.SeminarioId = this.SeminarioSeleccionado.SeminarioId;
                            this.dBContext.Entry(temporal).State = EntityState.Modified;
                            this.dBContext.SaveChanges();
                            this.DetalleActividadesViewModel.actividades.RemoveAt(posicion);
                            this.DetalleActividadesViewModel.actividades.Insert(posicion,temporal);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Actividades","Se modificó el registro exitosamente");
                        }
                    }catch(Exception e)
                    {
                        await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                    }
                    ((Window)parametro).Close();
                }
        }
    }
}