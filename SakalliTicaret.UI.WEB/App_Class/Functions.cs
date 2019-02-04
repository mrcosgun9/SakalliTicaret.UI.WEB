using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;

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
        public string UrlClear(string metin)
        {
            try
            {
                char tırnak = '"';
                string strReturn = metin.Trim();
                strReturn = strReturn.Replace("ğ", "g");
                strReturn = strReturn.Replace("Ğ", "G");
                strReturn = strReturn.Replace("ü", "u");
                strReturn = strReturn.Replace("Ü", "U");
                strReturn = strReturn.Replace("ş", "s");
                strReturn = strReturn.Replace("Ş", "S");
                strReturn = strReturn.Replace("ı", "i");
                strReturn = strReturn.Replace("İ", "I");
                strReturn = strReturn.Replace("ö", "o");
                strReturn = strReturn.Replace("Ö", "O");
                strReturn = strReturn.Replace("ç", "c");
                strReturn = strReturn.Replace("Ç", "C");
                strReturn = strReturn.Replace("-", "+");
                strReturn = strReturn.Replace(" ", "-");
                strReturn = strReturn.Replace("'", "");
                strReturn = strReturn.Replace("%", "");
                strReturn = strReturn.Replace("<", "");
                strReturn = strReturn.Replace(">", "");
                strReturn = strReturn.Replace("?", "");
                strReturn = strReturn.Replace("!", "");
                strReturn = strReturn.Replace(":", "");
                strReturn = strReturn.Replace("/", "");
                strReturn = strReturn.Replace("\"", "-");
                strReturn = strReturn.Replace(tırnak.ToString(), "");
                strReturn = strReturn.Trim();
                strReturn = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9+]").Replace(strReturn, "");
                strReturn = strReturn.Trim();
                strReturn = strReturn.Replace("+", "-");
                return strReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                            //var productPropertyList = db.prod.Include(x=>x.PropertyPropertyValues).Where(x => x.OrderProductId == item.Id).ToList();
                            //basketItem.PropertyPropertyValueses = productPropertyList;
                            //foreach (var productProperty in productPropertyList)
                            //{
                            //    basketItem.PropertyPropertyValueses.Add(productProperty.PropertyPropertyValues);

                            //}
                            //todo:Ürün özellikleri veri tabanından sepete aktarılacak                            
                            basketClass.SepeteEkle(basketItem);
                        }
                    }
                    basketClass = (BasketClass)HttpContext.Current.Session["AktifSepet"];
                    //basketClass = (BasketClass)HttpContext.Current.Session["AktifSepet"];
                    //basketClass = new BasketClass();
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
                if (item.PropertyPropertyValueses != null)
                {
                    foreach (var propertyValuese in item.PropertyPropertyValueses)
                    {
                        OrderProductProperty productProperty = new OrderProductProperty();
                        productProperty.OrderProductId = orderProduct.Id;
                        productProperty.PropertyPropertyValuesId = propertyValuese.Id;
                        productProperty.CreateDateTime = DateTime.Now;
                        productProperty.CreateUserId = _loginState.IsLoginUser().Id;
                        db.OrderProductProperties.Add(productProperty);
                        db.SaveChanges();
                    }
                }



            }

            //var basketItem = basketClass.BasketItems.Select(x => x.PropertyPropertyValueses);
            //foreach (var item in basketItem)
            //{
            //    if (item.Count > 0)
            //    {
            //        OrderProductProperty productProperty = new OrderProductProperty();
            //        foreach (var propertyValuese in item)
            //        {
            //            //productProperty.OrderProductId = orderProduct.Id;
            //            //productProperty.ProductPropertyId = propertyValuese.Id;
            //        }
            //        db.SaveChanges();
            //        //productProperty.OrderProductId=item.
            //    }
            //}


        }
    }
}