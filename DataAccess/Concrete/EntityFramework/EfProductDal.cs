using DataAccess.Abstract;
using Entitites.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        //using yazdıgımız zaman kodla işimiz bittiğinde  garbage collector dosyayı bellekten temizler.
        public void Add(Product entity)
        {
            //IDispossable pattern implemantation of c#
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }


        //Lambda kullanabilmemiz için yazılan Expression kodu 
        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using(NorthwindContext context = new NorthwindContext())
            {
                //Ternary Operatörü kullandık. İf else koşulu tek satırda. ? Filter null mı  nullsa : önceki değilse : sonraki çalışır
                return filter == null ? context.Set<Product>().ToList() 
                    : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
