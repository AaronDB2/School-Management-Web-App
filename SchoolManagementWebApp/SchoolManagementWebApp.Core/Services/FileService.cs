using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementWebApp.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementWebApp.Core.Services
{
	public class FileService : IFileService
	{
		public FileStream Download(string fileName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
			var stream = new FileStream(path, FileMode.Open);

			return stream;		
		}

		public async Task<bool> UploadFile(IFormFile assignmentFile) 
		{
			// Read the file and upload it to the UploadedFiles folder
			string path = "";
			try
			{
				if (assignmentFile.Length > 0)
				{
					path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}

					using (var fileStream = new FileStream(Path.Combine(path, assignmentFile.FileName), FileMode.Create))
					{
						await assignmentFile.CopyToAsync(fileStream);
					}

					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				return false;
				throw new Exception("File Copy Failed", ex);
			}
		}
	}
}
