using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OOP2Assignment2.Services 
{ 
    internal class Reservation
    {
        private string reservationCode;
        private string flightNumber;
        private string airline;
        private string day;
        private string time;
        private float cost;
        private string name;
        private string citizenShip;

        public string ReservationCode
        {
            get { return reservationCode; }
            set { reservationCode = value; }
        }
        public string FlightNumber
        {
            get { return flightNumber; }
            set { flightNumber = value; }
        }

        public string Airline
        {
            get { return airline; }
            set { airline = value; }
        }

        public string Day
        {
            get { return day; }
            set { day = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Citizenship
        {
            get { return citizenShip; }
            set { citizenShip = value; }
        }
        internal Reservation(string flightNumber, string airline, string day, string time, float cost, string Name, string citizenShip)
        {
            this.FlightNumber = flightNumber;
            this.Airline = airline;
            this.Day = day;
            this.Time = time;
            this.Cost = cost;
            this.name = Name;
            this.Citizenship = citizenShip;
            GenerateReservationCode();
        }

        //construct given reservationCode for the purposes of JsonDeserializer.
        [JsonConstructor]
        internal Reservation(string reservationCode, string flightNumber, string airline, string day, string time, float cost, string Name, string citizenShip)
        {
            this.ReservationCode = reservationCode;
            this.FlightNumber = flightNumber;
            this.Airline = airline;
            this.Day = day;
            this.Time = time;
            this.Cost = cost;
            this.name = Name;
            this.Citizenship = citizenShip;
        }

            //empty constructor, essentially used like null.
            internal Reservation()
        {
        }

        internal void GenerateReservationCode()
        {
            //pick a random item from this char array to get a random letter. Then generate 4 random numbers.
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            string reservationCode = alpha[RandomNumberGenerator.GetInt32(alpha.Count())].ToString();

            for (int i = 0; i < 4; i++)
            {
                reservationCode += RandomNumberGenerator.GetInt32(9).ToString();
            }

            this.ReservationCode =  reservationCode;
        }

        public override string ToString()
        {
            return $"{ReservationCode} - {Name}, {FlightNumber}, {Airline}, {Day}, {Time}, {Cost}";
        }
    }
}
