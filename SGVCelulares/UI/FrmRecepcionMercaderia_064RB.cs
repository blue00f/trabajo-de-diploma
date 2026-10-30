using BE_064RB;
using Servicio_064RB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB
{
    public partial class FrmRecepcionMercaderia_064RB : Form
    {
        BE_SolicitudCompra_064RB solicitud;
        BE_OrdenCompra_064RB orden;
        BE_RemitoCompra_064RB remito;
        public FrmRecepcionMercaderia_064RB()
        {
            InitializeComponent();
        }
        private void FrmRecepcionMercaderia_064RB_Load(object sender, EventArgs e)
        {
            solicitud = new();
            orden = new();
            remito = new();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            solicitud.Cantidad_064RB = 10;
            orden.SolicitudCompra_064RB = solicitud;
            remito.OrdenCompra_064RB = orden;

            FrmRegistrarDiscrepancia_064RB frm = new FrmRegistrarDiscrepancia_064RB(remito, null);
            if (frm.ShowDialog() == DialogResult.OK) MessageBox.Show("Faltantes: " + frm.DiscrepanciaCargada_064RB.CantidadFaltante_064RB);
        }

        public bool TienePermiso(string nombrePermiso)
        {
            foreach (SER_Componente_064RB hijo in SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.Componentes_064RB)
            {
                if (hijo.TienePermiso(nombrePermiso))
                    return true;
            }
            return false;
        }

    }
}
