using System.Data.SqlClient;

namespace StudentManagementSystem
{
    public class DbConnection
    {
        private SqlConnection connection;
        private string connectionString;

        public DbConnection()
        {
            connectionString = "Server=localhost;Database=student_db;User Id=user;Password=password";
            connection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}


namespace StudentManagementSystem
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public int RollNumber { get; set; }

        public void Add()
        {
            string query = "INSERT INTO students (first_name, last_name, class, roll_number) VALUES (@firstName, @lastName, @class, @rollNumber)";
            using (SqlCommand command = new SqlCommand(query, DbConnection.GetConnection()))
            {
                command.Parameters.AddWithValue("@firstName", FirstName);
                command.Parameters.AddWithValue("@lastName", LastName);
                command.Parameters.AddWithValue("@class", Class);
                command.Parameters.AddWithValue("@rollNumber", RollNumber);
                DbConnection.GetConnection().Open();
                command.ExecuteNonQuery();
                DbConnection.GetConnection().Close();
            }
        }

        public void Update()
        {
            string query = "UPDATE students SET first_name = @first