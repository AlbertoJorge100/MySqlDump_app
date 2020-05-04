using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Conexiones.CLS;
//using SegundoParcial.
namespace SegundoParcial.GUI
{
    public partial class MySql : Form
    {
        private FolderBrowserDialog fbdFolder;
        private Boolean Confirmacion;
        private DataTable _DATOS;
        private Boolean Seleccion;
        private void ConexionCmd()
        {
            Conexion con = new Conexion();
            /*
            string command = "dir";
            Console.WriteLine("CMD Executer V.0.1");
            Console.WriteLine("Last Release 05 - Febraury - 2012");
            while (true)
            {
                Console.Write("#:");
                command = Console.ReadLine();
                if (command == "Salir" || command == "salir")
                {
                    break;
                }
                con.ExecuteCommand(command);
            }*/
            //con.EjecutarComando("mysql -u root -p",");
            
        }

        private void Configurar()//añadimos una fila con las siguientes columnas
        {
            _DATOS.Columns.Add("Id");
            _DATOS.Columns.Add("Perfil");
            _DATOS.Columns.Add("Servidor");
            _DATOS.Columns.Add("BaseDatos");
            _DATOS.Columns.Add("Usuario");
            _DATOS.Columns.Add("Contrasena");
            _DATOS.Columns.Add("Puerto");            
            dtgConexiones.AutoGenerateColumns = false;
            dtgConexiones.DataSource = _DATOS;//

        }

        private void ContarRegistros()
        {
            lblEstado.Text=dtgConexiones.Rows.Count.ToString() + " Registros encontrados";
        }

        private void GuardarLista()//Xml para respaldo de datos
        {
            _DATOS.TableName = "Perfiles";
            _DATOS.WriteXml("Perfiles.xml");

        }
        
        private void LeerLista()//Leer los datos del xml
        {
            _DATOS.TableName = "Perfiles";
            _DATOS.ReadXml("Perfiles.xml");
        }

        public MySql()
        {
            InitializeComponent();
            Confirmacion = false;
            _DATOS = new DataTable();
            Configurar();
            LeerLista();
            ContarRegistros();
            this.Seleccion = false;
             fbdFolder= new FolderBrowserDialog();

            
        }
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        private void MySql_Load(object sender, EventArgs e)
        {
            
        }

        private void dtgConexiones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Perfil f = new Perfil();
            f.ShowDialog();
            if (f._Confirmacion)
            {
                DataRow fila = _DATOS.NewRow();
                fila["Id"] = f.txbId.Text;
                fila["Perfil"] = f.txbPerfil.Text;
                fila["Servidor"] = f.txbServidor.Text;
                fila["BaseDatos"] = f.txbBaseDatos.Text;
                fila["Usuario"] = f.txbUsuario.Text;
                fila["Contrasena"] = f.txbContrasena.Text;
                fila["Puerto"] = f.txbPuerto.Text;
                _DATOS.Rows.Add(fila);
                ContarRegistros();
                GuardarLista();
                MessageBox.Show("El Perfil se guardo correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Perfil f = new Perfil();
            f.txbId.Text = dtgConexiones.CurrentRow.Cells["Id"].Value.ToString();
            f.txbPerfil.Text = dtgConexiones.CurrentRow.Cells["Perfil"].Value.ToString();
            f.txbServidor.Text = dtgConexiones.CurrentRow.Cells["Servidor"].Value.ToString();
            f.txbBaseDatos.Text = dtgConexiones.CurrentRow.Cells["BaseDatos"].Value.ToString();
            f.txbUsuario.Text = dtgConexiones.CurrentRow.Cells["Usuario"].Value.ToString();
            f.txbContrasena.Text = dtgConexiones.CurrentRow.Cells["Contrasena"].Value.ToString();
            f.txbPuerto.Text = dtgConexiones.CurrentRow.Cells["Puerto"].Value.ToString();
            f.ShowDialog();
            if (f._Confirmacion)
            {
                dtgConexiones.CurrentRow.Cells["Id"].Value = f.txbId.Text;
                dtgConexiones.CurrentRow.Cells["Perfil"].Value = f.txbPerfil.Text;
                dtgConexiones.CurrentRow.Cells["Servidor"].Value = f.txbServidor.Text;
                dtgConexiones.CurrentRow.Cells["BaseDatos"].Value = f.txbBaseDatos.Text;
                dtgConexiones.CurrentRow.Cells["Usuario"].Value = f.txbUsuario.Text;
                dtgConexiones.CurrentRow.Cells["Contrasena"].Value = f.txbContrasena.Text;
                dtgConexiones.CurrentRow.Cells["Puerto"].Value = f.txbPuerto.Text;
                GuardarLista();
                MessageBox.Show("El Perfil se actualizo correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea eliminar el perfil", "Confirmacion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)==DialogResult.Yes)
            {
                dtgConexiones.Rows.RemoveAt(dtgConexiones.CurrentRow.Index);
                GuardarLista();
                ContarRegistros();
                MessageBox.Show("El Perfil se elimino correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {            
            if (this.Seleccion)
            {
                if (MessageBox.Show("Desea generar el respaldo de la base de datos? ", "Pregunta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String usuario = dtgConexiones.CurrentRow.Cells["Usuario"].Value.ToString();
                    String contrasena = dtgConexiones.CurrentRow.Cells["Contrasena"].Value.ToString();
                    String DB = dtgConexiones.CurrentRow.Cells["BaseDatos"].Value.ToString();
                    String ruta=fbdFolder.SelectedPath.ToString();
                    //ruta = ruta.Replace("\\", @"\");                    
                    try
                    {
                        Conexion cn = new Conexion();                        
                        Boolean resultado = cn.EjecutarComando(usuario, contrasena,ruta,DB);
                        if (resultado)
                        {
                            MessageBox.Show("Se respaldo la base de datos correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Seleccion = false;
                            fbdFolder = new FolderBrowserDialog();
                            this.lblRuta.Text = "Ruta no especificada";
                        }
                        else
                        {
                            MessageBox.Show("Error interno 1 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("Error interno !" + e2.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else { MessageBox.Show("Debe seleccionar una ruta para guardar el respaldo !" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        

        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            
            if (fbdFolder.ShowDialog() == DialogResult.OK)
            {                
                this.Seleccion = true;
                lblRuta.Text = fbdFolder.SelectedPath.ToString() + @"\";
                //MessageBox.Show(fbdFolder.SelectedPath.ToString());
            }
        }

        private void btnRespaldarTodos_Click(object sender, EventArgs e)
        {
            if (this.Seleccion)
            {
                if (MessageBox.Show("Desea respaldar todas las bases de datos? ", "Pregunta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String usuario = dtgConexiones.CurrentRow.Cells["Usuario"].Value.ToString();
                    String contrasena = dtgConexiones.CurrentRow.Cells["Contrasena"].Value.ToString();
                    String servidor = dtgConexiones.CurrentRow.Cells["Servidor"].Value.ToString();
                    String ruta = fbdFolder.SelectedPath.ToString();
                    //ruta = ruta.Replace("\\", @"\");                    
                    try
                    {
                        Conexion cn = new Conexion();
                        Boolean resultado = cn.EjecutarComando(usuario, contrasena, ruta,"",servidor);
                        if (resultado)
                        {
                            MessageBox.Show("Se respaldaron todas las base de datos correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Seleccion = false;
                            fbdFolder = new FolderBrowserDialog();
                            this.lblRuta.Text = "Ruta no especificada";
                        }
                        else
                        {
                            MessageBox.Show("Error interno 1 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("Error interno !" + e2.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else { MessageBox.Show("Debe seleccionar una ruta para guardar el respaldo !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

      
    }
}
