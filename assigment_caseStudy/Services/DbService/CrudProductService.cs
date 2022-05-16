using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Day39CaseStudy.Services.DbService;

public class CrudProductService : ICrudService<Product>
{
    public void Add(Product product)
    {
        using var context = new SampleStoreDbContext();

        context.Products.Add(product);
        context.SaveChanges();
    }

    public IEnumerable<Product> GetAll()
    {
        using var context = new SampleStoreDbContext();

        //var brand = from Product in context.Products.ToList()


        //var brand = from b in context.Brands
        //            group b by b.BrandName into g
        //            select new Brandnew { Bandname = g.Key};



        ////var prod =
        ////     from p in context.Products
        ////     from b in context.Brands
        ////     where p.BrandId == b.BrandId
        ////     group p by b.BrandId into g
        ////     select new g.();





        //var getProducts = from p in context.Products
        //                  join b in context.Brands
        //                  on p.BrandId equals b.BrandId
        //                  join cat in context.Categories
        //                  on p.CategoryId equals cat.CategoryId
        //                  orderby
        //                      p.BrandId, p.ProductId

        //                  select p;


        var products = (from p in context.Products
                       join b in context.Brands
                         on p.BrandId equals b.BrandId
                       join cat in context.Categories
                       on p.CategoryId equals cat.CategoryId
                       orderby
                             p.BrandId, p.ProductId
                       select new Product
                       {
                           ProductId = p.ProductId, 
                           ProductName = p.ProductName,
                           BrandId = p.BrandId,
                           CategoryId = p.CategoryId,
                           ModelYear = p.ModelYear,
                           ListPrice = p.ListPrice,
                           Brand   = b,
                           Category = cat,
                           
                       }).ToList();

        return products;






       // return getProducts.ToList();



        //return context.Products
        //    .Include("Brand")
        //    .Include("Category")
        //    .OrderBy(p => p.BrandId)
        //        .ThenBy(p => p.ProductId)
        //    .ToList();
    }

    public void Update(Product product)
    {
        using var context = new SampleStoreDbContext();

        context.Products.Update(product);
        context.SaveChanges();
    }

    public Product GetByName(string productName)
    {
        using var context = new SampleStoreDbContext();
        //var product = context.Products.SingleOrDefault(b => b.ProductName == productName);
        var product = from p in context.Products
                      where p.ProductName == productName
                      select p;

        return product.SingleOrDefault();
    }

    public void Delete(int productId)
    {
        using var context = new SampleStoreDbContext();

        //var product = context.Products.Find(productId);
        var product = from p in context.Products
                      where p.ProductId == productId
                      select p;

        if (product == null)
        {
            Console.WriteLine($"ProductId {productId} not found");
            return;
        }

        context.Products.Remove(product.SingleOrDefault());
        context.SaveChanges();
    }
}

public class BrandProduct
{
    public int BrandId { get; set; }
    public string Bandname { get; set; }

    public Product Product { get; set; }
    public override string ToString()
    {
        return $"| {BrandId}|";
    }

}