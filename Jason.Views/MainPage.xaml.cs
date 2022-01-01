using Jason.ViewModels;
using Jason.Views.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Jason.Views
{
    public sealed partial class MainPage : Page
    {
        #region Fields
        private readonly List<(NavigationOptions Tag, Type PageType)> pages = new List<(NavigationOptions Tag, Type PageType)>
        {
            (NavigationOptions.Home, typeof(HomeView)),
            (NavigationOptions.Settings, typeof(SettingsView)),
            (NavigationOptions.Open, typeof(HomeView))
        };
        #endregion

        #region Constructors
        public MainPage()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        private void Navigate(NavigationOptions target,
                              NavigationTransitionInfo transitionInfo)
        {
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (pages.Any(p => p.Tag == target))
            {
                Type targetType = pages.SingleOrDefault(p => p.Tag == target)
                                       .PageType;

                // Only navigate if the selected page isn't currently loaded.
                if (!Type.Equals(preNavPageType, targetType))
                    ContentFrame.Navigate(targetType, null, transitionInfo);
            }

            // If the target "Open" then also fire the command
            if (target == NavigationOptions.Open)
            {
                MainViewModel vm = Resources["ViewModel"] as MainViewModel;

                if (vm != null)
                    vm.OpenServiceCommand.Execute(null);
            }
        }

        private bool TryGoBack()
        {
            bool wentBack = false;

            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
                wentBack = true;
            }

            return wentBack;
        }
        #endregion

        #region Event Handlers
        private void OnNavViewLoaded(object sender, RoutedEventArgs e)
        {
            NavigationView navView = sender as NavigationView;

            // NavView doesn't load any page by default, so load home page.
            navView.SelectedItem = navView.MenuItems[0];

            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            Navigate(NavigationOptions.Home, new EntranceNavigationTransitionInfo());

            SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
        }

        private void OnNavViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem selected = args.InvokedItemContainer as NavigationViewItem;

            if (selected?.Tag != null)
                Navigate((NavigationOptions)selected.Tag, new EntranceNavigationTransitionInfo());
        }

        private void OnContentFrameNavigated(object sender, NavigationEventArgs e)
        {
            NavigationView navView = sender as NavigationView;

            if (navView != null)
            {
                navView.IsBackEnabled = ContentFrame.CanGoBack;

                if (ContentFrame.SourcePageType != null)
                {
                    var item = pages.FirstOrDefault(p => p.PageType == e.SourcePageType);

                    navView.SelectedItem = navView.MenuItems
                        .OfType<NavigationViewItem>()
                        .First(n => n.Tag.Equals(item.Tag));

                    navView.Header = ((NavigationViewItem)navView.SelectedItem)?.Content?.ToString();
                }
            }
        }

        private void OnContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
            => TryGoBack();

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Handle mouse back button.
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                e.Handled = TryGoBack();
            }
        }

        private void System_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
                e.Handled = TryGoBack();
        }

        private void NavigationView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            MainViewModel vm = args.NewValue as MainViewModel;

            if (vm != null)
                (vm.RecentServices as INotifyCollectionChanged).CollectionChanged += OnRecentServicesCollectionChanged;
        }

        /// <summary>
        /// Updates the navigation menu with recent items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRecentServicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {

            }
        }
        #endregion
    }
}