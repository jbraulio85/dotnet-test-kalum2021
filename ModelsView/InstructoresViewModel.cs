using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class InstructoresViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Instructores> instructores { get; set; }
        public InstructoresViewModel Instancia{get;set;}
        public Instructores Seleccionado {get;set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public InstructoresViewModel (IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.instructores = new ObservableCollection<Instructores>();
            this.instructores.Add(new Instructores("06CE8CE6-6D4D-4BF0-B003-7ABA5D56A296","Munguía","VMware y AWS","Ciudad",
            "Activo","Default.png","Héctor Leonel","22160000"));
            this.instructores.Add(new Instructores("06CE8CE6-6D4D-4BF0-B003-7ABA5D56A296","Tumax Chaclan","Instrucdtor de desarrollo de software","Ciudad",
            "ALTA","edwintumax.png","Edwin Rolando","33124569"));
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
                        this.instructores.Remove(Seleccionado);
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