namespace DigitalLeader.Services.Interfaces
{
	using System.Collections.Generic;

	public interface IService<T> where T : class
	{
		List<T> GetAll();

		T GetById(int id);

		void Update(T value);

		void Insert(T value);

		void Delete(T value);
	}
}
