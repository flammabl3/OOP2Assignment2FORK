﻿using System;
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
        private int unsure;
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

        public int Unsure
        {
            get { return unsure; }
            set { unsure = value; }
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }


        public Flight(string flightNumber, string airline, string airportCodeStart, string airportCodeEnd, string day, string time, int unsure, float cost)
        {
            this.flightNumber = flightNumber;
            this.airline = airline;
            this.airportCodeStart = airportCodeStart;
            this.airportCodeEnd = airportCodeEnd;
            this.day = day;
            this.time = time;
            this.unsure = unsure;
            this.cost = cost;
        }

        public Flight()
        {

        }

        public override string ToString()
        {
            return $"{FlightNumber}, {Airline}, {AirportCodeStart}, {AirportCodeEnd}, {Day}, {Time}, {Unsure}";
        }
    }
}
