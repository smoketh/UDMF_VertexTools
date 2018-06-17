using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using System.IO;
using System.Diagnostics;

namespace UDMF_VertexTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sw = new Stopwatch();
            gl = this.openGLControl1.OpenGL;
        }
        Stopwatch sw; 

        UDMFConverted OurMap;
        OpenGL gl;
        int PassedCount = 0;
        bool determinePass = true;
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        Vector3 center;
        float offsetDistance = 0f;
        void GLVertexV3(Vector3 vertex)
        {
            gl.Vertex(vertex.x, vertex.y, vertex.z);
            if(determinePass)
            {
                if (vertex.x < min.x) min.x = vertex.x;
                if (vertex.y < min.y) min.y = vertex.y;
                if (vertex.z < min.z) min.z = vertex.z;

                if (vertex.x > max.x) max.x = vertex.x;
                if (vertex.y > max.y) max.y = vertex.y;
                if (vertex.z > max.z) max.z = vertex.z;
            }


            PassedCount++;
        }

        float angle = 0;
        float deltaTime = 1f;
        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object, just to clean up the code.
            PassedCount = 0;
            //OpenGL gl = this.openGLControl1.OpenGL;
            sw.Start();


            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);	// Clear The Screen And The Depth Buffer
           

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();                  // Reset The View
            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)openGLControl1.Width / (double)openGLControl1.Height, 0.01, 25000.0);
            //  Use the 'look at' helper function to position and aim the camera.
            float vX = offsetDistance* (float)Math.Cos(angle * Vector3.Deg2Rad);
            float vZ = offsetDistance * (float)Math.Sin(angle * Vector3.Deg2Rad);

            gl.LookAt(center.x+vX,
                      center.y,
                      center.z+vZ,
                      center.x, center.y, center.z,
                        0, 1, 0);
            
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            //gl.Translate(-1.5f, 0.0f, -6.0f);

            gl.Begin(OpenGL.GL_TRIANGLES);					// Start Drawing The Pyramid

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Front)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Left Of Triangle (Front)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Right Of Triangle (Front)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Right)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Left Of Triangle (Right)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Right Of Triangle (Right)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Back)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Left Of Triangle (Back)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Right Of Triangle (Back)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Left)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Left Of Triangle (Left)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Right Of Triangle (Left)
            gl.End();						// Done Drawing The Pyramid

            gl.LineWidth(2f);
            if (OurMap != null)
            { 
                gl.Color(1.0f, 1.0f, 1.0f, 1.0f);
                //gl.Translate((double)DistanceValX.Value, (double)DistanceValY.Value, (double)DistanceValZ.Value);
                gl.Begin(OpenGL.GL_LINES);

                for (int q = 0; q < OurMap.lineDefs.Count; q++)
                {
                    Linedef ld = OurMap.lineDefs[q];

                    VertexDef v1 = OurMap.vertices[ld.v1],
                              v2 = OurMap.vertices[ld.v2];



                    Vector3 ldFrontBottomV1 = new Vector3(v1.position.x, 0, v1.position.y);
                    Vector3 ldBackBottomV1 = ldFrontBottomV1;
                    Vector3 ldFrontBottomV2 = new Vector3(v2.position.x, 0, v2.position.y);
                    Vector3 ldBackBottomV2 = ldFrontBottomV2;
                    if (v1.floorOffset != null)
                    {
                        ldFrontBottomV1.y = ldBackBottomV1.y = v1.floorOffset.Value;
                    }
                    else
                    {
                        if(ld.sideBack != -1)
                        {
                            ldBackBottomV1.y = OurMap.sectors[OurMap.sideDefs[ld.sideBack].sector].heightFloor;
                        }
                        if(ld.sideFront != -1)
                        {
                            ldFrontBottomV1.y = OurMap.sectors[OurMap.sideDefs[ld.sideFront].sector].heightFloor;
                        }
                    }


                    if (v2.floorOffset != null)
                    {
                        ldFrontBottomV2.y = ldBackBottomV2.y = v2.floorOffset.Value;
                    }
                    else
                    {
                        if(ld.sideBack!=-1)
                        {
                            ldBackBottomV2.y = OurMap.sectors[OurMap.sideDefs[ld.sideBack].sector].heightFloor;
                        }
                        if(ld.sideFront!=-1)
                        {
                            ldFrontBottomV2.y = OurMap.sectors[OurMap.sideDefs[ld.sideFront].sector].heightFloor;
                        }
                    }

                    Vector3 ldFrontTopV1 = new Vector3(v1.position.x, 0, v1.position.y);
                    Vector3 ldBackTopV1 = ldFrontTopV1;
                    Vector3 ldFrontTopV2 = new Vector3(v2.position.x, 0, v2.position.y);
                    Vector3 ldBackTopV2 = ldFrontTopV2;

                    if (v1.ceilingOffset != null)
                    {
                        ldFrontTopV1.y = ldBackTopV1.y = v1.ceilingOffset.Value;
                    }
                    else
                    {
                        if(ld.sideBack!=-1)
                        {
                            ldBackTopV1.y = OurMap.sectors[OurMap.sideDefs[ld.sideBack].sector].heightCeiling;
                        }
                        if(ld.sideFront!=-1)
                        {
                            ldFrontTopV1.y = OurMap.sectors[OurMap.sideDefs[ld.sideFront].sector].heightCeiling;
                        }
                    }


                    if (v2.ceilingOffset != null)
                    {
                        ldFrontTopV2.y = ldBackTopV2.y = v2.ceilingOffset.Value;
                    }
                    else
                    {
                        if(ld.sideBack!=-1)
                        {
                            ldBackTopV2.y = OurMap.sectors[OurMap.sideDefs[ld.sideBack].sector].heightCeiling;
                        }
                        if(ld.sideFront!=-1)
                        {
                            ldFrontTopV2.y = OurMap.sectors[OurMap.sideDefs[ld.sideFront].sector].heightCeiling;
                        }
                    }



                    if (ld.sideBack != -1)
                    {
                        gl.Color(0.3f, 1.0f, 0.3f, 1.0f);

                        if(RB_Copy_FtC.Checked)
                        {
                            ldBackTopV1.y = ldBackBottomV1.y;
                            ldBackTopV2.y = ldBackBottomV2.y;
                        }
                        else if (RB_Copy_CtF.Checked)
                        {
                            ldBackBottomV1.y = ldBackTopV1.y;
                            ldBackBottomV2.y = ldBackTopV2.y;
                        }
                        else if (RB_Swap.Checked )
                        {
                            float tB =ldBackBottomV1.y;
                            ldBackBottomV1.y = ldBackTopV1.y;
                            ldBackTopV1.y = tB;

                            tB = ldBackBottomV2.y;
                            ldBackBottomV2.y = ldBackTopV2.y;
                            ldBackTopV2.y = tB;

                        }


                        ldBackTopV1.y = ldBackTopV1.y * ((float)CeilingMult.Value) + ((float)CeilingOffs.Value);
                        ldBackTopV2.y = ldBackTopV2.y * ((float)CeilingMult.Value) + ((float)CeilingOffs.Value);
                        ldBackBottomV1.y = ldBackBottomV1.y * ((float)FloorMult.Value) + ((float)FloorOffs.Value);
                        ldBackBottomV2.y = ldBackBottomV2.y * ((float)FloorMult.Value) + ((float)FloorOffs.Value);

                        GLVertexV3(ldBackBottomV1);
                        GLVertexV3(ldBackBottomV2);

                        gl.Color(.3f, .3f, 1.0f, 1.0f);
                        
                        GLVertexV3(ldBackTopV1);
                        GLVertexV3(ldBackTopV2);

                    }

                    if (ld.sideFront != -1)
                    {
                        if (RB_Copy_FtC.Checked)
                        {
                            ldFrontTopV1.y = ldFrontBottomV1.y;
                            ldFrontTopV2.y = ldFrontBottomV2.y;
                        }
                        else if (RB_Copy_CtF.Checked)
                        {
                            ldFrontBottomV1.y = ldFrontTopV1.y;
                            ldFrontBottomV2.y = ldFrontTopV2.y;
                        }
                        else if (RB_Swap.Checked)
                        {
                            float tB = ldFrontBottomV1.y;
                            ldFrontBottomV1.y = ldFrontTopV1.y;
                            ldFrontTopV1.y = tB;

                            tB = ldFrontBottomV2.y;
                            ldFrontBottomV2.y = ldFrontTopV2.y;
                            ldFrontTopV2.y = tB;

                        }

                        ldFrontTopV1.y = ldFrontTopV1.y * ((float)CeilingMult.Value) + ((float)CeilingOffs.Value);
                        ldFrontTopV2.y = ldFrontTopV2.y * ((float)CeilingMult.Value) + ((float)CeilingOffs.Value);
                        ldFrontBottomV1.y = ldFrontBottomV1.y * ((float)FloorMult.Value) + ((float)FloorOffs.Value);
                        ldFrontBottomV2.y = ldFrontBottomV2.y * ((float)FloorMult.Value) + ((float)FloorOffs.Value);

                        gl.Color(0.3f, 1.0f, 0.3f, 1.0f);

                        GLVertexV3(ldFrontBottomV2);
                        GLVertexV3(ldFrontBottomV1);



                        gl.Color(0.3f, 0.3f, 1.0f, 1.0f);

                        GLVertexV3(ldFrontTopV2);
                        GLVertexV3(ldFrontTopV1);


                    }



                    if(q==0 && !threw)
                    {
                        threw = true;
                        Console.WriteLine(ldFrontTopV1);
                        Console.WriteLine(ldFrontTopV2);
                        Console.WriteLine(ldBackTopV1);
                        Console.WriteLine(ldBackTopV2);
                        Console.WriteLine(ldFrontBottomV1);
                        Console.WriteLine(ldFrontBottomV2);
                        Console.WriteLine( ldBackBottomV1);
                        Console.WriteLine( ldBackBottomV2);
                    }

                }
                gl.End();
                
            }
            
            if(threw && determinePass)
            {
                Console.WriteLine("Determined?");
                
                determinePass = false;
                //DistanceValY.Value =(decimal) ( min.y + (max.y - min.y) / 2f);
                //DistanceValX.Value = (decimal)(min.x + (max.x - min.x) / 2f);
                center = new Vector3(
                    (min.x + ((max.x - min.x) / 2f)),
                    (min.y + ((max.y - min.y) / 2f)),
                    (min.z + ((max.z - min.z) / 2f))
                    );
                Console.WriteLine("Min: " + min + " Max: " + max+" Center: "+center);
                offsetDistance = (max.x - min.x) * 1.5f;
                float zDistance = (max.z - min.z) * 1.5f;
                if (zDistance > offsetDistance) offsetDistance = zDistance;
            }

            gl.Flush();
            ServicedVerticesLabel.Text = "Passed vertices: "+PassedCount.ToString();
            sw.Stop();
            deltaTime = ((float)sw.ElapsedMilliseconds)/1000f;
            sw.Reset();
            angle += 35f * deltaTime;
            if (angle > 360) angle = 0f;
            /*
            // gl.Color(1.0f, 1.0f, 1.0f);
            // gl.FontBitmaps.DrawText(gl, 0, 0, "Arial", "Argh");



            gl.Translate(-1.5f, 0.0f, -6.0f);				// Move Left And Into The Screen

            gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);				// Rotate The Pyramid On It's Y Axis

            gl.Begin(OpenGL.GL_TRIANGLES);					// Start Drawing The Pyramid

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Front)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Left Of Triangle (Front)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Right Of Triangle (Front)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Right)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Left Of Triangle (Right)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Right Of Triangle (Right)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Back)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Left Of Triangle (Back)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Right Of Triangle (Back)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Left)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Left Of Triangle (Left)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Right Of Triangle (Left)
            gl.End();						// Done Drawing The Pyramid

            gl.LoadIdentity();
            gl.Translate(1.5f, 0.0f, -7.0f);				// Move Right And Into The Screen

            gl.Rotate(rquad, 1.0f, 1.0f, 1.0f);			// Rotate The Cube On X, Y & Z

            gl.Begin(OpenGL.GL_QUADS);					// Start Drawing The Cube

            gl.Color(0.0f, 1.0f, 0.0f);			// Set The Color To Green
            gl.Vertex(1.0f, 1.0f, -1.0f);			// Top Right Of The Quad (Top)
            gl.Vertex(-1.0f, 1.0f, -1.0f);			// Top Left Of The Quad (Top)
            gl.Vertex(-1.0f, 1.0f, 1.0f);			// Bottom Left Of The Quad (Top)
            gl.Vertex(1.0f, 1.0f, 1.0f);			// Bottom Right Of The Quad (Top)


            gl.Color(1.0f, 0.5f, 0.0f);			// Set The Color To Orange
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Top Right Of The Quad (Bottom)
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Top Left Of The Quad (Bottom)
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Bottom Left Of The Quad (Bottom)
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Bottom Right Of The Quad (Bottom)

            gl.Color(1.0f, 0.0f, 0.0f);			// Set The Color To Red
            gl.Vertex(1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Front)
            gl.Vertex(-1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Front)
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Bottom Left Of The Quad (Front)
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Bottom Right Of The Quad (Front)

            gl.Color(1.0f, 1.0f, 0.0f);			// Set The Color To Yellow
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Bottom Left Of The Quad (Back)
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Bottom Right Of The Quad (Back)
            gl.Vertex(-1.0f, 1.0f, -1.0f);			// Top Right Of The Quad (Back)
            gl.Vertex(1.0f, 1.0f, -1.0f);			// Top Left Of The Quad (Back)

            gl.Color(0.0f, 0.0f, 1.0f);			// Set The Color To Blue
            gl.Vertex(-1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Left)
            gl.Vertex(-1.0f, 1.0f, -1.0f);			// Top Left Of The Quad (Left)
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Bottom Left Of The Quad (Left)
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Bottom Right Of The Quad (Left)

            gl.Color(1.0f, 0.0f, 1.0f);			// Set The Color To Violet
            gl.Vertex(1.0f, 1.0f, -1.0f);			// Top Right Of The Quad (Right)
            gl.Vertex(1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Right)
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Bottom Left Of The Quad (Right)
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Bottom Right Of The Quad (Right)
            gl.End();                       // Done Drawing The Q

            gl.Flush();

            rtri += 3.0f;// 0.2f;						// Increase The Rotation Variable For The Triangle 
            rquad -= 3.0f;// 0.15f;						// Decrease The Rotation Variable For The Quad */
        }

        bool threw = false;
        float rtri = 0;
        float rquad = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            //OpenFileDialog.InitialDirectory = "c:\\";
            OpenFileDialog.FileName = "";
            OpenFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            OpenFileDialog.FilterIndex = 2;
            OpenFileDialog.RestoreDirectory = true;
            //var dialog = OpenFileDialog.ShowDialog();
            //Stream myStream;
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = OpenFileDialog.FileName;
                //string[] FileLines = File.ReadAllLines(fileName);
                ByteArrFile bar = new ByteArrFile("textmap");
                bar.bytes = File.ReadAllBytes(fileName);
                List<string> fl = UDMF_TextmapEx.ReadFileToNodes(bar);
                //fl.AddRange(FileLines);
                OurMap= UDMF_TextmapEx.ReadFile(fl);
                Console.WriteLine("Passed");
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDIalogV.FileName = "TEXTMAP.txt";
            SaveFileDIalogV.Filter= "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            SaveFileDIalogV.FilterIndex = 1;
            SaveFileDIalogV.RestoreDirectory = true;
            SaveFileDIalogV.AddExtension = true;

            if(SaveFileDIalogV.ShowDialog() == DialogResult.OK)
            {
                Stream fs = SaveFileDIalogV.OpenFile();
                StreamWriter fsw = new StreamWriter(fs);

                fsw.WriteLine("namespace = \"zdoom\";");
                for(int q=0; q<OurMap.vertices.Count; q++)
                {
                    fsw.WriteLine("vertex");
                    fsw.WriteLine("{");
                    fsw.WriteLine("x = " + OurMap.vertices[q].position.x.ToString("0.000") + ";");
                    fsw.WriteLine("y = " + OurMap.vertices[q].position.y.ToString("0.000") + ";");

                    float ceilOffs= OurMap.vertices[q].ceilingOffset!=null? OurMap.vertices[q].ceilingOffset.Value : float.MinValue;
                    float floorOffs= OurMap.vertices[q].floorOffset != null ? OurMap.vertices[q].floorOffset.Value : float.MinValue;



                    if (RB_Copy_CtF.Checked)
                    {
                        floorOffs = ceilOffs;
                    }
                    else if(RB_Copy_FtC.Checked)
                    {
                        ceilOffs = floorOffs;
                    }
                    else if (RB_Swap.Checked)
                    {
                        float tmp = floorOffs;
                        floorOffs = ceilOffs;
                        ceilOffs = tmp;
                        //var cb = OurMap.vertices[q].floorOffset;
                        //OurMap.vertices[q].floorOffset = OurMap.vertices[q].ceilingOffset;
                        //OurMap.vertices[q].ceilingOffset = cb;
                    }
                    

                    if (ceilOffs!=float.MinValue)
                    {
                        ceilOffs = ceilOffs * ((float)CeilingMult.Value) + ((float)CeilingOffs.Value);
                        fsw.WriteLine("zceiling = " + ceilOffs.ToString("0.000") + ";");
                    }
                    if (floorOffs!=float.MinValue)
                    {
                        floorOffs = floorOffs * ((float)FloorMult.Value) + ((float)FloorOffs.Value);
                        fsw.WriteLine("zfloor = " + floorOffs.ToString("0.000") + ";");
                    }
                    fsw.WriteLine("}");
                    fsw.WriteLine("");


                }

                for(int q=0; q<OurMap.lineDefs.Count; q++)
                {
                    fsw.WriteLine("linedef");
                    fsw.WriteLine("{");
                    fsw.WriteLine("v1 = " + OurMap.lineDefs[q].v1 + ";");
                    fsw.WriteLine("v2 = " + OurMap.lineDefs[q].v2 + ";");
                    if(OurMap.lineDefs[q].sideFront>-1)
                    {
                        fsw.WriteLine("sidefront = " + OurMap.lineDefs[q].sideFront + ";");
                    }
                    if(OurMap.lineDefs[q].sideBack>-1)
                    {
                        fsw.WriteLine("sideback = " + OurMap.lineDefs[q].sideBack + ";");
                    }
                    if(OurMap.lineDefs[q].twoSided)
                    {
                        fsw.WriteLine("twosided = " + OurMap.lineDefs[q].twoSided + ";");
                    }
                    if(OurMap.lineDefs[q].blocking)
                    {
                        fsw.WriteLine("blocking = " + OurMap.lineDefs[q].blocking + ";");
                    }
                    fsw.WriteLine("}");
                    fsw.WriteLine("");
                }

                for(int q=0; q<OurMap.sideDefs.Count; q++)
                {
                    fsw.WriteLine("sidedef");
                    fsw.WriteLine("{");
                    fsw.WriteLine("sector = " + OurMap.sideDefs[q].sector+";");
                    if(OurMap.sideDefs[q].offsetx!=0)
                    {
                        fsw.WriteLine("offsetx = " + OurMap.sideDefs[q].offsetx + ";");
                    }
                    if (OurMap.sideDefs[q].offsety != 0)
                    {
                        fsw.WriteLine("offsety = " + OurMap.sideDefs[q].offsety + ";");
                    }
                    if(OurMap.sideDefs[q].textureTop!="")
                    {
                        fsw.WriteLine("texturetop = \"" + OurMap.sideDefs[q].textureTop+"\";");
                    }
                    if (OurMap.sideDefs[q].textureBottom != "")
                    {
                        fsw.WriteLine("texturebottom = \"" + OurMap.sideDefs[q].textureBottom + "\";");
                    }
                    if (OurMap.sideDefs[q].textureMiddle != "")
                    {
                        fsw.WriteLine("texturemiddle = \"" + OurMap.sideDefs[q].textureMiddle + "\";");
                    }
                    if (OurMap.sideDefs[q].offsetX_top!=0f )
                    {
                        fsw.WriteLine("offsetx_top = " + OurMap.sideDefs[q].offsetX_top.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].offsetY_top != 0f)
                    {
                        fsw.WriteLine("offsety_top = " + OurMap.sideDefs[q].offsetY_top.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].offsetX_mid != 0f)
                    {
                        fsw.WriteLine("offsetx_mid = " + OurMap.sideDefs[q].offsetX_mid.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].offsetY_mid != 0f)
                    {
                        fsw.WriteLine("offsety_mid = " + OurMap.sideDefs[q].offsetY_mid.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].offsetX_bottom != 0f)
                    {
                        fsw.WriteLine("offsetx_bottom = " + OurMap.sideDefs[q].offsetX_bottom.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].offsetY_bottom != 0f)
                    {
                        fsw.WriteLine("offsety_bottom = " + OurMap.sideDefs[q].offsetY_bottom.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].scaleX_top != 1f)
                    {
                        fsw.WriteLine("scalex_top = "+OurMap.sideDefs[q].scaleX_top.ToString("0.000")+";");
                    }
                    if (OurMap.sideDefs[q].scaleY_top != 1f)
                    {
                        fsw.WriteLine("scaley_top = " + OurMap.sideDefs[q].scaleY_top.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].scaleX_mid != 1f)
                    {
                        fsw.WriteLine("scalex_mid = " + OurMap.sideDefs[q].scaleX_mid.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].scaleY_mid != 1f)
                    {
                        fsw.WriteLine("scaley_mid = " + OurMap.sideDefs[q].scaleY_mid.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].scaleX_bot != 1f)
                    {
                        fsw.WriteLine("scalex_bottom = " + OurMap.sideDefs[q].scaleX_bot.ToString("0.000") + ";");
                    }
                    if (OurMap.sideDefs[q].scaleY_bot != 1f)
                    {
                        fsw.WriteLine("scaley_bottom = " + OurMap.sideDefs[q].scaleY_bot.ToString("0.000") + ";");
                    }
                    fsw.WriteLine("}");
                    fsw.WriteLine("");
                }

                for(int q=0; q<OurMap.sectors.Count; q++)
                {
                    fsw.WriteLine("sector");
                    fsw.WriteLine("{");

                    float floorHeight = OurMap.sectors[q].heightFloor;
                    float ceilingHeight = OurMap.sectors[q].heightCeiling;

                    if(RB_Copy_CtF.Checked)
                    {
                        floorHeight = ceilingHeight;
                    }
                    else if(RB_Copy_CtF.Checked)
                    {
                        ceilingHeight = floorHeight;
                    }
                    else if(RB_Swap.Checked)
                    {
                        float t = floorHeight;
                        floorHeight = ceilingHeight;
                        ceilingHeight = t;
                    }

                    floorHeight = floorHeight * (float)FloorMult.Value + (float)FloorOffs.Value;
                    ceilingHeight = ceilingHeight * (float)CeilingMult.Value + (float)CeilingOffs.Value;

                    fsw.WriteLine("heightfloor = " + floorHeight.ToString("0")+ ";");
                    fsw.WriteLine("heightceiling = " + ceilingHeight.ToString("0") + ";");
                    fsw.WriteLine("texturefloor = " + "\"" + OurMap.sectors[q].textureFloor + "\";");
                    fsw.WriteLine("textureceiling = " + "\"" + OurMap.sectors[q].textureCeiling + "\";");
                    fsw.WriteLine("lightlevel = " + OurMap.sectors[q].lightLevel + ";");
                    fsw.WriteLine("xpanningceiling = " + OurMap.sectors[q].xOffsetCeiling.ToString("0.000")+";");
                    fsw.WriteLine("ypanningceiling = " + OurMap.sectors[q].yOffsetCeiling.ToString("0.000") + ";");
                    fsw.WriteLine("rotationceiling = " + OurMap.sectors[q].rotationCeiling.ToString("0.000") + ";");
                    fsw.WriteLine("xscaleceiling = " + OurMap.sectors[q].xScaleCeiling.ToString("0.000") + ";");
                    fsw.WriteLine("yscaleceiling = " + OurMap.sectors[q].yScaleCeiling.ToString("0.000") + ";");
                    fsw.WriteLine("xpanningfloor = " + OurMap.sectors[q].xOffsetFloor.ToString("0.000") + ";");
                    fsw.WriteLine("ypanningfloor = " + OurMap.sectors[q].yOffsetFloor.ToString("0.000") + ";");
                    fsw.WriteLine("rotationfloor = " + OurMap.sectors[q].rotationFloor.ToString("0.000") + ";");
                    fsw.WriteLine("xscalefloor = " + OurMap.sectors[q].xScaleFloor.ToString("0.000") + ";");
                    fsw.WriteLine("yscalefloor = " + OurMap.sectors[q].yScaleFloor.ToString("0.000") + ";");
                    fsw.WriteLine("}");
                    fsw.WriteLine("");

                }


                fsw.Flush();
                fsw.Dispose();
                fsw.Close();
                fs.Dispose();
                fs.Close();

            }
        }
    }
}
