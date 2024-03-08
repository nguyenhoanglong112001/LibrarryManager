using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibraryManager
{
    public class Program
    {
        public static User user = null;
        public static UIManager uimanager = new UIManager();
        public static BookManager bookmanager = new BookManager();
        public delegate void ShowBookDlg(Action<Book> callback);

        public static ShowBookDlg showbook;
        static void Main(string[] args)
        {
            uimanager.LoginMenu();
        }
    }
}
