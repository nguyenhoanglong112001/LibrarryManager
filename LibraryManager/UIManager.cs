using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
            Console.WriteLine("3. Danh sach da muon");
            Console.WriteLine("4. Dang xuat");
            int select = InputInt("Lua chon cua ban: ");
            if (select == 1)
            {
                MemberInfomation();
            }
            else if ( select == 2)
            {
                ShowlistBook();
            }
            else if (select == 3)
            {
                ShowBorrowList();
            }
            else if (select == 4)
            {
                LoginMenu();
            }
        }


        public void ShowlistBook()
        {
            Console.Clear();
            Console.WriteLine("-----------Danh sach sach-------------------");
            for (int i = 0; i < Program.bookmanager.listbook.Count; i++)
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
            Console.WriteLine($"Username: {Program.user.username}");
            Console.WriteLine($"Ho ten: {Program.user.name}");
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
            string json = File.ReadAllText("Data.json");
            Data data = JsonConvert.DeserializeObject<Data>(json);
            Label:
            string username = InputStr("username: ");
            foreach(var tk in data.users.Values)
            {
                if (username.Equals(tk.username))
                {
                    Console.WriteLine("Da ton tai tai khoan tren vui long nhap lai");
                    goto Label;
                }
            }
            string password = InputStr("Password: ");
            string name = InputStr("Ho ten: ");
            string birthdate = InputStr("Ngay thang nam sinh: ");
            int balance = InputInt("So du: ");

            Program.user = new User(username,DateTime.Parse(birthdate),name,balance,password);
            data.users.Add(username, Program.user);
            string newJson = JsonConvert.SerializeObject(data);
            File.WriteAllText("Data.json", newJson);
            Console.WriteLine("Tao tai khoan thanh cong");
            Console.ReadKey();
            LoginMenu();
        }

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("==========Dang nhap===========");
            string username = InputStr("username: ");
            string password = InputStr("password: ");

            string json = File.ReadAllText("Data.json");
            Data data = JsonConvert.DeserializeObject< Data>(json);

            if (data.users.Any(u => u.Value.username.Equals(username) &&  u.Value.password.Equals(password)))
            {
                Console.WriteLine("Dang nhap thanh cong");
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                Console.WriteLine("tai khoan khong ton tai tren he thong ban co muon dang ky tai khoan nay khong");
                string result = InputStr("Nhap lua chon: ");
                if (result.Equals("Y") || result.Equals("y"))
                {
                    string name = InputStr("Ho ten: ");
                    string birthdate = InputStr("Ngay thang nam sinh: ");
                    int balance = InputInt("So du: ");

                    User newUser = new User(username, DateTime.Parse(birthdate),name, balance,password);
                    data.users.Add(username, newUser);

                    string newjson = JsonConvert.SerializeObject(data);
                    File.WriteAllText("Data.json", newjson);
                    Console.WriteLine("Dang ky tai khoan thanh cong, vui long thuc hien dang nhap lai");
                    Console.ReadKey();
                    LoginMenu();
                }
                else
                {
                    LoginMenu();
                }
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
            int i = 0;
            foreach(KeyValuePair<Book,DateTime> kvp in Program.user.books)
            {
                Console.WriteLine($"{kvp.Key.bookname} , {kvp.Value}");
                i++;
            }
            Console.WriteLine("1. Tra sach");
            Console.WriteLine("2. Quay lai");
            int select = InputInt("Nhap lua chon cua ban: ");
            if (select == 1)
            {
                int index = InputInt("Nhap sach muon tra: ");
                string datereturn = InputStr("Nhap ngay tra sach: ");
                TimeSpan diff = DateTime.Parse(datereturn) - Program.user.books.ElementAt(index - 1).Value;
                int datediff = (int)diff.TotalDays;
                Console.WriteLine("Thong tin sach tra lai: ");
                Console.WriteLine($"{Program.user.books.ElementAt(index - 1).Key.bookname} \t {Program.user.books.ElementAt(index - 1).Key.price * datediff}");
                Console.WriteLine("Xac nhan tra sach");
                string choice = InputStr("Y/y: Dong y , N/n: Tu choi: ");
                if (choice.Equals("Y") || choice.Equals("y"))
                {
                    if (Program.user.CheckBalance(Program.user.books.ElementAt(index - 1).Key, datediff))
                    {
                        Program.bookmanager.BookReturn(Program.user.books.ElementAt(index - 1).Key);
                        Program.user.ReturnBook((Program.user.books.ElementAt(index - 1).Key), datediff);
                        Console.WriteLine("tra sach thanh cong");
                        Console.WriteLine($"So du con lai: {Program.user.balance}");
                        Console.ReadKey();
                        MainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Khong du so du de tra sach vui long nap them tien");
                        Console.ReadKey();
                        MainMenu();
                    }
                }
                else if (choice.Equals("n") || choice.Equals("N"))
                {
                    MainMenu();
                }
            }
        }
    }
}
