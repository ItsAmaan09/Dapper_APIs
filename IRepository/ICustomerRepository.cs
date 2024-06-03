namespace DAPPERCRUD;

public interface ICustomerRepository
{
	public IEnumerable<Customer> GetCustomers();
	public Customer GetCustomerDetails(int id);
	public Customer AddCustomer(Customer customer);
	public void UpdateCustomer(int id,Customer customer);
	public void DeleteCustomer(int id);
}
