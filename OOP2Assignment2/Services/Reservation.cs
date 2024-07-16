﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/* 
 * Reservation class
 * Author: Harry Jung
 * This is the class which represents our reservations.
 */

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
        private bool active;
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

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        //When we create a new reservation, we assign it a random reservation code.
        internal Reservation(string flightNumber, string airline, string day, string time, float cost, string Name, string citizenShip)
        {
            this.FlightNumber = flightNumber;
            this.Airline = airline;
            this.Day = day;
            this.Time = time;
            this.Cost = cost;
            this.name = Name;
            this.Citizenship = citizenShip;
            Active = true;
            GenerateReservationCode();
        }

        //This is the constructor our JsonSerializer uses to serialize each reservation to a file.
        [JsonConstructor]
        internal Reservation(string reservationCode, string flightNumber, string airline, string day, string time, float cost, string Name, string citizenShip, bool active)
        {
            this.ReservationCode = reservationCode;
            this.FlightNumber = flightNumber;
            this.Airline = airline;
            this.Day = day;
            this.Time = time;
            this.Cost = cost;
            this.name = Name;
            this.Citizenship = citizenShip;
            this.Active = active;
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
            return $"{ReservationCode} - {Name}, {FlightNumber}, {Airline}, {Day}, {Time}, ${cost}.00";
        }
    }
}
