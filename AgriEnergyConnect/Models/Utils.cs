using AgriEnergyConnect.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Models
{
    /// <summary>
    /// Static Class: Provides utility functions for AgriEnergyConnect.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Resolves a relative path to an absolute path.
        /// </summary>
        /// <param name="context">The Http context to be used in the process.</param>
        /// <param name="relativePath">The relative path to resolve.</param>
        /// <returns></returns>
        public static string GetAbsolutePath(HttpContext context, string relativePath)
        {
            return $"{context.Request.Scheme}://{context.Request.Host}/{relativePath}";
        }

        /// <summary>
        /// Checks if an employee exists within the database.
        /// </summary>
        /// <param name="userName">The username of the employee.</param>
        /// <param name="employees">A collection of employees.</param>
        /// <returns></returns>
        public static bool EmployeeExists(string userName, DbSet<Employee> employees)
        {
            // Boolean variable.
            bool foundItem = false;

            // Iterate through the collection.
            foreach(Employee employee in employees)
            {
                // Check if the employee's username matches the given username.
                if(employee.UserName == userName)
                {
                    foundItem = true;
                    // Exit the loop.
                    break;
                }
            }

            return foundItem;
        }

        /// <summary>
        /// Checks if a farmer exists within the databasee.
        /// </summary>
        /// <param name="userName">The username of the farmer.</param>
        /// <param name="farmers">A collection of farmers.</param>
        /// <returns></returns>
        public static bool FarmerExists(string userName, DbSet<Farmer> farmers)
        {
            // Boolean variable.
            bool foundItem = false;

            // Iterate through the farmer collection.
            foreach (Farmer farmer in farmers)
            {
                // Check if the username of the farmer matches the given username.
                if (farmer.UserName == userName)
                {
                    foundItem = true;
                    // Exit the loop.
                    break;
                }
            }

            return foundItem;
        }

        /// <summary>
        /// Returns an index value that represents the employee in the collection.
        /// </summary>
        /// <param name="userName">The username of the employee.</param>
        /// <param name="employees">A collection of employees.</param>
        /// <returns></returns>
        public static int FindEmployee(string userName, DbSet<Employee> employees)
        {
            // Variable declarations
            var index = -1;
            var items = employees.ToArray();

            // Iterate through the collection.
            for(int i = 0; i < items.Length; i++)
            {
                // Check if the employee's username equals the given username.
                if (items[i].UserName == userName)
                {
                    index = i;
                    // Exit the loop.
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns an index value that represents the farmer in the collection.
        /// </summary>
        /// <param name="userName">The username of the farmer.</param>
        /// <param name="farmers">A collection of farmers.</param>
        /// <returns></returns>
        public static int FindFarmer(string userName, DbSet<Farmer> farmers)
        {
            // Variable declarations.
            var index = -1;
            var items = farmers.ToArray();

            // Iterate through the collection.
            for (int i = 0; i < items.Length; i++)
            {
                // Check if the farmer's username equals to the given username.
                if (items[i].UserName == userName)
                {
                    index = i;
                    // Exit the loop.
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns an index value that represents the farmer in the collection.
        /// </summary>
        /// <param name="userName">The farmer ID from the database.</param>
        /// <param name="farmers">A collection of farmers.</param>
        /// <returns></returns>
        public static int FindFarmer(int farmerID, DbSet<Farmer> farmers)
        {
            // Variable declarations
            var index = -1;
            var items = farmers.ToArray();

            // Iterate through the collection.
            for (int i = 0; i < items.Length; i++)
            {
                // Check if the farmer's id equals to the given farmer id.
                if (items[i].FarmerID == farmerID)
                {
                    index = i;
                    // Exit the loop.
                    break;
                }
            }

            return index;
        }
    }
}
