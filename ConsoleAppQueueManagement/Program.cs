using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQueueManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {   Page m = new MainMenu(null);
                m.Title();
            do
            {
                

                m.ShowMenu();
                Console.ReadLine();
            } while (true);
        }
    }
}
