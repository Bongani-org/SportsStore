using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
