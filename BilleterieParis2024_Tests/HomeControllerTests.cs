using BilleterieParis2024.Areas.Customer.Controllers;
using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilleterieParis2024_Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Offers_check_TicketOffers_number_returned_correctly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Ajouter des données initiales dans la base de données
            using (var context = new ApplicationDbContext(options))
            {
                // Seed de la base de données avec des offres
                context.TicketsOffers.AddRange(
                new TicketsOffers
                {
                    Id = 1,
                    OfferName = "Test offre",
                    Description = "Test description",
                    IsAvailable = true,
                    Price = 100,
                    Image = null
                },
                new TicketsOffers
                {
                    Id = 2,
                    OfferName = "Test offre2",
                    Description = "Test description",
                    IsAvailable = true,
                    Price = 200,
                    Image = null
                },

                new TicketsOffers
                {
                    Id = 3,
                    OfferName = "Test offre3",
                    Description = "Test description",
                    IsAvailable = false,
                    Price = 300,
                    Image = null
                });
                context.SaveChanges();

            }

            using (var context = new ApplicationDbContext(options))
            {

                var controller = new HomeController(context);

                // Act
                var result = controller.Offers();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<TicketsOffers>>(viewResult.Model);
                Assert.Equal(2, model.Count); // Vérifie qu'il y a bien 3 offres de tickets
                Assert.Equal("Offers", viewResult.ViewName); // Vérifie la redirection
            }
        }
    }
}
