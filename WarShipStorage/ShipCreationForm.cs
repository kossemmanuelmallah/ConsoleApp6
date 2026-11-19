using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarShipStorage
{
    public partial class ShipCreationForm : Form
    {
        public EntityWarShip CreatedShip { get; private set; }
        private Color _mainColor = Color.Gray;
        private Color _deckColor = Color.DarkBlue;

        public ShipCreationForm()
        {
            InitializeComponent();
            cmbType.SelectedIndex = 0;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCarrier = cmbType.SelectedIndex == 1;
            lblDeckColor.Visible = isCarrier;
            btnPickDeckColor.Visible = isCarrier;
            pnlDeckPreview.Visible = isCarrier;
            lblAircraftCount.Visible = isCarrier;
            numAircraftCount.Visible = isCarrier;
            chkCatapult.Visible = isCarrier;
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            using var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _mainColor = dlg.Color;
                pnlColorPreview.BackColor = _mainColor;
            }
        }

        private void btnPickDeckColor_Click(object sender, EventArgs e)
        {
            using var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _deckColor = dlg.Color;
                pnlDeckPreview.BackColor = _deckColor;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex == 0)
            {
                CreatedShip = new EntityWarShip
                {
                    Speed = (int)numSpeed.Value,
                    Displacement = (double)numDisplacement.Value,
                    MainColor = _mainColor
                };
            }
            else
            {
                CreatedShip = new EntityAircraftCarrier
                {
                    Speed = (int)numSpeed.Value,
                    Displacement = (double)numDisplacement.Value,
                    MainColor = _mainColor,
                    DeckColor = _deckColor,
                    AircraftCount = (int)numAircraftCount.Value,
                    HasCatapult = chkCatapult.Checked
                };
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}