using AgriEnergyConnect.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Models
{
    public static class Utils
    {
        public static string GetAbsolutePath(HttpContext context, string relativePath)
        {
            return $"{context.Request.Scheme}://{context.Request.Host}/{relativePath}";
        }

        public static bool EmployeeExists(string userName, DbSet<Employee> employees)
        {
            bool foundItem = false;

            foreach(Employee employee in employees)
            {
                if(employee.UserName == userName)
                {
                    foundItem = true;
                    break;
                }
            }

            return foundItem;
        }

        public static bool FarmerExists(string userName, DbSet<Farmer> farmers)
        {
            bool foundItem = false;

            foreach (Farmer farmer in farmers)
            {
                if (farmer.UserName == userName)
                {
                    foundItem = true;
                    break;
                }
            }

            return foundItem;
        }

        public static int FindEmployee(string userName, DbSet<Employee> employees)
        {
            var index = -1;
            var items = employees.ToArray();

            for(int i = 0; i < items.Length; i++)
            {
                if (items[i].UserName == userName)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public static int FindFarmer(string userName, DbSet<Farmer> farmers)
        {
            var index = -1;
            var items = farmers.ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].UserName == userName)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        public static int FindFarmer(int farmerID, DbSet<Farmer> farmers)
        {
            var index = -1;
            var items = farmers.ToArray();

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].FarmerID == farmerID)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
    }
}
