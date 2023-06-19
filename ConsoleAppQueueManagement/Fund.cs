using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQueueManagement
{
    internal class Fund:Page
    {
        public Fund(Page previous)
        {
            PageName = "Fund Section";
            PreviousPage = previous;
            menuid = 2;
        }
        public override void Menu()
        {
            ShowPageName();
            base.Menu();
            DisplayQuestion(menuid);
        }
    }
}
