using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Arcturus.Tools;

namespace Arcturus.Ressources
{
    class ArcTable
    {
        string name;
        object[] rows;
        string[] columns;

        public void Save(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(name);
                    binaryWriter.Write(rows.Length);
                    binaryWriter.Write(columns.Length);

                    for (int x = 0; x < rows.Length; x++)
                    {
                        for (int y = 0; y < columns.Length; y++)
                        {
                            binaryWriter.Write(rows[y]);
                        }
                    }
                }
            }
        }

        public void Load(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {

                }
            }
        }


    }
}
