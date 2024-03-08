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
        public string password { get; protected set; }
        public DateTime birthdate { get;protected set; }
        public string name { get;protected set; }
        public int balance { get;protected set; }
        public Dictionary<Book, DateTime> books = null; 

        public User(string username, DateTime birthdate, string name, int balance, string password)
        {
            this.username = username;
            this.birthdate = birthdate;
            this.name = name;
            this.balance = balance;
            this.books = new Dictionary<Book, DateTime>();
            this.password = password;
        }

        public bool CheckBalance(Book book,int day)
        {
            if (balance < book.price * day)
            {
                return false;
            }
            return true;
        }

        public void BrowBook(Book book,DateTime date)
        {
            books.Add(book,date);
        }

        public void ReturnBook(Book book,int dayborown)
        {
            books.Remove(book);
            balance -= book.price * dayborown;
        }
    }
}
