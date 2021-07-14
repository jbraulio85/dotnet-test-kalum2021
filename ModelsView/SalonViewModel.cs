using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace kalum2021.ModelsView
{
    public class SalonViewModel : INotifyPropertyChanged, ICommand
    {
        public SalonViewModel Instancia { get; set; }
        public SalonesViewModel SalonesViewModel { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public string NombreSalon { get; set; }
        public Salones Salon { get; set; }
        public string Titulo {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();

        public SalonViewModel(SalonesViewModel SalonesViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.SalonesViewModel = SalonesViewModel;
            this.dBContext = new KalumDBContext();
            if (this.SalonesViewModel.Seleccionado != null)
            {
                this.Capacidad = this.SalonesViewModel.Seleccionado.Capacidad;
                this.Descripcion = this.SalonesViewModel.Seleccionado.Descripcion;
                this.NombreSalon = this.SalonesViewModel.Seleccionado.NombreSalon;
                this.Titulo = "Actualizando registro";
            }
            else if(this.SalonesViewModel.Seleccionado == null)
            {
                this.Titulo = "Nuevo Registro";
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
                try
                {
                    if(this.SalonesViewModel.Seleccionado == null)
                    {
                        Salones nuevoSalon = new Salones();
                        nuevoSalon.SalonId = Guid.NewGuid().ToString();
                        nuevoSalon.NombreSalon = NombreSalon;
                        nuevoSalon.Capacidad = Capacidad;
                        nuevoSalon.Descripcion = Descripcion;
                        dBContext.Salones.Add(nuevoSalon);
                        dBContext.SaveChanges();
                        this.SalonesViewModel.salones.Add(nuevoSalon);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Salones","El registro se agregó exitosamente",
                        MessageDialogStyle.Affirmative);
                    }else if (this.SalonesViewModel.Seleccionado != null)
                    {
                        int posicion = this.SalonesViewModel.salones.IndexOf(this.SalonesViewModel.Seleccionado);
                        Salones temporal = new Salones();
                        temporal.SalonId = this.SalonesViewModel.Seleccionado.SalonId;
                        temporal.NombreSalon = this.NombreSalon;
                        temporal.Capacidad = this.Capacidad;
                        temporal.Descripcion = this.Descripcion;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.SalonesViewModel.salones.RemoveAt(posicion);
                        this.SalonesViewModel.salones.Insert(posicion,temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Salones","El registro fue modificado exitosamente",
                        MessageDialogStyle.Affirmative);
                    }
                }catch (Exception e)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                }
                ((Window)parametro).Close();
            }
        }
    }
}