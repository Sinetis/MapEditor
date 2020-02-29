using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using MapEditor.MapInt;
using NoxShared;
using System.Collections.Generic;
using NoxShared.ObjDataXfer;

namespace MapEditor
{
	public class ObjectListDialog : Form
    {
        private IContainer components;
        private string lastSort;
        private bool showWarning;
		protected DataTable objList;
        private DataGridView dataGrid1;
		public Map.ObjectTable objTable
		{
			set
			{
				objList = new DataTable("objList");
				objList.Columns.Add("Extent",Type.GetType("System.UInt32"));
				objList.Columns.Add("X-Coor.",Type.GetType("System.Single"));
				objList.Columns.Add("Y-Coor.",Type.GetType("System.Single"));
                objList.Columns.Add("Name", Type.GetType("System.Object"));
				objList.Columns.Add("Scr. Name", Type.GetType("System.String"));
                objList.Columns.Add("Enchant1", Type.GetType("System.String"));
                objList.Columns.Add("Enchant2", Type.GetType("System.String"));
                objList.Columns.Add("Enchant3", Type.GetType("System.String"));
                objList.Columns.Add("Enchant4", Type.GetType("System.String"));

                foreach (Map.Object obj in value)
                {
                    var enchants = GetEnchants(obj);
                    if (enchants == null)
                        objList.Rows.Add(new object[] { obj.Extent, obj.Location.X, obj.Location.Y, obj, obj.Scr_Name, "", "", "", "" });
                    else
                        objList.Rows.Add(new object[] { obj.Extent, obj.Location.X, obj.Location.Y, obj, obj.Scr_Name, enchants[0], enchants[1], enchants[2], enchants[3] });

                    dataGrid1.DataSource = objList;
                }

                for (int i = 0; i < dataGrid1.Columns.Count; i++)
                    dataGrid1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (dataGrid1.Columns.Count > 0)
                {
                    for (int i = 0; i < 4; i++)
                        dataGrid1.Columns[i].ReadOnly = true;
                }

                // Sort by Name asc
                dataGrid1_ColumnHeaderMouseClick(this, new DataGridViewCellMouseEventArgs(3, 0, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));

                lblSearching.Text = dataGrid1.Rows.Count + " objects";
            }
		}
        public Map.ObjectTable objTable2;
        private Timer Helpmark;
        private MenuStrip menuMain;
        private ToolStripMenuItem menuGoToObj;
        private ToolStripMenuItem menuEditObj;
        private ToolStripMenuItem menuDeleteObj;
        private ToolStripMenuItem menuApplyChanges;
        private ToolStripTextBox txtObjFilter;
        private ToolStripMenuItem lblSearching;
        public MapView Map;
        public Map.ObjectTable Result { get; set; }

        public ObjectListDialog()
		{
			InitializeComponent();
            dataGrid1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(dataGrid1_ColumnHeaderMouseClick);
            lastSort = "";
            showWarning = true;
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Map.CenterAtPoint(new Point((int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["X-Coor."]), (int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["Y-Coor."])));
        }
        private void dataGrid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGrid1.Rows.Count == 0)
                return;
            var curCol = dataGrid1.Columns[e.ColumnIndex].Name;

            if (lastSort == "")
                objList.DefaultView.Sort = curCol + " asc";
            else
            {
                if (lastSort.StartsWith(curCol))
                { 
                    if (lastSort.EndsWith("asc"))
                        objList.DefaultView.Sort = curCol + " desc";
                    else
                        objList.DefaultView.Sort = curCol + " asc";
                }
                else
                    objList.DefaultView.Sort = curCol + " asc";
            }

            lastSort = objList.DefaultView.Sort;
            Text = "Object List  (" + lastSort + ")";
            objList = objList.DefaultView.ToTable();
            dataGrid1.DataSource = objList;
            UpdateFilter();
        }

        private void Helpmark_Tick(object sender, EventArgs e)
        {
            Map.higlightRad -= 30;

            if (Map.higlightRad > 40) return;
            Map.highlightUndoRedo = new Point();
            Map.higlightRad = 150;
            Helpmark.Enabled = false;
        }

        private Map.Object GetNewObject(string name)
        {
            // Ensure correct object is found based on sorting method
            for (int k = 0; k < objList.Rows.Count; k++)
                if (objList.Rows[k][3].ToString() == name)
                    return (Map.Object)objList.Rows[k][3];

            return null;
        }
        private string[] GetEnchants(Map.Object obj)
        {
            ThingDb.Thing tt = ThingDb.Things[obj.Name];
            WeaponXfer weapon; ArmorXfer armor; AmmoXfer ammo; TeamXfer team;
            string[] enchants = new string[4];
            switch (tt.Xfer)
            {
                case "WeaponXfer":
                    weapon = obj.GetExtraData<WeaponXfer>();
                    enchants[0] = weapon.Enchantments[0];
                    enchants[1] = weapon.Enchantments[1];
                    enchants[2] = weapon.Enchantments[2];
                    enchants[3] = weapon.Enchantments[3];
                    break;
                case "ArmorXfer":
                    armor = obj.GetExtraData<ArmorXfer>();
                    enchants[0] = armor.Enchantments[0];
                    enchants[1] = armor.Enchantments[1];
                    enchants[2] = armor.Enchantments[2];
                    enchants[3] = armor.Enchantments[3];
                    break;
                case "AmmoXfer":
                    ammo = obj.GetExtraData<AmmoXfer>();
                    enchants[0] = ammo.Enchantments[0];
                    enchants[1] = ammo.Enchantments[1];
                    enchants[2] = ammo.Enchantments[2];
                    enchants[3] = ammo.Enchantments[3];
                    break;
                case "TeamXfer":
                    team = obj.GetExtraData<TeamXfer>();
                    enchants[0] = team.Enchantments[0];
                    enchants[1] = team.Enchantments[1];
                    enchants[2] = team.Enchantments[2];
                    enchants[3] = team.Enchantments[3];
                    break;
                default:
                    enchants = null;
                    break;
            }
            return enchants;
        }
        private Map.Object SetEnchants(Map.Object obj, string[] enchants)
        {
            ThingDb.Thing tt = ThingDb.Things[obj.Name];
            WeaponXfer weapon; ArmorXfer armor; AmmoXfer ammo; TeamXfer team;

            switch (tt.Xfer)
            {
                case "WeaponXfer":
                    weapon = obj.GetExtraData<WeaponXfer>();
                    weapon.Enchantments[0] = enchants[0];
                    weapon.Enchantments[1] = enchants[1];
                    weapon.Enchantments[2] = enchants[2];
                    weapon.Enchantments[3] = enchants[3];
                    break;
                case "ArmorXfer":
                    armor = obj.GetExtraData<ArmorXfer>();
                    armor.Enchantments[0] = enchants[0];
                    armor.Enchantments[1] = enchants[1];
                    armor.Enchantments[2] = enchants[2];
                    armor.Enchantments[3] = enchants[3];
                    break;
                case "AmmoXfer":
                    ammo = obj.GetExtraData<AmmoXfer>();
                    ammo.Enchantments[0] = enchants[0];
                    ammo.Enchantments[1] = enchants[1];
                    ammo.Enchantments[2] = enchants[2];
                    ammo.Enchantments[3] = enchants[3];
                    break;
                case "TeamXfer":
                    team = obj.GetExtraData<TeamXfer>();
                    team.Enchantments[0] = enchants[0];
                    team.Enchantments[1] = enchants[1];
                    team.Enchantments[2] = enchants[2];
                    team.Enchantments[3] = enchants[3];
                    break;
            }
            return obj;
        }
        private string GetValidEnchant(string enchant)
        {
            foreach (var validEnch in XferGui.EquipmentEdit.ENCHANTMENTS)
            {
                if (validEnch.ToUpper() == enchant.ToUpper())
                    return validEnch;
            }
            return "-1";
        }

        private void txtObjFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (e.Control)
                    txtObjFilter.Clear();

                UpdateFilter();

                if (e.Control && dataGrid1.Rows.Count > 0)
                    dataGrid1.CurrentCell = dataGrid1[0, 0];
            }
        }
        private void UpdateFilter()
        {
            if (txtObjFilter.Text == "Search Name...")
                return;
            if (dataGrid1.DataSource == null)
                return;

            lblSearching.Text = "Searching..."; Application.DoEvents();
            CurrencyManager currMgr = (CurrencyManager)BindingContext[dataGrid1.DataSource];
            currMgr.SuspendBinding();
            if (txtObjFilter.Text.Trim().Length < 2)
            {
                for (int i = 0; i < dataGrid1.Rows.Count; i++)
                    dataGrid1.Rows[i].Visible = true;
            }
            else
            {
                for (int i = 0; i < dataGrid1.Rows.Count; i++)
                {
                    if (dataGrid1.Rows[i].Cells["Name"].Value.ToString().ToUpper().Contains(txtObjFilter.Text.ToUpper()))
                        dataGrid1.Rows[i].Visible = true;
                    else
                        dataGrid1.Rows[i].Visible = false;
                }
            }
            currMgr.ResumeBinding();
            lblSearching.Text = GetNumRowsVisible() + " objects";
        }
        private void txtObjFilter_Enter(object sender, EventArgs e)
        {
            if (txtObjFilter.Text == "Search Name...")
            {
                txtObjFilter.Clear();
                txtObjFilter.ForeColor = Color.Black;
            }
        }
        private void txtObjFilter_Leave(object sender, EventArgs e)
        {
            if (txtObjFilter.Text.Trim() == "")
            {
                txtObjFilter.ForeColor = Color.Gray;
                txtObjFilter.Text = "Search Name...";
            }
        }

        private void menuGoToObj_Click(object sender, EventArgs e)
        {
            if (dataGrid1.CurrentRow == null)
                return;

            Point target = new Point((int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["X-Coor."]), (int)((float)objList.Rows[dataGrid1.CurrentRow.Index]["Y-Coor."]));
            Map.CenterAtPoint(target);
            Helpmark.Enabled = true;
            Map.highlightUndoRedo = target;
            Map.Object P = (Map.Object)(objList.Rows[dataGrid1.CurrentRow.Index][3]);
            Map.SelectedObjects.Items.Clear();
            Map.SelectedObjects.Items.Add(P);
        }
        private void menuEditObj_Click(object sender, EventArgs e)
        {
            if (dataGrid1.CurrentRow == null)
                return;

            int curIndex = dataGrid1.CurrentRow.Index;
            Map.Object obj = (Map.Object)objList.Rows[dataGrid1.CurrentRow.Index][3];
            Map.ShowObjectProperties(obj);

            // Only update current row
            var enchants = GetEnchants(obj);
            if (enchants == null)
                objList.Rows[dataGrid1.CurrentRow.Index].ItemArray = new object[] { obj.Extent, obj.Location.X, obj.Location.Y, obj, obj.Scr_Name, "", "", "", "" };
            else
                objList.Rows[dataGrid1.CurrentRow.Index].ItemArray = new object[] { obj.Extent, obj.Location.X, obj.Location.Y, obj, obj.Scr_Name, enchants[0], enchants[1], enchants[2], enchants[3] };
        }
        private void menuDeleteObj_Click(object sender, EventArgs e)
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
            var obj = GetNewObject(name);
            MapInterface.ObjectRemove(obj);

            dataGrid1.Rows.RemoveAt(i);
            UpdateFilter();
            if (dataGrid1.Rows.Count > i)
            {
                if (dataGrid1.Rows[i].Visible)
                    dataGrid1.CurrentCell = dataGrid1.Rows[i].Cells[3];
                else
                {
                    if (GetNumRowsVisible() > 0)
                        dataGrid1.CurrentCell = dataGrid1.Rows[dataGrid1.Rows.GetLastRow(DataGridViewElementStates.Visible)].Cells[3];
                }
            }

            MainWindow.Instance.mapView.MapRenderer.UpdateCanvas(true, true);
        }
        private void menuApplyChanges_Click(object sender, EventArgs e)
        {
            dataGrid1.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange);

            // Find and return changes
            int c = 0;
            for (int i = 0; i < dataGrid1.Rows.Count; i++)
            {
                var name = dataGrid1[3, i].Value.ToString();
                var newScrName = dataGrid1[4, i].Value.ToString();

                Map.Object newObj = GetNewObject(name);
                Map.Object oldObj = null; int j = 0;

                for (int k = 0; k < objTable2.Count; k++)
                {
                    if (((Map.Object)objTable2[k]).ToString() == newObj.ToString())
                    {
                        oldObj = (Map.Object)objTable2[k];
                        j = k;
                        break;
                    }
                }
                if (oldObj == null)
                    continue;

                // Set new script name
                if (oldObj.Scr_Name != newScrName)
                {
                    ((Map.Object)objTable2[j]).Scr_Name = newScrName;
                    // Check 'Extra Bytes'
                    if (oldObj.Terminator == 0x00)
                        ((Map.Object)objTable2[j]).Terminator = 0xFF;
                    c++;
                }

                var enchants = GetEnchants(oldObj);
                if (enchants == null)
                    continue;

                string[] ench = new string[4];
                ench[0] = dataGrid1[5, i].Value.ToString();
                ench[1] = dataGrid1[6, i].Value.ToString();
                ench[2] = dataGrid1[7, i].Value.ToString();
                ench[3] = dataGrid1[8, i].Value.ToString();

                for (int k = 0; k < 4; k++)
                {
                    // Ignore all whitespace
                    if (ench[k] == "")
                        continue;
                    if (ench[k].Trim() == "")
                    {
                        ench[k] = "";
                        continue;
                    }

                    // Ensure valid enchant and proper case
                    var isValid = GetValidEnchant(ench[k]);
                    if (isValid != "-1")
                        ench[k] = isValid;
                    else
                        ench[k] = "";
                }

                // Set new enchants
                if (enchants != ench)
                    objTable2[j] = SetEnchants(oldObj, ench);
            }

            // Pass new object table back to MainWindow
            Result = null;
            if (c > 0)
                Result = objTable2;

            DialogResult = DialogResult.OK;
            Hide();
        }

        private int GetNumRowsVisible()
        {
            int c = 0;
            for (int i = 0; i < dataGrid1.Rows.Count; i++)
                if (dataGrid1.Rows[i].Visible)
                    c++;

            return c;
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectListDialog));
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.Helpmark = new System.Windows.Forms.Timer(this.components);
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuGoToObj = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditObj = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteObj = new System.Windows.Forms.ToolStripMenuItem();
            this.menuApplyChanges = new System.Windows.Forms.ToolStripMenuItem();
            this.txtObjFilter = new System.Windows.Forms.ToolStripTextBox();
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
            // 
            // Helpmark
            // 
            this.Helpmark.Interval = 120;
            this.Helpmark.Tick += new System.EventHandler(this.Helpmark_Tick);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGoToObj,
            this.menuEditObj,
            this.menuDeleteObj,
            this.menuApplyChanges,
            this.txtObjFilter,
            this.lblSearching});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // menuGoToObj
            // 
            this.menuGoToObj.Name = "menuGoToObj";
            resources.ApplyResources(this.menuGoToObj, "menuGoToObj");
            this.menuGoToObj.Click += new System.EventHandler(this.menuGoToObj_Click);
            // 
            // menuEditObj
            // 
            this.menuEditObj.Name = "menuEditObj";
            resources.ApplyResources(this.menuEditObj, "menuEditObj");
            this.menuEditObj.Click += new System.EventHandler(this.menuEditObj_Click);
            // 
            // menuDeleteObj
            // 
            this.menuDeleteObj.Name = "menuDeleteObj";
            resources.ApplyResources(this.menuDeleteObj, "menuDeleteObj");
            this.menuDeleteObj.Click += new System.EventHandler(this.menuDeleteObj_Click);
            // 
            // menuApplyChanges
            // 
            this.menuApplyChanges.Name = "menuApplyChanges";
            resources.ApplyResources(this.menuApplyChanges, "menuApplyChanges");
            this.menuApplyChanges.Click += new System.EventHandler(this.menuApplyChanges_Click);
            // 
            // txtObjFilter
            // 
            this.txtObjFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObjFilter.ForeColor = System.Drawing.Color.Gray;
            this.txtObjFilter.Name = "txtObjFilter";
            resources.ApplyResources(this.txtObjFilter, "txtObjFilter");
            this.txtObjFilter.Enter += new System.EventHandler(this.txtObjFilter_Enter);
            this.txtObjFilter.Leave += new System.EventHandler(this.txtObjFilter_Leave);
            this.txtObjFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObjFilter_KeyDown);
            // 
            // lblSearching
            // 
            resources.ApplyResources(this.lblSearching, "lblSearching");
            this.lblSearching.Name = "lblSearching";
            // 
            // ObjectListDialog
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.menuMain);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "ObjectListDialog";
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
