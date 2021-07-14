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
    public class SalonesViewModel : INotifyPropertyChanged, ICommand
    {
        private ObservableCollection<Salones> _Salones;
        public ObservableCollection<Salones> salones
        { 
            get
            {
                if(this._Salones == null)
                {
                    this._Salones = new ObservableCollection<Salones>(dBContext.Salones.ToList());
                }
                return this._Salones;
            }
            set
            {
                this._Salones = value;
            }
        }
        public SalonesViewModel Instancia { get; set; }
        public Salones Seleccionado { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;
        private KalumDBContext dBContext = new KalumDBContext();

        public SalonesViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
        }
        public void NotificarCambio(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
            }
        }

        public void agregarElemento(Salones nuevo)
        {
            this.salones.Add(nuevo);
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
                SalonView nuevoSalon = new SalonView(Instancia);
                nuevoSalon.Show();
            }
            else if (parametro.Equals("Eliminar"))
            {
                if (this.Seleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this, "Salones", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this, "Eliminar salón",
                    "¿Está seguro de eliminar el registro", MessageDialogStyle.AffirmativeAndNegative);
                    if (respuesta == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            int posicion = this.salones.IndexOf(this.Seleccionado);
                            this.dBContext.Remove(this.Seleccionado);
                            this.dBContext.SaveChanges();
                            this.salones.RemoveAt(posicion);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Salones","el registro fue eliminado exitosamente",
                            MessageDialogStyle.Affirmative);
                        }catch (Exception e)
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
                    await this.dialogCoordinator.ShowMessageAsync(this, "Salones", "Debe de seleccionar un elemento",
                    MessageDialogStyle.Affirmative);
                }
                else
                {
                    SalonView modificarSalon = new SalonView(Instancia);
                    modificarSalon.ShowDialog();
                }
            }
        }
    }
}