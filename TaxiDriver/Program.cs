using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TaxiDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect to the local Database
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database.mdf;Integrated Security=True");
            conn.Open();
            

            int exit = 0;
            do
            {
                Console.Clear();
                //Display Menu
                Console.WriteLine("TAXI DRIVER APPLICATION  \n");
                for (int i = 0; i < 23; i++)
                {
                    Console.Write("#");
                }
                Console.WriteLine("\n");
                for (int b = 1; b < 7; b++)
                {
                    switch (b)
                    {
                        case 1:
                            Console.WriteLine(b + ": Add a driver\n");
                            break;
                        case 2:
                            Console.WriteLine(b + ": Edit a driver\n");
                            break;
                        case 3:
                            Console.WriteLine(b + ": Add a trip\n");
                            break;
                        case 4:
                            Console.WriteLine(b + ": Import trips\n");
                            break;
                        case 5:
                            Console.WriteLine(b + ": View all trips for a driver\n");
                            break;
                        case 6:
                            Console.WriteLine(b + ": EXIT\n");
                            break;
                        default:
                            break;
                    }

                }

                //Verify Entry
                int choise = 0;
                do
                {
                    Console.WriteLine("What's your choise ?");
                    string user = Console.ReadLine();
                    choise = int.Parse(user);
                    if (choise < 1 || choise > 6)
                    {
                        Console.WriteLine("Choose a number between 1 and 5 \n");
                    }
                } while (choise < 1 || choise > 6);

                switch (choise)
                {
                    case 1:
                        Console.Clear();

                        //Set Last Name
                        string lastName = "";
                        bool isFullCharLN = false;
                        do
                        {
                            Console.WriteLine("Last Name : ");
                            lastName = Console.ReadLine();
                            Console.WriteLine("\n");
                            isFullCharLN = lastName.All(char.IsLetter);
                        } while (isFullCharLN == false || lastName == "");

                        //Set First name
                        string firstName = "";
                        bool isFullCharFN = false;
                        do
                        {
                            Console.WriteLine("First name : ");
                            firstName = Console.ReadLine();
                            Console.WriteLine("\n");
                            isFullCharFN = firstName.All(char.IsLetter);
                        } while (isFullCharFN == false || firstName == "");

                        //Set Car Model
                        string carModel = "";
                        do
                        {
                            Console.WriteLine("Car model : ");
                            carModel = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (carModel == null || carModel == "");

                        //Set Age
                        int age = 0;
                        do
                        {
                            Console.WriteLine("Age : ");
                            age = int.Parse(Console.ReadLine());
                            Console.WriteLine("\n");
                        } while (age == 0);

                        //Set Salary
                        int salary = 0;
                        do
                        {
                            Console.WriteLine("Salary : ");
                            salary = int.Parse(Console.ReadLine());
                            Console.WriteLine("\n");
                        } while (salary == 0);

                        //Set Campus
                        string campus = "";
                        do
                        {
                            Console.WriteLine("Campus : ");
                            campus = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (campus == null || campus == "");

                        //Set City
                        int cityNum = 0;
                        do
                        {
                            Console.WriteLine("Choose city : \n 1.Marseille\n 2.Paris\n 3.Lyon \n");
                            cityNum = int.Parse(Console.ReadLine());
                            Console.WriteLine("\n");
                        } while (cityNum < 1 || cityNum > 3);

                        string city = "";
                        switch (cityNum)
                        {
                            case 1:
                                city = "Marseille";
                                break;
                            case 2:
                                city = "Paris";
                                break;
                            case 3:
                                city = "Lyon";
                                break;
                            default:
                                break;
                        }

                        //Insert
                        string addStr = @"INSERT INTO Driver(Last_name , First_name , Car_model , Age , Salary , Campus , City) VALUES ('" + lastName + "' ,'" + firstName + "' ,'" + carModel + "' ,'" + age + "' ,'" + salary + "' ,'" + campus + "', '" + city + "')";
                        SqlCommand cmd = new SqlCommand(addStr, conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        break;
                    case 2:
                        Console.Clear();
                        //Edit a Driver
                        Console.WriteLine("Choose the driver you want edit : \n");

                        //Display all driver
                        string selectStr = @"Select * from Driver";
                        SqlCommand cmdSelect = new SqlCommand(selectStr, conn);
                        SqlDataReader rdr = cmdSelect.ExecuteReader();
                        while (rdr.Read())
                        {
                            int idRdr = (int)rdr["Id"];
                            string firstNameRdr = (string)rdr["First_name"];
                            string lastNameRdr = (string)rdr["Last_name"];
                            Console.WriteLine(" id : {1}, last name : {0}, first name : {2} ", lastNameRdr, idRdr, firstNameRdr);
                        }
                        rdr.Close();
                        
                        Console.WriteLine("\nRewrite his id :");
                        int idEntry = int.Parse(Console.ReadLine());

                        int editChoise = 0;
                        
                        //Modification loop
                        do
                        {
                            Console.Clear();
                            selectStr = @" Select First_name , Last_name from Driver where Id = " + idEntry;
                            cmdSelect = new SqlCommand(selectStr, conn);
                            SqlDataReader rdrUp = cmdSelect.ExecuteReader();
                            while (rdrUp.Read())
                            {
                                string firstNameRdrUp = (string)rdrUp["First_name"];
                                string lastNameRdrUp = (string)rdrUp["Last_name"];
                                Console.WriteLine("What would you edit for {0} {1} ?", lastNameRdrUp, firstNameRdrUp);
                            }
                            rdrUp.Close();
                            Console.WriteLine("1. Last Name");
                            Console.WriteLine("2. First Name");
                            Console.WriteLine("3. Car model");
                            Console.WriteLine("4. Age");
                            Console.WriteLine("5. Salary");
                            Console.WriteLine("6. Campus");
                            Console.WriteLine("7. City");
                            Console.WriteLine("8. Exit");
                            Console.WriteLine("Choose a number :");
                            editChoise = int.Parse(Console.ReadLine());

                            string updateStr = "";
                            string tableStr = "";
                            string editContent = "";
                            int editContentNum = 0;
                            switch (editChoise)
                            {
                                case 1:
                                    //Set new last name
                                    Console.WriteLine("Set new last name :");
                                    tableStr = "Last_name";
                                    editContent = Console.ReadLine();
                                    break;
                                case 2:
                                    //Set new First name
                                    Console.WriteLine("Set new first name :");
                                    tableStr = "First_name";
                                    editContent = Console.ReadLine();

                                    break;
                                case 3:
                                    //Set new car
                                    Console.WriteLine("Set new car model :");
                                    tableStr = "Car_model";
                                    editContent = Console.ReadLine();
                                    break;
                                case 4:
                                    //Set new age
                                    Console.WriteLine("Set new age :");
                                    tableStr = "Age";
                                    editContentNum = int.Parse(Console.ReadLine());
                                    break;
                                case 5:
                                    //Set new salary
                                    Console.WriteLine("Set new salary :");
                                    tableStr = "Salary";
                                    editContentNum = int.Parse(Console.ReadLine());
                                    break;
                                case 6:
                                    //Set new capus
                                    Console.WriteLine("Set new campus :");
                                    tableStr = "Campus";
                                    editContent = Console.ReadLine();
                                    break;
                                case 7:
                                    int cityUpNum = 0;
                                    do
                                    {
                                        //set new city
                                        Console.WriteLine("Set new city :\n1. Marseille\n 2. Paris\n 3.Lyon ");
                                        tableStr = "City";
                                        cityUpNum = int.Parse(Console.ReadLine());
                                        switch (cityUpNum)
                                        {
                                            case 1:
                                                editContent = "Marseille";
                                                break;
                                            case 2:
                                                editContent = "Paris";
                                                break;
                                            case 3:
                                                editContent = "Lyon";
                                                break;
                                            default:
                                                break;
                                        }
                                    } while (cityUpNum < 1 || cityUpNum > 3);
                                    break;
                                case 8:
                                    break;
                                default:
                                    break;

                            }
                                    
                            if (tableStr != "" && editContent != "")
                            {
                                //If content is String
                                updateStr = @"Update Driver SET " + tableStr + " = '" + editContent + "' WHERE Id =" + idEntry;
                                SqlCommand cmdUp = new SqlCommand(updateStr, conn);
                                cmdUp.ExecuteNonQuery();
                                cmdUp.Dispose();
                            }
                            else if (tableStr != "" && editContentNum != 0)
                            {
                                //If content is number
                                updateStr = @"Update Driver SET " + tableStr + " = '" + editContentNum + "' WHERE Id =" + idEntry;
                                SqlCommand cmdUp = new SqlCommand(updateStr, conn);
                                cmdUp.ExecuteNonQuery();
                                cmdUp.Dispose();
                            }
       

                            updateStr = "";
                            tableStr = "";
                            editContent = "";
                            editContentNum = 0;

                        } while (editChoise != 8);
                        


                        break;
                    case 3:
                        Console.Clear();
                        //Add Trip

                        //Set Departure
                        string departureStr = "";
                        do
                        {
                            Console.WriteLine("Departure address :");
                            departureStr = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (departureStr == null || departureStr == "");

                        //Set departure address
                        string departureDateStr = "";
                        do
                        {
                            Console.WriteLine("Departure time : (Hours:Mins)");
                            departureDateStr = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (departureDateStr == null || departureDateStr == "");

                        //Set departure date
                        string arrivalStr = "";
                        do
                        {
                            Console.WriteLine("Arrival address :");
                            arrivalStr = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (arrivalStr == null || arrivalStr == "");

                        //Set arrival date
                        string arrivalDateStr = "";
                        do
                        {
                            Console.WriteLine("Arrival time: (Hours:Mins)");
                            arrivalDateStr = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (arrivalDateStr == null || arrivalDateStr == "");

                        //set client first name
                        string clientFirstNameStr = "";
                        do
                        {
                            Console.WriteLine("Client first name :");
                            clientFirstNameStr = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (clientFirstNameStr == null || clientFirstNameStr == "");

                        //Set client last name
                        string clientLastNameStr = "";
                        do
                        {
                            Console.WriteLine("Client last name :");
                            clientLastNameStr = Console.ReadLine();
                            Console.WriteLine("\n");
                        } while (clientLastNameStr == null || clientLastNameStr == "");

                        //insert Trip
                        string addTripStr = "Insert into Trips(Departure , Departure_time, Arrival , Arrival_time , Client_first_name , Client_last_name) VALUES ('"+departureStr+"','"+departureDateStr+"','"+arrivalStr+"','"+arrivalDateStr+"','"+clientFirstNameStr+"','"+clientLastNameStr+"')";
                        SqlCommand cmdAddTrip = new SqlCommand(addTripStr, conn);
                        cmdAddTrip.ExecuteNonQuery();


                        cmdAddTrip.Dispose();
                        break;
                        
                    case 5:
                        Console.Clear();

                        //Display trip assign to driver

                        //Show all driver
                        string selectDriverStr = @"Select * from Driver";
                        SqlCommand cmdSelectDriver = new SqlCommand(selectDriverStr, conn);
                        SqlDataReader rdrDriver = cmdSelectDriver.ExecuteReader();
                        while (rdrDriver.Read())
                        {
                            int idRdrDriver = (int)rdrDriver["Id"];
                            string firstNameRdrDriver = (string)rdrDriver["First_name"];
                            string lastNameRdrDriver = (string)rdrDriver["Last_name"];
                            Console.WriteLine(" id : {1}, last name : {0}, first name : {2} ", lastNameRdrDriver, idRdrDriver, firstNameRdrDriver);
                        }
                        rdrDriver.Close();

                        int correct = 0;
                        string tableToSearch = "";
                        string driverId ="";
                        int driverNumId = 0;
                        do
                        {
                            //Select driver
                            Console.WriteLine("\n Enter a Driver Id or a Driver name ( last name only ): ");
                            driverId = Console.ReadLine();
                            int num;
                            bool isNum = int.TryParse(driverId.ToString(), out num);
                            bool isAllchar = driverId.All(char.IsLetter);
                            if (isNum)
                            {
                                //if is a number
                                tableToSearch = "Id";
                                driverNumId = int.Parse(driverId);
                                correct = 2;
                            }
                            else if (isAllchar)
                            {
                                //If is only letter
                                tableToSearch = "Last_name";
                                correct = 1;
                            }
                            else
                            {
                               
                                Console.WriteLine("\n Driver Id or Driver name incorrect.");
                            }
                        } while (correct == 0);

                        string selectSearchStr = "";
                        if (correct == 1)
                        {
                            //string
                            selectSearchStr = @"Select City from Driver Where "+tableToSearch+" = '"+driverId+"'";
                        }
                        else if (correct == 2 || driverNumId != 0)
                        {
                            //number
                            selectSearchStr = @"Select City from Driver Where " + tableToSearch + " = '" + driverNumId + "'";
                        }

                        //Get Driver city
                        SqlCommand cmdSearchSelect = new SqlCommand(selectSearchStr, conn);
                        SqlDataReader rdrSearch = cmdSearchSelect.ExecuteReader();
                        string cityRdrSearch = "";
                        while (rdrSearch.Read())
                        {
                            cityRdrSearch = (string)rdrSearch["City"];
                        }
                        rdrSearch.Close();
                        string selectTripStr = "";
                        if (cityRdrSearch != "")
                        {
                            //Select where
                            selectTripStr = @"select * from Trips where Departure LIKE '%" + cityRdrSearch +"%' OR Arrival LIKE '%" + cityRdrSearch + "%'";
                        }

                        //Get Trip for Driver
                        SqlCommand cmdSelectTrip = new SqlCommand(selectTripStr, conn);
                        SqlDataReader rdrTripFound = cmdSelectTrip.ExecuteReader();
                        int count = 0;
                        while (rdrTripFound.Read())
                        {
                            count++;
                            string tripDeparture = (string)rdrTripFound["Departure"];
                            string tripDepartureTime = (string)rdrTripFound["Departure_time"].ToString();
                            string tripArrival = (string)rdrTripFound["Arrival"];
                            string triparrivalTime = (string)rdrTripFound["Arrival_time"].ToString();
                            string tripClientLastName = (string)rdrTripFound["Client_last_name"];
                            string tripClientFirstName = (string)rdrTripFound["Client_first_name"];
                            Console.WriteLine("------------------------{0}-----------------------", count);
                            Console.WriteLine("Departure : {0} ,\n Departure time : {1} ,\n Arrival : {2} ,\n Arrival time : {3} ,\n Client first name : {4} ,\n Client last name : {5}\n ", tripDeparture, tripDepartureTime, tripArrival, triparrivalTime, tripClientFirstName, tripClientLastName);

                        }
                        Console.ReadLine();
                        break;
                    case 6:
                        exit = 1;
                        break;
                }
            } while (exit != 1);
            conn.Close();
            Console.ReadKey();                
        }

    }
}
