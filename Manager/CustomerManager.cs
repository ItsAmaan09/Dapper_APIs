namespace DAPPERCRUD
{
	public class CustomerManager
	{
		private ICustomerRepository _customerRepository;

		public CustomerManager()
		{
			_customerRepository = new CustomerRepository();
		}
		public IEnumerable<Customer> GetCustomers()
		{
			var customers =  _customerRepository.GetCustomers();
			return customers.ToList();
		}
		public Customer GetCustomerDetails(int id)
		{
			var customer =  _customerRepository.GetCustomerDetails(id);
			return customer;
		}
		public Customer AddCustomer(Customer customer)
		{
			var customerCreated =  _customerRepository.AddCustomer(customer);
			return customerCreated;
		}
		public void UpdateCustomer(int id, Customer customer)
		{
			if (IsCustomerExists(id))
			{
				throw new Exception("NotFounde");
			}
			 _customerRepository.UpdateCustomer(id, customer);
		}
		public void DeleteCustomer(int id)
		{
			 _customerRepository.DeleteCustomer(id);
		}

		public bool IsCustomerExists(int id)
		{
			bool result = false;
			var customer =  _customerRepository.GetCustomerDetails(id);

			return true ? customer == null : false;

		}
	}
}