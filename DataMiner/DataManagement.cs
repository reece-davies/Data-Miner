using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMiner
{
    /// <summary>
    /// The purpose of the class DataManagement is to make several objects out of each row from the data file that was loaded, those objects are then added to a List<> in Main, as there will be many objects so the List<> allows access to all objects
    /// So the specific purpose of this class is to create the objects, and that is all as the data is used in the class DataProcesser
    /// </summary>
    class DataManagement
    {
        //seven objects to store the data from the file
        private int invoiceNo;
        private string stockCode;
        private string description;
        private int quantity;
        private DateTime invoiceDate;
        private double unitPrice;
        private int customerID; 
        
        /// <summary>
        /// assign the data to the objects
        /// </summary>
        /// <param name="tempinvoiceNo"></param>
        /// <param name="tempstockCode"></param>
        /// <param name="tempdescription"></param>
        /// <param name="tempquantity"></param>
        /// <param name="tempinvoiceDate"></param>
        /// <param name="tempunitPrice"></param>
        /// <param name="tempcustomerID"></param>                  
        public DataManagement(int passedInvoiceNo, string passedStockCode, string passedDescription, int passedQuantity, DateTime passedInvoiceDate, double passedUnitPrice, int passedCustomerID)
        {
            invoiceNo = passedInvoiceNo;
            stockCode = passedStockCode;
            description = passedDescription;
            quantity = passedQuantity;
            invoiceDate = passedInvoiceDate;
            unitPrice = passedUnitPrice;
            customerID = passedCustomerID;
        }   
        /// <summary>
        /// allow access to and return the InvoiceNo to Main
        /// </summary>
        public int InvoiceNo
        {           
            get { return invoiceNo; }
        }
        /// <summary>
        /// allow access to and return the StockCode to Main
        /// </summary>
        public string StockCode
        {
            get { return stockCode; }
        }
        /// <summary>
        /// allow access to and return the Description to Main
        /// </summary>
        public string Description
        {
            get { return description; }
        }
        /// <summary>
        /// allow access to and return the Quantity to Main
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
        }
        /// <summary>
        /// allow access to and return the InvoiceDate to Main
        /// </summary>
        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
        }
        /// <summary>
        /// allow access to and return the UnitPrice to Main
        /// </summary>
        public double UnitPrice
        {
            get { return unitPrice; }
        }
        /// <summary>
        /// allow access to and return the CustomerID to Main
        /// </summary>
        public int CustomerID
        {
            get { return customerID; }
        }
    }
}
