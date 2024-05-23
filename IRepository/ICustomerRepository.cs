namespace DAPPERCRUD;

public interface ICustomerRepository
{
	public Task<IEnumerable<Customer>> GetCustomers();
	public Task<Customer> GetCustomerDetails(int id);
	public Task<Customer> AddCustomer(Customer customer);
	public Task UpdateCustomer(int id,Customer customer);
	public Task DeleteCustomer(int id);
}
