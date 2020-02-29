/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 14.11.2014
 */
namespace MapEditor.XferGui
{
	partial class SentryGlobeEdit
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblRotation = new System.Windows.Forms.Label();
            this.sentryAngle = new System.Windows.Forms.TextBox();
            this.sentrySpeed = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.sldrAngle = new System.Windows.Forms.TrackBar();
            this.sldrRotation = new System.Windows.Forms.TrackBar();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.tmrDraw = new System.Windows.Forms.Timer(this.components);
            this.lblDegrees = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.sldrAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldrRotation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAngle
            // 
            this.lblAngle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAngle.Location = new System.Drawing.Point(9, 15);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(92, 23);
            this.lblAngle.TabIndex = 0;
            this.lblAngle.Text = "Angle (Radians):";
            // 
            // lblRotation
            // 
            this.lblRotation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRotation.Location = new System.Drawing.Point(9, 85);
            this.lblRotation.Name = "lblRotation";
            this.lblRotation.Size = new System.Drawing.Size(92, 23);
            this.lblRotation.TabIndex = 1;
            this.lblRotation.Text = "Rotation Speed:";
            // 
            // sentryAngle
            // 
            this.sentryAngle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sentryAngle.Location = new System.Drawing.Point(98, 12);
            this.sentryAngle.Name = "sentryAngle";
            this.sentryAngle.Size = new System.Drawing.Size(100, 22);
            this.sentryAngle.TabIndex = 2;
            this.sentryAngle.TextChanged += new System.EventHandler(this.sentryAngle_TextChanged);
            // 
            // sentrySpeed
            // 
            this.sentrySpeed.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sentrySpeed.Location = new System.Drawing.Point(98, 82);
            this.sentrySpeed.Name = "sentrySpeed";
            this.sentrySpeed.Size = new System.Drawing.Size(100, 22);
            this.sentrySpeed.TabIndex = 3;
            this.sentrySpeed.TextChanged += new System.EventHandler(this.sentrySpeed_TextChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(25, 145);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 28);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // sldrAngle
            // 
            this.sldrAngle.LargeChange = 10;
            this.sldrAngle.Location = new System.Drawing.Point(12, 38);
            this.sldrAngle.Maximum = 360;
            this.sldrAngle.Name = "sldrAngle";
            this.sldrAngle.Size = new System.Drawing.Size(186, 45);
            this.sldrAngle.TabIndex = 13;
            this.sldrAngle.TickFrequency = 30;
            this.sldrAngle.Scroll += new System.EventHandler(this.sldrAngle_Scroll);
            // 
            // sldrRotation
            // 
            this.sldrRotation.Location = new System.Drawing.Point(12, 108);
            this.sldrRotation.Maximum = 50000;
            this.sldrRotation.Name = "sldrRotation";
            this.sldrRotation.Size = new System.Drawing.Size(189, 45);
            this.sldrRotation.TabIndex = 14;
            this.sldrRotation.TickFrequency = 5000;
            this.sldrRotation.Scroll += new System.EventHandler(this.sldrRotation_Scroll);
            this.sldrRotation.Enter += new System.EventHandler(this.sldrRotation_Enter);
            this.sldrRotation.Leave += new System.EventHandler(this.sldrRotation_Leave);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(106, 145);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 28);
            this.cmdCancel.TabIndex = 15;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // tmrDraw
            // 
            this.tmrDraw.Interval = 1;
            this.tmrDraw.Tick += new System.EventHandler(this.tmrDraw_Tick);
            // 
            // lblDegrees
            // 
            this.lblDegrees.AutoSize = true;
            this.lblDegrees.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDegrees.Location = new System.Drawing.Point(198, 13);
            this.lblDegrees.Name = "lblDegrees";
            this.lblDegrees.Size = new System.Drawing.Size(23, 20);
            this.lblDegrees.TabIndex = 16;
            this.lblDegrees.Text = "0°";
            // 
            // SentryGlobeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 177);
            this.Controls.Add(this.sentrySpeed);
            this.Controls.Add(this.lblRotation);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.sentryAngle);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.lblAngle);
            this.Controls.Add(this.sldrAngle);
            this.Controls.Add(this.sldrRotation);
            this.Controls.Add(this.lblDegrees);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SentryGlobeEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SentryGlobeXfer";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SentryGlobeEdit_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.sldrAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldrRotation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.TextBox sentrySpeed;
		private System.Windows.Forms.TextBox sentryAngle;
		private System.Windows.Forms.Label lblRotation;
		private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.TrackBar sldrAngle;
        private System.Windows.Forms.TrackBar sldrRotation;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Timer tmrDraw;
        private System.Windows.Forms.Label lblDegrees;
    }
}
