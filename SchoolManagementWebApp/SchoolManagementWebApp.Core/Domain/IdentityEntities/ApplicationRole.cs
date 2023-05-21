using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.Domain.IdentityEntities
{
	/// <summary>
	/// Model for ApplicationRole entity for database
	/// </summary>
	public class ApplicationRole : IdentityRole<Guid>
	{
	}
}
