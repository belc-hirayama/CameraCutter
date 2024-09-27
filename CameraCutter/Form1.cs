using CameraCutter.camera;
using CameraCutter.control;
using CameraCutter.imaeg;
using System.ComponentModel;
using System.Diagnostics;

namespace CameraCutter
{
    public partial class Form1 : Form
    {
        private WebCamera camera;
        private Graphics graphics;
        private List<Panel> rectFrame = [];
        public Form1()
        {
            InitializeComponent();
            camera = new WebCamera();
            graphics = pictureBox1.CreateGraphics();
            pictureBox1.Size = new(camera.Width, camera.Height);
            
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;

            rectFrame = TransParentRectangle.GetRectanglePanels(new Point(pictureBox1.Width/4, 0), new Size(pictureBox1.Width / 2, pictureBox1.Height), 2, Color.Blue);
            pictureBox1.Controls.AddRange([..rectFrame]);


            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object? sender, FormClosedEventArgs e)
        {
            backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy) Application.DoEvents();
        }

        private void BackgroundWorker1_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            graphics.DrawImage(camera.bmp, 0, 0, camera.Cols, camera.Rows);
            foreach (var panel in rectFrame)
                panel.BringToFront();
        }

        private void BackgroundWorker1_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker? bw = (BackgroundWorker?)sender;
            while (!backgroundWorker1.CancellationPending)
            {
                camera.Grab();
                bw.ReportProgress(0);
                System.Threading.Thread.Sleep(30);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var png = camera.GetImage();
            var img = ImageEditor.Trim(png, camera.Width / 4, 0, camera.Width * 3 / 4, camera.Height);
            ImageEditor.Save(img);
            this.Close();
        }
    }
}
