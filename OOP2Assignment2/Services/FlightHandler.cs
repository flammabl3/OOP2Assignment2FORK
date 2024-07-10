using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using CsvHelper;

namespace OOP2Assignment2.Services
{
    public class FlightHandler
    {
        internal List<Flight> ReadCSV()
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "csv", "flights.csv");

            try
            {
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    List<Flight> flights = new List<Flight>();
                    while (csv.Read())
                    {
                        flights.Add(new Flight(csv.GetField(0), csv.GetField(1), csv.GetField(2), csv.GetField(3), csv.GetField(4), csv.GetField(5), Int32.Parse(csv.GetField(6)), float.Parse(csv.GetField(7))));
                    }
                    return flights;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading CSV: {e.Message}");
                return new List<Flight>(); 
            }
        }
    }
}