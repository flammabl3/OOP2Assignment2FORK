using CsvHelper;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/* 
 * ReservationHandler class
 * Author: Harry Jung
 * The heart of our program. Injected into our .razor components, and stores Flight objects in a List.
 * Reads and writes from CSV.
 */

namespace OOP2Assignment2.Services
{
    public class ReservationHandler
    {
        //stores all Reservations in a list.
        internal List<Reservation>? reservations = new List<Reservation>();

        //Add a reservation to the file.
        internal void WriteToFile(Reservation reservation)
        {

            // Check to see we have no duplicates.
            bool alreadyTaken = false;


            //Check if the new reservation has an identical code to any of the current ones. This is highly improbable, but should still be checked.
            do {
                foreach (Reservation res in reservations)
                {
                    if (reservation.ReservationCode == res.ReservationCode)
                    {
                        alreadyTaken = true;
                        reservation.GenerateReservationCode();
                        break;
                    }   
                }
                alreadyTaken = false;
            } while (alreadyTaken);


            reservations.Add(reservation);

            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "binaryfiles", "reservations.txt");

            //truncate the file and serialize reservations to the file.
            try
            {
                string jsonString = JsonSerializer.Serialize(reservations);
                File.WriteAllText(filepath, jsonString);
                return;
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error reading Binary File: {e.Message}");
                return;
            }
        }

        // Same logic as above, but we replace an existing reservation with an updated version, truncate, and serialize.
        internal void UpdateToFile(Reservation reservation)
        {
            for (int i = 0; i < reservations.Count(); i++)
            {
                if (reservations[i].ReservationCode == reservation.ReservationCode)
                {
                    reservations[i] = reservation;
                    string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "binaryfiles", "reservations.txt");

                    try
                    {
                        string jsonString = JsonSerializer.Serialize(reservations);
                        File.WriteAllText(filepath, jsonString);
                        ReadFromFile();
                        return;
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine($"Error reading Binary File: {e.Message}");
                        return;
                    }
                }
            }

            throw new System.Exception("No entry found.");
        }
        
        //Same logic, set inactive, truncate, serialize.
        internal void DeleteFromFile(Reservation reservation)
        {
            for (int i = 0; i < reservations.Count(); i++)
            {
                if (reservations[i].ReservationCode == reservation.ReservationCode)
                {
                    reservations[i].Active = false;
                    string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "binaryfiles", "reservations.txt");

                    try
                    {
                        string jsonString = JsonSerializer.Serialize(reservations);
                        File.WriteAllText(filepath, jsonString);
                        ReadFromFile();
                        return;
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine($"Error reading Binary File: {e.Message}");
                        return;
                    }
                }
            }

            throw new System.Exception("No entry found.");
        }

        //Deserialize reservations from the file in JSON format. File is a one line array of JSON objects.
        internal void ReadFromFile()
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "binaryfiles", "reservations.txt");

            try
            {
                string jsonString = File.ReadAllText(filepath);
                reservations.Clear();
                reservations = JsonSerializer.Deserialize<List<Reservation>>(jsonString);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading binary: {e.Message}");
                return;
            }
        }

        //case insensitive search for a reservation.
        internal List<Reservation> FindReservation(string code, string airline, string name)
        {
            code = code.ToUpper();
            airline = airline.ToLower();
            name = name.ToLower();
            List<Reservation> matchingReservations = new List<Reservation>();

            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationCode == code || code == "ANY")
                {
                    if (reservation.Airline.ToLower() == airline || airline == "any")
                    {
                        if (reservation.Name.ToLower() == name || name == "any")
                        {
                            matchingReservations.Add(reservation);
                        }
                    }
                }
            }

            return matchingReservations;
        }

        //search with just the code.
        internal Reservation FindReservation(string code)
        {
            code = code.ToUpper();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationCode == code)
                {
                    return reservation;
                }
            }

            return new Reservation();
        }

    }

}
