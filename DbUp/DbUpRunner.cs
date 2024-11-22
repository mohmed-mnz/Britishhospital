
using System.Reflection;

namespace DbUp
{
    public class DbUpRunner
    {
        public static void Start(string connectionString)
        {

            var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw result.Error;
            }

        }
    }
}