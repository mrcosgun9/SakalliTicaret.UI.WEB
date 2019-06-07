using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SakalliTicaret.Core.Model.Entity;
using SakalliTicaret.Helper.Enums;
namespace SakalliTicaret.Helper.ProductOperations
{
    public class ProductOperations : BaseHelper
    {
        public void ProductOperationsCreate(int? productId, int transactionsType, string title, int userId)
        {
            ProductTransaction productTransaction = new ProductTransaction();
            productTransaction.TransactionsType = transactionsType;
            productId = productId == null ? 0 : productId;
            productTransaction.ProductId = (int) productId;
            productTransaction.Actions = title;
            productTransaction.CreateDateTime = DateTime.Now;
            productTransaction.CreateUserId = userId;
            db.ProductTransactions.Add(productTransaction);
            db.SaveChanges();
        }
    }
}
