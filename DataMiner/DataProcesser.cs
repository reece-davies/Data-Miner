using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiner
{
    /// <summary>
    /// the purpose of DataProcesser is to perform calculations on the data from the file, specifically the algorithms used for displaying the data on the graph
    /// </summary>
    class DataProcesser
    {
        //the List<DataManagement> recordList object is so created so that all the objects created can be accessed 
        private List<DataManagement> recordList;
        /// <summary>
        /// this method allocates the List<> in main that contains all the objects of DataManagement into the List<> object 
        /// </summary>
        /// <param name="tempList"></param>
        public DataProcesser(List<DataManagement> tempList)
        {
            recordList = tempList;
        }
        /// <summary>
        /// the purpose of this method is to identify which month is being used, and return the month value in a way that can be used to identify the data
        /// </summary>
        /// <param name="twelve"></param>
        /// <returns></returns>
        private int CheckMonth(int twelve)
        {
            //month will be used to hold the number which can be used for searching the data
            int month = 0;
            //the switch statement basically turns the index of the month into the syntax that can be used to search the data in a DateTime data type
            switch (twelve)
            {
                case 1:
                    month = 01;
                    break;
                case 2:
                    month = 02;
                    break;
                case 3:
                    month = 03;
                    break;
                case 4:
                    month = 04;
                    break;
                case 5:
                    month = 05;
                    break;
                case 6:
                    month = 06;
                    break;
                case 7:
                    month = 07;
                    break;
                case 8:
                    month = 08;
                    break;
                case 9:
                    month = 09;
                    break;
                case 10:
                    month = 10;
                    break;
                case 11:
                    month = 11;
                    break;
                case 12:
                    month = 12;
                    break;
            }
            //return the syntactfully correct value of the month
            return month;
        }
        /// <summary>
        /// the purpose of this method is to calculate the total quantity of items sold in that particular calendar month, only one month is used at a time
        /// </summary>
        /// <param name="m"></param> m is the month index from an array
        /// <returns></returns>
        public int TotalQuantityOfItemsSoldPerCalendarMonth(int m)
        {
            //quantityOfItemsSold will be the value returned, and is initialized to 0
            int quantityOfItemsSold = 0;
            // the month number is put through the method CheckMonth so we can get a number we can work with when sorting through the data
            int monthOne = CheckMonth(m);
            //search through all the objects in recordList by using a foreach loop, to check if the month of the invoice matches the month being searched
            foreach (DataManagement R in recordList)
            {
                //if the month of the invoice matches the month being searched then increase the quantity of items sold by the quantity in that object
                if (R.InvoiceDate.Month == monthOne)
                {
                    quantityOfItemsSold += R.Quantity;
                }
            }
            //return the quantity of items sold in that month
            return quantityOfItemsSold;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param> m is the month index of an array
        /// <returns></returns>
        public double TotalValueInPoundsOfItemsSoldPerMonth(int m)
        {
            //valueOfItemsSold will be the value returned, and is initialized to 0
            double valueOfItemsSold = 0;
            //the month number is put through the method CheckMonth so we can get a number we can work with when sorting through the data
            int monthOne = CheckMonth(m);
            //search through all the objects in recordList by using a foreach loop, to check if the month of the invoice matches the month being searched
            foreach (DataManagement R in recordList)
            {
                //if the month of the invoice matches the month being searched then increase the value of items sold by the quantity multiplied by the unit price in that object
                if (R.InvoiceDate.Month == monthOne)
                {
                    valueOfItemsSold += (R.Quantity * R.UnitPrice);
                }
            }   
            //return the value of items sold in that month     
            return valueOfItemsSold;
        }
        /// <summary>
        /// finds the total number of unique invoices in that month
        /// </summary>
        /// <param name="m"></param> m is the month index in an array
        /// <returns></returns>
        public int TotalNumberOfInvoicesGeneratedPerMonth(int m)
        {
            //numberOfInvoices will be the value returned, and is initialized to 0
            int numberOfInvoices = 0;
            //a temporary List<> is created to hold all the invoices already found in the current month
            List<int> currentMonth;
            //an instance of the List<> is created
            currentMonth = new List<int>();
            //List is cleared
            currentMonth.Clear();
            //a boolean to check if the invoice being checked is already in the month or not is set to false for the first invoice
            bool inMonth = false;
            //the month number is put through the method CheckMonth so we can get a number we can work with when sorting through the data
            int monthOne = CheckMonth(m);
            //go through each object in the recordList using a foreach loop
            foreach (DataManagement R in recordList)
            {
                //if the month of the invoice matches the month being searched then continue
                if (R.InvoiceDate.Month == monthOne)
                {
                    //set/reset the inMonth boolean to false for each pass
                    inMonth = false;
                    //a foreach loop goes through all invoices that have already been found and checks to see if the one being checked has already been found
                    foreach (int C in currentMonth)
                    {
                        if (C == R.InvoiceNo)
                        {
                            //if the invoice has already been found then set the inMonth boolean to true
                            inMonth = true;
                        }
                    }
                    //if the invoice number has not already been found then increment by 1 the number of invoices, and add that invoice number to the List<> of invoices found
                    if (!inMonth)
                    {
                        numberOfInvoices++;
                        currentMonth.Add(R.InvoiceNo);
                    }
                }
            }
            //return the number of invoices found
            return numberOfInvoices;
        }
        /// <summary>
        /// gets the total nunmber of unique customers in that particular month
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int TotalNumberOfUniqueCustomersPerMonth(int m)
        {
            //uniqueCustomers will be the value returned, and is initialized to 0
            int uniqueCustomers = 0;
            //a temporary List<> is created to hold all the customers already found in the current month
            List<int> currentMonth;
            //an instance of the List<> is created
            currentMonth = new List<int>();
            //List is cleared
            currentMonth.Clear();
            //a boolean to check if the customer being checked is already in the month or not is set to false for the first customer
            bool inMonth = false;
            //the month number is put through the method CheckMonth so we can get a number we can work with when sorting through the data
            int monthOne = CheckMonth(m);
            //go through each object in the recordList using a foreach loop
            foreach (DataManagement R in recordList)
            {
                //if the month of the invoice matches the month being searched then continue
                if (R.InvoiceDate.Month == monthOne)
                {
                    //set/reset the boolean to check if the customer is in the month to false
                    inMonth = false;
                    //a foreach loop goes through all customers that have already been found and checks to see if the one being checked has already been found
                    foreach (int C in currentMonth)
                    {
                        //if the customer has already been found then set the inMonth boolean to true
                        if (C == R.CustomerID)
                        {
                            inMonth = true;
                        }
                    }
                    //if the customer has not already been found then increment by 1 the number of customers, and add that customer to the List<> of customers found
                    if (!inMonth)
                    {
                        uniqueCustomers++;
                        currentMonth.Add(R.CustomerID);
                    }
                }
            }
            //return the number of unique customers
            return uniqueCustomers;
        }
        /// <summary>
        /// find the average value spend in pounds per customer in that month
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public double AverageSpendInPoundsPerCustomerPerMonth(int m)
        {
            //averageSpend is the value to be returned and is initialized to 0
            double averageSpend = 0;
            //totalCustomer uses the 'TotalNumberOfUniqueCustomersPerMonth' method to find the total number of unique customers in that month, passes the month index as a parameter
            double totalCustomer = TotalNumberOfUniqueCustomersPerMonth(m);
            //totalCost uses the 'TotalValueInPoundsOfItemsSoldPerMonth' method to find the total value in pounds of the items sold in that month, passes the month index as a parameter
            double totalCost = TotalValueInPoundsOfItemsSoldPerMonth(m);
            //average Spend is the total cost divided by the total number of customers in that month
            averageSpend = totalCost / totalCustomer;
            //return the average spend
            return averageSpend;
        }
        /// <summary>
        /// Finds the average spend in pounds per invoice for that particular month
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public double AverageSpendInPoundsPerInvoicePerCalendarMonth (int m)
        {
            //averageInvoiceSpend is the value returned, and is initialized to 0
            double averageInvoiceSpend = 0;
            //totalInvoices uses the method 'TotalNumberOfInvoicesGeneratedPerMonth' to find the total number of invoices in that month
            double totalInvoices = TotalNumberOfInvoicesGeneratedPerMonth(m);
            //totalCost uses the method 'TotalValueInPoundsOfItemsSoldPerMonth' to find the total value in pounds spend in that month
            double totalCost = TotalValueInPoundsOfItemsSoldPerMonth(m);
            //the average is the total cost divided by the total number of invoices in that month
            averageInvoiceSpend = totalCost / totalInvoices;
            //the average invoice spend is returned
            return averageInvoiceSpend;
        }
        /// <summary>
        /// this method finds the average number of items per invoice in the month
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public double AverageNumberOfItemsPerInvoicePerMonth(int m)
        {
            //numberOfItems is the value to be returned, and is initialised to 0
            double numberOfItems = 0;
            //totalNumberOfItems is used for finding the average number of items
            int totalNumberOfItems = 0;
            //List<> to hold the unique list of invoices in the month
            List<int> numberOfInvoices;
            //instance of the List<> is made
            numberOfInvoices = new List<int>();
            //List<> is cleared
            numberOfInvoices.Clear();
            //the list of invoices is found using the method 'GetNumberOfInvoicesPerMonth' passing the month index as a parameter
            numberOfInvoices = GetNumberOfInvoicesPerMonth(m);
            //the month number is put through the method CheckMonth so we can get a number we can work with when sorting through the data
            int monthTwo = CheckMonth(m);
            //using a foreach loop to go through each object in the recordList 
            foreach (DataManagement R in recordList)
            {
                //using a foreach loop to go through all the unique invoices in numberOfInvoies
                foreach (int I in numberOfInvoices)
                {
                    //if the invoice number being checked in the outside loop matches the invoice number in the inside loop then increment the number of items for that invoice by the quantity of that item
                    if (R.InvoiceNo == I)
                    {
                        totalNumberOfItems += R.Quantity;
                    }
                }
            }
            //the number of items in that invoice is the total number of items divided by the number of unique invoices
            numberOfItems = (totalNumberOfItems) / (numberOfInvoices.Count());
            return numberOfItems;
        }
        /// <summary>
        /// this method gets the list of unique invoices per month
        /// </summary>
        /// <param name="mon"></param>
        /// <returns></returns>
        private List<int> GetNumberOfInvoicesPerMonth(int mon)
        {
            //a List<> to hold all the unique invoice numbers in that month
            List<int> numberOfInvoicesPerMonth;
            numberOfInvoicesPerMonth = new List<int>();          
            numberOfInvoicesPerMonth.Clear();
            //set the boolean used to check if it is already in the month to false
            bool inMonth = false;
            //the month number is put through the method CheckMonth so we can get a number we can work with when sorting through the data
            int monthOne = CheckMonth(mon);
            //the foreach loop goes through all the objects stored in recordList 
            foreach (DataManagement R in recordList)
            {
                //if the month of the invoice being checked matches the month currently being checked then execute the code below
                if (R.InvoiceDate.Month == monthOne)
                {
                    //inMonth is set/reset to false
                    inMonth = false;
                    //foreach loop goes through the List<> of uniquely identified invoices in that month
                    foreach (int C in numberOfInvoicesPerMonth)
                    {
                        //if the invoice being checked matches the invoice number in the List<> then the inMonth boolean is set to true to indicate that the invoice number is in the list already
                        if (C == R.InvoiceNo)
                        {
                            inMonth = true;
                        }
                    }
                    //if the invoice is not in the list then add it to the list
                    if (!inMonth)
                    {
                        numberOfInvoicesPerMonth.Add(R.InvoiceNo);
                    }
                }
            }
            //return the list of uniquely identified invoices
            return numberOfInvoicesPerMonth;
        }
    }
}