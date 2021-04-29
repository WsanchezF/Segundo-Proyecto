using System;
using System.Collections.Generic;
using System.Text;

namespace Take_Back
{
    class Version
    {
        //para manejar version actual
        public int versionActual = 0;

        //apuntador al arbol binario

        public ABB ArbolBinario { get; set; }

        
        // apuntador a otra lista simple

        public ListaSimple<Version> Branch { get; set; }

        //Se crea los atributos de la clase
        public int Id { get; set; }
        public String Comentario { get; set; }
       
        //fecha
        public DateTime fechas ;

        //rama
        public String rama; 

        // se crea el constructor de la clase
        public  Version()
        {

        }
        public  Version(int id,String Comentario, ABB abb, ListaSimple<Version> Branch,String rama)
        {
            this.Id = id;
            this.Comentario = Comentario;
            this.Branch = Branch;
            this.rama = rama;
            this.fechas = DateTime.Now;// valir si funciona
            this.ArbolBinario = abb;
            this.Branch = null;
        }
        public override string ToString()
        {
            return  Id +"    "+ fechas +"     " + Comentario ;
        }


     public static bool Validar(Version v1,Version v2)
        {
            if (v1.Id == v2.Id)
            {
                return true;
            }
            return false;
        }


    }
}
