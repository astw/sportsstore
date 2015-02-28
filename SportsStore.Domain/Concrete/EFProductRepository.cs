using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Edmx;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
       /* private EFDbContext context = new EFDbContext();

        public IQueryable<ProductEntity> Products
        {
            get { return context.Prodcts; }
            
        }
        * */

     //   private SportsStoreEntities dbContext = new SportsStoreEntities();
        private dyhomeDBEntities dbContext = new dyhomeDBEntities();

        public IEnumerable<ProductEntity> Products
        {
            get
            {
                var products = new List<ProductEntity>(); 

                foreach (var p in dbContext.Products)
                {
                    products.Add(new ProductEntity
                    {
                        Category = p.Category,
                        Description = p.Description,
                        Name = p.Name,
                        Price = p.Price,
                        ProductID = p.ProductID
                    });
                    
                }

                return products.ToArray();
            }
        }

        public void SaveProduct(ProductEntity product)
        {
            var pd = new Product
            {
                Category = product.Category,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price,
                ProductID = product.ProductID


            };

            if (pd.ProductID == 0)
            {
                dbContext.Products.Add(pd);
            }
            else
            {
                Product dbEntry = dbContext.Products.Find(pd.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            dbContext.SaveChanges(); 
        }
    }
}
