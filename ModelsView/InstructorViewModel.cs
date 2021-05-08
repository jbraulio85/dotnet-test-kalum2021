using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class InstructorViewModel : INotifyPropertyChanged, ICommand
    {
        public InstructorViewModel Instancia { get; set; }
        public InstructoresViewModel InstructoresViewModel { get; set; }
        public string Apellidos { get; set; }
        public string Comentario { get; set; }
        public string Direccion { get; set; }
        public string Estatus { get; set; }
        public string Foto { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public Instructores Instructor { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public InstructorViewModel(InstructoresViewModel InstructoresViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.InstructoresViewModel = InstructoresViewModel;
            if (this.InstructoresViewModel.Seleccionado != null)
            {
                this.Instructor = new Instructores();
                this.Instructor.InstructorId = this.InstructoresViewModel.Seleccionado.InstructorId;
                this.Apellidos = this.InstructoresViewModel.Seleccionado.Apellidos;
                this.Comentario = this.InstructoresViewModel.Seleccionado.Comentario;
                this.Direccion = this.InstructoresViewModel.Seleccionado.Direccion;
                this.Estatus = this.InstructoresViewModel.Seleccionado.Estatus;
                this.Foto = this.InstructoresViewModel.Seleccionado.Foto;
                this.Nombres = this.InstructoresViewModel.Seleccionado.Nombres;
                this.Telefono = this.InstructoresViewModel.Seleccionado.Telefono;
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
                if (this.InstructoresViewModel.Seleccionado == null)
                {
                    Instructores nuevo = new Instructores("205", Apellidos, Comentario, Direccion, Estatus, Foto, Nombres, Telefono);
                    this.InstructoresViewModel.agregarElemento(nuevo);
                    await dialogCoordinator.ShowMessageAsync(this, "Agregar Instructor", "¡El instructor a sido creado exitosamente",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    Instructor.Apellidos = this.Apellidos;
                    Instructor.Comentario = this.Comentario;
                    Instructor.Direccion = this.Direccion;
                    Instructor.Estatus = this.Estatus;
                    Instructor.Foto = this.Foto;
                    Instructor.Nombres = this.Nombres;
                    Instructor.Telefono = this.Telefono;
                    int posicion = this.InstructoresViewModel.instructores.IndexOf(this.InstructoresViewModel.Seleccionado);
                    this.InstructoresViewModel.instructores.RemoveAt(posicion);
                    this.InstructoresViewModel.instructores.Insert(posicion, Instructor);
                    await dialogCoordinator.ShowMessageAsync(this, "Actualizar Instructor", "¡El instructor a sido modificado exitosamente",
                    MessageDialogStyle.Affirmative);
                }
                ((Window)parametro).Close();
            }
        }
    }
}