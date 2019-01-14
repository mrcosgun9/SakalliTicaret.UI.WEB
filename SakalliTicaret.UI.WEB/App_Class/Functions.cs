using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class Functions
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        private LoginState _loginState = new LoginState();

        public string CharactertReplace(string character)
        {
            string characterReplace = Regex.Replace(character, "[^0-9a-zA-Z- ]+", "");
            return characterReplace;
        }

        public void GirisSepetControl()
        {
            Basket basket = new Basket();

            BasketClass basketClass = (BasketClass)HttpContext.Current.Session["AktifSepet"];
            if (_loginState.IsLogin())
            {
                int id = _loginState.IsLoginUser().Id;
                basket = db.Baskets.FirstOrDefault(x => x.UserId == id && x.StatusId == 5);
            }
            else
            {

                basket = db.Baskets.FirstOrDefault(x => x.BasketKey == basketClass.BasketKey);
            }
            if (basketClass != null)
            {
                if (basket == null)
                {

                    basket = new Basket();
                    basket.StatusId = 5;
                    if (_loginState.IsLogin())
                    {
                        basket.UserId = _loginState.IsLoginUser().Id;
                        basket.CreateUserId = _loginState.IsLoginUser().Id;
                    }
                    else
                    {
                        basket.UserId = -1;
                        basket.CreateUserId = -1;
                    }
                    basket.CreateDateTime = DateTime.Now;
                    basket.Amount = basketClass.TotalAmount;
                    basket.BasketKey = basketClass.BasketKey;
                    db.Baskets.Add(basket);
                    db.SaveChanges();

                }
                else
                {
                    List<OrderProduct> dbOrderProduct = db.OrderProducts
                        .Where(x => x.BasketId == basket.Id).ToList();
                    if (dbOrderProduct.Count != 0)
                    {
                        foreach (var item in dbOrderProduct)
                        {
                            Product product = db.Products.Find(item.ProductId);
                            BasketClass.BasketItem basketItem = new BasketClass.BasketItem();
                            basketItem.Product = product;
                            basketItem.Quantity = item.Quantity;
                            basketItem.Tax = 0;
                            basketClass.SepeteEkle(basketItem);
                            db.OrderProducts.Remove(item);
                            db.SaveChanges();
                        }
                    }
                    if (_loginState.IsLogin())
                    {
                        basket.UserId = _loginState.IsLoginUser().Id;
                        basket.CreateUserId = _loginState.IsLoginUser().Id;
                    }
                    basketClass = (BasketClass)HttpContext.Current.Session["AktifSepet"];
                    basket.Amount = basketClass.TotalAmount;
                    db.Entry(basket).State = EntityState.Modified;
                    db.SaveChanges();
                }
                OrderProductControl(basket, basketClass);
            }
            else
            {
                
                if (basket != null)
                {
                    List<OrderProduct> dbOrderProduct = db.OrderProducts
                        .Where(x => x.BasketId == basket.Id).ToList();
                    if (dbOrderProduct.Count != 0)
                    {
                        basketClass = new BasketClass();
                        foreach (var item in dbOrderProduct)
                        {
                            BasketClass.BasketItem basketItem = new BasketClass.BasketItem();
                            Product product = db.Products.FirstOrDefault(x => x.Id == item.ProductId);
                            basketItem.Product = product;
                            basketItem.Quantity = item.Quantity;
                            basketItem.Tax = 0;
                            basketClass?.SepeteEkle(basketItem);
                        }
                    }
                    basketClass = (BasketClass) HttpContext.Current.Session["AktifSepet"];
                    basketClass = new BasketClass();
                    basketClass.BasketId = basket.Id;
                    basketClass.BasketKey = basket.BasketKey;
                    basketClass.UserId = basket.UserId;
                   HttpContext.Current.Session["AktifSepet"] = basketClass;
                }


            }
        }

        public void OrderProductControl(Basket basket, BasketClass basketClass)
        {
            int id = _loginState.IsLoginUser().Id;

            foreach (var item in basketClass.Products)
            {
                OrderProduct orderProduct = new OrderProduct();
                orderProduct.CreateDateTime = DateTime.Now;
                orderProduct.CreateUserId = _loginState.IsLoginUser().Id;
                orderProduct.BasketId = basket.Id;
                orderProduct.InTheBasket = true;
                orderProduct.ProductId = item.Product.Id;
                orderProduct.UserId = _loginState.IsLoginUser().Id;
                orderProduct.Quantity = item.Quantity;
                orderProduct.Amount = (double)item.Total;
                db.OrderProducts.Add(orderProduct);
                db.SaveChanges();
            }
        }
    }
}