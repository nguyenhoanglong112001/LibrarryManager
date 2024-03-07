﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class Book
    {
        public string bookid { get; protected set; }
        public string bookname { get; protected set; }
        public string author { get; protected set; }
        public int price { get; protected set; }

        public Book(string bookid, string name,string author,int price) 
        {
            this.bookid = bookid;
            this.bookname = name;
            this.author = author;
            this.price = price;
        }
    }
}
