using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class BookManager
    {
        public List<Book> listbook = new List<Book>()
        {
            new Book("1","Doraemon","Fujoko",10000),
            new Book("2","Connan","Gosho Aoyama",14000),
            new Book("3","One Piece","Oda",12000),
            new Book("4","Crayon Shin-chan","Yoshito Usui",18000),
            new Book("5","Harry Porter", "J.K. Rowling",21000),
            new Book("6","Hunger Game","Suzanne Collins",16000),
            new Book("7","The Witcher","Andrsej Sapkowski",20000)
        };
        public void BookBorrow(Book book)
        {
            listbook.Remove(book);
        }

        public void BookReturn(Book book)
        {
            listbook.Add(book);
        }
        public void ShowBooks(Action<Book> callback)
        {
            foreach (var book in listbook)
            {
                callback?.Invoke(book);
            }
        }
    }
}
