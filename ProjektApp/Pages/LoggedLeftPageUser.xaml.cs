﻿using DBconnectShop;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for LoggedLeftPageUser.xaml
    /// </summary>
    public partial class LoggedLeftPageUser : UserControl {
        ProductsBuyPage page;

        public LoggedLeftPageUser(BuyableProducts products, ProductsBuyPage page) {
            InitializeComponent();
            this.page = page;

            var dict = new Dictionary<int, Expander>();
            var categories = products.CatergorisRO;

            foreach(var category in categories) {
                var tmp = CreateContener(category.TrueName, category.ID);
                dict.Add(category.ID, tmp);

                if(category.ParentID == 0) {
                    ExpanderPanel.Children.Add(dict[category.ID]);
                } else { 
                    (dict[(int)category.ParentID].Content as StackPanel)
                        .Children.Add(dict[category.ID]);
                }
            }
        }

        private Expander CreateContener(string text, int id) {
            Button button = new Button();
            button.Style = this.Resources["MaterialDesignFloatingActionMiniLightButton"] as Style;
            button.ToolTip = "Wyszukaj";
            button.Click += (object o, RoutedEventArgs e) => ChangeCategory(id);

            PackIcon packIcon = new PackIcon();
            packIcon.Kind = PackIconKind.Magnify;
            button.Content = packIcon;

            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;

            Grid grid = new Grid();
            var column1 = new ColumnDefinition();
            var column2 = new ColumnDefinition();
            var column3 = new ColumnDefinition();

            column1.Width = new GridLength(1, GridUnitType.Auto);
            column2.Width = new GridLength(1, GridUnitType.Star);
            column3.Width = new GridLength(1, GridUnitType.Auto);

            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);
            grid.ColumnDefinitions.Add(column3);

            Grid.SetColumn(button, 2);
            Grid.SetColumn(textBlock, 0);

            grid.Children.Add(button);
            grid.Children.Add(textBlock);

            Expander expander = new Expander();
            expander.HorizontalAlignment = HorizontalAlignment.Stretch;
            expander.Header = grid;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.Margin = new Thickness(24, 8, 0, 16);

            expander.Content = stackPanel;

            return expander;
        }

        private void ChangeCategory(int id) {
            page.Category = id;
            page.UserPage();
        }
    }
}
