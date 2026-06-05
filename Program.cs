using System;
using System.Data.SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public Cliente(string nombrePersonal, string telefono, string tipo, double deuda, int visitas)
        : base(nombrePersonal, telefono)
    {
        this.tipo = tipo;
        this.deuda = deuda;     
        this.visitas = visitas;  
    }

    public string Tipo
    {
        get { return tipo; }
        set
        {
            if (value != null && value.Trim() != "")
                tipo = value;
        }
    }

    public double Deuda
    {
        get { return deuda; }
        set
        {
            if (value >= 0) 
                deuda = value;
        }
    }

    public int Visitas
    {
        get { return visitas; }
        set
        {
            if (value >= 0)
                visitas = value;
        }
    }

    public string MostrarCliente()
    {
        return "| Nombre: " + NombrePersonal +
               " | Telefono: " + Telefono +
               " | Tipo: " + Tipo +
               " | Deuda: Q" + Deuda +
               " | Visitas: " + Visitas + " |";
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

            string sql = @" 

         CREATE TABLE IF NOT EXISTS productos(
                codigo  INTEGER PRIMARY KEY,
                nombre  TEXT    NOT NULL,
                fecha   TEXT    NOT NULL,
                precio  REAL    NOT NULL,
                cantidad INTEGER NOT NULL,
                vendidos INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS clientes(
                id       INTEGER PRIMARY KEY AUTOINCREMENT,
                nombre   TEXT    NOT NULL,
                telefono TEXT    NOT NULL,
                tipo     TEXT    NOT NULL,
                deuda    REAL    NOT NULL,
                visitas  INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS proveedores(
                id           INTEGER PRIMARY KEY AUTOINCREMENT,
                nombre       TEXT    NOT NULL,
                telefono     TEXT    NOT NULL,
                distribuidor TEXT    NOT NULL,
                producto     TEXT    NOT NULL
            );";

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

    static void GuardarDato2(Cliente comprador)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql =  "INSERT INTO clientes(nombre, telefono, tipo, deuda, visitas) " +
            "VALUES(@nombre, @telefono, @tipo, @deuda, @visitas)";

            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@nombre", comprador.NombrePersonal);
            comando.Parameters.AddWithValue("@telefono", comprador.Telefono);
            comando.Parameters.AddWithValue("@tipo", comprador.Tipo);
            comando.Parameters.AddWithValue("@deuda", comprador.Deuda);  
            comando.Parameters.AddWithValue("@visitas", comprador.Visitas);

            try
            {
                comando.ExecuteNonQuery();
                Console.WriteLine("Cliente guardado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message); 
                Console.ReadKey();
            }
        }
    }

    static void GuardarDato3(Proveedor proveedor)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();
            string sql = "INSERT INTO proveedores(nombre, telefono, distribuidor, producto) " +
                         "VALUES(@nombre, @telefono, @distribuidor, @producto)";

            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@nombre", proveedor.NombrePersonal);
            comando.Parameters.AddWithValue("@telefono", proveedor.Telefono);
            comando.Parameters.AddWithValue("@distribuidor", proveedor.Distribuidor);
            comando.Parameters.AddWithValue("@producto", proveedor.Producto);

            try
            {
                comando.ExecuteNonQuery();
                Console.WriteLine("Proveedor guardado");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
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

    static void ModificarCliente(string nombre, Cliente cliente)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();

            string sql = "UPDATE clientes SET " +
                         "telefono=@telefono, tipo=@tipo, " +
                         "deuda=@deuda, visitas=@visitas " +
                         "WHERE nombre=@nombre";

            SQLiteCommand comando = new SQLiteCommand(sql, db);

            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@telefono", cliente.Telefono);
            comando.Parameters.AddWithValue("@tipo", cliente.Tipo);
            comando.Parameters.AddWithValue("@deuda", cliente.Deuda);
            comando.Parameters.AddWithValue("@visitas", cliente.Visitas);

            int filas = comando.ExecuteNonQuery();

            if (filas > 0)
                Console.WriteLine("Cliente modificado");
            else
                Console.WriteLine("Cliente no encontrado");
        }
    }

    static void ModificarProveedor(string nombre, Proveedor proveedor)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();
            string sql = "UPDATE proveedores SET " +
                         "nombre=@nombreNuevo, telefono=@telefono, " +
                         "distribuidor=@distribuidor, producto=@producto " +
                         "WHERE nombre=@nombre";

            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@nombreNuevo", proveedor.NombrePersonal);
            comando.Parameters.AddWithValue("@telefono", proveedor.Telefono);
            comando.Parameters.AddWithValue("@distribuidor", proveedor.Distribuidor);
            comando.Parameters.AddWithValue("@producto", proveedor.Producto);

            int filas = comando.ExecuteNonQuery();
            if (filas > 0)
                Console.WriteLine("Proveedor modificado");
            else
                Console.WriteLine("Proveedor no encontrado");
        }
    }

    static void EliminarProveedor(string nombre)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();
            string sql = "DELETE FROM proveedores WHERE nombre=@nombre";
            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.ExecuteNonQuery();
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

    static void EliminarCliente(string nombre)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();
            string sql = "DELETE FROM clientes WHERE nombre=@nombre";
            SQLiteCommand comando = new SQLiteCommand(sql, db);
            comando.Parameters.AddWithValue("@nombre", nombre);
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

    static void CargarCLientes(Dictionary<string, Cliente> clientes)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();
            string sql = "SELECT * FROM clientes";
            SQLiteCommand comando = new SQLiteCommand(sql, db);
            SQLiteDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                string nombre = lector["nombre"].ToString();
                string telefono = lector["telefono"].ToString();
                string tipo = lector["tipo"].ToString();
                double deuda = double.Parse(lector["deuda"].ToString());
                int visitas = int.Parse(lector["visitas"].ToString());

                Cliente c = new Cliente(nombre, telefono, tipo, deuda, visitas);
                clientes[nombre] = c;
            }
        }
    }

    static void CargarProveedor(Dictionary<string, Proveedor> proveedores)
    {
        using (SQLiteConnection db = new SQLiteConnection(conexion))
        {
            db.Open();
            string sql = "SELECT * FROM proveedores";
            SQLiteCommand comando = new SQLiteCommand(sql, db);
            SQLiteDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                string nombre = lector["nombre"].ToString();
                string telefono = lector["telefono"].ToString();
                string distribuidor = lector["distribuidor"].ToString();
                string producto = lector["producto"].ToString();

                Proveedor p = new Proveedor(distribuidor, producto, nombre, telefono);
                proveedores[nombre] = p;
            }
        }
    }




    ////////////////////////////////////////////////

    static void Main()
    {
   
        CrearTabla();

        Dictionary<int, Productos> producto = new Dictionary<int, Productos>();
        Dictionary<string, Cliente> clientes = new Dictionary<string, Cliente>();    
        Dictionary<string, Proveedor> proveedores = new Dictionary<string, Proveedor>(); 

        CargarInventario(producto);
        CargarCLientes(clientes);
        CargarProveedor(proveedores);

        int codigo, cantidad, opcion, vender, CodigoDeBarras, vendidos = 0;
        string productoNuevo;
        string distribuidorNuevo;
        string nombre, tipo, nombr3, telefono;
        DateTime fecha;
        double deuda, precio;
        bool validacion;
        int opc, visitas;

       

        do
        {
            Console.Clear();
            Console.WriteLine("|--------------Menu----------------|");
            Console.WriteLine("| Opcion 1: Vender Producto        |");
            Console.WriteLine("| Opcion 2: Agregar Producto       |");
            Console.WriteLine("| Opcion 3: Buscar Producto        |");
            Console.WriteLine("| Opcion 4: Mostrar Inventario     |");
            Console.WriteLine("| Opcion 5: Gestionar Cliente      |");
            Console.WriteLine("| Opcion 6: Gestionar Proveedor    |");
            Console.WriteLine("| Opcion 7: Informe General        |");
            Console.WriteLine("| Opcion 8: Cerrar                 |");
            Console.WriteLine("|----------------------------------|");
            Console.Write("           Opcion:  ");       
            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opcion Incorecta");
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
                                {                                    //-----------------------------------//
                                    Console.Write("| Producto: " + producto[CodigoDeBarras].Nombre + " | Precio: " + producto[CodigoDeBarras].Precio);
                                    Console.Write(" | Ingrese la cantidad: ");
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
                                    Console.WriteLine("");
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
                        Console.Clear();
                        Console.WriteLine("----------Agregar Productos-------");

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

                            do
                            {
                                Console.Write("Ingrese el nombre: ");
                                nombre = Console.ReadLine().Trim(); 
                                if (nombre == "")
                                {
                                    Console.WriteLine("Erroir ");
                                }
                            } 
                            while (nombre == "");
                                
                           nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1).ToLower();
                                
                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese fecha vencimiento: ");
                                validacion = DateTime.TryParse(Console.ReadLine(), out fecha);
                                if (!validacion || fecha.Date < DateTime.Now.Date)
                                {
                                    Console.WriteLine("Fecha invalida");
                                    validacion = false;
                                }

                            } while (!validacion);

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese el precio: ");
                                validacion = double.TryParse(Console.ReadLine(), out precio);
                                if (!validacion || precio <= 0)
                                {
                                    Console.WriteLine("Precio invalido");
                                    validacion = false;
                                }
                            } while (!validacion);
                            //-----------------------------------//

                            do
                            {
                                Console.Write("Ingrese la cantidad: ");
                                validacion = int.TryParse(Console.ReadLine(), out cantidad);
                                if (!validacion || cantidad < 0) 
                                {
                                    Console.WriteLine("Cantidad invalida");
                                    validacion = false;
                                }
                            } while (!validacion);

                            //-----------------------------------//

                            try
                            {
                                Productos almacenar = new Productos(codigo, nombre, fecha, precio, cantidad, vendidos);
                                producto.Add(codigo, almacenar);
                                GuardarDatos1(almacenar);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine("Error al guardar: " + ex.Message);
                            }


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

                    //-----------------------------------//
                    do
                    {
                        Console.Write("Ingrese el codigo del producto: ");
                        validacion = int.TryParse(Console.ReadLine(), out CodigoDeBarras);
                        if (!validacion || CodigoDeBarras <= 0)
                        {
                            Console.WriteLine("Codigo invalido");
                            validacion = false;
                        }
                    } while (!validacion);
                    //-----------------------------------//

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
                                        Console.WriteLine("Opcion 6: Terminar de editar");
                                        //-----------------------------------//
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
                                        //-----------------------------------//
                                        switch (opc)
                                        {
                                            case 1:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo nombre: ");
                                                    nombre = Console.ReadLine().Trim();
                                                    if (nombre == "")
                                                        Console.WriteLine("Nombre invalido");
                                                } while (nombre == "");
                                                producto[CodigoDeBarras].Nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1).ToLower();
                                                //-----------------------------------//
                                                break;
                                            case 2:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva Fecha: ");
                                                    validacion = DateTime.TryParse(Console.ReadLine(), out fecha);
                                                    if (!validacion || fecha.Date < DateTime.Now.Date)
                                                    {
                                                        Console.WriteLine("Fecha invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Fecha = fecha;
                                                //-----------------------------------//
                                                break;
                                            case 3:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo Precio: ");
                                                    validacion = double.TryParse(Console.ReadLine(), out precio);
                                                    if (!validacion || precio <= 0)
                                                    {
                                                        Console.WriteLine("Precio invalido");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Precio = precio;
                                                //-----------------------------------//
                                                break;
                                            case 4:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva cantidad: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out cantidad);
                                                    if (!validacion || cantidad < 0)
                                                    {
                                                        Console.WriteLine("Cantidad invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Cantidad = cantidad;
                                                //-----------------------------------//
                                                break;
                                            case 5:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo nombre: ");
                                                    nombre = Console.ReadLine().Trim();
                                                    if (nombre == "")
                                                        Console.WriteLine("Nombre invalido");
                                                } while (nombre == "");
                                                producto[CodigoDeBarras].Nombre = nombre.Substring(0, 1).ToUpper() + nombre.Substring(1).ToLower();

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva Fecha: ");
                                                    validacion = DateTime.TryParse(Console.ReadLine(), out fecha);
                                                    if (!validacion || fecha.Date < DateTime.Now.Date)
                                                    {
                                                        Console.WriteLine("Fecha invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Fecha = fecha;

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo Precio: ");
                                                    validacion = double.TryParse(Console.ReadLine(), out precio);
                                                    if (!validacion || precio <= 0)
                                                    {
                                                        Console.WriteLine("Precio invalido");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Precio = precio;

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva cantidad: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out cantidad);
                                                    if (!validacion || cantidad < 0)
                                                    {
                                                        Console.WriteLine("Cantidad invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].Cantidad = cantidad;
                                                //-----------------------------------//
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

                    Console.Clear();
                    Console.WriteLine("|-------- Gestionar Cliente --------|");
                    Console.WriteLine("| Opcion 1: Agregar                 |");
                    Console.WriteLine("| Opcion 2: Mostrar                 |");
                    Console.WriteLine("| Opcion 3: Modificar               |");
                    Console.WriteLine("| Opcion 4: Regresar                |");
                    Console.WriteLine("|-----------------------------------|");
                    Console.Write("           Opcion: ");
                    while (!int.TryParse(Console.ReadLine(), out opcion))
                    {
                        Console.WriteLine("Error de dato");
                    }

                    switch (opcion)
                    {
                        //////////////////////////////////////////////////////
                        case 1:
                          
                            break;
                        //////////////////////////////////////////////////////
                        case 2:
                            Console.Clear();
                            
                            Console.ReadKey();
                            break;
                        //////////////////////////////////////////////////////
                        case 3:

                            Console.Clear();

                           


                        
                            break;

                        default:
                            break;
                    }
                    break;


                ////////////////////////////////////////////////////////////////////
                case 6:
                    Console.Clear();
                    Console.WriteLine("|-------- Gestionar Proveedor ------|");
                    Console.WriteLine("| Opcion 1: Agregar                 |");
                    Console.WriteLine("| Opcion 2: Mostrar                 |");
                    Console.WriteLine("| Opcion 3: Borrar                  |");
                    Console.WriteLine("| Opcion 4: Regresar                |");
                    Console.WriteLine("|-----------------------------------|");
                    Console.Write("           Opcion: ");
                    while (!int.TryParse(Console.ReadLine(), out opcion))
                    {
                        Console.WriteLine("Error de dato");
                    }

                    switch (opcion)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("--------- Agregar Proveedor --------");

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese el nombre: ");
                                !validacion = string(Console.ReadLine(), out );
                                if (!validacion)
                                {
                                    Console.WriteLine("Nombre invalido");
                                }
                            } while (!validacion);

                            nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                            if (proveedores.ContainsKey(nombr3))
                            {
                                Console.WriteLine("Proveedor ya existe");

                                Console.ReadKey();
                            }

                            //-----------------------------------//
                           

                            //-----------------------------------//
                       
                        

                            //-----------------------------------//
                          
                   

                            //-----------------------------------//
                       
                            break;

                        case 2:
                            Console.Clear();
                            if (proveedores.Count == 0)
                            {
                                Console.WriteLine("No hay proveedores");
                            }
                            else
                            {
                                Console.WriteLine("-------- Proveedores --------");
                                foreach (var item in proveedores)
                                {
                                    Console.WriteLine(
                                        "| Nombre:       " + item.Value.NombrePersonal +
                                        " | Telefono:     " + item.Value.Telefono +
                                        " | Distribuidor: " + item.Value.Distribuidor +
                                        " | Producto:     " + item.Value.Producto + " |");
                                }
                            }
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.Clear();

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese el nombre del proveedor: ");
                                nombr3 = Console.ReadLine().Trim();
                                if (nombr3 == "")
                                    Console.WriteLine("Nombre invalido");
                            } while (nombr3 == "");
                            nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();
                            //-----------------------------------//

                            if (proveedores.ContainsKey(nombr3))
                            {
                                Console.WriteLine("");
                                Console.WriteLine("| Nombre:       " + proveedores[nombr3].NombrePersonal);
                                Console.WriteLine("| Telefono:     " + proveedores[nombr3].Telefono);
                                Console.WriteLine("| Distribuidor: " + proveedores[nombr3].Distribuidor);
                                Console.WriteLine("| Producto:     " + proveedores[nombr3].Producto);

                                do
                                {
                          Console.WriteLine("");
                          Console.WriteLine("¿Que desea realizar?");
                          Console.WriteLine("Opcion 1: Borrar");
                                    Console.WriteLine("Opcion 2: Editar");
                                    Console.WriteLine("Opcion 3: Regresar");
                                    while (!int.TryParse(Console.ReadLine(), out opcion))
                                    {
                                        Console.WriteLine("Error de dato");
                                    }

                                    switch (opcion)
                                    {
                                        case 1:
                                            //-----------------------------------//
                                            proveedores.Remove(nombr3);
                                            EliminarProveedor(nombr3);
                                            Console.WriteLine("Proveedor eliminado");
                                            Console.ReadKey();
                                            break;

                                        case 2:
                                            Console.Clear();
                                            Console.WriteLine("---------Editar Proveedor---------");

                                            do
                                            {
                                                Console.Clear();
                                                Console.WriteLine("| Nombre:       " + proveedores[nombr3].NombrePersonal);
                                                Console.WriteLine("| Telefono:     " + proveedores[nombr3].Telefono);
                                                Console.WriteLine("| Distribuidor: " + proveedores[nombr3].Distribuidor);
                                                Console.WriteLine("| Producto:     " + proveedores[nombr3].Producto);
                                                Console.WriteLine("");
                                                Console.WriteLine("Que deseas editar");
                                                Console.WriteLine("Opcion 1: Nombre");
                                                Console.WriteLine("Opcion 2: Telefono");
                                                Console.WriteLine("Opcion 3: Distribuidor");
                                                Console.WriteLine("Opcion 4: Producto");
                                                Console.WriteLine("Opcion 5: Terminar de editar");

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Opcion: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out opc);
                                                    if (!validacion)
                                                    {
                                                        Console.WriteLine("Opcion invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                //-----------------------------------//

                                                switch (opc)
                                                {
                                                    case 1:
                                                        //-----------------------------------//
                                                        string nombreViejoP = nombr3;
                                                        do
                                                        {
                                                            Console.Write("Nuevo nombre: ");
                                                            nombr3 = Console.ReadLine().Trim();
                                                            if (nombr3 == "")
                                                            {
                                                                Console.WriteLine("Nombre invalido");
                                                            }
                                                        } while (nombr3 == "");
                                                        nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                                                        Proveedor tempP = proveedores[nombreViejoP];
                                                        tempP.NombrePersonal = nombr3;
                                                        proveedores.Remove(nombreViejoP);
                                                        proveedores[nombr3] = tempP;
                                                        EliminarProveedor(nombreViejoP);
                                                        GuardarDato3(proveedores[nombr3]);
                                                        //-----------------------------------//
                                                        break;

                                                    case 2:
                                                        //-----------------------------------//
                                                        do
                                                        {
                                                            Console.Write("Nuevo telefono: ");
                                                            telefono = Console.ReadLine().Trim();
                                                            if (telefono.Length != 8)
                                                            {
                                                                Console.WriteLine("Telefono invalido");
                                                            }
                                                        } while (telefono.Length != 8);
                                                        proveedores[nombr3].Telefono = telefono;
                                                        //-----------------------------------//
                                                        break;

                                                    case 3:
                                                        //-----------------------------------//
                                                        do
                                                        {
                                                            Console.Write("Nuevo distribuidor: ");
                                                            distribuidorNuevo = Console.ReadLine().Trim();
                                                            if (distribuidorNuevo == "")
                                                            {
                                                                Console.WriteLine("Distribuidor invalido");
                                                            }
                                                        } while (distribuidorNuevo == "");
                                                        proveedores[nombr3].Distribuidor = distribuidorNuevo.Substring(0, 1).ToUpper() + distribuidorNuevo.Substring(1).ToLower();
                                                        //-----------------------------------//
                                                        break;

                                                    case 4:
                                                        //-----------------------------------//
                                                        do
                                                        {
                                                            Console.Write("Nuevo producto: ");
                                                            productoNuevo = Console.ReadLine().Trim();
                                                            if (productoNuevo == "")
                                                            {
                                                                Console.WriteLine("Producto invalido");
                                                            }
                                                        } while (productoNuevo == "");
                                                        proveedores[nombr3].Producto = productoNuevo.Substring(0, 1).ToUpper() + productoNuevo.Substring(1).ToLower();
                                                        //-----------------------------------//
                                                        break;

                                                    default:
                                                        break;
                                                }

                                            } while (opc != 5);

                                            ModificarProveedor(nombr3, proveedores[nombr3]);
                                            Console.ReadKey();
                                            break;

                                        default:
                                            break;
                                    }

                                } while (opcion != 3);
                            }
                            else
                            {
                                Console.WriteLine("Proveedor no encontrado");
                                Console.ReadKey();
                            }
                            break;

                        default:
                            break;
                    }
                    break;


                ////////////////////////////////////////////////////////////////////
                case 7:

                    int vence;
                    double totalVentas = 0;

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

                            totalVentas += item.Value.Vendidos * item.Value.Precio;
                        }

                        if (popular != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("----Producto mas vendido----");
                            Console.WriteLine("| Articulo: " + popular.Nombre + " | Unidade vendidas:  "+ popular.Vendidos +" |");
                        }

                        if(reponer.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("|---Productos a reponer---");
                            foreach(Productos item in reponer)
                            {
                                Console.WriteLine("| Codigo: " + item.Codigo +"| Nombre: " + item.Nombre + "| Sin existecias |");
                            }

                        }

                        if (cambio.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("----Productos ha vencer---");
                            foreach (Productos item in cambio)
                            {
                                Console.WriteLine("| Codigo: " + item.Codigo + "| Nombre: " + item.Nombre + "| fecha: "+ item.Fecha.ToString("dd/MM/yyyy") + " |");
                            }

                        }
                        Console.WriteLine("");
                        Console.WriteLine("--- - Total generado por ventas-----");
                        Console.WriteLine("Total: Q" + totalVentas);
                    }


                    Console.ReadKey();
                    break;


                ////////////////////////////////////////////////////////////////////
                default:
                    Console.WriteLine("Saliendo...");
                    break;
            }

        } while (opcion != 8);
    }
}
