using System.Security.AccessControl;

class uno
{
    private int codigo;
    private string nombre;
    private DateTime fecha;
    private double precio;
    private int cantidad;

    private string estado; 
    private double valor;
    private string encargo;



}

class dos : uno
{

}

class tres : uno
{

}

class Programa
{
    static void Main()
    { 

       Dictionary <int, dos> dato = new Dictionary <int, dos> ();
         

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
                    Console.WriteLine("");

                    break;
                    case 2:
                    Console.WriteLine("");


                    break;
                    case 3:
                    Console.WriteLine("");


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