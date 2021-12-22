using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Presentation;
using Jason.ViewModels.Extensions;
using System;

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
        protected void AddToSection(Section section)
        {
            Slide titleSlide = new Slide();
            titleSlide.AddTitle(PartName);
            section.AddSlide(titleSlide);
        }

        /// <summary>
        /// Adds this worship service part to an OpenXML powerpoint presentation
        /// </summary>
        public void AddToPowerpoint(Presentation presentation)
        {
            if (presentation == null)
                throw new ArgumentNullException(nameof(presentation));

            // Add a new section
            Section section = presentation.AddSection(PartName);

            // Add the part to the section
            AddToSection(section);
        }
        #endregion
    }
}