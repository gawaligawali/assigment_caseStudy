using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Day39CaseStudy.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day39CaseStudy.Services.UserInterface
{
    public class UserInterfaceCrudCategoryService
    {

        //CrudCategoryService _categoryService;           // TIGHTLY BOUND. VERY BAD

        readonly ICrudService<Category> _categoryService; // LOOSELY BOUND. VERY GOOD

        public UserInterfaceCrudCategoryService()
        {
            //_categoryService = new CrudCategoryService();       // TIGHTLY BOUND. VERY BAD

            _categoryService = CrudFactory.Create<Category>();    // LOOSELY BOUND. VERY GOOD
        }

        public void Add()
        {
            Console.WriteLine("Adding New Category");
            Console.WriteLine("----------------");

            Console.Write("Enter Category Name: ");
            var categoryNameText = Console.ReadLine();


            var category = new Category 
                                {
                                     CategoryName = categoryNameText
                                 };

            _categoryService.Add(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryService.GetAll();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            var brands = _categoryService.GetAll();

            Console.WriteLine("Category List");
            Console.WriteLine("----------");

            Console.WriteLine(Category.Header);
            Console.WriteLine("------------------");
            foreach (var brand in brands)
            {
                Console.WriteLine(brand);
            }
            Console.WriteLine("------------------");
        }
    }
}
