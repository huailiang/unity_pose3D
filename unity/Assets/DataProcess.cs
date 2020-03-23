using System.IO;
using UnityEngine;

public class DataProcess : MonoBehaviour
{


    public void Start()
    {
        using (FileStream fs = new FileStream("Assets/kun.bytes", FileMode.Open, FileAccess.Read))
        {
            BinaryReader reader = new BinaryReader(fs);
            int x = reader.ReadInt32();
            int y = reader.ReadInt32();
            int z = reader.ReadInt32();

            Vector3[] sktn = new Vector3[x * y];

            int pt = 0;
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    Vector3 v = Vector3.zero;
                    v.x = reader.ReadSingle();
                    v.y = reader.ReadSingle();
                    v.z = reader.ReadSingle();
                    sktn[pt++] = v;
                }
        }
    }
}
