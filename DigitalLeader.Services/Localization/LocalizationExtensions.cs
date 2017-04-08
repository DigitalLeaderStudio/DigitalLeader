using DigitalLeader.Entities;
using DigitalLeader.Entities.Interfaces;
using DigitalLeader.Services.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace DigitalLeader.Services.Localization
{
	public static class LocalizationExtensions
	{
		public static void GetLocalized<T>(this T entity,
			int languageId,
			bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
			where T : IEntity, ILocalizedEntity
		{
			var props = entity.GetType().GetProperties().Where(
				prop => Attribute.IsDefined(prop, typeof(LocalizedProperty)));

			foreach (var propInfo in props)
			{
				var param = Expression.Parameter(typeof(T));
				var field = Expression.PropertyOrField(param, propInfo.Name);
				var expr = Expression.Lambda<Func<T, string>>(field, param);

				propInfo.SetValue(entity, GetLocalized<T, string>(entity, expr, languageId));
			}
		}

		/// <summary>
		/// Get localized property of an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <param name="entity">Entity</param>
		/// <param name="keySelector">Key selector</param>
		/// <param name="languageId">Language identifier</param>
		/// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
		/// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
		/// <returns>Localized property</returns>
		public static string GetLocalized<T>(this T entity,
			Expression<Func<T, string>> keySelector, int languageId,
			bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
			where T : IEntity, ILocalizedEntity
		{
			return GetLocalized<T, string>(entity, keySelector, languageId, returnDefaultValue, ensureTwoPublishedLanguages);
		}

		/// <summary>
		/// Get localized property of an entity
		/// </summary>
		/// <typeparam name="T">Entity type</typeparam>
		/// <typeparam name="TPropType">Property type</typeparam>
		/// <param name="entity">Entity</param>
		/// <param name="keySelector">Key selector</param>
		/// <param name="languageId">Language identifier</param>
		/// <param name="returnDefaultValue">A value indicating whether to return default value (if localized is not found)</param>
		/// <param name="ensureTwoPublishedLanguages">A value indicating whether to ensure that we have at least two published languages; otherwise, load only default value</param>
		/// <returns>Localized property</returns>
		public static TPropType GetLocalized<T, TPropType>(this T entity,
			Expression<Func<T, TPropType>> keySelector, int languageId,
			bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true)
			where T : IEntity, ILocalizedEntity
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			var member = keySelector.Body as MemberExpression;
			if (member == null)
			{
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					keySelector));
			}

			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
			{
				throw new ArgumentException(string.Format(
					   "Expression '{0}' refers to a field, not a property.",
					   keySelector));
			}

			TPropType result = default(TPropType);
			string resultStr = string.Empty;

			//load localized value
			string localeKeyGroup = typeof(T).Name;
			string localeKey = propInfo.Name;

			if (languageId > 0)
			{
				//ensure that we have at least two published languages
				bool loadLocalizedValue = true;
				if (ensureTwoPublishedLanguages)
				{
					//var lService = EngineContext.Current.Resolve<ILanguageService>();
					//var totalPublishedLanguages = lService.GetAllLanguages().Count;
					loadLocalizedValue = true; // totalPublishedLanguages >= 2;
				}

				//localized value
				if (loadLocalizedValue)
				{
					//var leService = EngineContext.Current.Resolve<ILocalizedEntityService>();
					var leService = DependencyResolver.Current.GetService<ILocalizedEntityService>();
					resultStr = leService.GetLocalizedValue(languageId, entity.ID, localeKeyGroup, localeKey);
					if (!String.IsNullOrEmpty(resultStr))
						result = CommonHelper.To<TPropType>(resultStr);
				}
			}

			//set default value if required
			if (String.IsNullOrEmpty(resultStr) && returnDefaultValue)
			{
				var localizer = keySelector.Compile();
				result = localizer(entity);
			}

			return result;
		}
	}
}
