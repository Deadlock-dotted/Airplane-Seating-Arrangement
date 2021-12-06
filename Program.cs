using System;

namespace AirplaneSeating
{
    class Program
    {
        #region Static Variables

        static int filled = 0;
        static int seatsAvailable = 0;

        #endregion

        #region Main Method
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char[,] seats = new char[GlobalConstants.AeroplaneRow,GlobalConstants.AeroplaneColumn];
            for (int i = 0; i < GlobalConstants.AeroplaneRow; i++)
            {
                seats[i,0] = GlobalConstants.AisleSeat;
                seats[i,1] = GlobalConstants.MiddleSeat;
                seats[i,2] = GlobalConstants.WindowSeat;
            }

            Console.WriteLine("Airplane Reservation System");
            Console.WriteLine("Please enter the seat ((ie) 1A) you wish to reserve.");
            Console.WriteLine("Enter E to exit.");
            string seatNumber = Console.ReadLine();

            if (seatNumber.Equals(GlobalConstants.OptOut))
            {
                Console.WriteLine("You have Opted Out from Reservation Process");
                Console.ReadKey();
            }

            while (filled < GlobalConstants.MaximumPassengers && ValidateSeatNumber(seatNumber))
            {
                int row = seatNumber[0] - GlobalConstants.RowConstant;
                int col = seatNumber[1] - GlobalConstants.ColumnConstant;
                
                if (row < GlobalConstants.MinimunRow || row > GlobalConstants.MaximumRow || col < GlobalConstants.MinimumColumn || col > GlobalConstants.MaximumColumn)
                {
                    Console.WriteLine("Input error. Please Provide Valid Inputs ");
                    Console.ReadKey();
                }
                
                fillAisleFirst(seats, row, col); // Fill Aisle Followed By Window Seats and then Middle Seats

                if (filled < GlobalConstants.MaximumPassengers)
                {
                    Console.WriteLine("Please enter the seat ((ie) 1A) you wish to reserve." + "or E to quit.");
                    seatNumber = Console.ReadLine();
                    if (seatNumber.Equals(GlobalConstants.OptOut))
                    {
                        Console.WriteLine("Program ended.");
                        Console.ReadKey();
                    }
                }
                
            }
        }

#endregion

        #region Private Methods

        /// <summary>
        /// Printing the Updating Seats
        /// </summary>
        /// <param name="seats"></param>
        private static void PrintSeats(char[,] seats)
        {
            for (int i = 0; i < GlobalConstants.MaximumRow; i++)
            {   
                Console.WriteLine((i + 1) + "  " + seats[i,0] + " " + seats[i,1] + "  " + seats[i,2]);
            }
            seatsAvailable = (GlobalConstants.MaximumPassengers - filled);
            Console.WriteLine("There are " + seatsAvailable + " seats available.");
        }

        /// <summary>
        /// Fill Aisle Seats First Functionality Followed by Windows Seats
        /// </summary>
        /// <param name="seats"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void fillAisleFirst(char[,] seats, int row, int col)
        {
            if (seats[row, col] == GlobalConstants.MiddleSeat)
            {
                if (seats[row, col - 1] != GlobalConstants.Reserved)
                {
                    col = col - 1;
                    FillFrontToBack(seats, row, col);
                    filled++;
                    PrintSeats(seats);
                }
                else if (seats[row, col + 1] != GlobalConstants.Reserved)
                {
                    col = col + 1;
                    FillFrontToBack(seats, row, col);
                    filled++;
                    PrintSeats(seats);
                }
                else
                {
                    FillFrontToBack(seats, row, col);
                    filled++;
                    PrintSeats(seats);
                }
            }
            else if (seats[row, col] == GlobalConstants.WindowSeat)
            {
                if (seats[row, col - 2] != GlobalConstants.Reserved)
                {
                    col = col - 2;
                    FillFrontToBack(seats, row, col);
                    filled++;
                    PrintSeats(seats);
                }
                else if(seats[row,col] != GlobalConstants.Reserved)
                {
                    FillFrontToBack(seats, row, col);
                    filled++;
                    PrintSeats(seats);
                }
                else 
                {
                    col = col - 1;
                    FillFrontToBack(seats, row, col);
                    filled++;
                    PrintSeats(seats);
                }
            }
            else if(seats[row, col] == GlobalConstants.AisleSeat)
            {
                FillFrontToBack(seats, row, col);
                filled++;
                PrintSeats(seats);
            }
            else
            {
                Console.WriteLine("Selected Seat is already booked. Please choose another seat");
            }
        }

        /// <summary>
        /// Validate Seat Number
        /// </summary>
        /// <param name="seatNumber"></param>
        /// <returns></returns>
        private static bool ValidateSeatNumber(string seatNumber)
        {
            try
            {
                char convertedColumn = Convert.ToChar(seatNumber[1]);
                int convertedRow = seatNumber[0] - GlobalConstants.RowConstant;
                if (seatNumber.Length ==  2 && char.IsLetter(convertedColumn))
                {
                    if (convertedRow <= GlobalConstants.MaximumRow &&
                        convertedColumn == GlobalConstants.AisleSeat ||
                        convertedColumn == GlobalConstants.MiddleSeat ||
                        convertedColumn == GlobalConstants.WindowSeat)
                        return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Fill from Front to Back Rows
        /// </summary>
        /// <param name="seats"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private static void FillFrontToBack(char[,] seats, int row, int col)
        {
            for(int i = 0; i <= row; i++)
            {
                if (seats[i, col] != GlobalConstants.Reserved)
                {
                    seats[i, col] = GlobalConstants.Reserved;
                    break;
                }
                while (i == row)
                {
                    seats[row, col] = GlobalConstants.Reserved;
                    break;
                }
            }
            
        }

        #endregion
    }
}

