using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace ConsoleAppQueueManagement
{
    internal class Page
    {
       public SqlConnection con = new SqlConnection(@"Data Source=PC-227\SQL2016EXPRESS;Initial Catalog=Northwind;Persist Security Info=True;User ID=sagar;Password=aa");
        public string PageName { get; set; }
        public Page PreviousPage { get; set; }
        public int menuid { get; set; }
        public static int service_Branch_id { get; set; }
        public List<Page> pageList = new List<Page>();
        public List<QuestionAnswers> QAList = new List<QuestionAnswers>();
        public virtual void Menu()
        {
            ShowMenu();


        }
        public void ShowPageName() { Console.WriteLine("This is " + PageName + " page."); }
        public void ShowMenu()
        {
            //ShowPageName();
            if (pageList.Count > 0)
            {
                Console.WriteLine(Messge.ChoiceMsg);
                foreach (Page p in pageList)
                {
                    Console.WriteLine((pageList.IndexOf(p) + 1) + "." + p.PageName);
                }
                Console.WriteLine((pageList.Count + 1) + "." + PreviousPage.PageName + "(Previous Page)");

                int choice = consolereader.getint("");
                if (choice > 0 && choice <= pageList.Count)
                {
                    pageList[choice - 1].Menu();

                }
                else if (choice == pageList.Count + 1)
                {
                    PreviousPage.ShowPageName();
                    PreviousPage.Menu();
                }
                else
                {
                    Console.WriteLine("Try Again..");
                    ShowMenu();
                }
            }
        }
        public void Title()
        {
            string s = "";
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Ajay_ServicePin.txt";
            if (File.Exists(filepath))
            {
                s = File.ReadAllText(filepath);
            }
            if (s != "" || s != null)
            {
                service_Branch_id=Convert.ToInt32(s);
                string query = " Ajay_sp_Service_Branch_Details @service_Branch_id="+s;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Welcome To " + reader["CompanyName"].ToString() + " Of " + reader["BranchName"].ToString() + " Branch");
                }
                reader.Close();
                con.Close();
            }


        }
        public void DisplayQuestion(int menuid)
        {
            //Console.WriteLine("Menuid" + menuid);
            string query = "exec Ajay_Display_Question @menuid="+menuid ;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open(); 
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 1;
            while (reader.Read())
            {
                QuestionAnswers Qa= new QuestionAnswers();
                Qa.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                string Answer = consolereader.getstring(count.ToString() +". "+ reader["Question"].ToString());
                Qa.FullAnswer = Answer;
                QAList.Add(Qa);
                count++;
            }
            reader.Close();
            con.Close();
            convertToXML();
            SaveToDatabase();

        }
        public XElement xml;
        public void convertToXML()
        {
            xml = new XElement("Root",
               from Qa in QAList
               select new XElement("Ansdetail",
                                  new XElement("QuestionId", Qa.QuestionId),
                                  new XElement("FullAnswer", Qa.FullAnswer)
                              ));
        }

        public void SaveToDatabase()
        {
            string query = "exec Create_TokenId '" + service_Branch_id + "','" + xml + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            string Tokenno="";
            string CounterNo = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Tokenno = reader["Tokenno"].ToString();
                CounterNo = reader["CounterNo"].ToString();
            }
            Console.WriteLine("Your Token no is " + Tokenno +" And Counter No."+ CounterNo);
        }
    }
}
