using System;
using System.Collections.Generic;
using System.Text;

namespace Take_Back
{
    class Nodo<Version>
    {

        public Version dato { get; set; }
        public Nodo<Version> siguiente { get; set; }
   

        public Nodo(Version pDato)
        {
            this.dato = pDato;
            this.siguiente = null;
            
        }
    }
}
