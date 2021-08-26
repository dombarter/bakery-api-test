using BakeryApi.Data;
using BakeryApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BakeryAPI.Test
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanGetAllProducts()
        {

            var builder = new DbContextOptionsBuilder<BakeryDbContext>();
            builder.UseInMemoryDatabase("CanGetAllProducts");
            using (var context = new BakeryDbContext(builder.Options))
            {

                var repo = new ProductRepository(context);

                var product1 = new Product()
                {
                    Name = "Breadcakes",
                    ProductCode = "BDC",
                    ShortDescription = "Small round pieces of bread!",
                    Price = 1.15f,
                    Quantity = 10,
                    HiddenProperty = "random-hidden-property"
                };

                var product2 = new Product()
                {
                    Name = "Scones",
                    ProductCode = "SCN",
                    ShortDescription = "Try me with clotted cream!",
                    Price = 0.75f,
                    Quantity = 5,
                    HiddenProperty = "random-hidden-property"
                };

                // Save the products
                repo.CreateProduct(product1);
                repo.CreateProduct(product2);

                var products = repo.GetProducts(true, 0, float.MaxValue);

                Assert.AreEqual(product1.ProductCode, products[0].ProductCode);
                Assert.AreEqual(product2.ProductCode, products[1].ProductCode);

            }

        }
    }
}
