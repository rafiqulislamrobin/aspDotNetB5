

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
        public (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, int pageSize,
           string searchText, string sortText)
        {
            var productData = _productUnitOfWork.Products.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from product in productData.data
                              select new Product
                              {
                                  Id = product.Id,
                                  Name = product.Name,
                                  Price = product.Price,

                              }).ToList();

            return (resultData, productData.total, productData.totalDisplay);
        }


        public Product GetProduct(int id)
        {

            var student = _productUnitOfWork.Products.GetById(id);
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
            var productEntity = _productUnitOfWork.Products.GetById(product.Id);
            if (productEntity != null)
            {
                productEntity.Id = product.Id;
                productEntity.Name = product.Name;
                productEntity.Price = product.Price;

                _productUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("product is not available");
            

        }
        private bool IsNameAlreadyUsed(string name) =>
      _productUnitOfWork.Products.GetCount(n => n.Name == name) > 0;
        private bool IsNameAlreadyUsed(string name, int id) =>
            _productUnitOfWork.Products.GetCount(n => n.Name == name && n.Id != id) > 0;

        public void DeleteProduct(int id)
        {
            _productUnitOfWork.Products.Remove(id);
            _productUnitOfWork.Save();
        }
    }
}
