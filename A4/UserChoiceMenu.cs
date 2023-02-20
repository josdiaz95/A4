using System.ComponentModel.Design;

namespace A4;

public class UserChoiceMenu
{
    public void Display()
    {
        string choice;
        //string repeat;
        string file = "movies.csv";


        do
        {
            Console.WriteLine("WELCOME TO THE MOVIE LIBRARY");
            Console.WriteLine("-------------------");
            //ask user a question
            Console.WriteLine("1) Show Movies");
            Console.WriteLine("2) Add A Movie");
            Console.WriteLine("Press any other key to end");
            // input response
            choice = Console.ReadLine();

            
            if (choice == "1")
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine("File does not exist");
                }
                else
                {
                    // read data from file
                    AllMovies displayMovies = new AllMovies();
                    displayMovies.DisplayMoviesContent();

                }
            }
            else if (choice == "2")
            {
                System.Console.WriteLine("You entered Choice 2\n");
                AddMovie newMovie = new AddMovie();
                newMovie.CreateNewMovieTitle();
            }
            else 
            {
                Console.WriteLine("\nThank you for using the Movie Library!");
                
            }

        } while (choice == "1" || choice == "2");

    }
}