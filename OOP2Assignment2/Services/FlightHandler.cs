using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using CsvHelper;
using OOP2Assignment2.Components.Pages;

namespace OOP2Assignment2.Services
{
    public class FlightHandler
    {
        internal List<Flight> flights = new List<Flight>();
        internal void ReadCSV()
        {
            flights.Clear();
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "csv", "flights.csv");

            try
            {
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
                    {
                        flights.Add(new Flight(csv.GetField(0), csv.GetField(1), csv.GetField(2), csv.GetField(3), csv.GetField(4), csv.GetField(5), Int32.Parse(csv.GetField(6)), float.Parse(csv.GetField(7))));
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading CSV: {e.Message}");
                return;
            }
        }

        internal List<Flight> findFlights(string incoming, string outgoing, string day)
        {
            List<Flight> matchingFlights = new List<Flight>();
            foreach (Flight flight in flights)
            { 
                if (flight.AirportCodeStart == incoming || incoming == "Any")
                {
                    if (flight.AirportCodeEnd == outgoing || outgoing == "Any")
                    {
                        if (flight.Day == day || day == "Any")
                        {
                            matchingFlights.Add(flight);
                        }
                    }
                }
            }

            return matchingFlights;
        }

        internal Flight findFlights(string flightNumber)
        {
            foreach (Flight flight in flights)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    return flight;
                }
            }
            return new Flight();
        }
        internal void ReserveSeat(string flightNumber)
        {
            bool foundFlight = false;
            foreach (Flight flight in flights)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    foundFlight = true;
                    flight.Seats--;
                }
            }

            if (foundFlight)
                WriteToFile();
            
        }

        internal void FreeSeat(string flightNumber)
        {
            bool foundFlight = false;
            foreach (Flight flight in flights)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    foundFlight = true;
                    flight.Seats++;
                }
            }

            if (foundFlight)
                WriteToFile();
        }

        //write the stored flights to the file
        //Join all properties with commas and write back to the CSV.
        internal void WriteToFile()
        {
            try
            {
                string[] flightStrings = [];
                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "csv", "flights.csv");
                foreach (Flight flight in flights)
                {
                    string[] fields = [flight.FlightNumber, flight.Airline, flight.AirportCodeStart, flight.AirportCodeEnd, flight.Day, flight.Time, flight.Seats.ToString(), flight.Cost.ToString()];
                    flightStrings.Append(string.Join(",", fields));
                }
                File.WriteAllLines(filepath, flightStrings);


            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading CSV: {e.Message}");
                return;
            }
        }

    }
}