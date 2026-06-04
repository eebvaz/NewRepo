using System;
using System.Data.SQLite;

class Persona
{
    private string nombrePersonal;
    private string telefono;

    public Persona(string nombrePersonal, string telefono)
    {
        this.nombrePersonal = nombrePersonal;
        this.telefono = telefono;
    }

    public string NombrePersonal
    {
        get { return this.nombrePersonal; }
        set { this.nombrePersonal = value; }
    }

    public string Telefono
    {
        get { return telefono; }
        set
        {
            if (value.Length == 8)
            {
                telefono = value;
            }
        }
    }
}

//////////////////////////////////////////////////

class Cliente : Persona
{
  
    private string tipo;
    private double deuda;
    private int visitas;

    public Cliente(string nombrePersonal, string telefono, string comprador, string tipo)
       : base(nombrePersonal, telefono)
    {
       this.tipo = tipo;
       this.visitas = visitas;
       this.deuda = deuda;
    }


    public string Tipo
    {
        get { return tipo; }
        set { tipo = value; }
    }

    public double Deuda
    {
        get { return deuda; }
        set { deuda = value; }
    }

    public int Visitas
    {
        get { return visitas; }
        set
        {
            if (value >= 0)
            {
                visitas = value;
            }
        }
    }
}

//////////////////////////////////////////////////

class Proveedor : Persona
{
    private string distribuidor;
    private string producto;

    public Proveedor(string distribuidor, string producto, string nombrePersonal, string telefono)
      : base(nombrePersonal, telefono)
    {
        this.distribuidor = distribuidor;
        this.producto = producto;
    }

    public string Distribuidor
    {
        get { return distribuidor; }
        set { distribuidor = value; }
    }

    public string Producto
    {
        get { return producto; }
        set { producto = value; }
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
        Codigo = codigo;
        Nombre = nombre;
        Fecha = fecha;
        Precio = precio;
        Cantidad = cantidad;
        Vendidos = vendidos;
    }

    public int Codigo
    {
        get { return codigo; }
        set
        {
            if (value > 0)
            {
                codigo = value;
            }
        }
    }

    public string Nombre
    {
        get { return nombre; }
        set
        {
            if ((value != null && value.Trim() != ""))
            {
                nombre = value;
            }
        }
    }

    public DateTime Fecha
    {
        get { return fecha; }
        set
        {
            if (value >= DateTime.Now.Date)
            {
                fecha = value;
            }
        }
    }

    public double Precio
    {
        get { return precio; }
        set
        {
            if (value > 0)
            {
                precio = value;
            }
        }
    }

    public int Cantidad
    {
        get { return cantidad; }
        set
        {
            if (value >= 0)
            {
                cantidad = value;
            }
        }
    }

    public int Vendidos
    {
        get { return vendidos; }
        set
        {
            if (value >= 0)
            {
                vendidos = value;
            }
        }
    }

    public double Total()
    {
        return precio * cantidad;
    }

    public string EstadoProducto()
    {
        if (cantidad <= 0)
        {
            return "No disponible ";
        }
        else if (fecha < DateTime.Now)
        {
            return "Vencido";
        }
        else if ((fecha - DateTime.Now).Days <= 0)
        {
            return "Por vencer";
        }
        else
        {
            return "Disponible ";
        }
    }

    public string MostrarDatos()
    {
        return "| Codigo: " + Codigo +
              " | Nombre: " + Nombre +
              " | Fecha: " + Fecha.ToShortDateString() +
              " | Precio: Q" + Precio +
              " | Cantidad: " + Cantidad +
              " | Vendidos: " + Vendidos +
              " | Estado: " + EstadoProducto();
    }
}

//////////////////////////////////////////////////

class Programa
{

    private static string conexion = "Data Source=inventario.db";

    static void CrearTabla()
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql = @" CREATE TABLE IF NOT EXISTS productos(

                codigo  INTEGER PRIMARY KEY,
                nombre  TEXT    NOT NULL,
                fecha   TEXT    NOT NULL,
                precio  REAL    NOT NULL,
                cantidad INTEGER NOT NULL,
                vendidos INTEGER NOT NULL
            );

        
             






            ";

            SQLiteCommand comando = new SQLiteCommand(sql, db);

            comando.ExecuteNonQuery();
        }
    }


    static void GuardarDatos1(Productos articulo0)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql =  "INSERT INTO productos" +
                "(codigo, nombre, fecha, precio, cantidad, vendidos) " +
                "VALUES(@codigo, @nombre, @fecha, @precio, @cantidad, @vendidos)";

            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@codigo", articulo0.Codigo);
            comando.Parameters.AddWithValue("@nombre", articulo0.Nombre);
            comando.Parameters.AddWithValue("@fecha", articulo0.Fecha.ToString());
            comando.Parameters.AddWithValue("@precio", articulo0.Precio);
            comando.Parameters.AddWithValue("@cantidad", articulo0.Cantidad);
            comando.Parameters.AddWithValue("@vendidos", articulo0.Vendidos);

            comando.ExecuteNonQuery();
            Console.WriteLine("Producto agregado");
        }
    }

    static void GuardarDatos2(Productos articulo0)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql = "INSERT INTO productos" +
                "(codigo, nombre, fecha, precio, cantidad, vendidos) " +
                "VALUES(@codigo, @nombre, @fecha, @precio, @cantidad, @vendidos)";

            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@codigo", articulo0.Codigo);
            comando.Parameters.AddWithValue("@nombre", articulo0.Nombre);
            comando.Parameters.AddWithValue("@fecha", articulo0.Fecha.ToString());
            comando.Parameters.AddWithValue("@precio", articulo0.Precio);


            comando.ExecuteNonQuery();
            Console.WriteLine("Producto agregado");
        }
    }



    static void ModificarDatos(Productos articulo1)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql ="UPDATE productos SET " +
                "nombre=@nombre, fecha=@fecha, " +
                "precio=@precio, cantidad=@cantidad, " +
                "vendidos=@vendidos " +
                "WHERE codigo=@codigo";

            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@codigo", articulo1.Codigo);
            comando.Parameters.AddWithValue("@nombre", articulo1.Nombre);
            comando.Parameters.AddWithValue("@fecha", articulo1.Fecha.ToString());
            comando.Parameters.AddWithValue("@precio", articulo1.Precio);
            comando.Parameters.AddWithValue("@cantidad", articulo1.Cantidad);
            comando.Parameters.AddWithValue("@vendidos", articulo1.Vendidos);

            int filas = comando.ExecuteNonQuery();

            if (filas > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Inventario modificado");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Codigo no encontrado");
            }
        }
    }

    static void EliminarProducto(int codigo)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql = "DELETE FROM productos " + "WHERE codigo=@codigo";

            SQLiteCommand comando =new SQLiteCommand(sql, db);

            comando.Parameters.AddWithValue( "@codigo", codigo);
            comando.ExecuteNonQuery();
        }
    }




    static void CargarInventario(Dictionary<int, Productos> productos)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql = "SELECT * FROM productos";

            SQLiteCommand comando = new SQLiteCommand(sql, db);

            SQLiteDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                int codigo = int.Parse(lector["codigo"].ToString());
                string nombre = lector["nombre"].ToString();
                DateTime fecha = DateTime.Parse(lector["fecha"].ToString());
                double precio = double.Parse(lector["precio"].ToString());
                int cantidad = int.Parse(lector["cantidad"].ToString());
                int vendidos = int.Parse(lector["vendidos"].ToString());

                Productos articulo2 = new Productos(codigo, nombre, fecha, precio, cantidad, vendidos);

                productos[codigo] = articulo2;
            }
        }
    }

    ////////////////////////////////////////////////

    static void Main()
    {
   
        CrearTabla();

        Dictionary<int, Productos> producto = new Dictionary<int, Productos>();

        CargarInventario(producto);

        int codigo, cantidad, opcion, vender, CodigoDeBarras, vendidos = 0;
        string nombre;
        DateTime fecha;
        double precio;
        bool validacion;
        int opc;

        do
        {
            Console.Clear();
            Console.WriteLine("--------------Menu----------------");
            Console.WriteLine("Opcion 1: Vender Producto");
            Console.WriteLine("Opcion 2: Agregar Producto");
            Console.WriteLine("Opcion 3: Buscar Producto");
            Console.WriteLine("Opcion 4: Mostrar Inventario");
            Console.WriteLine("Opcion 5: Gestionar Cliente");
            Console.WriteLine("Opcion 6: Gestionar Proveedor");
            Console.WriteLine("Opcion 7: Cerrar");

            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Error");
            }

            switch (opcion)
            {
                ////////////////////////////////////////////////////////////////////
                case 1:
                    Console.Clear();
                    Console.WriteLine("------------Vender Producto-----------");
                    double PrecioTotal = 0;
                    do
                    {
                        do
                        {
                            //-----------------------------------//
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


                            if (producto[CodigoDeBarras].Cantidad <= 0)
                            {
                                Console.WriteLine("No hay existencias");
                            }

                            else
                            {
                                do
                                {

                                    //-----------------------------------//
                                    Console.Write("| Producto: " + producto[CodigoDeBarras].Nombre + " | Precio: " + producto[CodigoDeBarras].Precio);
                                    Console.Write("| Ingrese la cantidad: ");
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


                                    ModificarDatos(producto[CodigoDeBarras]);

                                    Console.WriteLine("Venta realizada");
                                    Console.WriteLine("Total: Q" + general);
                                }
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
                    break;


                ////////////////////////////////////////////////////////////////////
                case 2:
                    Console.Clear();
                    do
                    {
                        Console.WriteLine("---------Agregar Productos-------");

                        do
                        {
                            //-----------------------------------//
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
                            //-----------------------------------//
                            Console.Write("Ingrese el nombre: ");
                            nombre = Console.ReadLine();
                            nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1).ToLower();

                            do
                            {
                                //-----------------------------------//
                                Console.Write("Ingrese fecha vencimiento: ");
                                validacion = DateTime.TryParse(Console.ReadLine(), out fecha);
                                if (!validacion)
                                {
                                    Console.WriteLine("Fecha invalida");
                                }
                            } while (!validacion);

                            do
                            {
                                //-----------------------------------//
                                Console.Write("Ingrese el precio: ");
                                validacion = double.TryParse(Console.ReadLine(), out precio);
                                if (!validacion)
                                {
                                    Console.WriteLine("dato invalido");
                                }
                            } while (!validacion);

                            do
                            {
                                //-----------------------------------//
                                Console.Write("Ingrese la cantidad: ");
                                validacion = int.TryParse(Console.ReadLine(), out cantidad);
                                if (!validacion)
                                {
                                    Console.WriteLine("codigo invalido");
                                }
                            } while (!validacion);

                            //-----------------------------------//

                            Productos almacenar = new Productos(codigo, nombre, fecha, precio, cantidad, vendidos);

                            producto.Add(codigo, almacenar);


                            GuardarDatos1(almacenar);

                            Console.WriteLine("Productos añadidos");
                        }

                        Console.WriteLine("Desea agregar otro producto");
                        Console.WriteLine("1: Si");
                        Console.WriteLine("2: No");
                        do
                        {
                            //-----------------------------------//
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
                    break;


                ////////////////////////////////////////////////////////////////////
                case 3:
                    Console.Clear();
                    Console.WriteLine("-----------------Buscar-----------------");
                    Console.Write("Ingrese el codigo del producto: ");
                    while (!int.TryParse(Console.ReadLine(), out CodigoDeBarras))
                    {
                        Console.WriteLine("Error de dato");
                    }

                    if (producto.ContainsKey(CodigoDeBarras))
                    {
                        Console.WriteLine(producto[CodigoDeBarras].MostrarDatos());

                        do
                        {
                            Console.WriteLine("");
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

                                    EliminarProducto(CodigoDeBarras);

                                    Console.WriteLine("Se ha eliminado");
                                    break;



                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("---------Editar---------");

                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine(producto[CodigoDeBarras].MostrarDatos());
                                        Console.WriteLine("");
                                        Console.WriteLine("Que deseas editar");
                                        Console.WriteLine("Opcion 1: Nombre");
                                        Console.WriteLine("Opcion 2: Fecha");
                                        Console.WriteLine("Opcion 3: Precio");
                                        Console.WriteLine("Opcion 4: Cantidad");
                                        Console.WriteLine("Opcion 5: Todos");
                                        Console.WriteLine("Opcion 6: Regresar al menu");

                                        do
                                        {
                                            //-----------------------------------//
                                            Console.Write("Opcion: ");
                                            validacion = int.TryParse(Console.ReadLine(), out opc);
                                            if (!validacion)
                                            {
                                                Console.WriteLine("opcion invalida");
                                                validacion = false;
                                            }
                                        } while (!validacion);

                                        switch (opc)
                                        {
                                            case 1:

                                                Console.Write("Nuevo nombre: ");
                                                producto[CodigoDeBarras].Nombre = Console.ReadLine();
                                                producto[CodigoDeBarras].Nombre = producto[CodigoDeBarras].Nombre.Substring(0, 1).ToUpper()
                                                    + producto[CodigoDeBarras].Nombre.Substring(1).ToLower();

                                                break;
                                            case 2:

                                                do
                                                {
                                                    //-----------------------------------//
                                                    Console.Write("Nueva Fecha: ");
                                                    validacion = DateTime.TryParse(Console.ReadLine(), out fecha);
                                                    if (!validacion)
                                                    {
                                                        Console.WriteLine("Fecha invalida");
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Fecha = fecha;

                                                break;
                                            case 3:
                                                do
                                                {
                                                    //-----------------------------------//
                                                    Console.Write("Nuevo Precio: ");
                                                    validacion = double.TryParse(Console.ReadLine(), out precio);
                                                    if (!validacion)
                                                    {
                                                        Console.WriteLine("Precio invalido");
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Precio = precio;


                                                break;
                                            case 4:

                                                do
                                                {
                                                    //-----------------------------------//
                                                    Console.Write("Nueva cantidad: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out cantidad);
                                                    if (!validacion || cantidad < 0)
                                                    {
                                                        Console.WriteLine("Cantidad invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Cantidad = cantidad;

                                                break;
                                            case 5:

                                                Console.Write("Nuevo nombre: ");
                                                producto[CodigoDeBarras].Nombre = Console.ReadLine();
                                                producto[CodigoDeBarras].Nombre = producto[CodigoDeBarras].Nombre.Substring(0, 1).ToUpper()
                                                    + producto[CodigoDeBarras].Nombre.Substring(1).ToLower();

                                                do
                                                {
                                                    //-----------------------------------//
                                                    Console.Write("Nueva Fecha: ");
                                                    validacion = DateTime.TryParse(Console.ReadLine(), out fecha);
                                                    if (!validacion)
                                                    {
                                                        Console.WriteLine("Fecha invalida");
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Fecha = fecha;

                                                do
                                                {
                                                    //-----------------------------------//
                                                    Console.Write("Nuevo Precio: ");
                                                    validacion = double.TryParse(Console.ReadLine(), out precio);
                                                    if (!validacion)
                                                    {
                                                        Console.WriteLine("Precio invalido");
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Precio = precio;

                                                do
                                                {
                                                    //-----------------------------------//
                                                    Console.Write("Nueva cantidad: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out cantidad);
                                                    if (!validacion || cantidad < 0)
                                                    {
                                                        Console.WriteLine("Cantidad invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Cantidad = cantidad;

                                                break;

                                            default:
                                                break;

                                        }


                                    } while (opc != 6);


                                    ModificarDatos(producto[CodigoDeBarras]);


                                    Console.ReadKey();
                                    break;

                                default:
                                    break;
                            }

                        } while (opcion != 3);
                    }
                    else
                    {
                        Console.WriteLine("Producto no encontrado");
                    }
                    Console.ReadKey();
                    break;


                    ////////////////////////////////////////////////////////////////////
                case 4:
                    Console.Clear();
                    Console.WriteLine("Inventario");

                    if (producto.Count == 0)
                    {
                        Console.WriteLine("No hay productos");
                    }
                    else
                    {
                        Productos popular = null;

                        foreach (var item in producto)
                        {
                            Console.WriteLine(item.Value.MostrarDatos() + "| Valor total: Q" + item.Value.Total() + " |");

                            if (popular == null || item.Value.Vendidos > popular.Vendidos)
                            {
                                popular = item.Value;
                            }
                        }

                        if (popular != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Producto mas vendido: " + popular.Nombre);
                            Console.WriteLine("Cantidad mas vendida: " + popular.Vendidos);
                        }
                    }
                    Console.ReadKey();
                    break;


                ////////////////////////////////////////////////////////////////////
                case 5:
                    Console.WriteLine("Cliente");

                   Dictionary <string, Cliente> datos = new Dictionary<string, Cliente>();

                  
                    Console.Write("Ingrese el nombre: ");
                    Console.Write("Ingrese el apellido: ");

                   // GuardarDatos2();

                    break;
                ////////////////////////////////////////////////////////////////////
                case 6:

                    int vence;

                    if (producto.Count == 0)
                    {
                        Console.WriteLine("No hay productos");
                    }
                    else
                    {
                        List<Productos> reponer = new List<Productos>();
                        List<Productos> cambio = new List<Productos>();

                        Productos popular = null;

                        foreach (var item in producto)
                        {

                            ////popular
                            if (popular == null || item.Value.Vendidos > popular.Vendidos)
                            {
                                popular = item.Value;
                            }
                            /////reponer
                            if(item.Value.Cantidad == 0)
                            {
                                reponer.Add(item.Value);
                            }
                            ////Vencimiento
                            vence = (item.Value.Fecha - DateTime.Now).Days;

                            if(vence >= 0 && vence<=7)
                            {
                                cambio.Add(item.Value);
                            }


                        }

                        if (popular != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("----Producto mas vendido----");
                            Console.WriteLine("| Articulo: " + popular.Nombre + " | Unidades vendidas:  "+ popular.Vendidos +" |");
                        }

                        if(reponer.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("---Productos a reponer---");
                            foreach(Productos item in reponer)
                            {
                                Console.WriteLine("| Codigo: " + item.Codigo +"| Nombre: " + item.Nombre + "| Sin existecias |");
                            }

                        }

                        if (cambio.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("---Productos a vencer---");
                            foreach (Productos item in cambio)
                            {
                                Console.WriteLine("| Codigo: " + item.Codigo + "| Nombre: " + item.Nombre + "| fecha: "+ item.Fecha.ToString("dd/MM/yyyy") + " |");
                            }

                        }
                    }

                    Console.ReadKey();



                    Console.WriteLine("Proveedor");
                    Console.Write("Ingrese el nombre: ");
                    Console.Write("Ingrese el apellido: ");

                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Saliendo...");
                    break;
            }

        } while (opcion != 7);
    }
}
