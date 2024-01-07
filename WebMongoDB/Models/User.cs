using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMongoDB.Models
{
	[Table("Users")]
	public class User
	{
		[Column("Id")]
		[Display(Name = "Code")]
        public Guid Id { get; set; }

		[Column("Name")]
		[Display(Name = "Name")]
        public string Name { get; set; }
    }
}
