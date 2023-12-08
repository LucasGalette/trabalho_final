using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_emerson.Classes
{
    internal class Produto
    {
        public string nome { get; set; }
        public string tipo { get; set; }
        public string quantidade { get; set; }
        public string valor { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\trabalho_emerson\\Database1.mdf;Integrated Security=True");


        public void Inserir(string nome, string tipo, string quantidade, string valor)
        {
            string sql = "INSERT INTO Produto(nome,tipo,quantidade,valor) VALUES ('" + nome + "','" + tipo + "','" + quantidade + "','" + valor + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public List<Produto> listaProduto()
        {
            List<Produto> li = new List<Produto>();
            string sql = "SELECT * FROM Produto";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Produto p = new Produto();
                p.nome = dr["nome"].ToString();
                p.tipo = dr["tipo"].ToString();
                p.quantidade = dr["quantidade"].ToString();
                p.valor = dr["valor"].ToString();
                li.Add(p);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public bool RegistroRepetido(string nome)
        {
            string sql = "SELECT * FROM Produto WHERE nome='" + nome + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }

        public void Atualizar(string nome, string tipo, string quantidade, string valor)
        {
            string sql = "UPDATE Produto SET nome='" + nome + "',tipo='" + tipo + "',quantidade='" + quantidade + "',valor='" + valor + "' WHERE nome='" + nome + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Excluir(string nome)
        {
            string sql = "DELETE FROM Produto WHERE nome='" + nome + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
