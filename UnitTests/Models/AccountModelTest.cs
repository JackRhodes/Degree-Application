using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Degree_Application.Models;
using System.ComponentModel.DataAnnotations;

namespace Degree_Application_Test.Models
{
    /// <summary>
    /// SCOPE:
    /// Only added fields will be Unit Tested. It is assumed the Microsoft created properties created from IIdentityUser are correct.
    /// 
    /// This will not be present in code coverage report as I am using a Validation method in data annotations namespace.
    /// 
    /// </summary>
    [TestClass]
    public class AccountModelTest
    {
        private AccountModel validAccountModel = new AccountModel()
        {
            Id = "4fbb9ba5-c458-4c30-8afb-ef6495f483b4",
            UserName = "John Smith",
            Email = "john@SmithandCo.co.uk",
            Mobile = "+447476543786",
            PostCode = "bn12 6ea"
        };
        
        private ModelHelper modelHelper = new ModelHelper();
        
        /*https://dotnetliberty.com/index.php/2016/01/04/how-to-unit-test-asp-net-5-mvc-6-modelstate/*/
        [TestMethod]
        public void MobileNumber_ShouldValidate_WhenCorrectFormat()
        {
            //Assign
            AccountModel accountModel = validAccountModel;
            string validNumber = "+447479946217";

            accountModel.Mobile = validNumber;

            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);

            //Assert
            Assert.AreEqual(true, valid);
        }


        [TestMethod]
        public void MobileNumber_ShouldNotValidate_WhenWrongFormat()
        {
            //Assign
            AccountModel accountModel = validAccountModel;
            string validNumber = "087479946217";
            accountModel.Mobile = validNumber;
            
            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);
            
            //Assert
            Assert.AreEqual(false, valid);

        }

        [TestMethod]
        public void MobileNumber_ShouldNotValidate_WhenNull()
        {
            //Assign
            AccountModel accountModel = validAccountModel;
            string nullNumber = null;

            accountModel.Mobile = nullNumber;
            
            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);
            
            //Assert
            Assert.AreEqual(false, valid);

        }

        [TestMethod]
        public void PostCode_ShouldValidate_WhenValid()
        {
            //Assign
            AccountModel accountModel = validAccountModel;
            string validPostCode = "bn13 4bz";
        
            accountModel.PostCode = validPostCode;

            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);

            //Assert
            Assert.AreEqual(true, valid);
            
        }

        [TestMethod]
        public void PostCode_ShotNotValidate_WhenInvalid ()
        {
            //Assign
            AccountModel accountModel = validAccountModel;
            string validPostCode = "bn13a 4bz";

            accountModel.PostCode = validPostCode;

            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);

            //Assert
            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void PostCode_ShouldNotValidate_WhenNull ()
        {
            //Assign
            AccountModel accountModel = validAccountModel;
            string validPostCode = null;

            accountModel.PostCode = validPostCode;

            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);

            //Assert
            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void ProfilePicture_ShouldValidate_WhenNull()
        {
            //Assign
            AccountModel accountModel = validAccountModel;

            accountModel.ProfilePicture = null;

            //Act
            bool valid = modelHelper.CheckModelValidation(accountModel);

            //Assert
            Assert.AreEqual(true, valid);
        }
        
    }
}
