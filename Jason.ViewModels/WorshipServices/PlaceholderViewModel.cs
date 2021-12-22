namespace Jason.ViewModels.WorshipServices
{
    /// <summary>
    /// Represents a placeholder for various single entry items in a worship service (Ex. Welcome)
    /// </summary>
    public class PlaceholderViewModel : WorshipServicePartViewModel
    {
        #region Properties
        /// <summary>
        /// Gets the type of item represented
        /// </summary>
        public ItemsChoiceType Type { get; }
        #endregion

        #region Constructor
        public PlaceholderViewModel(ItemsChoiceType type)
            : base(type)
        {
            Type = type;
        }
        #endregion
    }
}