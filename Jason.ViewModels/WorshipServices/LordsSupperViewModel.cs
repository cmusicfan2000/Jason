using System;

namespace Jason.ViewModels.WorshipServices
{
    public class LordsSupperViewModel : WorshipServicePartViewModel
    {
        #region Fields
        private readonly LordsSupper model;
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

        public override string DisplayName => $"Lord's Supper: {Scripture.Book} {Scripture.Reference} ({Scripture.Translation})";

        /// <summary>
        /// Gets the text to show on the previous slide when this part is coming up next
        /// </summary>
        public override string CommingNextText => "The Lord's Supper";
        #endregion

        #region Constructor
        public LordsSupperViewModel(LordsSupper model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
        }
        #endregion
    }
}