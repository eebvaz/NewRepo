


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

        int codigo, cantidad,opcion,vender,CodigoDeBarras, vendidos=0;
        string nombre;
        DateTime fecha;
        double precio;
        bool validacion;
        int opc;

        do {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Opcion 1: Vender Producto");
            Console.WriteLine("Opcion 2: Agregar Producto");
            Console.WriteLine("Opcion 3: Buscar Producto");
            Console.WriteLine("Opcion 4: Mostrar Inventario");
            Console.WriteLine("Opcion 5: Verificar Vencimiento");
            Console.WriteLine("Opcion 6: Gestionar Cliente");
            Console.WriteLine("Opcion 7: Cerrar");

            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Error");
            }

         switch(opcion)
            {
                    case 1:
                    Console.Clear();
                    Console.WriteLine("------------Vender Producto-----------");
                    double PrecioTotal = 0;
                    do
                    {

                        do
                        {
                            Console.Write("Ingrese el codigo: ");
                            validacion = int.TryParse(Console.ReadLine(), out CodigoDeBarras);
                            if (!validacion || CodigoDeBarras <= 0)
                            {
                                Console.WriteLine("Codigo invalido");
                                validacion = false;
                            }

                        } while (!validacion);


                        if (products.ContainsKey(CodigoDeBarras))
                        {

                            do
                            {
                                Console.Write("Ingrese la cantidad: ");
                                validacion = int.TryParse(Console.ReadLine(), out vender);
                                if (!validacion || vender <= 0)
                                {
                                    Console.WriteLine("Cantidad invalida");
                                    validacion = false;
                                }
                            } while (!validacion);

                            if (vender > products[CodigoDeBarras].Cantidad)
                            {
                                Console.WriteLine("No hay existencias");
                            }
                            else
                            {
                                products[CodigoDeBarras].Cantidad -= vender;
                                products[CodigoDeBarras].Vendidos += vender;

                                double general = vender * products[CodigoDeBarras].Precio;

                                PrecioTotal += general;

                                Console.WriteLine("Venta realizada");
                                Console.WriteLine("Total: Q" + general);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay existencias");
                        }


                        Console.WriteLine("Desea agregar otro producto");
                        Console.WriteLine("1: Si");
                        Console.WriteLine("2: No");
                        do
                        {
                            Console.Write("Opcion: ");
                            validacion = int.TryParse(Console.ReadLine(), out opc);
                            if (!validacion)
                            {
                                Console.WriteLine("opcion invalida");
                                validacion= false;
                            }


                        } while (!validacion);


                    } while (opc == 1);
                    Console.ReadKey();

                   


                    ///////////////////////////////////////
                    break;
                    case 2:
                    Console.Clear();

                    do
                    {
                        Console.WriteLine("---------Agregar Producto-------");

                        do
                        {
                            Console.Write("Ingrese el codigo de producto: ");
                            validacion = int.TryParse(Console.ReadLine(), out codigo);

                            if (!validacion)
                            {
                                Console.WriteLine("codigo invalido");
                            }
                        } while (!validacion);


                        if (products.ContainsKey(codigo))
                        {
                            Console.WriteLine("Producto ya existente");
                        }
                        else
                        {
                            //---------------------------------------------//

                            Console.Write("Ingrese el nombre: ");
                            nombre = Console.ReadLine();

                            nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1).ToLower();

                            //---------------------------------------------//
                            do
                            {
                                Console.Write("Ingrese fecha nacimiento: ");
                                validacion = DateTime.TryParse(Console.ReadLine(), out fecha);

                                if (!validacion)
                                {
                                    Console.WriteLine("Fecha invalida");
                                }
                            } while (!validacion);

                            //---------------------------------------------//
                            do
                            {
                                Console.Write("Ingrese el precio: ");
                                validacion = double.TryParse(Console.ReadLine(), out precio);

                                if (!validacion)
                                {
                                    Console.WriteLine("dato invalido");
                                }
                            } while (!validacion);

                            //---------------------------------------------//

                            do
                            {
                                Console.Write("Ingrese la cantidad: ");
                                validacion = int.TryParse(Console.ReadLine(), out cantidad);

                                if (!validacion)
                                {
                                    Console.WriteLine("codigo invalido");
                                }
                            } while (!validacion);

                            //---------------------------------------------//

                            do
                            {
                                Console.Write("Ingrese el codigo de producto: ");
                                validacion = int.TryParse(Console.ReadLine(), out codigo);

                                if (!validacion)
                                {
                                    Console.WriteLine("codigo invalido");
                                }
                            } while (!validacion);

                            Productos almacenar = new Productos(codigo, nombre, fecha, precio, cantidad, vendidos);

                            products.Add(codigo, almacenar);
                            almacenar.GuardarArchivo(ruta);

                            Console.WriteLine("Productos añadidos");
                        }

                        //--------------------------//
                        Console.WriteLine("Desea agregar otro producto");
                        Console.WriteLine("1: Si");
                        Console.WriteLine("2: No");
                        do
                        {
                            Console.Write("Opcion: ");
                            validacion = int.TryParse(Console.ReadLine(), out opc);
                            if (!validacion)
                            {
                                Console.WriteLine("opcion invalida");
                                validacion = false;
                            }


                        } while (!validacion);


                    } while (opc == 1);
                    Console.ReadKey();

                    ///////////////////////////////////////
                    break;
                    case 3:
                    Console.WriteLine("");


                    ///////////////////////////////////////
                    break;
                    case 4:
                    Console.WriteLine("Mostrar");

                    Console.WriteLine("Inventario");
                    if(products.Count == 0)
                    {
                        Console.WriteLine("No hay productos");
                    }
                    else
                    {
                        foreach(var item in products)
                        {
                            Console.WriteLine(item.Value.ObtenerDatos()+" Valor total: Q"+item.Value.Total());
                        }
                    }



                    break;
                    case 5:
                    Console.WriteLine("");


                    break;
                    case 6:
                    Console.WriteLine("");


                    break;
                    default:
                    Console.WriteLine("Saliendo...");
                    break;

            }


        }while (opcion !=7);

    }
}