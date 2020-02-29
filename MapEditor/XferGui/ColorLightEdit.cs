/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 20.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using NoxShared.ObjDataXfer;
namespace MapEditor.XferGui
{
    public partial class ColorLightEdit : XferEditor
    {
        private Color UnknownRcol;
        private InvisibleLightXfer xfer;
        int lastPulseVal = 30;

        public ColorLightEdit()
        {
            InitializeComponent();
        }
        private void ColorLightEdit_Load(object sender, EventArgs e)
        {
            sizable();
        }

        private void sizable()
        {
            sequenceOptions.Height = 60;

            if (cmdColor1.Visible)
                sequenceOptions.Height = 90;

            if (cmdColor6.Visible)
                sequenceOptions.Height = 125;
        }
        public override void SetObject(NoxShared.Map.Object obj)
        {
            this.obj = obj;
            xfer = obj.GetExtraData<InvisibleLightXfer>();
            numericUpDown1.Value = xfer.LightIntensity;
            numericUpDown2.Value = xfer.PulseSpeed;
            numericUpDown3.Value = xfer.ChangeIntensity;
            outterSize.Value = xfer.LightRadius;
            shadow.Checked = xfer.type == 1 ? true : false;
            sequenceOptions.Height = 60;

            numericUpDown2.Value = xfer.PulseSpeed;

            xxx.Value = xfer.Unknown10;
            ChangeIntensitySingleNum.Value = xfer.ChangeIntensitySingle;
            PulseSpeedSingleNum.Value = xfer.PulseSpeedSingle;
            unkwn11.Value = xfer.Unknown11;
            /*
            if (xfer.PulseSpeed > 0)
            {
                StaticOptions.Enabled = false;
                PulsingOptions.Enabled = true;
            }
            */
            Color newColor = Color.FromArgb(xfer.R, xfer.G, xfer.B);
            Color newColor2 = Color.FromArgb(xfer.R2, xfer.G2, xfer.B2);

            UnknownRcol = Color.FromArgb(xfer.UnknownR, xfer.UnknownG, xfer.UnknownB);

            /*
            if (xfer.Color2.R == 0 && xfer.Color2.G == 0 && xfer.Color2.B == 0)
                Color2Pulse = Color.White;
            else
                Color2Pulse = xfer.Color2;

            
            if (xfer.UnknownR == 0 && xfer.UnknownG == 0 && xfer.UnknownB == 0)
                UnknownRcol = Color.White;
            */
            Color Color1Pulse = xfer.Color1;
            Color Color2Pulse = xfer.Color2;
            Color Color3Pulse = xfer.Color3;
            Color Color4Pulse = xfer.Color4;
            Color Color5Pulse = xfer.Color5;
            Color Color6Pulse = xfer.Color6;
            Color Color7Pulse = xfer.Color7;
            Color Color8Pulse = xfer.Color8;
            Color Color9Pulse = xfer.Color9;
            Color Color10Pulse = xfer.Color10;

            newColor2 = Color.FromArgb((byte)~newColor2.R, (byte)~newColor2.G, (byte)~newColor2.B);
            if (shadow.Checked)
            {
                newColor = Color.FromArgb((byte)~newColor.R, (byte)~newColor.G, (byte)~newColor.B);
                Color1Pulse = Color.FromArgb((byte)~Color1Pulse.R, (byte)~Color1Pulse.G, (byte)~Color1Pulse.B);
                Color2Pulse = Color.FromArgb((byte)~Color2Pulse.R, (byte)~Color2Pulse.G, (byte)~Color2Pulse.B);
                Color3Pulse = Color.FromArgb((byte)~Color3Pulse.R, (byte)~Color3Pulse.G, (byte)~Color3Pulse.B);
                Color4Pulse = Color.FromArgb((byte)~Color4Pulse.R, (byte)~Color4Pulse.G, (byte)~Color4Pulse.B);
                Color5Pulse = Color.FromArgb((byte)~Color5Pulse.R, (byte)~Color5Pulse.G, (byte)~Color5Pulse.B);
                Color6Pulse = Color.FromArgb((byte)~Color6Pulse.R, (byte)~Color6Pulse.G, (byte)~Color6Pulse.B);
                Color7Pulse = Color.FromArgb((byte)~Color7Pulse.R, (byte)~Color7Pulse.G, (byte)~Color7Pulse.B);
                Color8Pulse = Color.FromArgb((byte)~Color8Pulse.R, (byte)~Color8Pulse.G, (byte)~Color8Pulse.B);
                Color9Pulse = Color.FromArgb((byte)~Color9Pulse.R, (byte)~Color9Pulse.G, (byte)~Color9Pulse.B);
                Color10Pulse = Color.FromArgb((byte)~Color10Pulse.R, (byte)~Color10Pulse.G, (byte)~Color10Pulse.B);

            }
            cmdColor.BackColor = newColor;
            cmdColor1.BackColor = Color1Pulse;

            cmdColor2.BackColor = Color2Pulse;
            cmdColor3.BackColor = Color3Pulse;
            cmdColor4.BackColor = Color4Pulse;
            cmdColor5.BackColor = Color5Pulse;
            cmdColor6.BackColor = Color6Pulse;

            cmdColor7.BackColor = Color7Pulse;
            cmdColor8.BackColor = Color8Pulse;
            cmdColor9.BackColor = Color9Pulse;
            cmdColor10.BackColor = Color10Pulse;

            cmdGradient.BackColor = newColor2;

            cmdColor1.Visible = false;
            cmdColor2.Visible = false;
            cmdColor3.Visible = false;
            cmdColor4.Visible = false;
            cmdColor5.Visible = false;
            cmdColor6.Visible = false;
            cmdColor7.Visible = false;
            cmdColor8.Visible = false;
            cmdColor9.Visible = false;
            cmdColor10.Visible = false;

            if (xfer.UnknownR > 0 || xfer.UnknownG > 0 || xfer.UnknownB > 0)
            {
                chkCrazyLight.Checked = true;
                cmdCrazyColor.Enabled = true;
                cmdCrazyColor.BackColor = UnknownRcol;
            }
            else
            {
                chkCrazyLight.Checked = false;
                cmdCrazyColor.Enabled = false;
            }
            if (xfer.Color1.R > 0 || xfer.Color1.G > 0 || xfer.Color1.B > 0)
            {
                cmdColor1.Visible = true;
                cmdColor1.BackColor = Color1Pulse;
            }
            else
            {
                cmdColor1.Visible = false;
                goto done;
            }

            if (xfer.Color2.R > 0 || xfer.Color2.G > 0 || xfer.Color2.B > 0)
            {
                cmdColor2.Visible = true;
                cmdColor2.BackColor = Color2Pulse;
            }
            else
            {
                cmdColor1.Visible = false;
                cmdColor2.Visible = false;
                goto done;
            }

            if (xfer.Color3.R > 0 || xfer.Color3.G > 0 || xfer.Color3.B > 0)
            {
                cmdColor3.Visible = true;
                cmdColor3.BackColor = Color3Pulse;
            }
            else
            {
                cmdColor3.Visible = false;
                goto done;
            }

            if (xfer.Color4.R > 0 || xfer.Color4.G > 0 || xfer.Color4.B > 0)
            {
                cmdColor4.Visible = true;
                cmdColor4.BackColor = Color4Pulse;
            }
            else
            {
                cmdColor4.Visible = false;
                goto done;
            }

            if (xfer.Color5.R > 0 || xfer.Color5.G > 0 || xfer.Color5.B > 0)
            {
                cmdColor5.Visible = true;
                cmdColor5.BackColor = Color5Pulse;
            }
            else
            {
                cmdColor5.Visible = false;
                goto done;
            }
            if (xfer.Color6.R > 0 || xfer.Color6.G > 0 || xfer.Color6.B > 0)
            {
                cmdColor6.Visible = true;
                cmdColor6.BackColor = Color6Pulse;
            }
            else
            {
                cmdColor6.Visible = false;
                goto done;
            }
            if (xfer.Color7.R > 0 || xfer.Color7.G > 0 || xfer.Color7.B > 0)
            {
                cmdColor7.Visible = true;
                cmdColor7.BackColor = Color7Pulse;
            }
            else
            {
                cmdColor7.Visible = false;
                goto done;
            }
            if (xfer.Color8.R > 0 || xfer.Color8.G > 0 || xfer.Color8.B > 0)
            {
                cmdColor8.Visible = true;
                cmdColor8.BackColor = Color8Pulse;
            }
            else
            {
                cmdColor8.Visible = false;
                goto done;
            }
            if (xfer.Color9.R > 0 || xfer.Color9.G > 0 || xfer.Color9.B > 0)
            {
                cmdColor9.Visible = true;
                cmdColor9.BackColor = Color9Pulse;
            }
            else
            {
                cmdColor9.Visible = false;
                goto done;
            }
            if (xfer.Color10.R > 0 || xfer.Color10.G > 0 || xfer.Color10.B > 0)
            {
                cmdColor10.Visible = true;
                cmdColor10.BackColor = Color10Pulse;
            }
            else
            {
                cmdColor10.Visible = false;
                goto done;
            }
        done:

            max2.Value = xfer.MaxRadius2;
            max3.Value = xfer.MaxRadius3;
            max4.Value = xfer.MaxRadius4;
            max5.Value = xfer.MaxRadius5;
            max6.Value = xfer.MaxRadius6;
            max7.Value = xfer.MaxRadius7;
            max8.Value = xfer.MaxRadius8;
            max9.Value = xfer.MaxRadius9;
            max10.Value = xfer.MaxRadius10;

            min2.Value = xfer.MinRadius2;
            min3.Value = xfer.MinRadius3;
            min4.Value = xfer.MinRadius4;
            min5.Value = xfer.MinRadius5;
            min6.Value = xfer.MinRadius6;
            min7.Value = xfer.MinRadius7;
            min8.Value = xfer.MinRadius8;
            min9.Value = xfer.MinRadius9;
            min10.Value = xfer.MinRadius10;

            if (PulseSpeedSingleNum.Value > 0)
            {
                PulsingBox.Checked = true;
                PulsingOptions.Enabled = true;
                StaticOptions.Enabled = false;
            }
            else
            {
                PulsingBox.Checked = false;
                PulsingOptions.Enabled = false;
            }
        }

        private void cmdGradient_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color shouldn't be changed in order to keep normal looking colorlight.");
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdGradient.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdGradient.BackColor = colorDlg.Color;
            }
        }
        private void cmdCrazyColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdCrazyColor.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdCrazyColor.BackColor = colorDlg.Color;
                UnknownRcol = colorDlg.Color;
            }
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            xfer.LightIntensity = (byte)numericUpDown1.Value;
            xfer.PulseSpeed = (byte)numericUpDown2.Value;
            xfer.ChangeIntensity = (byte)numericUpDown3.Value;

            xfer.LightRadius = (byte)outterSize.Value;
            xfer.type = shadow.Checked ? (byte)1 : (byte)0;

            xfer.PulseSpeedSingle = (byte)PulseSpeedSingleNum.Value;
            xfer.ChangeIntensitySingle = (byte)ChangeIntensitySingleNum.Value;
            xfer.Unknown10 = (byte)xxx.Value;
            byte RR2 = cmdGradient.BackColor.R;
            byte GG2 = cmdGradient.BackColor.G;
            byte BB2 = cmdGradient.BackColor.B;

            xfer.Unknown11 = (byte)unkwn11.Value;
            //xfer.UnknownR = crazy.Checked ? (byte)crazyNum.Value : (byte)0;
            Color UnknownRcol = cmdCrazyColor.BackColor;

            Color pulseColor1 = cmdColor1.BackColor;
            Color pulseColor2 = cmdColor2.BackColor;
            Color pulseColor3 = cmdColor3.BackColor;
            Color pulseColor4 = cmdColor4.BackColor;
            Color pulseColor5 = cmdColor5.BackColor;
            Color pulseColor6 = cmdColor6.BackColor;
            Color pulseColor7 = cmdColor7.BackColor;
            Color pulseColor8 = cmdColor8.BackColor;
            Color pulseColor9 = cmdColor9.BackColor;
            Color pulseColor10 = cmdColor10.BackColor;

            Color gradColor = Color.FromArgb(cmdGradient.BackColor.R, cmdGradient.BackColor.G, cmdGradient.BackColor.B);
            Color baseColor = Color.FromArgb(cmdColor.BackColor.R, cmdColor.BackColor.G, cmdColor.BackColor.B);
            gradColor = Color.FromArgb((byte)~gradColor.R, (byte)~gradColor.G, (byte)~gradColor.B);
            //UnknownRcol = Color.FromArgb((byte)~UnknownRcol.R, (byte)~UnknownRcol.G, (byte)~UnknownRcol.B);

            if (shadow.Checked)
            {
                baseColor = Color.FromArgb((byte)~baseColor.R, (byte)~baseColor.G, (byte)~baseColor.B);

                pulseColor1 = Color.FromArgb((byte)~pulseColor1.R, (byte)~pulseColor1.G, (byte)~pulseColor1.B);
                pulseColor2 = Color.FromArgb((byte)~pulseColor2.R, (byte)~pulseColor2.G, (byte)~pulseColor2.B);
                pulseColor3 = Color.FromArgb((byte)~pulseColor3.R, (byte)~pulseColor3.G, (byte)~pulseColor3.B);
                pulseColor4 = Color.FromArgb((byte)~pulseColor4.R, (byte)~pulseColor4.G, (byte)~pulseColor4.B);

                pulseColor5 = Color.FromArgb((byte)~pulseColor5.R, (byte)~pulseColor5.G, (byte)~pulseColor5.B);

                pulseColor6 = Color.FromArgb((byte)~pulseColor6.R, (byte)~pulseColor6.G, (byte)~pulseColor6.B);
                pulseColor7 = Color.FromArgb((byte)~pulseColor7.R, (byte)~pulseColor7.G, (byte)~pulseColor7.B);
                pulseColor8 = Color.FromArgb((byte)~pulseColor8.R, (byte)~pulseColor8.G, (byte)~pulseColor8.B);
                pulseColor9 = Color.FromArgb((byte)~pulseColor9.R, (byte)~pulseColor9.G, (byte)~pulseColor9.B);
                pulseColor10 = Color.FromArgb((byte)~pulseColor10.R, (byte)~pulseColor10.G, (byte)~pulseColor10.B);
                // UnknownRcol = Color.FromArgb((byte)~pulseColor2.R, (byte)~pulseColor2.G, (byte)~pulseColor2.B);
            }

            UnknownRcol = chkCrazyLight.Checked ? UnknownRcol : Color.Black;

            xfer.R = baseColor.R;
            xfer.G = baseColor.G;
            xfer.B = baseColor.B;

            xfer.R2 = gradColor.R;
            xfer.G2 = gradColor.G;
            xfer.B2 = gradColor.B;

            xfer.UnknownR = UnknownRcol.R;
            xfer.UnknownG = UnknownRcol.G;
            xfer.UnknownB = UnknownRcol.B;

            xfer.Color1 = pulseColor1;
            xfer.Color2 = cmdColor2.Visible ? pulseColor2 : Color.Black;
            xfer.Color3 = cmdColor3.Visible ? pulseColor3 : Color.Black;
            xfer.Color4 = cmdColor4.Visible ? pulseColor4 : Color.Black;
            xfer.Color5 = cmdColor5.Visible ? pulseColor5 : Color.Black;
            xfer.Color6 = cmdColor6.Visible ? pulseColor6 : Color.Black;
            xfer.Color7 = cmdColor7.Visible ? pulseColor7 : Color.Black;
            xfer.Color8 = cmdColor8.Visible ? pulseColor8 : Color.Black;
            xfer.Color9 = cmdColor9.Visible ? pulseColor9 : Color.Black;
            xfer.Color10 = cmdColor10.Visible ? pulseColor10 : Color.Black;

            xfer.MaxRadius2 = (byte)max2.Value;
            xfer.MinRadius2 = (byte)min2.Value;
            xfer.MaxRadius3 = (byte)max3.Value;
            xfer.MinRadius3 = (byte)min3.Value;
            xfer.MaxRadius4 = (byte)max4.Value;
            xfer.MinRadius4 = (byte)min4.Value;
            xfer.MaxRadius5 = (byte)max5.Value;
            xfer.MinRadius5 = (byte)min5.Value;
            xfer.MaxRadius6 = (byte)max6.Value;
            xfer.MinRadius6 = (byte)min6.Value;
            xfer.MaxRadius6 = (byte)max6.Value;
            xfer.MinRadius6 = (byte)min6.Value;
            xfer.MaxRadius7 = (byte)max7.Value;
            xfer.MinRadius7 = (byte)min7.Value;
            xfer.MaxRadius8 = (byte)max8.Value;
            xfer.MinRadius8 = (byte)min8.Value;
            xfer.MaxRadius9 = (byte)max9.Value;
            xfer.MinRadius9 = (byte)min9.Value;
            xfer.MaxRadius10 = (byte)max10.Value;
            xfer.MinRadius10 = (byte)min10.Value;

            Close();
        }

        private void cmdColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor.BackColor = colorDlg.Color;
            }
        }
        private void cmdColor1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor1.BackColor = colorDlg.Color;
                if (cmdColor1.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor1.Visible = false;
                    cmdColor2.Visible = false;
                    cmdColor3.Visible = false;
                    cmdColor4.Visible = false;
                    cmdColor5.Visible = false;
                    cmdColor6.Visible = false;
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
                sizable();
            }
        }
        private void cmdColor2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor2.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor2.BackColor = colorDlg.Color;
                if (cmdColor2.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor1.Visible = false;
                    cmdColor2.Visible = false;
                    cmdColor3.Visible = false;
                    cmdColor4.Visible = false;
                    cmdColor5.Visible = false;
                    cmdColor6.Visible = false;
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
                sizable();
            }
        }
        private void cmdColor3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor3.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor3.BackColor = colorDlg.Color;
                if (cmdColor3.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor3.Visible = false;
                    cmdColor4.Visible = false;
                    cmdColor5.Visible = false;
                    cmdColor6.Visible = false;
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
                sizable();
            }
        }
        private void cmdColor4_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor4.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor4.BackColor = colorDlg.Color;
                if (cmdColor4.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor4.Visible = false;
                    cmdColor5.Visible = false;
                    cmdColor6.Visible = false;
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
                sizable();
            }
        }
        private void cmdColor5_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor5.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor5.BackColor = colorDlg.Color;
                if (cmdColor5.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor5.Visible = false;
                    cmdColor6.Visible = false;
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
                sizable();
            }
        }
        private void cmdColor6_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor6.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor6.BackColor = colorDlg.Color;
                if (cmdColor6.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor6.Visible = false;
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
                sizable();
            }
        }
        private void cmdColor7_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor7.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor7.BackColor = colorDlg.Color;
                if (cmdColor7.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor7.Visible = false;
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
            }
        }
        private void cmdColor8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor8.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor8.BackColor = colorDlg.Color;
                if (cmdColor8.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor8.Visible = false;
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }

            }
        }
        private void cmdColor9_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor9.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor9.BackColor = colorDlg.Color;
                if (cmdColor9.BackColor == (shadow.Checked ? Color.White : Color.Black))
                {
                    cmdColor9.Visible = false;
                    cmdColor10.Visible = false;
                }
            }
        }
        private void cmdColor10_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.Color = cmdColor10.BackColor;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                cmdColor10.BackColor = colorDlg.Color;
                if (cmdColor10.BackColor == (shadow.Checked ? Color.White : Color.Black))
                    cmdColor10.Visible = false;
            }
        }
        private void cmdRemoveColor_Click(object sender, EventArgs e)
        {
            if (cmdColor10.Visible)
            {
                cmdColor10.Visible = false;
                sizable();
                return;
            }
            if (cmdColor9.Visible)
            {
                cmdColor9.Visible = false;
                sizable();
                return;
            }
            if (cmdColor8.Visible)
            {
                cmdColor8.Visible = false;
                sizable();
                return;
            }
            if (cmdColor7.Visible)
            {
                cmdColor7.Visible = false;
                sizable();
                return;
            }
            if (cmdColor6.Visible)
            {
                cmdColor6.Visible = false;
                sizable();
                return;
            }
            if (cmdColor5.Visible)
            {
                cmdColor5.Visible = false;
                sizable();
                return;
            }
            if (cmdColor4.Visible)
            {
                cmdColor4.Visible = false;
                sizable();
                return;
            }
            if (cmdColor3.Visible)
            {
                cmdColor3.Visible = false;
                sizable();
                return;
            }
            if (cmdColor2.Visible)
            {
                cmdColor1.Visible = false;
                cmdColor2.Visible = false;
                sizable();
                return;
            }
            if (cmdColor1.Visible)
            {
                cmdColor1.Visible = false;
                sizable();
                return;
            }

        }
        private void cmdAddColor_Click(object sender, EventArgs e)
        {
            if (cmdColor1.BackColor == (!shadow.Checked ? Color.Black : Color.White))
            {
                cmdColor1.Visible = false;
                cmdColor2.Visible = false;
            }
            if (cmdColor2.BackColor == (!shadow.Checked ? Color.Black : Color.White))
            {
                cmdColor2.Visible = false;
                cmdColor1.Visible = false;
            }

            if (cmdColor3.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor3.Visible = false;
            if (cmdColor4.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor4.Visible = false;
            if (cmdColor5.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor5.Visible = false;
            if (cmdColor6.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor6.Visible = false;
            if (cmdColor7.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor7.Visible = false;
            if (cmdColor8.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor8.Visible = false;
            if (cmdColor9.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor9.Visible = false;
            if (cmdColor10.BackColor == (!shadow.Checked ? Color.Black : Color.White))
                cmdColor10.Visible = false;


            if (!cmdColor1.Visible)
            {
                cmdColor1.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor2.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor1.Visible = true;
                cmdColor2.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor2.Visible)
            {
                cmdColor2.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor2.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor3.Visible)
            {
                cmdColor3.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor3.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor4.Visible)
            {
                cmdColor4.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor4.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor5.Visible)
            {
                cmdColor5.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor5.Visible = true;
                sizable();
                return;
            }

            if (!cmdColor6.Visible)
            {
                cmdColor6.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor6.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor7.Visible)
            {
                cmdColor7.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor7.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor8.Visible)
            {
                cmdColor8.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor8.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor9.Visible)
            {
                cmdColor9.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor9.Visible = true;
                sizable();
                return;
            }
            if (!cmdColor10.Visible)
            {
                cmdColor10.BackColor = shadow.Checked ? Color.Black : Color.White;
                cmdColor10.Visible = true;
                sizable();
                return;
            }
        }

        private void ChangeIntensitySingleNum_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeIntensitySingleNum.Value <= xxx.Value)
                ChangeIntensitySingleNum.Value = xxx.Value + 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StaticOptions.Enabled = true;
            PulsingOptions.Enabled = false;

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            StaticOptions.Enabled = false;
            PulsingOptions.Enabled = true;
        }
        private void PulsingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PulsingBox.Checked)
            {
                StaticOptions.Enabled = false;
                PulsingOptions.Enabled = true;
                StaticOptions.Enabled = false;

                if (ChangeIntensitySingleNum.Value <= 0)
                    ChangeIntensitySingleNum.Value = numericUpDown1.Value;

                if (PulseSpeedSingleNum.Value <= 0)
                {
                    PulseSpeedSingleNum.Value = lastPulseVal;
                    PulseSpeedSingleNum.Minimum = 1;
                }

                if (xxx.Value <= 0)
                {
                    xxx.Value = 20;
                    xxx.Minimum = 1;
                }
            }
            else
            {
                lastPulseVal = (int)PulseSpeedSingleNum.Value;
                PulseSpeedSingleNum.Minimum = 0;

                StaticOptions.Enabled = true;
                PulsingOptions.Enabled = false;
                //numericUpDown1.Enabled = true;
                PulseSpeedSingleNum.Value = 0;
            }
        }
        private void shadow_CheckedChanged(object sender, EventArgs e)
        {
            if (shadow.Checked)
            {
                cmdColor1.BackColor = cmdColor1.BackColor.R == 255 && cmdColor1.BackColor.G == 255 && cmdColor1.BackColor.B == 255 ? Color.Black : cmdColor1.BackColor;
                cmdColor2.BackColor = cmdColor2.BackColor.R == 255 && cmdColor2.BackColor.G == 255 && cmdColor2.BackColor.B == 255 ? Color.Black : cmdColor2.BackColor;
                cmdColor3.BackColor = cmdColor3.BackColor == Color.White ? Color.Black : cmdColor3.BackColor;
                cmdColor4.BackColor = cmdColor4.BackColor.R == 255 && cmdColor4.BackColor.G == 255 && cmdColor4.BackColor.B == 255 ? Color.Black : cmdColor4.BackColor;
                cmdColor5.BackColor = cmdColor5.BackColor == Color.White ? Color.Black : cmdColor5.BackColor;
                cmdColor6.BackColor = cmdColor6.BackColor == Color.White ? Color.Black : cmdColor6.BackColor;
                cmdColor7.BackColor = cmdColor7.BackColor == Color.White ? Color.Black : cmdColor7.BackColor;
                cmdColor8.BackColor = cmdColor8.BackColor == Color.White ? Color.Black : cmdColor8.BackColor;
                cmdColor9.BackColor = cmdColor9.BackColor == Color.White ? Color.Black : cmdColor9.BackColor;
                cmdColor10.BackColor = cmdColor10.BackColor == Color.White ? Color.Black : cmdColor10.BackColor;
            }
            else
            {
                cmdColor1.BackColor = cmdColor1.BackColor.R == 0 && cmdColor1.BackColor.G == 0 && cmdColor1.BackColor.B == 0 ? Color.White : cmdColor1.BackColor;
                cmdColor2.BackColor = cmdColor2.BackColor.R == 0 && cmdColor2.BackColor.G == 0 && cmdColor2.BackColor.B == 0 ? Color.White : cmdColor2.BackColor;
                cmdColor3.BackColor = cmdColor3.BackColor.R == 0 && cmdColor3.BackColor.G == 0 && cmdColor3.BackColor.B == 0 ? Color.White : cmdColor3.BackColor;
                cmdColor4.BackColor = cmdColor4.BackColor.R == 0 && cmdColor4.BackColor.G == 0 && cmdColor4.BackColor.B == 0 ? Color.White : cmdColor4.BackColor;
                cmdColor5.BackColor = cmdColor5.BackColor.R == 0 && cmdColor5.BackColor.G == 0 && cmdColor5.BackColor.B == 0 ? Color.White : cmdColor5.BackColor;
                cmdColor6.BackColor = cmdColor6.BackColor.R == 0 && cmdColor6.BackColor.G == 0 && cmdColor6.BackColor.B == 0 ? Color.White : cmdColor6.BackColor;
                cmdColor7.BackColor = cmdColor7.BackColor.R == 0 && cmdColor7.BackColor.G == 0 && cmdColor7.BackColor.B == 0 ? Color.White : cmdColor7.BackColor;
                cmdColor8.BackColor = cmdColor8.BackColor.R == 0 && cmdColor8.BackColor.G == 0 && cmdColor8.BackColor.B == 0 ? Color.White : cmdColor8.BackColor;
                cmdColor9.BackColor = cmdColor9.BackColor.R == 0 && cmdColor9.BackColor.G == 0 && cmdColor9.BackColor.B == 0 ? Color.White : cmdColor9.BackColor;
                cmdColor10.BackColor = cmdColor10.BackColor.R == 0 && cmdColor10.BackColor.G == 0 && cmdColor10.BackColor.B == 0 ? Color.White : cmdColor10.BackColor;
            }
        }
        private void chkCrazyLight_CheckedChanged(object sender, EventArgs e)
        {
            cmdCrazyColor.Enabled = chkCrazyLight.Checked ? true : false;
            cmdCrazyColor.BackColor = chkCrazyLight.Checked ? UnknownRcol : Color.LightGray;
        }
        private void secCol_CheckedChanged(object sender, EventArgs e)
        {
            // Colbutton2.Enabled = secCol.Checked ? true : false;
            // Colbutton2.BackColor = secCol.Checked ? Color2Pulse : Color.LightGray;
        }

        private void cmdColor2_VisibleChanged(object sender, EventArgs e)
        {
            if (cmdColor2.Visible)
            {
                numericUpDown2.Value = numericUpDown2.Value <= 0 ? 20 : numericUpDown2.Value;
                numericUpDown2.Minimum = 1;
                cmdColor.Enabled = false;

            }
            else
            {
                numericUpDown2.Minimum = 0;
                cmdColor.Enabled = true;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
