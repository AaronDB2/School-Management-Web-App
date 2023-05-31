using System;
using System.Collections.Generic;
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

		public string CurrentPassword { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
	}
}
