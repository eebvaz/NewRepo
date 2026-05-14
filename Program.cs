


class Cliente
{
    private string estado; 
    private double valor;
    private string encargo;

    public Cliente(string estado, double valor, string encargo)
    {
        this.estado = estado;
        this.valor = valor;
        this.encargo = encargo;
    }

    public string Estado
    {
        get { return estado; }
        set { estado = value; }
    }
    public double Valor
    {
        get { return valor; }
        set { valor = value; }
    }
    public string Encargo
    {
        get { return encargo; }
        set { encargo = value; }
    }
}

//////////////////////////////////////////////////

class Persona : Cliente
{
    private string comprador;
    private string telefono;


    public Persona (string estado, double valor, string encargo,string comprador, string telefono): 
        base (estado, valor, encargo)
    {
        this.comprador = comprador;
        this.telefono = telefono;

    }

    public string Comprador
    {
        get { return comprador; }
        set { comprador = value; }
    }
    public string Telefono
    {
        get { return telefono; }
        set { telefono = value; }
    }


}

//////////////////////////////////////////////////
class Productos
{
    private int codigo;
    private string nombre;
    private DateTime fecha;
    private double precio;
    private int cantidad;
    private int vendidos;

    public Productos(int codigo, string nombre, DateTime fecha, double precio, int cantidad, int vendidos)
    {
        this.codigo = codigo;
        this.nombre = nombre;
        this.fecha = fecha;
        this.precio = precio;
        this.cantidad = cantidad;
        this.vendidos = vendidos;
    }

    public int Codigo
    {
        get { return codigo; }
      set { codigo = value; }

    }
    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
    public DateTime Fecha
    {
        get { return fecha; }
        set { fecha = value; }
    }
    public double Precio
    {
        get{ return precio; }
        set { precio = value; }
    }
    public int Cantidad
    {
        get{ return cantidad; }
        set { cantidad = value; }
    }
    public int Vendidos
    {
         get { return vendidos; }
        set { vendidos = value; }
    }


    public double Total()
    {
        return precio * cantidad;
    }

    public string EstadoProducto()
    {
        if(cantidad <= 0)
        {
            return "No disponible";
        }
        else if(fecha < DateTime.Now)
        {
            return "Vencido";
        }
        else if((fecha - DateTime.Now).Days <= 0)
        {
            return "Por vencer";
        }    
        else
        {
            return "Disponible";
        }
    }


    public string ObtenerDatos()
    {
        return "| Codigo: " + codigo + Environment.NewLine
            + "| Nombre: " + nombre + Environment.NewLine
            + "| Fecha: " + fecha.ToShortDateString() + Environment.NewLine
            + "| Precio: " + precio + Environment.NewLine
            + "| Cantidad: " + cantidad + Environment.NewLine
            + "| Vendidos: " + vendidos + Environment.NewLine
            + "| Estado: " + EstadoProducto() + Environment.NewLine;
    }

    public void GuardarArchivo(string ruta)
    {
        File.AppendAllText(ruta, ObtenerDatos()+Environment.NewLine);
    }

}


//////////////////////////////////////////////////



class Programa
{
    static void Main()
    { 

      
       Dictionary <int, Productos> products = new Dictionary<int, Productos> ();


        string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Registro.txt");

        int codigo, cantidad, vendidos;
        string nombre;
        DateTime fecha;
         double precio;
        bool validacion;
       int opcion;

        do {

            Console.WriteLine("");
            Console.WriteLine("Opcion 1: Vender Producto");
            Console.WriteLine("Opcion 2: Agregar Producto");
            Console.WriteLine("Opcion 3: Buscar Producto");
            Console.WriteLine("Opcion 4: Mostrar Inventario");
            Console.WriteLine("Opcion 5: Verificar Vencimiento");
            Console.WriteLine("Opcion 6: Gestionar Cliente");
            Console.WriteLine("Opcion 7: Cerrar");

            while (int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("");
            }

         switch(opcion)
            {
                    case 1:
                    Console.WriteLine("------------Vender Producto-----------");

                    int codigoBarra;
                    do
                    {
                        Console.WriteLine("Ingrese el codigo");
                        validacion = int.TryParse(Console.ReadLine(), out codigoBarra);
                        if(!validacion || codigoBarra <= 0)
                        {
                            Console.WriteLine("Codigo invalido");
                            validacion = false;
                        }

                    } while (!validacion);


                    if(products.ContainsKey(codigoBarra))
                    {
                        int vender;
                        do
                        {
                            Console.WriteLine("Ingrese el codigo");
                            validacion = int.TryParse(Console.ReadLine(), out vender);
                            if (!validacion || vender <= 0)
                            {
                                Console.WriteLine("Cantidad invalida");
                                validacion = false;
                            }
                        } while (!validacion);
                    }



                    ///////////////////////////////////////
                    break;
                    case 2:
                    Console.WriteLine("---------Agregar Producto-------");

                    do
                    {
                        Console.Write("Ingrese fecha nacimiento: ");
                        validacion = DateTime.TryParse(Console.ReadLine(), out fecha);

                        if (!validacion)
                        {
                            Console.WriteLine("Fecha invalida");
                        }
                    } while (!validacion);


                   

                    ///////////////////////////////////////
                    break;
                    case 3:
                    Console.WriteLine("");


                    ///////////////////////////////////////
                    break;
                    case 4:
                    Console.WriteLine("");


                    break;
                    case 5:
                    Console.WriteLine("");


                    break;
                    case 6:
                    Console.WriteLine("");


                    break;
                    default:
                    Console.WriteLine("");
                    break;

            }


        }while (opcion !=7);

    }
}