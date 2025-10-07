namespace CafeDotNet.Infra.Data.Common.DTOs;

public class DatabaseSettings
{
    private const string DefaultConnectionStringName = "SqlServer";

    public string Provider { get; set; } = DefaultConnectionStringName; 
    public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
}