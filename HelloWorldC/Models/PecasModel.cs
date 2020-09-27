using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HelloWorldC.Models
{
    public class PecaModel : IDisposable
    {
        private MySqlConnection connection;

        public PecaModel()
        {

            //string cs = @"server=localhost;userid=dbuser;password=s$cret;database=testdb";

            //using var con = new mysqlconnection(cs);
            //con.open();

            //console.writeline($"mysql version : {con.serverversion}");
            //string strConn = "Data Source= localhost;Initial Catalog=DBInventario; Integrated Security=true";

            //http://zetcode.com/csharp/mysql/
            string strConn = @"server=localhost;port=33061;userid=root;password=root123;database=inventario"; 
            connection = new MySqlConnection(strConn);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close(); 
        }

        public void Create(Peca peca)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = @"INSERT INTO pecas(nome, fabricante, quantidade, preco, date) VALUES (@nome, @fabricante, @quantidade, @preco, @date)";

            cmd.Parameters.AddWithValue("@nome", peca.nome);
            cmd.Parameters.AddWithValue("@fabricante", peca.fabricante);
            cmd.Parameters.AddWithValue("@quantidade", peca.quantidade);

            Console.WriteLine("Vai para banco model: "+ peca.preco.ToString());
            cmd.Parameters.AddWithValue("@preco", peca.preco);

            cmd.Parameters.AddWithValue("@date", peca.date);

            cmd.ExecuteNonQuery();
        }

        public List<Peca> Read()
        {
            List<Peca> lista = new List<Peca>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM pecas";

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Peca peca = new Peca();
                peca.id = (int)reader["id"];
                peca.nome = (string)reader["nome"];
                peca.fabricante = (string)reader["fabricante"];
                peca.quantidade = (int)reader["quantidade"];
                peca.preco = Double.Parse(reader["preco"].ToString());
                peca.date = (DateTime) reader["date"];

                lista.Add(peca);

            }

            return lista;
        }

        public void Update(Peca peca)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Peca SET Nome=@nome, Fabricante=@fabricante, Quantidade=@quantidade, Preco=@preco, Date=@date WHERE id=@id";

            cmd.Parameters.AddWithValue("@id", peca.id);
            cmd.Parameters.AddWithValue("@nome", peca.nome);
            cmd.Parameters.AddWithValue("@fabricante", peca.fabricante);
            cmd.Parameters.AddWithValue("@quantidade", peca.quantidade);
            cmd.Parameters.AddWithValue("@preco", peca.preco);
            cmd.Parameters.AddWithValue("@date", peca.date);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM Pecas WHERE Id=@id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }


    }
}
