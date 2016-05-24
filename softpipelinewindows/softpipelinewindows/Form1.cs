using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softpipelinewindows
{
    public partial class Form1 : Form
    {
        private IOrthoGraphicCamera camera;
        private IMesh mesh;
        private IScene scene;
        private IApplicationStage app;
        private Pipeline pipeline;
        private Vector4[,] renderbuffer;

        private Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitPipe();
            UpdateRender();
        }

        private void InitPipe()
        {
            camera = new OrthoGraphicCamera();
            camera.position = new Vector3(0, 0, 0);
            camera.near = -100;
            camera.far = 100;
            camera.size = 4;
            camera.aspect = 1;
            mesh = new Mesh();
            mesh.position = new Vector3(0, 0, 0);
            mesh.rotation = Quaternion.Eulers(0, 0, 0);
            mesh.scale = new Vector3(2, 2, 2);
            mesh.vertices = new Vector3[] 
            { 
                new Vector3(-4, 0, 0),
                new Vector3(0, 3, 0),
                new Vector3(4, 0, 0)
            };
            mesh.normals = new Vector3[]
            {
                new Vector3(0, 0, -1),
                new Vector3(0, 0, -1),
                new Vector3(0, 0, -1)
            };
            mesh.indices = new int[] 
            { 
                0, 1, 2
            };
            scene = new Scene();
            scene.camera = camera;
            scene.meshes = new IMesh[]
            {
                mesh
            };
            app = new ApplicationStage();
            pipeline = new Pipeline(pictureBox1.Width, pictureBox1.Height);

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
        }

        private void UpdateRender()
        {
            IDrawCall[] drawCalls = app.Process(scene);
            renderbuffer = pipeline.Process(drawCalls, camera);
            for (int y = 0; y < renderbuffer.GetLength(1); y++)
            {
                int yr = pictureBox1.Height - 1 - y;
                for (int x = 0; x < renderbuffer.GetLength(0); x++)
                {
                    Vector4 oc = renderbuffer[x, y];
                    Color c = Color.FromArgb((int)(oc.x * 255), (int)(oc.y * 255), (int)(oc.z * 255));
                    bmp.SetPixel(x, yr, c);
                }
            }
            pictureBox1.Invalidate();
        }

        private void rotateX_Click(object sender, EventArgs e)
        {
            mesh.rotation *= Quaternion.Eulers(10, 0, 0);
            UpdateRender();
        }

        private void rotateY_Click(object sender, EventArgs e)
        {
            mesh.rotation *= Quaternion.Eulers(0, 10, 0);
            UpdateRender();
        }

        private void rotateZ_Click(object sender, EventArgs e)
        {
            mesh.rotation *= Quaternion.Eulers(0, 0, 10);
            UpdateRender();
        }
    }
}
