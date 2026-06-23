using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Globalization; 
using GestionDevoluciones.Models;

namespace GestionDevoluciones.ViewModels;

public class DevolucionesViewModel : BaseViewModel
{
    public ObservableCollection<Devolucion> DevolucionesPendientes { get; set; }
    public ObservableCollection<Devolucion> DevolucionesConcluidas { get; set; }
    public ObservableCollection<Producto> Inventario { get; set; }

    private List<Devolucion> _respaldoPendientes;
    private List<Devolucion> _respaldoConcluidas;
    private List<Producto> _respaldoInventario;

    private string _textoBusqueda = string.Empty;
    public string TextoBusqueda
    {
        get => _textoBusqueda;
        set { if (_textoBusqueda != value) { _textoBusqueda = value; OnPropertyChanged(); FiltrarPendientes(); } }
    }

    private string _textoBusquedaConcluidas = string.Empty;
    public string TextoBusquedaConcluidas
    {
        get => _textoBusquedaConcluidas;
        set { if (_textoBusquedaConcluidas != value) { _textoBusquedaConcluidas = value; OnPropertyChanged(); FiltrarConcluidas(); } }
    }

    private string _textoBusquedaInventario = string.Empty;
    public string TextoBusquedaInventario
    {
        get => _textoBusquedaInventario;
        set { if (_textoBusquedaInventario != value) { _textoBusquedaInventario = value; OnPropertyChanged(); FiltrarInventario(); } }
    }

    public ICommand AbrirDetalleCommand { get; }
    public ICommand NuevaDevolucionCommand { get; }

    public DevolucionesViewModel()
    {
        DevolucionesPendientes = new ObservableCollection<Devolucion>();
        DevolucionesConcluidas = new ObservableCollection<Devolucion>();
        Inventario = new ObservableCollection<Producto>();
        
        _respaldoPendientes = new List<Devolucion>();
        _respaldoConcluidas = new List<Devolucion>();
        _respaldoInventario = new List<Producto>();

        AbrirDetalleCommand = new Command<Devolucion>(AbrirDetalleModal);
        NuevaDevolucionCommand = new Command(RegistrarDevolucion);

        CargarDatosDePrueba();
    }

    // --- FUNCIÓN PARA FORMATEAR TEXTO (Ej: "memoria ram" -> "Memoria Ram") ---
    private string FormatearTitulo(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(texto.ToLower().Trim());
    }

    private void FiltrarPendientes()
    {
        DevolucionesPendientes.Clear();
        var filtrados = string.IsNullOrWhiteSpace(TextoBusqueda) 
            ? _respaldoPendientes 
            : _respaldoPendientes.Where(d => d.NombreCliente.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase)).ToList();
        foreach (var item in filtrados) DevolucionesPendientes.Add(item);
    }

    private void FiltrarConcluidas()
    {
        DevolucionesConcluidas.Clear();
        var filtrados = string.IsNullOrWhiteSpace(TextoBusquedaConcluidas) 
            ? _respaldoConcluidas 
            : _respaldoConcluidas.Where(d => d.NombreCliente.Contains(TextoBusquedaConcluidas, StringComparison.OrdinalIgnoreCase)).ToList();
        foreach (var item in filtrados) DevolucionesConcluidas.Add(item);
    }

    private void FiltrarInventario()
    {
        Inventario.Clear();
        var filtrados = string.IsNullOrWhiteSpace(TextoBusquedaInventario) 
            ? _respaldoInventario 
            : _respaldoInventario.Where(p => p.Nombre.Contains(TextoBusquedaInventario, StringComparison.OrdinalIgnoreCase) || 
                                             p.Marca.Contains(TextoBusquedaInventario, StringComparison.OrdinalIgnoreCase)).ToList();
        foreach (var item in filtrados) Inventario.Add(item);
    }

    private void CargarDatosDePrueba()
    {
        var p1 = new Producto { Id = 101, Nombre = "Placa Madre", Marca = "ASUS", Modelo = "X570", Stock = 10 };
        var p2 = new Producto { Id = 105, Nombre = "Fuente de Poder", Marca = "EVGA", Modelo = "700W", Stock = 5 };
        var p3 = new Producto { Id = 202, Nombre = "Memoria RAM", Marca = "Corsair", Modelo = "16GB", Stock = 20 };
        Inventario.Add(p1); _respaldoInventario.Add(p1);
        Inventario.Add(p2); _respaldoInventario.Add(p2);
        Inventario.Add(p3); _respaldoInventario.Add(p3);

        var d1 = new Devolucion { Id = 1, ProductoId = 101, NombreProducto = "Placa Madre", MarcaProducto = "ASUS", ModeloProducto = "X570", NombreCliente = "Carlos Rodríguez", Contacto = "987654321", Fecha = DateTime.Now.AddDays(-1), Motivo = "Pines doblados", Estado = EstadoDevolucion.Pendiente };
        var d2 = new Devolucion { Id = 2, ProductoId = 105, NombreProducto = "Fuente de Poder", MarcaProducto = "EVGA", ModeloProducto = "700W", NombreCliente = "Ana Torres", Contacto = "ana@correo.cl", Fecha = DateTime.Now, Motivo = "No enciende, huele a quemado", Estado = EstadoDevolucion.Pendiente };
        DevolucionesPendientes.Add(d1); _respaldoPendientes.Add(d1);
        DevolucionesPendientes.Add(d2); _respaldoPendientes.Add(d2);

        var d3 = new Devolucion { Id = 3, ProductoId = 202, NombreProducto = "Memoria Ram", MarcaProducto = "Corsair", ModeloProducto = "16GB", NombreCliente = "Luis Silva", Contacto = "luis@correo.cl", Fecha = DateTime.Now.AddDays(-4), Motivo = "Incompatibilidad", Estado = EstadoDevolucion.Concluida };
        DevolucionesConcluidas.Add(d3); _respaldoConcluidas.Add(d3);
    }

    private async void AbrirDetalleModal(Devolucion devolucion)
    {
        if (devolucion != null && Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new Views.DetalleDevolucionPage(this, devolucion));
        }
    }

    public void ProcesarDevolucionConfirmada(Devolucion devolucion)
    {
        if (devolucion != null && devolucion.Estado == EstadoDevolucion.Pendiente)
        {
            devolucion.Estado = EstadoDevolucion.Concluida;
            DevolucionesPendientes.Remove(devolucion);
            _respaldoPendientes.Remove(devolucion);
            
            DevolucionesConcluidas.Add(devolucion);
            _respaldoConcluidas.Add(devolucion);
            FiltrarConcluidas();

            var producto = _respaldoInventario.FirstOrDefault(p => p.Id == devolucion.ProductoId);
            if (producto != null)
            {
                // Reemplazamos la tarjeta completa en el respaldo para forzar a la pantalla a redibujar el nuevo número
                int index = _respaldoInventario.IndexOf(producto);
                _respaldoInventario[index] = new Producto 
                { 
                    Id = producto.Id, 
                    Nombre = producto.Nombre, 
                    Marca = producto.Marca, 
                    Modelo = producto.Modelo, 
                    Stock = producto.Stock + 1 
                };
                FiltrarInventario(); 
            }
        }
    }

    private async void RegistrarDevolucion()
    {
        if (Application.Current?.MainPage == null) return;

        string nombreCliente = await Application.Current.MainPage.DisplayPromptAsync("Nuevo Registro", "Nombre del Cliente:");
        if (string.IsNullOrWhiteSpace(nombreCliente)) return; 
        nombreCliente = FormatearTitulo(nombreCliente); // Aplicamos mayúsculas

        string contacto = await Application.Current.MainPage.DisplayPromptAsync("Nuevo Registro", "Contacto (Teléfono o Email):");
        if (string.IsNullOrWhiteSpace(contacto)) contacto = "Sin contacto";

        string nombreProducto = await Application.Current.MainPage.DisplayPromptAsync("Nuevo Registro", "Nombre del producto devuelto:");
        if (string.IsNullOrWhiteSpace(nombreProducto)) return;
        nombreProducto = FormatearTitulo(nombreProducto); // Aplicamos mayúsculas

        string marcaAsignada = await Application.Current.MainPage.DisplayPromptAsync("Detalle del Producto", $"Ingrese la MARCA de '{nombreProducto}':");
        if (string.IsNullOrWhiteSpace(marcaAsignada)) marcaAsignada = "Genérica";
        marcaAsignada = FormatearTitulo(marcaAsignada); // Aplicamos mayúsculas

        string modeloAsignado = await Application.Current.MainPage.DisplayPromptAsync("Detalle del Producto", $"Ingrese el MODELO de '{nombreProducto}':");
        if (string.IsNullOrWhiteSpace(modeloAsignado)) modeloAsignado = "Genérico";
        modeloAsignado = FormatearTitulo(modeloAsignado); // Aplicamos mayúsculas

        string motivo = await Application.Current.MainPage.DisplayPromptAsync("Nuevo Registro", "Motivo de la devolución:");
        if (string.IsNullOrWhiteSpace(motivo)) motivo = "Sin especificar";

        var productoExistente = _respaldoInventario.FirstOrDefault(p => 
            p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase) &&
            p.Marca.Equals(marcaAsignada, StringComparison.OrdinalIgnoreCase) &&
            p.Modelo.Equals(modeloAsignado, StringComparison.OrdinalIgnoreCase));
        
        int productoIdAsignado;

        if (productoExistente != null)
        {
            productoIdAsignado = productoExistente.Id;
        }
        else
        {
            productoIdAsignado = _respaldoInventario.Count > 0 ? _respaldoInventario.Max(p => p.Id) + 1 : 100;
            var nuevoProducto = new Producto { Id = productoIdAsignado, Nombre = nombreProducto, Marca = marcaAsignada, Modelo = modeloAsignado, Stock = 0 };
            _respaldoInventario.Add(nuevoProducto);
            FiltrarInventario(); 
        }

        var nuevaDevolucion = new Devolucion
        {
            Id = _respaldoPendientes.Count + _respaldoConcluidas.Count + 1,
            ProductoId = productoIdAsignado,
            NombreProducto = nombreProducto, 
            MarcaProducto = marcaAsignada,     
            ModeloProducto = modeloAsignado,   
            NombreCliente = nombreCliente,
            Contacto = contacto,
            Fecha = DateTime.Now,
            Motivo = motivo,
            Estado = EstadoDevolucion.Pendiente
        };

        _respaldoPendientes.Add(nuevaDevolucion);
        FiltrarPendientes(); 

        await Application.Current.MainPage.DisplayAlert("Éxito", "Devolución y reembolso registrados correctamente.", "OK");
    }
}