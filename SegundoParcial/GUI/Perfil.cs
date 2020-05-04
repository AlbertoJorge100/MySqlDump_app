using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SegundoParcial.GUI
{
    public partial class Perfil : Form
    {
        public enum Accion { INSERTAR, EDITAR };
        private Boolean Confirmacion;
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        private Accion Opcion=Accion.INSERTAR;
        public Perfil( )
        {
            InitializeComponent();
            //this.Opcion = opcion;
            this.Confirmacion = false;
        }
        
        private void Perfil_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(!txbId.Text.Equals("") && !txbPuerto.Text.Equals("") && !txbPerfil.Text.Equals("")
                && !txbServidor.Text.Equals("") && !txbUsuario.Text.Equals("") && !txbContrasena.Text.Equals("")
                && !txbBaseDatos.Text.Equals(""))
            {
                this.Confirmacion = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("No deje campos vacios !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();        }
    }
}
