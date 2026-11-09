using BLL_064RB.Tecnico_064RB;
using QuestPDF.Infrastructure;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            QuestPDF.Settings.License = LicenseType.Community;
            ApplicationConfiguration.Initialize();
            BLL_Inicializacion_064RB inicializacion = new();
            inicializacion.InicializarBaseDatos_064RB();
            Application.Run(new FrmLogin_064RB());
        }
    }
}