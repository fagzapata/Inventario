using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace firebird
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }


        //BOTON GUARDAR
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion con = new Conexion();
                con.Abrir();
                //Consultando el ultimo registro
                string consultaid = "SELECT MAX(ID)FROM INTERESADOS";
                FbCommand comandoid = new FbCommand(consultaid, con.fbconexion);
                comandoid.ExecuteNonQuery();
                string resultado = "-1";
                resultado = comandoid.ExecuteScalar().ToString();
                con.Cerrar();
                //MessageBox.Show("resultado " + resultado);
                //Estableciendo en la etiqueta el ultimo registro de la consulta
                if (resultado == DBNull.Value.ToString())
                {
                    //MessageBox.Show("es nulo");

                    tbid.Text = "1";
                }
                else
                {
                  //  MessageBox.Show("no es nulo ");

                    tbid.Text = (Convert.ToInt32(resultado) + 1).ToString();
                    //string status;

                }

                

                con.Abrir();


                string GUARDAR = "INSERT INTO INTERESADOS  (ID,NOMBRE,ESCUELA_P,EMAIL) VALUES ('" + tbid.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                FbCommand comando = new FbCommand(GUARDAR, con.fbconexion);
                comando.ExecuteNonQuery();
                con.Cerrar();
                actualizar();
                limpiar();
                MessageBox.Show("Los Datos Fueron Guardados");
               
                
            }
            catch (Exception er)
            {

                MessageBox.Show("Error al Guardar" + er.Message+" "+er.ToString());
            }


        }
        public void actualizar()
        {
            try
            {

                DataTable dt1 = (DataTable)dgv1.DataSource;
                dt1.Clear();
                Conexion con = new Conexion();
                con.Abrir();
                string carga = "SELECT * FROM INTERESADOS";
                FbCommand comando = new FbCommand(carga, con.fbconexion);
                comando.ExecuteNonQuery();
                FbDataAdapter da = new FbDataAdapter(comando);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv1.DataSource = dt;
                con.Cerrar();

            }
            catch (Exception er)

            {

                MessageBox.Show("Error" + er.Message);
            }
        }

        public void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            tbid.Text = "";
        }



        //Editar
        private void button2_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            con.Abrir();
            //(ID,NOMBRE,DIRECCION,TELEFONO)
            string editar = "UPDATE INTERESADOS SET NOMBRE='" + textBox1.Text + "'" + ",ESCUELA_P='" + textBox2.Text + "'" + ",EMAIL='" + textBox3.Text +"'WHERE ID='"+tbid.Text+ "'";
            FbCommand comando = new FbCommand(editar, con.fbconexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Datos Actualizados");
            con.Cerrar();
            actualizar();
            limpiar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion conecta = new Conexion();
                conecta.Abrir();
                MessageBox.Show("Conexion establecida con exito");
            }
            catch (Exception er)

            {

                MessageBox.Show("Error de Conexion:  " + er.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            con.Abrir();
            string carga = "SELECT * FROM INTERESADOS";
            FbCommand comando = new FbCommand(carga, con.fbconexion);
            comando.ExecuteNonQuery();
            FbDataAdapter da = new FbDataAdapter(comando);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv1.DataSource = dt;
            con.Cerrar();
        }
        //ELIMINAR
        private void button4_Click(object sender, EventArgs e)
        {
        
                if (MessageBox.Show("¿Desea Eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                {

                if ( textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || tbid.Text != "")
                {
                    Conexion con = new Conexion();
                    con.Abrir();
                    string elimina = "DELETE FROM INTERESADOS WHERE ID='" + tbid.Text + "'";
                    FbCommand comando = new FbCommand(elimina, con.fbconexion);
                    comando.ExecuteNonQuery();
                    con.Cerrar();
                    actualizar();
                    limpiar();
                    MessageBox.Show("Eliminado corretamente");
                }
                else
                {
                    MessageBox.Show("No existen datos");
                }

            }
                else
                {
                    MessageBox.Show("Error al Eliminar");
                }
  
        }


        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgv1.CurrentRow;
            textBox1.Text = dgv1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dgv1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dgv1.CurrentRow.Cells[3].Value.ToString();
            tbid.Text = dgv1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarDataGridViewExcel2(dgv1);
            }
            catch (Exception excel)
            {
                MessageBox.Show("No existen datos a exportar " + excel.Message);
            }
        }


        //Exporta a excel
        public void ExportarDataGridViewExcel2(DataGridView grd)
        {
            //creamos un objeto de tipo Excel sobre le cual vamos a trabajar (solo si ya agregamos la  referencia y la libreria) 
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //agregamos un nuevo libro a nuestro objeto excel
            excel.Application.Workbooks.Add(true);
            //contador para las columnas
            int ColumnIndex = 0;
            //este ciclo es para saber cuantas columnas y que nombre de encabezado tiene nuestro grid e irlas agregando a nuestro objeto excel
            foreach (DataGridViewColumn col in grd.Columns)
            {
                //aumentamos la variable de uno en uno por cada columna en el grid
                ColumnIndex++;
                //agregamos una nueva celda con el nombre de la columna para tener el encabezado de nuestra tabla de registro:
                //ejemplo:
                /*
                 * Nombre | apellido | direccion | telefono
                 * 
                 * y de esta forma copiamos el encabezado de nuestro data grid a nuestro objeto de escel
                 * */
                excel.Cells[1, ColumnIndex] = col.Name;
            }
            //variable para saber el numero de renglones
            int rowIndex = 0;
            //recorremos el grid cumpliedo la condicion del foreach que por cada renglon que halla en el grid hara lo que esta dentro del ciclo
            foreach (DataGridViewRow row in grd.Rows)
            {
                //aumentamos el numero del renglon
                rowIndex++;
                //el numero de columnas lo regresamos a cero
                ColumnIndex = 0;
                //en este ciclo recorremos cada columna del renglon en el que estamos trabajando, el recorrido es simple es recorrer una matriz
                foreach (DataGridViewColumn col in grd.Columns)
                {
                    //aumentamos la columna
                    ColumnIndex++;
                    //agregamos el valor contenido en determinada posicion del grid a nuestro objeto de excel.
                    /*
                     ejemplo: excel.cell[0+1,1]= row.cell["Nombre].value"
                     * 
                     * 
                     */
                    excel.Cells[rowIndex + 1, ColumnIndex] = row.Cells[col.Name].Value;
                    //vamos a ir recorriendo todas las columnas, renglon por renglon hasta terminar los renglones que tenga nuestro grid
                }
            }
            //finalmente mostramos en pantalla nuestro archivo de excel listo para ser guardado.
            excel.Visible = true;
            //habilitamos una hoja
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
        }


    }
}
