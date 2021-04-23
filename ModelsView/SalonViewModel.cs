using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

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
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;

        public SalonViewModel(SalonesViewModel SalonesViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.SalonesViewModel = SalonesViewModel;
            if (this.SalonesViewModel.Seleccionado != null)
            {
                this.Salon = new Salones();
                this.Salon.SalonId = this.SalonesViewModel.Seleccionado.SalonId;
                this.Salon.Capacidad = this.SalonesViewModel.Seleccionado.Capacidad;
                this.Salon.Descripcion = this.SalonesViewModel.Seleccionado.Descripcion;
                this.Salon.NombreSalon = this.SalonesViewModel.Seleccionado.NombreSalon;
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
                if (this.SalonesViewModel.Seleccionado == null)
                {
                    Salones nuevo = new Salones("C-29", Capacidad, Descripcion, NombreSalon);
                    this.SalonesViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this, "Agregar Salón", "¡El salón a sido creado exitosamente",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    Salon.Capacidad = this.Capacidad;
                    Salon.Descripcion = this.Descripcion;
                    Salon.NombreSalon = this.NombreSalon;
                    int posicion = this.SalonesViewModel.salones.IndexOf(this.SalonesViewModel.Seleccionado);
                    this.SalonesViewModel.salones.RemoveAt(posicion);
                    this.SalonesViewModel.salones.Insert(posicion, Salon);
                    await dialogCoordinator.ShowMessageAsync(this, "Actualizar Salón", "¡El salón a sido modificado exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}