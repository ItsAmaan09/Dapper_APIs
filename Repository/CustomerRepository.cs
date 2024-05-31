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

	public async Task<IEnumerable<Customer>> GetCustomers()
	{
		var query = "SELECT * FROM Customer";
		using (var connection = new SqlConnection(this.connectionString))
		{
			var customers = await connection.QueryAsync<Customer>(query);
			return customers.ToList();
		}
	}
	// public async Task<Customer> GetCustomerDetails(int CustomerId)
	// {
	// 	var query = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";
	// 	using (var connection = _context.CreateConnection())
	// 	{
	// 		var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { CustomerId }); // the column must be same with db column
	// 		return customer;
	// 	}
	// }
	// public async Task<Customer> AddCustomer(Customer customer)
	// {
	// 	var query = "INSERT INTO CUSTOMER (CustomerCode, CustomerName) VALUES (@CustomerCode, @CustomerName)" +
	// 	"SELECT CAST(SCOPE_IDENTITY() as int)";
	// 	var parameters = new DynamicParameters();
	// 	parameters.Add("CustomerCode", customer.CustomerCode, DbType.String);
	// 	parameters.Add("CustomerName", customer.CustomerName, DbType.String);

	// 	using (var connection = _context.CreateConnection())
	// 	{
	// 		int id = await connection.QuerySingleAsync<int>(query, parameters);

	// 		var customerCreated = new Customer
	// 		{
	// 			CustomerId = id,
	// 			CustomerCode = customer.CustomerCode,
	// 			CustomerName = customer.CustomerName
	// 		};
	// 		return customerCreated;
	// 	}
	// }
	// public async Task UpdateCustomer(int id, Customer customer)
	// {
	// 	var query = "UPDATE Customer SET CustomerCode = @CustomerCode, CustomerName = @CustomerName WHERE CustomerId = @CustomerId";

	// 	var parameters = new DynamicParameters();
	// 	parameters.Add("CustomerId", id, DbType.Int32);
	// 	parameters.Add("CustomerCode", customer.CustomerCode, DbType.String);
	// 	parameters.Add("CustomerName", customer.CustomerName, DbType.String);

	// 	using (var connection = _context.CreateConnection())
	// 	{
	// 		await connection.ExecuteAsync(query, parameters);
	// 	}
	// }
	// public async Task DeleteCustomer(int CustomerId)
	// {
	// 	var query = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
	// 	using (var connection = _context.CreateConnection())
	// 	{
	// 		await connection.ExecuteAsync(query, new { CustomerId });
	// 	}
	// }
}
