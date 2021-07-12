using System;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace kalum2021.ModelsView
{
    public class AlumnoViewModel : INotifyPropertyChanged, ICommand
    {
        public AlumnoViewModel Instancia { get; set; }
        public AlumnosViewModel AlumnosviewModel { get; set; }
        public string Carne { get; set; }
        public string NoExpediente { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public Alumnos Alumno { get; set; }
        private KalumDBContext dBContext;
        public String Titulo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public AlumnoViewModel(AlumnosViewModel AlumnosViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.dBContext = new KalumDBContext();
            this.AlumnosviewModel = AlumnosViewModel;
            if (this.AlumnosviewModel.Seleccionado != null)
            {
                this.NoExpediente = this.AlumnosviewModel.Seleccionado.NoExpediente;
                this.Apellidos = this.AlumnosviewModel.Seleccionado.Apellidos;
                this.Nombres = this.AlumnosviewModel.Seleccionado.Nombres;
                this.Email = this.AlumnosviewModel.Seleccionado.Email;
                this.Titulo = "Actualizando registro";
            }
            else if (this.AlumnosviewModel.Seleccionado == null)
            {
                this.Titulo = "Nuevo Registro";
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
                    if (this.AlumnosviewModel.Seleccionado == null)
                    {
                        var ApellidosParameter = new SqlParameter("@Apellidos", this.Apellidos);
                        var NombresParameter = new SqlParameter("@Nombres", this.Nombres);
                        var EmailParameter = new SqlParameter("@Email", this.Email);
                        var Resultado = this.dBContext
                                            .Alumnos
                                            .FromSqlRaw("sp_registrar_alumno @Apellidos, @Nombres, @Email",
                                                    ApellidosParameter, NombresParameter, EmailParameter)
                                            .ToList();
                            foreach(Object registro in Resultado)
                            {
                                this.AlumnosviewModel.alumnos.Add((Alumnos)registro);
                            }
                            await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Registro Almacenado exitosamente");
                    }
                    else if (this.AlumnosviewModel.Seleccionado != null)
                    {
                        int posicion = this.AlumnosviewModel.alumnos.IndexOf(this.AlumnosviewModel.Seleccionado);
                        Alumnos temporal = new Alumnos();
                        temporal.Carne = this.AlumnosviewModel.Seleccionado.Carne;
                        temporal.NoExpediente = this.NoExpediente;
                        temporal.Apellidos = this.Apellidos;
                        temporal.Nombres = this.Nombres;
                        temporal.Email = this.Email;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.AlumnosviewModel.alumnos.RemoveAt(posicion);
                        this.AlumnosviewModel.alumnos.Insert(posicion, temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this, "Alumnos", "¡Los datos han sido almacenados exitosamente!");
                    }
                }
                catch (Exception e)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Error", e.Message);
                }
                ((Window)parametro).Close();
            }
        }
    }
}