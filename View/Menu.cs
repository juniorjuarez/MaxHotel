namespace MaxHotel.View
{
    using System;
    using MaxHotel.Controllers;

    internal class Menu
    {
        private enum OptionMenu
        {
            Exit = 0,
            CheckIn = 1,
            CheckOut = 2,
            ViewReservations = 3,
            ListSuites = 4

        }


        public static void ShowMainMenu()
        {
            Hotel meuHotel = new Hotel();

            bool ChoiceExit = false;
            while (!ChoiceExit)
            {

                Console.WriteLine("\n============================================");
                Console.WriteLine("\nSystem Menu");
                Console.WriteLine("1. Check In");
                Console.WriteLine("2. Check Out");
                Console.WriteLine("3. View Reservations");
                Console.WriteLine("4. List Suites");
                Console.WriteLine("0. Exit");
                Console.Write("Please select an option: ");
                Console.WriteLine("\n=============================================");


                if (Enum.TryParse<OptionMenu>(Console.ReadLine(), out var option))
                {
                    switch (option)
                    {
                        case OptionMenu.CheckIn:
                            Console.WriteLine("You selected Check In.");
                            meuHotel.RealizaCheckIn();
                            break;
                        case OptionMenu.CheckOut:
                            Console.WriteLine("You selected Check Out.");
                            meuHotel.RealizaCheckOut();
                            break;
                        case OptionMenu.ViewReservations:
                            Console.WriteLine("You selected View Reservations.");
                            meuHotel.ListaReservas();
                            break;
                        case OptionMenu.ListSuites:
                            Console.WriteLine("You selected View Reservations.");
                            meuHotel.LitaSuitesDisponiveis();
                            break;
                        case OptionMenu.Exit:
                            Console.WriteLine("Exiting the application. Goodbye!");
                            ChoiceExit = true;
                            break;
                        default:

                            Console.WriteLine("Opção não reconhecida. Pressione qualquer tecla para tentar novamente.");
                            Console.ReadKey();
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to the menu options.");
                }




            }
            Console.WriteLine("Thank you for using MaxHotel!");
            Console.Clear();
        }
    }
}