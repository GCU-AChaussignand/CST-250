/*
 * Aaron Chaussignand
 * Grand Canyon University
 * CST-250: Programming in C# II
 * Instructor: Brian Grey
 * Activity 4 - Pizza Maker
 * August 4, 2026
 *
 * Source: Grand Canyon University. (2025). CST-250 Activity 4:
 * Building a Data-Driven Pizza Order System with N-Layer Architecture.
 */

namespace PizzaMaker
{
    partial class FrmPizzaMaker
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblNameTitle = new Label();
            txtName = new TextBox();
            grpIngredients = new GroupBox();
            chbTomatoes = new CheckBox();
            chbPeppers = new CheckBox();
            chbSausage = new CheckBox();
            chbPineapple = new CheckBox();
            chbMushrooms = new CheckBox();
            chbOlives = new CheckBox();
            chbBacon = new CheckBox();
            chbPepperoni = new CheckBox();
            lblStrangeAddOnsTitle = new Label();
            lsbStrangeAddOns = new ListBox();
            grpCrust = new GroupBox();
            rdoGlutenFree = new RadioButton();
            rdoStuffedCrust = new RadioButton();
            rdoDeepDish = new RadioButton();
            rdoThinCrust = new RadioButton();
            grpExtraGoodies = new GroupBox();
            lblCheese = new Label();
            lblCheeseTitle = new Label();
            hsbCheese = new HScrollBar();
            lblSauce = new Label();
            lblSauceTitle = new Label();
            hsbSauce = new HScrollBar();
            lblDeliveryTimeTitle = new Label();
            dtpDeliveryTime = new DateTimePicker();
            lblPizzaBoxColorTitle = new Label();
            picPizzaBoxColor = new PictureBox();
            lblPizzaPriceTitle = new Label();
            lblPizzaPrice = new Label();
            btnResetForm = new Button();
            btnCreatePizza = new Button();
            btnSeeFullOrder = new Button();
            grpIngredients.SuspendLayout();
            grpCrust.SuspendLayout();
            grpExtraGoodies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPizzaBoxColor).BeginInit();
            SuspendLayout();
            // 
            // lblNameTitle
            // 
            lblNameTitle.AutoSize = true;
            lblNameTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNameTitle.Location = new Point(24, 25);
            lblNameTitle.Name = "lblNameTitle";
            lblNameTitle.Size = new Size(52, 23);
            lblNameTitle.TabIndex = 0;
            lblNameTitle.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(86, 22);
            txtName.Name = "txtName";
            txtName.Size = new Size(252, 30);
            txtName.TabIndex = 1;
            txtName.Leave += TxtNameLeaveEH;
            // 
            // grpIngredients
            // 
            grpIngredients.Controls.Add(chbTomatoes);
            grpIngredients.Controls.Add(chbPeppers);
            grpIngredients.Controls.Add(chbSausage);
            grpIngredients.Controls.Add(chbPineapple);
            grpIngredients.Controls.Add(chbMushrooms);
            grpIngredients.Controls.Add(chbOlives);
            grpIngredients.Controls.Add(chbBacon);
            grpIngredients.Controls.Add(chbPepperoni);
            grpIngredients.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpIngredients.Location = new Point(24, 72);
            grpIngredients.Name = "grpIngredients";
            grpIngredients.Size = new Size(314, 196);
            grpIngredients.TabIndex = 2;
            grpIngredients.TabStop = false;
            grpIngredients.Text = "Ingredients";
            // 
            // ingredient checkboxes
            // 
            chbPepperoni.AutoSize = true;
            chbPepperoni.Font = new Font("Segoe UI", 10F);
            chbPepperoni.Location = new Point(18, 35);
            chbPepperoni.Name = "chbPepperoni";
            chbPepperoni.Size = new Size(111, 27);
            chbPepperoni.Text = "Pepperoni";
            chbPepperoni.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbBacon.AutoSize = true;
            chbBacon.Font = new Font("Segoe UI", 10F);
            chbBacon.Location = new Point(18, 69);
            chbBacon.Name = "chbBacon";
            chbBacon.Size = new Size(78, 27);
            chbBacon.Text = "Bacon";
            chbBacon.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbOlives.AutoSize = true;
            chbOlives.Font = new Font("Segoe UI", 10F);
            chbOlives.Location = new Point(18, 103);
            chbOlives.Name = "chbOlives";
            chbOlives.Size = new Size(78, 27);
            chbOlives.Text = "Olives";
            chbOlives.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbMushrooms.AutoSize = true;
            chbMushrooms.Font = new Font("Segoe UI", 10F);
            chbMushrooms.Location = new Point(18, 137);
            chbMushrooms.Name = "chbMushrooms";
            chbMushrooms.Size = new Size(120, 27);
            chbMushrooms.Text = "Mushrooms";
            chbMushrooms.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbPineapple.AutoSize = true;
            chbPineapple.Font = new Font("Segoe UI", 10F);
            chbPineapple.Location = new Point(164, 35);
            chbPineapple.Name = "chbPineapple";
            chbPineapple.Size = new Size(107, 27);
            chbPineapple.Text = "Pineapple";
            chbPineapple.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbSausage.AutoSize = true;
            chbSausage.Font = new Font("Segoe UI", 10F);
            chbSausage.Location = new Point(164, 69);
            chbSausage.Name = "chbSausage";
            chbSausage.Size = new Size(98, 27);
            chbSausage.Text = "Sausage";
            chbSausage.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbPeppers.AutoSize = true;
            chbPeppers.Font = new Font("Segoe UI", 10F);
            chbPeppers.Location = new Point(164, 103);
            chbPeppers.Name = "chbPeppers";
            chbPeppers.Size = new Size(93, 27);
            chbPeppers.Text = "Peppers";
            chbPeppers.CheckedChanged += ChbIngredientCheckedChangedEH;
            chbTomatoes.AutoSize = true;
            chbTomatoes.Font = new Font("Segoe UI", 10F);
            chbTomatoes.Location = new Point(164, 137);
            chbTomatoes.Name = "chbTomatoes";
            chbTomatoes.Size = new Size(104, 27);
            chbTomatoes.Text = "Tomatoes";
            chbTomatoes.CheckedChanged += ChbIngredientCheckedChangedEH;
            // 
            // lblStrangeAddOnsTitle
            // 
            lblStrangeAddOnsTitle.AutoSize = true;
            lblStrangeAddOnsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStrangeAddOnsTitle.Location = new Point(24, 287);
            lblStrangeAddOnsTitle.Name = "lblStrangeAddOnsTitle";
            lblStrangeAddOnsTitle.Size = new Size(139, 23);
            lblStrangeAddOnsTitle.Text = "Strange Add Ons";
            // 
            // lsbStrangeAddOns
            // 
            lsbStrangeAddOns.FormattingEnabled = true;
            lsbStrangeAddOns.ItemHeight = 23;
            lsbStrangeAddOns.Items.AddRange(new object[] { "Hotdogs", "Eggplant", "Artichoke Hearts", "Eggs", "Peanut Butter", "Prosciutto", "Honey", "Chili Thread", "Olive Oil", "Arugula", "Garlic", "Chicken", "Anchovies", "BBQ Sauce", "Green Onion", "Red Onions", "Carrots", "Peanuts" });
            lsbStrangeAddOns.Location = new Point(24, 316);
            lsbStrangeAddOns.Name = "lsbStrangeAddOns";
            lsbStrangeAddOns.SelectionMode = SelectionMode.MultiSimple;
            lsbStrangeAddOns.Size = new Size(314, 188);
            lsbStrangeAddOns.TabIndex = 4;
            lsbStrangeAddOns.SelectedIndexChanged += LsbStrangeAddOnsSelectedIndexChangedEH;
            // 
            // grpCrust
            // 
            grpCrust.Controls.Add(rdoGlutenFree);
            grpCrust.Controls.Add(rdoStuffedCrust);
            grpCrust.Controls.Add(rdoDeepDish);
            grpCrust.Controls.Add(rdoThinCrust);
            grpCrust.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpCrust.Location = new Point(365, 72);
            grpCrust.Name = "grpCrust";
            grpCrust.Size = new Size(250, 196);
            grpCrust.TabIndex = 5;
            grpCrust.TabStop = false;
            grpCrust.Text = "Crust";
            rdoThinCrust.AutoSize = true;
            rdoThinCrust.Font = new Font("Segoe UI", 10F);
            rdoThinCrust.Location = new Point(20, 35);
            rdoThinCrust.Name = "rdoThinCrust";
            rdoThinCrust.Size = new Size(111, 27);
            rdoThinCrust.Text = "Thin Crust";
            rdoThinCrust.CheckedChanged += RdoCrustCheckedChangedEH;
            rdoDeepDish.AutoSize = true;
            rdoDeepDish.Font = new Font("Segoe UI", 10F);
            rdoDeepDish.Location = new Point(20, 69);
            rdoDeepDish.Name = "rdoDeepDish";
            rdoDeepDish.Size = new Size(109, 27);
            rdoDeepDish.Text = "Deep Dish";
            rdoDeepDish.CheckedChanged += RdoCrustCheckedChangedEH;
            rdoStuffedCrust.AutoSize = true;
            rdoStuffedCrust.Font = new Font("Segoe UI", 10F);
            rdoStuffedCrust.Location = new Point(20, 103);
            rdoStuffedCrust.Name = "rdoStuffedCrust";
            rdoStuffedCrust.Size = new Size(141, 27);
            rdoStuffedCrust.Text = "Stuffed Crust";
            rdoStuffedCrust.CheckedChanged += RdoCrustCheckedChangedEH;
            rdoGlutenFree.AutoSize = true;
            rdoGlutenFree.Font = new Font("Segoe UI", 10F);
            rdoGlutenFree.Location = new Point(20, 137);
            rdoGlutenFree.Name = "rdoGlutenFree";
            rdoGlutenFree.Size = new Size(125, 27);
            rdoGlutenFree.Text = "Gluten Free";
            rdoGlutenFree.CheckedChanged += RdoCrustCheckedChangedEH;
            // 
            // grpExtraGoodies
            // 
            grpExtraGoodies.Controls.Add(lblCheese);
            grpExtraGoodies.Controls.Add(lblCheeseTitle);
            grpExtraGoodies.Controls.Add(hsbCheese);
            grpExtraGoodies.Controls.Add(lblSauce);
            grpExtraGoodies.Controls.Add(lblSauceTitle);
            grpExtraGoodies.Controls.Add(hsbSauce);
            grpExtraGoodies.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpExtraGoodies.Location = new Point(365, 287);
            grpExtraGoodies.Name = "grpExtraGoodies";
            grpExtraGoodies.Size = new Size(382, 217);
            grpExtraGoodies.TabIndex = 6;
            grpExtraGoodies.TabStop = false;
            grpExtraGoodies.Text = "Extra Goodies";
            lblSauceTitle.AutoSize = true;
            lblSauceTitle.Font = new Font("Segoe UI", 10F);
            lblSauceTitle.Location = new Point(18, 37);
            lblSauceTitle.Name = "lblSauceTitle";
            lblSauceTitle.Size = new Size(144, 23);
            lblSauceTitle.Text = "Amount of Sauce";
            lblSauce.AutoSize = true;
            lblSauce.Font = new Font("Segoe UI", 10F);
            lblSauce.Location = new Point(168, 37);
            lblSauce.Name = "lblSauce";
            lblSauce.Size = new Size(20, 23);
            lblSauce.Text = "0";
            hsbSauce.LargeChange = 10;
            hsbSauce.Location = new Point(22, 70);
            hsbSauce.Maximum = 100;
            hsbSauce.Name = "hsbSauce";
            hsbSauce.Size = new Size(338, 25);
            hsbSauce.SmallChange = 1;
            hsbSauce.ValueChanged += HsbExtraGoodiesValueChangedEH;
            lblCheeseTitle.AutoSize = true;
            lblCheeseTitle.Font = new Font("Segoe UI", 10F);
            lblCheeseTitle.Location = new Point(18, 120);
            lblCheeseTitle.Name = "lblCheeseTitle";
            lblCheeseTitle.Size = new Size(151, 23);
            lblCheeseTitle.Text = "Amount of Cheese";
            lblCheese.AutoSize = true;
            lblCheese.Font = new Font("Segoe UI", 10F);
            lblCheese.Location = new Point(175, 120);
            lblCheese.Name = "lblCheese";
            lblCheese.Size = new Size(20, 23);
            lblCheese.Text = "0";
            hsbCheese.LargeChange = 10;
            hsbCheese.Location = new Point(22, 154);
            hsbCheese.Maximum = 100;
            hsbCheese.Name = "hsbCheese";
            hsbCheese.Size = new Size(338, 25);
            hsbCheese.SmallChange = 1;
            hsbCheese.ValueChanged += HsbExtraGoodiesValueChangedEH;
            // 
            // delivery, color, price, buttons
            // 
            lblDeliveryTimeTitle.AutoSize = true;
            lblDeliveryTimeTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDeliveryTimeTitle.Location = new Point(777, 25);
            lblDeliveryTimeTitle.Name = "lblDeliveryTimeTitle";
            lblDeliveryTimeTitle.Size = new Size(124, 23);
            lblDeliveryTimeTitle.Text = "Delivery Time";
            dtpDeliveryTime.CustomFormat = "MM/dd/yyyy hh:mm tt";
            dtpDeliveryTime.Format = DateTimePickerFormat.Custom;
            dtpDeliveryTime.Location = new Point(777, 55);
            dtpDeliveryTime.Name = "dtpDeliveryTime";
            dtpDeliveryTime.Size = new Size(267, 30);
            dtpDeliveryTime.ValueChanged += DtpDeliveryTimeValueChangedEH;
            lblPizzaBoxColorTitle.AutoSize = true;
            lblPizzaBoxColorTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPizzaBoxColorTitle.Location = new Point(777, 113);
            lblPizzaBoxColorTitle.Name = "lblPizzaBoxColorTitle";
            lblPizzaBoxColorTitle.Size = new Size(137, 23);
            lblPizzaBoxColorTitle.Text = "Pizza Box Color";
            picPizzaBoxColor.BackColor = Color.White;
            picPizzaBoxColor.BorderStyle = BorderStyle.FixedSingle;
            picPizzaBoxColor.Cursor = Cursors.Hand;
            picPizzaBoxColor.Location = new Point(777, 143);
            picPizzaBoxColor.Name = "picPizzaBoxColor";
            picPizzaBoxColor.Size = new Size(267, 84);
            picPizzaBoxColor.TabStop = false;
            picPizzaBoxColor.Click += PicPizzaBoxColorClickEH;
            lblPizzaPriceTitle.AutoSize = true;
            lblPizzaPriceTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPizzaPriceTitle.Location = new Point(777, 273);
            lblPizzaPriceTitle.Name = "lblPizzaPriceTitle";
            lblPizzaPriceTitle.Size = new Size(119, 28);
            lblPizzaPriceTitle.Text = "Pizza Price:";
            lblPizzaPrice.AutoSize = true;
            lblPizzaPrice.Font = new Font("Segoe UI", 12F);
            lblPizzaPrice.ForeColor = Color.Red;
            lblPizzaPrice.Location = new Point(908, 273);
            lblPizzaPrice.Name = "lblPizzaPrice";
            lblPizzaPrice.Size = new Size(60, 28);
            lblPizzaPrice.Text = "$0.00";
            btnResetForm.Location = new Point(777, 330);
            btnResetForm.Name = "btnResetForm";
            btnResetForm.Size = new Size(125, 42);
            btnResetForm.Text = "Reset Form";
            btnResetForm.UseVisualStyleBackColor = true;
            btnResetForm.Click += BtnResetFormClickEH;
            btnCreatePizza.Location = new Point(919, 330);
            btnCreatePizza.Name = "btnCreatePizza";
            btnCreatePizza.Size = new Size(125, 42);
            btnCreatePizza.Text = "Create Pizza";
            btnCreatePizza.UseVisualStyleBackColor = true;
            btnCreatePizza.Click += BtnCreatePizzaClickEH;
            btnSeeFullOrder.Location = new Point(777, 392);
            btnSeeFullOrder.Name = "btnSeeFullOrder";
            btnSeeFullOrder.Size = new Size(267, 42);
            btnSeeFullOrder.Text = "See Full Order";
            btnSeeFullOrder.UseVisualStyleBackColor = true;
            btnSeeFullOrder.Click += BtnSeeFullOrderClickEH;
            // 
            // FrmPizzaMaker
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 535);
            Controls.Add(btnSeeFullOrder);
            Controls.Add(btnCreatePizza);
            Controls.Add(btnResetForm);
            Controls.Add(lblPizzaPrice);
            Controls.Add(lblPizzaPriceTitle);
            Controls.Add(picPizzaBoxColor);
            Controls.Add(lblPizzaBoxColorTitle);
            Controls.Add(dtpDeliveryTime);
            Controls.Add(lblDeliveryTimeTitle);
            Controls.Add(grpExtraGoodies);
            Controls.Add(grpCrust);
            Controls.Add(lsbStrangeAddOns);
            Controls.Add(lblStrangeAddOnsTitle);
            Controls.Add(grpIngredients);
            Controls.Add(txtName);
            Controls.Add(lblNameTitle);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmPizzaMaker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pizza Maker";
            grpIngredients.ResumeLayout(false);
            grpIngredients.PerformLayout();
            grpCrust.ResumeLayout(false);
            grpCrust.PerformLayout();
            grpExtraGoodies.ResumeLayout(false);
            grpExtraGoodies.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPizzaBoxColor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNameTitle;
        private TextBox txtName;
        private GroupBox grpIngredients;
        private CheckBox chbPepperoni;
        private CheckBox chbBacon;
        private CheckBox chbOlives;
        private CheckBox chbMushrooms;
        private CheckBox chbPineapple;
        private CheckBox chbSausage;
        private CheckBox chbPeppers;
        private CheckBox chbTomatoes;
        private Label lblStrangeAddOnsTitle;
        private ListBox lsbStrangeAddOns;
        private GroupBox grpCrust;
        private RadioButton rdoThinCrust;
        private RadioButton rdoDeepDish;
        private RadioButton rdoStuffedCrust;
        private RadioButton rdoGlutenFree;
        private GroupBox grpExtraGoodies;
        private HScrollBar hsbSauce;
        private Label lblSauceTitle;
        private Label lblSauce;
        private HScrollBar hsbCheese;
        private Label lblCheeseTitle;
        private Label lblCheese;
        private Label lblDeliveryTimeTitle;
        private DateTimePicker dtpDeliveryTime;
        private Label lblPizzaBoxColorTitle;
        private PictureBox picPizzaBoxColor;
        private Label lblPizzaPriceTitle;
        private Label lblPizzaPrice;
        private Button btnResetForm;
        private Button btnCreatePizza;
        private Button btnSeeFullOrder;
    }
}
