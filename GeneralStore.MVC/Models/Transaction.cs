using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [ForeignKey(nameof(Customer))]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(Product))]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}