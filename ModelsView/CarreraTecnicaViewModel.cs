using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class CarreraTecnicaViewModel : INotifyPropertyChanged, ICommand
    {
        public CarreraTecnicaViewModel Instancia {get;set;}
        public CarrerasTecnicasViewModel CarrerasTecnicasViewModel {get;set;}
        public string NombreCarrera {get;set;}
        public CarrerasTecnicas CarreraTecnica {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public CarreraTecnicaViewModel(CarrerasTecnicasViewModel CarrerasTecnicasViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.CarrerasTecnicasViewModel = CarrerasTecnicasViewModel;
            if(this.CarrerasTecnicasViewModel.Seleccionado != null)
            {
                this.CarreraTecnica = new CarrerasTecnicas();
                this.CarreraTecnica.CodigoCarrera = this.CarrerasTecnicasViewModel.Seleccionado.CodigoCarrera;
                this.NombreCarrera = this.CarrerasTecnicasViewModel.Seleccionado.NombreCarrera;
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
                if(this.CarrerasTecnicasViewModel.Seleccionado == null)
                {
                    CarrerasTecnicas nuevo = new CarrerasTecnicas("9A515FB3-3686-4638-8E2B-2D754DEBD789",NombreCarrera);
                    this.CarrerasTecnicasViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar Carrera","¡La carrera a sido creado exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    CarreraTecnica.NombreCarrera = this.NombreCarrera;
                    int posicion = this.CarrerasTecnicasViewModel.carreras.IndexOf(this.CarrerasTecnicasViewModel.Seleccionado);
                    this.CarrerasTecnicasViewModel.carreras.RemoveAt(posicion);
                    this.CarrerasTecnicasViewModel.carreras.Insert(posicion,CarreraTecnica);
                    await dialogCoordinator.ShowMessageAsync(this,"Actualizar carrera","¡La carrera a sido modificada exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}