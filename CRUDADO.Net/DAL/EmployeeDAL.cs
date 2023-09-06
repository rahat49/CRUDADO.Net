using CRUDADOdotNet.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDADOdotNet.Data
{
    public class EmployeeDAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get;set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("AppConn");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[GET_EMPLOYEES]";
                _connection.Open();
                SqlDataReader list = _command.ExecuteReader();
                while (list.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(list["Id"]);
                    employee.FirstName = list["FirstName"].ToString();
                    employee.LastName = list["LastName"].ToString();
                    employee.DateofBirth = Convert.ToDateTime(list["DateofBirth"]).Date;
                    employee.Email = list["Email"].ToString();
                    employee.Salary = Convert.ToDouble(list["Salary"]);
                    employees.Add(employee);
                }
                _connection.Close();
            }
            return employees;

        }
        public bool Insert (Employee employee)
        {
            int id = 0;
            using (_connection =new SqlConnection(GetConnectionString()))
            {
               _command=_connection.CreateCommand();
               _command.CommandType=CommandType.StoredProcedure;
               _command.CommandText = "[INSERT_EMPLOYEE]";

                _command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                _command.Parameters.AddWithValue("@LastName", employee.LastName);
                _command.Parameters.AddWithValue("@DateofBirth", employee.DateofBirth);
                _command.Parameters.AddWithValue("@Email", employee.Email);
                _command.Parameters.AddWithValue("@Salary", employee.Salary);
                
                _connection.Open();
                id=_command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[GET_EMPLOYEEBYID]";
                _command.Parameters.AddWithValue ("@Id", id);
                _connection.Open();
                SqlDataReader list = _command.ExecuteReader();
                while (list.Read())
                {
                    employee.Id = Convert.ToInt32(list["Id"]);
                    employee.FirstName = list["FirstName"].ToString();
                    employee.LastName = list["LastName"].ToString();
                    employee.DateofBirth = Convert.ToDateTime(list["DateofBirth"]).Date;
                    employee.Email = list["Email"].ToString();
                    employee.Salary = Convert.ToDouble(list["Salary"]);
                }
                _connection.Close();
            }
            return employee;

        }
        public bool Update(Employee employee)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[UPDATE_EMPLOYEE]";
                _command.Parameters.AddWithValue("@Id", employee.Id);
                _command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                _command.Parameters.AddWithValue("@LastName", employee.LastName);
                _command.Parameters.AddWithValue("@DateofBirth", employee.DateofBirth);
                _command.Parameters.AddWithValue("@Email", employee.Email);
                _command.Parameters.AddWithValue("@Salary", employee.Salary);

                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            int count= 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DELETE_EMPLOYEE]";
                _command.Parameters.AddWithValue("@Id",id);
                _connection.Open();
                count = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return count > 0 ? true:false;
        }
    }
}
