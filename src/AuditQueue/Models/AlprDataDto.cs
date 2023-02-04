using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditQueue.Models
{
    public class AlprDataDto
    {
        public VehicleRegionDto VehicleCordinates { get; set; }
        public string PlateNumber { get; set; }
        public string BestUuid { get; set; }

        public string AgentUid { get; set; }
    }

    public class VehicleRegionDto
    {
        public int x { get; set; }
        public int height { get; set; }
        public int y { get; set; }
        public int width { get; set; }
    }
}
