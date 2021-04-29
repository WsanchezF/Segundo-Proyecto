using System;
using System.IO;


namespace Take_Back
{
    class Modelo
    {
       
        Version univer = new Version();
       Archivo cradle = new Archivo();
         ListaSimple<Version> nuevo;
        public String ramaActual="";
        
        // se modela las acciones de comandos 

        /*metodo watch consulta cualquiera de las versiones que se tenga en el sistema recibe un argumento 
         de tipo int para validadr ubicacion en lista*/
        public void watch(int id) 
        {
            if (ramaActual == "principal")
            {
                bool si = nuevo.Resplado(id, univer.versionActual, cradle.rutaActual);
                if (si)
                {
                    univer.versionActual = id;
                    Console.WriteLine("Version actualizada.");
                }
                else
                {
                    Console.WriteLine("No se realizo correcatamente.");
                }
            }
            else
            {
                //otra rama

                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);

                bool si = aux.Resplado(id, univer.versionActual, cradle.rutaActual);
                if (si)
                {
                    univer.versionActual = id;
                    Console.WriteLine("Version actualizada.");
                }
                else
                {
                    Console.WriteLine("No se realizo correcatamente.");
                }
            }
           
        }

        // change
        public void change(string nombre)
        {
            if (nombre == "principal" && ramaActual =="principal")
            {
                Console.WriteLine("Se encuentra en la rama principal"); 

            }
            else
            {
                if (nombre == "principal")
                {
                    ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                    ramaActual = nombre;
                    nuevo.RespladoRama(univer.versionActual,aux,nuevo,cradle.rutaActual);
                    univer.versionActual = nuevo.NodoUltimo().dato.Id;
                    Console.WriteLine("Rama actualizada");

                }
                else
                {
                    if (ramaActual=="principal")
                    {
                        if (nuevo.buscarRamaBoolean(nuevo, nombre, false))
                        {
                            ListaSimple<Version> aux = nuevo.buscarRama(nuevo, nombre);
                        ramaActual = nombre;
                        nuevo.RespladoRama(univer.versionActual, nuevo, aux, cradle.rutaActual);
                        univer.versionActual = aux.NodoUltimo().dato.Id;
                        Console.WriteLine("Rama actualizada");
                        }
                        else
                        {
                            Console.WriteLine("No existe rama.");
                        }
                    }
                    else {
                        if (nuevo.buscarRamaBoolean(nuevo, nombre, false)) {

                            ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                            ListaSimple<Version> aux1 = nuevo.buscarRama(nuevo, nombre);

                            ramaActual = nombre;
                            // ....................................
                            aux1.RespladoRama(univer.versionActual, aux, aux1, cradle.rutaActual);
                            univer.versionActual = aux1.NodoUltimo().dato.Id;
                            Console.WriteLine("Rama actualizada");
                        }
                        else
                        {
                            Console.WriteLine("No existe rama.");
                        }
                    }
                }


                }
            }

        

        //metodo rm
        public bool comprobar()
        {
            if (ramaActual == "principal")
            {
                if (nuevo != null)
                {

                    return true;
                }

                return false;
            }
            else
            {
                // otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                if (aux != null)
                {

                    return true;
                }

               
                return false;
            }
        }

            //metodo rm
            public void rm(String nombre)
        {
            if (ramaActual == "principal")
            {


                if (nuevo.eliminarRM(univer.versionActual, nombre, cradle.rutaActual))
                {

                }
                else
                {
                    Console.WriteLine("No fue posible");
                }

            }
            else
            {
                //otra rama

                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);

                if (aux.eliminarRM(univer.versionActual, nombre, cradle.rutaActual))
                {

                }
                else
                {
                    Console.WriteLine("No fue posible");
                }
            }

        }

        // metodo para crear ramas
        public void branch(string nombre)
        {
            if (ramaActual == "principal")
            {
                if (nuevo != null)
                {
                    if (nuevo.crearBranch(nombre))
                    {
                        ramaActual = nombre;
                        univer.versionActual = 0;
                        Console.WriteLine("Rama creada.");
                    }
                }
                else
                {

                    Console.WriteLine("No ha iniciado el versionamiento\n");

                }

            }
            else
            {
                //otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                if (aux != null)
                {
                    if (aux.crearBranch(nombre))
                    {
                        ramaActual = nombre;
                        univer.versionActual = 0;
                        Console.WriteLine("Rama creada.");
                    }
                }
                else
                {

                    Console.WriteLine("No ha iniciado el versionamiento\n");

                }
            }
          
        }

        // metodo para ver rama actual
        public void viewBranch()
        {

            if (ramaActual!="") {
                Console.WriteLine(ramaActual);
            }
            else
            {
                Console.WriteLine("No ha iniciado el versionamiento\n");
            }
        }


        //metodo compass consulta una bitacora de todos las versiones
        public bool compass()
        {

            if (ramaActual == "principal")
            {

                if (nuevo != null)
                {
                    Console.WriteLine("ID" + "   " + "Fecha" + "                   " + " Comentario");
                    Console.WriteLine("---------------------------------------------------------------------");
                    nuevo.historial();
                    return true;
                }
                else
                {
                    Console.WriteLine("No ha iniciado el versionamiento\n");
                }

                return false;
            }
            else
            {
                //otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                if (aux != null)
                {
                    Console.WriteLine("ID" + "   " + "Fecha" + "                   " + " Comentario");
                    Console.WriteLine("---------------------------------------------------------------------");
                    aux.historial();
                    return true;
                }
                else
                {
                    Console.WriteLine("No ha iniciado el versionamiento\n");
                }

                return false;
            }
        }

        /*metodo save verifica si el documento a sufrido algun tipo de cambio (valida, compara)
         recibe como parametro el comentario de modificacion */
        public void save(String datos, String Coment)
        {

            if (ramaActual == "principal" || ramaActual=="")
            {
                if (nuevo != null)
                {
                    Nodo<Version> a = nuevo.NodoUltimo();
                    bool n = true;

                    if (a != null)
                    {
                        if (nuevo.NodoUltimo().dato.Id == univer.versionActual)
                        {
                            n = true;
                        }
                        else
                        {
                            n = false;
                        }
                    }

                    if (n)
                    {

                        if (cradle.rutaActual == "")
                        {
                            Console.WriteLine("No ha iniciado el versionamiento\n");
                        }
                        else
                        {
                            int size = nuevo.tamano();

                            if (size == 0)
                            {
                                // insertar valores al alrbol binario
                                ramaActual = "principal";
                               
                                Version Crear = new Version(size, Coment, insertABB(cradle.rutaActual), null, "principal");
                                nuevo.agregar(Crear);

                                Console.WriteLine("Version creada");
                                univer.versionActual = 0;

                            }
                            else
                            {
                                // comparacion de versiones

                                ComparacionVersiones(univer.versionActual, nuevo.NodoActual(), Coment, datos);


                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Para registrar una nuva version debe moverse a la ultima.");
                    }
                }
                else
                {
                    Console.WriteLine("No ha iniciado el versionamiento\n");
                }
            }
            else
            {
                // cuando estoy en otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);

                if (aux != null)
                {
                    Nodo<Version> a = aux.NodoUltimo();
                    bool n = true;

                    if (a != null)
                    {
                        if (aux.NodoUltimo().dato.Id == univer.versionActual)
                        {
                            n = true;
                        }
                        else
                        {
                            n = false;
                        }
                    }

                    if (n)
                    {

                        if (cradle.rutaActual == "")
                        {
                            Console.WriteLine("No ha iniciado el versionamiento\n");
                        }
                        else
                        {
                            int size = aux.tamano();

                            if (size == 0)
                            {
                                // insertar valores al alrbol binario
                                ramaActual = "principal";
                                Version Crear = new Version(size, Coment, insertABB(cradle.rutaActual), null, "principal");
                                aux.agregar(Crear);

                                Console.WriteLine("Version creada");
                                univer.versionActual = 0;

                            }
                            else
                            {
                                // comparacion de versiones

                                ComparacionVersiones(univer.versionActual, aux.NodoActual(), Coment, datos);


                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Para registrar una nuva version debe moverse a la ultima.");
                    }
                }
                else
                {
                    Console.WriteLine("No ha iniciado el versionamiento\n");
                }
            }
        
        }

        // comando view
        public void view(int id)
        {
            if (ramaActual == "principal")
            {
                nuevo.graficar(id);

            }
            else
            {
                // otra rama
                ListaSimple<Version> aux= nuevo.buscarRama(nuevo, ramaActual);
                aux.graficar(id);
            }
            
        }

        //recorre y comprar
        public Boolean ComparacionVersiones(int idActual, Nodo<Version> actual, String Coment,String datos)
        {
            if (ramaActual == "principal")
            {
                Nodo<Version> primero = actual;
                bool si = false;
                try
                {

                    actual = primero;
                    while (actual != null && si == false)
                    {

                        if (idActual == actual.dato.Id)
                        {



                            ABB temp = insertABB(cradle.rutaActual);
                            VersionNuevaArchivos(actual.dato.ArbolBinario.root, temp.root);
                            if (cambia2)
                            {
                                int id = nuevo.tamano();

                                Version Crear = new Version(id, Coment, temp, null, ramaActual);
                                nuevo.agregar(Crear);

                                Console.WriteLine("Version creada");
                                univer.versionActual = id;


                                cambia2 = false;
                                si = true;
                            }
                            else
                            {
                                cambia2 = false;
                                si = true;
                                VersionNuevaArchivos(temp.root, actual.dato.ArbolBinario.root);
                                if (cambia2)
                                {
                                    int id = nuevo.tamano();

                                    Version Crear = new Version(id, Coment, temp, null, ramaActual);
                                    nuevo.agregar(Crear);

                                    Console.WriteLine("Version creada");
                                    univer.versionActual = id;

                                    cambia2 = false;
                                    si = true;
                                }
                                else
                                {
                                    Console.WriteLine("La ultima version se encuentra actualizada.");
                                    si = true;
                                }
                            }


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
            else
            {
                //otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                Nodo<Version> primero = actual;
                bool si = false;
                try
                {

                    actual = primero;
                    while (actual != null && si == false)
                    {

                        if (idActual == actual.dato.Id)
                        {



                            ABB temp = insertABB(cradle.rutaActual);
                            VersionNuevaArchivos(actual.dato.ArbolBinario.root, temp.root);
                            if (cambia2)
                            {
                                int id = aux.tamano();

                                Version Crear = new Version(id, Coment, temp, null, ramaActual);
                                aux.agregar(Crear);

                                Console.WriteLine("Version creada");
                                univer.versionActual = id;


                                cambia2 = false;
                                si = true;
                            }
                            else
                            {
                                cambia2 = false;
                                si = true;
                                VersionNuevaArchivos(temp.root, actual.dato.ArbolBinario.root);
                                if (cambia2)
                                {
                                    int id = aux.tamano();

                                    Version Crear = new Version(id, Coment, temp, null, ramaActual);
                                    aux.agregar(Crear);

                                    Console.WriteLine("Version creada");
                                    univer.versionActual = id;

                                    cambia2 = false;
                                    si = true;
                                }
                                else
                                {
                                    Console.WriteLine("La ultima version se encuentra actualizada.");
                                    si = true;
                                }
                            }


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

        // variables auxiliares

        Boolean cambia = false;
        Boolean cambia2 = false;
        Boolean nuevoArchivo = true;
        
        // metodo para comparar version de archivos
        public void VersionActualArchivos(NodoABB raiz, String contenido, String nombre)
        {
          

            if (raiz.nombre == nombre )
            {

                nuevoArchivo = false;

                if (raiz.contenido.Equals(contenido))
                {

                                     
                  
                }
                else
                {
                    cambia = true;
                  
                }
            }
            if (raiz.nodoIzq != null)
                VersionActualArchivos(raiz.nodoIzq, contenido, nombre);
            if (raiz.nodoDer != null)
                VersionActualArchivos(raiz.nodoDer, contenido, nombre);


           
        }

        // metodo para comparar version de archivos
        public void VersionNuevaArchivos(NodoABB raiz, NodoABB raizNueva)
        {

            VersionActualArchivos(raizNueva, raiz.contenido, raiz.nombre);
            if (cambia || nuevoArchivo)
            {
                Console.WriteLine(raiz.nombre + " cambio");
                cambia = false;
                cambia2 = true;

            }
            nuevoArchivo = true;

            if (raiz.nodoIzq != null)
                VersionNuevaArchivos(raiz.nodoIzq, raizNueva);
            if (raiz.nodoDer != null)
                VersionNuevaArchivos(raiz.nodoDer,raizNueva);

           
        }


        // metodo para insrtar datos en el arbol binario
        public ABB insertABB(string dir)
        {

            ABB nuevoArbol = new ABB();
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    int valor = 0;
                    for(int i =0; i< Path.GetFileName(f).Length; i++)
                    {
                        char a = Path.GetFileName(f)[i];
                        valor += (int)a;
                    }
                   
                    string text = System.IO.File.ReadAllText(@""+dir+Path.GetFileName(f));

                    System.IO.FileInfo info = new System.IO.FileInfo(dir + Path.GetFileName(f));
                    int tamano = (int)info.Length;
                    nuevoArbol.insert(valor,text, Path.GetFileName(f), tamano);
                }
                foreach (string d in Directory.GetDirectories(dir))
                {
                    int valor = 0;
                    for (int i = 0; i < Path.GetFileName(d).Length; i++)
                    {
                        char a = Path.GetFileName(d)[i];
                        valor += (int)a;
                    }
                  
                    string text = System.IO.File.ReadAllText(@"" + dir + Path.GetFileName(d));

                    System.IO.FileInfo info = new System.IO.FileInfo(dir + Path.GetFileName(d));
                    int tamano = (int)info.Length;
                    nuevoArbol.insert(valor, text, Path.GetFileName(d), tamano);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("No existe la ruta");
                return null;
            }
            return nuevoArbol;
        }
        /*metodo Init inicia el programa validando ubicacion y archivo 
         recibe como parametro la direccion de archivo nota: falta validad nombre de archivo*/
        public void init<T>(String Direccion)
        {

           

                //aqui se siente que comparar la informacion del archivo
                if (Direccion + "\\" == cradle.rutaActual)
                {
                    Console.WriteLine("Ya se ha iniciado con anterioridad");
                }
                else
                {
                    if (cradle.BusquedaArch(Direccion))
                    {
                        nuevo = new ListaSimple<Version>();

                    }
                }
            
            

        }


        // returnar version
        public int versionActual()
        {
            return univer.versionActual;
        }


        // imprimir arbol
        public void imprimirArbol(int vA)
        {
            if (ramaActual == "principal")
            {
                nuevo.imprimirArbol(vA);
            }
            else
            {
                //otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                aux.imprimirArbol(vA);
            }
        }




        /* metodo Delete encargado de eliminar el nodo y de mantener la estructura de datos 
         recibe como parametro la ubicacion el estructura la posicion a eliminar */
        public void Delete(int Ubicacion)
        {
            if (ramaActual == "principal")
            {
                nuevo.Eliminar(Version.Validar, new Version { Id = Ubicacion });
                univer.versionActual = nuevo.NodoUltimo().dato.Id;
                watch(nuevo.NodoUltimo().dato.Id);
            }
            else
            {
                //otra rama
                ListaSimple<Version> aux = nuevo.buscarRama(nuevo, ramaActual);
                aux.Eliminar(Version.Validar, new Version { Id = Ubicacion });
                univer.versionActual = aux.NodoUltimo().dato.Id;
                watch(aux.NodoUltimo().dato.Id);
            }
        }
       
    }
}
