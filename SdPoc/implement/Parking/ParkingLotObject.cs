using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implement.Parking
{
    public class ParkingLot
    {

    }
    public class Car
    {
        public string licenseNumber { get; set; }
        public string Name { get; set; }
        public CarType CarSizeType { get; set; }

        public bool isRecognize { get; set; }

        private int Width { get; set; }
        private int Height { get; set; }
        private int Length { get; set; }
    }

    public class ParkingTicket
    {
        public int ParkingTicketNo { get; set; }
        public Car Car { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }
    }

    public enum CarType
    {
        Small = 1,
        Medium = 2,
        Large = 3,
        ExtraLarge = 4
    }
}
