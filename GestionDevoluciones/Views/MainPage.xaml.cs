namespace GestionDevoluciones.Views;

public partial class MainPage : TabbedPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Para que el compilador XAML pueda enlazar el evento correctamente
    public void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        if (Application.Current != null)
        {
            Application.Current.MainPage = new AppShell();
        }
    }
}