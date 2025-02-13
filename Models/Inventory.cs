using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class Inventory // Changed from internal to public (for accessibility)
    {
        public static BindingList<Product> Products = new BindingList<Product>();
        public static BindingList<Part> AllParts = new BindingList<Part>();

        // Add Product
        public static void AddProduct(Product product) => Products.Add(product);

        // Remove Product - Ensures product exists before removing
        public static bool RemoveProduct(int id)
        {
            Product productToRemove = LookupProduct(id);
            if (productToRemove != null)
            {
                return Products.Remove(productToRemove);
            }
            return false;
        }

        // Lookup Product
        public static Product LookupProduct(int id) => Products.FirstOrDefault(p => p.ProductID == id);

        // Update Product
        public static void UpdateProduct(int id, Product product)
        {
            var index = Products.IndexOf(LookupProduct(id));
            if (index >= 0) Products[index] = product;
        }

        // Add Part
        public static void AddPart(Part part) => AllParts.Add(part);

        // Delete Part - Ensures part is not associated with any product before deleting
        public static bool DeletePart(Part part)
        {
            if (part == null) return false;

            // Check if the part is used in any product
            foreach (var product in Products)
            {
                if (product.AssociatedParts.Contains(part))
                {
                    return false; // Part is associated, prevent deletion
                }
            }

            return AllParts.Remove(part);
        }

        // Lookup Part
        public static Part LookupPart(int id) => AllParts.FirstOrDefault(p => p.PartID == id);

        // Update Part
        public static void UpdatePart(int id, Part part)
        {
            var index = AllParts.IndexOf(LookupPart(id));
            if (index >= 0) AllParts[index] = part;
        }
    }
}
