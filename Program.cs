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


    public string MostrarProovedor()
    {
        return "| Nombre: " + NombrePersonal +
               " | Telefono: " + Telefono +
               " | Distribidor: " + Distribuidor +
               " | Producto: Q" + Producto +" |";
    }
}

//////////////////////////////////////////////////

class Productos
{
    private int codigoProd;
    private string nombreProd;
    private DateTime fechaProd;
    private double precioProd;
    private int cantidadProd;
    private int vendidosProd;

    public Productos(int codigoProd, string nombreProd, DateTime fechaProd, double precioProd, int cantidadProd, int vendidosProd)
    {
        CodigoProd = codigoProd;
        NombreProd = nombreProd;
        FechaProd = fechaProd;
        PrecioProd = precioProd;
        CantidadProd = cantidadProd;
        VendidosProd = vendidosProd;
    }

    public int CodigoProd
    {
        get { return codigoProd; }
        set
        {
            if (value > 0)
            {
                codigoProd = value;
            }
        }
    }

    public string NombreProd
    {
        get { return nombreProd; }
        set
        {
            if ((value != null && value.Trim() != ""))
            {
                nombreProd = value;
            }
        }
    }

    public DateTime FechaProd
    {
        get { return fechaProd; }
        set
        {
            if (value >= DateTime.Now.Date)
            {
                fechaProd = value;
            }
        }
    }

    public double PrecioProd
    {
        get { return precioProd; }
        set
        {
            if (value > 0)
            {
                precioProd = value;
            }
        }
    }

    public int CantidadProd
    {
        get { return cantidadProd; }
        set
        {
            if (value >= 0)
            {
                cantidadProd = value;
            }
        }
    }

    public int VendidosProd
    {
        get { return vendidosProd; }
        set
        {
            if (value >= 0)
            {
                vendidosProd = value;
            }
        }
    }

    public double TotalProd()
    {
        return precioProd * cantidadProd;
    }

    public string EstadoProducto()
    {
        if (cantidadProd <= 0)
        {
            return "No disponible ";
        }
        else if (fechaProd < DateTime.Now)
        {
            return "Vencido";
        }
        else if ((fechaProd - DateTime.Now).Days <= 0)
        {
            return "Por vencer";
        }
        else
        {
            return "Disponible ";
        }
    }

    public string MostrarDatosProd()
    {
        return "| Codigo: " + CodigoProd +
              " | Nombre: " + NombreProd +
              " | Fecha: " + FechaProd.ToShortDateString() +
              " | Precio: Q" + PrecioProd +
              " | Cantidad: " + CantidadProd +
              " | Vendidos: " + VendidosProd +
              " | Estado: " + EstadoProducto();
    }
}

//////////////////////////////////////////////////

class Programa
{

    private static string conexion = "Data Source=DatiosTienda.db";

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
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                nombre   TEXT    NOT NULL,
                telefono TEXT    NOT NULL,
                tipo     TEXT    NOT NULL,
                deuda    REAL    NOT NULL,
                visitas  INTEGER NOT NULL
            );

          CREATE TABLE IF NOT EXISTS proveedores(
                id INTEGER PRIMARY KEY AUTOINCREMENT,
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
            comando.Parameters.AddWithValue("@codigo", articulo0.CodigoProd);
            comando.Parameters.AddWithValue("@nombre", articulo0.NombreProd);
            comando.Parameters.AddWithValue("@fecha", articulo0.FechaProd.ToString());
            comando.Parameters.AddWithValue("@precio", articulo0.PrecioProd);
            comando.Parameters.AddWithValue("@cantidad", articulo0.CantidadProd);
            comando.Parameters.AddWithValue("@vendidos", articulo0.VendidosProd);

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

    static void ModificarDatosProductos(Productos articulo1)
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
            comando.Parameters.AddWithValue("@codigo", articulo1.CodigoProd);
            comando.Parameters.AddWithValue("@nombre", articulo1.NombreProd);
            comando.Parameters.AddWithValue("@fecha", articulo1.FechaProd.ToString());
            comando.Parameters.AddWithValue("@precio", articulo1.PrecioProd);
            comando.Parameters.AddWithValue("@cantidad", articulo1.CantidadProd);
            comando.Parameters.AddWithValue("@vendidos", articulo1.VendidosProd);

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

        int codigoProd, cantidadProd, opcion, venderProd, CodigoDeBarras, vendidosProd = 0;
        string productoNuevo,  distribuidorNuevo;
        string nombreProd, tipoCliente, nombr3, telefono;
        DateTime fechaProd;
        double deudatCliente, precioProd;
        bool validacion;
        int opc, visitastCliente;

       

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
                    double PrecioTotal = 0, general = 0;

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


                            if (producto[CodigoDeBarras].CantidadProd <= 0)
                            {
                                Console.WriteLine("No hay existencias");
                            }

                            else
                            {
                                do
                                {                                    //-----------------------------------//
                                    Console.Write("| Producto: " + producto[CodigoDeBarras].NombreProd + " | Precio: " + producto[CodigoDeBarras].PrecioProd);
                                    Console.Write(" | Ingrese la cantidad: ");
                                    validacion = int.TryParse(Console.ReadLine(), out venderProd);
                                    if (!validacion || venderProd <= 0)
                                    {
                                        Console.WriteLine("Cantidad invalida");
                                        validacion = false;

                                    }
                                } while (!validacion);

                                if (venderProd > producto[CodigoDeBarras].CantidadProd)
                                {
                                    Console.WriteLine("No hay existencias");
                                }
                                else
                                {
                                    producto[CodigoDeBarras].CantidadProd -= venderProd;
                                    producto[CodigoDeBarras].VendidosProd += venderProd;

                                    general = venderProd * producto[CodigoDeBarras].PrecioProd;


                                    PrecioTotal += general;

                                    ModificarDatosProductos(producto[CodigoDeBarras]);

                                    Console.WriteLine("");
                                    Console.WriteLine("Venta realizada");
                                    Console.WriteLine("Total: Q" + general);
                                    Console.ReadKey();
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
                            validacion = int.TryParse(Console.ReadLine(), out codigoProd);
                            if (!validacion)
                            {
                                Console.WriteLine("codigo invalido");
                            }
                        } while (!validacion);

                        if (producto.ContainsKey(codigoProd))
                        {
                            Console.WriteLine("Producto ya existente");
                        }

                        else

                        {
                            //-----------------------------------//

                            do
                            {
                                Console.Write("Ingrese el nombre: ");
                                nombreProd = Console.ReadLine().Trim();
                                if (nombreProd == "")
                                {
                                    Console.WriteLine("Erroir ");
                                }
                            }
                            while (nombreProd == "");

                            nombreProd = nombreProd.Substring(0, 1).ToUpper() + nombreProd.Substring(1).ToLower();

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese fecha vencimiento: ");
                                validacion = DateTime.TryParse(Console.ReadLine(), out fechaProd);
                                if (!validacion || fechaProd.Date < DateTime.Now.Date)
                                {
                                    Console.WriteLine("Fecha invalida");
                                    validacion = false;
                                }

                            } while (!validacion);

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese el precio: ");
                                validacion = double.TryParse(Console.ReadLine(), out precioProd);
                                if (!validacion || precioProd <= 0)
                                {
                                    Console.WriteLine("Precio invalido");
                                    validacion = false;
                                }
                            } while (!validacion);
                            //-----------------------------------//

                            do
                            {
                                Console.Write("Ingrese la cantidad: ");
                                validacion = int.TryParse(Console.ReadLine(), out cantidadProd);
                                if (!validacion || cantidadProd < 0)
                                {
                                    Console.WriteLine("Cantidad invalida");
                                    validacion = false;
                                }
                            } while (!validacion);

                            //-----------------------------------//

                            try
                            {
                                Productos almacenar = new Productos(codigoProd, nombreProd, fechaProd, precioProd, cantidadProd, vendidosProd);
                                producto.Add(codigoProd, almacenar);
                                GuardarDatos1(almacenar);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine("Error al guardar: " + ex.Message);
                            }


                        }
                        Console.WriteLine("");
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
                        Console.WriteLine(producto[CodigoDeBarras].MostrarDatosProd());

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
                                        Console.WriteLine(producto[CodigoDeBarras].MostrarDatosProd());
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
                                                    nombreProd = Console.ReadLine().Trim();
                                                    if (nombreProd == "")
                                                        Console.WriteLine("Nombre invalido");
                                                } while (nombreProd == "");
                                                producto[CodigoDeBarras].NombreProd = nombreProd.Substring(0, 1).ToUpper() + nombreProd.Substring(1).ToLower();
                                                //-----------------------------------//
                                                break;
                                            case 2:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva Fecha: ");
                                                    validacion = DateTime.TryParse(Console.ReadLine(), out fechaProd);
                                                    if (!validacion || fechaProd.Date < DateTime.Now.Date)
                                                    {
                                                        Console.WriteLine("Fecha invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].FechaProd = fechaProd;
                                                //-----------------------------------//
                                                break;
                                            case 3:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo Precio: ");
                                                    validacion = double.TryParse(Console.ReadLine(), out precioProd);
                                                    if (!validacion || precioProd <= 0)
                                                    {
                                                        Console.WriteLine("Precio invalido");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].PrecioProd = precioProd;
                                                //-----------------------------------//
                                                break;
                                            case 4:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva cantidad: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out cantidadProd);
                                                    if (!validacion || cantidadProd < 0)
                                                    {
                                                        Console.WriteLine("Cantidad invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].CantidadProd = cantidadProd;
                                                //-----------------------------------//
                                                break;
                                            case 5:
                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo nombre: ");
                                                    nombreProd = Console.ReadLine().Trim();
                                                    if (nombreProd == "")
                                                        Console.WriteLine("Nombre invalido");
                                                } while (nombreProd == "");
                                                producto[CodigoDeBarras].NombreProd = nombreProd.Substring(0, 1).ToUpper() + nombreProd.Substring(1).ToLower();

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva Fecha: ");
                                                    validacion = DateTime.TryParse(Console.ReadLine(), out fechaProd);
                                                    if (!validacion || fechaProd.Date < DateTime.Now.Date)
                                                    {
                                                        Console.WriteLine("Fecha invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].FechaProd = fechaProd;

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nuevo Precio: ");
                                                    validacion = double.TryParse(Console.ReadLine(), out precioProd);
                                                    if (!validacion || precioProd <= 0)
                                                    {
                                                        Console.WriteLine("Precio invalido");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].PrecioProd = precioProd;

                                                //-----------------------------------//
                                                do
                                                {
                                                    Console.Write("Nueva cantidad: ");
                                                    validacion = int.TryParse(Console.ReadLine(), out cantidadProd);
                                                    if (!validacion || cantidadProd < 0)
                                                    {
                                                        Console.WriteLine("Cantidad invalida");
                                                        validacion = false;
                                                    }
                                                } while (!validacion);
                                                producto[CodigoDeBarras].CantidadProd = cantidadProd;
                                                //-----------------------------------//
                                                break;

                                            default:
                                                break;
                                        }

                                    } while (opc != 6);

                                    ModificarDatosProductos(producto[CodigoDeBarras]);
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
                            Console.WriteLine(item.Value.MostrarDatosProd() + "| Valor total: Q" + item.Value.TotalProd() + " |");

                            if (popular == null || item.Value.VendidosProd > popular.VendidosProd)
                            {
                                popular = item.Value;
                            }
                        }

                        if (popular != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Producto mas vendido: " + popular.NombreProd);
                            Console.WriteLine("Cantidad mas vendida: " + popular.VendidosProd);
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
                    Console.WriteLine("| Opcion 3: Eliminar                |");
                    Console.WriteLine("| Opcion 4: Modificar               |");
                    Console.WriteLine("| Opcion 5: Regresar                |");
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
                            Console.Clear();

                            Console.WriteLine("-------ClIENTES--------- ");
                            //-----------------------------------//

                            do
                            {
                                Console.Write("Ingrese el nombre: ");
                                nombr3 = Console.ReadLine().Trim();
                                if (nombr3 == "")
                                {
                                    Console.WriteLine("Erroir ");
                                }
                            }
                            while (nombr3 == "");

                            nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese el telefono: ");
                                telefono = Console.ReadLine().Trim();
                                if (telefono.Length != 8)
                                {
                                    Console.WriteLine("datos insuficients del numero telefonico");
                                }
                            } while (telefono.Length != 8);

                            //-----------------------------------//

                            do
                            {

                                Console.WriteLine("[Casual | Frecuente] ");
                                Console.Write("Ingrese el tipo de cliente ");
                                tipoCliente = Console.ReadLine().Trim();
                                if (tipoCliente == "")
                                {
                                    Console.WriteLine("Erroir ");
                                }
                            }
                            while (tipoCliente == "");

                            tipoCliente = tipoCliente.Substring(0, 1).ToUpper() + tipoCliente.Substring(1).ToLower();

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Ingrese la deuda: Q");
                                validacion = double.TryParse(Console.ReadLine(), out deudatCliente);
                                if (!validacion)
                                {
                                    Console.WriteLine("Error de dato");
                                }
                            } while (!validacion);

                            //-----------------------------------//
                            do
                            {
                                Console.Write("Numero de visitas: ");
                                validacion = int.TryParse(Console.ReadLine(), out visitastCliente);
                                if (!validacion)
                                {
                                    Console.WriteLine("Error de dato");
                                }
                            } while (!validacion);

                            //-----------------------------------//

                            try
                            {
                                Cliente clienteDatos = new Cliente(nombr3, telefono, tipoCliente, deudatCliente, visitastCliente);
                                clientes.Add(nombr3, clienteDatos);
                                GuardarDato2(clienteDatos);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine("Error al guardar: " + ex.Message);
                            }

                            Console.ReadKey();
                            break;
                        //////////////////////////////////////////////////////
                        case 2:
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("- - Lista de clientes ---");

                            if (clientes.Count == 0)
                            {
                                Console.WriteLine("No hay clientes");
                            }
                            else
                            {
                                foreach (var item in clientes)
                                {
                                    Console.WriteLine(item.Value.MostrarCliente());
                                }


                            }

                            Console.ReadKey();
                            break;
                        //////////////////////////////////////////////////////
                        case 3:
                            Console.Clear();
                            Console.WriteLine("Borrrar deuda: ");
                            do
                            {
                                Console.Write("Ingrese el nombre: ");
                                nombr3 = Console.ReadLine().Trim();
                                if (nombr3 == "")
                                {
                                    Console.WriteLine("Error ");
                                }
                            }
                            while (nombr3 == "");

                            nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                            if (clientes.ContainsKey(nombr3))
                            {
                                Console.WriteLine("Desea borrarlo: ");
                                Console.WriteLine("Opcion 1: si");
                                Console.WriteLine("Opcion 2: no ");
                                opcion = int.Parse(Console.ReadLine());

                                if (opcion == 1)
                                {
                                    clientes.Remove(nombr3);
                                    EliminarCliente(nombr3);
                                    Console.WriteLine("Se ha eliminado");
                                }
                                else
                                {
                                    Console.WriteLine("Regresando...");
                                }

                            }


                            Console.ReadKey();
                            break;
                        case 4:
                            do
                            {
                                Console.Write("Ingrese el nombre: ");
                                nombr3 = Console.ReadLine().Trim();
                                if (nombr3 == "")
                                {
                                    Console.WriteLine("Error ");
                                }
                            }
                            while (nombr3 == "");

                            nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                            do
                            {
                                Console.Clear();
                                Console.WriteLine(clientes[nombr3].MostrarCliente());
                                Console.WriteLine("");
                                Console.WriteLine("Que deseas editar");
                                Console.WriteLine("Opcion 1: Deuda");
                                Console.WriteLine("Opcion 2: Visitas");
                                Console.WriteLine("Opcion 3: Telefono");
                                Console.WriteLine("Opcion 4: Terminar de editar");
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
                                            Console.Write("Ingrese la deuda: Q");
                                            validacion = double.TryParse(Console.ReadLine(), out deudatCliente);
                                            if (!validacion)
                                            {
                                                Console.WriteLine("Error de dato");
                                            }
                                        } while (!validacion);

                                        clientes[nombr3].Deuda = deudatCliente;
                                        break;
                                    case 2:
                                        //-----------------------------------//
                                        do
                                        {
                                            Console.Write("Numero de visitas: ");
                                            validacion = int.TryParse(Console.ReadLine(), out visitastCliente);
                                            if (!validacion)
                                            {
                                                Console.WriteLine("Error de dato");
                                            }
                                        } while (!validacion);

                                        clientes[nombr3].Visitas = visitastCliente;
                                        //-----------------------------------//
                                        break;
                                    case 3:
                                        //-----------------------------------//
                                        do
                                        {
                                            Console.Write("Ingrese el telefono: ");
                                            telefono = Console.ReadLine().Trim();
                                            if (telefono.Length != 8)
                                            {
                                                Console.WriteLine("datos insuficients del numero telefonico");
                                            }
                                        } while (telefono.Length != 8);
                                        clientes[nombr3].Telefono = telefono;

                                        //-----------------------------------//

                                        break;

                                    default:
                                        break;
                                }

                            } while (opc != 4);

                            ModificarCliente(nombr3, clientes[nombr3]);

                            Console.ReadKey();


                            ///-------------------------------------------////
                            break;

                        default:
                            break;
                    }
                    break;


                ////////////////////////////////////////////////////////////////////
                case 6:
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("|-------- Gestionar Proveedor ------|");
                        Console.WriteLine("| Opcion 1: Agregar                 |");
                        Console.WriteLine("| Opcion 2: Mostrar                 |");
                        Console.WriteLine("| Opcion 3: Eliminar                |");
                        Console.WriteLine("| Opcion 4: Modificar               |");
                        Console.WriteLine("| Opcion 5: Regresar                |");
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
                           

                                Console.Write("-------Provedor--------- ");
                                //-----------------------------------//

                                do
                                {
                                    Console.Write("Ingrese el nombre: ");
                                    nombr3 = Console.ReadLine().Trim();
                                    if (nombr3 == "")
                                    {
                                        Console.WriteLine("Erroir ");
                                    }
                                }
                                while (nombr3 == "");

                                nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                                //-----------------------------------//
                                do
                                {
                                    Console.Write("Ingrese el telefono: ");
                                    telefono = Console.ReadLine().Trim();
                                    if (telefono.Length != 8)
                                    {
                                        Console.WriteLine("datos insuficients del numero telefonico");
                                    }
                                } while (telefono.Length != 8);

                                //-----------------------------------//

                                do
                                {
                                    Console.Write("Ingrese el provedor: ");
                                    distribuidorNuevo = Console.ReadLine().Trim();
                                    if (distribuidorNuevo == "")
                                    {
                                        Console.WriteLine("Error ");
                                    }
                                }
                                while (distribuidorNuevo == "");

                                distribuidorNuevo = distribuidorNuevo.Substring(0, 1).ToUpper() + distribuidorNuevo.Substring(1).ToLower();

                                //-----------------------------------//
                                do
                                {
                                    Console.Write("Ingrese el producto: ");
                                    productoNuevo = Console.ReadLine().Trim();
                                    if (productoNuevo == "")
                                    {
                                        Console.WriteLine("Error ");
                                    }
                                }
                                while (distribuidorNuevo == "");

                                productoNuevo = productoNuevo.Substring(0, 1).ToUpper() + productoNuevo.Substring(1).ToLower();

                        
                                //-----------------------------------//

                                try
                                {
                                    Proveedor distribuidors = new Proveedor(nombr3, telefono, distribuidorNuevo, productoNuevo);
                                    proveedores.Add(nombr3, distribuidors);
                                    GuardarDato3(distribuidors);
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine("Error al guardar: " + ex.Message);
                                }

                              
                                Console.ReadKey();
                                break;
                            case 2:
                                Console.Clear();

                                Console.WriteLine("---- Lista de Proveedors ---");

                                if (proveedores.Count == 0)
                                {
                                    Console.WriteLine("No hay provedores");
                                }
                                else
                                {
                                    foreach (var item in proveedores)
                                    {
                                        Console.WriteLine(item.Value.MostrarProovedor());
                                    }


                                }

                                Console.ReadKey();

                                break;
                            case 3:
                                Console.Clear();
                                Console.Write("Borrrar Proovedor: ");
                                do
                                {
                                    Console.Write("Ingrese el nombre: ");
                                    nombr3 = Console.ReadLine().Trim();
                                    if (nombr3 == "")
                                    {
                                        Console.WriteLine("Error ");
                                    }
                                }
                                while (nombr3 == "");

                                nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                                if (clientes.ContainsKey(nombr3))
                                {
                                    Console.WriteLine("Desea borrarlo: ");
                                    Console.WriteLine("Opcion 1: si");
                                    Console.WriteLine("Opcion 2: no ");
                                    opcion = int.Parse(Console.ReadLine());

                                    if (opcion == 1)
                                    {
                                        proveedores.Remove(nombr3);
                                        EliminarProveedor(nombr3);
                                        Console.WriteLine("Se ha eliminado");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Regresando...");
                                    }

                                }
                                Console.ReadKey();
                                break;
                            case 4:
                                do
                                {
                                    Console.Write("Ingrese el nombre: ");
                                    nombr3 = Console.ReadLine().Trim();
                                    if (nombr3 == "")
                                    {
                                        Console.WriteLine("Error ");
                                    }
                                }
                                while (nombr3 == "");

                                nombr3 = nombr3.Substring(0, 1).ToUpper() + nombr3.Substring(1).ToLower();

                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine(proveedores[nombr3].MostrarProovedor());
                                    Console.WriteLine("");
                                    Console.WriteLine("Que deseas editar");
                                    Console.WriteLine("Opcion 1: Distribuidor");
                                    Console.WriteLine("Opcion 2: Producto");
                                    Console.WriteLine("Opcion 3: Telefono");
                                    Console.WriteLine("Opcion 4: Terminar de editar");
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
                                                distribuidorNuevo = Console.ReadLine().Trim();
                                                if (distribuidorNuevo == "")
                                                {
                                                    Console.WriteLine("Error ");
                                                }
                                            }
                                            while (distribuidorNuevo == "");

                                            distribuidorNuevo = distribuidorNuevo.Substring(0, 1).ToUpper() + distribuidorNuevo.Substring(1).ToLower();

                                            proveedores[nombr3].Distribuidor = distribuidorNuevo;
                                            break;
                                        case 2:
                                            //-----------------------------------//
                                            do
                                            {
                                                productoNuevo = Console.ReadLine().Trim();
                                                if (productoNuevo == "")
                                                {
                                                    Console.WriteLine("Error ");
                                                }
                                            }
                                            while (productoNuevo == "");

                                            productoNuevo = productoNuevo.Substring(0, 1).ToUpper() + productoNuevo.Substring(1).ToLower();

                                            proveedores[nombr3].Producto = productoNuevo;
                                            //-----------------------------------//
                                            break;
                                        case 3:
                                            //-----------------------------------//
                                            do
                                            {
                                                Console.Write("Ingrese el telefono: ");
                                                telefono = Console.ReadLine().Trim();
                                                if (telefono.Length != 8)
                                                {
                                                    Console.WriteLine("datos insuficients del numero telefonico");
                                                }
                                            } while (telefono.Length != 8);
                                            clientes[nombr3].Telefono = telefono;

                                            //-----------------------------------//

                                            break;

                                        default:
                                            break;
                                    }

                                } while (opc != 4);

                                ModificarCliente(nombr3, clientes[nombr3]);

                                Console.ReadKey();


                                ///-------------------------------------------////
                                break;
                            default:
                                break;
                        }
                    } while (opcion != 6);
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
                            if (popular == null || item.Value.VendidosProd > popular.VendidosProd)
                            {
                                popular = item.Value;
                            }
                            /////reponer
                            if(item.Value.CantidadProd == 0)
                            {
                                reponer.Add(item.Value);
                            }
                            ////Vencimiento
                            vence = (item.Value.FechaProd - DateTime.Now).Days;

                            if(vence >= 0 && vence<=7)
                            {
                                cambio.Add(item.Value);
                            }

                            totalVentas += item.Value.VendidosProd * item.Value.PrecioProd;
                        }

                        if (popular != null)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("----Producto mas vendido----");
                            Console.WriteLine("| Articulo: " + popular.NombreProd + " | Unidade vendidas:  "+ popular.VendidosProd +" |");
                        }

                        if(reponer.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("|---Productos a reponer---");
                            foreach(Productos item in reponer)
                            {
                                Console.WriteLine("| Codigo: " + item.CodigoProd +" | Nombre: " + item.NombreProd + " | Sin existecias |");
                            }

                        }

                        if (cambio.Count > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("----Productos ha vencer---");
                            foreach (Productos item in cambio)
                            {
                                Console.WriteLine("| Codigo: " + item.CodigoProd + " | Nombre: " + item.NombreProd + " | fecha: "+ item.FechaProd.ToString("dd/MM/yyyy") + " |");
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
