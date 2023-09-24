using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
 
        public void OnGet()
        {
            try
            {
                /*                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                                builder.DataSource = "166cs436.database.windows.net";
                                builder.UserID = "supat.r";
                                builder.Password = "$up@T59801";
                                builder.InitialCatalog = "BUEMAIL";

                                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                */
                String connectionString = "Server=tcp:166cs436.database.windows.net,1433;Initial Catalog=BUEMAIL;Persist Security Info=False;User ID=supat.r;Password=$up@T59801;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand (sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            } 
            catch (Exception ex) 
            { 
                Console.WriteLine ("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
