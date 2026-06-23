using System.Windows.Input;
using GestionDevoluciones.Models;

namespace GestionDevoluciones.ViewModels;

//  Agregamos ": BaseViewModel" para heredar la capacidad de avisar a la pantalla
public class LoginViewModel : BaseViewModel
{
    // Propiedades que se conectarán a las cajas de texto
    public string UsernameInput { get; set; }
    public string PasswordInput { get; set; }
    
    // Cuando el valor cambia (set), lanza OnPropertyChanged() automáticamente.
    private string _mensajeError;
    public string MensajeError 
    { 
        get => _mensajeError; 
        set 
        { 
            _mensajeError = value; 
            OnPropertyChanged(); // El mensajero
        } 
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(RealizarLogin);
    }

    private async void RealizarLogin()
    {
        // Limpiamos el mensaje de error antes de intentar
        MensajeError = string.Empty;

        // Creamos nuestro usuario para validar
        var usuarioValido = new Usuario 
        { 
            Id = 1, 
            Username = "admin", 
            Password = "1234" 
        };

        // Validamos la lógica
        if (UsernameInput == usuarioValido.Username && PasswordInput == usuarioValido.Password)
        {
            // Login exitoso
            Application.Current.MainPage = new GestionDevoluciones.Views.MainPage();
        }
        else
        {
            // Al igualar esto, se activa el "set" de arriba y la pantalla se entera sola.
            MensajeError = "Usuario o contraseña incorrectos.";
        }
    }
}