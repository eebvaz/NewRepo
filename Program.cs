


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

  

    public string MostrarDatos()
    {
        return "| Codigo: " + Codigo + 
             " | Nombre: " + Nombre +
             " | Fecha: " + Fecha.ToShortDateString() + 
             " | Precio: " + Precio + 
             " | Cantidad: " + Cantidad + 
             " | Vendidos: " + Vendidos +
             " | Estado: " + EstadoProducto();
    }

    public string ObtenerDatos()
    {
        return codigo + " | " +
               nombre + " | " +
               fecha.ToShortDateString() + " | " +
               precio + " | " +
               cantidad + " | " +
               vendidos;
    }

}


//////////////////////////////////////////////////



class Programa
{
    //---------------------------//
    static void GuardarInventario(Dictionary<int, Productos> products,string ruta)
    {
        StreamWriter escribir = new StreamWriter(ruta);

        foreach (var item in products)
        {
            escribir.WriteLine(item.Value.ObtenerDatos());
        }

        escribir.Close();
    }
    //---------------------------//

    static void CargarInventario(Dictionary<int, Productos> products,string ruta)
    {
       
        if (File.Exists(ruta))
        {
      
            string[] lineas = File.ReadAllLines(ruta);

           
            foreach (string linea in lineas)
            {
           
                string[] datos = linea.Split('|');

               
                int codigo = int.Parse(datos[0].Trim());
                string nombre = datos[1].Trim();
                DateTime fecha = DateTime.Parse(datos[2].Trim());
                double precio = double.Parse(datos[3].Trim());
                int cantidad = int.Parse(datos[4].Trim());
                int vendidos = int.Parse(datos[5].Trim());

                Productos producto = new Productos(codigo,nombre,fecha, precio,cantidad,vendidos);

                products[codigo] = producto;
            }
        }
    }

    ////////////////////////////////////////////////

    static void Main()
    { 

      
       Dictionary <int, Productos> producto = new Dictionary<int, Productos> ();

        string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Registro.txt");
        CargarInventario(producto, ruta);

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

            switch (opcion)
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


                        if (producto.ContainsKey(CodigoDeBarras))
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

                            if (vender > producto[CodigoDeBarras].Cantidad)
                            {
                                Console.WriteLine("No hay existencias");
                            }
                            else
                            {
                                producto[CodigoDeBarras].Cantidad -= vender;
                                producto[CodigoDeBarras].Vendidos += vender;

                                double general = vender * producto[CodigoDeBarras].Precio;

                                PrecioTotal += general;

                                GuardarInventario(producto, ruta);
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
                                validacion = false;
                            }


                        } while (!validacion);


                    } while (opc == 1);
                    Console.ReadKey();




                    ///////////////////////////////////////
                    break;
                case 2:
                    Console.Clear();

                    //crear un objeto para validacion en el set//

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


                        if (producto.ContainsKey(codigo))
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
                                Console.Write("Ingrese fecha vencimiento: ");
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



                            Productos almacenar = new Productos(codigo, nombre, fecha, precio, cantidad, vendidos);

                            producto.Add(codigo, almacenar);
                            GuardarInventario(producto, ruta);


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
                    Console.Clear();
                    Console.WriteLine("");
                    Console.Write("Ingrese el codigo del producto: ");
                    while(!int.TryParse(Console.ReadLine(),out CodigoDeBarras))
                    {
                        Console.WriteLine("Error de dato");
                    }

                    if(producto.ContainsKey(CodigoDeBarras))
                    {
                        Console.WriteLine(producto[CodigoDeBarras]);


                        do
                        {
                            Console.WriteLine("¿Que desea realizar?");
                            Console.WriteLine("Opcion 1: Borrar");
                            Console.WriteLine("Opcion 2: Editar");
                            Console.WriteLine("Opcion 3: Regresar al menu");
                            while (!int.TryParse(Console.ReadLine(), out opcion))
                            {
                                Console.WriteLine("Error de dato");
                            }

                            switch (opcion)
                            {
                                case 1:
                                    Console.WriteLine("");
                                    producto.Remove(CodigoDeBarras);
                                    Console.WriteLine("Se ha eliminado");
                                    GuardarInventario(producto, ruta);

                                    break;
                                case 2:

                                    

                                    break;
                                default:
                                    break;
                            }

                        }while(opcion != 3);

                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrados");
                    }
                    Console.ReadKey();



                    ///////////////////////////////////////
                    break;
                    case 4:
                    Console.Clear();
                    Console.WriteLine("Mostrar");

                    Console.WriteLine("Inventario");
                    if(producto.Count == 0)
                    {
                        Console.WriteLine("No hay productos");
                    }
                    else

                    {
                        Productos popular = null;

                        foreach(var item in producto)
                        {
                            Console.WriteLine(item.Value.MostrarDatos()+"| Valor total: Q"+item.Value.Total() + " |");
                           
                            if (popular == null || item.Value.Vendidos > popular.Vendidos)
                            {
                                popular = item.Value;
                            }
                        }

                    

                        if(popular != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Producto mas vendido");
                            Console.WriteLine("Articulo "+ popular.Nombre);
                            Console.WriteLine("Cantidad mas vendidad: "+ popular.Vendidos);
                        }

                    }


                    Console.ReadKey();
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