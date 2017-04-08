using System.Collections.Generic;

namespace DigitalLeader.ViewModels.Localization
{
	public interface ILocalizedModel
	{

	}
	public interface ILocalizedModel<TLocalizedModel> : ILocalizedModel
	{
		IList<TLocalizedModel> Locales { get; set; }
	}
}
