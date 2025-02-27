using System;
using MySql.Data.MySqlClient;

class Program
{
    static string connStr = "server=localhost;user=root;database=to_do_list;";

    static void Main(string[] args)
    {
        while (true)
        {

            Console.WriteLine("===========================");
            Console.WriteLine("Willkommen zur To Do Liste");
            Console.WriteLine("===========================");
            Console.WriteLine("Was möchten Sie machen");
            Console.WriteLine("Wählen Sie eine Option:");
            Console.WriteLine("Option 1: Aufgabe hinzufügen, 2: Augabe anzeigen, 3; Augabe löschen, 4: Programm Beenden");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Aufgabehinzufügen();
                    break;

                case "2":
                    AufgabeAnzeigen();
                    break;

                case "3":
                    Augabelöschen();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

            }

        }
    }

    static void Aufgabehinzufügen()
    {
        Console.WriteLine("Bitte geben Sie die Aufgabe ein:");
        string aufgabe = Console.ReadLine();

        using (MySqlConnection conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string sql = "INSERT INTO todo (aufgabe) VALUES (@aufgabe)";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@aufgabe", aufgabe);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Aufgabe erfolgreich hinzugefügt!");
    }

    static void AufgabeAnzeigen()
    {
        using (MySqlConnection conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string sql = "SELECT id, aufgabe FROM todo";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                 Console.WriteLine("Aktuelle Aufgaben:");
                 while (reader.Read())
                {
                    Console.WriteLine($"{reader["id"]}: {reader["aufgabe"]}");
                }
            }
            
        }

        
    }

    static void Augabelöschen()
    { 
        Console.WriteLine("Bitte geben Sie die ID der Aufgabe ein, die Sie löschen möchten:");
        int id = Convert.ToInt32(Console.ReadLine());

        using (MySqlConnection conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string sql = "DELETE FROM todo WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    static void ProgrammBeenden()
    {
        Console.WriteLine("Programm wird beendet.");
        Environment.Exit(0);
    }

}

