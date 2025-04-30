using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;



namespace pdv
{
    class MainClass
    {
        public static readonly string con_string = "server=localhost;database=pdv;uid=root;pwd=payura01;";
        public static MySqlConnection con = new MySqlConnection(con_string);

        // Método para verificar usuário e senha:
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = "SELECT * FROM usuario WHERE usuario = @user AND senha = @pass";

            if (con.State != ConnectionState.Open)
                con.Open();

            MySqlCommand cmd = new MySqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["usuario"].ToString(); // Armazena o nome do usuário
            }

            con.Close();  // fecha a conexão depois

            return isValid;
        }


        //Criar uma propriedade para armazenar o nome do usuário
        public static string user;

        public static string USER
        {
            get { return user; }
            set { user = value; }
        }
    }
}