using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoToDos.Web.Models
{
	public class Mission
	{
		[Key]
		public int MissionID { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }

		[Column(TypeName = "varchar(1000)")]
		public string Description { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime? DateToDo { get; set; }

		public DateTime Created { get; set; } = DateTime.Now;

		public bool IsDone { get; set; } = false;

		public int Importance { get; set; } = 1;
	}
}
