using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace ConsoleAppQueueManagement
{
    internal class MainMenu:Page
    {
        public MainMenu(Page previous)
        {
            PageName = "MainMenu";
            PreviousPage = this;
            //pageList = new List<Page>();
            pageList.Add(new Loan(this));
            pageList.Add(new Fund(this));
            pageList.Add(new ExitPage(this));
        }
        // static Config c = new Config();
        public override void Menu()
        {
        }
       
    }
}
