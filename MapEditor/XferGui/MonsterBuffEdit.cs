using NoxShared;
using NoxShared.ObjDataXfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static NoxShared.ObjDataXfer.MonsterXfer;

namespace MapEditor.XferGui
{
    public class MonsterBuffEdit : Form
    {
        #region Windows Designer Components
        private Button cmdCancel;
        private Label lblAvailable;
        private CheckedListBox lstEnchants;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtName;
        private NumericUpDown numSpellLevel;
        private NumericUpDown numDuration;
        private NumericUpDown numShieldHP;
        private Button cmdSave;

        private void InitializeComponent()
        {
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblAvailable = new System.Windows.Forms.Label();
            this.lstEnchants = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.numSpellLevel = new System.Windows.Forms.NumericUpDown();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.numShieldHP = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numSpellLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShieldHP)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSave.Location = new System.Drawing.Point(135, 329);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(102, 33);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.Location = new System.Drawing.Point(239, 329);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(102, 33);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblAvailable
            // 
            this.lblAvailable.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailable.Location = new System.Drawing.Point(12, 3);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(276, 20);
            this.lblAvailable.TabIndex = 4;
            this.lblAvailable.Text = "Enchants";
            this.lblAvailable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstEnchants
            // 
            this.lstEnchants.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEnchants.FormattingEnabled = true;
            this.lstEnchants.Location = new System.Drawing.Point(16, 26);
            this.lstEnchants.MultiColumn = true;
            this.lstEnchants.Name = "lstEnchants";
            this.lstEnchants.Size = new System.Drawing.Size(272, 293);
            this.lstEnchants.TabIndex = 10;
            this.lstEnchants.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstEnchants_ItemCheck);
            this.lstEnchants.SelectedIndexChanged += new System.EventHandler(this.lstEnchants_SelectedIndexChanged);
            this.lstEnchants.DoubleClick += new System.EventHandler(this.lstEnchants_DoubleClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(299, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Spell Level:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(370, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Duration(ticks):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(302, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Shield HP:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Properties";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(299, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(163, 22);
            this.txtName.TabIndex = 18;
            // 
            // numSpellLevel
            // 
            this.numSpellLevel.Enabled = false;
            this.numSpellLevel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSpellLevel.Location = new System.Drawing.Point(299, 74);
            this.numSpellLevel.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSpellLevel.Name = "numSpellLevel";
            this.numSpellLevel.Size = new System.Drawing.Size(65, 23);
            this.numSpellLevel.TabIndex = 19;
            this.numSpellLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numSpellLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSpellLevel.ValueChanged += new System.EventHandler(this.numSpellLevel_ValueChanged);
            // 
            // numDuration
            // 
            this.numDuration.Enabled = false;
            this.numDuration.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDuration.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numDuration.Location = new System.Drawing.Point(370, 74);
            this.numDuration.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(90, 23);
            this.numDuration.TabIndex = 20;
            this.numDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numDuration.ValueChanged += new System.EventHandler(this.numDuration_ValueChanged);
            // 
            // numShieldHP
            // 
            this.numShieldHP.Enabled = false;
            this.numShieldHP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numShieldHP.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numShieldHP.Location = new System.Drawing.Point(299, 122);
            this.numShieldHP.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.numShieldHP.Name = "numShieldHP";
            this.numShieldHP.Size = new System.Drawing.Size(65, 23);
            this.numShieldHP.TabIndex = 21;
            this.numShieldHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numShieldHP.ValueChanged += new System.EventHandler(this.numShieldHP_ValueChanged);
            // 
            // MonsterBuffEdit
            // 
            this.ClientSize = new System.Drawing.Size(474, 374);
            this.Controls.Add(this.numShieldHP);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.numSpellLevel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstEnchants);
            this.Controls.Add(this.lblAvailable);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MonsterBuffEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buff Editor";
            ((System.ComponentModel.ISupportInitialize)(this.numSpellLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShieldHP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public static string[] enchants = new string[] { "ENCHANT_AFRAID", "ENCHANT_ANCHORED", "ENCHANT_ANTI_MAGIC", "ENCHANT_BLINDED", "ENCHANT_BURNING", "ENCHANT_CHARMING", "ENCHANT_CONFUSED", "ENCHANT_CROWN", "ENCHANT_DEATH", "ENCHANT_DETECTING", "ENCHANT_ETHEREAL", "ENCHANT_FREEZE", "ENCHANT_HASTED", "ENCHANT_HELD", "ENCHANT_INFRAVISION", "ENCHANT_INVISIBLE", "ENCHANT_INVULNERABLE", "ENCHANT_LIGHT", "ENCHANT_MOONGLOW", "ENCHANT_PROTECT_FROM_ELECTRICITY", "ENCHANT_PROTECT_FROM_FIRE", "ENCHANT_PROTECT_FROM_MAGIC", "ENCHANT_PROTECT_FROM_POISON", "ENCHANT_REFLECTIVE_SHIELD", "ENCHANT_RUN", "ENCHANT_SHIELD", "ENCHANT_SHOCK", "ENCHANT_SLOWED", "ENCHANT_SNEAK", "ENCHANT_TELEKINESIS", "ENCHANT_VAMPIRISM", "ENCHANT_VILLAIN" };
        private List<BuffEntry> mBuffs;
        private bool enable;

        public MonsterBuffEdit()
        {
            InitializeComponent();
            foreach (var s in enchants)
            {
                var trim = s.Substring(8);
                lstEnchants.Items.Add(trim);
            }
        }

        public void SetBuffs(BuffEntry[] buffs)
        {
            // Store updatable Buff list
            mBuffs = buffs.ToList();

            foreach (var buff in buffs)
            {
                var trim = buff.Name.Substring(8);
                var i = lstEnchants.Items.IndexOf(trim);
                if (i < 0)
                    continue;

                lstEnchants.SetItemChecked(i, true);
            }

            enable = true;
        }
        public BuffEntry[] GetBuffs()
        {
            return mBuffs.ToArray();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstEnchants_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update values and temporarily disable update events from firing
            enable = false;
            var i = lstEnchants.SelectedIndex;
            txtName.Text = lstEnchants.Text;

            var curBuff = new BuffEntry();
            foreach (var be in mBuffs)
                if (be.Name == "ENCHANT_" + lstEnchants.Text)
                    curBuff = be;

            numSpellLevel.Value = curBuff.Power;
            numDuration.Value = curBuff.Duration;
            numShieldHP.Value = curBuff.ShieldHealth;

            UpdateNumerics();
            enable = true;
        }
        private void lstEnchants_DoubleClick(object sender, EventArgs e)
        {
            lstEnchants_SelectedIndexChanged(sender, e);
        }
        private void UpdateNumerics()
        {
            if (lstEnchants.SelectedItem == null)
                return;

            if (lstEnchants.GetItemCheckState(lstEnchants.SelectedIndex) == CheckState.Checked)
            {
                numSpellLevel.Enabled = true;
                numDuration.Enabled = true;
                if (lstEnchants.Text == "SHIELD")
                    numShieldHP.Enabled = true;
                else
                    numShieldHP.Enabled = false;
            }
            else
            {
                numSpellLevel.Enabled = false;
                numDuration.Enabled = false;
                numShieldHP.Enabled = false;
            }
        }

        #region Update Buff List Values
        private void lstEnchants_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!enable)
                return;

            // Add/Remove buff
            var curBuff = "ENCHANT_" + lstEnchants.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                if (curBuff == "ENCHANT_SHIELD")
                    mBuffs.Add(new BuffEntry() { Name = curBuff, Power = 3, Duration = 0, ShieldHealth = 100 });
                else
                    mBuffs.Add(new BuffEntry() { Name = curBuff, Power = 3, Duration = 0 });
            }
            else
            {
                foreach (var b in mBuffs)
                {
                    if (b.Name == curBuff)
                    {
                        mBuffs.Remove(b);
                        break;
                    }
                }
            }
        }

        private void numSpellLevel_ValueChanged(object sender, EventArgs e)
        {
            // Accepts 0-5  (4 and 5 never used)
            if (!enable)
                return;

            var buff = GetCurrentBuff();
            mBuffs.Remove(buff);
            int result = (int)numSpellLevel.Value;
            buff.Power = (byte)result;
            mBuffs.Add(buff);
        }
        private void numDuration_ValueChanged(object sender, EventArgs e)
        {
            // Duration in ticks (30 ticks per 1 sec)
            if (!enable)
                return;

            var buff = GetCurrentBuff();
            mBuffs.Remove(buff);
            buff.Duration = (int)numDuration.Value;
            mBuffs.Add(buff);
        }
        private void numShieldHP_ValueChanged(object sender, EventArgs e)
        {
            // Only available on ENCHANT_SHIELD
            if (!enable)
                return;

            var buff = GetCurrentBuff();
            mBuffs.Remove(buff);
            buff.ShieldHealth = (int)numShieldHP.Value;
            mBuffs.Add(buff);
        }
        private BuffEntry GetCurrentBuff()
        {
            return mBuffs.Find(b => b.Name == "ENCHANT_" + txtName.Text);
        }
        #endregion
    }
}
