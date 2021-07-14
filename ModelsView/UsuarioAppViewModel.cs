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
    public class UsuarioAppViewModel : INotifyPropertyChanged, ICommand
    {
        public UsuarioAppViewModel Instancia {get;set;}
        public UsuariosAppViewModel UsuariosAppViewModel {get;set;}
        public string Apellidos {get;set;}
        public string Nombres {get;set;}
        public string Email {get;set;}
        public string Username {get;set;}
        public string Password {get;set;}
        public string HeightLblPassword {get;set;} = "Auto";
        public string HeightTxtPassword {get;set;} = "Auto";
        public UsuariosApp Usuario {get;set;}
        private KalumDBContext dBContext = new KalumDBContext();
        public string Titulo {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;

        public UsuarioAppViewModel(UsuariosAppViewModel UsuariosAppViewModel, IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.dBContext = new KalumDBContext();
            this.UsuariosAppViewModel = UsuariosAppViewModel;
            if(this.UsuariosAppViewModel.Seleccionado != null)
            {
                this.Apellidos = this.UsuariosAppViewModel.Seleccionado.Apellidos;
                this.Nombres = this.UsuariosAppViewModel.Seleccionado.Nombres;
                this.Email = this.UsuariosAppViewModel.Seleccionado.Email;
                this.Username = this.UsuariosAppViewModel.Seleccionado.Username;
                this.HeightLblPassword = "0";
                this.HeightTxtPassword = "0";
                this.Titulo = "Actualizando Registro";
            }
            else if (this.UsuariosAppViewModel.Seleccionado == null)
            {
                this.HeightLblPassword = "Auto";
                this.HeightTxtPassword = "Auto";
                this.Titulo = "Nuevo Registro";
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
                    if(this.UsuariosAppViewModel.Seleccionado == null)
                    {
                        var UsernameParameter = new SqlParameter("@Username",this.Username);
                        var PaswordParameter = new SqlParameter("@Password",this.Password);
                        var NombresParameter = new SqlParameter("@Nombres",this.Nombres);
                        var ApellidosParameter = new SqlParameter("@Apellidos",this.Apellidos);
                        var EmailParameter = new SqlParameter("@Email",this.Email);
                        var Resultado = this.dBContext
                                            .UsuariosApp
                                            .FromSqlRaw("sp_agregar_usuario @Username, @Password, @Nombres, @Apellidos, @Email",
                                                        UsernameParameter, PaswordParameter, NombresParameter, ApellidosParameter,EmailParameter)
                                            .ToList();
                            foreach(Object registro in Resultado)
                            {
                                this.UsuariosAppViewModel.usuarios.Add((UsuariosApp)registro);
                            }
                        await this.dialogCoordinator.ShowMessageAsync(this,"Usuarios","El registro fue agregado exitosamente");
                    }
                    else if (this.UsuariosAppViewModel.Seleccionado != null)
                    {
                        int posicion = this.UsuariosAppViewModel.usuarios.IndexOf(this.UsuariosAppViewModel.Seleccionado);
                        UsuariosApp temporal = new UsuariosApp();
                        temporal.Id = this.UsuariosAppViewModel.Seleccionado.Id;
                        temporal.Password = this.Password;
                        temporal.Apellidos = this.Apellidos;
                        temporal.Nombres = this.Nombres;
                        temporal.Username = this.Username;
                        temporal.Email = this.Email;
                        this.dBContext.Entry(temporal).State = EntityState.Modified;
                        this.dBContext.SaveChanges();
                        this.UsuariosAppViewModel.usuarios.RemoveAt(posicion);
                        this.UsuariosAppViewModel.usuarios.Insert(posicion,temporal);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Usuarios","El registro fue modificado exitosamente");
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