/*
Copyright (C) <2015>  <gary@duncodin.it>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Part2SourceCode
{
    class Program
    {
        /// <summary>
        /// Entry point. Run the required functions
        /// </summary>
        static void Main()
        {
            List<string> data = GetData();
            string top10 = GetTop10Locations(data);
            string bottom10 = GetBottom10Locations(data);
            GetAllLocations(data);
        }

        /// <summary>
        /// Take a list of locations. Group the location. Create
        /// an output file of locations and number of posts from location
        /// </summary>
        /// <param name="data">a list of string locations</param>
        private static void GetAllLocations(List<string> data)
        {
            string path = @"C:\Users\Gary\Documents\BlogPostExamples\"
                + @"Oscars2015\Part2\locations.data";

            StreamWriter sw = File.AppendText(path);
            data
                .AsParallel()
                .GroupBy(location => location)
                .OrderByDescending(group => group.Count())
                .ToList()
                .ForEach(group => sw.Write(String.Format(
                            "{0}\u00B1{1}\n",
                            group.Key,
                            group.Count())));
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Take a list of locations. Group the locations. Take the top 10.
        /// Return a string of name and number of posts from each location
        /// </summary>
        /// <param name="data">A list of string locations</param>
        /// <returns>A string of key value pairs. Key = locations, value
        /// = number of posts from location</returns>
        private static string GetBottom10Locations(List<string> data)
        {
            return data
                .GroupBy(location => location)
                .OrderBy(group => group.Count())
                .ToList()
                .FindAll(group => group.Count() > 1)
                .Take(10)
                .ToList()
                .Aggregate(
                    string.Empty,
                    (acc, group) =>
                        acc += String.Format(
                            "{0}~{1}\n",
                            group.Key,
                            group.Count()));
        }

        /// <summary>
        /// Take a list of locations. Group the locations. Take the bottom 10.
        /// Return a string of name and number of posts from each location
        /// </summary>
        /// <param name="data">A list of string locations</param>
        /// <returns>A string of key value pairs. Key = locations, value
        /// = number of posts from location</returns>
        private static string GetTop10Locations(List<string> data)
        {
            return data
                .GroupBy(location => location)
                .OrderByDescending(group => group.Count())
                .ToList()
                .Take(10)
                .ToList()
                .Aggregate(
                    string.Empty,
                    (acc, group) =>
                        acc += String.Format(
                            "{0}~{1}\n",
                            group.Key,
                            group.Count()));
        }

        /// <summary>
        /// Open a file. Return all lines as a list of strings
        /// </summary>
        /// <returns>A list of string locations</returns>
        private static List<string> GetData()
        {
            string path = @"C:\Users\Gary\Documents\BlogPostExamples\"
                + @"Oscars2015\Part2\locsNoBlanks.txt";

            return File.ReadAllLines(path).ToList();
        }
    }
}
