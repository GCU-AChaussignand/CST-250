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
    partial class FrmOrderDetails
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
            pnlOrderDetails = new Panel();
            lblOrderDetails = new Label();
            btnSaveOrder = new Button();
            btnBack = new Button();
            lblHeading = new Label();
            pnlOrderDetails.SuspendLayout();
            SuspendLayout();
            // 
            // pnlOrderDetails
            // 
            pnlOrderDetails.AutoScroll = true;
            pnlOrderDetails.BorderStyle = BorderStyle.FixedSingle;
            pnlOrderDetails.Controls.Add(lblOrderDetails);
            pnlOrderDetails.Location = new Point(24, 70);
            pnlOrderDetails.Name = "pnlOrderDetails";
            pnlOrderDetails.Size = new Size(732, 431);
            pnlOrderDetails.TabIndex = 0;
            // 
            // lblOrderDetails
            // 
            lblOrderDetails.AutoSize = true;
            lblOrderDetails.Location = new Point(18, 18);
            lblOrderDetails.MaximumSize = new Size(680, 0);
            lblOrderDetails.Name = "lblOrderDetails";
            lblOrderDetails.Size = new Size(108, 23);
            lblOrderDetails.TabIndex = 0;
            lblOrderDetails.Text = "Order Details";
            // 
            // btnSaveOrder
            // 
            btnSaveOrder.Location = new Point(594, 520);
            btnSaveOrder.Name = "btnSaveOrder";
            btnSaveOrder.Size = new Size(162, 43);
            btnSaveOrder.TabIndex = 1;
            btnSaveOrder.Text = "Save Order";
            btnSaveOrder.UseVisualStyleBackColor = true;
            btnSaveOrder.Click += BtnSaveOrderClickEH;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(24, 520);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(162, 43);
            btnBack.TabIndex = 2;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += BtnBackClickEH;
            // 
            // lblHeading
            // 
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeading.Location = new Point(24, 23);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(229, 32);
            lblHeading.TabIndex = 3;
            lblHeading.Text = "Current Pizza Order";
            // 
            // FrmOrderDetails
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 586);
            Controls.Add(lblHeading);
            Controls.Add(btnBack);
            Controls.Add(btnSaveOrder);
            Controls.Add(pnlOrderDetails);
            Font = new Font("Segoe UI", 10F);
            MinimizeBox = false;
            Name = "FrmOrderDetails";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Pizza Order Details";
            pnlOrderDetails.ResumeLayout(false);
            pnlOrderDetails.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlOrderDetails;
        private Label lblOrderDetails;
        private Button btnSaveOrder;
        private Button btnBack;
        private Label lblHeading;
    }
}
