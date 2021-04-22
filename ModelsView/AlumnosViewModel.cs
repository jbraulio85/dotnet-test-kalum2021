using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class AlumnosViewModel : INotifyPropertyChanged, ICommand 
    {
        public ObservableCollection<Alumnos> alumnos {get;set;}
        public AlumnosViewModel Instancia {get;set;}
        public Alumnos Seleccionado {get;set;}
        private IDialogCoordinator dialogCoordinator;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public AlumnosViewModel (IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;
            this.alumnos = new ObservableCollection<Alumnos>();
            this.alumnos.Add(new Alumnos("2021001","E2021-001","José Braulio","Echeverria Montufar","becheverria@kinal.edu.gt"));
            this.alumnos.Add(new Alumnos("2021002","E2021-002","Francisco Eduardo","Xol Fuentes","fxol@kinal.edu.gt"));
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }
        public void agregarElemento(Alumnos nuevo)
        {
            this.alumnos.Add(nuevo);
        }
        public bool CanExecute(object parametro)
        {
            return true;
        }

        public async void Execute(object parametro)
        {
            if(parametro.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                AlumnoView nuevoAlumno = new AlumnoView(Instancia);
                nuevoAlumno.Show();
            }
            else if(parametro.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar alumno","¿Está seguro de eliminar el registro?", MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.alumnos.Remove(Seleccionado);
                    }
                }
            }
            else if (parametro.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    AlumnoView modificarAlumno = new AlumnoView(Instancia);
                    modificarAlumno.ShowDialog();
                }
            }
        }
    }
}