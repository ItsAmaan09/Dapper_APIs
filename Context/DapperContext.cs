using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAPPERCRUD;

public class DapperContext
{
	private static DapperContext _instance;
	private IConfiguration _configuration;
	public static DapperContext Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new DapperContext();
			}
			return _instance;
		}
	}
	public void Initialize(IConfiguration configuration)
	{
		this._configuration = configuration;
	}

	public string GetDatabaseConnectionString()
	{
		return this._configuration.GetConnectionString("SqlConnection");
	}
	// private readonly string _connectionString;
	// public DapperContext(IConfiguration configuration)
	// {
	// 	_configuration = configuration;
	// 	_connectionString = _configuration.GetConnectionString("SqlConnection");
	// }
	// public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

}
