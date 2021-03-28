using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamGameCopy
{
    public partial class fMain : Form
    {
        internal struct Game
        {
            public string name;
            public string gamePath;
            public string configPath;
        }
        /// <summary>
        /// Default Path to SteamLibrary
        /// </summary>
        const string steamDefaultPath = @"C:\Program Files (x86)\Steam\steamapps";
        internal string steamPath;
        internal List<Game> lGames = new List<Game>();
        internal int[] simultaniousZippings = new int[5];
        internal CancellationTokenSource cts = new CancellationTokenSource();
        internal Dictionary<string, CancellationTokenSource> lCancel = new Dictionary<string, CancellationTokenSource>();

        public fMain()
        {
            InitializeComponent();
            lblStatus.Text = "Loading...";
            if (String.IsNullOrEmpty(steamPath))
            {
                steamPath = steamDefaultPath;
            }
            try
            {
                getGames();
                setCombobox();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Please define steamPath in the Properties!";
            }
            btnCopy.BackColor = Color.Black;
            btnShow.BackColor = Color.Black;
            btnImport.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            cbxGames.ForeColor = Color.White;
            menuStrip1.Renderer = new BlackRenderer();
        }
        #region Methods
        private void getGames()
        {
            List<string> lHold = Directory.GetDirectories(steamPath + @"\common").ToList();
            foreach (string s in lHold)
            {
                string directoryName = Path.GetFullPath(s).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Last();
                lGames.Add(new Game() { name = getGameName(directoryName), gamePath = s, configPath = getConfigPath(directoryName) });
            }
            lGames.RemoveAll(x => String.IsNullOrEmpty(x.configPath));
            lblStatus.Text = lGames.Count + " Games found in "+steamPath;
        }
        private string getConfigPath(string name)
        {
            string ret = "";

            foreach (string f in Directory.GetFiles(steamPath))
            {
                if (Path.GetExtension(f) == ".acf")
                {
                    string line = File.ReadLines(f).Skip(6).Take(1).First();

                    if (line.Substring(16, line.LastIndexOf("\"") - 16).Equals(name))
                    {
                        ret = f;
                    }
                }
            }
            return ret;
        }
        private string getGameName(string name)
        {
            string ret = "";
            foreach (string f in Directory.GetFiles(steamPath))
            {
                if (Path.GetExtension(f) == ".acf")
                {
                    string line = File.ReadLines(f).Skip(6).Take(1).First();

                    if (line.Substring(16, line.LastIndexOf("\"") - 16).Equals(name))
                    {
                        ret = "";
                        int startIndex = File.ReadAllText(f).IndexOf("name");
                        int endIndex = File.ReadAllText(f).IndexOf("StateFlags");
                        ret = File.ReadAllText(f).Substring(startIndex + 8, endIndex - startIndex - 12);
                    }
                }
            }
            return ret;
        }
        private void setCombobox()
        {
            foreach (Game g in lGames)
            {
                cbxGames.Items.Add(g.name);
            }
        }
        private async void copyToDesktop()
        {
            string _name = (string)cbxGames.SelectedItem;
            string startPath = lGames.Find(x => x.name == _name).gamePath;
            string zipPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), lGames.Find(x => x.name == _name).name.Replace(":", "_") + ".zip");

            if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).Contains(zipPath))
            {
                lblStatus.Text = "Archive already existing!";
                return;
            }
            #region print Layout
            int index = Array.IndexOf(simultaniousZippings, 0);
            if (index == -1)
            {
                lblStatus.Text = "Please wait till the queue is empty.";
                return;
            }
            simultaniousZippings[index] = 1;
            if (index >= 5)
            {
                this.Height += 25;
                this.Refresh();
            }
            Label l = new Label();
            l.AutoSize = true;
            l.Text = cbxGames.Text;
            l.ForeColor = Color.White;
            ProgressBar p = new ProgressBar();
            p.Name = "pb" + index;
            ModifyProgressBarColor.SetState(p, 2);
            l.Location = new Point(110, 100 + (25 * index) + 10);
            p.Location = new Point(10, 100 + (25 * index) + 5);
            p.Show();
            p.Visible = true;
            p.Value = 0;
            p.MouseClick += new MouseEventHandler(cancelProgress);
            this.Controls.Add(p);
            this.Controls.Add(l);
            #endregion
            lblStatus.Text = "Creating Zip-File...";
            CancellationTokenSource cts = new CancellationTokenSource();
            if (lCancel.ContainsKey(p.Name))
            {
                lCancel[p.Name] = cts;
            }
            else
            {
                lCancel.Add(p.Name, cts);
            }
            await Task.Factory.StartNew(() => createZipArchive(startPath, zipPath, p, l, _name, cts.Token));
            lblStatus.Text = "Adding acf-File...";
            await Task.Factory.StartNew(() => addConfigToZipArchive(zipPath, _name, cts.Token));
            lblStatus.Text = lGames.Count + " Games found in " + steamPath;
            #region Recreate Layout
            if (index >= 5)
            {
                this.Height -= 25;
                this.Refresh();
            }
            this.Controls.Remove(p);
            this.Controls.Remove(l);
            #endregion
            simultaniousZippings[index] = 0;
            if(cts.IsCancellationRequested)
            {
                File.Delete(zipPath);
            }
        }
        private void createZipArchive(string startPath, string zipPath, ProgressBar progressBar, Label l, string _name, CancellationToken token)
        {
             ZipFileWithProgress.CreateFromDirectory(startPath, zipPath, new BasicProgress<double>(p => lblStatus.Invoke(new Action(() =>
             {
                 if(token.IsCancellationRequested)
                 {
                     l.Text = "Aborting...";
                     return;
                 }
                 progressBar.Maximum = 100;
                 progressBar.Step = 1;
                 int hold = (int)(p * 100);
                 progressBar.Value = hold;
                 l.Text = _name + " || " + p.ToString("P2");
             }))), token);
        }
        private void addConfigToZipArchive(string zipPath, string _name, CancellationToken token)
        {
            using (var zip = Ionic.Zip.ZipFile.Read(zipPath))
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                zip.AddFile(lGames.Find(x => x.name == _name).configPath, @"\");
                if (token.IsCancellationRequested)
                {
                    return;
                }
                zip.Save();
            }
        }
        /// <summary>
        /// Reloads the Programs and Clears Lists
        /// </summary>
        private void reload()
        {
            lGames.Clear();
            cbxGames.Items.Clear();
            getGames();
            setCombobox();
        }
        #endregion
        #region Event Methods
        /// <summary>
        /// ButtonClick Event for Copy Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyToDesktop();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty((string)cbxGames.SelectedItem))
            {
                Process.Start(lGames.Find(x => x.name == (string)cbxGames.SelectedItem).gamePath);
            }
        }
        private void cbxGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(lGames.Find(x => x.name == (string)cbxGames.SelectedItem).gamePath);
            lblStatus.Text = di.DirSize().FormatBytes() + " in " + di.FullName;
            toolTip1.SetToolTip(lblStatus, lblStatus.Text);
        }
        private void warteschlangengrößeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!simultaniousZippings.Contains(1))
            {
                int size = 5;
                if (Int32.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Please enter you desired Queuesize", "Queuesize", ""), out size))
                {
                    simultaniousZippings = new int[size];
                }
            }
        }
        private void steamlibraryPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;

            bool correctPath = false;
            while (!correctPath)
            {
                DialogResult dr = fbd.ShowDialog();
                if (dr == DialogResult.Cancel) { correctPath = true; return; }
                try
                {
                    if (Path.GetFullPath(fbd.SelectedPath).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar).Last().Equals("steamapps"))
                    {
                        correctPath = true;
                        steamPath = fbd.SelectedPath;
                        reload();
                    }
                    else
                    {
                        MessageBox.Show("Please select the folder steamapps in your Steam Library Folder.");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
        private void cancelProgress(object sender, MouseEventArgs e)
        {
            lCancel[(sender as Control).Name].Cancel();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            importInLib();
        }
        #endregion

       

        private void importInLib()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Game(s) to import";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Zip Archive (*.zip) | *.zip;";
            openFileDialog.ShowDialog();
            try
            {
                foreach (string s in openFileDialog.FileNames)
                {
                    ZipFileWithProgress.ExtractToDirectory(s, Path.Combine(steamPath, "common"), null);
                    string configFile = "";
                    foreach (string cf in Directory.GetFiles(Path.Combine(steamPath, "common"), "*.acf"))
                    {
                        configFile = cf;
                    }
                    string debug = Path.Combine(steamPath, Path.GetFileName(configFile));
                    File.Move(configFile, debug);
                }
                lblStatus.Text = openFileDialog.FileNames.Length + " Game(s) imported!";
            }
            catch(Exception exc)
            {
                lblStatus.Text = "Error while importing!";
            }
        }
    }
}