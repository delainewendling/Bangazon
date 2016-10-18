using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    
    [DataType(DataType.Date)]
    //This value is nullable because you can create an order without paying for it right now. 
    public DateTime? DateCompleted {get;set;}

    // Foreign key reference to the Customer table
    public int CustomerId {get;set;}
    //This is the way of telling the compiler what table it's a foreign key to.
    public Customer Customer {get;set;}

    //Foreign key reference to the Payment table
    //This value is nullable because you can create an order without paying for it yet. This has to match the id of the table PaymentTypeId 
    public int? PaymentTypeId {get;set;}
    //This is the way of telling the compiler what table it's a foreign key to. 
    public PaymentType PaymentType {get;set;} 

    public ICollection<LineItem> LineItems;
  }
}