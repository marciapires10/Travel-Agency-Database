namespace WindowsFormsApp2
{
    partial class add_flight
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
            this.addflight_airline_label = new System.Windows.Forms.Label();
            this.addflight_classtype_label = new System.Windows.Forms.Label();
            this.addflight_price_label = new System.Windows.Forms.Label();
            this.addflght_departure_loc_label = new System.Windows.Forms.Label();
            this.addflght_departure_date_label = new System.Windows.Forms.Label();
            this.addflght_arrival_loc_label = new System.Windows.Forms.Label();
            this.addflght_arrival_date_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.addflight_cancel_button = new System.Windows.Forms.Button();
            this.addflight_addnew_button = new System.Windows.Forms.Button();
            this.addflight_airline_combobox = new System.Windows.Forms.ComboBox();
            this.addflight_classtype_combobox = new System.Windows.Forms.ComboBox();
            this.addflight_arrTime = new System.Windows.Forms.DateTimePicker();
            this.addflight_depTime = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.addflight_depLoc = new System.Windows.Forms.ComboBox();
            this.addflight_departure_addLoc = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.addflight_arrLoc = new System.Windows.Forms.ComboBox();
            this.addflight_arrival_addLoc = new System.Windows.Forms.Button();
            this.p1g9DataSet1 = new WindowsFormsApp2.p1g9DataSet();
            this.addflight_price_textbox = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.p1g9DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addflight_price_textbox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.addflight_airline_label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.addflight_classtype_label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.addflight_price_label, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.addflght_departure_loc_label, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.addflght_departure_date_label, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.addflght_arrival_loc_label, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.addflght_arrival_date_label, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.addflight_airline_combobox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.addflight_classtype_combobox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.addflight_arrTime, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.addflight_depTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.addflight_price_textbox, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 452);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // addflight_airline_label
            // 
            this.addflight_airline_label.AutoSize = true;
            this.addflight_airline_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflight_airline_label.Location = new System.Drawing.Point(3, 0);
            this.addflight_airline_label.Name = "addflight_airline_label";
            this.addflight_airline_label.Size = new System.Drawing.Size(161, 56);
            this.addflight_airline_label.TabIndex = 0;
            this.addflight_airline_label.Text = "Select Airline:";
            this.addflight_airline_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addflight_classtype_label
            // 
            this.addflight_classtype_label.AutoSize = true;
            this.addflight_classtype_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflight_classtype_label.Location = new System.Drawing.Point(3, 56);
            this.addflight_classtype_label.Name = "addflight_classtype_label";
            this.addflight_classtype_label.Size = new System.Drawing.Size(161, 56);
            this.addflight_classtype_label.TabIndex = 1;
            this.addflight_classtype_label.Text = "Select Class Type:";
            this.addflight_classtype_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addflight_price_label
            // 
            this.addflight_price_label.AutoSize = true;
            this.addflight_price_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflight_price_label.Location = new System.Drawing.Point(3, 112);
            this.addflight_price_label.Name = "addflight_price_label";
            this.addflight_price_label.Size = new System.Drawing.Size(161, 56);
            this.addflight_price_label.TabIndex = 2;
            this.addflight_price_label.Text = "Price:";
            this.addflight_price_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addflght_departure_loc_label
            // 
            this.addflght_departure_loc_label.AutoSize = true;
            this.addflght_departure_loc_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflght_departure_loc_label.Location = new System.Drawing.Point(3, 168);
            this.addflght_departure_loc_label.Name = "addflght_departure_loc_label";
            this.addflght_departure_loc_label.Size = new System.Drawing.Size(161, 56);
            this.addflght_departure_loc_label.TabIndex = 3;
            this.addflght_departure_loc_label.Text = "Select Departure Location:";
            this.addflght_departure_loc_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addflght_departure_date_label
            // 
            this.addflght_departure_date_label.AutoSize = true;
            this.addflght_departure_date_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflght_departure_date_label.Location = new System.Drawing.Point(3, 224);
            this.addflght_departure_date_label.Name = "addflght_departure_date_label";
            this.addflght_departure_date_label.Size = new System.Drawing.Size(161, 56);
            this.addflght_departure_date_label.TabIndex = 4;
            this.addflght_departure_date_label.Text = "Select Departure Date:";
            this.addflght_departure_date_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addflght_arrival_loc_label
            // 
            this.addflght_arrival_loc_label.AutoSize = true;
            this.addflght_arrival_loc_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflght_arrival_loc_label.Location = new System.Drawing.Point(3, 280);
            this.addflght_arrival_loc_label.Name = "addflght_arrival_loc_label";
            this.addflght_arrival_loc_label.Size = new System.Drawing.Size(161, 56);
            this.addflght_arrival_loc_label.TabIndex = 5;
            this.addflght_arrival_loc_label.Text = "Select Arrival Location:";
            this.addflght_arrival_loc_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // addflght_arrival_date_label
            // 
            this.addflght_arrival_date_label.AutoSize = true;
            this.addflght_arrival_date_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflght_arrival_date_label.Location = new System.Drawing.Point(3, 336);
            this.addflght_arrival_date_label.Name = "addflght_arrival_date_label";
            this.addflght_arrival_date_label.Size = new System.Drawing.Size(161, 56);
            this.addflght_arrival_date_label.TabIndex = 6;
            this.addflght_arrival_date_label.Text = "Select Arrival Date:";
            this.addflght_arrival_date_label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.addflight_cancel_button, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.addflight_addnew_button, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(170, 395);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(498, 54);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // addflight_cancel_button
            // 
            this.addflight_cancel_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflight_cancel_button.Location = new System.Drawing.Point(3, 3);
            this.addflight_cancel_button.Name = "addflight_cancel_button";
            this.addflight_cancel_button.Size = new System.Drawing.Size(243, 48);
            this.addflight_cancel_button.TabIndex = 10;
            this.addflight_cancel_button.Text = "Cancel";
            this.addflight_cancel_button.UseVisualStyleBackColor = true;
            this.addflight_cancel_button.Click += new System.EventHandler(this.addflight_cancel_button_Click);
            // 
            // addflight_addnew_button
            // 
            this.addflight_addnew_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addflight_addnew_button.Location = new System.Drawing.Point(252, 3);
            this.addflight_addnew_button.Name = "addflight_addnew_button";
            this.addflight_addnew_button.Size = new System.Drawing.Size(243, 48);
            this.addflight_addnew_button.TabIndex = 9;
            this.addflight_addnew_button.Text = "Add New Flight";
            this.addflight_addnew_button.UseVisualStyleBackColor = true;
            this.addflight_addnew_button.Click += new System.EventHandler(this.addflight_addnew_button_Click);
            // 
            // addflight_airline_combobox
            // 
            this.addflight_airline_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_airline_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addflight_airline_combobox.FormattingEnabled = true;
            this.addflight_airline_combobox.Location = new System.Drawing.Point(170, 17);
            this.addflight_airline_combobox.Name = "addflight_airline_combobox";
            this.addflight_airline_combobox.Size = new System.Drawing.Size(498, 21);
            this.addflight_airline_combobox.TabIndex = 0;
            this.addflight_airline_combobox.SelectedIndexChanged += new System.EventHandler(this.addflight_airline_combobox_SelectedIndexChanged);
            // 
            // addflight_classtype_combobox
            // 
            this.addflight_classtype_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_classtype_combobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addflight_classtype_combobox.FormattingEnabled = true;
            this.addflight_classtype_combobox.Items.AddRange(new object[] {
            "Economy",
            "Business",
            "Executive"});
            this.addflight_classtype_combobox.Location = new System.Drawing.Point(170, 73);
            this.addflight_classtype_combobox.Name = "addflight_classtype_combobox";
            this.addflight_classtype_combobox.Size = new System.Drawing.Size(498, 21);
            this.addflight_classtype_combobox.TabIndex = 1;
            this.addflight_classtype_combobox.SelectedIndexChanged += new System.EventHandler(this.addflight_classtype_combobox_SelectedIndexChanged);
            // 
            // addflight_arrTime
            // 
            this.addflight_arrTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_arrTime.CustomFormat = "dd/MM/yy HH:mm";
            this.addflight_arrTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.addflight_arrTime.Location = new System.Drawing.Point(170, 354);
            this.addflight_arrTime.MinDate = new System.DateTime(2020, 6, 5, 0, 0, 0, 0);
            this.addflight_arrTime.Name = "addflight_arrTime";
            this.addflight_arrTime.Size = new System.Drawing.Size(498, 20);
            this.addflight_arrTime.TabIndex = 8;
            this.addflight_arrTime.Value = new System.DateTime(2020, 6, 5, 16, 15, 26, 0);
            // 
            // addflight_depTime
            // 
            this.addflight_depTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_depTime.CustomFormat = "dd/MM/yy HH:mm";
            this.addflight_depTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.addflight_depTime.Location = new System.Drawing.Point(170, 242);
            this.addflight_depTime.MinDate = new System.DateTime(2020, 6, 5, 0, 0, 0, 0);
            this.addflight_depTime.Name = "addflight_depTime";
            this.addflight_depTime.Size = new System.Drawing.Size(498, 20);
            this.addflight_depTime.TabIndex = 5;
            this.addflight_depTime.Value = new System.DateTime(2020, 6, 5, 16, 14, 6, 0);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.Controls.Add(this.addflight_depLoc, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.addflight_departure_addLoc, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(170, 171);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(498, 50);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // addflight_depLoc
            // 
            this.addflight_depLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_depLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addflight_depLoc.FormattingEnabled = true;
            this.addflight_depLoc.Location = new System.Drawing.Point(3, 14);
            this.addflight_depLoc.Name = "addflight_depLoc";
            this.addflight_depLoc.Size = new System.Drawing.Size(342, 21);
            this.addflight_depLoc.TabIndex = 3;
            this.addflight_depLoc.SelectedIndexChanged += new System.EventHandler(this.addflight_depLoc_SelectedIndexChanged);
            // 
            // addflight_departure_addLoc
            // 
            this.addflight_departure_addLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_departure_addLoc.Location = new System.Drawing.Point(351, 13);
            this.addflight_departure_addLoc.Name = "addflight_departure_addLoc";
            this.addflight_departure_addLoc.Size = new System.Drawing.Size(144, 23);
            this.addflight_departure_addLoc.TabIndex = 4;
            this.addflight_departure_addLoc.Text = "New";
            this.addflight_departure_addLoc.UseVisualStyleBackColor = true;
            this.addflight_departure_addLoc.Click += new System.EventHandler(this.addflight_departure_addLoc_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.addflight_arrLoc, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.addflight_arrival_addLoc, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(170, 283);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(498, 50);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // addflight_arrLoc
            // 
            this.addflight_arrLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_arrLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addflight_arrLoc.FormattingEnabled = true;
            this.addflight_arrLoc.ItemHeight = 13;
            this.addflight_arrLoc.Location = new System.Drawing.Point(3, 14);
            this.addflight_arrLoc.Name = "addflight_arrLoc";
            this.addflight_arrLoc.Size = new System.Drawing.Size(342, 21);
            this.addflight_arrLoc.TabIndex = 0;
            this.addflight_arrLoc.SelectedIndexChanged += new System.EventHandler(this.addflight_arrLoc_SelectedIndexChanged);
            // 
            // addflight_arrival_addLoc
            // 
            this.addflight_arrival_addLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_arrival_addLoc.Location = new System.Drawing.Point(351, 13);
            this.addflight_arrival_addLoc.Name = "addflight_arrival_addLoc";
            this.addflight_arrival_addLoc.Size = new System.Drawing.Size(144, 23);
            this.addflight_arrival_addLoc.TabIndex = 7;
            this.addflight_arrival_addLoc.Text = "New";
            this.addflight_arrival_addLoc.UseVisualStyleBackColor = true;
            this.addflight_arrival_addLoc.Click += new System.EventHandler(this.addflight_arrival_addLoc_Click);
            // 
            // p1g9DataSet1
            // 
            this.p1g9DataSet1.DataSetName = "p1g9DataSet";
            this.p1g9DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // addflight_price_textbox
            // 
            this.addflight_price_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addflight_price_textbox.DecimalPlaces = 2;
            this.addflight_price_textbox.Location = new System.Drawing.Point(170, 130);
            this.addflight_price_textbox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.addflight_price_textbox.Name = "addflight_price_textbox";
            this.addflight_price_textbox.Size = new System.Drawing.Size(498, 20);
            this.addflight_price_textbox.TabIndex = 2;
            // 
            // add_flight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 452);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "add_flight";
            this.Text = "Add New Flight";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.p1g9DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addflight_price_textbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label addflight_airline_label;
        private System.Windows.Forms.Label addflight_classtype_label;
        private System.Windows.Forms.Label addflight_price_label;
        private System.Windows.Forms.Label addflght_departure_loc_label;
        private System.Windows.Forms.Label addflght_departure_date_label;
        private System.Windows.Forms.Label addflght_arrival_loc_label;
        private System.Windows.Forms.Label addflght_arrival_date_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button addflight_cancel_button;
        private System.Windows.Forms.Button addflight_addnew_button;
        private System.Windows.Forms.ComboBox addflight_airline_combobox;
        private System.Windows.Forms.ComboBox addflight_classtype_combobox;
        private System.Windows.Forms.DateTimePicker addflight_arrTime;
        private System.Windows.Forms.DateTimePicker addflight_depTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox addflight_depLoc;
        private System.Windows.Forms.Button addflight_departure_addLoc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox addflight_arrLoc;
        private System.Windows.Forms.Button addflight_arrival_addLoc;
        private p1g9DataSet p1g9DataSet1;
        private System.Windows.Forms.NumericUpDown addflight_price_textbox;
    }
}