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

        public void UserLog(User _user , string action)
        {
            LogUsers _logUsers = new LogUsers();
            _logUsers.Actions = action;
            _logUsers.CreateDateTime = DateTime.Now;
            _logUsers.CreateUserID = 0;
            _logUsers.User = _user;
            db.LogUsers.Add(_logUsers);
            db.SaveChanges();
        }

        public void CategoryLog(Category _category, string action)
        {
            LogCategory _logCategory = new LogCategory();
            _logCategory.Actions = action;
            _logCategory.CreateDateTime = DateTime.Now;
            _logCategory.CreateUserID = 0;
            _logCategory.Kategori = _category;
            db.LogCategories.Add(_logCategory);
            db.SaveChanges();
        }

        public void BasketLog(Basket _basket, string action)
        {
            LogBasket _logBasket = new LogBasket();
            _logBasket.Actions = action;
            _logBasket.CreateDateTime = DateTime.Now;
            _logBasket.CreateUserID = 0;
            _logBasket.Sepet = _basket;
            db.LogBaskets.Add(_logBasket);
            db.SaveChanges();
        }

        public void ProductLog(Product _prodoct, string action)
        {
            LogProduct _logProduct = new LogProduct();
            _logProduct.Actions = action;
            _logProduct.CreateDateTime = DateTime.Now;
            _logProduct.CreateUserID = 0;
            _logProduct.Urun = _prodoct;
            db.LogProducts.Add(_logProduct);
            db.SaveChanges();
        }

    }
}