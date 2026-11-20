namespace WarShipStorage
{
    partial class ShipCreationForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblDisplacement = new System.Windows.Forms.Label();
            this.numDisplacement = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnPickColor = new System.Windows.Forms.Button();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.lblDeckColor = new System.Windows.Forms.Label();
            this.btnPickDeckColor = new System.Windows.Forms.Button();
            this.pnlDeckPreview = new System.Windows.Forms.Panel();
            this.lblAircraftCount = new System.Windows.Forms.Label();
            this.numAircraftCount = new System.Windows.Forms.NumericUpDown();
            this.chkCatapult = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

           
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(20, 18);
            this.lblType.Name = "lblType";
            this.lblType.Text = "Тип:";

            
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Location = new System.Drawing.Point(150, 15);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(200, 28);
            this.cmbType.Items.AddRange(new object[] { "Военный корабль", "Авианосец" });
            this.cmbType.SelectedIndex = 0;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);

            
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(20, 60);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Text = "Скорость (узлы):";

            
            this.numSpeed.Location = new System.Drawing.Point(150, 58);
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(120, 27);
            this.numSpeed.Minimum = 1;
            this.numSpeed.Maximum = 100;
            this.numSpeed.Value = 20;

            
            this.lblDisplacement.AutoSize = true;
            this.lblDisplacement.Location = new System.Drawing.Point(20, 100);
            this.lblDisplacement.Name = "lblDisplacement";
            this.lblDisplacement.Text = "Водоизмещение (т):";

            
            this.numDisplacement.Location = new System.Drawing.Point(150, 98);
            this.numDisplacement.Name = "numDisplacement";
            this.numDisplacement.Size = new System.Drawing.Size(120, 27);
            this.numDisplacement.Minimum = 100;
            this.numDisplacement.Maximum = 100000;
            this.numDisplacement.Value = 5000;

            
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(20, 140);
            this.lblColor.Name = "lblColor";
            this.lblColor.Text = "Основной цвет:";

            
            this.btnPickColor.Location = new System.Drawing.Point(150, 136);
            this.btnPickColor.Name = "btnPickColor";
            this.btnPickColor.Size = new System.Drawing.Size(100, 30);
            this.btnPickColor.Text = "Выбрать...";
            this.btnPickColor.UseVisualStyleBackColor = true;
            this.btnPickColor.Click += new System.EventHandler(this.btnPickColor_Click);

            
            this.pnlColorPreview.Location = new System.Drawing.Point(260, 138);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(30, 25);
            this.pnlColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColorPreview.BackColor = System.Drawing.Color.Gray;

            
            this.lblDeckColor.AutoSize = true;
            this.lblDeckColor.Location = new System.Drawing.Point(20, 180);
            this.lblDeckColor.Name = "lblDeckColor";
            this.lblDeckColor.Text = "Цвет палубы:";

           
            this.btnPickDeckColor.Location = new System.Drawing.Point(150, 176);
            this.btnPickDeckColor.Name = "btnPickDeckColor";
            this.btnPickDeckColor.Size = new System.Drawing.Size(100, 30);
            this.btnPickDeckColor.Text = "Выбрать...";
            this.btnPickDeckColor.UseVisualStyleBackColor = true;
            this.btnPickDeckColor.Click += new System.EventHandler(this.btnPickDeckColor_Click);

            
            this.pnlDeckPreview.Location = new System.Drawing.Point(260, 178);
            this.pnlDeckPreview.Name = "pnlDeckPreview";
            this.pnlDeckPreview.Size = new System.Drawing.Size(30, 25);
            this.pnlDeckPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDeckPreview.BackColor = System.Drawing.Color.DarkBlue;

            
            this.lblAircraftCount.AutoSize = true;
            this.lblAircraftCount.Location = new System.Drawing.Point(20, 220);
            this.lblAircraftCount.Name = "lblAircraftCount";
            this.lblAircraftCount.Text = "Количество самолётов:";

            
            this.numAircraftCount.Location = new System.Drawing.Point(200, 218);
            this.numAircraftCount.Name = "numAircraftCount";
            this.numAircraftCount.Size = new System.Drawing.Size(120, 27);
            this.numAircraftCount.Minimum = 0;
            this.numAircraftCount.Maximum = 100;
            this.numAircraftCount.Value = 30;

            
            this.chkCatapult.AutoSize = true;
            this.chkCatapult.Location = new System.Drawing.Point(20, 258);
            this.chkCatapult.Name = "chkCatapult";
            this.chkCatapult.Text = "Есть катапульта";

           
            this.btnOK.Location = new System.Drawing.Point(150, 300);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 32);
            this.btnOK.Text = "Создать";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            
            this.btnCancel.Location = new System.Drawing.Point(260, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.numSpeed);
            this.Controls.Add(this.lblDisplacement);
            this.Controls.Add(this.numDisplacement);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.btnPickColor);
            this.Controls.Add(this.pnlColorPreview);
            this.Controls.Add(this.lblDeckColor);
            this.Controls.Add(this.btnPickDeckColor);
            this.Controls.Add(this.pnlDeckPreview);
            this.Controls.Add(this.lblAircraftCount);
            this.Controls.Add(this.numAircraftCount);
            this.Controls.Add(this.chkCatapult);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "ShipCreationForm";
            this.Text = "Создание корабля";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.Label lblDisplacement;
        private System.Windows.Forms.NumericUpDown numDisplacement;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnPickColor;
        private System.Windows.Forms.Panel pnlColorPreview;
        private System.Windows.Forms.Label lblDeckColor;
        private System.Windows.Forms.Button btnPickDeckColor;
        private System.Windows.Forms.Panel pnlDeckPreview;
        private System.Windows.Forms.Label lblAircraftCount;
        private System.Windows.Forms.NumericUpDown numAircraftCount;
        private System.Windows.Forms.CheckBox chkCatapult;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}