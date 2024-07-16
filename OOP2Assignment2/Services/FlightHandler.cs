using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using CsvHelper;
using OOP2Assignment2.Components.Pages;


/* 
 * FlightHandler class
 * Author: Harry Jung
 * The heart of our program. Injected into our .razor components, and stores Flight objects in a List.
 * Reads and writes from CSV.
 */

namespace OOP2Assignment2.Services
{
    public class FlightHandler
    {
        internal List<Flight> flights = new List<Flight>();
        
        // read from CSV and create a Flight object using the Flight() constructor and the fields from each line of the CSV.
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
            //case insensitive search, return flights which match our 3 inputs, using "any" as a wildcard.
            incoming = incoming.ToUpper();
            outgoing = outgoing.ToUpper();
            day = day.ToLower();
            List<Flight> matchingFlights = new List<Flight>();
            foreach (Flight flight in flights)
            { 
                if (flight.AirportCodeStart == incoming || incoming == "ANY")
                {
                    if (flight.AirportCodeEnd == outgoing || outgoing == "ANY")
                    {
                        if (flight.Day.ToLower() == day || day == "any")
                        {
                            matchingFlights.Add(flight);
                        }
                    }
                }
            }

            return matchingFlights;
        }

        //find a flight solely by flight number.
        internal Flight findFlights(string flightNumber)
        {
            flightNumber = flightNumber.ToUpper();
            foreach (Flight flight in flights)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    return flight;
                }
            }
            return new Flight();
        }

        //Find a flight and subtract 1 from its available seats, then update the CSV.
        internal void ReserveSeat(string flightNumber)
        {
            flightNumber = flightNumber.ToUpper();
            bool foundFlight = false;
            foreach (Flight flight in flights)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    foundFlight = true;
                    flight.Seats--;
                    break;
                }
            }

            if (foundFlight)
                WriteToFile();
            
        }

        //same as above, but add a seat.
        internal void FreeSeat(string flightNumber)
        {
            bool foundFlight = false;
            foreach (Flight flight in flights)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    foundFlight = true;
                    flight.Seats++;
                    break;
                }
            }

            if (foundFlight)
                WriteToFile();
        }

        //write the stored flights to the file.
        //Join all properties with commas and write back to the CSV.
        internal void WriteToFile()
        {
            try
            {
                //make an array of each of the Flight object's properties, then join them and add to an array of flight csv strings.
                //Truncate to file and write.
                List<string> flightStrings = new List<string>();
                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "csv", "flights.csv");
                foreach (Flight flight in flights)
                {
                    string[] fields = [flight.FlightNumber, flight.Airline, flight.AirportCodeStart, flight.AirportCodeEnd, flight.Day, flight.Time, flight.Seats.ToString(), flight.Cost.ToString()];
                    flightStrings.Add(string.Join(",", fields));
                }
                if (flightStrings.Count() > 0)
                {
                    File.WriteAllLines(filepath, flightStrings);
                    ReadCSV();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading CSV: {e.Message}");
                return;
            }
        }

    }
}