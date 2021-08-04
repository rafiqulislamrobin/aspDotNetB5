using InventorySystem.Store.Business_Object;
using InventorySystem.Store.Context;
using InventorySystem.Store.Unit_of_work;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Exceptions;

namespace InventorySystem.Store.Services
{
    public class InventoryService : IInvenStoryService
    {
        private readonly IInventoryUnitOfWork _iInventoryUnitOfWork;
        public InventoryService(IInventoryUnitOfWork iInventoryUnitOfWork)
        {
            _iInventoryUnitOfWork = iInventoryUnitOfWork;
        }
        public IList<Product> GetAllProducts()
        {
            var prodcutEntity = _iInventoryUnitOfWork.Products.GetAll();

            var products = new List<Product>();

            foreach (var entity in prodcutEntity)
            {
                var product = new Product()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Price = entity.Price
                };

                products.Add(product);
            }
            return products;

        }

        public void CreateProduct (Product product)
        {
            if (product == null)
                throw new InvalidParameterException("Product was not found");

            if (IsNameAlreadyUsed(product.Name))
                throw new DuplicateException("Product Name");

            _iInventoryUnitOfWork.Products.Add(
                new Entity.Product
                {
                    
                    Name = product.Name,
                    Price = product.Price
                });
            _iInventoryUnitOfWork.Save();
        }
        public void StockChecking(Product product, Stock stock)
        {
            var productEntity = _iInventoryUnitOfWork.Products.GetById(product.Id);

            if (productEntity == null)
            {
                throw new InvalidOperationException("Product was not found");
            }
            if (productEntity.Stocks == null)
                productEntity.Stocks = new List<Entity.Stock>();


            productEntity.Stocks.Add(new Entity.Stock
            {
               productId = stock.ProductId,
               Qunatity  = stock.Qunatity,

            });

            _iInventoryUnitOfWork.Save();


        }
        public (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, int pageSize,
              string searchText, string sortText)
        {
            var productData = _iInventoryUnitOfWork.Products.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name == searchText,
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from product in productData.data
                              select new  Product
                              {
                                  Id = product.Id,
                                  Name = product.Name,
                                  Price = product.Price,
                                  
                              }).ToList();

            return (resultData, productData.total, productData.totalDisplay);
        }

        public Product GetProduct(int id)
        {

            var student = _iInventoryUnitOfWork.Products.GetById(id);
            if (student == null)
            {
                return null;
            }
            return new Product
            {
                Id = student.Id,
                Name = student.Name,
                Price = student.Price,

            };
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new InvalidOperationException("product is missing");
            }
            if (IsNameAlreadyUsed(product.Name, product.Id))
            {
                throw new DuplicateException("product name is already used");
            }
            var productEntity = _iInventoryUnitOfWork.Products.GetById(product.Id);
            if (productEntity != null)
            {
                productEntity.Id = product.Id;
                productEntity.Name = product.Name;
                productEntity.Price = product.Price;

                _iInventoryUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("product is not available");


        }
        private bool IsNameAlreadyUsed(string name) =>
      _iInventoryUnitOfWork.Products.GetCount(n => n.Name == name) > 0;
        private bool IsNameAlreadyUsed(string name, int id) =>
            _iInventoryUnitOfWork.Products.GetCount(n => n.Name == name && n.Id != id) > 0;

        public void DeleteProduct(int id)
        {
            _iInventoryUnitOfWork.Products.Remove(id);
            _iInventoryUnitOfWork.Save();
        }
    } 
}
