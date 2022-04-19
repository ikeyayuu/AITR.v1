using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using AITR.Utils;

namespace AITR
{
    public partial class survey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["SID"] = null;
            Session["question_nb"] = 3;
            Session["question_nb_display"] = 1;

        }

        protected void ButtonNext_Click(object sender, EventArgs e)
        {           

            if (anonymous_yes.Checked)
            {
                using (SqlConnection conn = Utils.Utils.GetConnection())
                {
                    String query = "INSERT INTO Respondent (register) VALUES (@register) select SCOPE_IDENTITY()";

                    conn.Open();



                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@register", 0);                        

                        String id = command.ExecuteScalar().ToString();

                        Session["RID"] = id;
                    }

                    conn.Close();
                }

                    Response.Redirect("Survey.aspx");
                

            } else
            {
                Response.Redirect("RespondentRegister.aspx");
            }
        }
    }
}