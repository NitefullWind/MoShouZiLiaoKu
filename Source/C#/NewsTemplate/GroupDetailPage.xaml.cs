using ChannelMTemplate;
using NewsTemplate.Data;
using NewsTemplate.EnableLiveTile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Group Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234229

namespace NewsTemplate
{
    /// <summary>
    /// A page that displays an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    public sealed partial class GroupDetailPage : NewsTemplate.Common.LayoutAwarePage
    {
        public GroupDetailPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            string uniqueId = (string)navigationParameter;
            var group = SampleDataSource.GetGroup(uniqueId);
            this.DefaultViewModel["Group"] = group;
            this.DefaultViewModel["Items"] = group.Items;

            CreateLiveTile.ShowliveTile(false, "News");
        }

        /// <summary>
        /// Invoked when an item is clicked.
        /// </summary>
        /// <param name="sender">The GridView (or ListView when the application is snapped)
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            //this.Frame.Navigate(typeof(ItemDetailPage), e.ClickedItem);
            var group = e.ClickedItem as Data.SampleDataItem;
            var value1 = group.Title;

            setTitle title = new setTitle(value1);

            if (value1.ToString() == "宇宙开启——黑暗之门31年")
            {
                this.Frame.Navigate(typeof(HistoryDetail));
            }
            else if (value1.ToString() == "《魔兽世界》" || value1.ToString() == "《魔兽世界：燃烧的远征》" || value1.ToString() == "《魔兽世界：巫妖王之怒》" || value1.ToString() == "《魔兽世界：大地的裂变》" || value1.ToString() == "《魔兽世界：熊猫人之谜》" || value1.ToString() == "《魔兽世界：德拉诺之王》")
            {
                this.Frame.Navigate(typeof(VidesDetail));
            }
            else
            {
                this.Frame.Navigate(typeof(ArticlesDetail));
                this.pageTitle.Text = value1.ToString();
            }
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        { }
    }
}
