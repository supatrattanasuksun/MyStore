using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class createModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 ||
                clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the name client into the database
            try
            {
                /*               SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
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
                    String sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}
