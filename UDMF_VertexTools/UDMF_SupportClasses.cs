using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using System.Drawing;

namespace UDMF_VertexTools
{


    [System.Serializable]
    public class Thing
    {
        //SharpGL.
        public Vector2 position;
        public float height = 0;
        public float yAngle = 0;
        public int id = -1;
        public int type = 1;


        public void FormFromList(string[] input)
        {
            position = new Vector3();


            foreach (string str in input)
            {
                //Debug.Log(str);
                //Debug.Log(StrUtils.StripUDMFFluff(str));
                if (str.Contains("x ="))
                {
                    position.x = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("y ="))
                {
                    position.y = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("type ="))
                {
                    type = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("angle ="))
                {
                    yAngle = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("id ="))
                {
                    id = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("type ="))
                {
                    type = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("height ="))
                {
                    height = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

            }


        }
    }




    [System.Serializable]
    public class Sector
    {
        public float heightFloor;
        public float heightCeiling;
        public string textureFloor;
        public string textureCeiling;
        public float xOffsetFloor;
        public float yOffsetFloor;
        public float xOffsetCeiling;
        public float yOffsetCeiling;
        public float rotationFloor;
        public float rotationCeiling;
        public float xScaleFloor = 1f;
        public float yScaleFloor = 1f;
        public float xScaleCeiling = 1f;
        public float yScaleCeiling = 1f;
        public int lightLevel;
        public System.Drawing.Color lightColor = Color.White;
        public int id = -1;
        public ImpesSpecial special;


        public void FormFromList(string[] input)
        {


            foreach (string str in input)
            {
                if (str.Contains("heightfloor ="))
                {
                    heightFloor = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("heightceiling ="))
                {
                    heightCeiling = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("texturefloor ="))
                {
                    textureFloor = StrUtils.StripFluffFromString(str);
                }
                else if (str.Contains("textureceiling ="))
                {
                    textureCeiling = StrUtils.StripFluffFromString(str);
                }
                else if (str.Contains("lightlevel ="))
                {
                    lightLevel = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("id ="))
                {
                    id = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_brushchunk ="))
                {
                    string mtr = StrUtils.StripUDMFFluff(str);
                    if (mtr == "true")
                    {
                        special = new ImpesSpecial();
                        special.activated = true;
                    }
                }//xpanningfloor
                else if (str.Contains("xpanningfloor ="))
                {
                    xOffsetFloor = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("ypanningfloor ="))
                {
                    yOffsetFloor = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("xpanningceiling ="))
                {
                    xOffsetCeiling = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("ypanningceiling ="))
                {
                    yOffsetCeiling = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }//lightcolor
                else if (str.Contains("rotationfloor ="))
                {
                    rotationFloor = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("rotationceiling ="))
                {
                    rotationCeiling = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

                else if (str.Contains("xscalefloor ="))
                {
                    xScaleFloor = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

                else if (str.Contains("yscalefloor ="))
                {
                    yScaleFloor = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

                else if (str.Contains("xscaleceiling ="))
                {
                    xScaleCeiling = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

                else if (str.Contains("yscaleceiling ="))
                {
                    yScaleCeiling = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

                else if (str.Contains("lightcolor ="))
                {
                    byte[] t = BitConverter.GetBytes(System.Convert.ToInt32(StrUtils.StripUDMFFluff(str)));
                    lightColor = Color.FromArgb(t[2], t[1], t[0]);
                    //lightColor.R = t[2];
                    //lightColor.G = t[1];
                    //lightColor.B = t[0];
                    //Debug.Log(t.Length+" "+t[0]+" "+t[1]+" "+t[2]+" "+t[3]);
                    //lightColor.r=t[0];
                }

            }


            //Debug.Log(t.Length+" "+t[0]+" "+t[1]+" "+t[2]+" "+t[3]);

            if (special == null || !special.activated) return;
            foreach (string str in input)
            {
                if (str.Contains("impes_speed ="))
                {
                    special.speed = (float)System.Convert.ToDouble(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_distance_x ="))
                {
                    special.endPos.x = (float)System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_distance_y ="))
                {
                    special.endPos.y = (float)System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_distance_z ="))
                {
                    special.endPos.z = (float)System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_sidetexture ="))
                {
                    special.texture = StrUtils.StripFluffFromString(str);
                }
                else if (str.Contains("impes_distfloor ="))
                {
                    special.distFloor = (float)System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_distceiling ="))
                {
                    special.distCeiling = (float)System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_rotation_x ="))
                {
                    special.endRot.x = (float)System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_rotation_y ="))
                {
                    special.endRot.y = (float)System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_rotation_z ="))
                {
                    special.endRot.z = (float)System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("impes_delay ="))
                {
                    special.delay = (float)System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                    special.delay /= 1000f;
                }
            }
            special.tag = id;
            special.endPos /= 32f;

        }

    }

    [System.Serializable]
    public class ImpesSpecial
    {
        public bool activated = false;
        public Vector3 endPos = new Vector3();
        public Vector3 endRot = new Vector3();
        public float speed;
        public string texture;
        public float distFloor = 0;
        public float distCeiling = 0;
        public int tag = -1;
        public float delay = 0f;

    }

    [System.Serializable]
    public class VertexDef
    {
        public Vector2 position;
        public float? floorOffset;
        public float? ceilingOffset;

        public void FormFromList(string[] input)
        {
            foreach (string str in input)
            {
                if (str.Contains("x ="))
                {
                    position.x = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                if (str.Contains("y ="))
                {
                    position.y = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                if (str.Contains("zfloor ="))
                {
                    floorOffset = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                if (str.Contains("zceiling ="))
                {
                    ceilingOffset = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
            }
        }

    }

    [System.Serializable]
    public class SideDef
    {
        public int sector;
        public string textureTop="";
        public string textureMiddle="";
        public string textureBottom="";
        public int offsetx = 0;
        public int offsety = 0;
        public float offsetX_mid = 0;
        public float offsetY_mid = 0;
        public float offsetX_top = 0;
        public float offsetY_top = 0;
        public float offsetX_bottom = 0;
        public float offsetY_bottom = 0;
        public float scaleX_top = 1f;
        public float scaleY_top = 1f;
        public float scaleX_mid = 1f;
        public float scaleY_mid = 1f;
        public float scaleX_bot = 1f;
        public float scaleY_bot = 1f;




        public void FormFromList(string[] input)
        {


            foreach (string str in input)
            {
                if (str.Contains("sector ="))
                {
                    sector = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsetx"))
                {
                    offsetx = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsety"))
                {
                    offsety = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("texturemiddle ="))
                {
                    textureMiddle = StrUtils.StripFluffFromString(str);
                }
                else if (str.Contains("texturetop ="))
                {
                    textureTop = StrUtils.StripFluffFromString(str);
                }
                else if (str.Contains("texturebottom ="))
                {
                    textureBottom = StrUtils.StripFluffFromString(str);
                }
                else if (str.Contains("offsetx_mid ="))
                {
                    offsetX_mid = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsety_mid ="))
                {
                    offsetY_mid = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsetx_top ="))
                {
                    offsetX_top = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsety_top ="))
                {
                    offsetY_top = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsetx_bottom ="))
                {
                    offsetX_bottom = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("offsety_bottom ="))
                {
                    offsetY_bottom = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }

                else if (str.Contains("scalex_bottom ="))
                {
                    scaleX_bot = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("scaley_bottom ="))
                {
                    scaleY_bot = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("scalex_mid ="))
                {
                    scaleX_mid = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("scaley_mid ="))
                {
                    scaleY_mid = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("scalex_top ="))
                {
                    scaleX_top = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("scaley_top ="))
                {
                    scaleY_top = System.Convert.ToSingle(StrUtils.StripUDMFFluff(str));
                }
            }


        }

    }

    [System.Serializable]
    public class LineSpecial
    {
        public int num;
        public int arg0;
        public int arg1;
        public int arg2;
        public int arg3;
        public int arg4;
        public bool backSideCounts = false;
        public string addendum = "";
    }

    [System.Serializable]
    public class Linedef
    {
        public int v1;
        public int v2;
        public int sideFront = -1;
        public int sideBack = -1;
        public bool twoSided = false;
        public bool blocking = false;
        public int id = -1;
        public bool uppedUnpegged = false;
        public bool bottomUnpegged = false;
        public LineSpecial special = new LineSpecial();

        public void FormFromList(string[] input)
        {



            foreach (string str in input)
            {
                if (str.Contains("v1 ="))
                {
                    v1 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("v2 ="))
                {
                    v2 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("sidefront ="))
                {
                    sideFront = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("sideback ="))
                {
                    sideBack = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("id ="))
                {
                    id = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }
                else if (str.Contains("blocking ="))
                {
                    string mtr = StrUtils.StripUDMFFluff(str);
                    if (mtr == "true") blocking = true;
                }
                else if (str.Contains("twosided ="))
                {
                    string mtr = StrUtils.StripUDMFFluff(str);
                    if (mtr == "true") twoSided = true;
                }
                else if (str.Contains("dontpegtop ="))
                {
                    string mtr = StrUtils.StripUDMFFluff(str);
                    if (mtr == "true") uppedUnpegged = true;
                }
                else if (str.Contains("dontpegbottom ="))
                {
                    string mtr = StrUtils.StripUDMFFluff(str);
                    if (mtr == "true") bottomUnpegged = true;
                }
                else if (str.Contains("special =") && !str.Contains("repeatspecial ="))
                {
                    //Debug.Log(str);
                    special.num = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                }

            }

            if (special.num != 0)
            {
                foreach (string str in input)
                {
                    if (str.Contains("arg0 ="))
                    {

                        special.arg0 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                    }
                    else if (str.Contains("arg1 ="))
                    {

                        special.arg1 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                    }
                    else if (str.Contains("arg2 ="))
                    {

                        special.arg2 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                    }
                    else if (str.Contains("arg3 ="))
                    {

                        special.arg3 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                    }
                    else if (str.Contains("arg4 ="))
                    {

                        special.arg4 = System.Convert.ToInt32(StrUtils.StripUDMFFluff(str));
                    }
                    else if (str.Contains("playeruseback ="))
                    {
                        string mtr = StrUtils.StripUDMFFluff(str);
                        if (mtr == "true") special.backSideCounts = true;
                    }
                    else if (str.Contains("impes_mapname ="))
                    {
                        special.addendum = StrUtils.StripFluffFromString(str);
                    }
                }
            }

        }

    }
}
