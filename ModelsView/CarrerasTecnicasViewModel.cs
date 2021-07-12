using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using kalum2021.DataContext;

namespace kalum2021.ModelsView
{
    public class CarrerasTecnicasViewModel : INotifyPropertyChanged, ICommand
    {
        private ObservableCollection<CarrerasTecnicas> _Carreras;
        public ObservableCollection<CarrerasTecnicas> carreras
        { 
            get
            {
                if (this._Carreras == null)
                {
                    this._Carreras = new ObservableCollection<CarrerasTecnicas>(dBContex.CarrerasTecnicas.ToList());
                }
                return this._Carreras;
            }
            set
            {
                this._Carreras = value;
            }
        }
        public CarrerasTecnicasViewModel Instancia { get; set; }
        public CarrerasTecnicas Seleccionado { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        private IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContex = new KalumDBContext();

        public CarrerasTecnicasViewModel(IDialogCoordinator instance)
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
                    try
                        {
                            int posicion = this.carreras.IndexOf(this.Seleccionado);
                            this.dBContex.Remove(this.Seleccionado);
                            this.dBContex.SaveChanges();
                            this.carreras.RemoveAt(posicion);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Carreras","El registro fue eliminado correctamente");
                        }catch (Exception e)
                        {
                            await this.dialogCoordinator.ShowMessageAsync(this,"Error",e.Message);
                        }
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