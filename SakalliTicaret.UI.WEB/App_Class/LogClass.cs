using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class LogClass
    {
        private SakalliTicaretDb db = new SakalliTicaretDb();

        public void UserLog(User _user , string action , int SessionUser)
        {
            LogUsers _logUsers = new LogUsers();
            _logUsers.Actions = action;
            _logUsers.CreateDateTime = DateTime.Now;
            _logUsers.CreateUserID = SessionUser;
            _logUsers.Email = _user.Email;
            _logUsers.LastName = _user.LastName;
            _logUsers.Name = _user.Name;
            _logUsers.Password = _user.Password;
            _logUsers.Telephone = _user.Telephone;
            db.LogUsers.Add(_logUsers);
            db.SaveChanges();
        }

        public void CategoryLog(Category _category, string action)
        {
            LogCategory _logCategory = new LogCategory();
            _logCategory.Actions = action;
            _logCategory.CreateDateTime = DateTime.Now;
            _logCategory.CreateUserID = 0;
            _logCategory.Name = _category.Name;
            _logCategory.IsActive = _category.IsActive;
            _logCategory.ParentId = _category.ParentCategoryId;
            db.LogCategories.Add(_logCategory);
            db.SaveChanges();
        }

        public void BasketLog(Basket _basket, string action)
        {
            LogBasket _logBasket = new LogBasket();
            _logBasket.Actions = action;
            _logBasket.CreateDateTime = DateTime.Now;
            _logBasket.CreateUserID = 0;
            _logBasket.Amount = _basket.Amount;
            _logBasket.BasketKey = _basket.BasketKey;
            _logBasket.Quantity = _basket.Quantity;
            _logBasket.UserId = _basket.UserId;
            db.LogBaskets.Add(_logBasket);
            db.SaveChanges();
        }

        public void ProductLog(Product _prodoct, string action)
        {
            LogProduct _logProduct = new LogProduct();
            _logProduct.Actions = action;
            _logProduct.CreateDateTime = DateTime.Now;
            _logProduct.CreateUserID = 0;
            _logProduct.Name = _prodoct.Name;
            _logProduct.Brand = _prodoct.Brand;
            _logProduct.Category = _prodoct.Category;
            _logProduct.CategoryID = _prodoct.CategoryId;
            _logProduct.Definition = _prodoct.Definition;
            _logProduct.Description = _prodoct.Description;
            _logProduct.Discount = _prodoct.Discount;
            _logProduct.ImageUrl = _prodoct.ImageUrl;
            _logProduct.IsActive = _prodoct.IsActive;
            _logProduct.Model = _prodoct.Model;
            _logProduct.Price = _prodoct.Price;
            _logProduct.Stock = _prodoct.Stock;
            _logProduct.Tax = _prodoct.Tax;
            db.LogProducts.Add(_logProduct);
            db.SaveChanges();
        }

    }
}