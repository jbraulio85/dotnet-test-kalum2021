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
    public class InstructoresViewModel : INotifyPropertyChanged, ICommand
    {
        private ObservableCollection<Instructores> _Instructores;
        public ObservableCollection<Instructores> instructores
        { 
            get
            {
                if(this._Instructores == null)
                {
                    this._Instructores = new ObservableCollection<Instructores>(dBContext.Instructores.ToList());
                }
                return this._Instructores;
            }
            set
            {
                this._Instructores = value;
            }
        }
        public InstructoresViewModel Instancia{get;set;}
        public Instructores Seleccionado {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();

        public InstructoresViewModel (IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(Instructores nuevo)
        {
            this.instructores.Add(nuevo);
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
                InstructorView nuevoInstructor = new InstructorView(Instancia);
                nuevoInstructor.Show();
            }
            else if(parametro.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await dialogCoordinator.ShowMessageAsync(this,"Instructores","Debe de seleccionar un registro",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                    "Eliminar Instructor","¿Está seguro de eliminar este registro?",
                    MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            int posicion = this.instructores.IndexOf(this.Seleccionado);
                            this.dBContext.Remove(this.Seleccionado);
                            this.dBContext.SaveChanges();
                            this.instructores.RemoveAt(posicion);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Instructores","el registro fue eliminado exitosamente",
                            MessageDialogStyle.Affirmative);
                        }catch (Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                        }
                    }
                }
            }
            else if(parametro.Equals("Modificar"))
            {
                if(this.Seleccionado == null)
                {
                    await dialogCoordinator.ShowMessageAsync(this,"Instructores","Debe de seleccionar un registro",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    InstructorView modificarInstructor = new InstructorView(Instancia);
                    modificarInstructor.ShowDialog();
                }
            }
        }
    }
}