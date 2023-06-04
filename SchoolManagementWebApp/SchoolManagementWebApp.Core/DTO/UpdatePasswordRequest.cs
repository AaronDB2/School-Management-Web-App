using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO for request that updates password for ApplicationUser entity
	/// </summary>
	public class UpdatePasswordRequest
	{
		public Guid UserId { get; set; }

		[Required(ErrorMessage = "Current Password can't be blank")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required(ErrorMessage = "Password can't be blank")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password can't be blank")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and confirm password do not match")]
		public string ConfirmPassword { get; set; }
	}
}
