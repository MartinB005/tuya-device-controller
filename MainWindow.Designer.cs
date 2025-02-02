namespace Room_light
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.bright_slider = new Guna.UI2.WinForms.Guna2TrackBar();
            this.light_select = new Guna.UI2.WinForms.Guna2ComboBox();
            this.power = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2ImageButton1 = new Guna.UI2.WinForms.Guna2ImageButton();
            this.temp_icon = new Guna.UI2.WinForms.Guna2ImageButton();
            this.temp_slider = new Guna.UI2.WinForms.Guna2TrackBar();
            this.settings = new Guna.UI2.WinForms.Guna2ImageButton();
            this.SuspendLayout();
            // 
            // bright_slider
            // 
            this.bright_slider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.bright_slider.HoverState.Parent = this.bright_slider;
            this.bright_slider.Location = new System.Drawing.Point(66, 63);
            this.bright_slider.Name = "bright_slider";
            this.bright_slider.Size = new System.Drawing.Size(264, 23);
            this.bright_slider.TabIndex = 0;
            this.bright_slider.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(156)))), ((int)(((byte)(227)))));
            this.bright_slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bright_slider_MouseUp);
            // 
            // light_select
            // 
            this.light_select.BackColor = System.Drawing.Color.Transparent;
            this.light_select.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.light_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.light_select.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.light_select.FocusedColor = System.Drawing.Color.Empty;
            this.light_select.FocusedState.Parent = this.light_select;
            this.light_select.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.light_select.ForeColor = System.Drawing.Color.White;
            this.light_select.FormattingEnabled = true;
            this.light_select.HoverState.Parent = this.light_select;
            this.light_select.ItemHeight = 30;
            this.light_select.ItemsAppearance.Parent = this.light_select;
            this.light_select.Location = new System.Drawing.Point(66, 12);
            this.light_select.Name = "light_select";
            this.light_select.ShadowDecoration.Parent = this.light_select;
            this.light_select.Size = new System.Drawing.Size(227, 36);
            this.light_select.TabIndex = 1;
            this.light_select.SelectedIndexChanged += new System.EventHandler(this.light_select_SelectedIndexChanged);
            // 
            // power
            // 
            this.power.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.power.CheckedState.Image = ((System.Drawing.Image)(resources.GetObject("power.CheckedState.Image")));
            this.power.CheckedState.ImageSize = new System.Drawing.Size(30, 30);
            this.power.CheckedState.Parent = this.power;
            this.power.Cursor = System.Windows.Forms.Cursors.Hand;
            this.power.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("power.HoverState.Image")));
            this.power.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.power.HoverState.Parent = this.power;
            this.power.Image = ((System.Drawing.Image)(resources.GetObject("power.Image")));
            this.power.ImageSize = new System.Drawing.Size(30, 30);
            this.power.Location = new System.Drawing.Point(2, 3);
            this.power.Name = "power";
            this.power.PressedState.ImageSize = new System.Drawing.Size(25, 25);
            this.power.PressedState.Parent = this.power;
            this.power.Size = new System.Drawing.Size(58, 48);
            this.power.TabIndex = 2;
            this.power.Click += new System.EventHandler(this.power_Click);
            // 
            // guna2ImageButton1
            // 
            this.guna2ImageButton1.CheckedState.Parent = this.guna2ImageButton1;
            this.guna2ImageButton1.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2ImageButton1.HoverState.Parent = this.guna2ImageButton1;
            this.guna2ImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2ImageButton1.Image")));
            this.guna2ImageButton1.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2ImageButton1.Location = new System.Drawing.Point(12, 54);
            this.guna2ImageButton1.Name = "guna2ImageButton1";
            this.guna2ImageButton1.PressedState.Parent = this.guna2ImageButton1;
            this.guna2ImageButton1.Size = new System.Drawing.Size(54, 40);
            this.guna2ImageButton1.TabIndex = 3;
            // 
            // temp_icon
            // 
            this.temp_icon.CheckedState.Parent = this.temp_icon;
            this.temp_icon.HoverState.ImageSize = new System.Drawing.Size(30, 30);
            this.temp_icon.HoverState.Parent = this.temp_icon;
            this.temp_icon.Image = ((System.Drawing.Image)(resources.GetObject("temp_icon.Image")));
            this.temp_icon.ImageSize = new System.Drawing.Size(30, 30);
            this.temp_icon.Location = new System.Drawing.Point(12, 97);
            this.temp_icon.Name = "temp_icon";
            this.temp_icon.PressedState.Parent = this.temp_icon;
            this.temp_icon.Size = new System.Drawing.Size(54, 40);
            this.temp_icon.TabIndex = 4;
            // 
            // temp_slider
            // 
            this.temp_slider.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(207)))));
            this.temp_slider.HoverState.Parent = this.temp_slider;
            this.temp_slider.Location = new System.Drawing.Point(66, 100);
            this.temp_slider.Name = "temp_slider";
            this.temp_slider.Size = new System.Drawing.Size(264, 23);
            this.temp_slider.TabIndex = 5;
            this.temp_slider.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(156)))), ((int)(((byte)(227)))));
            this.temp_slider.DragLeave += new System.EventHandler(this.temp_slider_DragLeave);
            this.temp_slider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.temp_slider_MouseUp);
            // 
            // settings
            // 
            this.settings.CheckedState.Parent = this.settings;
            this.settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settings.HoverState.ImageSize = new System.Drawing.Size(25, 25);
            this.settings.HoverState.Parent = this.settings;
            this.settings.Image = ((System.Drawing.Image)(resources.GetObject("settings.Image")));
            this.settings.ImageSize = new System.Drawing.Size(25, 25);
            this.settings.Location = new System.Drawing.Point(303, 12);
            this.settings.Name = "settings";
            this.settings.PressedState.Parent = this.settings;
            this.settings.Size = new System.Drawing.Size(40, 36);
            this.settings.TabIndex = 6;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(349, 135);
            this.ControlBox = false;
            this.Controls.Add(this.settings);
            this.Controls.Add(this.temp_slider);
            this.Controls.Add(this.temp_icon);
            this.Controls.Add(this.guna2ImageButton1);
            this.Controls.Add(this.power);
            this.Controls.Add(this.light_select);
            this.Controls.Add(this.bright_slider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(900, 1600);
            this.Name = "MainWindow";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TrackBar bright_slider;
        private Guna.UI2.WinForms.Guna2ComboBox light_select;
        private Guna.UI2.WinForms.Guna2ImageButton power;
        private Guna.UI2.WinForms.Guna2ImageButton guna2ImageButton1;
        private Guna.UI2.WinForms.Guna2ImageButton temp_icon;
        private Guna.UI2.WinForms.Guna2TrackBar temp_slider;
        private Guna.UI2.WinForms.Guna2ImageButton settings;
    }
}

