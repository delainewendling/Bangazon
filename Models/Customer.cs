using System;
using System.Collections.Generic;
//Alows us to use the square bracket notation and specifically data annotations like in the square brackets. We need both using statements to create the annotations below
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  //The name of the class is the name of the table in the database
  public class Customer
  {
    //Primary Key of this table
    [Key]
    //CustomerId is the name of the primary key 
    public int CustomerId {get;set;}

    //This field is required 
    [Required]
    //Needs to be of type date, not datetime 
    [DataType(DataType.Date)]
    //I want the database to generate the current timestamp
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    //Needs to be of type date, not datetime 
    [DataType(DataType.Date)]
    //I want the database to generate the current timestamp
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastUpdated {get;set;}

    //Need to specify first name and last name. An error message will be sent back to the user trying to create a post request saying the first or last name is required
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    //This ICollection is establishing the relationship one-to-many between customer and paymentTypes. 
    //This is the 'one' side to the one-to-many relationship
    //It sets up the foreign key relationship 
    public ICollection<PaymentType> PaymentTypes;
  }
}