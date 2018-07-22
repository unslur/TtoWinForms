using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tto
{
    /// 作者：煎饼的归宿
    /// <summary>
    /// 圆形进度条 - 项目中添加这个类编译一下，工具箱就会出现圆弧形进度条
    /// </summary>
    public class CircularProgressBar : Control
    {
        private Color mainColor = Color.LimeGreen;

        public Color MainColor
        {
            get
            {
                return mainColor;
            }
            set
            {
                mainColor = value;
                this.Invalidate();
            }
        }
        private int value = 100;

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                this.Invalidate();
            }
        }

        private int lastWidth = 0;
        private int lastHeight = 0;
        private Point lastLocation = Point.Empty;
        public CircularProgressBar()
        {
            //指定控件的样式和行为
            //this.SetStyle(ControlStyles.UserPaint, true); //用户自行重绘
            //this.SetStyle(ControlStyles.ResizeRedraw, true); //调整大小时重绘
            //this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.Opaque, false);
            //this.BackColor = Color.White;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.ContainerControl |
                ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            this.BackColor = Color.White;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            this.lastWidth = this.Width;
            this.lastHeight = this.Height;
            this.lastLocation = this.Location;
            base.OnHandleCreated(e);
        }
        private bool isSizeChangeAble = true; //是否允许OnSizeChanged执行
        protected override void OnSizeChanged(EventArgs e)
        {
            if (isSizeChangeAble)
            {
                isSizeChangeAble = false;
                if (this.Width < this.lastWidth || this.Height < this.lastHeight)
                {
                    this.Width = Math.Min(this.Width, this.Height);
                    this.Height = Math.Min(this.Width, this.Height);
                    this.lastWidth = this.Width;
                    this.lastHeight = this.Height;
                    base.OnSizeChanged(e);
                    isSizeChangeAble = true;
                    return;
                }
                if (this.Width > this.lastWidth || this.Height > this.lastHeight)
                {
                    this.Width = Math.Max(this.Width, this.Height);
                    this.Height = Math.Max(this.Width, this.Height);
                    this.lastWidth = this.Width;
                    this.lastHeight = this.Height;
                    base.OnSizeChanged(e);
                    isSizeChangeAble = true;
                    return;
                }
                this.lastWidth = this.Width;
                this.lastHeight = this.Height;
                base.OnSizeChanged(e);
                isSizeChangeAble = true;
                return;
            }
        }
        private int circularWidth = 16;
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                if (this.value == 100)
                {
                    e.Graphics.FillEllipse(new SolidBrush(this.mainColor), new Rectangle(new Point(e.ClipRectangle.X + circularWidth / 2, e.ClipRectangle.Y + circularWidth / 2), new Size(e.ClipRectangle.Width - 1 - circularWidth, e.ClipRectangle.Height - 1 - circularWidth)));
                }
                using (Pen p = new Pen(Brushes.LightGray, circularWidth))
                {
                    //设置起止点线帽
                    //p.StartCap = LineCap.Round;
                    //p.EndCap = LineCap.Round;
                    //设置连续两段的联接样式
                    p.LineJoin = LineJoin.Round;
                    e.Graphics.DrawEllipse(p, new Rectangle(new Point(e.ClipRectangle.X + circularWidth / 2, e.ClipRectangle.Y + circularWidth / 2), new Size(e.ClipRectangle.Width - 1 - circularWidth, e.ClipRectangle.Height - 1 - circularWidth)));
                }
                if (this.value < 100)
                {
                    using (Pen p = new Pen(new SolidBrush(this.mainColor), circularWidth))
                    {
                        //设置起止点线帽
                        //p.StartCap = LineCap.Round;
                        //p.EndCap = LineCap.Round;
                        //设置连续两段的联接样式
                        p.LineJoin = LineJoin.Round;
                        e.Graphics.DrawArc(p, new Rectangle(new Point(e.ClipRectangle.X + circularWidth / 2, e.ClipRectangle.Y + circularWidth / 2), new Size(e.ClipRectangle.Width - 1 - circularWidth, e.ClipRectangle.Height - 1 - circularWidth)), 45, (float)((float)this.value * 3.6));
                    }
                }
                if (this.value == 100)
                {
                    SizeF size = e.Graphics.MeasureString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold));
                    e.Graphics.DrawString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.White), new Point(this.Width / 2 - (int)size.Width / 2 - 1, this.Height / 2 - (int)size.Height / 2 + 2));
                    e.Graphics.DrawString("%", new Font("华文新魏", 9F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.White), new Point(this.Width / 2 + (int)size.Width / 2 - 8, this.Height / 2 - (int)size.Height + 8));
                }
                else
                {
                    SizeF size = e.Graphics.MeasureString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold));
                    e.Graphics.DrawString(this.value.ToString(), new Font("黑体", 15F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.DimGray), new Point(this.Width / 2 - (int)size.Width / 2 - 1, this.Height / 2 - (int)size.Height / 2 + 2));
                    e.Graphics.DrawString("%", new Font("华文新魏", 9F, System.Drawing.FontStyle.Bold), new SolidBrush(Color.DimGray), new Point(this.Width / 2 + (int)size.Width / 2 - 8, this.Height / 2 - (int)size.Height + 8));
                }
            }
            catch { }
        }
    }
    public class CircularProgressBarEx : Control
    {
        private CircularProgressBar circularProgressBar = new CircularProgressBar();
        private string completeText = "";
        private string doingText = "";

        public int Value
        {
            get
            {
                return this.circularProgressBar.Value;
            }
            set
            {
                this.circularProgressBar.Value = value;
                this.Invalidate();
            }
        }

        public Color Color
        {
            get
            {
                return this.circularProgressBar.MainColor;
            }
            set
            {
                this.circularProgressBar.MainColor = value;
                this.Invalidate();
            }
        }

        [LocalizableAttribute(true)]
        public string CompleteText
        {
            get
            {
                return completeText;
            }
            set
            {
                completeText = value;
                this.Invalidate();
            }
        }

        [LocalizableAttribute(true)]
        public string DoingText
        {
            get
            {
                return doingText;
            }
            set
            {
                doingText = value;
                this.Invalidate();
            }
        }

        public CircularProgressBarEx()
        {
            //指定控件的样式和行为
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.ContainerControl |
                ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.UpdateStyles();
            this.BackColor = Color.White;
            circularProgressBar.Dock = DockStyle.Top;
            this.Controls.Add(circularProgressBar);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Value == 100)
            {
                SizeF size = e.Graphics.MeasureString(this.completeText, new Font("微软雅黑", 12F));
                e.Graphics.DrawString(this.completeText, new Font("微软雅黑", 12F), new SolidBrush(Color.Gray), new Point(this.Width / 2 - (int)size.Width / 2 - 1, this.Height - (int)size.Height - 5));
            }
            else
            {
                SizeF size = e.Graphics.MeasureString(this.doingText, new Font("微软雅黑", 12F));
                e.Graphics.DrawString(this.doingText, new Font("微软雅黑", 12F), new SolidBrush(Color.Gray), new Point(this.Width / 2 - (int)size.Width / 2 - 1, this.Height - (int)size.Height - 5));
            }

        }
    }

}
