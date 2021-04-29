using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Take_Back
{
    // ABB: Arbol Binario de Busqueda
    class ABB
    {


        private string code_graph = "";
        public NodoABB root;

        public ABB()
        {
            root = null;
        }
        public void insert(int id, String contenido, String nombre,int size)
        {
            NodoABB nuevo = new NodoABB();
            nuevo.id = id;
            nuevo.size = size;
            nuevo.contenido = contenido;
            nuevo.nombre = nombre;
            nuevo.nodoDer = null;
            nuevo.nodoIzq = null;

            if (root == null)
            {
                root = nuevo;
            }
            else
            {
                NodoABB anterior = null, recorrer;
                recorrer = root;
                while (recorrer != null)
                {
                    anterior = recorrer;
                    if (id > recorrer.id)
                    {
                        recorrer = recorrer.nodoDer;
                    }
                    else
                    {
                        recorrer = recorrer.nodoIzq;
                    }
                }
                if (id > anterior.id)
                {
                    anterior.nodoDer = nuevo;
                }
                else
                {
                    anterior.nodoIzq = nuevo;
                }
            }
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
                    for (int i = 0; i < Path.GetFileName(f).Length; i++)
                    {
                        char a = Path.GetFileName(f)[i];
                        valor += (int)a;
                    }
                    Console.WriteLine(Path.GetFileName(f) + " agregado");
                    string text = System.IO.File.ReadAllText(@"" + dir + Path.GetFileName(f));

                    System.IO.FileInfo info = new System.IO.FileInfo(dir + Path.GetFileName(f));
                    int tamano = (int)info.Length;
                    nuevoArbol.insert(valor, text, Path.GetFileName(f), tamano);
                }
                foreach (string d in Directory.GetDirectories(dir))
                {
                    int valor = 0;
                    for (int i = 0; i < Path.GetFileName(d).Length; i++)
                    {
                        char a = Path.GetFileName(d)[i];
                        valor += (int)a;
                    }
                    Console.WriteLine(Path.GetFileName(d) + " agregado");
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
        //Preorden para comparar archivos
        public void PreOrdenArchivos(NodoABB raiz)
        {
            // comparar

            Console.Write(" * " + raiz.id + " * ");
            if (raiz.nodoIzq != null)
                PreOrden(raiz.nodoIzq);
            if (raiz.nodoDer != null)
                PreOrden(raiz.nodoDer);

        }

        //Preorden
        public void PreOrden(NodoABB raiz)
        {

            Console.Write(raiz.nombre + "\n");
            if (raiz.nodoIzq != null)
                PreOrden(raiz.nodoIzq);
            if (raiz.nodoDer != null)
                PreOrden(raiz.nodoDer);

        }

        // eliminar un nodo del arbol

        public ABB eliminarDato(ABB abb, String archivo,String ruta)
        {
            ABB aux = new ABB();
            return PreOrdenInsert(abb.root,aux,archivo,ruta);
        }
        // preorden insertar

        public ABB PreOrdenInsert(NodoABB raiz, ABB a, String archivo,String ruta)
        {

            if (archivo!= raiz.nombre)
            {
                a.insert(raiz.id,raiz.contenido,raiz.nombre,raiz.size);
            }
            else if(archivo == raiz.nombre)
            {

               if(eliminarArchivo(ruta + archivo))
                {
                    Console.WriteLine("Archivo eliminado");
                }
                else
                {
                    Console.WriteLine("Archivo no eliminado");
                }
            }
            if (raiz.nodoIzq != null)
               a= PreOrdenInsert(raiz.nodoIzq,a,archivo,ruta);
            if (raiz.nodoDer != null)
               a= PreOrdenInsert(raiz.nodoDer,a,archivo,ruta);

            return a;
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

        //InOrden
        public void EnOrden(NodoABB raiz)
        {
            if (raiz.nodoIzq!= null)
                EnOrden(raiz.nodoIzq);
            Console.Write(" * " + raiz.id + " * ");
            if (raiz.nodoDer != null)
                EnOrden(raiz.nodoDer);
        }

        //PostOrden
        public void PostOrden(NodoABB raiz)
        {
            if (raiz.nodoIzq != null)
                PostOrden(raiz.nodoIzq);
            if (raiz.nodoDer != null)
                PostOrden(raiz.nodoDer);
            Console.Write(" * " + raiz.id + " * ");
        }



        public String graficar(int vA)
        {
            return obtenernodos(vA);
           
        }

        private string obtenernodos(int vA)
        {

            declarar(root, vA);
            agregarmasnodos(root,vA);//METODO QUE AGREGA EL CUERPO DEL ARCHIVO .txt QUE SE VA A GENERAR
            string aux = code_graph;
            code_graph = "";

            return aux;
        }


        // declarar nodos

        public void declarar(NodoABB raiz,int vA)
        {

            code_graph += "nodo"+raiz.nombre.Replace('.', '_').Replace(' ', '_') + vA+"[label=\"" + raiz.nombre + "\"];\n";
            if (raiz.nodoIzq != null)
                declarar(raiz.nodoIzq,vA);
            if (raiz.nodoDer != null)
                declarar(raiz.nodoDer,vA);

        }
        private void agregarmasnodos(NodoABB raiz, int vA)
        {



            if (raiz != null)
            {
                code_graph += "\n";
                if (raiz.nodoIzq != null)
                {
                    agregarmasnodos(raiz.nodoIzq,vA);
                    code_graph += ("nodo"+raiz.nombre.Replace('.','_').Replace(' ', '_') + vA + "->nodo" + raiz.nodoIzq.nombre.Replace('.', '_').Replace(' ', '_') + vA +";");
                    code_graph += "\n";
                }
                if (raiz.nodoDer != null)
                {
                    agregarmasnodos(raiz.nodoDer,vA);
                    code_graph += ("nodo" + raiz.nombre.Replace('.', '_').Replace(' ', '_') + vA + "->nodo" + raiz.nodoDer.nombre.Replace('.', '_').Replace(' ', '_') + vA + ";");
                    code_graph += "\n";
                }
            }
        }

        public  void Generate_Graph(string fileName, string path)
        {
            try
            {
                var command = string.Format("dot -Tpng {0} -o {1}", Path.Combine(path, fileName), Path.Combine(path, fileName.Replace(".txt", ".png")));
                var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/C " + command);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception x)
            {
                Console.WriteLine(x.ToString());
            }
        }



    }
}
