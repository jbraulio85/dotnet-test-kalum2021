using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace kalum2021.ModelsView
{
    public class CarreraTecnicaViewModel : INotifyPropertyChanged, ICommand
    {
        public CarreraTecnicaViewModel Instancia {get;set;}
        public CarrerasTecnicasViewModel CarrerasTecnicasViewModel {get;set;}
        public string NombreCarrera {get;set;}
        public CarrerasTecnicas CarreraTecnica {get;set;}
        public KalumDBContext dBContext;
        public string Titulo {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public CarreraTecnicaViewModel(CarrerasTecnicasViewModel CarrerasTecnicasViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.dBContext = new KalumDBContext();
            this.CarrerasTecnicasViewModel = CarrerasTecnicasViewModel;
            if(this.CarrerasTecnicasViewModel.Seleccionado != null)
            {
                this.NombreCarrera = this.CarrerasTecnicasViewModel.Seleccionado.NombreCarrera;
                this.Titulo ="Actualizando Registro";
            }
            else if (this.CarrerasTecnicasViewModel.Seleccionado == null)
            {
                this.Titulo ="Nuevo Registro";
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
                    if(this.CarrerasTecnicasViewModel.Seleccionado == null)
                    {
                        var CarreraParameter = new SqlParameter("@NombreCarrera",this.NombreCarrera);
                        var Resultado = this.dBContext
                                            .CarrerasTecnicas
                                            .FromSqlRaw("sp_registrar_carrera @NombreCarrera",
                                                        CarreraParameter)
                                            .ToList();
                            foreach(Object registro in Resultado)
                            {
                                this.CarrerasTecnicasViewModel.carreras.Add((CarrerasTecnicas)registro);
                            }
                            await dialogCoordinator.ShowMessageAsync(this,"Agregar Carrera","El registro fue creado exitosamente",
                            MessageDialogStyle.Affirmative);
                    }
                    else if (this.CarrerasTecnicasViewModel.Seleccionado != null)
                    {
                        int posicion = this.CarrerasTecnicasViewModel.carreras.IndexOf(this.CarrerasTecnicasViewModel.Seleccionado);
                        CarrerasTecnicas temporal = new CarrerasTecnicas();
                        temporal.CodigoCarrera = this.CarrerasTecnicasViewModel.Seleccionado.CodigoCarrera;
                        temporal.NombreCarrera = this.NombreCarrera;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.CarrerasTecnicasViewModel.carreras.RemoveAt(posicion);
                        this.CarrerasTecnicasViewModel.carreras.Insert(posicion,temporal);
                        await dialogCoordinator.ShowMessageAsync(this,"Carreras","El registro fue modificado exitosamente");
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