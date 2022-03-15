using CommunityToolkit.Mvvm.ComponentModel;
using Jason.ViewModels.Powerpoint;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Jason.ViewModels.WorshipServices
{
    public class SongPartViewModel : ObservableObject
    {
        #region Fields
        private readonly SongPart model;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the part (Verse 1, Chorus, etc)
        /// </summary>
        public string Name
        {
            get => model.Name;
            set
            {
                if (model.Name != value)
                {
                    model.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets a collection of slides to include in this song part, in order
        /// </summary>
        public ObservableCollection<SlideViewModel> Slides { get; } = new ObservableCollection<SlideViewModel>();

        private PresentationViewModel presentation;
        /// <summary>
        /// Gets or sets the presentation associated with this part
        /// </summary>
        public PresentationViewModel Presentation
        {
            get => presentation;
            set
            {
                if(SetProperty(ref presentation, value))
                {
                    if (presentation != null)
                    {
                        IEnumerable<SlideViewModel> slides = presentation.Slides;

                        if (presentation.AreSlidesLoading)
                            presentation.PropertyChanged += OnPresentationPropertyChanged;
                        else
                            LoadSlides();
                    }
                }
            }
        }

        private void OnPresentationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PresentationViewModel.AreSlidesLoading))
            {
                (sender as PresentationViewModel).PropertyChanged -= OnPresentationPropertyChanged;
                LoadSlides();
            }
        }
        #endregion

        #region Constructor
        public SongPartViewModel(SongPart model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.model = model;
            Slides.CollectionChanged += OnSlidesCollectionChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the model whenever the viewmodel's indexes change
        /// </summary>
        /// <param name="sender">Not used</param>
        /// <param name="e">Not used</param>
        private void OnSlidesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            model.Slides = Slides.Select(s => s.Index.ToString())
                                 .Aggregate((x, y) => $"{x} {y}");
        }

        private void LoadSlides()
        {
            Slides.CollectionChanged -= OnSlidesCollectionChanged;

            Slides.Clear();

            if (model.Slides?.Any() == true)
            {
                foreach (SlideViewModel slide in model.Slides
                                                      .Split(' ')
                                                      .Where(i => int.TryParse(i, out _))
                                                      .Select(i => Presentation.Slides.ElementAtOrDefault(int.Parse(i)))
                                                      .Where(x => x != null))
                {
                    Slides.Add(slide);
                }
            }

            Slides.CollectionChanged += OnSlidesCollectionChanged;
        }
        #endregion
    }
}