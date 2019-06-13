using System.Collections.Generic;
using Autofac.Extras.Moq;
using Data.Interfaces;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void GetCategories_IsValid()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<ICategoryContext>()
                    .Setup(x => x.GetAllCategories())
                    .Returns(GetSampleCategory());

                CategoryLogic cls = mock.Create<CategoryLogic>();
                List<Category> expected = GetSampleCategory();

                List<Category> actual = cls.GetAllCategories();

                Assert.IsTrue(actual != null);
                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        private List<Category> GetSampleCategory()
        {
            List<Category> output = new List<Category>
            {
                new Category
                {
                    Name = "Medisch"
                },
                new Category
                {
                    Name = "Huishoudelijk"
                }
            };

            return output;
        }
    }
}