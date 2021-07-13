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
    public class SeminarioViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public SeminarioViewModel Instancia { get; set; }
        public SeminariosViewModel SeminariosViewModel { get; set; }
        
        public string Titulo { get; set; }
        public string ValorNombreSeminario { get; set; }
        public DateTime ValorFechaInicio { get; set; } = DateTime.Now;
        public DateTime ValorFechaFinal { get; set; } = DateTime.Now;
        public string ModuloDefinido { get; set; } = "SELECCIONAR";
        private IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();
        public ObservableCollection<Modulos> Modulos { get; set; }
        public Modulos ModuloSeleccionado { get; set; }

        public SeminarioViewModel(SeminariosViewModel SeminariosViewModel, IDialogCoordinator dialogCoordinator)
        {
            this.Instancia = this;
            this.dialogCoordinator = dialogCoordinator;
            this.SeminariosViewModel = SeminariosViewModel;
            this.Modulos = new ObservableCollection<Modulos>(this.dBContext.Modulos.ToList());
            if (this.SeminariosViewModel.Seleccionado != null)
            {
                this.Titulo = "Modificar Registro";
                this.ValorNombreSeminario = this.SeminariosViewModel.Seleccionado.NombreSeminario;
                this.ValorFechaInicio = this.SeminariosViewModel.Seleccionado.FechaInicio;
                this.ValorFechaFinal = this.SeminariosViewModel.Seleccionado.FechaFinal;
                this.ModuloSeleccionado = this.SeminariosViewModel.Seleccionado.Modulos;
            }
            else
            {
                this.Titulo = "Nuevo Registro";
            }
        }

        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
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
                    if(this.SeminariosViewModel.Seleccionado == null)
                    {
                        Seminarios nuevoSeminario = new Seminarios();
                        nuevoSeminario.SeminarioId = Guid.NewGuid().ToString();
                        nuevoSeminario.NombreSeminario = ValorNombreSeminario;
                        nuevoSeminario.FechaInicio = Convert.ToDateTime(this.ValorFechaInicio);
                        nuevoSeminario.FechaFinal = Convert.ToDateTime(this.ValorFechaFinal);
                        nuevoSeminario.Modulos = ModuloSeleccionado;
                        dBContext.Seminarios.Add(nuevoSeminario);
                        dBContext.SaveChanges();
                        this.SeminariosViewModel.seminarios.Add(nuevoSeminario);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Seminarios","Registro agregado exitosamente");
                    }
                    else
                    {
                        int posicion = this.SeminariosViewModel.seminarios.IndexOf(this.SeminariosViewModel.Seleccionado);
                        Seminarios temporal = new Seminarios();
                        temporal.SeminarioId = this.SeminariosViewModel.Seleccionado.SeminarioId;
                        temporal.NombreSeminario = this.ValorNombreSeminario;
                        temporal.FechaInicio = Convert.ToDateTime(this.ValorFechaInicio);
                        temporal.FechaFinal = Convert.ToDateTime(this.ValorFechaFinal);
                        temporal.ModuloId = this.ModuloSeleccionado.ModuloId;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.SeminariosViewModel.seminarios.RemoveAt(posicion);
                        this.SeminariosViewModel.seminarios.Insert(posicion,temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Seminarios","Registro modificado existosamente");
                    }
                }catch (Exception e )
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                }
                ((Window)parametro).Close();
            }
        }
    }
}