using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2Assignment2.Services
{
    internal class Flight
    {
        private string flightNumber;
        private string airline;
        private string airportCodeStart;
        private string airportCodeEnd;
        private string day;
        private string time;

        //I do not know what one of the csv columns is for
        private int seats;
        private float cost;

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

        public string AirportCodeStart
        {
            get { return airportCodeStart; }
            set { airportCodeStart = value; }
        }

        public string AirportCodeEnd
        {
            get { return airportCodeEnd; }
            set { airportCodeEnd = value; }
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

        public int Seats
        {
            get { return seats; }
            set { seats = value; }
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }


        public Flight(string flightNumber, string airline, string airportCodeStart, string airportCodeEnd, string day, string time, int seats, float cost)
        {
            this.FlightNumber = flightNumber;
            this.Airline = airline;
            this.AirportCodeStart = airportCodeStart;
            this.AirportCodeEnd = airportCodeEnd;
            this.Day = day;
            this.Time = time;
            this.Seats = seats;
            this.Cost = cost;
        }

        public Flight()
        {

        }

        public override string ToString()
        {
            return $"{FlightNumber}, {Airline}, {AirportCodeStart}, {AirportCodeEnd}, {Day}, {Time}, {Seats}";
        }
    }
}
