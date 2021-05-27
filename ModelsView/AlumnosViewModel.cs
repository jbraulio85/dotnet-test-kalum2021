using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.DataContext;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;

namespace kalum2021.ModelsView
{
    public class AlumnosViewModel : INotifyPropertyChanged, ICommand
    {
        private ObservableCollection<Alumnos> _Alumnos;
        public ObservableCollection<Alumnos> alumnos
        {
            get
            {
                if (this._Alumnos == null)
                {
                    this._Alumnos = new ObservableCollection<Alumnos>(dBContext.Alumnos.ToList());
                }
                return this._Alumnos;
            }
            set
            {
                this._Alumnos = value;
            }
        }
        public AlumnosViewModel Instancia { get; set; }
        public Alumnos Seleccionado { get; set; }
        private IDialogCoordinator dialogCoordinator;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public KalumDBContext dBContext = new KalumDBContext();

        public AlumnosViewModel(IDialogCoordinator instance)
        {
            this.dialogCoordinator = instance;
            this.Instancia = this;

        }

        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
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
            if (parametro.Equals("Nuevo"))
            {
                this.Seleccionado = null;
                AlumnoView nuevoAlumno = new AlumnoView(Instancia);
                nuevoAlumno.Show();
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Alumnos", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar alumno", "¿Está seguro de eliminar el registro?", MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                        int posicion = this.alumnos.IndexOf(this.Seleccionado);
                        this.dBContext.Remove(this.Seleccionado);
                        this.dBContext.SaveChanges();
                        this.alumnos.RemoveAt(posicion);
                        await this.dialogCoordinator.ShowMessageAsync(this,"Alumnos","Registro Eliminado");
                        }catch(Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                        }
                        
                    }
                }
            }
            else if (parametro.Equals("Modificar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Alumnos", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    AlumnoView modificarAlumno = new AlumnoView(this.Instancia);
                    modificarAlumno.ShowDialog();
                }
            }
        }
    }
}