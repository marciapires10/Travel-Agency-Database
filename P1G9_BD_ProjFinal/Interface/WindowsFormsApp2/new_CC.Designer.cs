namespace WindowsFormsApp2
{
    partial class new_CC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cc_cancel_button = new System.Windows.Forms.Button();
            this.cc_addNew_button = new System.Windows.Forms.Button();
            this.city_textbox = new System.Windows.Forms.TextBox();
            this.country_textbox = new System.Windows.Forms.TextBox();
            this.city_label = new System.Windows.Forms.Label();
            this.country_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.city_textbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.country_textbox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.city_label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.country_label, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(296, 110);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.cc_cancel_button, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cc_addNew_button, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(91, 72);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(202, 35);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cc_cancel_button
            // 
            this.cc_cancel_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc_cancel_button.Location = new System.Drawing.Point(3, 3);
            this.cc_cancel_button.Name = "cc_cancel_button";
            this.cc_cancel_button.Size = new System.Drawing.Size(95, 29);
            this.cc_cancel_button.TabIndex = 3;
            this.cc_cancel_button.Text = "Cancel";
            this.cc_cancel_button.UseVisualStyleBackColor = true;
            this.cc_cancel_button.Click += new System.EventHandler(this.cc_cancel_button_Click);
            // 
            // cc_addNew_button
            // 
            this.cc_addNew_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc_addNew_button.Location = new System.Drawing.Point(104, 3);
            this.cc_addNew_button.Name = "cc_addNew_button";
            this.cc_addNew_button.Size = new System.Drawing.Size(95, 29);
            this.cc_addNew_button.TabIndex = 2;
            this.cc_addNew_button.Text = "Add";
            this.cc_addNew_button.UseVisualStyleBackColor = true;
            this.cc_addNew_button.Click += new System.EventHandler(this.cc_addNew_button_Click);
            // 
            // city_textbox
            // 
            this.city_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.city_textbox.Location = new System.Drawing.Point(91, 7);
            this.city_textbox.Name = "city_textbox";
            this.city_textbox.Size = new System.Drawing.Size(202, 20);
            this.city_textbox.TabIndex = 0;
            // 
            // country_textbox
            // 
            this.country_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.country_textbox.Location = new System.Drawing.Point(91, 41);
            this.country_textbox.Name = "country_textbox";
            this.country_textbox.Size = new System.Drawing.Size(202, 20);
            this.country_textbox.TabIndex = 1;
            // 
            // city_label
            // 
            this.city_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.city_label.AutoSize = true;
            this.city_label.Location = new System.Drawing.Point(3, 10);
            this.city_label.Name = "city_label";
            this.city_label.Size = new System.Drawing.Size(82, 13);
            this.city_label.TabIndex = 3;
            this.city_label.Text = "City name:";
            this.city_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // country_label
            // 
            this.country_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.country_label.AutoSize = true;
            this.country_label.Location = new System.Drawing.Point(3, 45);
            this.country_label.Name = "country_label";
            this.country_label.Size = new System.Drawing.Size(82, 13);
            this.country_label.TabIndex = 4;
            this.country_label.Text = "Country name:";
            this.country_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // new_CC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 110);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "new_CC";
            this.Text = "New City & Country";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button cc_cancel_button;
        private System.Windows.Forms.Button cc_addNew_button;
        private System.Windows.Forms.TextBox city_textbox;
        private System.Windows.Forms.TextBox country_textbox;
        private System.Windows.Forms.Label city_label;
        private System.Windows.Forms.Label country_label;
    }
}