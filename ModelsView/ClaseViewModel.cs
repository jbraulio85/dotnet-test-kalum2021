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
    public class ClaseViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ClaseViewModel Instancia { get; set; }
        public ClasesViewModel ClasesViewModel { get; set; }
        public string ValorDescripcion {get;set;}
        public string ValorCiclo {get;set;}
        public string ValorCupoMaximo{get;set;}
        public string ValorCupoMinimo {get;set;}
        public string Titulo { get; set; }
        public string InstructorDefindo {get;set;} = "SELECCIONAR";
        public string HorarioDefinido {get;set;} = "SELECCIONAR";
        public string CarreraTecnicaDefinida {get;set;} = "SELECCIONAR";
        public string SalonDefinido {get;set;} = "SELECCIONAR";
        private IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();
        private List<int> _Ciclos;
        private List<int> _CupoMaximo;
        private List<int> _CupoMinimo;
        public ObservableCollection<CarrerasTecnicas> CarrerasTecnicas {get;set;}
        public ObservableCollection<Instructores> Instructores {get;set;}
        public ObservableCollection<Salones> Salones{get;set;}
        public ObservableCollection<Horarios> Horarios {get;set;}
        public CarrerasTecnicas CarreraTecnicaSeleccionada {get;set;}
        public Instructores InstructorSeleccionado {get;set;}
        public Salones SalonSeleccionado {get;set;}
        public Horarios HorarioSeleccionado {get;set;}
        public List<int> CupoMinimo
        {
            get
            {
                if(this._CupoMinimo == null)
                {
                    this._CupoMinimo = new List<int>();
                    int i = 0;
                    do
                    {
                        i++;
                        this._CupoMinimo.Add(i);
                    }while(i < 30);
                }
                return this._CupoMinimo;
            }
            set
            {
                this._CupoMinimo = value;
            }
        }
        public List<int> CupoMaximo
        {
            get
            {
                if(this._CupoMaximo == null)
                {
                    this._CupoMaximo = new List<int>();
                    int i = 0;
                    while(i<30)
                    {
                        i++;
                        this._CupoMaximo.Add(i);
                    }
                }
                return this._CupoMaximo;
            }
            set
            {
                this._CupoMaximo = value;
            }
        }
        public List<int> Ciclos 
        { 
            get
            {
                if(_Ciclos ==  null)
                {
                    this._Ciclos = new List<int>();
                    for(int i=2020; i<=2030; i++)
                    {
                        this._Ciclos.Add(i);
                    }
                }
                return this._Ciclos;
            } 
            set
            {
                this._Ciclos = value;
            } 
        }
        public ClaseViewModel(ClasesViewModel ClasesViewModel, IDialogCoordinator DialogCoordinator)
        {
            this.Instancia = this;
            this.dialogCoordinator = DialogCoordinator;
            this.ClasesViewModel = ClasesViewModel;
            this.CarrerasTecnicas = new ObservableCollection<CarrerasTecnicas>(this.dBContext.CarrerasTecnicas.ToList());
            this.Instructores = new ObservableCollection<Instructores>(this.dBContext.Instructores.ToList());
            this.Salones = new ObservableCollection<Salones>(this.dBContext.Salones.ToList());
            this.Horarios = new ObservableCollection<Horarios>(this.dBContext.Horarios.ToList());
            if (ClasesViewModel.Seleccionado != null)
            {
                Titulo = "Modificar Registro";
                this.ValorDescripcion = this.ClasesViewModel.Seleccionado.Descripcion;
                this.ValorCiclo = this.ClasesViewModel.Seleccionado.Ciclo.ToString();
                this.ValorCupoMaximo = this.ClasesViewModel.Seleccionado.CupoMaximo.ToString();
                this.ValorCupoMinimo = this.ClasesViewModel.Seleccionado.CupoMinimo.ToString();
                this.InstructorSeleccionado = this.ClasesViewModel.Seleccionado.Instructores;
                this.HorarioSeleccionado = this.ClasesViewModel.Seleccionado.Horarios;
                this.SalonSeleccionado = this.ClasesViewModel.Seleccionado.Salones;
                this.CarreraTecnicaSeleccionada = this.ClasesViewModel.Seleccionado.CarrerasTecnicas;
                this.InstructorDefindo = this.ClasesViewModel.Seleccionado.Instructores.Apellidos + " " + this.ClasesViewModel.Seleccionado.Instructores.Nombres;
                this.HorarioDefinido = $"{this.ClasesViewModel.Seleccionado.Horarios.HorarioInicio.ToString(@"hh\:mm")} - {this.ClasesViewModel.Seleccionado.Horarios.HorarioFinal.ToString(@"hh\:mm")}";
                this.CarreraTecnicaDefinida = this.ClasesViewModel.Seleccionado.CarrerasTecnicas.NombreCarrera;
                this.SalonDefinido = this.ClasesViewModel.Seleccionado.Salones.NombreSalon;
            }
            else
            {
                this.Titulo = "Nuevo Registro";
                
            }
        }

        public void NotificarCambio(string Propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(Propiedad));
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
                    if(this.ClasesViewModel.Seleccionado == null)
                    {
                        Clases NuevaClase = new Clases();
                        NuevaClase.ClaseId = Guid.NewGuid().ToString();
                        NuevaClase.Descripcion = ValorDescripcion;
                        NuevaClase.Ciclo = Convert.ToInt16(ValorCiclo);
                        NuevaClase.CupoMaximo = Convert.ToInt16(ValorCupoMaximo);
                        NuevaClase.CupoMinimo = Convert.ToInt16(ValorCupoMinimo);
                        NuevaClase.CarrerasTecnicas = CarreraTecnicaSeleccionada;
                        NuevaClase.Horarios = HorarioSeleccionado;
                        NuevaClase.Instructores = InstructorSeleccionado;
                        NuevaClase.Salones = SalonSeleccionado;
                        dBContext.Clases.Add(NuevaClase);
                        dBContext.SaveChanges();
                        this.ClasesViewModel.Clases.Add(NuevaClase);
                        await this.dialogCoordinator.ShowMessageAsync(this, "Clases","Registro almacenado correctamente");
                    }else {
                        int posicion = this.ClasesViewModel.Clases.IndexOf(this.ClasesViewModel.Seleccionado);
                        Clases temporal = new Clases();
                        temporal.ClaseId = this.ClasesViewModel.Seleccionado.ClaseId;
                        temporal.Descripcion = this.ValorDescripcion;
                        temporal.Ciclo = Convert.ToInt16(this.ValorCiclo);
                        temporal.CupoMaximo = Convert.ToInt16(this.ValorCupoMaximo);
                        temporal.CupoMinimo = Convert.ToInt16(this.ValorCupoMinimo);
                        temporal.CodigoCarrera = this.CarreraTecnicaSeleccionada.CodigoCarrera; 
                        temporal.InstructorId = this.InstructorSeleccionado.InstructorId;
                        temporal.HorarioId = this.HorarioSeleccionado.HorarioId;
                        temporal.SalonId = this.SalonSeleccionado.SalonId;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.ClasesViewModel.Clases.RemoveAt(posicion);
                        this.ClasesViewModel.Clases.Insert(posicion,temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Registro Actualizado");
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