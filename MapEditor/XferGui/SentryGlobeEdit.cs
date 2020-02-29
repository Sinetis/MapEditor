/*
 * MapEditor
 * Пользователь: AngryKirC
 * Copyleft - Public Domain
 * Дата: 14.11.2014
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using NoxShared.ObjDataXfer;
using NoxShared;

namespace MapEditor.XferGui
{
    public partial class SentryGlobeEdit : XferEditor
    {
        private static NumberFormatInfo floatFormat = NumberFormatInfo.InvariantInfo;
        private static int POINT_RATIO = 100000;
        private static float BEAM_LENGTH = 85F;
        private float rad = 0;
        private float speed = 0;

        public SentryGlobeEdit()
        {
            InitializeComponent();
        }

        public override void SetObject(Map.Object obj)
        {
            this.obj = obj;
            SentryXfer xfer = obj.GetExtraData<SentryXfer>();
            sentryAngle.Text = xfer.BasePosRadian.ToString(floatFormat);
            sentrySpeed.Text = xfer.RotateSpeed.ToString(floatFormat);
            try { sldrAngle.Value = RadianToDegrees(xfer.BasePosRadian); }
            catch { }
            try { sldrRotation.Value = (int)(xfer.RotateSpeed * POINT_RATIO); }
            catch { }
            lblDegrees.Text = sldrAngle.Value + "°";
            Invalidate();
        }

        private void ButtonOKClick(object sender, EventArgs e)
        {
            SentryXfer xfer = obj.GetExtraData<SentryXfer>();
            xfer.BasePosRadian = float.Parse(sentryAngle.Text, floatFormat);
            xfer.CurrentPosRadian = float.Parse(sentryAngle.Text, floatFormat);
            xfer.RotateSpeed = float.Parse(sentrySpeed.Text, floatFormat);
            Close();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sldrAngle_Scroll(object sender, EventArgs e)
        {
            sentryAngle.Text = DegreesToRadian(sldrAngle.Value).ToString(floatFormat);
            lblDegrees.Text = sldrAngle.Value + "°";
            Invalidate();
        }
        private void sentryAngle_TextChanged(object sender, EventArgs e)
        {
            if (sentryAngle.Focused)
            {
                try
                {
                    int currVal = RadianToDegrees(float.Parse(sentryAngle.Text));
                    if (currVal > sldrAngle.Maximum)
                        return;

                    sldrAngle.Value = currVal;
                    Invalidate();
                }
                catch { }
            }
        }

        private void sldrRotation_Scroll(object sender, EventArgs e)
        {
            var result = (float)sldrRotation.Value / POINT_RATIO;
            sentrySpeed.Text = result.ToString();
            speed = result * 0.5753975F;
        }
        private void sentrySpeed_TextChanged(object sender, EventArgs e)
        {
            if (sentrySpeed.Focused)
            {
                try
                {
                    int currVal = (int)(float.Parse(sentrySpeed.Text) * POINT_RATIO);
                    if (currVal > sldrRotation.Maximum)
                        return;

                    sldrRotation.Value = currVal;
                }
                catch { }
            }
        }

        private void SentryGlobeEdit_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(Color.Violet);
            pen.Width = 5;

            float x = 290.0F; float y = 80.0F;

            if (!tmrDraw.Enabled)
            {
                float targX = x + ((float)Math.Cos(DegreesToRadian(sldrAngle.Value)) * BEAM_LENGTH);
                float targY = y + ((float)Math.Sin(DegreesToRadian(sldrAngle.Value)) * BEAM_LENGTH);

                var m = new Pen(Color.Magenta, 5);
                e.Graphics.DrawLine(m, x, y, targX, targY);
            }
            else
            {
                float targX = x + ((float)Math.Cos(rad) * BEAM_LENGTH);
                float targY = y + ((float)Math.Sin(rad) * BEAM_LENGTH);

                e.Graphics.DrawLine(pen, x, y, targX, targY);
            }
            e.Graphics.FillEllipse(Brushes.Magenta, x - 7, y - 7, 13, 13);
        }
        private void tmrDraw_Tick(object sender, EventArgs e)
        {
            if (rad < Math.PI * 2)
                rad += speed;
            else
                rad = 0;

            Invalidate();
        }
        private void sldrRotation_Enter(object sender, EventArgs e)
        {
            tmrDraw.Start();
        }
        private void sldrRotation_Leave(object sender, EventArgs e)
        {
            tmrDraw.Stop();
            Invalidate();
        }

        private int RadianToDegrees(float rad) { return (int)(rad * (180.0 / Math.PI)); }
        private float DegreesToRadian(int deg) { return (float)Math.Round((Math.PI * deg / 180.0), 6); }
    }
}
