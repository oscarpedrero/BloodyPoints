
using Unity.Mathematics;

namespace BloodyPoints.DB
{
    public class WaypointData
    {
        public string Name { get; set; }
        public ulong Owner { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public WaypointData(string name, ulong owner, float x, float y, float z )
        {
            Name = name;
            Owner = owner;
            X = x;
            Y = y;
            Z = z;
        }

        public float3 getLocation()
        {
            return new float3(X, Y, Z);
        }
    }
}
