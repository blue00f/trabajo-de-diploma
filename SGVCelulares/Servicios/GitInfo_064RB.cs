
using System;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace Servicio_064RB
{
    public static class GitInfo_064RB
    {
        private static string EjecutarGit_064RB(string argumentos)
        {
            string rutaProyecto = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directorio = new DirectoryInfo(rutaProyecto);
            while (directorio != null && directorio.Name != "SGVCelulares")
            {
                directorio = directorio.Parent;
            }
            if (directorio == null) throw new DirectoryNotFoundException("No se encontró la carpeta SGVCelulares.");
            string rutaRepositorio = directorio.Parent.FullName;
            var inicio = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = argumentos,
                WorkingDirectory = rutaRepositorio,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (var proceso = Process.Start(inicio))
            {
                string resultado = proceso.StandardOutput.ReadToEnd().Trim();
                proceso.WaitForExit();
                if (proceso.ExitCode != 0) throw new Exception(proceso.StandardError.ReadToEnd());
                return resultado;
            }
        }
        public static string ObtenerRama_064RB() => EjecutarGit_064RB("branch --show-current");
        public static string ObtenerFechaCommit_064RB() => EjecutarGit_064RB("log -1 --format=%cd --date=format:%d/%m/%Y_%H:%M").Replace("_", " ");
        public static string ObtenerHashCommit_064RB() => EjecutarGit_064RB("rev-parse --short HEAD");
    }
}
