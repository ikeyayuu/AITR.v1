using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AITR.Utils;
using System.Net;
using AITR.DTO;


namespace AITR.respondent
{
    public partial class Survey : System.Web.UI.Page
    {
        private bool openQuestion = false;
        private int questionOptions = 0;
        private int nextQuestion = 0;
        private int previousQuestion = 0;
        private int maxAnswer = 0;
        private string havePreviousQuestion = String.Empty;
        private string haveNextQuestion = String.Empty;

        private TextBox textBox = new TextBox();
        private DropDownList dropDownList = new DropDownList();
        private CheckBoxList checkBoxList = new CheckBoxList();
        private RadioButtonList radioBtnList = new RadioButtonList();
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            page_title.Text = "Question number " + Session["question_nb_display"].ToString();

            if (Session["SID"] == null)
            {
                TestSessionId();
            }
            

            using (SqlConnection conn = Utils.Utils.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT Question.*, QuestionType.* FROM Question INNER JOIN QuestionType ON Question.QTID = QuestionType.QTID", conn);
                conn.Open();

                SqlDataReader rd = cmd.ExecuteReader();

                // Database table for Question
                DataTable questionDB = new DataTable();
                questionDB.Columns.Add("QID", System.Type.GetType("System.String"));
                questionDB.Columns.Add("QTID", System.Type.GetType("System.String"));
                questionDB.Columns.Add("title", System.Type.GetType("System.String"));
                questionDB.Columns.Add("description", System.Type.GetType("System.String"));
                questionDB.Columns.Add("prevQID", System.Type.GetType("System.Int32"));
                questionDB.Columns.Add("nextQID", System.Type.GetType("System.Int32"));
                questionDB.Columns.Add("multiple_answer", System.Type.GetType("System.Boolean"));
                questionDB.Columns.Add("max_answer", System.Type.GetType("System.Int32"));
                DataRow question;

                while (rd.Read())
                {

                    question = questionDB.NewRow();

                    question["QID"] = rd["QID"].ToString();
                    question["QTID"] = rd["QTID"].ToString();
                    question["title"] = rd["title"].ToString();
                    question["description"] = rd["description"].ToString();
                    question["prevQID"] =  rd["prevQID"];
                    question["nextQID"] = rd["nextQID"];
                    question["multiple_answer"] = Convert.ToBoolean(rd["multiple_answer"]);
                    question["max_answer"] = Convert.ToInt32(rd["max_answer"]);

                    if (int.Parse((string)question["QID"]) == Convert.ToInt32(Session["question_nb"]))
                    {
                        havePreviousQuestion = question["prevQID"].ToString();
                        haveNextQuestion = question["nextQID"].ToString();
                        question_title.Text = question["title"].ToString();
                        question_description.Text = question["description"].ToString();
                        maxAnswer = Convert.ToInt32(question["max_answer"]);

                        if (Convert.ToInt32(question["QTID"]) > 1)
                        {
                            if (Convert.ToInt32(question["max_answer"]) == 1)
                            {
                                question_option.Text = "Select " + question["max_answer"].ToString() + " option";
                            }
                            else
                            {
                                question_option.Text = "Select " + question["max_answer"].ToString() + " options";
                            }
                        } else
                        {
                            question_option.Text = "Type your answer";
                        }

                        questionOptions = Convert.ToInt32(question["QTID"]);
                        if (questionOptions == 1)
                        {                            
                            Option.Controls.Add(textBox);
                            textBox.Text = String.Empty;
                        }

                        questionDB.Rows.Add(question);
                        dbTableView.DataSource = questionDB;
                        dbTableView.DataBind();

                        if (Convert.ToInt32(question["QTID"]) != 1)
                        {
                            openQuestion = true;
                        } else
                        {
                            openQuestion = false;
                        }

                        if (havePreviousQuestion.Length == 0)
                        {
                            button_previous.Visible = false;                            
                        }
                        else
                        {
                            button_previous.Visible = true;                            
                            previousQuestion = Convert.ToInt32(question["prevQID"]);

                        }

                        if (haveNextQuestion.Length == 0)
                        {
                            button_next.Visible = false;
                            button_finish.Visible = true;
                        }
                        else
                        {
                            button_next.Visible = true;
                            button_finish.Visible = false;
                            nextQuestion = Convert.ToInt32(question["nextQID"]);
                        }
                    }

                    

                }
                conn.Close();

                SqlCommand Q_OptionCommand;
                conn.Open();
                Q_OptionCommand = new SqlCommand("SELECT * FROM MultipleAnswer WHERE QID = @id", conn);
                Q_OptionCommand.Parameters.AddWithValue("@id", Convert.ToInt32(Session["question_nb"]));

                SqlDataReader Q_OptionReader;
                Q_OptionReader = Q_OptionCommand.ExecuteReader();
                // Database table for Question Option
                DataTable Q_OptionDB = new DataTable();
                Q_OptionDB.Columns.Add("id", System.Type.GetType("System.String"));
                Q_OptionDB.Columns.Add("title", System.Type.GetType("System.String"));
                Q_OptionDB.Columns.Add("subQuestion", System.Type.GetType("System.Boolean"));
                Q_OptionDB.Columns.Add("subQID", System.Type.GetType("System.String"));
                DataRow Q_OptionRow;
                while (Q_OptionReader.Read())
                {
                    Q_OptionRow = Q_OptionDB.NewRow();
                    Q_OptionRow["id"] = Q_OptionReader["multiple_answer_id"].ToString();
                    Q_OptionRow["title"] = Q_OptionReader["title"].ToString();
                    Q_OptionRow["subQuestion"] = Convert.ToBoolean(Q_OptionReader["subQuestion"]);
                    Q_OptionRow["subQID"] = Q_OptionReader["subQID"].ToString();
                    Q_OptionDB.Rows.Add(Q_OptionRow);
                    dbSubQuestion.DataSource = Q_OptionDB;
                    dbSubQuestion.DataBind();
                    Console.WriteLine(questionOptions);

                     if (questionOptions == 2)
                    {
                        ListItem item = new ListItem();
                        item.Text = Q_OptionRow["title"].ToString();
                        item.Value = Q_OptionRow["id"].ToString();

                        radioBtnList.Items.Add(item);
                        Option.Controls.Add(radioBtnList);
                    } 
                    else if (questionOptions == 3 || questionOptions == 4 || questionOptions == 6 || questionOptions == 7 || questionOptions == 8) 
                    {
                        ListItem item = new ListItem();
                        item.Text = Q_OptionRow["title"].ToString();
                        item.Value = Q_OptionRow["id"].ToString();

                        checkBoxList.Items.Add(item);
                        Option.Controls.Add(checkBoxList);

                    } 
                    else if (questionOptions == 5)
                    {

                        ListItem item = new ListItem();
                        item.Text = Q_OptionRow["title"].ToString();
                        item.Value = Q_OptionRow["id"].ToString();
                        
                        dropDownList.Items.Add(item);
                        Option.Controls.Add(dropDownList);
                    } 
                    
                    

                    
                    
                }                
            }

        }

        private void TestSessionId()
        {

            String SID =  String.Empty;           
                        
                using (SqlConnection conn = Utils.Utils.GetConnection())
                {
                    String query = "INSERT INTO Session (RID, date, ip) VALUES (@RID, @date, @ip) select SCOPE_IDENTITY()";

                    conn.Open();

                    String dateString = DateTime.Today.ToShortDateString();
                    string[] dateTimeParts = dateString.Split('/');
                    int day = Convert.ToInt32(dateTimeParts[0]);
                    int month = Convert.ToInt32(dateTimeParts[1]);
                    int year = Convert.ToInt32(dateTimeParts[2]);

                    DateTime date = new DateTime(year, month, day);
                    String hostName = Dns.GetHostName();
                    String ip = Dns.GetHostByName(hostName).AddressList[0].ToString();

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@RID", Session["RID"]);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@ip", ip);

                        String id = command.ExecuteScalar().ToString(); 

                        Session["SID"] = id;                        
                    }
                    conn.Close();
                
            }
        }

        protected void button_next_Click(object sender, EventArgs e)
        {  
            //textBox
            if(questionOptions == 1)
            {
                SaveAnswerInDb(textBox.Text);
            }
            //radioButton
            else if (questionOptions == 2)
            {
                foreach(ListItem item in radioBtnList.Items)
                {
                    if (item.Selected == true)
                    {
                        SaveAnswerInDb(item.Value);
                    }
                }
                
            }
            //Checkbox
            else if (questionOptions == 3 || questionOptions == 4 || questionOptions == 6 || questionOptions == 7 || questionOptions == 8)
            {
                
                foreach(ListItem item in checkBoxList.Items)
                {
                    if (item.Selected == true)
                    {
                        SaveAnswerInDb(item.Value);
                    }
                }
               

            }
            //dropdownlist
            else if (questionOptions == 5)
            {
                foreach (ListItem item in dropDownList.Items)
                {
                    if (item.Selected == true)
                    {
                        SaveAnswerInDb(item.Value);
                    }
                }

            }
           Session["question_nb_display"] = Convert.ToInt32(Session["question_nb_display"]) + 1;
           Session["question_nb"] = nextQuestion;
           Response.Redirect("Survey.aspx");
        }

        protected void button_previous_Click(object sender, EventArgs e)
        {
            Session["question_nb_display"] = Convert.ToInt32(Session["question_nb_display"]) - 1;
            Session["question_nb"] = previousQuestion;
            Response.Redirect("Survey.aspx");
        }

        private void SaveAnswerInDb(string answer)
        {
            ResultStatus resultStatus = new ResultStatus();

            using (SqlConnection conn = Utils.Utils.GetConnection())
            {
                String query = "INSERT INTO research_answer (research_id, question_id, answer) VALUES (@research_id, @question_id, @answer)";

                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@SID", Session["SID"]);
                    command.Parameters.AddWithValue("@QID", Session["question_nb"]);
                    command.Parameters.AddWithValue("@answer", answer);

                    int result = command.ExecuteNonQuery();

                    // Error/Success message
                    if (result < 0)
                    {
                        resultStatus.ResultStatusCode = 3;
                        resultStatus.Message = "Error in registration";
                    }
                    else
                    {

                        resultStatus.ResultStatusCode = 1;
                        resultStatus.Message = "Registration succeed";
                    }
                }
            }
        }

        
        

    }
}