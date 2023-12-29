using OfficeOpenXml;

namespace Bislerium_Cafe;

public partial class adminLogin : ContentPage
{
	public adminLogin()
	{
		InitializeComponent();
	}
	 private void showpasswordcheckchange(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.IsChecked)
            {
                PasswordEntry.IsPassword = false;
            }
            else
            {
                PasswordEntry.IsPassword = true;
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                string username = UsernameEntry.Text;
                string password = PasswordEntry.Text;
                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;
                bool errorMessage = true;

                string excelPath = "C:\\Users\\rasho\\Desktop\\Application development\\staff_login_data.xlsx";

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(excelPath)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        string username_ = worksheet.Cells[row, 1].Text;
                        string password_ = worksheet.Cells[row, 2].Text;

                        if (!string.IsNullOrEmpty(username_) && username_ == username && password_ == password)
                        {
                            LoadingIndicator.IsRunning = false;
                            LoadingIndicator.IsVisible = false;
                            errorMessage = false;

                            await Navigation.PushAsync(new order());
                            break;
                        }
                    }

                    if (errorMessage)
                    {
                        ErrorMessage.Text = "Username and password didn't match!!";
                        LoadingIndicator.IsRunning = false;
                        Timer();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
            }
        }

        public async void Timer()
        {
            await Task.Delay(6000);
            ErrorMessage.Text = "";
        }
}