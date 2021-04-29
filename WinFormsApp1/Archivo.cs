using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Take_Back
{
    class Archivo
    {
        
        public String rutaActual = "";
        //metodo que busca el archivo

        public Boolean BusquedaArch(String dir)
        {
            try
            {
                Directory.GetFiles(dir);
                if (dir[dir.Length - 1] != '\\')
                {
                    rutaActual = dir + "\\";
                }
                else
                {
                    rutaActual = dir;
                }
                Console.WriteLine("Versionamiento iniciado en: " + dir);
               


            }
            catch (System.Exception ex)
            {
                Console.WriteLine("No existe la ruta");
                return false;
            }
            return true;
            }
        public void Escriturarch(String Archivo, String dat)
        {
            
            try
            {
                //lee el archivo y lo sube a memoria
                StreamWriter fr = new StreamWriter(Archivo);
                // se asigna la informacion a variable

                fr.WriteLine(dat);
              
                fr.Close();

                Console.WriteLine("Resplado ejecutado");
            }
            catch (Exception e)
            {
                Console.WriteLine("Archivo no exite");
                Console.WriteLine("Expcion" + e.Message);
            }
           
        }
    }
}
