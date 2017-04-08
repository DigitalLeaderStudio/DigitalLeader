using DigitalLeader.Entities;
using DigitalLeader.Entities.Interfaces;
using DigitalLeader.Services.Interfaces;
using System;
using System.Linq.Expressions;

namespace DigitalLeader.Services.Localization
{
	/// <summary>
	/// Localized entity service interface
	/// </summary>
	public partial interface ILocalizedEntityService : IService<LocalizedProperty>
	{
		/// <summary>
		/// Find localized value
		/// </summary>
		/// <param name="languageId">Language identifier</param>
		/// <param name="entityId">Entity identifier</param>
		/// <param name="localeKeyGroup">Locale key group</param>
		/// <param name="localeKey">Locale key</param>
		/// <returns>Found localized value</returns>
		string GetLocalizedValue(int languageId, int entityId, string localeKeyGroup, string localeKey);

		/// <summary>
		/// Save localized value
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="entity">Entity</param>
		/// <param name="keySelector">Key selector</param>
		/// <param name="localeValue">Locale value</param>
		/// <param name="languageId">Language ID</param>
		void SaveLocalizedValue<T>(T entity,
			Expression<Func<T, string>> keySelector,
			string localeValue,
			int languageId) where T : IEntity, ILocalizedEntity;

		void SaveLocalizedValue<T, TPropType>(T entity,
		   Expression<Func<T, TPropType>> keySelector,
		   TPropType localeValue,
		   int languageId) where T : IEntity, ILocalizedEntity;
	}
}
