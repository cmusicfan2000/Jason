using Syncfusion.Presentation;
using System;
using System.Threading.Tasks;

namespace Jason.ViewModels.WorshipServices
{
    public abstract class WorshipServicePartViewModel : ViewModel
    {
        /// <summary>
        /// Gets the name of the part
        /// </summary>
        public string PartName { get; }

        #region Constructor
        public WorshipServicePartViewModel(ItemsChoiceType type)
        {
            switch (type)
            {
                case ItemsChoiceType.FamilyNewsAndPrayer:
                    PartName = "Family News and Prayer";
                    break;
                case ItemsChoiceType.LordsSupper:
                    PartName = "Lord's Supper";
                    break;
                default:
                    PartName = type.ToString();
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        protected virtual Task AddToSection(ISection section) => Task.CompletedTask;

        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        public async Task AddToPresentation(IPresentation presentation)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            // Create a new section
            ISection partSection = presentation.Sections.Add();
            partSection.Name = PartName;

            // Add the appropraite slides to it
            await AddToSection(partSection);
        }
        #endregion
    }
}