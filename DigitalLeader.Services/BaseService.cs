namespace DigitalLeader.Services
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Extensions;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public abstract class BaseService
	{
		/// <summary>
		/// Copy data to original file or set it wit new one
		/// </summary>
		/// <param name="original"></param>
		/// <param name="newItem"></param>
		public File HandleFile(File original, File newItem)
		{
			var result = original;
			//Modify original image. If update whole object, new one will be added so just update nessesary data.
			if (result != null && newItem != null)
			{
				result.FileName = newItem.FileName;
				result.Content = newItem.Content;
				result.ContentType = newItem.ContentType;
			}
			else if (newItem != null)
			{
				result = newItem;
			}

			return result;
		}

		public List<T> HandleCollection<T, TKey>(
			List<T> existed,
			List<T> value,
			Func<T, TKey> getKey,
			ApplicationDbContext dbContext) where T : class
		{
			var result = existed;

			var deletedItems = result.Except(value, getKey).ToList();
			var addedItems = value.Except(result, getKey).ToList();

			foreach (var delItem in deletedItems)
			{
				result.Remove(delItem);
			}

			foreach (var addedItem in addedItems)
			{
				var entry = dbContext.Entry(addedItem);
				if (entry.State == EntityState.Detached)
				{
					dbContext.Set<T>().Attach(addedItem);
				}

				result.Add(addedItem);
			}

			return result;
		}

		public List<T> HandleCollection<T>(List<T> sourse, ApplicationDbContext dbContext) where T : class
		{
			var result = new List<T>();

			foreach (var item in sourse)
			{
				if (dbContext.Entry(item).State == EntityState.Detached)
				{
					dbContext.Set<T>().Attach(item);
				}

				result.Add(item);
			}

			return result;
		}
	}
}
