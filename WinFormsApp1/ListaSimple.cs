using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WinFormsApp1;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Take_Back
{
    class ListaSimple<T>
    {
        //Variable de tipo nodo
        Nodo<Version> primero;
        Nodo<Version> ultimo;
        Nodo<Version> actual;
        Nodo<Version> anterior;

        ABB abb = new ABB();
        int total { get; set; }

        //Constructor de clase
        public ListaSimple()
        {
            actual = ultimo = primero = anterior = null;
            total = 0;
        }
        //crea un nuevo nodo
        public void agregar(Version pDato)
        {
            //1. crear el nuevo nodo
            Nodo<Version> nuevo = new Nodo<Version>(pDato);
            //2. depende
            if (primero == null)//lista vacía
            {
                primero = nuevo;
                ultimo = nuevo;
                total = 1;
            }
            else //lista NO vacía
            {
                ultimo.siguiente = nuevo;
                ultimo = nuevo;
                total++;
            }

        }


        // buscar rama

      public ListaSimple<Version> buscarRama(ListaSimple<Version> act,String nombre)
        {
            try
            {

                actual = act.primero;
                while (actual != null)
                {
                    if (actual.dato.Branch != null)
                    {
                      
                        if (actual.dato.Branch.primero.dato.rama==nombre)
                        {
                            return actual.dato.Branch;
                        }else if (actual.dato.Branch!=null)
                        {
                           act= buscarRama(actual.dato.Branch, nombre);
                        }



                    }
                    try
                    {
                        if (actual!=null) {
                            actual = actual.siguiente;
                        }
                    }
                    catch
                    {
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("no se pudo");
            }


            return act;
        }

        // existe rama?

        public bool buscarRamaBoolean(ListaSimple<Version> act, String nombre, bool si)
        {
            try
            {

                actual = act.primero;
                while (actual != null)
                {
                    if (actual.dato.Branch != null)
                    {

                        if (actual.dato.Branch.primero.dato.rama == nombre)
                        {
                            return true;
                        }else if (actual.dato.Branch != null)
                        {
                            si = buscarRamaBoolean(actual.dato.Branch, nombre,si);
                        }

                    }
                    try
                    {
                        if (actual != null)
                        {
                            actual = actual.siguiente;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                  
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return si;
        }



        // iniciar rama


        public bool crearBranch(String nombre){

            try
            {
                if (ultimo!=null && nombre!="principal")
                {
                    if (ultimo.dato.Branch==null) {
                        ultimo.dato.Branch = new ListaSimple<Version>();
                        ultimo.dato.Branch.agregar(new Version(0, "version inicial de rama", ultimo.dato.ArbolBinario, null, nombre));
                        
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("el apuntador ya esta ocupado, debe crear otra version.");
                    }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
           

        }


        //devuelve el tamaño de la lista
        public int tamano()
        {
          
            return total;
        }

        public int tamanoUltimo()
        {

            return ultimo.dato.Id;
        }

        //muestra bitacora
        public void historial()
        {
            int conta = 0;
            Stack<Version> aux = new Stack<Version>();
            actual = primero;
            while (actual != null)
            {
                //Console.WriteLine(actual.dato + " -> ");
                aux.Push(actual.dato);
                actual = actual.siguiente;

                conta++;
            }

            for (int i = 0; i < conta; i++)
            {

                Console.WriteLine(aux.Pop());
            }

        }

                

        //busca el respaldo y ejecuta 
        public bool Resplado(int id, int idActual, String ruta)
        {

            bool si=false;
            // quitando archivos de la versionactual
            try
            {

                actual = primero;
                while (actual != null)
                {

                    anterior = actual;
                    if (idActual == actual.dato.Id)
                    {
                        // recorrer arbol y eliminar
                        
                        EliminarArchivosABB(actual.dato.ArbolBinario.root,ruta);
                        si = true;
                    }
                    actual = actual.siguiente;

                }
            }
            catch (Exception e)
            {
                return false;
            }

            // recuperando version solicitada

            try
            {

                actual = primero;
                while (actual != null)
                {

                    anterior = actual;
                    if (id == actual.dato.Id && si ==true)
                    {
                        // recorrer arbol y crear
                        CrearArchivosABB(actual.dato.ArbolBinario.root, ruta);

                    }
                    actual = actual.siguiente;

                }
            }
            catch (Exception e)
            {
                return false;
            }

            if(si == false)
            {
                return false;
            }

            return true;
        }



        // respaldo cambio de rama
        public bool RespladoRama(int idActual,ListaSimple<Version> act, ListaSimple<Version> n, String ruta)
        {

            bool si = false;
            // quitando archivos de la versionactual
            try
            {

                actual = act.primero;
                while (actual != null)
                {
                    
                    anterior = actual;
                    if (idActual == actual.dato.Id)
                    {
                        // recorrer arbol y eliminar

                       
                        EliminarArchivosABB(actual.dato.ArbolBinario.root, ruta);
                        si = true;
                    }
                    actual = actual.siguiente;

                }
            }
            catch (Exception e)
            {
                return false;
            }

            // recuperando version solicitada

            try
            {

                actual = n.primero;
                while (actual != null)
                {

                    anterior = actual;
                    if (n.NodoUltimo().dato.Id == actual.dato.Id && si == true)
                    {
                        // recorrer arbol y crear
                        CrearArchivosABB(actual.dato.ArbolBinario.root, ruta);

                    }
                    actual = actual.siguiente;

                }
            }
            catch (Exception e)
            {
                return false;
            }

            if (si == false)
            {
                return false;
            }

            return true;
        }

        // recursivo eliminar arbol
        public void CrearArchivosABB(NodoABB raiz, String ruta)
        {



            crearArchivo(ruta + raiz.nombre,raiz.contenido);

            if (raiz.nodoIzq != null)
                CrearArchivosABB(raiz.nodoIzq, ruta);
            if (raiz.nodoDer != null)
                CrearArchivosABB(raiz.nodoDer, ruta);

        }

        // metodo para crear archivos
        public void crearArchivo(String ruta, String contenido)
        {

            string path = @ruta;

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(contenido);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
                  
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

           

        }

        // recursivo eliminar arbol
        public void EliminarArchivosABB(NodoABB raiz, String ruta)
        {

           

            eliminarArchivo(ruta+raiz.nombre);

            if (raiz.nodoIzq != null)
                EliminarArchivosABB(raiz.nodoIzq,ruta);
            if (raiz.nodoDer != null)
                EliminarArchivosABB(raiz.nodoDer,ruta);

        }

        // metodo para eliminar archivos

        public bool eliminarArchivo(string rutaA)
        {

            string ruta = @rutaA;
            try
            {
                File.Delete(ruta);
                if (File.Exists(ruta))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }

        }

        //busca y compara retorna nodo actual
        public Nodo<Version> NodoActual()
        {
         

            return primero;

        }


        //retorna ultima version
        public Nodo<Version> NodoUltimo()
        {


            return ultimo;

        }

        //busca y compara le ultimo nodo
        public Version recorrer()
        {
            try
            {
                
                actual = primero;
                while (actual != null)
                {
                   
                    anterior = actual;
                    Console.Write(actual.dato + " -> ");
                    actual = actual.siguiente;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return anterior.dato;

        }



        public Nodo<Version> NodoActual(int vA)
        {
            try
            {

                actual = primero;
                while (actual != null)
                {

                    anterior = actual;
                    if (vA==actual.dato.Id)
                    {
                        return actual;
                    }
                    actual = actual.siguiente;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;

        }
        //graficar
        public void graficar(int vA)

         {
            Nodo<Version> nodoActual = NodoActual(vA);
            if (nodoActual != null)
            {
                String codDot = "";
                String declaracion = "";
                String enlaces = "";
                String rank = "{rank = same;";

                codDot += "digraph{";
                codDot += "node[shape=box, color=black];\n";
                codDot += "label=\"Version " + vA + " Coment: " + nodoActual.dato.Comentario + " Date: " + nodoActual.dato.fechas + "\";\n";
                codDot += "rankdir = TB;\n";
                codDot += "\n";




                codDot += nodoActual.dato.ArbolBinario.graficar(actual.dato.Id);


                rank += "}";
                codDot += declaracion + enlaces + rank;

                codDot += "\n";
                codDot += "}";
                TextWriter text;
                text = new StreamWriter("abb.txt");
                string escribir;
                escribir = codDot;
                text.WriteLine(escribir);
                text.Close();

                //Generar imagen             
                abb.Generate_Graph("abb.txt", "");



                // abriendo Form




                Application.Run(new Form1());


                string ruta = @"abb.png";
                try
                {
                    File.Delete(ruta);
                    if (File.Exists(ruta))
                    {
                        Console.WriteLine("El archivo sigue existiendo.");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al borrar archivo: {0}", e.ToString());
                }

            }
            else
            {
                Console.WriteLine("Id invalido");
            }


        }
















        //elimina el nodo de la lista enlazada

        public void Eliminar(Func<Version, Version, bool> p,
                                    Version elementoaeliminar)
        {
            bool encontrado = false;
            actual = primero;
            if (primero != null)
            {
                while (actual != null && encontrado != true)
                {
                    if (p(actual.dato, elementoaeliminar))
                    { //elimina si el nodo es cabeza
                        if (actual == primero)
                        {
                            primero = primero.siguiente;
                        }
                        //elimina si el nodo es cola 
                        else if (actual == ultimo)
                        {
                            anterior.siguiente = null;
                            ultimo = anterior;
                        }
                        //elimina si el nodo es cualquiera de la lista
                        else
                        {
                            anterior.siguiente = actual.siguiente;

                        }
                        Console.WriteLine("Nodo eliminado");
                        encontrado = true;


                    }
                    anterior = actual;
                    actual = actual.siguiente;
                }
                if (!encontrado)
                {
                    Console.WriteLine("Elemento no encontrado");
                }

            }
            else
            {
                Console.WriteLine("Lista Vacia");
            }
        }


        // imprimir arbol
        public void imprimirArbol(int vA)
        {
            try
            {

                actual = primero;
                while (actual != null)
                {
                    if (actual.dato.Id == vA)
                    {

                        // imprimir listado de archivos
                        actual.dato.ArbolBinario.PreOrden(actual.dato.ArbolBinario.root);



                    }
                    actual = actual.siguiente;
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        //busca y compara le ultimo nodo
        public bool eliminarRM(int vA,string archivo, String ruta)
        {
            try
            {

                actual = primero;
                while (actual != null)
                {
                    if (actual.dato.Id == vA)
                    {



                        //recorrer arbol y eliminar
                        actual.dato.ArbolBinario = actual.dato.ArbolBinario.eliminarDato(actual.dato.ArbolBinario, archivo, ruta);




                        return true;

                    }
                    actual = actual.siguiente;
                }
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;

        }


    }

}
