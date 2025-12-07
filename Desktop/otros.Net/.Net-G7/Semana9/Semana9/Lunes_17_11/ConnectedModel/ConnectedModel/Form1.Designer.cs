namespace ConnectedModel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxAirline = new TextBox();
            textBoxDestination = new TextBox();
            label2 = new Label();
            textBoxFlightNumber = new TextBox();
            label3 = new Label();
            label4 = new Label();
            comboBoxAirPlaneType = new ComboBox();
            buttonAddFlight = new Button();
            comboBoxSelect = new ComboBox();
            buttonUpdate = new Button();
            buttonDelete = new Button();
            dataGridViewFlight = new DataGridView();
            buttonRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFlight).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(89, 43);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 0;
            label1.Text = "AirLine";
            // 
            // textBoxAirline
            // 
            textBoxAirline.Location = new Point(156, 40);
            textBoxAirline.Name = "textBoxAirline";
            textBoxAirline.Size = new Size(177, 27);
            textBoxAirline.TabIndex = 1;
            // 
            // textBoxDestination
            // 
            textBoxDestination.Location = new Point(156, 156);
            textBoxDestination.Name = "textBoxDestination";
            textBoxDestination.Size = new Size(177, 27);
            textBoxDestination.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(54, 163);
            label2.Name = "label2";
            label2.Size = new Size(90, 20);
            label2.TabIndex = 2;
            label2.Text = "Destinattion";
            // 
            // textBoxFlightNumber
            // 
            textBoxFlightNumber.Location = new Point(156, 97);
            textBoxFlightNumber.Name = "textBoxFlightNumber";
            textBoxFlightNumber.Size = new Size(177, 27);
            textBoxFlightNumber.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(40, 100);
            label3.Name = "label3";
            label3.Size = new Size(104, 20);
            label3.TabIndex = 4;
            label3.Text = "Flight Number";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 226);
            label4.Name = "label4";
            label4.Size = new Size(99, 20);
            label4.TabIndex = 6;
            label4.Text = "AirPlane Type";
            // 
            // comboBoxAirPlaneType
            // 
            comboBoxAirPlaneType.FormattingEnabled = true;
            comboBoxAirPlaneType.Location = new Point(156, 226);
            comboBoxAirPlaneType.Name = "comboBoxAirPlaneType";
            comboBoxAirPlaneType.Size = new Size(177, 28);
            comboBoxAirPlaneType.TabIndex = 7;
            // 
            // buttonAddFlight
            // 
            buttonAddFlight.Location = new Point(566, 44);
            buttonAddFlight.Name = "buttonAddFlight";
            buttonAddFlight.Size = new Size(197, 29);
            buttonAddFlight.TabIndex = 8;
            buttonAddFlight.Text = "Add Flight";
            buttonAddFlight.UseVisualStyleBackColor = true;
            buttonAddFlight.Click += buttonAddFlight_Click;
            // 
            // comboBoxSelect
            // 
            comboBoxSelect.FormattingEnabled = true;
            comboBoxSelect.Location = new Point(515, 135);
            comboBoxSelect.Name = "comboBoxSelect";
            comboBoxSelect.Size = new Size(292, 28);
            comboBoxSelect.TabIndex = 9;
            comboBoxSelect.SelectedIndexChanged += comboBoxSelect_SelectedIndexChanged;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(515, 226);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(94, 29);
            buttonUpdate.TabIndex = 10;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(707, 225);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(94, 29);
            buttonDelete.TabIndex = 11;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // dataGridViewFlight
            // 
            dataGridViewFlight.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFlight.Location = new Point(40, 320);
            dataGridViewFlight.Name = "dataGridViewFlight";
            dataGridViewFlight.RowHeadersWidth = 51;
            dataGridViewFlight.Size = new Size(761, 272);
            dataGridViewFlight.TabIndex = 12;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(707, 623);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(94, 29);
            buttonRefresh.TabIndex = 13;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(840, 682);
            Controls.Add(buttonRefresh);
            Controls.Add(dataGridViewFlight);
            Controls.Add(buttonDelete);
            Controls.Add(buttonUpdate);
            Controls.Add(comboBoxSelect);
            Controls.Add(buttonAddFlight);
            Controls.Add(comboBoxAirPlaneType);
            Controls.Add(label4);
            Controls.Add(textBoxFlightNumber);
            Controls.Add(label3);
            Controls.Add(textBoxDestination);
            Controls.Add(label2);
            Controls.Add(textBoxAirline);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewFlight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxAirline;
        private TextBox textBoxDestination;
        private Label label2;
        private TextBox textBoxFlightNumber;
        private Label label3;
        private Label label4;
        private ComboBox comboBoxAirPlaneType;
        private Button buttonAddFlight;
        private ComboBox comboBoxSelect;
        private Button buttonUpdate;
        private Button buttonDelete;
        private DataGridView dataGridViewFlight;
        private Button buttonRefresh;
    }
}
