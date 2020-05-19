using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;

namespace NewSoftware
{
    class Program
    {
        public static SqlCommand cmd;
        // cmd is type of SqlCommand Object that is used in the program to store the SQL Query.
        
        public static SqlDataReader dr;
        // dr is the SqlDataReader object which is used to read the set of data which is retrieved from the database

        public static Dictionary<string, string> ColumnNamesAndDataTypes = new Dictionary<string, string>();
        // ColumnNamesAndDataTypes is the Dictionary object which is used to store the Table structure/ Description in the form of 
        // <string,string> which represents <ColumnName,DataType>
        
        public static string dbname = "testdb";
        // At any point of time while the software/ Program is running dbname contains the Present coonected database name.

        public static string message;
        // message is string variable which stores the information/message which is required to display on the output-screen/console.
        
        public static string message1;
        // The purpose of  message1 is also similar to the message variable.

        public static SqlConnection con;
        // con is the SqlConnection objectwhich the stores the information about the connection to the database.

        public static List<string> ListOfDatabaseName = new List<string>();
        // ListOfDatabaseName contains the all the database names which are present in the backend SQl Server Management Studio.

        // getColumnNamesAndDataTypes is the method which reterives the table structure/description into the ColumnNamesAndDataTypes variable.
        public static void  getColumnNamesAndDataTypes(string TableName)
        {
            ColumnNamesAndDataTypes.Clear();
            // Clearing the Structure of the Previous table that is stored in the ColumnsNamesAndDataTypes

            string ColumnsRetrievalQuery = "select column_name , DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='" + TableName + "'";
            //The above query is used to retrieve the table schema/description of column names and the data types.

            cmd = new SqlCommand(ColumnsRetrievalQuery, con);
           
            
            con.Open();
            //  Opening the Sql Connection using the connection object (con).

            dr = cmd.ExecuteReader();
            // Executing the Sql Query and storing the results back to the DataReader object (dr).

            while (dr.Read())
            {
                // dr.Read() is a built in method which is to move the cursor from the one line to the next one. 
                
                ColumnNamesAndDataTypes.Add((string)dr[0], (string)dr[1]);
                // Adding the data to the ColumnNamesAndDataTypes object.
            }
            dr.Close();
            //  Closing the Sql Connection using the connection object (con).

            con.Close();
            //  Closing the DataReader object  using the dr object .

        }
        public static void ShowAllTables(SqlConnection con)
        {
            
            string Query = "select table_name from INFORMATION_SCHEMA.TABLES";
            // The above query will get the names of al tables into the result set of data Reader object dr.
            SqlDataReader dr;
            cmd = new SqlCommand(Query, con);
            
            con.Open();
            // Opening the SQl Connection using the con object.

            dr = cmd.ExecuteReader();
            //  Executing the sql statement and Getting the results to the data reader object
            message1 = "Present Connected Database is : " + dbname;
            
            // Display message is a method which is used to display the message to the console with different colour.
            DisplayMessage(message1);
            Console.WriteLine("The List of tables present in the database are :");
            
            // by using dr.Read() we can move the pointers to the record from one record to the another record.
            while (dr.Read())
            {
                DisplayMessage((string)dr[0]);
            }

            // Closing the database Connection using the Close() built in method.
            // Closing the DataReader object so that it can be used in another loaction in the program.
            dr.Close();
            con.Close();
        }
        //  DML Oerations Completed Sucessfully
        public static void DMLOperations(int option)
        {
            // This method will perform the dml operations like Insert, delete, update and select query
            // We can perform these operations according to the value present in the option variable that is passed as a parameter.
            // First it will display the present working database name and then it will display the tables that are present in the database
            // We want to select the table name in which we are going to perform the DML opration.

            SqlCommand cmd;
            SqlDataReader dr;
            DisplayMessage("The Present Connected Database is :" + dbname);
            DisplayMessage("The following are the List Of Available tables in the Connected Database ");
            con = GetConnection(dbname);
            ShowAllTables(con);
            Console.Write("Enter the table name that you want to perform  the DML Operation  :  ");
            string TableName=ReadingInput();
            if (option==1)
            {
                //  Insert  query
                Console.Write("Please provide the number of records that you want to insert  : ");
                int NumberOfRecords = int.Parse(ReadingInput());
                // Retrieving the Column names from the Database in the given table
                getColumnNamesAndDataTypes(TableName);
                
                // Completed the task of Retrieving the Column names from the given table

                // Taking the values from the user to Insert Query
                string InsertQuery;
                int NumberOfRowsAffected=0;
               for(int i=0;i<NumberOfRecords;i++)
                {
                    // For loop is iterated  for how many records to be inserted
                    // foreach loop is used to get details of each and every record to particular column values.
                    // After that opening th sql connection and executing the query and then closing the database connection.
                    InsertQuery = "insert into " + TableName + " values(";
                    DisplayMessage("Provide the Details for the Record Number   :   " + (i + 1));
                    Console.WriteLine("DataType              ColumnName                Enter the Value ");
                    foreach(KeyValuePair<string,string> item in ColumnNamesAndDataTypes)
                    {

                        Console.Write("{0}                     {1}                     :  ",item.Key,item.Value);
                        string InputValue = ReadingInput();
                        if((string)item.Value=="int")
                        {
                            InsertQuery += InputValue + ",";
                        }
                        else if((string)item.Value=="varchar" )
                        {
                            InsertQuery += "'" + InputValue + "',";
                        }
                        
                    }
                    InsertQuery = InsertQuery.Substring(0, InsertQuery.Length - 1);
                    InsertQuery += ")";
                    cmd = new SqlCommand(InsertQuery, con);
                    con.Open();
                    NumberOfRowsAffected += cmd.ExecuteNonQuery();
                    con.Close();
                }
               // Displaying the message showing that number of records to be inserted/affected.
                DisplayMessage(NumberOfRowsAffected + " rows Affected");
            }
            else if( option==2)
            {
                // if option value=2 then Delete Query.
                // 1. first it will display the available columns in the table.
                // 2. Enter the base column name  that you want to delete. 
                //3. Enter value of that column for the deleting record.
                //4.Deleting a record happens here that based on the selected column datatype.
                string DeleteQuery= "";
                Console.Write("These are the available Column Names   :  ");
                 
                getColumnNamesAndDataTypes(TableName);
                foreach(KeyValuePair<string,string> item in ColumnNamesAndDataTypes)
                {
                    Console.Write("{0}   :   ", item.Key);
                }
                
                Console.Write("\nEnter the Column name  to delete a record  :  ");
                string ColumnName = ReadingInput();
                Console.Write("Enter the value of a record for the choosen column  : ");
                if(ColumnNamesAndDataTypes[ColumnName]=="int")
                {
                    DeleteQuery = "delete from " + TableName + " where " + ColumnName + "=" + int.Parse(ReadingInput()) + "";
                }
                else if(ColumnNamesAndDataTypes[ColumnName]=="varchar")
                {
                    DeleteQuery = "delete from " + TableName + " where " + ColumnName + "='" +ReadingInput()+ "'";        

                }
                cmd = new SqlCommand(DeleteQuery, con);
                con.Open();
                int NumberOfRowsAffected=cmd.ExecuteNonQuery();
                con.Close();
                DisplayMessage(NumberOfRowsAffected+"  rows Affected");
            }
            else if (option==3)
            {
                //  if option =3 then Update Query
                // 1. Enter your own  update query
                //2.  Execute the Update query and then display the number of records effected.
                string UpdateQuery;
                Console.WriteLine("Enter Your Update Query : ");
                UpdateQuery = ReadingInput();
                cmd = new SqlCommand(UpdateQuery, con);
                con.Open();
                int NumberOfRowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine(NumberOfRowsAffected + " rows Affected");
            }
            else if(option==4)
            {
                // if option =4 then  Select Query
                // ?Here select query canbe categorized into two types
                // 1. Default select query which brings all columns and all records into the output screen like 
                // select * from table_name.
                // 2. Customized select query user want to enter their own select query then the query is read from console and 
                // then it is executed and then the result will be displayed in the console using the DataReader object.
                
                Console.WriteLine("Do you want to retrieve all columns in the table (Yes) or Do You want to write your own Select Query (No) ");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                Console.Write("Enter Your Option : ");
                int OwnQuery = int.Parse(ReadingInput());
                if(OwnQuery==1)
                {
                   
                    string SelectQueryWithAllColumns = "select * from " + TableName + "";
                    cmd = new SqlCommand(SelectQueryWithAllColumns, con);
                  
                    // Displaying the  Column Names using the ColumnNamesandDataTypes Dictionary object
                    // Calling the getColumnNamesAndDataTypes method to get stored in the dictionary object
                    getColumnNamesAndDataTypes(TableName);
                    foreach(KeyValuePair<string,string> item in ColumnNamesAndDataTypes)
                    {
                        Console.Write("{0}                ",item.Key);
                    }
                    Console.WriteLine();
                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        foreach(KeyValuePair<string,string> item in ColumnNamesAndDataTypes)
                        {
                            Console.Write("{0}                ", dr[item.Key]);
                        }
                        Console.WriteLine();
                    }
                    dr.Close();
                    con.Close();
                }
                else if(OwnQuery==2)
                {
                    Console.WriteLine("Enter Your Select Query");
                    string CustomizedUserSelectQuery = ReadingInput();
                    cmd = new SqlCommand(CustomizedUserSelectQuery, con);
                    string[] arr = CustomizedUserSelectQuery.Split(",");
                    int NumberOfRowsToDisplay = arr.Length;
                    Console.WriteLine(arr[0]+"Number of rows to be displayed is " +NumberOfRowsToDisplay);
                    int var = 0;
                    getColumnNamesAndDataTypes(TableName);
                    foreach (KeyValuePair<string, string> item in ColumnNamesAndDataTypes)
                    {
                        if (var >= NumberOfRowsToDisplay  && NumberOfRowsToDisplay!=0)
                            break;
                        Console.Write("{0}                ", item.Key);
                        var++;
                    }
                    var = 0;
                    Console.WriteLine();
                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        foreach (KeyValuePair<string, string> item in ColumnNamesAndDataTypes)
                        {
                            if (var >= NumberOfRowsToDisplay && NumberOfRowsToDisplay != 0)
                                break;
                            Console.Write("{0}                ", dr[item.Key]);
                            var++;
                        }
                        var = 0;
                        Console.WriteLine();
                    }
                    dr.Close();
                    con.Close();

                }


            }
        }
        // DDL Oprations Completed Sucessfully
        private static void DDLOperations(int dDLoption)
        {
            // Getting the Sql Connection with the present database name
            con = GetConnection(dbname);

            // Displaying the Database name that effects by doing the following operation
            message1 = "Present Connected Database is : " + dbname;
            DisplayMessage(message1);

            // Creating the SqlCommand Variable  
            SqlCommand cmd;
 
           
            
            if (dDLoption == 1)
            {
                // if dDLoption =1 then Create table Query.
                // For creating table we need table name, number of columns in the table..
                // Procedure for the create table query.
                // Step 1: Read the table name from the console.
                // Step2 : Read the number of clumns that should be present in the table.
                // Step 3: For each column get the information about column name and the data type using the for loop.
                // Step 4: After getting information then open the SQL coneection execute the sql query and then close the connection.
                // for above all process insert query should be made by appending the column name and the datatype.
                // at last append the ) to the insert query string.
                // Variables used in this method are
                // 1. InsertQuery is of string type and this is our insert query
                // 2. Attribute name contains the column name 
                // 3. DataType variable contains the data type of the particular column name. 

                // Step 1,2 starts from here.
                string InsertQuery;
                Console.Write("Enter the table name : ");
                string TableName = ReadingInput();
                Console.WriteLine("Enter the number of Columns in a table : ");
                int NumberOfColumns = int.Parse(ReadingInput());
                // Step1,2 ends here.


                // Step 3,4 starts from here.
                string AttributeName;
                string DataType;
                InsertQuery = "create table " + TableName + "(";
                for (int i = 0; i < NumberOfColumns; i++)
                {
                    Console.Write("Enter the Column name : ");
                    AttributeName = ReadingInput();
                    Console.Write("Enter the Data Type   : ");
                    DataType = ReadingInput();
                    if (i != NumberOfColumns - 1)
                        InsertQuery += AttributeName + " " + DataType + ",";
                    else
                        InsertQuery += AttributeName + " " + DataType;
                }
                InsertQuery += ")";
                // Step3,4 ends here.
                cmd = new SqlCommand(InsertQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else if (dDLoption == 2)
            {
                // if dDLoption=2 then it is Alter Query.
                // Procedure using steps are ,
                // Step1 : It will show all the tables.
                // Step2 : Read the customized user's Alter Query.
                // Step3 : Execute the Alter Query.
                // Display the message finally : Alter Query Performed Sucessfully.
                ShowAllTables(con);
                Console.WriteLine("Enter Your Alter Query  ");
                string AlterQuery = ReadingInput();
                cmd = new SqlCommand(AlterQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayMessage("Alter Query Performed Sucessfully ");
            }
            else if (dDLoption == 3)
            {
                // if dDLoption=3 then Drop Query Performed.
                // Step1: Display the all tables by calling the user defined method ShowAllTables().
                // Step2: Read the table name from the user to drop the table.
                // Step3: Prepare the drop table Query using table name.
                // Step4: Execute the drop query by opening and closing the Database Connection.
                // Display message  : Table Dropped Sucessfully.
                // Table name should be stored in the TableNameDropOperation.

                ShowAllTables(con);
                Console.Write("Enter the table name to perform the Drop  Operation :  ");
                string TableNameDropOperation = ReadingInput();
                string DropQuery = "drop table " + TableNameDropOperation + "";
                cmd = new SqlCommand(DropQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayMessage("Table Dropped Sucessfully");
            }
            else if (dDLoption == 4)
            {
                // if dDLoption=4 then Truncate Query Performed.
                // Step1: Display the all tables by calling the user defined method ShowAllTables().
                // Step2: Read the table name from the user to truncate the table.
                // Step3: Prepare the truncate table Query using table name.
                // Step4: Execute the truncate query by opening and closing the Database Connection.
                // Display message : Truncate Performed Sucessfully.
                ShowAllTables(con);
                Console.Write("Enter the table name to perform the truncate Operation :  ");
                string TableNameTruncateOperation = ReadingInput();
                string TruncateQuery = "truncate table " + TableNameTruncateOperation + "";
                cmd = new SqlCommand(TruncateQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                DisplayMessage("Truncate Performed Sucessfully");
            }
        }
        public static string ReadingInput()
        {
            // This method will read the input from the user and then return the input that is read from the user.
            // While raeding the this method will change the Foreground Color to white and then read the input.
            // After reading input then again it will change the Foreground Colour to the orevious one (Magneta).
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            return input;
        }
        public static void DisplayMessage(string s)
        {
            // This method will change the Foreground Color to to Green then Display the message and again it changes the
            // the foregroundColor to Magneta.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        public static SqlConnection GetConnection(string Dbname = "testdb")
        {
            // Description : This method will be helpful in getting the connection from the databse
            // To which database connected is present in the dbname
            // The default database name will be : testdb
            // To connect to the different databse use the method Connect to Database Option


            // ListOfDatabaseName is the List that contains the List Of Database names 
            // Checking the entered database exists or not 
            // If Entered database name is correct then it connect to database sucessfully
            
            if (!ListOfDatabaseName.Contains(Dbname))
            {
                message = "Database with the name " + Dbname + " does not exist.";
            }
            // If the database does not contain then it will through a message saying that Database with the name dbname does not exist

            else
            {
                message = "Connected to " + Dbname + " Database Sucessfully";
            }
           // Server = localhost\MSSQLSERVER01; Database = master; Trusted_Connection = True;
            string ConnectionString = "Server = DESKTOP-GPM99QS; Database ="+Dbname+"; Trusted_Connection = True";
            SqlConnection conn = new SqlConnection(ConnectionString);

            return conn;
        }
        public static string CreateDatabase(SqlConnection con)
        {
            // Description : About the method CreateDatabase
            // This method (CreateDatabase) will create the Database with the Query Entered by the user.
            // If the Database with the given name already exists then it will -->
            // return with the message "Database with given name already exists" otherwise it will create the Database

            // Reading and storing the query entered by the user  
            Console.Write("Enter the name of the Database : ");
            string NameOfDatabase = Console.ReadLine();
            string CreateDatabaseQuery = "create database " + NameOfDatabase + "";

            // Preparing SqlCommand to execute the query 
            SqlCommand cmd = new SqlCommand(CreateDatabaseQuery, con);

            // ReturnStatement is the string variable that stores the message which should be returned by this method
            string ReturnStatement;

            // Openning the Database Connection and executing the SqlCommand and then closing the Connection
            // When the Database with the given name already exists then the program will raise a Exception so -->
            // for that purpose we are placing these statements within the try blck and catching the Exception
            try
            {
                // Opening the Database Connection with the the SqlConnection Object con and Executing the Open() method
                con.Open();

                // Executing the Query
                cmd.ExecuteNonQuery();

                // Closing the Database Connection
                con.Close();

                ReturnStatement = "Database with the name " + CreateDatabaseQuery.Split()[2] + " Created Sucessfully.";
            }

            // By using Catch Block we are Handling the Exception
            catch (Exception e)
            {
                ReturnStatement = "Database with the name " + CreateDatabaseQuery.Split()[2] + " already exists";
            }

            // Returning the Message 
            return ReturnStatement;
        }
        public static void ShowDatabase(SqlConnection con)
        {
            // Description : This method should display names of all databases.

            string ShowDatabasesQuery = "SELECT name FROM sys.databases";
            SqlCommand cmd = new SqlCommand(ShowDatabasesQuery, con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            Console.WriteLine("___________________________________");
            while (dr.Read())
            {
                string s = Convert.ToString(dr[0]);
                ListOfDatabaseName.Add(s);
                Console.WriteLine("|   " + dr[0] + "    ");
                Console.WriteLine("|__________________________________|");
            }
            dr.Close();
            con.Close();


        }
        static void Main(string[] args)
        {
            bool valid = true;

            while (valid)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("****************************************");
                Console.WriteLine("*    1. Show Databases                 *");
                Console.WriteLine("*    2. Create Database                *");
                Console.WriteLine("*    3. Connect To Database            *");
                Console.WriteLine("*    4. Table Related Queries          *");
                Console.WriteLine("*    5. Clear Console                  *");
                Console.WriteLine("*    6. Exit                           *");
                Console.WriteLine("****************************************");

                Console.Write("Enter Your Option : ");
                Console.ForegroundColor = ConsoleColor.White;
                int option = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Magenta;
                switch (option)
                {
                    case 1:
                        ShowDatabase(GetConnection());
                        break;
                    case 2:
                        DisplayMessage(CreateDatabase(GetConnection()));
                        break;

                    case 3:
                        SqlConnection con = GetConnection();
                        Console.WriteLine("These are the available list of Databases");
                        ShowDatabase(con);
                        Console.Write("Enter Database Name : ");
                        dbname = ReadingInput();
                        GetConnection(dbname);
                        DisplayMessage(message);
                        break;

                    case 4:
                        Console.WriteLine(" ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                        Console.WriteLine("|    1. Data Definition Language Commands                 |");
                        Console.WriteLine("|    2. Data Manipulation Language Commands               |");
                        Console.WriteLine("|    3. Transaction Control Language Commands             |");
                        Console.WriteLine("|    4. Show All Tables                                   |");
                        Console.WriteLine("|_________________________________________________________|");
                        Console.Write("Enter Your Option : ");
                        int TableOption = int.Parse(ReadingInput());
                        switch (TableOption)
                        {
                            case 1:
                                Console.WriteLine("The following options are list of DDL Commands ");
                                Console.WriteLine("1. Create Table");
                                Console.WriteLine("2. Alter Table");
                                Console.WriteLine("3. Drop Table");
                                Console.WriteLine("4. Truncate Table");
                                Console.Write("Enter Your Option : ");
                                int DDLoption = int.Parse(ReadingInput());
                                DDLOperations(DDLoption);
                                break;
                            case 2:
                                Console.WriteLine("The following options are list of DML Commands ");
                                Console.WriteLine("1. Insert ");
                                Console.WriteLine("2. Delete ");
                                Console.WriteLine("3. Update ");
                                Console.WriteLine("4. Select ");
                                Console.Write("Enter Your Option : ");
                                int DMLoption = int.Parse(ReadingInput());
                                DMLOperations(DMLoption);
                                break;
                            case 3:
                                Console.WriteLine("Comitted sucessfully");
                                break;
                            case 4:
                                // Showing all tables information
                                ShowAllTables(GetConnection(dbname));
                                break;
                        }
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    case 6:
                        valid = false;
                        break;


                }
            }


        }


    }
}
