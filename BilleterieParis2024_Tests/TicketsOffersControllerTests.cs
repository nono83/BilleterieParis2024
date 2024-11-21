using BilleterieParis2024.Areas.Admin.Controllers;
using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Moq;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BilleterieParis2024_Tests
{
    public class TicketsOffersControllerTests
    {
        private IHostingEnvironment _he;

        //Solution avec Mock
        //[Fact]
        //public async Task Create_Post_ValidModel_RedirectsToIndex()
        //{


        //    // Arrange
        //    var mockSet = new Mock<DbSet<TicketsOffers>>();
        //    var mockContext = new Mock<ApplicationDbContext>();

        //    mockContext.Setup(c => c.TicketsOffers).Returns(mockSet.Object);
        //    TicketsOffersController controller = new TicketsOffersController(mockContext.Object, _he);

        //    var model = new TicketsOffers { 
        //        OfferName = "Test offre",
        //        Description = "Test", 
        //        IsAvailable=true,
        //        Price=200, 
        //        Image=""  
        //    };

        //    // Act
        //    var result = await controller.Create(model,null,"Test description");

        //    // Assert
        //    var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        //    Assert.Equal("Index", redirectResult.ActionName);

        //    // Verify that Add and SaveChanges were called
        //    mockSet.Verify(m => m.Add(It.IsAny<TicketsOffers>()), Times.Once);
        //    mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //Solutions avec BDD en mémoire
        [Fact]
        public async Task Index_check_TicketOffers_number_returned_correctly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            //using var context = new ApplicationDbContext(options);
            //var controller = new TicketsOffersController(context, _he);

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
                    IsAvailable = true,
                    Price = 300,
                    Image = null
                });
                context.SaveChanges();
                
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new TicketsOffersController(context, _he);

                // Act
                var result = controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<TicketsOffers>>(viewResult.Model);
                Assert.Equal(3, model.Count); // Vérifie qu'il y a bien 3 offres de tickets

                var redirectResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("Index", redirectResult.ViewName); // Vérifie la redirection
            }
        }


        [Fact]
        public async Task Create_TicketOffer_ValidModel_Correctly()
        {  
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new TicketsOffersController(context, _he);

            var model = new TicketsOffers
            {
                OfferName = "Test offre",
                Description = "Test description",
                IsAvailable = true,
                Price = 200,
                Image = ""
            };

            // Act
            var result = await controller.Create(model, null, "Test description");

            // Assert
            var redirectResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Index", redirectResult.ViewName);

            //Vérifier que le TicketOffer a bien été ajouté dans la base
            Assert.Single(context.TicketsOffers);
            Assert.Equal("Test offre", context.TicketsOffers.First().OfferName);
        }

        [Fact]
        public async Task Edit_TicketOffer_Updates_Correctly()
        {
            //Arrange
            // Configurer le contexte en mémoire
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Ajouter des données initiales dans la base de données
            using (var context = new ApplicationDbContext(options))
            {
                context.TicketsOffers.Add(new TicketsOffers
                {
                    Id=1,
                    OfferName = "Test offre",
                    Description = "Test description",
                    IsAvailable = true,
                    Price = 200,
                    Image = ""
                });
                context.SaveChanges();
            }

            //Act
            // Tester l'action en créant une nouvelle donnée depuis l'action Edit du controller
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new TicketsOffersController(context, _he);

                var updatedTicketOffer = new  TicketsOffers
                {
                    Id=1,
                    OfferName = "Test offre maj",
                    Description = "Test description maj",
                    IsAvailable = false,
                    Price = 300,
                    Image = null
                };

                var result = await controller.Edit(updatedTicketOffer,null,"", "Test description maj");

                //Assert
                //Vérifier les résultats
                var ticketOfferInDb = await context.TicketsOffers.FindAsync(1);

                //Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Test offre maj", ticketOfferInDb.OfferName);
                Assert.Equal(300, ticketOfferInDb.Price);
                Assert.Equal("Test description maj", ticketOfferInDb.Description);
                Assert.Equal(false, ticketOfferInDb.IsAvailable);
            }
        }

        [Fact]
        public async Task Delete_Removes_TicketOffer_And_Redirects()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Configure le contexte en mémoire
            using (var context = new ApplicationDbContext(options))
            {
                // Ajoutez un ticketOffer de test dans la base de données
                var TicketsOffer = new TicketsOffers
                {
                    Id = 1,
                    OfferName = "Test offre",
                    Description = "Test description",
                    IsAvailable = true,
                    Price = 200,
                    Image = null
                };
                context.TicketsOffers.Add(TicketsOffer);
                await context.SaveChangesAsync();
            }

            // Nouveau contexte pour émuler un scénario réel
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new TicketsOffersController(context, _he);

                // Act
                var result = await controller.DeleteConfirmed(1);

                // Assert
                var deletedTicketsOffer = await context.TicketsOffers.FindAsync(1);
                Assert.Null(deletedTicketsOffer); // Le produit doit être supprimé
                var redirectResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("Index", redirectResult.ViewName); // Vérifie la redirection
            }
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_IfTicketOfferDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new TicketsOffersController(context, _he);

                // Act
                var result = await controller.Delete(999999); 

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Details_Get_TicketOffer_And_Redirects()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Configure le contexte en mémoire
            using (var context = new ApplicationDbContext(options))
            {
                // Ajoutez un ticketOffer de test dans la base de données
                var TicketsOffer = new TicketsOffers
                {
                    Id = 1,
                    OfferName = "Test offre",
                    Description = "Test description",
                    IsAvailable = true,
                    Price = 200,
                    Image = null
                };
                context.TicketsOffers.Add(TicketsOffer);
                await context.SaveChangesAsync();
            }

            // Nouveau contexte pour émuler un scénario réel
            using (var context = new ApplicationDbContext(options))
            {
                var controller = new TicketsOffersController(context, _he);

                // Act
                var result = await controller.Details(1);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var TicketsOffer = (TicketsOffers)viewResult.Model;
               Assert.Equal("Test offre", TicketsOffer.OfferName);

                //var redirectResult = Assert.IsType<ViewResult>(result);
                Assert.Equal("Details", viewResult.ViewName); // Vérifie la redirection
            }
        }

        [Fact]
        public async Task Details_ReturnsNotFound_IfTicketOfferDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new TicketsOffersController(context, _he);

                // Act
                var result = await controller.Details(999999);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
