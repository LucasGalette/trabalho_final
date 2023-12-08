using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalho_emerson
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            FormSplash splash = new FormSplash();
            splash.Show();
            Application.DoEvents();
            Thread.Sleep(3000);
            splash.Close();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FormCliente cliente = new FormCliente();
            cliente.Show();
        }

        private void btnProduto_Click(object sender, EventArgs e)
        {
            FormProduto produto = new FormProduto();
            produto.Show();
        }

        private void btnVendas_Click(object sender, EventArgs e)
        {
            FormVenda venda = new FormVenda();
            venda.Show();
        }
    }
}
