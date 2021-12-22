using System;

namespace Jason.ViewModels.WorshipServices
{
    public class LordsSupperViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly WorshipServiceLordsSupper model;
        #endregion

        #region Properties
        private ScriptureViewModel scripture;
        /// <summary>
        /// Gets the scripture reference for the Lord's Supper
        /// </summary>
        public ScriptureViewModel Scripture
        {
            get
            {
                if (scripture == null &&
                    model.Scripture != null)
                    scripture = new ScriptureViewModel(model.Scripture);

                return scripture;
            }
        }
        #endregion

        #region Constructor
        public LordsSupperViewModel(WorshipServiceLordsSupper model)
            : base(ItemsChoiceType.LordsSupper)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion
    }
}