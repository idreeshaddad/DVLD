using System.Collections.Generic;

namespace MB.T.DVLD.Entities
{
    public class PoliceCar
    {
        public int Id { get; set; }
        public string Manu { get; set; }
        public string Model { get; set; }
        public int CarNumber { get; set; }

        public List<Officer> Officers { get; set; }
    }
}
