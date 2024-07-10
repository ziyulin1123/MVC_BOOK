using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaTimeDemo.DataAccess.Data;
using TeaTimeDemo.DataAccess.Repository.IRepository;
using TeaTimeDemo.Models;

namespace TeaTimeDemo.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,
        IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):
            base(db) {
            _db = db;        
        }
        //public void Save()
        //{
        //    _db.SaveChanges();
        //}統一寫在unitofwork了

        public void Update(Product obj)
        {
            var objFormDb  = _db.Products.FirstOrDefault(u => u.Id == obj.Id);

            if (objFormDb != null)
            {
                objFormDb.Name = obj.Name;
                objFormDb.Size = obj.Size;
                objFormDb.Price = obj.Price;
                objFormDb.Description = obj.Description;
                objFormDb.CategoryId = obj.CategoryId;
                if (objFormDb.ImageUrl != null)
                {
                    objFormDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
