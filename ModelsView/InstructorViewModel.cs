using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

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
        public string Email {get;set;}
        public Instructores Instructor { get; set; }
        public string Titulo;
        private KalumDBContext dBContext;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        public InstructorViewModel(InstructoresViewModel InstructoresViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.InstructoresViewModel = InstructoresViewModel;
            this.dBContext = new KalumDBContext();
            if (this.InstructoresViewModel.Seleccionado != null)
            {
                this.Apellidos = this.InstructoresViewModel.Seleccionado.Apellidos;
                this.Email = this.InstructoresViewModel.Seleccionado.Email;
                this.Comentario = this.InstructoresViewModel.Seleccionado.Comentario;
                this.Direccion = this.InstructoresViewModel.Seleccionado.Direccion;
                this.Estatus = this.InstructoresViewModel.Seleccionado.Estatus;
                this.Foto = this.InstructoresViewModel.Seleccionado.Foto;
                this.Nombres = this.InstructoresViewModel.Seleccionado.Nombres;
                this.Telefono = this.InstructoresViewModel.Seleccionado.Telefono;
                this.Titulo = "Actualizando registro";
            }
            else if (this.InstructoresViewModel.Seleccionado == null)
            {
                this.Titulo ="Nuevo registro";
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
                    if(this.InstructoresViewModel.Seleccionado == null)
                    {
                        Instructores nuevoInstructor = new Instructores();
                        nuevoInstructor.InstructorId = Guid.NewGuid().ToString();
                        nuevoInstructor.Apellidos = Apellidos;
                        nuevoInstructor.Email = Email;
                        nuevoInstructor.Comentario = Comentario;
                        nuevoInstructor.Direccion = Direccion;
                        nuevoInstructor.Estatus = Estatus;
                        nuevoInstructor.Foto = Foto;
                        nuevoInstructor.Nombres = Nombres;
                        nuevoInstructor.Telefono = Telefono;
                        this.dBContext.Instructores.Add(nuevoInstructor);
                        this.dBContext.SaveChanges();
                        this.InstructoresViewModel.instructores.Add(nuevoInstructor);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Instructores","El registro fue agreado exitosamente",
                        MessageDialogStyle.Affirmative);
                    }else
                    {
                        int posicion = this.InstructoresViewModel.instructores.IndexOf(this.InstructoresViewModel.Seleccionado);
                        Instructores temporal = new Instructores();
                        temporal.InstructorId = this.InstructoresViewModel.Seleccionado.InstructorId;
                        temporal.Apellidos = this.Apellidos;
                        temporal.Email = this.Email;
                        temporal.Comentario = this.Comentario;
                        temporal.Direccion = this.Direccion;
                        temporal.Estatus = this.Estatus;
                        temporal.Foto = this.Foto;
                        temporal.Nombres = this.Nombres;
                        temporal.Telefono = this.Telefono;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.InstructoresViewModel.instructores.RemoveAt(posicion);
                        this.InstructoresViewModel.instructores.Insert(posicion,temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Instructores","El registro fue modificado exitosamente",
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