namespace SteamGameCopy
{
    partial class fMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.cbxGames = new System.Windows.Forms.ComboBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.eigenschaftenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warteschlangengrößeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamlibraryPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblGame = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxGames
            // 
            this.cbxGames.BackColor = System.Drawing.Color.Black;
            this.cbxGames.FormattingEnabled = true;
            this.cbxGames.Location = new System.Drawing.Point(12, 27);
            this.cbxGames.Name = "cbxGames";
            this.cbxGames.Size = new System.Drawing.Size(248, 21);
            this.cbxGames.TabIndex = 0;
            this.cbxGames.SelectedIndexChanged += new System.EventHandler(this.cbxGames_SelectedIndexChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(12, 54);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(123, 23);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Export auf Desktop";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(306, 54);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(123, 23);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "Ordner anzeigen";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 80);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eigenschaftenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(442, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // eigenschaftenToolStripMenuItem
            // 
            this.eigenschaftenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warteschlangengrößeToolStripMenuItem,
            this.steamlibraryPathToolStripMenuItem});
            this.eigenschaftenToolStripMenuItem.Name = "eigenschaftenToolStripMenuItem";
            this.eigenschaftenToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.eigenschaftenToolStripMenuItem.Text = "Eigenschaften";
            // 
            // warteschlangengrößeToolStripMenuItem
            // 
            this.warteschlangengrößeToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.warteschlangengrößeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.warteschlangengrößeToolStripMenuItem.Name = "warteschlangengrößeToolStripMenuItem";
            this.warteschlangengrößeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.warteschlangengrößeToolStripMenuItem.Text = "Warteschlangengröße";
            this.warteschlangengrößeToolStripMenuItem.Click += new System.EventHandler(this.warteschlangengrößeToolStripMenuItem_Click);
            // 
            // steamlibraryPathToolStripMenuItem
            // 
            this.steamlibraryPathToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.steamlibraryPathToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.steamlibraryPathToolStripMenuItem.Name = "steamlibraryPathToolStripMenuItem";
            this.steamlibraryPathToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.steamlibraryPathToolStripMenuItem.Text = "SteamlibraryPath";
            this.steamlibraryPathToolStripMenuItem.Click += new System.EventHandler(this.steamlibraryPathToolStripMenuItem_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(159, 54);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(123, 23);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "Import in Bibliothek";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblGame
            // 
            this.lblGame.AutoSize = true;
            this.lblGame.Location = new System.Drawing.Point(266, 30);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(123, 13);
            this.lblGame.TabIndex = 5;
            this.lblGame.Text = "<-- Bitte Spiel auswählen";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(442, 239);
            this.Controls.Add(this.lblGame);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.cbxGames);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "fMain";
            this.ShowIcon = false;
            this.Text = "SteamGameCopyHelper";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxGames;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eigenschaftenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warteschlangengrößeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem steamlibraryPathToolStripMenuItem;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

