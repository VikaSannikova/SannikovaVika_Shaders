using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace SannikovaVika_Shaders
{
    public partial class Form1 : Form
    {

        int BasicProgramID;
        int BasicVertexShader;
        int BasicFragmentShader;
        int vaoHandle;
        public Form1()
        {
            InitializeComponent();
        }
        //   // string glVersion = GL.GetString(StringName.Version);
        //   // string glslVersion = GL.GetString(StringName.ShadingLanguageVersion);

        void loadShader(String filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (System.IO.StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        private void InitShaders()
        {
            // создание объекта программы
            BasicProgramID = GL.CreateProgram();
            loadShader("C:\\Users\\Виктория\\Documents\\Visual Studio 2015\\Projects\\SannikovaVika_Shaders\\basic.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader("C:\\Users\\Виктория\\Documents\\Visual Studio 2015\\Projects\\SannikovaVika_Shaders\\basic.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            //Компановка программы
            GL.LinkProgram(BasicProgramID);
            // Проверить успех компановки
            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

            float[] positionData = { -0.8f, -0.8f, 0.0f, 0.8f, -0.8f, 0.0f, 0.0f, 0.8f, 0.0f }; //хранятся параметры вершин
            float[] colorData = { 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f };

            int[] vboHandlers = new int[2]; //

            GL.GenBuffers(2, vboHandlers); //создание двух буферных объектов
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * positionData.Length), positionData, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[1]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * colorData.Length), colorData, BufferUsageHint.StaticDraw);

            vaoHandle = GL.GenVertexArray();

            GL.BindVertexArray(vaoHandle);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[0]);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0); //здесь фигня
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[1]);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
        }


        private void Draw()
        {
            GL.UseProgram(BasicProgramID);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            InitShaders();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.UseProgram(BasicProgramID);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }
    }
}










//public partial class Form1 : Form
//    {
//        int BasicProgramID;
//        int BasicVertexShader;
//        int BasicFragmentShader;
//        int vaoHandle;

//        public Form1()
//        {
//            InitializeComponent();
//        }

//        void loadShader(String filename, ShaderType type, int program, out int address)//создает объект шейдера
//        {
//            address = GL.CreateShader(type);
//            using (System.IO.StreamReader sr = new StreamReader(filename))
//            {
//                GL.ShaderSource(address, sr.ReadToEnd());
//            }
//            GL.CompileShader(address);
//            GL.AttachShader(program, address);
//            Console.WriteLine(GL.GetShaderInfoLog(address));
//        }

//        private void InitShaders()
//        {
//            // создание объекта программы
//            BasicProgramID = GL.CreateProgram();
//            loadShader("C:\\Users\\Виктория\\Documents\\Visual Studio 2015\\Projects\\SannikovaVika_Shaders\\basic.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
//            loadShader("C:\\Users\\Виктория\\Documents\\Visual Studio 2015\\Projects\\SannikovaVika_Shaders\\basic.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
//            //Компановка программы
//            GL.LinkProgram(BasicProgramID);
//            // Проверить успех компановки
//            int status = 0;
//            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);
//            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

//            float[] positionData = { -0.8f, -0.8f, 0.0f, 0.8f, -0.8f, 0.0f, 0.0f, 0.8f, 0.0f };
//            float[] colorData = { 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f };

//            int[] vboHandlers = new int[2];

//            GL.GenBuffers(2, vboHandlers);
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[0]);
//            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * positionData.Length),
//                                                 positionData, BufferUsageHint.StaticDraw);
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[1]);
//            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * colorData.Length),
//                                                colorData, BufferUsageHint.StaticDraw);

//            vaoHandle = GL.GenVertexArray();
//            GL.BindVertexArray(vaoHandle);
//            GL.EnableVertexAttribArray(0);
//            GL.EnableVertexAttribArray(1);

//            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[0]);
//            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
//            GL.BindBuffer(BufferTarget.ArrayBuffer, vboHandlers[1]);
//            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);

//        }

//        private void Draw()
//        {
//            GL.UseProgram(BasicProgramID);
//            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
//            glControl1.SwapBuffers();
//            GL.UseProgram(0);
//        }

//        private void glControl1_Load(object sender, EventArgs e)
//        {
//            InitShaders();
//        }

//        private void glControl1_Paint(object sender, PaintEventArgs e)
//        {
//            InitShaders();
//            Draw();
//        }
//   }




