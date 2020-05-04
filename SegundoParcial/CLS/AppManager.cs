using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SegundoParcial.GUI;

namespace SegundoParcial.CLS
{
    class AppManager : ApplicationContext
    {
        public AppManager(){
            Conexion();             
        }

        private Boolean Conexion()
        {
            MySql f = new MySql();
            f.ShowDialog();
            return f._Confirmacion;
        }
        
    }
}
