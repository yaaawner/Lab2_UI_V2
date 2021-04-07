using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    [Serializable]
    struct DataItem:ISerializable
    {
        public Vector2 Vector { get; set; }
        public Complex Complex { get; set; }

        public DataItem(Vector2 vector, Complex complex)
        {
            Vector = vector;
            Complex = complex;
        }

        public override string ToString()
        {
            return "Vector: " + Vector.X.ToString() + " " + Vector.Y.ToString() + " " +
                   "Complex: " + Complex.ToString();
        }

        public string ToString(string format)
        {
            return "Vector: " + Vector.X.ToString(format) + " " + Vector.Y.ToString(format) + " " +
                   "Complex: " + Complex.ToString(format);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Vector_X", Vector.X);
            info.AddValue("Vector_Y", Vector.Y);
            info.AddValue("Complex", Complex);
        }

        public DataItem(SerializationInfo info, StreamingContext context)
        {
            float x = info.GetSingle("Vector_X");
            float y = info.GetSingle("Vector_Y");
            Vector = new Vector2(x, y);
            Complex = (Complex)info.GetValue("Complex", typeof(System.Numerics.Complex));
        }
    }
}
