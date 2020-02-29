using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using NoxShared;

namespace MapEditor
{
	public class PolygonDialog : System.Windows.Forms.Form
	{
		private ColorDialog clr = new ColorDialog();
        private NumericUpDown numMinimapGroup;
        public int[] CustomColors;

        public PolygonDialog()
		{
			InitializeComponent();
			colorButton.BackColor = Color.White;
		}

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox boxPoints;
        private System.Windows.Forms.TextBox name;

        private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolygonDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.boxPoints = new System.Windows.Forms.TextBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scriptOnUnknown = new System.Windows.Forms.TextBox();
            this.scriptOnEnter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.questSecretChk = new System.Windows.Forms.CheckBox();
            this.numMinimapGroup = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimapGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // boxPoints
            // 
            resources.ApplyResources(this.boxPoints, "boxPoints");
            this.boxPoints.Name = "boxPoints";
            // 
            // colorButton
            // 
            resources.ApplyResources(this.colorButton, "colorButton");
            this.colorButton.Name = "colorButton";
            this.colorButton.Click += new System.EventHandler(this.changeColorButtonClick);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            // 
            // name
            // 
            resources.ApplyResources(this.name, "name");
            this.name.Name = "name";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.scriptOnUnknown);
            this.groupBox1.Controls.Add(this.scriptOnEnter);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // scriptOnUnknown
            // 
            resources.ApplyResources(this.scriptOnUnknown, "scriptOnUnknown");
            this.scriptOnUnknown.Name = "scriptOnUnknown";
            // 
            // scriptOnEnter
            // 
            resources.ApplyResources(this.scriptOnEnter, "scriptOnEnter");
            this.scriptOnEnter.Name = "scriptOnEnter";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // questSecretChk
            // 
            resources.ApplyResources(this.questSecretChk, "questSecretChk");
            this.questSecretChk.Name = "questSecretChk";
            this.questSecretChk.UseVisualStyleBackColor = true;
            // 
            // numMinimapGroup
            // 
            resources.ApplyResources(this.numMinimapGroup, "numMinimapGroup");
            this.numMinimapGroup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numMinimapGroup.Name = "numMinimapGroup";
            this.numMinimapGroup.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // PolygonDialog
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.numMinimapGroup);
            this.Controls.Add(this.questSecretChk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.boxPoints);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PolygonDialog";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimapGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.CheckBox questSecretChk;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox scriptOnEnter;
		private System.Windows.Forms.TextBox scriptOnUnknown;
		private System.Windows.Forms.GroupBox groupBox1;
		#endregion

		private void changeColorButtonClick(object sender, System.EventArgs e)
		{
			clr.Color = colorButton.BackColor;
            if (CustomColors != null)
                clr.CustomColors = CustomColors;
            if (clr.ShowDialog() == DialogResult.OK)
            {
                colorButton.BackColor = clr.Color;
                CustomColors = clr.CustomColors;
            }
		}

		protected Map.Polygon polygon;
		public Map.Polygon Polygon
		{
			get
			{
                List<PointF> points = new List<PointF>();
                Regex regex = new Regex(@"(\s*(?<xval>[\+-]?[1-9][0-9]*\.?[0-9]*\s*)\s*,\s*(?<yval>[\+-]?[1-9][0-9]*\.?[0-9]*)\s*)");

                foreach (Match match in regex.Matches(boxPoints.Text))
                    points.Add(new PointF(Convert.ToSingle(match.Groups["xval"].Value), Convert.ToSingle(match.Groups["yval"].Value)));

				if (polygon == null)
				{
					return new Map.Polygon(name.Text, colorButton.BackColor, Convert.ToByte(numMinimapGroup.Value), points, scriptOnEnter.Text, scriptOnUnknown.Text, questSecretChk.Checked);
				}
				else
				{
					polygon.EnterFuncPlayer = scriptOnEnter.Text;
					polygon.EnterFuncMonster = scriptOnUnknown.Text;
					polygon.IsQuestSecret = questSecretChk.Checked;
					polygon.Name = name.Text;
					polygon.AmbientLightColor = colorButton.BackColor;
					polygon.MinimapGroup = Convert.ToByte(numMinimapGroup.Value);
					polygon.Points = points;

                    return polygon;
				}
			}
			set
			{
				polygon = value;
				if (polygon == null)
				{
					name.Text = "Main";
					colorButton.BackColor = Color.White;
					boxPoints.Text = "";
				}
				else
				{
                    scriptOnEnter.Text = polygon.EnterFuncPlayer;
                    scriptOnUnknown.Text = polygon.EnterFuncMonster;
                    questSecretChk.Checked = polygon.IsQuestSecret;
                    name.Text = polygon.Name;
					colorButton.BackColor = polygon.AmbientLightColor;
                    numMinimapGroup.Value = polygon.MinimapGroup;
					boxPoints.Text = "";
					foreach (PointF pt in polygon.Points)
						boxPoints.Text += String.Format("({0}, {1}) ", pt.X, pt.Y);
				}
			}
		}

        private void buttonOK_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.mapView.PolygonEditDlg.SelectedPolygon =  polygon;
            MainWindow.Instance.mapView.PolygonEditDlg.SuperPolygon = polygon;
        }
	}
}
