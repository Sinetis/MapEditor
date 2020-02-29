using NoxShared;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor
{
    public class WaypointProperties : Form
    {
        private bool BlockEvent = false;
        private bool BlockEvent2 = false;
        private Map.WaypointList wpList;
        private Map.Waypoint wp;

        public Map.WaypointList wplist
        {
            get
            {
                return wpList;
            }
            set
            {
                wpList = value;
                wpBox.Items.Clear();
                foreach (Map.Waypoint wp in wpList)
                    wpBox.Items.Add(wp);
            }
        }
        public Map.Waypoint wpPub
        {
            get
            {
                return wp;
            }
            set
            {
                wp = value;
                nameText.Text = wp.Name == null ? "" : wp.Name;
                enabledCheck.Checked = wp.Flags == 1;
                Num.Text = wp.Number.ToString();
                posX.Text = wp.Point.X.ToString();
                posY.Text = wp.Point.Y.ToString();
                connList.Items.Clear();
                foreach (Map.Waypoint.WaypointConnection connection in wp.connections)
                    connList.Items.Add(new Map.Waypoint.WaypointConnection(connection.wp, connection.flag));
            }
        }

        public WaypointProperties()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            connList.Items.Add(new Map.Waypoint.WaypointConnection((Map.Waypoint)wpBox.SelectedItem, byte.Parse(flagText.Text)));
            wpBox.Text = null;
        }

        private void WaypointProperties_Load(object sender, EventArgs e)
        {
            wpBox.SelectedItem = wpPub;
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (connList.SelectedItem == null)
                return;
            int i = connList.SelectedIndex;
            connList.Items.Remove(connList.SelectedItem);
            if (connList.Items.Count == 0)
                return;
            if (i >= connList.Items.Count)
                i = connList.Items.Count - 1;
            connList.SelectedIndex = i;
        }

        private void flagText_TextChanged(object sender, EventArgs e)
        {
            if (connList.SelectedItem == null)
                return;
            if (BlockEvent)
            {
                BlockEvent = false;
            }
            else
            {
                BlockEvent2 = true;
                byte result;
                if (!byte.TryParse(flagText.Text, out result))
                {
                    int num = (int)MessageBox.Show("Invalid Format. Flag must be a number between 0 - 255", "Error");
                }
                else
                {
                    Map.Waypoint.WaypointConnection selectedItem = (Map.Waypoint.WaypointConnection)connList.SelectedItem;
                    selectedItem.flag = result;
                    int selectedIndex = connList.SelectedIndex;
                    connList.Items.RemoveAt(selectedIndex);
                    BlockEvent2 = true;
                    connList.Items.Insert(selectedIndex, selectedItem);
                    BlockEvent2 = true;
                    connList.SelectedIndex = selectedIndex;
                }
            }
        }

        private void connList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BlockEvent2)
                BlockEvent2 = false;
            else if (connList.SelectedItem != null)
            {
                flagText.Enabled = true;
                BlockEvent = true;
                flagText.Text = ((Map.Waypoint.WaypointConnection)connList.SelectedItem).flag.ToString();
            }
            else
                flagText.Enabled = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int result1;
            if (!int.TryParse(posX.Text, out result1))
            {
                int num1 = (int)MessageBox.Show("Invalid X.", "Error");
            }
            else
            {
                int result2;
                if (!int.TryParse(posY.Text, out result2))
                {
                    int num2 = (int)MessageBox.Show("Invalid Y.", "Error");
                }
                else
                {
                    wp.Name = nameText.Text;
                    wp.Flags = enabledCheck.Checked ? 1 : 0;
                    wp.connections.Clear();
                    wp.connections.AddRange(connList.Items);
                    wp.Point = new Point(result1, result2);
                    MainWindow.Instance.mapView.waypointName.Text = wp.Name;
                    MainWindow.Instance.mapView.waypointEnabled.Checked = wp.Flags > 0;
                    Close();
                }
            }
        }

        #region Windows Designer Components
        private Button delButton;
        private Label label3;
        private TextBox flagText;
        private CheckBox enabledCheck;
        private Button addButton;
        private Button cancelButton;
        private Button okButton;
        private TextBox nameText;
        private ListBox connList;
        private ComboBox wpBox;
        private Label label1;
        private Label label2;
        private Label label4;
        private TextBox Num;
        private TextBox posX;
        private TextBox posY;
        private Label label6;
        private GroupBox groupBox1;
        private Label label5;

        private void InitializeComponent()
        {
            this.delButton = new Button();
            this.label3 = new Label();
            this.flagText = new TextBox();
            this.enabledCheck = new CheckBox();
            this.addButton = new Button();
            this.cancelButton = new Button();
            this.okButton = new Button();
            this.nameText = new TextBox();
            this.connList = new ListBox();
            this.wpBox = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.Num = new TextBox();
            this.posX = new TextBox();
            this.posY = new TextBox();
            this.label6 = new Label();
            this.groupBox1 = new GroupBox();
            this.label5 = new Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            this.delButton.Location = new Point(97, 150);
            this.delButton.Name = "delButton";
            this.delButton.Size = new Size(31, 23);
            this.delButton.TabIndex = 21;
            this.delButton.Text = "Del";
            this.delButton.Click += new EventHandler(this.delButton_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(24, 164);
            this.label3.Name = "label3";
            this.label3.Size = new Size(87, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Connection Flag:";
            this.flagText.Enabled = false;
            this.flagText.Location = new Point(117, 162);
            this.flagText.MaxLength = 3;
            this.flagText.Name = "flagText";
            this.flagText.Size = new Size(36, 20);
            this.flagText.TabIndex = 19;
            this.flagText.TextChanged += new EventHandler(this.flagText_TextChanged);
            this.enabledCheck.AutoSize = true;
            this.enabledCheck.Location = new Point(8, 136);
            this.enabledCheck.Name = "enabledCheck";
            this.enabledCheck.Size = new Size(65, 17);
            this.enabledCheck.TabIndex = 18;
            this.enabledCheck.Text = "Enabled";
            this.addButton.Location = new Point(88, 111);
            this.addButton.Name = "addButton";
            this.addButton.Size = new Size(34, 23);
            this.addButton.TabIndex = 17;
            this.addButton.Text = "Add";
            this.addButton.Visible = false;
            this.addButton.Click += new EventHandler(this.addButton_Click);
            this.cancelButton.DialogResult = DialogResult.Cancel;
            this.cancelButton.Location = new Point(69, 179);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new Size(59, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.okButton.DialogResult = DialogResult.OK;
            this.okButton.Location = new Point(7, 179);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(59, 23);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "OK";
            this.okButton.Click += new EventHandler(this.okButton_Click);
            this.nameText.Location = new Point(10, 23);
            this.nameText.Name = "nameText";
            this.nameText.Size = new Size(112, 20);
            this.nameText.TabIndex = 14;
            this.connList.FormattingEnabled = true;
            this.connList.HorizontalScrollbar = true;
            this.connList.Location = new Point(9, 18);
            this.connList.Name = "connList";
            this.connList.Size = new Size(144, 134);
            this.connList.TabIndex = 13;
            this.connList.SelectedIndexChanged += new EventHandler(this.connList_SelectedIndexChanged);
            this.wpBox.FormattingEnabled = true;
            this.wpBox.Location = new Point(27, 113);
            this.wpBox.Name = "wpBox";
            this.wpBox.Size = new Size(92, 21);
            this.wpBox.TabIndex = 12;
            this.wpBox.Visible = false;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new Size(38, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Name:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new Size(47, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Number:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(7, 88);
            this.label4.Name = "label4";
            this.label4.Size = new Size(47, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Position:";
            this.Num.Location = new Point(27, 63);
            this.Num.Name = "Num";
            this.Num.ReadOnly = true;
            this.Num.Size = new Size(33, 20);
            this.Num.TabIndex = 25;
            this.posX.Location = new Point(27, 105);
            this.posX.Name = "posX";
            this.posX.Size = new Size(33, 20);
            this.posX.TabIndex = 26;
            this.posY.Location = new Point(86, 105);
            this.posY.Name = "posY";
            this.posY.Size = new Size(33, 20);
            this.posY.TabIndex = 27;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(66, 109);
            this.label6.Name = "label6";
            this.label6.Size = new Size(17, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Y:";
            this.groupBox1.Controls.Add((Control)this.connList);
            this.groupBox1.Controls.Add((Control)this.flagText);
            this.groupBox1.Controls.Add((Control)this.label3);
            this.groupBox1.Controls.Add((Control)this.addButton);
            this.groupBox1.Controls.Add((Control)this.wpBox);
            this.groupBox1.Location = new Point(133, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(161, 190);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connections";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(7, 109);
            this.label5.Name = "label5";
            this.label5.Size = new Size(17, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "X:";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(301, 214);
            this.Controls.Add((Control)this.label5);
            this.Controls.Add((Control)this.groupBox1);
            this.Controls.Add((Control)this.posY);
            this.Controls.Add((Control)this.label6);
            this.Controls.Add((Control)this.posX);
            this.Controls.Add((Control)this.delButton);
            this.Controls.Add((Control)this.Num);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.enabledCheck);
            this.Controls.Add((Control)this.cancelButton);
            this.Controls.Add((Control)this.okButton);
            this.Controls.Add((Control)this.nameText);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Name = "WaypointProperties";
            this.Text = "Waypoint Properties";
            this.Load += new EventHandler(this.WaypointProperties_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

#endregion
    }
}
