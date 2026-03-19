using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data Source=localhost\\evaleyn5;Initial Catalog=TblTodo;Integrated Security=True");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("*****Todo Uygulaması*****");
                Console.WriteLine("1- Görev Ekle");
                Console.WriteLine("2- Görevleri Listele");
                Console.WriteLine("3- Görev Sil");
                Console.WriteLine("4- Görev Tamamlandı Yap");
                Console.WriteLine("0- Çıkış");
                Console.Write("Seçiminiz: ");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                }

                #region Görev Ekle

                if (choice == 1)
                {
                    Console.Write("Görev Adı: ");
                    string name = Console.ReadLine();

                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO TbsTask (TaskName, IsCompleted) VALUES (@name, 0)", connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Görev eklendi!");
                    Console.ReadKey();
                }

                #endregion


                #region Görev Listele

                if (choice == 2)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM TbsTask", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    connection.Close();

                    Console.WriteLine("*****Görev Listesi*****");

                    foreach (DataRow row in table.Rows)
                    {
                        string status = (bool)row["IsCompleted"] ? "✔️" : "❌";
                        Console.WriteLine($"{row["TaskId"],-5} | {row["TaskName"],-30} | {status}");
                    }
                    Console.WriteLine("********************************");
                    Console.ReadKey();
                }

                #endregion


                #region Görev Sil

                if (choice == 3)
                {
                    Console.Write("Silinecek Görev ID'si: ");
                    int id = int.Parse(Console.ReadLine());

                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM TbsTask WHERE TaskId = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Görev Silindi!");
                    Console.ReadKey();
                }

                #endregion


                #region Görev Tamamlandı Yap

                if (choice == 4)
                {
                    Console.Write("Tamamlanacak Görev ID'si: ");
                    int id = int.Parse(Console.ReadLine());

                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE TbsTask SET IsCompleted = 1 WHERE TaskId = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("Görev tamamlandı olarak işaretlendi!");
                    Console.ReadKey();
                }

                #endregion
            }
        }
    }
}
