using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class LineItem
  {
    //This is a Join Table to establish a relationship between a many to many relationship
    [Key]
    public int LineItemId {get;set;}

    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }
  }
}