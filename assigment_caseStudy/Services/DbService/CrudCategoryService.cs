using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;

namespace Day39CaseStudy.Services.DbService.Interfaces;

public class CrudCategoryService : ICrudService<Category>
{
    public void Add(Category category)
    {
        using var context = new SampleStoreDbContext();
        context.Add(category);
        context.SaveChanges();
    }

    public void Delete(int catId)
    {

        using var context = new SampleStoreDbContext();
        var category = from c in context.Categories
                       where c.CategoryId == catId
                       select c;

        if (category == null)
        {
            Console.WriteLine($"Category Id {catId} not found");
            return;
        }


        context.Categories.Remove(category.SingleOrDefault());
        context.SaveChanges();

    }

    public IEnumerable<Category> GetAll()
    {
        using var context = new SampleStoreDbContext();

        var category = from Category in context.Categories.ToList()
                    select Category;
        return category;
    }

    public Category GetByName(string categoryName)
    {

        using var context = new SampleStoreDbContext();
        var category = from c in context.Categories
                       where c.CategoryName == categoryName
                       select c;

        return category.SingleOrDefault();
    }

    public void Update(Category category)
    {

        using var context = new SampleStoreDbContext();

        context.Categories.Update(category);
        context.SaveChanges();
    }
}

