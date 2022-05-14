using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;

namespace Day39CaseStudy.Services.DbService;

public class CrudBrandService : ICrudService<Brand>
{
    public void Add(Brand brand)
    {
        using var context = new SampleStoreDbContext();

        context.Brands.Add(brand);
        context.SaveChanges();
    }

    public IEnumerable<Brand> GetAll()
    {
        using var context = new SampleStoreDbContext();

        var brand = from Brand in context.Brands.ToList() 
                    select Brand;
                   
       return brand;
    }

    public void Update(Brand brand)
    {
        using var context = new SampleStoreDbContext();


        context.Brands.Update(brand);
        context.SaveChanges();
    }

    public Brand GetByName(string brandName)
    {
        using var context = new SampleStoreDbContext();

        // var brand = context.Brands.SingleOrDefault(b => b.BrandName == brandName);

        var brand = from b in context.Brands
                    where b.BrandName == brandName
                    select b;

        return brand.SingleOrDefault();
    }

    public void Delete(int brandId)
    {
        using var context = new SampleStoreDbContext();

        //var brand = context.Brands.Find(brandId);
        var brand = from b in context.Brands
                  where b.BrandId == brandId
                  select b;

        if (brand == null)
        {
            Console.WriteLine($"BrandId {brandId} not found");
            return;
        }


        context.Brands.Remove(brand.SingleOrDefault());
        context.SaveChanges();
    }
}
