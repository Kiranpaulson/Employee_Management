using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMPLOYEE_MANAGEMENT.Application.Absractions.Repositories
{
    /// <summary>
    /// Provides a generic contract for standard CRUD operations on any entity type.
    /// </summary>
    /// <typeparam name="T">The entity type, which must be a reference type.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities of the specified type.
        /// </summary>
        /// <returns>A list containing all entities.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Retrieves a single entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Creates a new entity and saves it to the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity with any generated values.</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>The updated entity.</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The deleted entity.</returns>
        Task<T> DeleteAsync(T entity);
    }
}
