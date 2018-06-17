using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using SharpGL;

namespace UDMF_VertexTools
{
    static class UDMF_TextmapEx
    {
        public static void GetFromWad(string path)
        {
            var files= GetAllFiles( File.ReadAllBytes(path));
            if(files.Exists(x=>x.name=="textmap") )
            {
                var map= files.Find(x => x.name == "texmap");
                var nodes = ReadFileToNodes(map);
            }
        }

        public static UDMFConverted ReadFile(List<string> file)
        {
            /*foreach(string m in file)
            {
                Debug.Log(m);
            }
            return new UDMFConverted();*/
            if (file.Count > 0)
            {
                UDMFConverted dConv = new UDMFConverted();

                List<VertexDef> vertices = new List<VertexDef>();
                List<Sector> sectors = new List<Sector>();
                List<SideDef> sideDefs = new List<SideDef>();
                List<Linedef> lineDefs = new List<Linedef>();
                List<Thing> things = new List<Thing>();
                //			CRd current= CRd.core;
                for (int ln = 0; ln < file.Count; ln++)
                {
                    //int ePos=ln;
                    if (Regex.IsMatch(file[ln], @"thing.*\n{", RegexOptions.IgnoreCase))
                    {
                        //Debug.Log("thing:"+file[ln]);
                        Thing tn = new Thing();
                        tn.FormFromList(file[ln].Split(new char[] { '\n' }, StringSplitOptions.None));
                        things.Add(tn);
                    }
                    else if (Regex.IsMatch(file[ln], @"vertex.*\n{", RegexOptions.IgnoreCase))
                    {
                        //Debug.Log("vertex:"+file[ln]);
                        VertexDef vect = new VertexDef();
                        vect.FormFromList(file[ln].Split(new char[] { '\n' }, StringSplitOptions.None));
                        //Vector2 vect = StrUtils.FormVector2FromList(file[ln].Split(new char[] { '\n' }, StringSplitOptions.None));
                        vertices.Add(vect);
                    }
                    else if (Regex.IsMatch(file[ln], @"linedef.*\n{", RegexOptions.IgnoreCase))
                    {
                        //Debug.Log("linedef:"+file[ln]);
                        Linedef ld = new Linedef();
                        ld.FormFromList(file[ln].Split(new char[] { '\n' }, StringSplitOptions.None));
                        lineDefs.Add(ld);
                    }
                    else if (Regex.IsMatch(file[ln], @"sidedef.*\n{", RegexOptions.IgnoreCase))
                    {
                        //Debug.Log("sidedef:"+file[ln]);
                        SideDef sd = new SideDef();
                        sd.FormFromList(file[ln].Split(new char[] { '\n' }, StringSplitOptions.None));
                        sideDefs.Add(sd);
                    }
                    else if (Regex.IsMatch(file[ln], @"sector.*\n{", RegexOptions.IgnoreCase))
                    {
                        //Debug.Log("sector:"+file[ln]);
                        Sector sec = new Sector();
                        sec.FormFromList(file[ln].Split(new char[] { '\n' }, StringSplitOptions.None));
                        sectors.Add(sec);
                    }


                    //if(ePos!=ln) ln=ePos;
                }


                dConv.things = things;
                dConv.vertices = vertices;
                dConv.sectors = sectors;
                dConv.sideDefs = sideDefs;
                dConv.lineDefs = lineDefs;



                return dConv;
            }

            return null;
        }



        public static List<string> ReadFileToNodes(ByteArrFile bfile)
        {
            List<string> file = new List<string>();
            MemoryStream ms = new MemoryStream(bfile.bytes);
            try
            {
                using (StreamReader sr = new StreamReader(ms))
                {
                    string q = sr.ReadToEnd();
                    MatchCollection mc = Regex.Matches(q, @".+\n{[^}]*}", RegexOptions.IgnoreCase);
                    for (int b = 0; b < mc.Count; b++)
                    {
                        file.Add(mc[b].Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File could not be read:");
                throw new System.Exception(e.Message);
            }
            return file;
        }
        
        

        public static List<ByteArrFile> GetAllFiles(byte[] dumpedWad)
        {

            List<ByteArrFile> files = new List<ByteArrFile>();
            //IWAD? PWAD? who the fuck cares. It's a bunch of lumps in any case
            //everything in little endian order
            //0x00	4	identification	The ASCII characters "IWAD" or "PWAD". Defines whether the WAD is an IWAD or a PWAD.
            //0x04	4	numlumps	An integer specifying the number of lumps in the WAD.
            //0x08	4	infotableofs	An integer holding a pointer to the location of the directory.


            int fileCount = System.BitConverter.ToInt32(GetSubArray(ref dumpedWad, 0x04, 4), 0);
            int namesPosition = System.BitConverter.ToInt32(GetSubArray(ref dumpedWad, 0x08, 4), 0);
            //		Debug.Log(fileCount+" "+namesPosition);
            //0x00	4	filepos	An integer holding a pointer to the start of the lump's data in the file.
            //0x04	4	size	An integer representing the size of the lump in bytes.
            //0x08	8	name	An ASCII string defining the lump's name. Only the characters A-Z
            //(uppercase), 0-9, and [ ] - _ should be used in lump names 
            //(an exception has to be made for some of the Arch-Vile sprites, which use "\"). When a string is less than 8 bytes long, it should be null-padded to the tight byte.

            int queryLength = namesPosition + fileCount * 16;
            for (int q = namesPosition; q < queryLength; q += 16)
            {
                int filePos = System.BitConverter.ToInt32(GetSubArray(ref dumpedWad, q, 4), 0);
                int fileSize = System.BitConverter.ToInt32(GetSubArray(ref dumpedWad, q + 4, 4), 0);
                byte[] arr2 = GetSubArray(ref dumpedWad, q + 8, 8);
                int mArrq = 8;
                for (int ar2c = 0; ar2c < arr2.Length; ar2c++)
                {
                    if (arr2[ar2c] == 0x00)//all wad names are 8 bytes, unused bytes get filled out with 0x00s which causes resulting strings to have junk at their tail.
                    {
                        mArrq = ar2c;
                        break;
                    }
                }
                string name = System.Text.Encoding.ASCII.GetString(GetSubArray(ref arr2, 0, mArrq));
                //string name=System.BitConverter.ToString(GetSubArray(ref dumpedWad, q+8, 8));
                ByteArrFile tFile = new ByteArrFile(name);
                if (fileSize != 0) tFile.bytes = GetSubArray(ref dumpedWad, filePos, fileSize);
                files.Add(tFile);

            }

            return files;


        }

        public static byte[] GetBEVal(ref byte[] arr, int pos, int length)
        {
            byte[] t = GetSubArray(ref arr, pos, length);
            ByteArrFile.ReverseBytes(ref t);
            return t;
        }


        public static byte[] GetSubArray(ref byte[] arr, int pos, int length)
        {
            byte[] subArr = new byte[length];
            System.Array.Copy(arr, pos, subArr, 0, length);
            return subArr;


        }
    }

    [System.Serializable]
    public class ByteArrFile
    {
        public string name;
        public byte[] bytes;
        public ByteArrFile(string nm, byte[] bs)
        {
            name = nm;
            bytes = bs;
        }
        public ByteArrFile(string nm) //MARKER, god damn it!
        {
            name = nm;
        }



        public override bool Equals(object obj)
        {
            ByteArrFile iv2 = (ByteArrFile)obj;// as IntVector2;
                                               //if(iv2==null) return false;
            return (name.Equals(iv2.name)) && (bytes.Equals(iv2.bytes));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + name.GetHashCode();
                hash = hash * 23 + bytes.GetHashCode();
                return hash;
            }
        }

        public static void ReverseBytes(ref byte[] arr)
        {
            System.Array.Reverse(arr);
        }
    }

    

    [System.Serializable]
    public class UDMFConverted
    {
        public List<VertexDef> vertices;
        public List<Sector> sectors;
        public List<SideDef> sideDefs;
        public List<Linedef> lineDefs;
        public List<Thing> things;


    }


}


