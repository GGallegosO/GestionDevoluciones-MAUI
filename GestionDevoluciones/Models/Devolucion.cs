namespace GestionDevoluciones.Models;


// Usar un enum evita que alguien escriba "Pendient" o "Concluido" por error
public enum EstadoDevolucion
{
    Pendiente,
    Concluida
}


public class Devolucion
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; } = string.Empty;
    
    public string MarcaProducto { get; set; } = string.Empty;
    public string ModeloProducto { get; set; } = string.Empty;
    
    public string NombreCliente { get; set; } = string.Empty;
    public string Contacto { get; set; } = string.Empty; 
    public DateTime Fecha { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public EstadoDevolucion Estado { get; set; }
}