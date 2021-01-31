﻿using DBconnectShop;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace ProjektApp.Pages {
    /// <summary>
    /// Interaction logic for LoggedLeftPageUser.xaml
    /// </summary>
    public partial class LoggedLeftPageUser : UserControl {

        public LoggedLeftPageUser(BuyableProducts products) {
            InitializeComponent();

            var dict = new Dictionary<int, Expander>();
            var categories = products.ProductCategory();

            foreach(var category in categories) {
                var tmp = CreateContener(category.name);
                dict.Add(category.id, tmp);
            }

            foreach(var category in categories) {
                if(!(category.parentID is null)) {
                    (dict[(int)category.parentID].Content as StackPanel)
                        .Children.Add(dict[category.id]);
                }
            }

            var categoriesNull = categories.Where(a => a.parentID == null).ToList();

            foreach(var category in categoriesNull) {
                ExpanderPanel.Children.Add(dict[category.id]);
            }
        }

        private Expander CreateContener(string text) {
            Button button = new Button();
            button.Style = this.Resources["MaterialDesignFloatingActionMiniLightButton"] as Style;
            button.ToolTip = "Wyszukaj";

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
    }
}