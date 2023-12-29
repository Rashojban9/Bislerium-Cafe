namespace Bislerium_Cafe
{
    public partial class order : ContentPage
    {
        private List<OrderInfo> orderList;
        public order()
        {
            orderList = new List<OrderInfo>();

            InitializeComponent();
            InitializePickers();
        }

        private void InitializePickers()
        {
            List<string> coffeeNames = new List<string>
            {
                "Espresso",
                "Cappuccino",
                "Latte",
                "Americano"
            };

            List<string> addIns = new List<string>
            {
                "Sugar",
                "Milk",
                "Whipped Cream"
            };

            CoffeeTypePicker.ItemsSource = coffeeNames;
            AddInsPicker.ItemsSource = addIns;
        }

        private void OnSizeCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;

            if (checkBox.IsChecked)
            {
                UncheckOtherSizeCheckBoxes(checkBox);
            }

            UpdateSummary();
        }

        private void OnExpressDeliveryCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            UpdateSummary();
        }

        private async void OnPlaceOrderClicked(object sender, EventArgs e)
        {
            if (ValidateOrder())
            {
                OrderConfirmation.IsVisible = true;
                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;

               
                await Task.Delay(TimeSpan.FromSeconds(2));

                OrderConfirmation.IsVisible = false;
                LoadingIndicator.IsRunning = false;
                LoadingIndicator.IsVisible = false;
                var orderInfo = new OrderInfo
                {
                    CoffeeType = CoffeeTypePicker.SelectedItem.ToString(),
                    AddIns = AddInsPicker.SelectedItem?.ToString(),
                    Size = GetSelectedSize(),
                    quantity = QuantityEntry.Text,
                    ExpressDelivery = ExpressDeliveryCheckBox.IsChecked ? "Yes" : "No"
                };

                
                orderList.Add(orderInfo);


            }
            else
            {
                await DisplayAlert("Error", "Please select a coffee type before placing the order.", "OK");
            }
        }

        private void UpdateSummary()
        {
            SummaryLabel.Text = $"Coffee Type: {CoffeeTypePicker.SelectedItem}, " +
                                $"Add-ins: {AddInsPicker.SelectedItem}, " +
                                $"Size: {GetSelectedSize()}, " +
                                $"Express Delivery: {(ExpressDeliveryCheckBox.IsChecked ? "Yes" : "No")}";
        }

        private string GetSelectedSize()
        {
            if (SmallCheckBox.IsChecked)
                return "Small";
            else if (MediumCheckBox.IsChecked)
                return "Medium";
            else if (LargeCheckBox.IsChecked)
                return "Large";
            else
                return "Not selected";
        }

        private void UncheckOtherSizeCheckBoxes(CheckBox checkedBox)
        {
            if (checkedBox == SmallCheckBox)
            {
                MediumCheckBox.IsChecked = false;
                LargeCheckBox.IsChecked = false;
            }
            else if (checkedBox == MediumCheckBox)
            {
                SmallCheckBox.IsChecked = false;
                LargeCheckBox.IsChecked = false;
            }
            else if (checkedBox == LargeCheckBox)
            {
                SmallCheckBox.IsChecked = false;
                MediumCheckBox.IsChecked = false;
            }
        }

        private bool ValidateOrder()
        {
           
            return CoffeeTypePicker.SelectedItem != null;
        }

    }
    public class OrderInfo
    {
        public string CoffeeType { get; set; }
        public string AddIns { get; set; }
        public string Size { get; set; }
        public string quantity { get; set; }
        public string ExpressDelivery { get; set; }
    }
}
