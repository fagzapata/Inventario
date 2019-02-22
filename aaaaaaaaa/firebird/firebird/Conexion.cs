using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firebird
{
    class Conexion
    {
        
        public FbConnection fbconexion;
        public Conexion()
        {
            FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
            csb.UserID = "SYSDBA";
            csb.Password = "masterkey";

            csb.Database = @"C:\PREALUMNOS\FBN.FDB";
            fbconexion = null;
            fbconexion = new FbConnection(csb.ToString());
        }

        internal static void Open()
        {
            throw new NotImplementedException();
        }

        
        public void Abrir() // Metodo para Abrir la Conexion
        {
            fbconexion.Open();
        }

        internal static void Close()
        {
            throw new NotImplementedException();
        }

        public void Cerrar() // Metodo para Cerrar la Conexion
        {
            fbconexion.Close();
        }
        

    }
}
