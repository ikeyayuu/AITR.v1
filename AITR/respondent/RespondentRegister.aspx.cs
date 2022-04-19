using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AITR.Utils;
using AITR.DTO;

namespace AITR
{
    public partial class RespondentRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int GetRespondentId(String PhoneNumber)
        {
            int respondent_id = -1;

            using (SqlConnection conn = Utils.Utils.GetConnection())
            {                
                SqlCommand cmd = new SqlCommand("SELECT respondent_id FROM respondent WHERE phone_number = @PhoneNumber", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                SqlDataReader rd = cmd.ExecuteReader();

                if(rd.Read())
                {
                    respondent_id = Convert.ToInt32(rd["respondent_id"].ToString());
                } 
            }
            return respondent_id;
        }

        /*
         * 1 . Registration successful
         * 2 . User already exist in the DB
         * 3 . Registration error
         */

        private ResultStatus RegisterRespondent(String FirstName, String LastName, String DOB, String PhoneNumber)
        {

            ResultStatus resultStatus = new ResultStatus();

            int respondent_id = GetRespondentId(PhoneNumber);

            if (respondent_id != -1)
            {
                Session["respondent_id"] = respondent_id;

                resultStatus.ResultStatusCode = 2;
                resultStatus.Message = "User already exist";                
            } else
            {
                using (SqlConnection conn = Utils.Utils.GetConnection())
                {
                    String query = "INSERT INTO respondent (register, first_name, last_name, dob, phone_number) VALUES (@register, @FirstName, @LastName, @DOB, @PhoneNumber)";

                    conn.Open();

                    //convert Date time
                    string[] dateTimeParts = DOB.Split('/');
                    int day = Convert.ToInt32(dateTimeParts[0]);
                    int month = Convert.ToInt32(dateTimeParts[1]);
                    int year = Convert.ToInt32(dateTimeParts[2]);
                    DateTime dateOfBirth = new DateTime(year, month, day);

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@register", 1);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@DOB", dateOfBirth);
                        command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                        int result = command.ExecuteNonQuery();

                        // Error/Success message
                        if (result < 0)
                        {
                            resultStatus.ResultStatusCode = 3;
                            resultStatus.Message = "Error in registration";
                        } 
                        else
                        {
                            respondent_id = GetRespondentId(PhoneNumber);
                            Session["respondent_id"] = respondent_id;

                            resultStatus.ResultStatusCode = 1;
                            resultStatus.Message = "Registration succeed";
                        }
                    }                   

                }
            }
            return resultStatus;
                      
        }
              

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            
            String FirstName = TextBoxFirstName.Text;
            String LastName = TextBoxLastName.Text;
            String DOB = TextBoxDOB.Text;
            String PhoneNumber = TextBoxPhoneNumber.Text;

            ResultStatus resultStatus =  RegisterRespondent(FirstName, LastName, DOB, PhoneNumber);

            Label1.Text = resultStatus.Message;

        }
    }
}