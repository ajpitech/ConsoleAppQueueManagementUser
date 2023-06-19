using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQueueManagement
{
    internal class Loan:Page
    {
        public Loan(Page previous)
        {
            PageName = "Loan Section";
            PreviousPage = previous;
            menuid= 1;
        }
        public override void Menu()
        {
            ShowPageName();
            base.Menu();
            DisplayQuestion(menuid);

        }
    }
}
