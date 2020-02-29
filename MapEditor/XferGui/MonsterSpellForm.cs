using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NoxShared;
using NoxShared.ObjDataXfer;

namespace MapEditor.XferGui
{
    public class MonsterSpellForm : Form
    {
        #region Windows Designer Components
        private System.Windows.Forms.CheckedListBox usageCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListBox spellsListBox;

        private void InitializeComponent()
        {
            this.spellsListBox = new System.Windows.Forms.ListBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usageCheckBox = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // spellsListBox
            // 
            this.spellsListBox.FormattingEnabled = true;
            this.spellsListBox.Location = new System.Drawing.Point(12, 38);
            this.spellsListBox.Name = "spellsListBox";
            this.spellsListBox.Size = new System.Drawing.Size(116, 95);
            this.spellsListBox.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(73, 148);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Spell";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(134, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Usage flags";
            // 
            // usageCheckBox
            // 
            this.usageCheckBox.FormattingEnabled = true;
            this.usageCheckBox.Location = new System.Drawing.Point(134, 38);
            this.usageCheckBox.Name = "usageCheckBox";
            this.usageCheckBox.Size = new System.Drawing.Size(88, 94);
            this.usageCheckBox.TabIndex = 4;
            // 
            // MonsterSpellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 183);
            this.Controls.Add(this.usageCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.spellsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonsterSpellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Monster Spell ";
            this.ResumeLayout(false);
        }
        #endregion

        private string[] spellIDArray;
        private uint[] spellUseFlags;

        public MonsterSpellForm()
        {
            InitializeComponent();
            // индексируем флаги
            spellUseFlags = (uint[])Enum.GetValues(typeof(NoxEnums.NPCSpellCastFlags));
            // добавляем флаги в чеклистбокс
            usageCheckBox.Items.AddRange(Enum.GetNames(typeof(NoxEnums.NPCSpellCastFlags)));
            // добавляем имена спеллов в листбокс
            spellIDArray = new string[ThingDb.Spells.Count];
            int i = 0;
            string humanSpellName;
            foreach (ThingDb.Spell spell in ThingDb.Spells.Values)
            {
                spellIDArray[i] = spell.Name; i++;
                humanSpellName = spell.NameString;
                int di = humanSpellName.IndexOf(':') + 1;
                humanSpellName = humanSpellName.Remove(0, di);
                spellsListBox.Items.Add(humanSpellName);
            }
            spellsListBox.SelectedIndex = 0;
        }

        public void SetSpell(MonsterXfer.SpellEntry spell)
        {
            // указываем имя заклинания
            spellsListBox.SelectedIndex = Array.IndexOf(spellIDArray, spell.SpellName);
            // чекаем флаги
            if ((spell.UseFlags & 0x08000000) == 0x08000000) usageCheckBox.SetItemChecked(0, true);
            if ((spell.UseFlags & 0x10000000) == 0x10000000) usageCheckBox.SetItemChecked(1, true);
            if ((spell.UseFlags & 0x20000000) == 0x20000000) usageCheckBox.SetItemChecked(2, true);
            if ((spell.UseFlags & 0x40000000) == 0x40000000) usageCheckBox.SetItemChecked(3, true);
            if ((spell.UseFlags & 0x80000000) == 0x80000000) usageCheckBox.SetItemChecked(4, true);
        }

        public MonsterXfer.SpellEntry GetSpell()
        {
            int spellIndex = spellsListBox.SelectedIndex;
            if (spellIndex < 0) spellIndex = 0;
            // смотрим флаги
            uint flags = 0;
            for (int i = 0; i < 5; i++)
            {
                if (usageCheckBox.GetItemChecked(i))
                    flags |= spellUseFlags[i];
            }
            // возвращаем результат
            return new MonsterXfer.SpellEntry(spellIDArray[spellIndex], flags);
        }

        private void ButtonOKClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
