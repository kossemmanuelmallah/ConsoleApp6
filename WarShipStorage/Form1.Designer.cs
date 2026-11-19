namespace WarShipStorage
{
    partial class Form1
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
            this.btnCreateShip = new System.Windows.Forms.Button();
            this.btnCreateCarrier = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.txtDeleteIndex = new System.Windows.Forms.TextBox();
            this.lstShips = new System.Windows.Forms.ListBox();
            this.pnlField = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();

            
            this.btnCreateShip.Location = new System.Drawing.Point(12, 12);
            this.btnCreateShip.Name = "btnCreateShip";
            this.btnCreateShip.Size = new System.Drawing.Size(130, 30);
            this.btnCreateShip.TabIndex = 0;
            this.btnCreateShip.Text = "Создать корабль";
            this.btnCreateShip.UseVisualStyleBackColor = true;
            this.btnCreateShip.Click += new System.EventHandler(this.btnCreateShip_Click);

            
            this.btnCreateCarrier.Location = new System.Drawing.Point(150, 12);
            this.btnCreateCarrier.Name = "btnCreateCarrier";
            this.btnCreateCarrier.Size = new System.Drawing.Size(140, 30);
            this.btnCreateCarrier.TabIndex = 1;
            this.btnCreateCarrier.Text = "Создать авианосец";
            this.btnCreateCarrier.UseVisualStyleBackColor = true;
            this.btnCreateCarrier.Click += new System.EventHandler(this.btnCreateCarrier_Click);

            
            this.btnDelete.Location = new System.Drawing.Point(298, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

           
            this.btnTransfer.Location = new System.Drawing.Point(396, 12);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(130, 30);
            this.btnTransfer.TabIndex = 3;
            this.btnTransfer.Text = "Передать на тесты";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);

           
            this.btnStep.Location = new System.Drawing.Point(460, 510);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(100, 30);
            this.btnStep.TabIndex = 4;
            this.btnStep.Text = "Шаг";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);


            this.btnRotate.Location = new System.Drawing.Point(760, 510);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(100, 30);
            this.btnRotate.TabIndex = 5;
            this.btnRotate.Text = "Повернуть";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);

            
            this.txtDeleteIndex.Location = new System.Drawing.Point(12, 50);
            this.txtDeleteIndex.Name = "txtDeleteIndex";
            this.txtDeleteIndex.Size = new System.Drawing.Size(376, 27);
            this.txtDeleteIndex.TabIndex = 6;
            this.txtDeleteIndex.PlaceholderText = "Индекс для удаления";

            
            this.lstShips.ItemHeight = 20;
            this.lstShips.Location = new System.Drawing.Point(12, 85);
            this.lstShips.Name = "lstShips";
            this.lstShips.Size = new System.Drawing.Size(376, 404);
            this.lstShips.TabIndex = 7;

            
            this.pnlField.BackColor = System.Drawing.Color.White;
            this.pnlField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlField.Location = new System.Drawing.Point(396, 85);
            this.pnlField.Name = "pnlField";
            this.pnlField.Size = new System.Drawing.Size(560, 415);
            this.pnlField.TabIndex = 8;
            this.pnlField.TabStop = true;

            
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 520);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 20);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Готово";

           
            this.cmbStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStrategy.Location = new System.Drawing.Point(570, 510);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(180, 28);
            this.cmbStrategy.TabIndex = 10;
            this.cmbStrategy.Items.AddRange(new object[] { "К центру", "В правый нижний угол" });
            this.cmbStrategy.SelectedIndex = 0;
            this.cmbStrategy.SelectedIndexChanged += new System.EventHandler(this.cmbStrategy_SelectedIndexChanged);

            
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 560);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStrategy);
            this.Controls.Add(this.pnlField);
            this.Controls.Add(this.lstShips);
            this.Controls.Add(this.txtDeleteIndex);
            this.Controls.Add(this.btnRotate);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCreateCarrier);
            this.Controls.Add(this.btnCreateShip);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление эскадрой - Хранилище военных кораблей";

           
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnCreateShip;
        private System.Windows.Forms.Button btnCreateCarrier;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnRotate;
        private System.Windows.Forms.TextBox txtDeleteIndex;
        private System.Windows.Forms.ListBox lstShips;
        private System.Windows.Forms.Panel pnlField;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStrategy;
    }
}