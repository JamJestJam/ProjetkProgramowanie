using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ProjektApp.Pages.Products {
    /// <summary>
    /// Interaction logic for LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : UserControl {
        readonly ProductList productList;

        public LeftPanel(ProductList product) {
            InitializeComponent();

            this.productList = product;
        }

        public void ShowCategory() {
            var dict = new Dictionary<int, Expander>();
            var categories = productList.Products.CatergorisRO;
            Panel.Children.Clear();

            foreach(var category in categories) {
                var tmp = CreateConteiner(category.TrueName, category.ID);
                dict.Add(category.ID, tmp);

                if(category.ParentID == 0) {
                    Panel.Children.Add(dict[category.ID]);
                } else {
                    (dict[(int)category.ParentID].Content as StackPanel)
                        .Children.Add(dict[category.ID]);
                }
            }
        }

        private Expander CreateConteiner(string name, int ID) {
            Button button = new Button {
                Style = this.Resources["MaterialDesignFloatingActionMiniLightButton"] as Style,
                ToolTip = "Wyszukaj"
            };
            button.Click += (object o, RoutedEventArgs e) => ChangeCategory(ID);

            PackIcon packIcon = new PackIcon {
                Kind = PackIconKind.Magnify
            };
            button.Content = packIcon;

            TextBlock textBlock = new TextBlock {
                Text = name
            };

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

            Expander expander = new Expander {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Header = grid
            };

            StackPanel stackPanel = new StackPanel {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(24, 8, 0, 16)
            };

            expander.Content = stackPanel;

            return expander;
        }

        private void ChangeCategory(int id) {
            productList.Category = id;
            productList.ShowProducts();
        }
    }
}
