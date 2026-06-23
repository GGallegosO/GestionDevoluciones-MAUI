
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionDevoluciones.ViewModels;

// INotifyPropertyChanged es la interfaz nativa que actúa como "mensajero"
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // Este es el método que usaremos para avisarle a la pantalla que algo cambió
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}