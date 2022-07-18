using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    public class Program
    {
        public static void Main()
        {
            var myParkingLot = new ParkingLot(5);
            Car c0 = new Car("000");
            Car c3 = new Car("333");

            myParkingLot[3] = c3;
            myParkingLot = myParkingLot + c0;

            var result = myParkingLot.PrintInfo();
            Console.WriteLine(result);

            myParkingLot = myParkingLot >> 3;

            result = myParkingLot.PrintInfo();
            Console.WriteLine("");
            Console.WriteLine(result);
        }
    }

    public class ParkingLot
    {
        public ParkingLot(int capicity)
        {
            Capicity = capicity;
            ParkingCars = new Car[capicity];

        }
        public int Capicity { get; set; }
        public Car[] ParkingCars { get; set; }
        public Car this[int index]
        {
            get
            {
                return ParkingCars[index];
            }
            set
            {
                ParkingCars[index] = value;
            }
        }

        public static ParkingLot operator +(ParkingLot parkingLot, Car car)
        {
            for (int i = 0; i < parkingLot.ParkingCars.Length; i++)
            {
                if (parkingLot.ParkingCars[i] == null)
                {
                    parkingLot.ParkingCars[i] = car;
                    break;
                }
            }
            return parkingLot;
        }

        public static ParkingLot operator >>(ParkingLot parkingLot, int shift)
        {
            var length = parkingLot.ParkingCars.Length;
            var newParkingLot = new ParkingLot(length);

            for (int i = 0; i < length; i++)
            {
                if (parkingLot.ParkingCars[i] != null)
                {
                    if (i + shift >= 0 && i + shift < parkingLot.ParkingCars.Length)
                    {
                        newParkingLot.ParkingCars[i + shift] = parkingLot.ParkingCars[i];
                    }
                    else if (length != 0)
                    {
                        newParkingLot.ParkingCars[(i + shift) % length] = parkingLot.ParkingCars[i];
                    }
                }
            }
            return newParkingLot;
        }

        public string PrintInfo()
        {
            var carList = new StringBuilder();

            for (int i = 0; i < ParkingCars.Length; i++)
            {
                if (ParkingCars[i] != null)
                {
                    carList.AppendLine("index : " + i.ToString() + ParkingCars[i].ToString());
                }
            }
            return carList.ToString();
        }
    }

    public class Car
    {
        public Car(string licenseNumber)
        {
            LicenseNumber = licenseNumber;
        }
        public string LicenseNumber { get; set; }
        public override string ToString() => $",LicenseNumber : {LicenseNumber}";
    }
}

