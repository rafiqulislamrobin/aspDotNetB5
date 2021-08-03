﻿

using ECommerceSystem.ProductInfo.Business_Object;
using ECommerceSystem.ProductInfo.Unit_of_work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Exceptions;

namespace ECommerceSystem.ProductInfo.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        public ProductService(IProductUnitOfWork productUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork;
        }
        public IList<Product> GetAllProduct()
        {
            var productEntities = _productUnitOfWork.Products.GetAll();

            var products = new List<Product>();

            foreach (var entity in productEntities)
            {
                var product = new Product()
                {
                    Id = entity.Id,
                    Name = entity.Name ,
                    Price = entity.Price
                };
                products.Add(product);
            }
            return products;
        }
        public void CreateProduct(Product product)
        {
            if (product == null)
                throw new InvalidParameterException("Product was not found");

            if (IsNameAlreadyUsed(product.Name))
                throw new DuplicateException("Product Name is Already available");

            _productUnitOfWork.Products.Add(
                new Entity.Product
                {

                    Name = product.Name,
                    Price = product.Price
                });
            _productUnitOfWork.Save();
        }


        private bool IsNameAlreadyUsed(string name) =>
              _productUnitOfWork.Products.GetCount(n => n.Name == name) > 0;

    }
}
