using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MonyCounter.Controllers
{

    public class dbOperator
    {
       
        public SqlConnection sc = new SqlConnection();

        public SqlConnection Connect()
        {
            string connectionstring = @"Server=tcp:new-sql-server.database.windows.net,1433;Initial Catalog=MonyCounter;Persist Security Info=False;User ID=adm;Password=hAp3KQDndFyP;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            sc = new SqlConnection(connectionstring);
            //sc = new SqlConnection("Integrated Security=false;database=" + Database + ";server=" + Server +
            //    @",1433;User ID= UNGG\logvinova_sv; pwd = ;Asynchronous Processing=true");
            sc.Open();

            return sc;
        }

       
        private static void Disconnect(SqlConnection scc )
        {
            scc.Close();
        }





        public class User
        {
            public int? Id;
            public long ChatId;
            public string FirstName;

            public SqlConnection sc = new SqlConnection();

            dbOperator db = new dbOperator();


            public User() { }
            public User(long chatId, string firstName)
            {
                ChatId = chatId;
                FirstName = firstName;
            }


            public string AddUser(User ob)
            {


                if (sc == null || sc.State != System.Data.ConnectionState.Open)
                    sc = db.Connect();

                string sql = "INSERT INTO Users (nameUser, chatID) VALUES (@nameUser, @chatID)";


                SqlCommand command = new SqlCommand(sql, sc);

                command.Parameters.AddWithValue("@nameUser", ob.FirstName);
                command.Parameters.AddWithValue("@chatID", ob.ChatId);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }


                Disconnect(sc);
                return "Good";
            }


            public User CheckUser(User ob)
            {
                try
                {
                    if (sc == null || sc.State != System.Data.ConnectionState.Open)
                        sc = db.Connect();

                    string sql = "Select chatID from Users where nameUser = @nameUser";


                    SqlCommand command = new SqlCommand(sql, sc);

                    command.Parameters.AddWithValue("@nameUser", ob.FirstName);

                    SqlDataReader rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        ob.Id = Convert.ToInt32(rd["chatID"]);
                    }



                    Disconnect(sc);
                    return ob;
                }
                catch(Exception ex)
                {
                    ob.FirstName = ex.Message;
                    return ob;
                }
                
            }



        }


        public class Category
        {
            public SqlConnection sc = new SqlConnection();

            dbOperator db = new dbOperator();
            public int id;
            public string Name; 
            public Category() { }
            public Category(int Id, string name) {
                id = Id;
                Name = name;
            }

            public List<Category> GetAllCategory()
            {
                List<Category> categories = new List<Category>();
                if (sc == null || sc.State != System.Data.ConnectionState.Open)
                    sc = db.Connect();

                string sql = "Select id, nameCategory from Category order by id ";

                SqlCommand command = new SqlCommand(sql, sc);

                SqlDataReader rd = command.ExecuteReader();

                while (rd.Read())
                {
                    categories.Add(new Category(Convert.ToInt32(rd["id"]), rd["nameCategory"].ToString()));
                }



                Disconnect(sc);
                return categories;



            }


            public override bool Equals(Object obj)
            {
                if (obj == null)
                    return false;

                Category other = obj as Category;
                if ((Object)other == null)
                    return false;



                return this.id == other.id
                    && this.Name == other.Name;
                    
            }

        }


        public class Money
        {
            public SqlConnection sc = new SqlConnection();

            dbOperator db = new dbOperator();
            

            int id { get; set; }
            public string idUser { get; set; }
            public string nameCost { get; set; }
            public string spending { get; set; }
            public int isCosts { get; set; }

            public Money()
            {

            }
            
            public string addCost( Money ob)
            {


                if (sc == null || sc.State != System.Data.ConnectionState.Open)
                    sc = db.Connect();

                string sql = "INSERT INTO Money (idUser, nameCost, isCosts, spending) VALUES (@idUser, @nameCost, @isCosts, @spending)";


                SqlCommand command = new SqlCommand(sql, sc);



                command.Parameters.AddWithValue("@idUser", ob.idUser);
                command.Parameters.AddWithValue("@nameCost", ob.nameCost);
                command.Parameters.AddWithValue("@spending", ob.spending);
                command.Parameters.AddWithValue("@isCosts", ob.isCosts);
                
               

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }


                Disconnect(sc);
                return "Good";



              
               
            }

            public string UpdateIsCost(Money ob)
            {
                

                if (sc == null || sc.State != System.Data.ConnectionState.Open)
                    sc = db.Connect();

                string sql = "Update money set isCosts = @isCosts where idUser = @idUser  where ";


                SqlCommand command = new SqlCommand(sql, sc);

                command.Parameters.AddWithValue("@idUser", ob.idUser);
                command.Parameters.AddWithValue("@isCosts", ob.isCosts);



                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }


                Disconnect(sc);
                return "Good";





            }

            public List<string> GetAllSpending(string usrerID)
            {
                List<string> distNameCost = new List<string>();
                List<string> allSpending = new List<string>();
                try
                {
                    if (sc == null || sc.State != System.Data.ConnectionState.Open)
                        sc = db.Connect();

                    string sql = "SELECT DISTINCT nameCost, idUser from Money WHERE idUser = @idUser and isCosts = 1";


                    SqlCommand command = new SqlCommand(sql, sc);

                    command.Parameters.AddWithValue("@idUser", usrerID);

                    SqlDataReader rd = command.ExecuteReader();

                    while (rd.Read())
                    {
                        distNameCost.Add(rd["nameCost"].ToString()) ;
                    }
                    rd.Close();
                    double summ = 0;
                    foreach (string item in distNameCost)
                    {
                        sql = "SELECT SUM(CASE WHEN ISNUMERIC(m.spending) = 1 THEN CAST(m.spending AS INT) ELSE 0 END) as allSpending FROM Money m WHERE m.nameCost  = @nameCost  AND idUser = @idUser and isCosts = 1 and MONTH(m.dateCosts ) = MONTH(GETDATE()) AND YEAR(m.dateCosts ) = YEAR(GETDATE());";
                        command = new SqlCommand(sql, sc);

                        command.Parameters.AddWithValue("@idUser", usrerID);
                        command.Parameters.AddWithValue("@nameCost", item);
                        rd = command.ExecuteReader();
                        
                        while (rd.Read())
                        {
                            summ = summ+ Convert.ToDouble(rd["allSpending"]);
                            allSpending.Add(item+" - "+ rd["allSpending"].ToString());
                        }
                        
                        rd.Close();
                    }
                    allSpending.Add("Всего: " + summ);



                    Disconnect(sc);
                    return allSpending;
                }
                catch (Exception ex)
                {
                    //ob.FirstName = ex.Message;
                    //return ob;
                    allSpending.Add(ex.Message);
                    return allSpending;
                }
            }


        }
    }
}