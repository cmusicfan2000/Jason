using System.Threading.Tasks;

namespace Jason.Interfaces.WorshipService
{
    public interface IWorshipServiceRepository
    {
        /// <summary>
        /// Loads an existing <see cref="IWorshipService"/>
        /// asynchronously
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> which returns a <see cref="IWorshipService"/>
        /// when complete
        /// </returns>
        Task<IWorshipService> LoadAsync();

        /// <summary>
        /// Creates a new <see cref="IWorshipService"/>
        /// </summary>
        /// <returns>
        /// An empty <see cref="IWorshipService"/>
        /// </returns>
        IWorshipService Create();

        /// <summary>
        /// Saves a <see cref="IWorshipService"/> asynchronously using the last
        /// known location for it if it has been saved before
        /// </summary>
        /// <param name="service">
        /// The <see cref="IWorshipService"/> to save 
        /// </param>
        Task<bool> SaveAsync(IWorshipService service);

        /// <summary>
        /// Saves a <see cref="IWorshipService"/> asynchronously to a
        /// newly selected location
        /// </summary>
        /// <param name="service">
        /// The <see cref="IWorshipService"/> to save 
        /// </param>
        Task<bool> SaveAsAsync(IWorshipService service);
    }
}