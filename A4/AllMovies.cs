
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.VisualBasic;

namespace A4
{

    public class MenuLibrary
    {
        public string movieId { get; set; }

        public string title { get; set; }

        public string genres { get; set; }

        public override string ToString()
        {
            string genresString = string.Join("|", genres);
            return "Movie ID: " + movieId + " Title of Movie:  " + title + " Genre/s: " + genresString;
        }

    }


    public class AllMovies
    {
        public void DisplayMoviesContent()
        {
            try
            {
                //searching for a movie with a keyword or tittle

                bool found = false;
                string file = "movies.csv";
                Console.WriteLine("Enter a keyword/title to search for a movie");
                string movieSearch = Console.ReadLine().ToUpper();



                while (movieSearch.Length <= 1)
                {
                    Console.WriteLine("***You must enter a movie title or keyword!***\n");
                    Console.WriteLine("Enter a keyword/title to search for a movie");
                    movieSearch = Console.ReadLine().ToUpper();
                }

                if (movieSearch.Length > 1)
                {
                    {
                        Console.WriteLine("-----------------");
                        Console.WriteLine($"Searching with word: {movieSearch}\n");

                        if (movieSearch.Contains("THE"))
                        {
                            movieSearch = movieSearch.Replace("THE", "");
                        }


                        using (var reader = new StreamReader(file))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {

                            var movies = csv.GetRecords<MenuLibrary>().ToList();

                            foreach (var mt in movies)
                            {
                                var titles = mt.movieId + "-- " + mt.title.ToUpper() + "," + mt.genres;

                                if (titles.Contains(movieSearch))
                                {
                                    found = true;
                                    Console.WriteLine(titles);
                                }

                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("Uh oh! Movie(s) not found.");
                        }

                    }
                }


                Console.WriteLine("");
                Console.WriteLine("*************************************************\n");

            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry, File doesn't exit." + e);
            }
        }
    }
}




    

