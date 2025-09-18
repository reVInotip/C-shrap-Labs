using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Interface;

/// <summary>
/// Load all philosophers from file
/// </summary>
/// <typeparam name="T">Defines a concrete implementation of the IPhilosopher interface</typeparam>
public static class Loader<T> where T : class, IPhilosopher
{
    public static List<T> philosophers = new();

    /// <summary>
    ///     Load philosophers from file
    /// </summary>
    /// <param name="filePath"></param>
    /// <throws>ArgumentException</throws>
    public static void LoadPhilosophersFromFile(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            if (reader.Peek() < 0)
                throw new ArgumentException("Configuration file is empty");

            string? line = null;

            do
            {
                line = reader.ReadLine();

                // add some validation here

                if (line is not null)
                    philosophers.Add((T)T.Create(line));
            }
            while (line is not null);
        }
    }
}
