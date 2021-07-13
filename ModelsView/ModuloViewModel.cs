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
    public class ModuloViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ModuloViewModel Instancia {get;set;}
        public ModulosViewModel ModulosViewModel {get;set;}
        public string ValorNombreModulo {get;set;}
        public string ValorSeminarios {get;set;}
        public string Titulo {get;set;}
        public string CarreraTecnicaDefinida {get;set;} = "SELECCIONAR";
        private IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();
        private List<int> _Seminarios;
        public ObservableCollection<CarrerasTecnicas> CarrerasTecnicas {get;set;}
        public CarrerasTecnicas CarreraTecnicaSeleccionada {get;set;}
        public List<int> seminarios
        {
            get
            {
                if(this._Seminarios == null)
                {
                    this._Seminarios = new List<int>();
                    int i = 0;
                    do
                    {
                        i++;
                        this._Seminarios.Add(i);
                    }while(i < 10);
                }
                return this._Seminarios;
            }
            set
            {
                this._Seminarios = value;
            }
        }

        public ModuloViewModel(ModulosViewModel ModulosViewModel, IDialogCoordinator dialogCoordinator)
        {
            this.Instancia = this;
            this.dialogCoordinator = dialogCoordinator;
            this.ModulosViewModel = ModulosViewModel;
            this.CarrerasTecnicas = new ObservableCollection<CarrerasTecnicas>(this.dBContext.CarrerasTecnicas.ToList());
            if (ModulosViewModel.Seleccionado != null)
            {
                this.Titulo ="Modificar Registro";
                this.ValorNombreModulo = this.ModulosViewModel.Seleccionado.NombreModulo;
                this.ValorSeminarios = this.ModulosViewModel.Seleccionado.NumeroSeminarios.ToString();
                this.CarreraTecnicaSeleccionada = this.ModulosViewModel.Seleccionado.CarrerasTecnicas;
            }
            else
            {
                this.Titulo="Nuevo Registro";
            }
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propiedad));
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
                    if(this.ModulosViewModel.Seleccionado == null)
                    {
                        Modulos nuevoModulo = new Modulos();
                        nuevoModulo.ModuloId = Guid.NewGuid().ToString();
                        nuevoModulo.NombreModulo = ValorNombreModulo;
                        nuevoModulo.NumeroSeminarios = Convert.ToInt16(this.ValorSeminarios);
                        nuevoModulo.CarrerasTecnicas = CarreraTecnicaSeleccionada;
                        dBContext.Modulos.Add(nuevoModulo);
                        dBContext.SaveChanges();
                        this.ModulosViewModel.modulos.Add(nuevoModulo);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Modulos","El registro fue agregado exitosamente");
                    }
                    else
                    {
                        int posicion = this.ModulosViewModel.modulos.IndexOf(this.ModulosViewModel.Seleccionado);
                        Modulos temporal = new Modulos();
                        temporal.ModuloId = this.ModulosViewModel.Seleccionado.ModuloId;
                        temporal.NombreModulo = this.ValorNombreModulo;
                        temporal.NumeroSeminarios = Convert.ToInt16(this.ValorSeminarios);
                        temporal.CodigoCarrera = this.CarreraTecnicaSeleccionada.CodigoCarrera;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.ModulosViewModel.modulos.RemoveAt(posicion);
                        this.ModulosViewModel.modulos.Insert(posicion,temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Modulos","Registro modificado exitosamente");
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