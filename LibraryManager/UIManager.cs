using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class UIManager
    {
        public void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            int select = InputInt("lua chon cua ban: ");
            if (select == 1)
            {
                Login();
            }
            else if (select == 2)
            {
                Register();
            }
        }
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("----------Library Manager----------");
            Console.WriteLine("1. Thong tin thanh vien");
            Console.WriteLine("2. Danh sach sach trong thu vien");
            int select = InputInt("Lua chon cua ban: ");
            if (select == 1)
            {
                MemberInfomation();
            }
            else if ( select == 2)
            {
                ShowlistBook();
            }
        }

        public void ShowlistBook()
        {
            Console.Clear();
            Console.WriteLine("-----------Danh sach sach-------------------");
            for (int i=0;i<Program.bookmanager.listbook.Count;i++)
            {
                if (Program.bookmanager.listbook[i] != null)
                {
                    Console.WriteLine($"{i + 1}. {Program.bookmanager.listbook[i].bookname}");
                    //Console.WriteLine($"{Program.bookmanager.listbook[i].bookid}. \t {Program.bookmanager.listbook[i].bookname} \t {Program.bookmanager.listbook[i].author} \t {Program.bookmanager.listbook[i].price}");
                }
            }
            int select = InputInt("Chon sach muon xem chi tiet hoac bam 0 de quay lai menu: ");
            if (select == 0)
            {
                MainMenu();
            }
            BookDetail(Program.bookmanager.listbook[select - 1]);

        }

        public void BookDetail(Book book)
        {
            Console.Clear();
            Console.WriteLine("===========Thong tin sach============");
            Console.WriteLine($"Ten sach: {book.bookname}");
            Console.WriteLine($"Ma so sach: {book.bookid}");
            Console.WriteLine($"Tac gia: {book.author}");
            Console.WriteLine($"Gia: {book.price}/ngay");
            Console.ReadKey();
            Console.WriteLine("1. Muon sach");
            Console.WriteLine("2. Quay lai menu");
            int select = InputInt("Lua chon: ");
            if (select == 1)
            {
                BorrowBookUI(book);
            }
            else if (select == 2)
            {
                ShowlistBook();
            }
        }

        public void BorrowBookUI(Book book)
        {
            string select = BeforePay();
            {
                if (select.Equals("y") || select.Equals("y"))
                {
                    Program.user.BrowBook(book, DateTime.Now);
                    Program.bookmanager.BookBorrow(book);
                    Console.WriteLine($"Ban da muon {book.bookname} thanh cong");
                    ShowlistBook();
                }
                else
                {
                    ShowlistBook();
                }
            }
        }

        public void MemberInfomation()
        {
            Console.Clear();
            Console.WriteLine("----------Member Information----------");
            Console.WriteLine($"Ho ten: {Program.user.username}");
            Console.WriteLine($"Userid: {Program.user.userid}");
            string formattedDate = Program.user.birthdate.ToString("dd/MM/yyyy");
            Console.WriteLine($"Ngay thang nam sinh: {formattedDate}");
            Console.WriteLine($"So du: {Program.user.balance}");
            Console.WriteLine("Danh sach sach da muon: ");
            foreach (var name in Program.user.books.Keys)
            {
                Console.WriteLine($"{name}");
            }
            Console.ReadKey();
            MainMenu();
        }

        public void Register()
        {
            Console.Clear();
            string username = InputStr("Ho ten: ");
            string userid = InputStr("Userid: ");
            string birthdate = InputStr("Ngay thang nam sinh: ");
            int balance = InputInt("So du: ");

            Program.user = new User(username,DateTime.Parse(birthdate),userid,balance);
            Console.WriteLine("Tao tai khoan thanh cong");
            Console.ReadKey();
            LoginMenu();
        }

        public bool CanLogin(string username)
        {
            if (Program.user.username != username)
            {
                return false;
            }
            return true;
        }

        public void Login()
        {
            Console.Clear();
            if (Program.user == null)
            {
                Console.WriteLine("Ban chua tao tai khoan vui long dang ky");
                LoginMenu();
            }
            string username = InputStr("Username: ");
            if (CanLogin(username))
            {
                Console.WriteLine("Chao mung ban den voi chuong trinh quan ly");
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Console.WriteLine("Sai username vui long nhap lai");
                LoginMenu();
            }
        }

        public string InputStr(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

        public int InputInt(string input)
        {
            Console.Write(input);
            return int.Parse(Console.ReadLine());
        }
        
        public string BeforePay()
        {
            Console.WriteLine("Xac nhan muon sach: ");
            return InputStr("Y/y: Dong y, N/n: Tu choi \t");
        }

        public void ShowBorrowList()
        {
            foreach(KeyValuePair<string,DateTime> kvp in Program.user.books)
            {
                Console.WriteLine($"{kvp.Key} , {kvp.Value}");
            }
        }
    }
}
