using System.Collections.Generic;

namespace Arqsi_1160752_1161361_3DF.Dtos
{
    public class CatergoryWithNamesDto
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string ParentCategoryName { get; set; }

        public ICollection<string> ChildCategoriesNames { get; set; }
    }
}