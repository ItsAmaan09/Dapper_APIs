using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DAPPERCRUD;

public class CustomerRepository : ICustomerRepository
{
	private readonly DapperContext _context;
	private readonly string connectionString = string.Empty;
	public CustomerRepository()
	{
		this.connectionString = DapperContext.Instance.GetDatabaseConnectionString();
	}

	public IEnumerable<Customer> GetCustomers()
	{
		var query = "SELECT * FROM Customer";
		using (var connection = new SqlConnection(this.connectionString))
		{
			var customers =  connection.Query<Customer>(query);
			return customers.ToList();
		}
	}
	public Customer GetCustomerDetails(int CustomerId)
	{
		var query = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";
		using (var connection = new SqlConnection(this.connectionString))
		{
			var customer =  connection.QuerySingleOrDefault<Customer>(query, new { CustomerId }); // the column must be same with db column
			return customer;
		}
	}
	public Customer AddCustomer(Customer customer)
	{
		var query = "INSERT INTO CUSTOMER (CustomerCode, CustomerName) VALUES (@CustomerCode, @CustomerName)" +
		"SELECT CAST(SCOPE_IDENTITY() as int)";
		var parameters = new DynamicParameters();
		parameters.Add("CustomerCode", customer.CustomerCode, DbType.String);
		parameters.Add("CustomerName", customer.CustomerName, DbType.String);

		using (var connection = new SqlConnection(this.connectionString))
		{
			int id =  connection.QuerySingle<int>(query, parameters);

			var customerCreated = new Customer
			{
				CustomerId = id,
				CustomerCode = customer.CustomerCode,
				CustomerName = customer.CustomerName
			};
			return customerCreated;
		}
	}
	public void UpdateCustomer(int id, Customer customer)
	{
		var query = "UPDATE Customer SET CustomerCode = @CustomerCode, CustomerName = @CustomerName WHERE CustomerId = @CustomerId";

		var parameters = new DynamicParameters();
		parameters.Add("CustomerId", id, DbType.Int32);
		parameters.Add("CustomerCode", customer.CustomerCode, DbType.String);
		parameters.Add("CustomerName", customer.CustomerName, DbType.String);

		using (var connection = new SqlConnection(this.connectionString))
		{
			 connection.Execute(query, parameters);
		}
	}
	public void DeleteCustomer(int CustomerId)
	{
		var query = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
		using (var connection = new SqlConnection(this.connectionString))
		{
			 connection.ExecuteAsync(query, new { CustomerId });
		}
	}
}
