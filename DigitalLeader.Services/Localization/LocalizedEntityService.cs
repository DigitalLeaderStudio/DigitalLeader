using DigitalLeader.DAL;
using DigitalLeader.Entities;
using DigitalLeader.Entities.Interfaces;
using DigitalLeader.Services.Helpers;
using EntityFramework.DbContextScope.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DigitalLeader.Services.Localization
{
	public class LocalizedEntityService : ILocalizedEntityService
	{
		#region Constants

		/// <summary>
		/// Key for caching
		/// </summary>
		/// <remarks>
		/// {0} : language ID
		/// {1} : entity ID
		/// {2} : locale key group
		/// {3} : locale key
		/// </remarks>
		private const string LOCALIZEDPROPERTY_KEY = "Nop.localizedproperty.value-{0}-{1}-{2}-{3}";
		/// <summary>
		/// Key for caching
		/// </summary>
		private const string LOCALIZEDPROPERTY_ALL_KEY = "Nop.localizedproperty.all";
		/// <summary>
		/// Key pattern to clear cache
		/// </summary>
		private const string LOCALIZEDPROPERTY_PATTERN_KEY = "Nop.localizedproperty.";

		#endregion

		#region Fields

		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public Expression<Func<LocalizedProperty, object>>[] Includes => throw new NotImplementedException();

		#endregion

		#region Ctor
		public LocalizedEntityService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}
		#endregion

		#region Utilities

		/// <summary>
		/// Gets localized properties
		/// </summary>
		/// <param name="entityId">Entity identifier</param>
		/// <param name="localeKeyGroup">Locale key group</param>
		/// <returns>Localized properties</returns>
		protected virtual IList<LocalizedProperty> GetLocalizedProperties(int entityId, string localeKeyGroup)
		{
			if (entityId == 0 || string.IsNullOrEmpty(localeKeyGroup))
				return new List<LocalizedProperty>();

			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = from lp in dbContext.Set<LocalizedProperty>()
							orderby lp.ID
							where lp.EntityId == entityId &&
								  lp.LocaleKeyGroup == localeKeyGroup
							select lp;
				var props = query.ToList();

				return props;
			}
		}

		///// <summary>
		///// Gets all cached localized properties
		///// </summary>
		///// <returns>Cached localized properties</returns>
		//protected virtual IList<LocalizedPropertyForCaching> GetAllLocalizedPropertiesCached()
		//{
		//	//cache
		//	string key = string.Format(LOCALIZEDPROPERTY_ALL_KEY);
		//	return _cacheManager.Get(key, () =>
		//	{
		//		var query = from lp in _localizedPropertyRepository.Table
		//					select lp;
		//		var localizedProperties = query.ToList();
		//		var list = new List<LocalizedPropertyForCaching>();
		//		foreach (var lp in localizedProperties)
		//		{
		//			var localizedPropertyForCaching = new LocalizedPropertyForCaching
		//			{
		//				Id = lp.Id,
		//				EntityId = lp.EntityId,
		//				LanguageId = lp.LanguageId,
		//				LocaleKeyGroup = lp.LocaleKeyGroup,
		//				LocaleKey = lp.LocaleKey,
		//				LocaleValue = lp.LocaleValue
		//			};
		//			list.Add(localizedPropertyForCaching);
		//		}
		//		return list;
		//	});
		//}

		#endregion

		#region Nested classes

		[Serializable]
		public class LocalizedPropertyForCaching
		{
			public int Id { get; set; }
			public int EntityId { get; set; }
			public int LanguageId { get; set; }
			public string LocaleKeyGroup { get; set; }
			public string LocaleKey { get; set; }
			public string LocaleValue { get; set; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Deletes a localized property
		/// </summary>
		/// <param name="localizedProperty">Localized property</param>
		public virtual void Delete(LocalizedProperty localizedProperty)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<LocalizedProperty>()
					.SingleOrDefault(item => item.ID == localizedProperty.ID);

				if (existed != null)
				{
					dbContext.Set<LocalizedProperty>().Remove(existed);
					scope.SaveChanges();
				}
			}
			//if (localizedProperty == null)
			//	throw new ArgumentNullException("localizedProperty");

			//_localizedPropertyRepository.Delete(localizedProperty);

			////cache
			//_cacheManager.RemoveByPattern(LOCALIZEDPROPERTY_PATTERN_KEY);
		}

		/// <summary>
		/// Gets a localized property
		/// </summary>
		/// <param name="localizedPropertyId">Localized property identifier</param>
		/// <returns>Localized property</returns>
		public virtual LocalizedProperty GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<LocalizedProperty>()
					.SingleOrDefault(lp => lp.ID == id);
			}

			//if (localizedPropertyId == 0)
			//	return null;

			//return _localizedPropertyRepository.GetById(localizedPropertyId);
		}

		/// <summary>
		/// Find localized value
		/// </summary>
		/// <param name="languageId">Language identifier</param>
		/// <param name="entityId">Entity identifier</param>
		/// <param name="localeKeyGroup">Locale key group</param>
		/// <param name="localeKey">Locale key</param>
		/// <returns>Found localized value</returns>
		public virtual string GetLocalizedValue(int languageId, int entityId, string localeKeyGroup, string localeKey)
		{
			//if (_localizationSettings.LoadAllLocalizedPropertiesOnStartup)
			//{
			//	string key = string.Format(LOCALIZEDPROPERTY_KEY, languageId, entityId, localeKeyGroup, localeKey);
			//	return _cacheManager.Get(key, () =>
			//	{
			//		//load all records (we know they are cached)
			//		var source = GetAllLocalizedPropertiesCached();
			//		var query = from lp in source
			//					where lp.LanguageId == languageId &&
			//					lp.EntityId == entityId &&
			//					lp.LocaleKeyGroup == localeKeyGroup &&
			//					lp.LocaleKey == localeKey
			//					select lp.LocaleValue;
			//		var localeValue = query.FirstOrDefault();
			//		//little hack here. nulls aren't cacheable so set it to ""
			//		if (localeValue == null)
			//			localeValue = "";
			//		return localeValue;
			//	});

			//}
			//else
			//{
			//gradual loading
			//string key = string.Format(LOCALIZEDPROPERTY_KEY, languageId, entityId, localeKeyGroup, localeKey);
			//return _cacheManager.Get(key, () =>
			//{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var source = dbContext.Set<LocalizedProperty>();
				var query = from lp in source
							where lp.LanguageId == languageId &&
							lp.EntityId == entityId &&
							lp.LocaleKeyGroup == localeKeyGroup &&
							lp.LocaleKey == localeKey
							select lp.LocaleValue;
				var localeValue = query.FirstOrDefault();
				//little hack here. nulls aren't cacheable so set it to ""
				if (localeValue == null)
					localeValue = "";
				return localeValue;
			}

			//});
			//}
		}

		/// <summary>
		/// Inserts a localized property
		/// </summary>
		/// <param name="localizedProperty">Localized property</param>
		public virtual void Insert(LocalizedProperty value)
		{
			//if (localizedProperty == null)
			//	throw new ArgumentNullException("localizedProperty");

			//_localizedPropertyRepository.Insert(localizedProperty);

			////cache
			//_cacheManager.RemoveByPattern(LOCALIZEDPROPERTY_PATTERN_KEY);

			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<LocalizedProperty>().Add(value);

				scope.SaveChanges();
			}
		}

		/// <summary>
		/// Updates the localized property
		/// </summary>
		/// <param name="localizedProperty">Localized property</param>
		public virtual void Update(LocalizedProperty value)
		{
			//if (localizedProperty == null)
			//	throw new ArgumentNullException("localizedProperty");

			//_localizedPropertyRepository.Update(localizedProperty);

			////cache
			//_cacheManager.RemoveByPattern(LOCALIZEDPROPERTY_PATTERN_KEY);

			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<LocalizedProperty>()
					.SingleOrDefault(lp => lp.ID == value.ID);

				existed.EntityId = value.EntityId;
				existed.LanguageId = value.LanguageId;
				existed.LocaleKey = value.LocaleKey;
				existed.LocaleKeyGroup = value.LocaleKeyGroup;
				existed.LocaleValue = value.LocaleValue;

				scope.SaveChanges();
			}
		}

		/// <summary>
		/// Save localized value
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="entity">Entity</param>
		/// <param name="keySelector">Key selector</param>
		/// <param name="localeValue">Locale value</param>
		/// <param name="languageId">Language ID</param>
		public virtual void SaveLocalizedValue<T>(T entity,
			Expression<Func<T, string>> keySelector,
			string localeValue,
			int languageId) where T : IEntity, ILocalizedEntity
		{
			SaveLocalizedValue<T, string>(entity, keySelector, localeValue, languageId);
		}

		public virtual void SaveLocalizedValue<T, TPropType>(T entity,
			Expression<Func<T, TPropType>> keySelector,
			TPropType localeValue,
			int languageId) where T : IEntity, ILocalizedEntity
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			if (languageId == 0)
				throw new ArgumentOutOfRangeException("languageId", "Language ID should not be 0");

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

			string localeKeyGroup = typeof(T).Name;
			string localeKey = propInfo.Name;

			var props = GetLocalizedProperties(entity.ID, localeKeyGroup);
			var prop = props.FirstOrDefault(lp => lp.LanguageId == languageId &&
				lp.LocaleKey.Equals(localeKey, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

			var localeValueStr = CommonHelper.To<string>(localeValue);

			if (prop != null)
			{
				if (string.IsNullOrWhiteSpace(localeValueStr))
				{
					//delete
					Delete(prop);
				}
				else
				{
					//update
					prop.LocaleValue = localeValueStr;
					Update(prop);
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(localeValueStr))
				{
					//insert
					prop = new LocalizedProperty
					{
						EntityId = entity.ID,
						LanguageId = languageId,
						LocaleKey = localeKey,
						LocaleKeyGroup = localeKeyGroup,
						LocaleValue = localeValueStr
					};
					Insert(prop);
				}
			}
		}

		public List<LocalizedProperty> GetAll()
		{
			throw new NotImplementedException();
		}

		public List<LocalizedProperty> GetAllInclude(params Expression<Func<LocalizedProperty, object>>[] includes)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
