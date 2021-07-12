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
    public class ClasesViewModel : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public ClasesViewModel Instancia {get;set;}
        public Clases Seleccionado {get;set;}
        private IDialogCoordinator dialogCoordinator {get;set;}
        public KalumDBContext dBContext = new KalumDBContext();
        private ObservableCollection<Clases> _Clases;
        public ObservableCollection<Clases> Clases
        {
            get
            {
                if(this._Clases == null)
                {
                    this._Clases = new ObservableCollection<Clases>(dBContext.Clases
                                                                    .Include(c => c.Instructores)
                                                                    .Include(s => s.Salones)
                                                                    .Include(c => c.CarrerasTecnicas)
                                                                    .Include(h => h.Horarios)
                                                                    .ToList());
                }
                return this._Clases;
            }
            set
            {
                this._Clases = value;
            }
        }

        public ClasesViewModel(IDialogCoordinator DialogCoordinator)
        {
            this.dialogCoordinator = dialogCoordinator;
            this.Instancia = this;
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
            if(parametro.Equals("Nuevo"))
            {
                if(this.Seleccionado != null)
                {
                    this.Seleccionado = null;
                }
                ClaseView ventanaClase = new ClaseView(this.Instancia);
                ventanaClase.ShowDialog();
            }
            else if(parametro.Equals("Modificar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Alumnos", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    ClaseView ventanaClase = new ClaseView(this.Instancia);
                    ventanaClase.ShowDialog();
                }
            }
            else if(parametro.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Debe de seleccionar un registro",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult resultado = await this.dialogCoordinator.
                        ShowMessageAsync(this,"Eliminar","¿está seguro que desea eliminar el registro?",
                        MessageDialogStyle.AffirmativeAndNegative);
                        if(resultado == MessageDialogResult.Affirmative)
                        {
                            try
                            {
                                int posicion = this.Clases.IndexOf(this.Seleccionado);
                                this.dBContext.Remove(this.Seleccionado);
                                this.dBContext.SaveChanges();
                                this.Clases.RemoveAt(posicion);
                                await this.dialogCoordinator.ShowMessageAsync(this,"Clases","Registro Eliminado exitosamente",
                                MessageDialogStyle.Affirmative);
                            }catch(Exception e)
                            {
                                await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                            }
                        }
                }
            }
        }
    }
}