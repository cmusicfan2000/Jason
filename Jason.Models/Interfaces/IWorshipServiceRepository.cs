using System.Threading.Tasks;

namespace Jason.Models
{
    public interface IWorshipServiceRepository
    {
        /// <summary>
        /// Loads an existing <see cref="IWorshipServiceOrder"/>
        /// asynchronously
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> which returns a <see cref="IWorshipServiceOrder"/>
        /// when complete
        /// </returns>
        Task<IWorshipServiceOrder> LoadAsync();

        /// <summary>
        /// Creates a new <see cref="IWorshipServiceOrder"/>
        /// asynchronously
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> which returns a <see cref="IWorshipServiceOrder"/>
        /// when complete
        /// </returns>
        Task<IWorshipServiceOrder> CreateAsync();

        /// <summary>
        /// Saves a <see cref="IWorshipServiceOrder"/>
        /// asynchronously
        /// </summary>
        /// <param name="service">
        /// The <see cref="IWorshipServiceOrder"/> to save 
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the save operation
        /// </returns>
        Task SaveAsync(IWorshipServiceOrder service);
    }
}