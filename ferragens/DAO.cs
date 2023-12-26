using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ferragens
{
    public class ClassDao
    {
        class DAO
        {
        }
        public class Campos
        {
            public int id;
            public string nome;
            public string desc;
            public string forn;
            public int valor;
            public int qnt;
        }

        public Campos campos = new Campos();

        public MySqlConnection minhaConexao;

        public string usuarioBD = "root";
        public string senhaBD = "jose";
        public string servidor = "localhost";
        string bancoDados;
        string tabela;
        public void Conecte(string BancoDados, string Tabela)
        {
            bancoDados = BancoDados;
            tabela = Tabela;

            minhaConexao = new MySqlConnection("server= " + servidor +
                " ; database= " + bancoDados +
                " ; uid= " + usuarioBD +
                " ; password= " + senhaBD);
        }

        void Abrir()
        {
            minhaConexao.Open();
        }

        void Fechar()
        {
            minhaConexao.Close();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela, minhaConexao);

            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void ConsultaTabela(System.Windows.Forms.DataGridView dataGridView, string busca)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * from " + tabela
                + " where nome_prod like " + "'" + busca + "%';", minhaConexao);

            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }
        public void Consulta(string campoNome)
        {
            //consulta por nome

            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela
                + " where nome = '" + campoNome + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();

            if (dtReader.Read())
            {
                campos.id = int.Parse(dtReader["id_prod"].ToString());
                campos.nome = dtReader["nome_prod"].ToString();
                campos.desc = dtReader["descr_prod"].ToString();
                campos.forn = dtReader["fornecedor "].ToString();
                campos.valor = int.Parse(dtReader["valor_unit "].ToString());
                campos.qnt = int.Parse(dtReader["qtde_estoque "].ToString());

            }
            Fechar();
        }

       public void Add(string campoNome, string campoDesc, string campoForn, int campoQnt, int campoValor )
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand ("INSERT INTO " + tabela+ "(nome_prod,descr_prod ,valor_unit, qtde_estoque,fornecedor) "
  + "VALUES(@nome, @desc,@valor,@qnt,@forn) ", minhaConexao);
            comando.Parameters.AddWithValue("@nome", campoNome);
            comando.Parameters.AddWithValue("@desc", campoDesc);
            comando.Parameters.AddWithValue("@forn", campoForn);
            comando.Parameters.AddWithValue("@qnt", campoQnt);
            comando.Parameters.AddWithValue("@valor", campoValor);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Att(string campoNome, string campoDesc, string campoForn, int campoQnt, int campoValor, int campoId)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("UPDATE " + tabela + " set nome_prod=@nome, descr_prod=@desc, valor_unit=@valor, qtde_estoque=@qnt, fornecedor=@forn" + " where id_prod=@id;", minhaConexao);
            comando.Parameters.AddWithValue("@nome", campoNome);
            comando.Parameters.AddWithValue("@desc", campoDesc);
            comando.Parameters.AddWithValue("@forn", campoForn);
            comando.Parameters.AddWithValue("@qnt", campoQnt);
            comando.Parameters.AddWithValue("@valor", campoValor);
            comando.Parameters.AddWithValue("@id", campoId);
            comando.ExecuteNonQuery();

            Fechar();
        }



    }
    }

