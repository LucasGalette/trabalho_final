using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalho_emerson
{
    public partial class FormVenda : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\trabalho_emerson\\Database1.mdf;Integrated Security=True");
        public FormVenda()
        {
            InitializeComponent();
            CarregaCbxCliente();
            CarregacbxProduto();

            dgvAdicionados.Columns.Add("Nome", "Nome");
            dgvAdicionados.Columns.Add("Produto", "Produto");
            dgvAdicionados.Columns.Add("Quantidade", "Quantidade");
            dgvAdicionados.Columns.Add("Valor", "Valor");
            dgvAdicionados.Columns.Add("Total", "Total");
        }

        private void btnNovoProduto_Click(object sender, EventArgs e)
        {
            if (cbxNome.Text == "" || cbxProduto.Text == "" || txtValor.Text == "" || txtQuantidade.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var repetido = false;
                foreach (DataGridViewRow dr in dgvAdicionados.Rows)
                {
                    if (cbxProduto.Text == Convert.ToString(dr.Cells[0].Value))
                    {
                        repetido = true;
                    }
                }
                if (repetido == false)
                {
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dgvAdicionados);
                    item.Cells[0].Value = cbxNome.Text;
                    item.Cells[1].Value = cbxProduto.Text;
                    item.Cells[2].Value = txtQuantidade.Text;
                    item.Cells[3].Value = txtValor.Text;
                    item.Cells[4].Value = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
                    dgvAdicionados.Rows.Add(item);
                    cbxNome.Text = "";
                    txtValor.Text = "";
                    txtQuantidade.Text = "";
                    cbxProduto.Text = "";
                    decimal soma = 0;
                    foreach (DataGridViewRow dr in dgvAdicionados.Rows)
                        soma += Convert.ToDecimal(dr.Cells[4].Value);
                    lblValorTotal.Text = Convert.ToString(soma);
                }
                else
                {
                    MessageBox.Show("Produto já Cadastrado!!", "Produto Repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Inserção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void CarregaCbxCliente()
        {
            string cli = "SELECT nome FROM Cliente";
            SqlCommand cmd = new SqlCommand(cli, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cli, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "cliente");
            cbxNome.ValueMember = "nome";
            cbxNome.DisplayMember = "nome";
            cbxNome.DataSource = ds.Tables["cliente"];
            con.Close();
        }

        public void CarregacbxProduto()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            string pro = "SELECT nome FROM Produto";
            SqlCommand cmd = new SqlCommand(pro, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(pro, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "produto");
            cbxProduto.ValueMember = "nome";
            cbxProduto.DisplayMember = "nome";
            cbxProduto.DataSource = ds.Tables["produto"];
            con.Close();
        }

        private void cbxProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM Produto WHERE nome=@nome", con);
            cmd.Parameters.AddWithValue("@nome", cbxProduto.SelectedValue);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtValor.Text = dr["valor"].ToString();
                txtQuantidade.Focus();
                dr.Close();
            }
            con.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cbxNome.Text == "" || cbxProduto.Text == "" || txtValor.Text == "" || txtQuantidade.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvAdicionados.CurrentRow.Index;
                dgvAdicionados.Rows[linha].Cells[0].Value = cbxNome.Text;
                dgvAdicionados.Rows[linha].Cells[1].Value = cbxProduto.Text;
                dgvAdicionados.Rows[linha].Cells[2].Value = txtQuantidade.Text;
                dgvAdicionados.Rows[linha].Cells[3].Value = txtValor.Text;
                dgvAdicionados.Rows[linha].Cells[4].Value = Convert.ToDecimal(txtValor.Text) * Convert.ToDecimal(txtQuantidade.Text);
                cbxNome.Text = "";
                txtValor.Text = "";
                txtQuantidade.Text = "";
                cbxProduto.Text = "";
                decimal soma = 0;
                foreach (DataGridViewRow dr in dgvAdicionados.Rows)
                    soma += Convert.ToDecimal(dr.Cells[4].Value);
                lblValorTotal.Text = Convert.ToString(soma);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAdicionados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvAdicionados.Rows[e.RowIndex];
            cbxNome.Text = row.Cells[0].Value.ToString();
            cbxProduto.Text = row.Cells[1].Value.ToString();
            txtQuantidade.Text = row.Cells[2].Value.ToString();
            txtValor.Text = row.Cells[3].Value.ToString();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (cbxNome.Text == "" || cbxProduto.Text == "" || txtValor.Text == "" || txtQuantidade.Text == "")
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int linha = dgvAdicionados.CurrentRow.Index;
                dgvAdicionados.Rows.RemoveAt(linha);
                dgvAdicionados.Refresh();
                cbxNome.Text = "";
                txtValor.Text = "";
                txtQuantidade.Text = "";
                cbxProduto.Text = "";
                decimal soma = 0;
                foreach (DataGridViewRow dr in dgvAdicionados.Rows)
                    soma += Convert.ToDecimal(dr.Cells[4].Value);
                lblValorTotal.Text = Convert.ToString(soma);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro - Edição", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            dgvAdicionados.Rows.Clear();
            MessageBox.Show("Obrigado por comprar na GATO GEEK, volte sempre", "Compra bem sucedida", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
