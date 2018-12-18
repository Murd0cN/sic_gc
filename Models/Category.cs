using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arqsi_1160752_1161361_3DF.Models
{
    public class Category
    {
        //Id of the Category
        public int ID { get; set; }
        
        //Name of the Category
        public string Name { get; set; }

        //Parent Category Id
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryID { get; set; }
        
        //Parent Category Reference
        public Category ParentCategory { get; set; }

        //List with the category children
        public ICollection<Category> ChildCategory { get; set; }
    }
}