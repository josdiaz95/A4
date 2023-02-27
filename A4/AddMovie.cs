using System;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace A4 
{

    public class AddMovie
    {
        public class toFile
        {
            [Index(0)]
            public int movieId { get; set; }

            [Index(1)]
            public string title { get; set; }

            [Index(2)]
            public string genres { get; set; }
        }

        public void CreateNewMovieTitle()
        {
            try
            {
                string file = "movies.csv";

                List<string> genreList = new List<string>();

                //genre looping
                bool loopGenres = true;

                //checking if movie file exist or not
                bool exist = false;

                //string reader for year
                string year;

                //Try adding a movie

                Console.WriteLine("Enter movie title");
                string newMovie = FirstLetterCap(Console.ReadLine());
                Console.WriteLine("Add movie year");
                year = Console.ReadLine();

                //to remove "the" to check for equals without ruining user input
                string compareMovie = newMovie;

                string isThe = FirstLetterCap("the");
                if (compareMovie.Contains(isThe))
                {
                    compareMovie = compareMovie.Remove(0, 4);
                }

                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {

                    var movies = csv.GetRecords<MenuLibrary>().ToList();

                    foreach (var mt in movies)
                    {
                        var titles = mt.title;

                        if (titles.Contains(compareMovie) && titles.Contains(year))
                        {
                            Console.WriteLine("This movie already exist. Try adding another.");
                            Console.WriteLine(titles);
                            exist = true;
                            break;
                        }

                    }

                    if (!exist)
                    {
                        Console.WriteLine("Please add a genre:");
                        string newGenre = FirstLetterCap(Console.ReadLine());
                        genreList.Add(newGenre);
                        //loop genre adding
                        while (loopGenres)
                        {
                            try
                            {
                                Console.WriteLine("Would you like to add a movie genre? (Y/N)");
                                char answer = Convert.ToChar(Console.ReadLine());
                                answer = Char.ToUpper(answer);

                                if (answer == 'Y')
                                {
                                    Console.WriteLine("Enter the movie genre:");
                                    newGenre = FirstLetterCap(Console.ReadLine());
                                    genreList.Add(newGenre);
                                }

                                else if (answer == 'N')
                                {
                                    loopGenres = false;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid key!Try again.");
                                }
                            }


                            catch (Exception e)
                            {
                                Console.WriteLine("You've entered to many characters!Try again.");
                            }

                        }
                    }
                }


                //generating next id
                int nextId;

                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MenuLibrary>().Last();
                    int lastId = Int32.Parse(records.movieId);
                    nextId = 1;
                    nextId += lastId;

                }


                //writing list to add to the file
                var movieRecords = new List<toFile>
            {
                 new toFile { movieId = nextId, title = newMovie + " " + $"({year})", genres =(string.Join("|",genreList)) }
            };

                //capitalizing the first letter of every word
                static string FirstLetterCap(string str1)
                {
                    return string.Join(" ", str1.Split(' ').Select(str1 => char.ToUpper(str1[0]) + str1.Substring(1)));
                }


                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };

                using (var stream = File.Open(file, FileMode.Append))

                using (var writer = new StreamWriter(stream))

                using (var csv = new CsvWriter(writer, config))
                {
                    //if movie doesn't exist add to file --add to file
                    if (!exist)
                    {
                        csv.WriteRecords(movieRecords);
                    }

                }

                //Getting total number of movies
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var count = csv.GetRecords<toFile>().ToList();
                    System.Console.WriteLine($"The total number of movies are: {count.Count()}\n");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There is  no file to add to.");
                
            }
           

        }

    }


}