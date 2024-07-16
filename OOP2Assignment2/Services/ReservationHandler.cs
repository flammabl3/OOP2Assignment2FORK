using CsvHelper;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP2Assignment2.Services
{
    public class ReservationHandler
    {
        internal List<Reservation>? reservations = new List<Reservation>();
        internal void WriteToFile(Reservation reservation)
        {

            // Check to see we have no duplicates.
            bool alreadyTaken = false;

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

        internal void DeleteFromFile(Reservation reservation)
        {
            for (int i = 0; i < reservations.Count(); i++)
            {
                if (reservations[i].ReservationCode == reservation.ReservationCode)
                {
                    reservations.RemoveAt(i);
                    string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../..", "resources", "binaryfiles", "reservations.txt");

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
            }

            throw new System.Exception("No entry found.");
        }

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

        internal List<Reservation> FindReservation(string code, string airline, string name)
        {
            List<Reservation> matchingReservations = new List<Reservation>();

            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationCode == code || code == "Any")
                {
                    if (reservation.Airline == airline || airline == "Any")
                    {
                        if (reservation.Name == name || name == "Any")
                        {
                            matchingReservations.Add(reservation);
                        }
                    }
                }
            }

            return matchingReservations;
        }

        internal Reservation FindReservation(string code)
        {
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
