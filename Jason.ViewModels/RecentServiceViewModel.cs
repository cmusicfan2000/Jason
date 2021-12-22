using System;
using Windows.Storage;

namespace Jason.ViewModels
{
    public class RecentServiceViewModel : ViewModel
    {
        #region Fields
        private readonly StorageFile model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the recent service
        /// </summary>
        public string Name { get; }
        #endregion

        #region Commands
        // Open command

        // Remove command?
        #endregion

        #region Constructor
        public RecentServiceViewModel(StorageFile model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            Name = model.Name;
        }
        #endregion
    }
}
