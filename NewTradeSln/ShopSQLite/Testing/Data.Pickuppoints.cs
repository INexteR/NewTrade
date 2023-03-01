using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Pickuppoint[]? pickuppoints;
        public static IEnumerable<Pickuppoint> GetPickuppoints()
        {
            if (pickuppoints is null)
            {
                var lines = File.ReadAllLines(pickuppointsDataFullName);
                pickuppoints = new Pickuppoint[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    pickuppoints[i] = new Pickuppoint
                    {
                        Id = int.Parse(props[0]),
                        Address = props[1],
                        Index = props[2]
                    };
                }
            }
            return pickuppoints;
        }
    }
}
