using MySql.Data.MySqlClient;

namespace ProblematicSearcher {
    public class PersonsRepository {
        private const string DatabaseName = "myshop";
        private const string User = "store";
        private const string Pass = "123456";
        public static IEnumerable<Person> Query(string sql)
        {
            Logger.Log(sql);
            var persons = new List<Person>();
            
            using var connection = new MySqlConnection
            {
                ConnectionString = $"Database={DatabaseName};Data Source=localhost;User Id={User};Password={Pass}"
            };
            
            connection.Open();
            
            using (var command = new MySqlCommand(sql, connection))
            {
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    persons.Add(new Person((string)reader[0], (string)reader[1]));
                }
                reader.Close();
            }
            
           persons.ForEach(person => Logger.Log(person.ToString()));
            return persons;
        }
    }
}