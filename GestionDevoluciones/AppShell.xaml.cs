
using GestionDevoluciones.Views;

namespace GestionDevoluciones;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Registramos la ruta para que la app sepa a dónde ir tras el login
        Routing.RegisterRoute("MainPage", typeof(MainPage));
    }
}