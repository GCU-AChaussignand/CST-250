// Aaron Chaussignand
// CST-250 Activity 1 - VehicleStoreGUIApp
// Date: July 13, 2026
// References: CST-250 Activity 1 Guide, GCU coding guidelines, Microsoft WinForms documentation.

using VehicleClassLibrary.Models;
using VehicleClassLibrary.Services.BusinessLogicLayer;

namespace VehicleStoreGUIApp
{
    /// <summary>
    /// GUI presentation layer for the vehicle store application.
    /// </summary>
    public class FrmVehicleStore : Form
    {
        private readonly StoreLogic _storeLogic;
        private readonly BindingSource _inventoryBindingSource;
        private readonly BindingSource _shoppingCartBindingSource;
        private string _currentVehicleType;

        private bool _vehicleTypeValid;
        private bool _makeValid;
        private bool _modelValid;
        private bool _yearValid;
        private bool _priceValid;
        private bool _wheelsValid;
        private bool _colorValid;
        private bool _mileageValid;
        private bool _specialtyBooleanValid;
        private bool _specialtyDecimalValid;

        private RadioButton rdoCar = null!;
        private RadioButton rdoMotorcycle = null!;
        private RadioButton rdoPickup = null!;
        private RadioButton rdoVehicle = null!;
        private TextBox txtMake = null!;
        private TextBox txtModel = null!;
        private TextBox txtYear = null!;
        private TextBox txtPrice = null!;
        private TextBox txtWheels = null!;
        private TextBox txtColor = null!;
        private TextBox txtMileage = null!;
        private Label lblSpecialtyBoolean = null!;
        private Label lblSpecialtyDecimal = null!;
        private RadioButton rdoSpecialtyYes = null!;
        private RadioButton rdoSpecialtyNo = null!;
        private TextBox txtSpecialtyDecimal = null!;
        private Button btnCreate = null!;
        private ListBox lstInventory = null!;
        private Button btnAddToCart = null!;
        private ListBox lstShoppingCart = null!;
        private Button btnCheckout = null!;
        private Label lblTotal = null!;
        private TextBox txtSearch = null!;
        private ComboBox cboSort = null!;
        private Button btnSearch = null!;
        private Button btnClearSearch = null!;
        private Button btnSort = null!;
        private Button btnRemoveFromCart = null!;
        private Button btnSaveInventory = null!;
        private Button btnLoadInventory = null!;

        private Label lblVehicleTypeError = null!;
        private Label lblMakeError = null!;
        private Label lblModelError = null!;
        private Label lblYearError = null!;
        private Label lblPriceError = null!;
        private Label lblWheelsError = null!;
        private Label lblColorError = null!;
        private Label lblMileageError = null!;
        private Label lblSpecialtyBooleanError = null!;
        private Label lblSpecialtyDecimalError = null!;

        /// <summary>
        /// Initializes the vehicle store form, controls, data bindings, and default state.
        /// </summary>
        public FrmVehicleStore()
        {
            _storeLogic = new StoreLogic();
            _inventoryBindingSource = new BindingSource();
            _shoppingCartBindingSource = new BindingSource();
            _currentVehicleType = string.Empty;

            InitializeComponent();
            HideErrorLabels();
            HideSpecialtyControls();
            ResetInventoryBinding(_storeLogic.GetInventory());
            _shoppingCartBindingSource.DataSource = _storeLogic.GetShoppingCart();
            lstShoppingCart.DataSource = _shoppingCartBindingSource;
        }

        /// <summary>
        /// Creates all controls for the form without requiring a designer file.
        /// </summary>
        private void InitializeComponent()
        {
            Text = "Vehicle Store";
            Width = 1180;
            Height = 720;
            StartPosition = FormStartPosition.CenterScreen;

            GroupBox grpCreate = new GroupBox { Text = "Create a Vehicle", Left = 12, Top = 12, Width = 350, Height = 370 };
            Controls.Add(grpCreate);

            rdoCar = new RadioButton { Text = "Car", Name = "rdoCar", Left = 15, Top = 25, Width = 75 };
            rdoMotorcycle = new RadioButton { Text = "Motorcycle", Name = "rdoMotorcycle", Left = 95, Top = 25, Width = 105 };
            rdoPickup = new RadioButton { Text = "Pickup", Name = "rdoPickup", Left = 205, Top = 25, Width = 75 };
            rdoVehicle = new RadioButton { Text = "Vehicle", Name = "rdoVehicle", Left = 280, Top = 25, Width = 75 };
            rdoCar.Click += RdoCarClickEH;
            rdoMotorcycle.Click += RdoMotorcycleClickEH;
            rdoPickup.Click += RdoPickupClickEH;
            rdoVehicle.Click += RdoVehicleClickEH;
            grpCreate.Controls.AddRange(new Control[] { rdoCar, rdoMotorcycle, rdoPickup, rdoVehicle });

            lblVehicleTypeError = CreateErrorLabel("Please choose a vehicle type", 15, 50);
            lblVehicleTypeError.Name = "lblVehicleTypeError";
            grpCreate.Controls.Add(lblVehicleTypeError);

            int labelLeft = 15;
            int textLeft = 110;
            int top = 80;
            AddLabeledTextBox(grpCreate, "Make:", labelLeft, top, textLeft, out txtMake, out lblMakeError, "Please enter a make");
            txtMake.Name = "txtMake";
            lblMakeError.Name = "lblMakeError";
            top += 40;
            AddLabeledTextBox(grpCreate, "Model:", labelLeft, top, textLeft, out txtModel, out lblModelError, "Please enter a model");
            txtModel.Name = "txtModel";
            lblModelError.Name = "lblModelError";
            top += 40;
            AddLabeledTextBox(grpCreate, "Year:", labelLeft, top, textLeft, out txtYear, out lblYearError, "Enter a valid year");
            txtYear.Name = "txtYear";
            lblYearError.Name = "lblYearError";
            top += 40;
            AddLabeledTextBox(grpCreate, "Price:", labelLeft, top, textLeft, out txtPrice, out lblPriceError, "Enter a valid price");
            txtPrice.Name = "txtPrice";
            lblPriceError.Name = "lblPriceError";
            top += 40;
            AddLabeledTextBox(grpCreate, "Wheels:", labelLeft, top, textLeft, out txtWheels, out lblWheelsError, "Enter valid wheels");
            txtWheels.Name = "txtWheels";
            lblWheelsError.Name = "lblWheelsError";
            top += 40;
            AddLabeledTextBox(grpCreate, "Color:", labelLeft, top, textLeft, out txtColor, out lblColorError, "Please enter a color");
            txtColor.Name = "txtColor";
            lblColorError.Name = "lblColorError";
            top += 40;
            AddLabeledTextBox(grpCreate, "Mileage:", labelLeft, top, textLeft, out txtMileage, out lblMileageError, "Enter valid mileage");
            txtMileage.Name = "txtMileage";
            lblMileageError.Name = "lblMileageError";

            btnCreate = new Button { Text = "Create", Name = "btnCreate", Left = 230, Top = 325, Width = 90 };
            btnCreate.Click += BtnCreateClickEH;
            grpCreate.Controls.Add(btnCreate);

            txtMake.Leave += TxtMakeLeaveEH;
            txtModel.Leave += TxtModelLeaveEH;
            txtYear.Leave += TxtYearLeaveEH;
            txtPrice.Leave += TxtPriceLeaveEH;
            txtWheels.Leave += TxtWheelsLeaveEH;
            txtColor.Leave += TxtColorLeaveEH;
            txtMileage.Leave += TxtMileageLeaveEH;

            GroupBox grpSpecialty = new GroupBox { Text = "Specialty Properties", Left = 12, Top = 390, Width = 350, Height = 150 };
            Controls.Add(grpSpecialty);

            lblSpecialtyBoolean = new Label { Text = "Specialty Boolean:", Left = 15, Top = 30, Width = 130 };
            rdoSpecialtyYes = new RadioButton { Text = "Yes", Name = "rdoSpecialtyYes", Left = 145, Top = 28, Width = 60 };
            rdoSpecialtyNo = new RadioButton { Text = "No", Name = "rdoSpecialtyNo", Left = 215, Top = 28, Width = 60 };
            rdoSpecialtyYes.Click += RdoSpecialtyBooleanClickEH;
            rdoSpecialtyNo.Click += RdoSpecialtyBooleanClickEH;
            lblSpecialtyBooleanError = CreateErrorLabel("Choose Yes or No", 15, 55);
            lblSpecialtyBooleanError.Name = "lblSpecialtyBooleanError";
            lblSpecialtyDecimal = new Label { Text = "Specialty Decimal:", Left = 15, Top = 85, Width = 130 };
            txtSpecialtyDecimal = new TextBox { Name = "txtSpecialtyDecimal", Left = 145, Top = 82, Width = 160 };
            txtSpecialtyDecimal.Leave += TxtSpecialtyDecimalLeaveEH;
            lblSpecialtyDecimalError = CreateErrorLabel("Enter a valid decimal", 15, 110);
            lblSpecialtyDecimalError.Name = "lblSpecialtyDecimalError";
            grpSpecialty.Controls.AddRange(new Control[] { lblSpecialtyBoolean, rdoSpecialtyYes, rdoSpecialtyNo, lblSpecialtyBooleanError, lblSpecialtyDecimal, txtSpecialtyDecimal, lblSpecialtyDecimalError });

            GroupBox grpInventory = new GroupBox { Text = "Store Inventory", Left = 380, Top = 12, Width = 360, Height = 415 };
            Controls.Add(grpInventory);
            lstInventory = new ListBox { Name = "lstInventory", Left = 10, Top = 25, Width = 340, Height = 375, HorizontalScrollbar = true };
            grpInventory.Controls.Add(lstInventory);

            GroupBox grpSearchSort = new GroupBox { Text = "Search and Sort Inventory", Left = 380, Top = 435, Width = 360, Height = 130 };
            Controls.Add(grpSearchSort);
            txtSearch = new TextBox { Name = "txtSearch", Left = 10, Top = 25, Width = 160 };
            btnSearch = new Button { Text = "Search", Name = "btnSearch", Left = 180, Top = 23, Width = 75 };
            btnClearSearch = new Button { Text = "Clear", Name = "btnClearSearch", Left = 265, Top = 23, Width = 75 };
            cboSort = new ComboBox { Name = "cboSort", Left = 10, Top = 70, Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };
            cboSort.Items.AddRange(new object[] { "Id", "Make", "Model", "Year", "Price" });
            cboSort.SelectedIndex = 0;
            btnSort = new Button { Text = "Sort", Name = "btnSort", Left = 180, Top = 68, Width = 75 };
            btnSaveInventory = new Button { Text = "Save", Name = "btnSaveInventory", Left = 265, Top = 68, Width = 75 };
            btnLoadInventory = new Button { Text = "Load", Name = "btnLoadInventory", Left = 265, Top = 95, Width = 75 };
            btnSearch.Click += BtnSearchClickEH;
            btnClearSearch.Click += BtnClearSearchClickEH;
            btnSort.Click += BtnSortClickEH;
            btnSaveInventory.Click += BtnSaveInventoryClickEH;
            btnLoadInventory.Click += BtnLoadInventoryClickEH;
            grpSearchSort.Controls.AddRange(new Control[] { txtSearch, btnSearch, btnClearSearch, cboSort, btnSort, btnSaveInventory, btnLoadInventory });

            btnAddToCart = new Button { Text = "Add to Cart", Name = "btnAddToCart", Left = 760, Top = 180, Width = 110, Height = 40 };
            btnAddToCart.Click += BtnAddToCartClickEH;
            Controls.Add(btnAddToCart);

            GroupBox grpCart = new GroupBox { Text = "Shopping Cart", Left = 890, Top = 12, Width = 260, Height = 415 };
            Controls.Add(grpCart);
            lstShoppingCart = new ListBox { Name = "lstShoppingCart", Left = 10, Top = 25, Width = 240, Height = 375, HorizontalScrollbar = true };
            grpCart.Controls.Add(lstShoppingCart);

            btnRemoveFromCart = new Button { Text = "Remove From Cart", Name = "btnRemoveFromCart", Left = 890, Top = 440, Width = 135, Height = 35 };
            btnRemoveFromCart.Click += BtnRemoveFromCartClickEH;
            Controls.Add(btnRemoveFromCart);

            btnCheckout = new Button { Text = "Checkout", Name = "btnCheckout", Left = 1035, Top = 440, Width = 115, Height = 35 };
            btnCheckout.Click += BtnCheckoutClickEH;
            Controls.Add(btnCheckout);
            Label lblTotalText = new Label { Text = "Total:", Left = 890, Top = 490, Width = 60 };
            lblTotal = new Label { Text = "$0.00", Name = "lblTotal", Left = 950, Top = 490, Width = 180, Font = new Font(Font.FontFamily, 11, FontStyle.Bold) };
            Controls.Add(lblTotalText);
            Controls.Add(lblTotal);
        }

        /// <summary>
        /// Creates a label and textbox pair with its validation error label.
        /// </summary>
        private static void AddLabeledTextBox(GroupBox parent, string labelText, int labelLeft, int top, int textLeft, out TextBox textBox, out Label errorLabel, string errorText)
        {
            Label label = new Label { Text = labelText, Left = labelLeft, Top = top + 3, Width = 80 };
            textBox = new TextBox { Left = textLeft, Top = top, Width = 150 };
            errorLabel = CreateErrorLabel(errorText, textLeft, top + 22);
            parent.Controls.AddRange(new Control[] { label, textBox, errorLabel });
        }

        /// <summary>
        /// Creates a formatted red error label.
        /// </summary>
        private static Label CreateErrorLabel(string text, int left, int top)
        {
            return new Label
            {
                Text = text,
                Left = left,
                Top = top,
                Width = 250,
                ForeColor = Color.Red,
                Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold)
            };
        }

        /// <summary>
        /// Handles the car radio button click event.
        /// </summary>
        private void RdoCarClickEH(object? sender, EventArgs e)
        {
            _currentVehicleType = "Car";
            lblSpecialtyBoolean.Text = "Convertible:";
            lblSpecialtyDecimal.Text = "Trunk Size:";
            ShowSpecialtyControls();
            ValidateVehicleType();
        }

        /// <summary>
        /// Handles the motorcycle radio button click event.
        /// </summary>
        private void RdoMotorcycleClickEH(object? sender, EventArgs e)
        {
            _currentVehicleType = "Motorcycle";
            lblSpecialtyBoolean.Text = "Side Car:";
            lblSpecialtyDecimal.Text = "Seat Height:";
            ShowSpecialtyControls();
            ValidateVehicleType();
        }

        /// <summary>
        /// Handles the pickup radio button click event.
        /// </summary>
        private void RdoPickupClickEH(object? sender, EventArgs e)
        {
            _currentVehicleType = "Pickup";
            lblSpecialtyBoolean.Text = "Bed Cover:";
            lblSpecialtyDecimal.Text = "Bed Size:";
            ShowSpecialtyControls();
            ValidateVehicleType();
        }

        /// <summary>
        /// Handles the general vehicle radio button click event.
        /// </summary>
        private void RdoVehicleClickEH(object? sender, EventArgs e)
        {
            _currentVehicleType = "Vehicle";
            HideSpecialtyControls();
            ValidateVehicleType();
        }

        /// <summary>
        /// Handles the create button click event.
        /// </summary>
        private void BtnCreateClickEH(object? sender, EventArgs e)
        {
            ValidateVehicleType();
            string make = ValidateTxtMake();
            string model = ValidateTxtModel();
            int year = ValidateTxtYear();
            decimal price = ValidateTxtPrice();
            int wheels = ValidateTxtWheels();
            string color = ValidateTxtColor();
            int mileage = ValidateTxtMileage();
            bool specialtyBoolean = ValidateSpecialtyBoolean();
            decimal specialtyDecimal = ValidateSpecialtyDecimal();

            if (!AllInputIsValid())
            {
                MessageBox.Show("Please correct the highlighted errors before creating a vehicle.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VehicleModel vehicle = _currentVehicleType switch
            {
                "Car" => new CarModel(0, make, model, year, price, wheels, color, mileage, specialtyBoolean, specialtyDecimal),
                "Motorcycle" => new MotorcycleModel(0, make, model, year, price, wheels, color, mileage, specialtyBoolean, specialtyDecimal),
                "Pickup" => new PickupModel(0, make, model, year, price, wheels, color, mileage, specialtyBoolean, specialtyDecimal),
                _ => new VehicleModel(0, make, model, year, price, wheels, color, mileage)
            };

            int result = _storeLogic.AddVehicleToInventory(vehicle);

            if (result == 0)
            {
                MessageBox.Show("Duplicate vehicle entry prevented.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (result < 0)
            {
                MessageBox.Show("The vehicle could not be created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ResetInventoryBinding(_storeLogic.GetInventory());
            ClearCreateInputs();
            MessageBox.Show($"Vehicle created:\n{vehicle}", "Vehicle Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Validates that the user selected a vehicle type.
        /// </summary>
        private void ValidateVehicleType()
        {
            _vehicleTypeValid = rdoCar.Checked || rdoMotorcycle.Checked || rdoPickup.Checked || rdoVehicle.Checked;
            lblVehicleTypeError.Visible = !_vehicleTypeValid;
        }

        /// <summary>
        /// Validates and returns the make textbox value.
        /// </summary>
        private string ValidateTxtMake()
        {
            _makeValid = !string.IsNullOrWhiteSpace(txtMake.Text);
            lblMakeError.Visible = !_makeValid;
            return txtMake.Text.Trim();
        }

        /// <summary>
        /// Validates and returns the model textbox value.
        /// </summary>
        private string ValidateTxtModel()
        {
            _modelValid = !string.IsNullOrWhiteSpace(txtModel.Text);
            lblModelError.Visible = !_modelValid;
            return txtModel.Text.Trim();
        }

        /// <summary>
        /// Validates and returns the year textbox value.
        /// </summary>
        private int ValidateTxtYear()
        {
            _yearValid = int.TryParse(txtYear.Text, out int year) && year >= 1886 && year <= DateTime.Now.Year + 1;
            lblYearError.Visible = !_yearValid;
            return year;
        }

        /// <summary>
        /// Validates and returns the price textbox value.
        /// </summary>
        private decimal ValidateTxtPrice()
        {
            _priceValid = decimal.TryParse(txtPrice.Text, out decimal price) && price >= 0;
            lblPriceError.Visible = !_priceValid;
            return price;
        }

        /// <summary>
        /// Validates and returns the wheels textbox value.
        /// </summary>
        private int ValidateTxtWheels()
        {
            _wheelsValid = int.TryParse(txtWheels.Text, out int wheels) && wheels > 0;
            lblWheelsError.Visible = !_wheelsValid;
            return wheels;
        }

        /// <summary>
        /// Validates and returns the color textbox value.
        /// </summary>
        private string ValidateTxtColor()
        {
            _colorValid = !string.IsNullOrWhiteSpace(txtColor.Text);
            lblColorError.Visible = !_colorValid;
            return txtColor.Text.Trim();
        }

        /// <summary>
        /// Validates and returns the mileage textbox value.
        /// </summary>
        private int ValidateTxtMileage()
        {
            _mileageValid = int.TryParse(txtMileage.Text, out int mileage) && mileage >= 0;
            lblMileageError.Visible = !_mileageValid;
            return mileage;
        }

        /// <summary>
        /// Validates and returns the specialty Boolean radio button choice.
        /// </summary>
        private bool ValidateSpecialtyBoolean()
        {
            if (_currentVehicleType == "Vehicle")
            {
                _specialtyBooleanValid = true;
                lblSpecialtyBooleanError.Visible = false;
                return false;
            }

            _specialtyBooleanValid = rdoSpecialtyYes.Checked || rdoSpecialtyNo.Checked;
            lblSpecialtyBooleanError.Visible = !_specialtyBooleanValid;
            return rdoSpecialtyYes.Checked;
        }

        /// <summary>
        /// Validates and returns the specialty decimal textbox value.
        /// </summary>
        private decimal ValidateSpecialtyDecimal()
        {
            if (_currentVehicleType == "Vehicle")
            {
                _specialtyDecimalValid = true;
                lblSpecialtyDecimalError.Visible = false;
                return 0.0m;
            }

            _specialtyDecimalValid = decimal.TryParse(txtSpecialtyDecimal.Text, out decimal specialtyDecimal) && specialtyDecimal >= 0;
            lblSpecialtyDecimalError.Visible = !_specialtyDecimalValid;
            return specialtyDecimal;
        }

        /// <summary>
        /// Handles the make textbox leave event.
        /// </summary>
        private void TxtMakeLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtMake();
        }

        /// <summary>
        /// Handles the model textbox leave event.
        /// </summary>
        private void TxtModelLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtModel();
        }

        /// <summary>
        /// Handles the year textbox leave event.
        /// </summary>
        private void TxtYearLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtYear();
        }

        /// <summary>
        /// Handles the price textbox leave event.
        /// </summary>
        private void TxtPriceLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtPrice();
        }

        /// <summary>
        /// Handles the wheels textbox leave event.
        /// </summary>
        private void TxtWheelsLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtWheels();
        }

        /// <summary>
        /// Handles the color textbox leave event for challenge property validation.
        /// </summary>
        private void TxtColorLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtColor();
        }

        /// <summary>
        /// Handles the mileage textbox leave event for challenge property validation.
        /// </summary>
        private void TxtMileageLeaveEH(object? sender, EventArgs e)
        {
            ValidateTxtMileage();
        }

        /// <summary>
        /// Handles the specialty decimal textbox leave event.
        /// </summary>
        private void TxtSpecialtyDecimalLeaveEH(object? sender, EventArgs e)
        {
            ValidateSpecialtyDecimal();
        }

        /// <summary>
        /// Handles both specialty Boolean radio button click events.
        /// </summary>
        private void RdoSpecialtyBooleanClickEH(object? sender, EventArgs e)
        {
            ValidateSpecialtyBoolean();
        }

        /// <summary>
        /// Adds the selected inventory vehicle to the shopping cart.
        /// </summary>
        private void BtnAddToCartClickEH(object? sender, EventArgs e)
        {
            if (lstInventory.SelectedItem is not VehicleModel vehicle)
            {
                MessageBox.Show("Select a vehicle from inventory first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int result = _storeLogic.AddVehicleToCart(vehicle.Id);

            if (result == -1)
            {
                MessageBox.Show("The selected vehicle could not be added to the cart.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _shoppingCartBindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Checks out the shopping cart and displays the total.
        /// </summary>
        private void BtnCheckoutClickEH(object? sender, EventArgs e)
        {
            decimal total = _storeLogic.Checkout();
            lblTotal.Text = total.ToString("C2");
            _shoppingCartBindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Searches inventory and updates the inventory list box.
        /// </summary>
        private void BtnSearchClickEH(object? sender, EventArgs e)
        {
            ResetInventoryBinding(_storeLogic.SearchInventory(txtSearch.Text));
        }

        /// <summary>
        /// Clears the inventory search results.
        /// </summary>
        private void BtnClearSearchClickEH(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            ResetInventoryBinding(_storeLogic.GetInventory());
        }

        /// <summary>
        /// Sorts the inventory list by the selected value.
        /// </summary>
        private void BtnSortClickEH(object? sender, EventArgs e)
        {
            string sortBy = cboSort.SelectedItem?.ToString() ?? "Id";
            _storeLogic.SortInventory(sortBy);
            ResetInventoryBinding(_storeLogic.GetInventory());
        }

        /// <summary>
        /// Saves inventory to a text file.
        /// </summary>
        private void BtnSaveInventoryClickEH(object? sender, EventArgs e)
        {
            bool saved = _storeLogic.WriteInventory();
            MessageBox.Show(saved ? "Inventory saved successfully." : "Inventory could not be saved.", "Save Inventory", MessageBoxButtons.OK, saved ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        /// <summary>
        /// Loads inventory from a text file.
        /// </summary>
        private void BtnLoadInventoryClickEH(object? sender, EventArgs e)
        {
            _storeLogic.ReadInventory();
            ResetInventoryBinding(_storeLogic.GetInventory());
            MessageBox.Show("Inventory load attempted.", "Load Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Removes the selected vehicle from the shopping cart.
        /// </summary>
        private void BtnRemoveFromCartClickEH(object? sender, EventArgs e)
        {
            if (lstShoppingCart.SelectedItem is not VehicleModel vehicle)
            {
                MessageBox.Show("Select a vehicle from the shopping cart first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _storeLogic.RemoveVehicleFromCart(vehicle.Id);
            _shoppingCartBindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Returns true when all form validation flags are valid.
        /// </summary>
        private bool AllInputIsValid()
        {
            return _vehicleTypeValid
                && _makeValid
                && _modelValid
                && _yearValid
                && _priceValid
                && _wheelsValid
                && _colorValid
                && _mileageValid
                && _specialtyBooleanValid
                && _specialtyDecimalValid;
        }

        /// <summary>
        /// Hides validation error labels.
        /// </summary>
        private void HideErrorLabels()
        {
            foreach (Label label in new[] { lblVehicleTypeError, lblMakeError, lblModelError, lblYearError, lblPriceError, lblWheelsError, lblColorError, lblMileageError, lblSpecialtyBooleanError, lblSpecialtyDecimalError })
            {
                label.Visible = false;
            }
        }

        /// <summary>
        /// Shows specialty controls for specialized vehicle types.
        /// </summary>
        private void ShowSpecialtyControls()
        {
            lblSpecialtyBoolean.Visible = true;
            lblSpecialtyDecimal.Visible = true;
            rdoSpecialtyYes.Visible = true;
            rdoSpecialtyNo.Visible = true;
            txtSpecialtyDecimal.Visible = true;
        }

        /// <summary>
        /// Hides specialty controls for the standard vehicle type.
        /// </summary>
        private void HideSpecialtyControls()
        {
            lblSpecialtyBoolean.Visible = false;
            lblSpecialtyDecimal.Visible = false;
            rdoSpecialtyYes.Visible = false;
            rdoSpecialtyNo.Visible = false;
            txtSpecialtyDecimal.Visible = false;
            lblSpecialtyBooleanError.Visible = false;
            lblSpecialtyDecimalError.Visible = false;
        }

        /// <summary>
        /// Clears text and specialty inputs after a vehicle is created.
        /// </summary>
        private void ClearCreateInputs()
        {
            txtMake.Clear();
            txtModel.Clear();
            txtYear.Clear();
            txtPrice.Clear();
            txtWheels.Clear();
            txtColor.Clear();
            txtMileage.Clear();
            txtSpecialtyDecimal.Clear();
            rdoSpecialtyYes.Checked = false;
            rdoSpecialtyNo.Checked = false;
        }

        /// <summary>
        /// Resets the inventory binding source to show the supplied list.
        /// </summary>
        private void ResetInventoryBinding(List<VehicleModel> vehicles)
        {
            _inventoryBindingSource.DataSource = vehicles;
            lstInventory.DataSource = _inventoryBindingSource;
            _inventoryBindingSource.ResetBindings(false);
        }
    }
}
