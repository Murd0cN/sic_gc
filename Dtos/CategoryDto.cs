using System.Collections.Generic;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int ParentCategoryID { get; set; }

        public ICollection<int> ChildCategoriesIDs { get; set; }
    }
}