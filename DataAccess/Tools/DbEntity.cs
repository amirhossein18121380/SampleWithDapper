using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace DataAccess.Tools;

public class DbEntity
{
    public SqlConnection GetConnectionString() => new SqlConnection(ConfigurationHelper.Current.GetConnectionString("Default"));
}

public class ConfigurationHelper
{
    private static IConfiguration? _configuration;
    public static void Configure(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static IConfiguration Current => _configuration;
}
