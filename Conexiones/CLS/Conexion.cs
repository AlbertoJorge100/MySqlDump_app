using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace Conexiones.CLS
{
    public class Conexion
    {
        public Conexion()
        {
            
        }

      
        public Boolean EjecutarComando(String usuario, String contrasena, String ruta, String DB="", String servidor="")
        {
            //mysqldump -u root -p mydb > C:\ja\nueva2.sql
            //System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c "+_Command+" && \.");
            try
            {
                
                ProcessStartInfo procStartInfo = new ProcessStartInfo();
                procStartInfo.FileName = "CMD";
                DateTime myDateTime = DateTime.Now;
                String fecha = myDateTime.ToString("yyyy-mm-dd hh:mm:ss");
                fecha = fecha.Replace("/", "_");
                fecha = fecha.Replace(" ", "_");
                fecha = fecha.Replace(":", "_");
                fecha = fecha.Replace("-", "_");
                String _DB = "";
                
                if (DB.Equals(""))
                {
                    _DB = servidor + "_" + fecha; 
                    ruta += @"\\" + _DB + @".sql";
                    procStartInfo.Arguments = @"/C mysqldump --user=" + usuario + @" --password=" + contrasena + @" -A > " + ruta;
                }
                else
                {
                    _DB = DB + "_" + fecha; 
                    ruta += @"\\" + _DB + @".sql";
                    procStartInfo.Arguments = @"/C mysqldump --user=" + usuario + @" --password=" + contrasena + @" " + DB + @" > " + ruta;
                }
                
                //pStartInfo.WindowStyle = ProcessWindowStyle.Hidden; 
                Process.Start(procStartInfo);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
            //procStartInfo.Arguments = "jorge_perez100";
            // Indicamos que la salida del proceso se redireccione en un Stream
            /*procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            //procStartInfo.Arguments = @"/C mysql --user=root --password=sa casemanager && \.";
            //Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
            procStartInfo.CreateNoWindow = false;
            
            //Inicializa el proceso
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            
            proc.StartInfo = procStartInfo;
            
            proc.Start();
            proc.Close();
            
            //Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
            string result = proc.StandardOutput.ReadToEnd();
            //Muestra en pantalla la salida del Comando
            Console.WriteLine(result);*/
        }
        public void EjecutarComando()
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = false;
            p.StartInfo = info;
            p.Start();

            using (StreamWriter sw = p.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("mysql -u root -p");
                    //sw.WriteLine("jorge_perez100");
                    //sw.WriteLine("use mydb;");
                }
            }
        }
    }
}
