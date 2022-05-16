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
            Console.WriteLine("----------------------------");

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
            Console.WriteLine("Updating existing category");
            Console.WriteLine("----------------------------");

            Console.Write("Enter Category Name to Update: ");
            var catNameText = Console.ReadLine();

            var cat = _categoryService.GetByName(catNameText);

            if (cat == null)
            {
                Console.WriteLine($"Category Name {catNameText} not found!!");
                return;
            }

            Console.WriteLine($"Found Category: {cat}");

            Console.Write("Enter Brand Name to change: ");
            var changedCatNameText = Console.ReadLine();

            cat.CategoryName = changedCatNameText;

            _categoryService.Update(cat);
        }

        public void Delete()
        {
            Console.WriteLine("Deleting existing category");
            Console.WriteLine("----------------------------");

            Console.Write("Enter the Caytegory Id to delete: ");
            var catIdText = Console.ReadLine();

            var catId = int.Parse(catIdText);

            try
            {
                _categoryService.Delete(catId);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Delete Category Failed!! {ex.Message}");
                Console.ResetColor();
            }
        }

        public void Show()
        {
            var brands = _categoryService.GetAll();

            Console.WriteLine("Category List");
            Console.WriteLine("----------------------------");

            Console.WriteLine(Category.Header);
            Console.WriteLine("----------------------------");
            foreach (var brand in brands)
            {
                Console.WriteLine(brand);
            }
            Console.WriteLine("----------------------------");
        }
    }
}
