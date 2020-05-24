using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonyCounter.Controllers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MonyCounterTestProj
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestDbOperatorAddUser()
        {
           
            dbOperator.User user = new dbOperator.User(11111, "Poper");

            Assert.IsTrue(user.AddUser(user) == "Good");

        }

        [TestMethod]
        public void TestDbOperatorCheckUser()
        {

            dbOperator.User user = new dbOperator.User(11111, "Poper");
            dbOperator.User user2 = new dbOperator.User(9847389723, "krip");
            user = user.CheckUser(user);
            user2 = user2.CheckUser(user2);
            Assert.IsTrue( user.Id != null);
            Assert.IsTrue(user2.Id == null);


        }


        [TestMethod]
        public void TestDbOperatorGetCategory()
        {
            
            dbOperator.Category category = new dbOperator.Category();
            
            List<dbOperator.Category> categories = new List<dbOperator.Category>();
            List<dbOperator.Category> allCategory = new List<dbOperator.Category>();
            categories.Add(new dbOperator.Category(0, "Еда"));
            categories.Add(new dbOperator.Category(1, "Жилье"));
            categories.Add(new dbOperator.Category(2, "Кафе"));
            categories.Add(new dbOperator.Category(3, "Гигиена"));
            categories.Add(new dbOperator.Category(4, "Спорт"));
            categories.Add(new dbOperator.Category(5, "Здоровье"));
            categories.Add(new dbOperator.Category(6, "Связь"));
            categories.Add(new dbOperator.Category(7, "Одежда"));
            categories.Add(new dbOperator.Category(8, "Такси"));
            categories.Add(new dbOperator.Category(9, "Развлечения"));
            categories.Add(new dbOperator.Category(10, "Транспорт"));
            categories.Add(new dbOperator.Category(11, "Машина"));

            allCategory = category.GetAllCategory();
            //Assert.Equals(category.GetAllCategory(), categories);
            CollectionAssert.AreEqual(allCategory, categories); // для того чтобы это работало - нужно переопределять метод Equal, что впринципе логично



        }



    }
}
