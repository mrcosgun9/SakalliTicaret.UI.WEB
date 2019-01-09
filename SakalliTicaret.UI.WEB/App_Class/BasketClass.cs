using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SakalliTicaret.Core.Model;
using SakalliTicaret.Core.Model.Entity;

namespace SakalliTicaret.UI.WEB.App_Class
{
    public class BasketClass
    {
        SakalliTicaretDb db = new SakalliTicaretDb();
        public static BasketClass AktifSepet
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx.Session["AktifSepet"] == null)
                    ctx.Session["AktifSepet"] = new Basket();
                return (BasketClass)ctx.Session["AktifSepet"];
            }
        }
        public void SepeteEkle(BasketItem basketItem)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                BasketClass s = (BasketClass)HttpContext.Current.Session["AktifSepet"];
                if (basketItem.Product != null)
                {
                    if (s.Products.Any(x => x.Product.ID == basketItem.Product.ID))
                    {
                        s.Products.FirstOrDefault(x => x.Product.ID == basketItem.Product.ID).Quantity++;
                    }
                    else
                    {
                        s.Products.Add(basketItem);
                    }
                }

                else
                {
                    //tblSiparis siparisKontrol = db.tblSiparis.FirstOrDefault(x => x.MusteriId == musteriId);
                    s.Products.Add(basketItem);
                }

            }
            else
            {
                BasketClass s = new BasketClass();
                s.BasketKey = RandomBasketKey(8);
                s.Products.Add(basketItem);
                HttpContext.Current.Session["AktifSepet"] = s;
            }
        }
        public static string RandomBasketKey(int length)
        {
            string ts = DateTime.Now.ToString("hhmmss");
            string chars = "ST123456789ABCDEFGHJKLMNOPRSTUIVYZWX";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()) + ts;
        }
        public void BasketItemRemove(int id)
        {
            BasketClass s = (BasketClass)HttpContext.Current.Session["AktifSepet"];
            if (s != null)
            {
                BasketItem si = s.Products.FirstOrDefault(x => x.siparisDetayId == id);
                s.Products.Remove(si);
                HttpContext.Current.Session["AktifSepet"] = s;
            }

            //tblSiparisDetay siparisDetay=db.tblSiparisDetay.Where(x=>x.Urun==id&&x.siparis==)


        }
        public void BasketItemUpdate(int id, int adet)
        {
            BasketClass s = (BasketClass)HttpContext.Current.Session["AktifSepet"];
            BasketItem si = s.Products.FirstOrDefault(x => x.Product.ID == id);
            if (s.Products.Any(x => x.Product.ID == si.Product.ID))
                s.Products.FirstOrDefault(x => x.Product.ID == si.Product.ID).Quantity = adet;
            HttpContext.Current.Session["AktifSepet"] = s;
        }
        public void AllClear()
        {
            HttpContext.Current.Session.Remove("AktifSepet");
        }
        public decimal TotalAmount
        {
            get
            {
                decimal Amount = Products.Sum(x => x.Total);
                if (ShippingCost)
                {
                    Amount += 7;
                }

                return Amount;
            }
        }
        public decimal SubTotal
        {
            get
            {
                decimal Amount = Products.Sum(x => x.Total);
                return Amount;
            }
        }
        public decimal KargoTutari
        {
            get
            {
                decimal Amount = 0;
                if (ShippingCost)
                {
                    Amount = 7;
                }

                return Amount;
            }
        }
        private List<BasketItem> BasketItems = new List<BasketItem>();
        public List<BasketItem> Products
        {
            get
            {
                return BasketItems;
            }
            set
            {
                BasketItems = value;
            }
        }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int BasketId { get; set; }
        public string BasketKey { get; set; }
        public bool ShippingCost { get; set; }
        public class BasketItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public double Tax { get; set; }
            public decimal Total
            {
                get
                {
                    decimal Amount;
                    Amount = (decimal)Product.Price;
                    return (decimal)((decimal)(Quantity) * (decimal)Amount * (decimal)(1 - Tax));
                }
                set { throw new NotImplementedException(); }
            }

            public int siparisDetayId { get; set; }
            //public int musteriId { get; set; }
            //public DateTime sepetTarih { get; set; }
            //public int sepetId { get; set; }
            //public int adresId { get; set; }
        }
    }
}