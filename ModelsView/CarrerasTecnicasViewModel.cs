using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class CarrerasTecnicasViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<CarrerasTecnicas> carreras { get; set; }
        public CarrerasTecnicasViewModel Instancia { get; set; }
        public CarrerasTecnicas Seleccionado { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;

        public CarrerasTecnicasViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.carreras = new ObservableCollection<CarrerasTecnicas>();
            this.carreras.Add(new CarrerasTecnicas("9A515FB3-3686-4638-8E2B-2D754DEBD789","Desarrollo Full-stack .NET"));
            this.carreras.Add(new CarrerasTecnicas("C7390B0D-86DE-417E-8220-2390FCEF74F3","Desarrollo Fullstack JAVA"));
        }

        public void NotificarCambio(string propiedad)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(CarrerasTecnicas nuevo)
        {
            this.carreras.Add(nuevo);
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
                CarreraTecnicaView nuevaCarrera = new CarreraTecnicaView(Instancia);
                nuevaCarrera.Show();
            }
            else if(parametro.Equals("Eliminar"))
            {
                if(this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Carreras","Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,"Eliminar Carrera",
                    "¿Está seguro de eliminar el registro?",MessageDialogStyle.AffirmativeAndNegative);
                    if(respuesta == MessageDialogResult.Affirmative)
                    {
                        this.carreras.Remove(Seleccionado);
                    }
                }
            }
            else if(parametro.Equals("Modificar"))
            {
                if(this.Seleccionado ==  null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Carreras","Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    CarreraTecnicaView modificarCarrera = new CarreraTecnicaView(Instancia);
                    modificarCarrera.ShowDialog();
                }
            }
        }
    }
}