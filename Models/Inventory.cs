using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class Inventory 
    {
        public static BindingList<Product> Products = new BindingList<Product>();
        public static BindingList<Part> AllParts = new BindingList<Part>();

        public static void AddProduct(Product product) => Products.Add(product);

        public static bool RemoveProduct(int id)
        {
            Product productToRemove = LookupProduct(id);
            if (productToRemove != null)
            {
                return Products.Remove(productToRemove);
            }
            return false;
        }

        public static Product LookupProduct(int id) => Products.FirstOrDefault(p => p.ProductID == id);

        public static void UpdateProduct(int id, Product product)
        {
            var index = Products.IndexOf(LookupProduct(id));
            if (index >= 0) Products[index] = product;
        }

        public static void AddPart(Part part) => AllParts.Add(part);

        public static bool DeletePart(Part part)
        {
            if (part == null) return false;

            foreach (var product in Products)
            {
                if (product.AssociatedParts.Contains(part))
                {
                    return false; 
                }
            }

            return AllParts.Remove(part);
        }

        public static Part LookupPart(int id) => AllParts.FirstOrDefault(p => p.PartID == id);

        public static void UpdatePart(int id, Part part)
        {
            var index = AllParts.IndexOf(LookupPart(id));
            if (index >= 0) AllParts[index] = part;
        }
    }
}
