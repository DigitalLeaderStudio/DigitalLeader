namespace DigitalLeader.Services.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public interface IService<T> where T : class
	{
		List<T> GetAll();

		List<T> GetAllInclude(params Expression<Func<T, object>>[] includes);

		Expression<Func<T, object>>[] Includes { get; }

		T GetById(int id);

		void Update(T value);

		void Insert(T value);

		void Delete(T value);
	}
}
