using System;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Views;

namespace kalum2021.ModelsView
{
    public class MainViewModel : INotifyPropertyChanged, ICommand
    {
        public string Fondo {get;set;} =$"{Environment.CurrentDirectory}\\Images\\Landscape.gif";
        public MainViewModel Instancia {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public MainViewModel()
        {
            this.Instancia = this;
        }
        public bool CanExecute(object parametro)
        {
            return true;
        }

        public void Execute(object parametro)
        {
            if(parametro.Equals("Usuarios"))
            {
                UsuariosAppView ventanaUsuarios = new UsuariosAppView();
                ventanaUsuarios.ShowDialog();
            }
            else if (parametro.Equals("Roles"))
            {
                RolesView ventanaRoles = new RolesView();
                ventanaRoles.ShowDialog();
            }
            else if(parametro.Equals("Alumnos"))
            {
                AlumnosView ventanaAlumnos = new AlumnosView();
                ventanaAlumnos.ShowDialog();
            }
            else if (parametro.Equals("Instructores"))
            {
                InstructoresView ventanaInstructores = new InstructoresView();
                ventanaInstructores.ShowDialog();
            }
            else if (parametro.Equals("Salones"))
            {
                SalonesView ventanaSalones = new SalonesView();
                ventanaSalones.ShowDialog();
            }
            else if (parametro.Equals("Carreras"))
            {
                CarrerasTecnicasView ventanaCarreras = new CarrerasTecnicasView();
                ventanaCarreras.ShowDialog();
            }
            else if (parametro.Equals("Clases"))
            {
                ClasesView ventanaClases = new ClasesView();
                ventanaClases.ShowDialog();
            }
            else if (parametro.Equals("Modulos"))
            {
                ModulosView ventanaModulos = new ModulosView();
                ventanaModulos.ShowDialog();
            }
            else if(parametro.Equals("Seminarios"))
            {
                SeminariosView ventanaSeminarios = new SeminariosView();
                ventanaSeminarios.ShowDialog();
            }
        }
    }
}