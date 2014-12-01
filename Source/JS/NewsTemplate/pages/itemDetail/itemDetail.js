﻿(function () {
    "use strict";

    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var color;
    var groupTitle;

    ui.Pages.define("/pages/itemDetail/itemDetail.html", {
        // Navigates to the groupHeaderPage. Called from the groupHeaders,
        // keyboard shortcut and iteminvoked.
        navigateToGroup: function (key) {
            nav.navigate("/pages/groupDetail/groupDetail.html", { groupKey: key });
        },

        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            var group = (options && options.group.key) ? itemDetailData.resolveGroupReference(options.group.key) : itemDetailData.groups.getAt(0);
            this._items = itemDetailData.getItemsFromGroup(group);
            element.querySelector(".pagetitle").textContent = group.title;
            groupTitle = group.title;
            color = group.backgroundImage;

            var listView = element.querySelector(".groupeditemslist").winControl;
            listView.groupHeaderTemplate = element.querySelector(".headertemplate");
            listView.itemTemplate = element.querySelector(".itemtemplate");
            listView.oniteminvoked = this._itemInvoked.bind(this);

            // Set up a keyboard shortcut (ctrl + alt + g) to navigate to the
            // current group when not in snapped mode.
            listView.addEventListener("keydown", function (e) {
                if (appView.value !== appViewState.snapped && e.ctrlKey && e.keyCode === WinJS.Utilities.Key.g && e.altKey) {
                    var data = listView.itemDataSource.list.getAt(listView.currentItem.index);
                    this.navigateToGroup(data.group.key);
                    e.preventDefault();
                    e.stopImmediatePropagation();
                }
            }.bind(this), true);

            this._initializeLayout(listView, appView.value);
            listView.element.focus();
        },

        // This function updates the page layout in response to viewState changes.
        updateLayout: function (element, viewState, lastViewState) {
            /// <param name="element" domElement="true" />

            var listView = element.querySelector(".groupeditemslist").winControl;
            if (lastViewState !== viewState) {
                if (lastViewState === appViewState.snapped || viewState === appViewState.snapped) {
                    var handler = function (e) {
                        //element.querySelector(".item-overlay").style.backgroundColor = color;
                        listView.removeEventListener("contentanimating", handler, false);
                        e.preventDefault();
                    }
                    listView.addEventListener("contentanimating", handler, false);
                    this._initializeLayout(listView, viewState);
                }
            }
        },

        // This function updates the ListView with new layouts
        _initializeLayout: function (listView, viewState) {
            /// <param name="listView" value="WinJS.UI.ListView.prototype" />

            if (viewState === appViewState.snapped) {

                listView.itemDataSource = this._items.dataSource;
                listView.groupDataSource = null;
                listView.layout = new ui.ListLayout();
            } else {
                listView.itemDataSource = this._items.dataSource;
                listView.groupDataSource = null;
                listView.layout = new ui.GridLayout({ groupHeaderPosition: "top" });
            }
        },

        _itemInvoked: function (args) {
            if (appView.value === appViewState.snapped) {
                // If the page is snapped, the user invoked a group.
                if (groupTitle == "Articles" || groupTitle == "All") {
                    var item = itemDetailData.items.getAt(args.detail.itemIndex);
                    nav.navigate("/pages/Articles/Articles.html", { item: itemDetailData.getItemReference(item) });

                }
                else if (groupTitle == "Videos") {
                    var item = itemDetailData.items.getAt(args.detail.itemIndex);
                    nav.navigate("/pages/Videos/Videos.html", { item: itemDetailData.getItemReference(item) });
                }
            } else {
                // If the page is not snapped, the user invoked an item.
                if (groupTitle == "Articles" || groupTitle == "All") {
                    var item = itemDetailData.items.getAt(args.detail.itemIndex);
                    nav.navigate("/pages/Articles/Articles.html", { item: itemDetailData.getItemReference(item) });

                }
                else if (groupTitle == "Videos") {
                    var item = itemDetailData.items.getAt(args.detail.itemIndex);
                    nav.navigate("/pages/Videos/Videos.html", { item: itemDetailData.getItemReference(item) });
                }
            }

        }
    });
})();
