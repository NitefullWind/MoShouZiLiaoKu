using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace NewsTemplate.Data
{
    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class SampleDataCommon : NewsTemplate.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public SampleDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description, String bgColor)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
            this._bgColor = bgColor;


        }

        private string _bgColor = string.Empty;
        public string BgColor
        {
            get { return this._bgColor; }
            set { this.SetProperty(ref this._bgColor, value); }
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(SampleDataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        public string novelTitle;
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, SampleDataGroup group, string bgColor)
            : base(uniqueId, title, subtitle, imagePath, description, bgColor)
        {
            this._content = content;
            this._group = group;
            this.novelTitle = title;
        }
        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private SampleDataGroup _group;
        public SampleDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup : SampleDataCommon
    {
        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description, String bgColor)
            : base(uniqueId, title, subtitle, imagePath, description, bgColor)
        {

            this._bgColor = bgColor;
        }

        private ObservableCollection<SampleDataItem> _items = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataItem> Items
        {
            get { return this._items; }
        }

        private string _bgColor = string.Empty;
        public string bgColor
        {
            get { return this._bgColor; }
            set { this.SetProperty(ref this._bgColor, value); }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _itemGroups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> ItemGroups
        {
            get { return this._itemGroups; }
        }

        public static SampleDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.ItemGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }


        public SampleDataSource()
        {
            String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}", "AAAAA");
                        //"Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new SampleDataGroup("Group-1",
                    "魔兽编年史",
                    "宇宙开启——黑暗之门31年",
                    "Assets/Images/NewsImage.jpg",
                    "这里是描述",
                    "#FFd00000");

                    group1.Items.Add(new SampleDataItem("Group-1-Item-1",
                            "宇宙开启——黑暗之门31年",
                            "概述从泰坦创世，到潘达利亚现世期间十几万年间的历史",
                            "Assets/Images/NewsImage1.jpg",
                            "",
                            ITEM_CONTENT,
                            group1,
                            "#FFd00000"));

                //group1.Items.Add(new SampleDataItem("Group-1-Item-1",
                //        "大标题",
                //        "小标题",
                //        "Assets/Images/NewsImage1.png",
                //        "描述：下个属性是内容",
                //        ITEM_CONTENT,
                //        group1,
                //        "#FFd00000"));
                this.ItemGroups.Add(group1);

            var group2 = new SampleDataGroup("Group-2",
                    "精彩资料片",
                    "Some Sub-Heading if needed",
                    "Assets/Images/VideoImage.jpg",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante",
                    "#FFf8931f");
            group2.Items.Add(new SampleDataItem("Group-2-Item-1",
                    "《魔兽世界》",
                    "",
                    "Assets/VideoImages/1.png",
                    "",
                    ITEM_CONTENT,
                    group2,
                   "#FFf8931f"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-2",
                    "《魔兽世界：燃烧的远征》",
                    "",
                      "Assets/VideoImages/2.png",
                    "",
                    ITEM_CONTENT,
                    group2,
                   "#FFf8931f"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-3",
                    "《魔兽世界：巫妖王之怒》",
                    "",
                      "Assets/VideoImages/3.png",
                    "",
                    ITEM_CONTENT,
                    group2,
                   "#FFf8931f"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-4",
                  "《魔兽世界：大地的裂变》",
                  "",
                    "Assets/VideoImages/4.png",
                  "",
                  ITEM_CONTENT,
                  group2,
                 "#FFf8931f"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-5",
                 "《魔兽世界：熊猫人之谜》",
                  "",
                    "Assets/VideoImages/5.png",
                  "",
                  ITEM_CONTENT,
                  group2,
                 "#FFf8931f"));
            group2.Items.Add(new SampleDataItem("Group-2-Item-6",
                 "《魔兽世界：德拉诺之王》",
                  "",
                    "Assets/VideoImages/6.png",
                  "",
                  ITEM_CONTENT,
                  group2,
                 "#FFf8931f"));
            this.ItemGroups.Add(group2);

            var group3 = new SampleDataGroup("Group-3",
                    "官方小说",
                    "Some Sub-Heading if needed",
                    "Assets/Images/AllImage.jpg",
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante",
                    "#FF77bb44");
            group3.Items.Add(new SampleDataItem("Group-3-Item-1",
                     "部落的崛起",
                    "",
                    "Assets/NovelImages/部落的崛起.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-2",
                     "黑暗之潮",
                    "",
                     "Assets/NovelImages/黑暗之潮.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-3",
                     "黑暗之门",
                    "",
                    "Assets/NovelImages/黑暗之门.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-4",
                     "阿尔萨斯：巫妖王的崛起",
                    "",
                     "Assets/NovelImages/巫妖王的崛起.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-5",
                     "仇恨之轮",
                    "",
                    "Assets/NovelImages/仇恨之轮.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-6",
                     "怒风",
                    "",
                    "Assets/NovelImages/怒风.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-7",
                     "天崩地裂：浩劫的前奏",
                    "",
                    "Assets/NovelImages/浩劫的前奏.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-8",
                     "萨尔：龙王之暮",
                    "",
                    "Assets/NovelImages/龙王之暮.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-9",
                     "狼族之心",
                    "",
                    "Assets/NovelImages/狼族之心.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-10",
                     "吉安娜·普罗德摩尔：战争之潮",
                    "",
                    "Assets/NovelImages/战争之潮.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-11",
                     "巨龙时代",
                    "",
                    "Assets/NovelImages/巨龙时代.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-12",
                     "上古三部曲一：永恒之井",
                    "",
                    "Assets/NovelImages/永恒之井.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-13",
                     "上古三部曲二：恶魔之魂",
                    "",
                    "Assets/NovelImages/恶魔之魂.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-14",
                     "上古三部曲三：天崩地裂",
                    "",
                    "Assets/NovelImages/天崩地裂.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-15",
                     "巨龙之夜",
                    "",
                    "Assets/NovelImages/巨龙之夜.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-16",
                     "氏族之王",
                    "",
                    "Assets/NovelImages/氏族之王.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-17",
                     "血与荣耀",
                    "",
                    "Assets/NovelImages/血与荣耀.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-18",
                     "诅咒之路",
                    "",
                    "Assets/NovelImages/诅咒之路.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));
            group3.Items.Add(new SampleDataItem("Group-3-Item-19",
                     "最后的守护者",
                    "",
                    "Assets/NovelImages/最后的守护者.jpg",
                    "",
                    ITEM_CONTENT,
                    group3,
                   "#FF77bb44"));

            this.ItemGroups.Add(group3);

        }
    }
}
