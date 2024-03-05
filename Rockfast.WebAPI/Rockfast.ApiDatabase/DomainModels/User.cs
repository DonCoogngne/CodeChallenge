using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rockfast.ApiDatabase.DomainModels
{
    [Table("Users", Schema = "dbo")]
    public class User
	{
             
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int UserId { get; set; }

     [Required]
     public string Name { get; set; }

     public List<Todo> Todos { get; set; }

    }
}

