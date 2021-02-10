using System;
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
            var dict = new Dictionary<int, TreeViewItem>();
            var categories = productList.Products.CatergorisRO;
            Panel.Items.Clear();

            foreach(var category in categories) {
                var tmp = CreateConteiner(category.TrueName, category.ID);
                dict.Add(category.ID, tmp);

                if(category.ParentID == 0)
                    Panel.Items.Add(dict[category.ID]);
                else
                    dict[(int)category.ParentID].Items.Add(dict[category.ID]);
            }
        }

        private TreeViewItem CreateConteiner(string name, int ID) {
            var item = new TreeViewItem();
            item.Header = name;
            item.Selected += (object o, RoutedEventArgs e) => {
                ChangeCategory(ID);
                e.Handled = true;
            };

            return item;
        }

        private void ChangeCategory(int id) {
#if DEBUG
            Console.WriteLine($"Jestem tu {id}");
#endif
            productList.Category = id;
            productList.ShowProducts();
        }
    }
}
