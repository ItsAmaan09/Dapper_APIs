namespace DAPPERCRUD
{
	public class CustomerManager
	{
		private ICustomerRepository _customerRepository;

		public CustomerManager()
		{
			_customerRepository = new CustomerRepository();
		}
		public async Task<IEnumerable<Customer>> GetCustomers()
		{
			var customers = await _customerRepository.GetCustomers();
			return customers.ToList();
		}
		// public async Task<Customer> GetCustomerDetails(int id)
		// {
		// 	var customer = await _customerRepository.GetCustomerDetails(id);
		// 	return customer;
		// }
		// public async Task<Customer> AddCustomer(Customer customer)
		// {
		// 	var customerCreated = await _customerRepository.AddCustomer(customer);
		// 	return customerCreated;
		// }
		// public async Task UpdateCustomer(int id, Customer customer)
		// {
		// 	if (await IsCustomerExists(id))
		// 	{
		// 		throw new Exception("NotFounde");
		// 	}
		// 	await _customerRepository.UpdateCustomer(id, customer);
		// }
		// public async Task DeleteCustomer(int id)
		// {
		// 	await _customerRepository.DeleteCustomer(id);
		// }

		// public async Task<bool> IsCustomerExists(int id)
		// {
		// 	bool result = false;
		// 	var customer = await _customerRepository.GetCustomerDetails(id);

		// 	return true ? customer == null : false;

		// }
	}
}