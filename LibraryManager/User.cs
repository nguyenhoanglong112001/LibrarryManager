using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class User
    {
        public string username {  get;protected set; }
        public DateTime birthdate { get;protected set; }
        public string userid { get;protected set; }
        public int balance { get;protected set; }
        public Dictionary<string, DateTime> books = null; 

        public User(string username, DateTime birthdate, string userid, int balance)
        {
            this.username = username;
            this.birthdate = birthdate;
            this.userid = userid;
            this.balance = balance;
            this.books = new Dictionary<string, DateTime>();
        }

        public bool CheckBalance(Book book)
        {
            if (balance < book.price)
            {
                return false;
            }
            return true;
        }

        public void BrowBook(Book book,DateTime date)
        {
            books.Add(book.bookname,date);
        }

        public void ReturnBook(Book book,int dayborown)
        {
            books.Remove(book.bookname);
            balance -= book.price * dayborown;
        }
    }
}
