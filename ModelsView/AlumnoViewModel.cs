using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class AlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        
        public AlumnoViewModel Instancia {get;set;}
        public AlumnosViewModel AlumnosviewModel {get;set;}
        public string Carne {get;set;}
        public string NoExpediente {get;set;}
        public string Apellidos {get;set;}
        public string Nombres {get;set;}
        public string Email {get;set;}
        public Alumnos Alumno {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public AlumnoViewModel(AlumnosViewModel AlumnosViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.AlumnosviewModel = AlumnosViewModel;
            if(this.AlumnosviewModel.Seleccionado != null)
            {
                this.Alumno = new Alumnos();
                this.Alumno.Carne = this.AlumnosviewModel.Seleccionado.Carne;
                this.NoExpediente = this.AlumnosviewModel.Seleccionado.NoExpediente;
                this.Apellidos = this.AlumnosviewModel.Seleccionado.Apellidos;
                this.Nombres = this.AlumnosviewModel.Seleccionado.Nombres;
                this.Email = this.AlumnosviewModel.Seleccionado.Email;
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
                if(this.AlumnosviewModel.Seleccionado == null)
                {
                    Alumnos nuevo = new Alumnos ("2021005", NoExpediente, Apellidos, Nombres, Email);
                    this.AlumnosviewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this,"Agregar alumno","¡El registro fue agregado exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    Alumno.NoExpediente = this.NoExpediente;
                    Alumno.Apellidos = this.Apellidos;
                    Alumno.Nombres = this.Nombres;
                    Alumno.Email = this.Email;
                    int posicion = this.AlumnosviewModel.alumnos.IndexOf(this.AlumnosviewModel.Seleccionado);
                    this.AlumnosviewModel.alumnos.RemoveAt(posicion);
                    this.AlumnosviewModel.alumnos.Insert(posicion,Alumno);
                    await dialogCoordinator.ShowMessageAsync(this,"Modificador de alumnos","¡El registro fue modificado exitosamente!",
                    MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}