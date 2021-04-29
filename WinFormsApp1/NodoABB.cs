using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Take_Back
{
    class NodoABB
    {

        //hijo izquierdo
        public NodoABB nodoIzq;

        //hijo derecho
        public NodoABB nodoDer;

        public int id;

        //datos
        public string contenido;
        public int size;
        public String nombre;


        public NodoABB()
        {
            nodoIzq = null;
            nodoDer = null;
            id = size = 0;
            contenido = nombre = "";
        }


    }
}
