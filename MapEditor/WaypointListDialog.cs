using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.MapInt;
using NoxShared;
using System.Collections.Generic;

namespace MapEditor
{
	public class WaypointListDialog : Form
    {
        private IContainer components;
        private bool showWarning;
        private bool stun = false;
        private bool backed = false;
        private int id;
        protected DataTable wpList;

        private DataGridView dataGrid1;
        private DataGridViewColumn setting;
        private Timer Helpmark;
        private MenuStrip menuMain;
        private ToolStripMenuItem menuGoToWaypoint;
        private ToolStripMenuItem menuEditWaypoint;
        private ToolStripMenuItem menuDeleteWaypoint;
        private ToolStripTextBox txtFilter;
        private ToolStripMenuItem lblSearching;
        public MapView Map;

        public Map.WaypointList WpsTable
        {
            set
            {
                wpList = new DataTable("wpList");
                wpList.Columns.Add("ID", Type.GetType("System.Int32"));
                wpList.Columns.Add("X-Coor.", Type.GetType("System.Single"));
                wpList.Columns.Add("Y-Coor.", Type.GetType("System.Single"));
                wpList.Columns.Add("Name", Type.GetType("System.String"));
                wpList.Columns.Add("", Type.GetType("System.Object"));
                int num = 0;
                foreach (Map.Waypoint waypoint in value)
                {
                    wpList.Rows.Add(waypoint.Number, waypoint.Point.X, waypoint.Point.Y, waypoint.Name, waypoint);
                    ++num;
                }
                dataGrid1.DataSource = wpList;
                dataGrid1.Columns[4].Visible = false;
                if (dataGrid1.Rows.Count <= 0)
                    return;
                dataGrid1.CurrentCell = dataGrid1.Rows[0].Cells[3];
                lblSearching.Text = dataGrid1.Rows.Count + " waypoints";
            }
        }


        public WaypointListDialog()
		{
			InitializeComponent();
            dataGrid1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dataGrid1_ColumnHeaderMouseClick);
            showWarning = true;
        }

        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid1.Rows.Count < 1)
                return;
            id = (int)dataGrid1.CurrentRow.Cells[0].Value;
            string name = dataGrid1.Columns[dataGrid1.CurrentCell.ColumnIndex].Name;
            stun = true;
            txtFilter.Text = name + " search...";
        }
        private void dataGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Map.CenterAtPoint(new Point((int)((float)wpList.Rows[dataGrid1.CurrentRow.Index]["X-Coor."]), (int)((float)wpList.Rows[dataGrid1.CurrentRow.Index]["Y-Coor."])));
        }
        private void dataGrid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DatatableSync();
            int index = 0;
            foreach (DataGridViewRow row in dataGrid1.Rows)
            {
                if ((int)row.Cells[0].Value == id)
                {
                    dataGrid1.Rows[index].Selected = true;
                    dataGrid1.CurrentCell = dataGrid1.Rows[index].Cells[e.ColumnIndex];
                    break;
                }
                ++index;
            }
            if (dataGrid1.Rows.Count < 1)
                return;
            string name = dataGrid1.Columns[dataGrid1.CurrentCell.ColumnIndex].Name;
            stun = true;
            txtFilter.Text = name + " search...";
        }
        private void DatatableSync()
        {
            if (dataGrid1.SortedColumn == null || dataGrid1.SortedColumn.Name.Length <= 0)
                return;
            wpList.DefaultView.Sort = dataGrid1.SortOrder != SortOrder.Descending ? dataGrid1.SortedColumn.Name + " ASC" : dataGrid1.SortedColumn.Name + " DESC";
            wpList = wpList.DefaultView.ToTable();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (dataGrid1.Rows.Count < 1)
                return;
            if (stun)
            {
                stun = false;
            }
            else
            {
                string upper = txtFilter.Text.ToUpper();
                dataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                string str = "";
                bool flag = false;
                string name = dataGrid1.Columns[dataGrid1.CurrentCell.ColumnIndex].Name;
                int index = 0;
                foreach (DataGridViewRow row in dataGrid1.Rows)
                {
                    str = row.Cells[name].Value.ToString();
                    if (str.ToUpper().StartsWith(upper))
                    {
                        index = row.Index;
                        dataGrid1.Rows[index].Selected = true;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    txtFilter.BackColor = Color.Red;
                }
                else
                {
                    if (!backed)
                    {
                        stun = true;
                        txtFilter.Text = str;
                        dataGrid1.CurrentCell = dataGrid1.Rows[index].Cells[name];
                    }
                    txtFilter.SelectionStart = upper.Length;
                    txtFilter.SelectionLength = str.Length - upper.Length;
                    txtFilter.BackColor = Color.White;
                    backed = false;
                }
            }
        }
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z || e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                stun = false;
            else if (e.KeyCode == Keys.Back)
                backed = true;
            else
                stun = true;
        }
        private void txtFilter_Enter(object sender, EventArgs e)
        {
            stun = true;
            if (!txtFilter.Text.EndsWith("search..."))
                return;
            txtFilter.Text = "";
        }

        private void menuGoToWaypoint_Click(object sender, EventArgs e)
        {
            if (dataGrid1.Rows.Count < 1)
                return;
            Map.CenterAtPoint(new Point((int)(float)wpList.Rows[dataGrid1.CurrentRow.Index]["X-Coor."], (int)(float)wpList.Rows[dataGrid1.CurrentRow.Index]["Y-Coor."]));
            MapInterface.SelectedWaypoint = (Map.Waypoint)wpList.Rows[dataGrid1.CurrentRow.Index][4];
        }
        private void menuEditWaypoint_Click(object sender, EventArgs e)
        {
            if ((dataGrid1.Rows.Count < 1) || (dataGrid1.CurrentRow == null))
                return;
            int index = dataGrid1.CurrentRow.Index;
            
        }
        private void menuDeleteWaypoint_Click(object sender, EventArgs e)
        {
            if (dataGrid1.CurrentRow == null)
                return;

            if (showWarning)
            {
                var dr = MessageBox.Show("This will permanently delete the selected row/object.\n\nAre you sure?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                    return;

                showWarning = false; // User has been warned
            }

            var i = dataGrid1.CurrentRow.Index;
            var name = dataGrid1[3, i].Value.ToString();
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaypointListDialog));
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.Helpmark = new System.Windows.Forms.Timer(this.components);
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuGoToWaypoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditWaypoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteWaypoint = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.lblSearching = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.AllowUserToAddRows = false;
            this.dataGrid1.AllowUserToDeleteRows = false;
            this.dataGrid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGrid1, "dataGrid1");
            this.dataGrid1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid1_CellClick);
            // 
            // Helpmark
            // 
            this.Helpmark.Interval = 120;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGoToWaypoint,
            this.menuEditWaypoint,
            this.menuDeleteWaypoint,
            this.txtFilter,
            this.lblSearching});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // menuGoToWaypoint
            // 
            this.menuGoToWaypoint.Name = "menuGoToWaypoint";
            resources.ApplyResources(this.menuGoToWaypoint, "menuGoToWaypoint");
            this.menuGoToWaypoint.Click += new System.EventHandler(this.menuGoToWaypoint_Click);
            // 
            // menuEditWaypoint
            // 
            this.menuEditWaypoint.Name = "menuEditWaypoint";
            resources.ApplyResources(this.menuEditWaypoint, "menuEditWaypoint");
            this.menuEditWaypoint.Click += new System.EventHandler(this.menuEditWaypoint_Click);
            // 
            // menuDeleteWaypoint
            // 
            this.menuDeleteWaypoint.Name = "menuDeleteWaypoint";
            resources.ApplyResources(this.menuDeleteWaypoint, "menuDeleteWaypoint");
            this.menuDeleteWaypoint.Click += new System.EventHandler(this.menuDeleteWaypoint_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.ForeColor = System.Drawing.Color.Black;
            this.txtFilter.Name = "txtFilter";
            resources.ApplyResources(this.txtFilter, "txtFilter");
            this.txtFilter.Enter += new System.EventHandler(this.txtFilter_Enter);
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblSearching
            // 
            resources.ApplyResources(this.lblSearching, "lblSearching");
            this.lblSearching.Name = "lblSearching";
            // 
            // WaypointListDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.menuMain);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "WaypointListDialog";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
