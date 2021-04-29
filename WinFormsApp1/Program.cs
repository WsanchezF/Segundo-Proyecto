using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;
using WinFormsApp1;

namespace Take_Back
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AsignarConsola();
                      


            //almacena la informacion
            String aux = "";
            String aux1 = "";
            int menuvar = 1;
            //se Envia mensaje a usuario 


            Modelo precision = new Modelo();
            //Menu interactivo
            do
            {
                Console.Write("Take_Back  C:/");
                String Funcion = Console.ReadLine();
                string[] subs = Funcion.Split(' ');
                string parametro = "";
                for (int i=1; i< subs.Length; i++)
                {
                   
                    parametro += subs[i];
                    if (i == subs.Length-1)
                    {

                    }
                    else
                    {
                        parametro += ' ';
                    }

                }
                Console.WriteLine(parametro); 
                //validando ingreso de comandos
                switch (subs[0])
                {
                    //comando init busca la locacion del archivo y sube a memoria su informacion
                    case "init":
                        if (parametro != "")
                        {
                            precision.init<Version>(parametro);
                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }
                        break;

                    case "save":

                        if (parametro != "")
                        {
                            precision.save(aux, parametro);
                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }
                        break;

                    case "compass": precision.compass(); break;

                    case "watch":

                        if (parametro != "")
                        {
                            if (precision.compass())
                        {
                           
                           
                            int a = 0;
                            a = Int32.Parse(parametro);
                            precision.watch(a);
                        }

                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }
                        break;
                    case "rm":
                        if (parametro != "")
                        {
                            if (precision.comprobar())
                        {
                            precision.imprimirArbol(precision.versionActual());
                            
                           
                            precision.rm(parametro);
                        }

                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }

                        break;
                    case "view":

                        if (parametro != "")
                        {
                            if (precision.compass())
                        {
                          
                            int a = 0;
                            a = Int32.Parse(parametro);
                            precision.view(a);
                        }

                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }

                        break;


                    case "branch":



                      

                            if (parametro!="") {
                                // crear rama
                                precision.branch(parametro);
                            }
                            else{
                           
                                // ver rama actual
                                precision.viewBranch();
                            }

                       
                      

                        break;

                    case "change":


                        if (parametro != "")
                        {

                            if (parametro != "")
                        {
                            // crear rama
                            precision.change(parametro);
                        }
                        else
                        {

                            Console.WriteLine("Ingrese un nombre.");
                        }

                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }

                        break;

                    case "delete":
                        if (parametro != "")
                        {
                            Console.Write("C:Take_Back delete "); 
                        int r = 0; 
                        precision.Delete(r = Int32.Parse(parametro));
                        }
                        else
                        {
                            Console.WriteLine("falta parametro");
                        }

                        break;
                    case "help":
                        Console.WriteLine("Comandos A utilizar");
                        Console.WriteLine("init: Buscamos archivo para verificar informacion \n");
                        Console.WriteLine("save: creamos un respaldo en el sistema\n");
                        Console.WriteLine("compass: bitacora del sistema\n");
                        Console.WriteLine("rm: eliminar archivo\n");
                        Console.WriteLine("branch (con parametro): crear rama\n");
                        Console.WriteLine("branch (sin parametro): ver ramas\n");
                        Console.WriteLine("change: cambiar de rama\n");
                        Console.WriteLine("view: ver arvol de una version\n");
                        Console.WriteLine("watch: repuera cualquier version que exista en nuestro sistema\n");
                        Console.WriteLine("delete: elimina cualquier version que exista en nuestro sistema\n");
                        break;
                    case "exit":
                        Console.WriteLine("Salir Si / No");
                    
                        if (parametro == "si")
                        {
                            menuvar = 0;
                        }

                        break;
                    default: Console.WriteLine("elija una opcion"); break;

                }
                Console.WriteLine("");
            } while (menuvar != 0);

            /*

            ABB arbol = new ABB();//Declaramos una instancia del arbol binario de busqueda

            //agregamos algunos valores
            arbol.insert(1);
            arbol.insert(-1);
            arbol.insert(5);
            arbol.insert(-8);
            arbol.insert(0);
            arbol.insert(3);
            arbol.insert(8);
            arbol.insert(10);
            arbol.insert(15);

            //imprimir en consola en EnOrden
            Console.WriteLine("Recorrido EnOrden");
            arbol.EnOrden(arbol.root);
            Console.WriteLine("");

            //imprimir en consola en PosOrden 
            Console.WriteLine("Recorrido PosOrden");
            arbol.PostOrden(arbol.root);
            Console.WriteLine("");

            //imprimir en consola en PreOrden 
            Console.WriteLine("Recorrido PreOrden");
            arbol.PreOrden(arbol.root);
            Console.WriteLine("");

            arbol.graficar();
            */

        }

        public static void DirectorySearch(string dir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    Console.WriteLine(Path.GetFileName(f));
                }
                foreach (string d in Directory.GetDirectories(dir))
                {
                    Console.WriteLine(Path.GetFileName(d));
                    DirectorySearch(d);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("No existe la ruta");
            }
        }
        public static int AsignarConsola()
        {
            return AllocConsole() ? 0 : Marshal.GetLastWin32Error();
        }

        public static int LiberarConsola()
        {
            return FreeConsole() ? 0 : Marshal.GetLastWin32Error();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
          "Microsoft.Security",
          "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"),
          SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
          "Microsoft.Security",
          "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"),
          SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();
    }
}
