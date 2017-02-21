namespace DigitalLeader.Web.MappingProfiles
{
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;

	public class MapperImageConverter
	{
		public static object ImageConverter(FileViewModel viewModel)
		{
			File result = null;

			if (viewModel.File != null && viewModel.File.ContentLength > 0)
			{
				using (var reader = new System.IO.BinaryReader(viewModel.File.InputStream))
				{
					result = new File
					{
						FileName = System.IO.Path.GetFileName(viewModel.File.FileName),
						ContentType = viewModel.File.ContentType,
						Content = reader.ReadBytes(viewModel.File.ContentLength)
					};
				}
			}

			return result;
		}
	}
}