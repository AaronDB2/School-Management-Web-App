using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.DTO
{
	/// <summary>
	/// DTO class for registering new users
	/// </summary>
	public class RegisterDTO
	{
		[Required(ErrorMessage = "Name can't be blank")]
		public string UserName { get; set; }


		[Required(ErrorMessage = "Email can't be blank")]
		[EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
		//[Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already is use")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password can't be blank")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Required(ErrorMessage = "Confirm Password can't be blank")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and confirm password do not match")]
		public string ConfirmPassword { get; set; }

		public string? Admin { get; set; }

		public string? Teacher { get; set; }
	}
}
