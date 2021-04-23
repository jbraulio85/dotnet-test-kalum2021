using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2021.Models;
using kalum2021.Views;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2021.ModelsView
{
    public class SalonesViewModel : INotifyPropertyChanged, ICommand
    {
        public ObservableCollection<Salones> salones { get; set; }
        public SalonesViewModel Instancia { get; set; }
        public Salones Seleccionado { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
        public IDialogCoordinator dialogCoordinator;

        public SalonesViewModel(IDialogCoordinator instance)
        {
            this.Instancia = this;
            this.dialogCoordinator = instance;
            this.salones = new ObservableCollection<Salones>();
            this.salones.Add(new Salones("C23", 20, "Salon de informática", "Salón C-23"));
            this.salones.Add(new Salones("C28", 20, "Salon de informática", "Salón C-28"));
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
                        this.salones.Remove(Seleccionado);
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