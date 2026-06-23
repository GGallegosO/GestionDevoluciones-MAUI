using GestionDevoluciones.Models;
using GestionDevoluciones.ViewModels;

namespace GestionDevoluciones.Views;

public partial class DetalleDevolucionPage : ContentPage
{
    private DevolucionesViewModel _viewModel;
    private Devolucion _devolucion;

    public DetalleDevolucionPage(DevolucionesViewModel viewModel, Devolucion devolucion)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _devolucion = devolucion;
        
        // Conectamos esta ventana a los datos exactos del registro que tocaste
        BindingContext = _devolucion;
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(); // Cierra la ventana sin hacer nada
    }

    private async void OnConfirmarClicked(object sender, EventArgs e)
    {
        // Cierra la ventana emergente
        await Navigation.PopModalAsync();
        
        // Le dice al cerebro que ejecute la lógica de mover y ajustar stock
        _viewModel.ProcesarDevolucionConfirmada(_devolucion);
        
        await App.Current.MainPage.DisplayAlert("Éxito", "Devolución procesada e inventario actualizado.", "OK");
    }
}