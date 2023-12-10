using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace DataMiner
{ 
    public partial class Main : Form
    {
        //global variables are declared here due to the need of accessibility throughout the program
        //recordList is a list that holds objects, specifically each object is a row of data from the csv file.
        private List<DataManagement> recordList;
        //fileName holds the name of the file being read
        private string fileName;
        //directoryName holds the name of the directory of the file being read
        private string directoryName;
        //drawn is a boolean used to identify whether the graph has been drawn or not. the purpose of this boolean is to know whether to draw the graph or not when resizing the form, as there would be no data to use to draw the graph when resizing if the graph has not already been drawn
        private bool drawn = false;
        //month is a double array used to hold the value of each month, which is to be displayed on the graph
        private double[] month;
        //chart is a boolean used to identify whether the bar chart is displayed or if the textbox is displayed
        bool chart;
        //money is a boolean used to identify whether the value to be displayed on the graph is measured in pounds or not, so the program knows whether to round to whole numbers or two decimal points
        bool money;
        //fileExists is a boolean used to identify whether the file has been selected, and allow the user to choose an option to display a graph
        bool fileExists;
        //graphLabels is an array of 12 labels which are used to hold the first letter of each month
        Label[] graphLabels = new Label[12];
        //graphLabelValues is an array of 12 labels which are used to hold the value of each bar in the bar graph
        Label[] graphLabelValues = new Label[12];

        /// <summary>
        /// creates the list<> to hold each record of data and initializes the file name (of the file to read) to null
        /// </summary>
        public Main()
        {
            //calls method 'InitializeComponent'
            InitializeComponent();
            //sets the fileName variable equal to null
            fileName = null;
            //creates a new list to hold the objects
            recordList = new List<DataManagement>();
            //calls method 'InitializeGraph' to prepare visibility of labels and other objects, and to set up other variables
            InitializeGraph();         
        }
        /// <summary>
        /// 'openToolStripMenuItem_Click' is used for selecting a file to read
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if the cancel button is not clicked while menu is open, then set file name and directory name and proceed to load the file
            if (openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                //fileName is set to the name of the file selected
                fileName = openFileDialog.FileName;
                //directoryName is set to the directory name of the selected file.
                directoryName = openFileDialog.InitialDirectory;
                //calls method 'ReadFile'
                FileRead();           
            }
        }
        /// <summary>
        /// initialises the graph by setting label visibility to false/invisible and setting variable values.
        /// </summary>
        private void InitializeGraph()
        {
            //variable to avoid magic numbers
            int maxMonthIndex = 11;
            //make the loading bar invisible until needed
            progressBarLoading.Visible = false;
            //creates an array called 'month' which is type double and has 12 memory slots
            month = new double[12];
            //set chart equal to true as the form starts on the graph/panel mode
            chart = true;
            //sets money to be false as a default
            money = false;
            //disables the comboBox used to select what type of graph to display, until a file is loaded
            comboBoxDataToDisplay.Enabled = false;
            //disables the visibility of the textbox used to display data
            textBoxOutput.Visible = false;
            //disables the search box used to search the data
            textBoxSearch.Enabled = false;
            //disables the button that is clicked to initiate the search
            buttonSearch.Enabled = false;
            //sets the fileExists boolean to false as the file has not been selected yet
            fileExists = false;
            //disables the groupbox for choosing between customer ID and invoice ID
            groupBoxOption.Enabled = false;
            //sets the visibility of the Y-axis labels to invisible
            labelYAxisHeight.Visible = false;
            labelMidYAxis.Visible = false;     
            //this for loop goes through the twelve indexs in the graphLabels array and creates a label for each and sets the autosize to true so the labels don't overlap, and adds the label to the panelChart panel, finally setting the visiblity to false 
            for (int L = 0; L <= maxMonthIndex; L++)
            {
                graphLabels[L] = new Label();
                graphLabels[L].AutoSize = true;
                panelChart.Controls.Add(graphLabels[L]);
                graphLabels[L].Visible = false;
            }
            //this for loop goes through the twelve indexs in the graphLabelsValues array and creates a label for each and sets the autosize to true so the labels don't overlap, and adds the label to the panelChart panel, finally setting the visiblity to false 
            for (int V = 0; V <= maxMonthIndex; V++)
            {
                graphLabelValues[V] = new Label();
                graphLabelValues[V].AutoSize = true;
                panelChart.Controls.Add(graphLabelValues[V]);
                graphLabelValues[V].Visible = false;
            }
        }
        /// <summary>
        /// This method deals with reading the file and then calls the 'CreateRecord' method which creates the row of data. This method also updates a label to display the number of rows in the file to show when the file has been loaded.
        /// </summary>
        private void FileRead()
        {
            //initialise the record number to 0 as no lines have been read yet
            int recordNumber = 0;
            //string to hold the data on the current line
            string lineFromFile;
            //clears the List<> of any data is may have had previously
            recordList.Clear();
            //uses try catch to read from the file so that the program does not crash if anything goes wrong
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    lineFromFile = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        //data on that line in the file is all assigned to 'lineFromFile'
                        lineFromFile = reader.ReadLine();
                        //method 'CreateRecord' is called to handle the data
                        CreateRecord(lineFromFile);
                        //recordNumber is incremented
                        recordNumber++;
                    }
                    //sets the comboBox 'comboBoxDataToDisplay' to enabled as file now exists
                    comboBoxDataToDisplay.Enabled = true;
                    //sets the fileExists to true as the file has been uploaded
                    fileExists = true;
                    //sets the label to display to the user that the file has been loaded and to display the number of records
                    labelRecordNumber.Text = "Loading complete, number of records loaded: " + recordNumber.ToString();
                }       
            }
            //an IO exception handling is here in case there are any IO issues.
            catch (IOException)
            {
                MessageBox.Show("Error reading from file, check that you have chosen the correct file.", "File Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   
        /// <summary>
        /// Creates an object for each row of data in the .csv file and then adds that data to a List<> of records.
        /// </summary>
        /// <param name="lineFromFile"></param>
        private void CreateRecord(string lineFromFile)
        {
            //creates the string[] that will hold the row of data when split
            string[] splitData;
            //individual variables to hold the data of each column to use when creating an object
            int invoiceNo;
            string stockCode;
            string description;
            int quantity;
            DateTime invoiceDate;
            double unitPrice;
            int customerID;
            //a temporary object used to create an object of the row of data
            DataManagement Data;
            //gets the split data from the method "SplitString"
            splitData = SplitString(lineFromFile);
            //allocates the data from the splitData into their respective variable
            invoiceNo = Convert.ToInt32(splitData[0]);
            stockCode = splitData[1];
            description = splitData[2];
            quantity = Convert.ToInt32(splitData[3]);
            invoiceDate = Convert.ToDateTime(splitData[4]);
            unitPrice = Convert.ToDouble(splitData[5]);
            customerID = Convert.ToInt32(splitData[6]);
            //creates the object of the row of data in the DataManagement class
            Data = new DataManagement(invoiceNo, stockCode, description, quantity, invoiceDate, unitPrice, customerID);
            //adds that object to a List<> which holds all objects in DataManagement
            recordList.Add(Data);
        }
        /// <summary>
        /// Splits the row of data that was read in from the file into each piece of data.
        /// </summary>
        /// <param name="lineFromFile"></param>
        /// <returns></returns>
        private string[] SplitString(string lineFromFile)
        {
            string[] splitString;
            //uses the "," between each piece of data on the line to separate the data.
            char[] deliminter = { ',' };
            splitString = lineFromFile.Split(deliminter);
            return splitString;
        }
        /// <summary>
        /// DrawAxis is responsible for drawing the X-axis and Y-axis, as well as the two dashes on the Y-axis
        /// </summary>
        /// <param name="xAxisStartPointYcoordinate"></param> X-axis start point Y-coordinate 
        /// <param name="xAxisStartPointXcoordinate"></param> X-axis start point X-coordinate
        /// <param name="xAxisEndPointYcoordinate"></param> X-axis end point Y-coordinate
        /// <param name="xAxisEndPointXcoordinate"></param> X-axis end point X-coordinate
        /// <param name="yAxisStartPointYcoordinate"></param> Y-axis start point Y-coordinate
        /// <param name="yAxisStartPointXcoordinate"></param> Y-axis start point X-coordinate
        /// <param name="yAxisEndPointYcoordinate"></param> Y-axis end point Y-coordinate
        /// <param name="yAxisEndPointXcoordinate"></param> Y-axis end point X-coordinate
        private void DrawAxis(float xAxisStartPointYcoordinate, float xAxisStartPointXcoordinate, float xAxisEndPointYcoordinate, float xAxisEndPointXcoordinate, float yAxisStartPointYcoordinate, float yAxisStartPointXcoordinate, float yAxisEndPointYcoordinate, float yAxisEndPointXcoordinate)
        {
            //variables to avoid magic numbers
            int dashDistance = 4;
            int moveLabelDown = 3;
            int startingPoint = 5;
            //creates all the start and end points for the axis and two dashes using the variables passed in as parameters
            //start and end points for the axis are directly below
            PointF xAxisStart = new PointF(xAxisStartPointXcoordinate, xAxisStartPointYcoordinate);
            PointF xAxisEnd = new PointF(xAxisEndPointXcoordinate, xAxisEndPointYcoordinate);

            PointF yAxisStart = new PointF(yAxisStartPointXcoordinate, yAxisStartPointYcoordinate);
            PointF yAxisEnd = new PointF (yAxisEndPointXcoordinate, yAxisEndPointYcoordinate);
            //start and end points for the dashes are directly below
            PointF dashStart = new PointF(yAxisStartPointXcoordinate - dashDistance, yAxisStartPointYcoordinate);
            PointF dashEnd = new PointF(yAxisStartPointXcoordinate + dashDistance, yAxisStartPointYcoordinate);
            //creates a new colour variable for the axis and dashes
            Color lineColour = Color.Black;
            // draws the lines using Graphics
            using (Graphics panelGraphics = panelChart.CreateGraphics())
            using (Pen penY = new Pen(lineColour, 2))
            {
                //clears the panel before drawing any new lines, this is mainly for resizing the form
                panelGraphics.Clear(Color.White);
                panelGraphics.DrawLine(penY, yAxisStart, yAxisEnd);
                panelGraphics.DrawLine(penY, xAxisStart, xAxisEnd);
                panelGraphics.DrawLine(penY, dashStart, dashEnd);
                dashStart = new PointF(yAxisStartPointXcoordinate - dashDistance, ((yAxisEndPointYcoordinate - yAxisStartPointYcoordinate) / 2) + yAxisStartPointYcoordinate);
                dashEnd = new PointF(yAxisStartPointXcoordinate + dashDistance, ((yAxisEndPointYcoordinate - yAxisStartPointYcoordinate) / 2) + yAxisStartPointYcoordinate);
                panelGraphics.DrawLine(penY, dashStart, dashEnd);

            }
            //sets the location of the label to show the height of the Y-axis
            labelYAxisHeight.Location = new Point(startingPoint, (int)(yAxisStartPointYcoordinate) + moveLabelDown);
        }
        /// <summary>
        /// SetUpLabels sets the up location of the labels and enables them to be visible
        /// </summary>
        /// <param name="xAxisStartPointYcoordinate"></param> X-axis start point Y-coordinate 
        /// <param name="xAxisStartPointXcoordinate"></param> X-axis start point X-coordinate
        /// <param name="xAxisEndPointYcoordinate"></param> X-axis end point Y-coordinate
        /// <param name="xAxisEndPointXcoordinate"></param> X-axis end point X-coordinate
        private void SetUpLabels(int xAxisStartPointYcoordinate, int xAxisStartPointXcoordinate, int xAxisEndPointYcoordinate, int xAxisEndPointXcoordinate, int yAxisEndPointYcoordinate)
        {
            //variable to avoid magic numbers
            int maxOfIndex = 11;
            int plusGap = 5;
            int moveProgressBarLeft = 175;
            int moveProgressBarUp = 15;
            //doubles to hold the position multiplier and the amount the multiplier increments by for each label
            double pos = 0.04;
            double space = 0.08;
            //this for loop sets all the labels with the month names to be visible
            for (int G = 0; G <= maxOfIndex; G++)
            {
                graphLabels[G].Visible = true;
            }
            //this for loop sets all the labels with the month values to be visible
            for (int A = 0; A <= maxOfIndex; A++)
            {
                graphLabelValues[A].Visible = true;
            }

            //all the month names are manually added as there is not a more elegant way of doing this
            graphLabels[0].Text = "J";
            graphLabels[1].Text = "F";
            graphLabels[2].Text = "M";
            graphLabels[3].Text = "A";
            graphLabels[4].Text = "M";
            graphLabels[5].Text = "J";
            graphLabels[6].Text = "J";
            graphLabels[7].Text = "A";
            graphLabels[8].Text = "S";
            graphLabels[9].Text = "O";
            graphLabels[10].Text = "N";
            graphLabels[11].Text = "D";
     
            //set the labels for the Y-axis height to visible
            labelYAxisHeight.Visible = true;
            labelMidYAxis.Visible = true;
            //setting the loading bar location to be the centre of the panel
            progressBarLoading.Location = new Point((xAxisEndPointXcoordinate / 2) - moveProgressBarLeft, (yAxisEndPointYcoordinate / 2) - moveProgressBarUp);
            //sets the location of the twelve labels Janurary through to December so that they scale when the form is resized
            for (int P = 0; P <= maxOfIndex; P++)
            {
                graphLabels[P].Location = new Point((int)(((xAxisEndPointXcoordinate - xAxisStartPointXcoordinate) * pos)) + (xAxisStartPointXcoordinate), (xAxisStartPointYcoordinate) + plusGap);
                //increase the space between the labels by 8% of the X-axis so they are proportionally spaced when the form is resized
                pos += space;
            }        
        }


        /// <summary>
        /// DrawChart is used each time the user selects a different option in the comboBox
        /// </summary>
        private void DrawChart()
        {
            //calls the method 'PerformAlgorithm' which calculates the data for the twelve months 
            PerformAlgorithm();
            //calculates the dimensions and draws the axis and bars, is all in one method for when the form gets resized
            ResizeGraph();
        }
        /// <summary>
        /// ResizeGraph is called whenever the form is resized and when an a different data to display is selected
        /// </summary>
        private void ResizeGraph()
        {
            //find the height and width of the panel so can use for scaling the axis and bars
            int panelHeight = panelChart.Height;
            int panelWidth = panelChart.Width;
            //use of pointNine representing 0.90 to avoid magic numbers
            double multiplier = 0.90;
            double yAxisMultiplier = 0.08;
            double yAxisXPoint = 55;
            double xAxisMultiplier = 0.99;
            int firstLimit = 180;
            int secondLimit = 70;
            int firstLimitAdjustment = 10;
            int secondLimitAdjustment = 13;
            //setting the start and end points for the X-axis and Y-axis as multiples of the panel height and width
            double yAxisStartPointY = panelHeight * yAxisMultiplier;
            double yAxisStartPointX = yAxisXPoint;
            double yAxisEndPointY = panelHeight * multiplier;
            double yAxisEndPointX = yAxisXPoint;
            double xAxisStartPointY = panelHeight * multiplier;
            double xAxisStartPointX = yAxisXPoint;
            double xAxisEndPointY = panelHeight * multiplier;
            double xAxisEndPointX = panelWidth * xAxisMultiplier;
            //set the location of the midpoint label to the appropriate location
            labelMidYAxis.Location = new Point(5, (int)(((yAxisEndPointY - yAxisStartPointY) / 2) + yAxisStartPointY));
            //if the form gets resized to a smaller size then the height is adjusted so the graph is appropriately displayed by setting minimum values for the axis
            if (panelHeight < firstLimit)
            {
                yAxisEndPointY = panelHeight * multiplier - firstLimitAdjustment;
                xAxisEndPointY = panelHeight * multiplier - firstLimitAdjustment;
                xAxisStartPointY = panelHeight * multiplier - firstLimitAdjustment;
                if(panelHeight < secondLimit)
                {
                    yAxisEndPointY = panelHeight * multiplier - secondLimitAdjustment;
                    xAxisEndPointY = panelHeight * multiplier - secondLimitAdjustment;
                    xAxisStartPointY = panelHeight * multiplier - secondLimitAdjustment;
                }
            }
            //calls method DrawAxis which draws the X and Y axis
            DrawAxis((float)xAxisStartPointY, (float)xAxisStartPointX, (float)xAxisEndPointY, (float)xAxisEndPointX, (float)yAxisStartPointY, (float)yAxisStartPointX, (float)yAxisEndPointY, (float)yAxisEndPointX);
            //calls method SetUpLabels, which sets up the location and visibility of most labels
            SetUpLabels((int)xAxisEndPointY, (int)xAxisStartPointX, (int)xAxisEndPointY, (int)xAxisEndPointX, (int)yAxisEndPointY);
            //calls method DrawBars, which calculates dimensions of each bar and uses the graphics to draw them
            DrawBars((int)xAxisStartPointY, (int)xAxisStartPointX, (int)xAxisEndPointY, (int)xAxisEndPointX, (int)yAxisStartPointY, (int)yAxisStartPointX, (int)yAxisEndPointY, (int)yAxisEndPointX);
        }
        /// <summary>
        /// Sorts through the twelve months and finds the one with the highest value and then returns that value
        /// </summary>
        /// <returns></returns>
        private double FindHighestValue()
        {        
            //sets highestValue to the first month
            double highestValue = month[0];
            int monthIndexMax = 11;
            //for loop to go through the other 11 months
            for (int T = 1; T <= monthIndexMax; T++)
            {
                //if the value in current month is greater then the current highest value, highest value gets updated to the current month value
                if (highestValue <= month[T])
                {
                    highestValue = month[T];
                }
            }
            //returns the highest value
            return highestValue;
        }
        /// <summary>
        /// finds the Y coordinate of each bar, one at a time
        /// </summary>
        /// <param name="bar"></param> bar is the value of the length of the Y-axis
        /// <param name="index"></param> index is the month number, eg 1 = janurary, 2 = feburary etc
        /// <param name="yStartY"></param> yStartY is the Y-axis start point Y coordinate
        /// <param name="yEndY"></param> yEndY is the Y-axis end point Y coordinate
        /// <returns></returns>
        private double FindYCoord(double bar, int index, double yStartY, double yEndY)
        {
            //set coordinate to 0
            double coordinate = 0;
            //get the value of that particular month
            double value = month[index];
            //if statement to check if the value is in terms of money, if not then round the data
            if (!money)
            {
                value = Math.Round(value, 0, MidpointRounding.AwayFromZero);
            }
            //set the starting Y coordinate point of the bar to the value of the month divided by the value of the Y-axis to get the ratio of the bar to axis, and then multiply by the length of the bar
            coordinate = (value / bar) * (yEndY - yStartY);
            //return the starting Y-axis point 
            return coordinate;
        }
        /// <summary>
        /// rounds the value of the Y-axis up to the nearest suitable value
        /// </summary>
        /// <param name="y"></param> y is the highest value of the 12 months
        /// <returns></returns>
        private double SetYAxisValue(double y)
        {
            //initialise the number to 0
            double number = 0;
            //if statement to select the appropriate height to set the Y-axis to
            //if value is less than or equal to 10 then...
            if(y <= 10)
            {
                //round number up to nearest 1 (whole number)
                number = Math.Ceiling(y / 1) * 1;
            }
            //if value is less than or equal to 100 then...
            else if (y <= 100)
            {
                //round number up to nearest 10
                number = Math.Ceiling(y / 10) * 10;
            }
            //if value is less than or equal to 100 then...
            else if (y <= 300)
            {
                //round number up to nearest 100
                number = Math.Ceiling(y / 100) * 100;
            }
            //if value is less than or equal to 3000 then...
            else if (y <= 3000)
            {
                //round number up to nearest 250
                number = Math.Ceiling(y / 250) * 250;
            }
            //if value is less than or equal to 5000 then...
            else if (y <= 5000)
            {
                //round number up to nearest 10
                number = Math.Ceiling(y / 500) * 500;
            }
            //if value is less than or equal to 50000 then...
            else if (y <= 50000)
            {
                //round number up to nearest 2500
                number = Math.Ceiling(y / 5000) * 5000;
            }
            //if value is less than or equal to 500000 then...
            else if (y <= 500000)
            {
                //round number up to nearest 25000
                number = Math.Ceiling(y / 25000) * 25000;
            }
            //otherwise the value is rounded to nearest 200000
            else
            {
                //round number up to nearest 200000
                number = Math.Ceiling(y / 50000) * 50000;
            }
            //return the rounded value
            return number;
        }
        /// <summary>
        /// Draws the twelve bars by calculating the dimensions and starting position
        /// </summary>
        /// <param name="xAxisStartPointYcoordinate"></param> X-axis start point Y-coordinate 
        /// <param name="xAxisStartPointXcoordinate"></param> X-axis start point X-coordinate
        /// <param name="xAxisEndPointYcoordinate"></param> X-axis end point Y-coordinate
        /// <param name="xAxisEndPointXcoordinate"></param> X-axis end point X-coordinate
        /// <param name="yAxisStartPointYcoordinate"></param> Y-axis start point Y-coordinate
        /// <param name="yAxisStartPointXcoordinate"></param> Y-axis start point X-coordinate
        /// <param name="yAxisEndPointYcoordinate"></param> Y-axis end point Y-coordinate
        /// <param name="yAxisEndPointXcoordinate"></param> Y-axis end point X-coordinate
        private void DrawBars(int xAxisStartPointYcoordinate, int xAxisStartPointXcoordinate, int xAxisEndPointYcoordinate, int xAxisEndPointXcoordinate, int yAxisStartPointYcoordinate, int yAxisStartPointXcoordinate, int yAxisEndPointYcoordinate, int yAxisEndPointXcoordinate)
        {
            //set the yPoint to Y-axis end point Y coordinate - 1 (so that the height will never be the full length of the Y-axis) and xWidth to the X-axis end point X coordinate minus X-axis start point X coordinate all divided by 25 so that there are 25 increments along the X-axis
            int yPoint = yAxisEndPointYcoordinate - 1;
            int xWidth = (xAxisEndPointXcoordinate - xAxisStartPointXcoordinate) / 25;
            int xAxisSegment = 24;
            double movementPercentage = 0.04;
            //set yaxis to the highest value of the twelve months
            double yAxis = FindHighestValue();
            //yHeight is set to default 100
            double yHeight = 100; 
            //yAxis is set to the appropriate value rounded up  
            yAxis = SetYAxisValue(yAxis);
            //assign the appropriate height values to the top point and mid point of the Y-axis
            labelYAxisHeight.Text = yAxis.ToString();
            labelMidYAxis.Text = (yAxis / 2).ToString();
            //create a brush and colour for the brush
            Brush solidBrush;
            Color solidColour;
            //assign red to the colour of the brush so the twelve bars are red
            solidColour = Color.Blue;
            //index is to identify from the twelve months
            int index;
            //create new rectangle 
            Rectangle solidRectangle;
            //this for loop goes through 24 of the 25 sections of the X-axis and is also used to draw the twelve bars and make a space between each bar
            for (int P = 1; P <= xAxisSegment; P+=2)
            {
                //the switch statement is to allocate the correct month to the correct position
                //P is the position of the 24 segments, and index is going to be one of the twelve months
                switch (P)
                {
                    case 1:
                        index = 0;
                        break;
                    case 3:
                        index = 1;
                        break;
                    case 5:
                        index = 2;
                        break;
                    case 7:
                        index = 3;
                        break;
                    case 9:
                        index = 4;
                        break;
                    case 11:
                        index = 5;
                        break;
                    case 13:
                        index = 6;
                        break;
                    case 15:
                        index = 7;
                        break;
                    case 17:
                        index = 8;
                        break;
                    case 19:
                        index = 9;
                        break;
                    case 21:
                        index = 10;
                        break;
                    default:
                        index = 11;
                        break;
                }
                //assign the starting height value of the rectangle for the current bar by calling the method
                yHeight = FindYCoord(yAxis, index, yAxisStartPointYcoordinate, yAxisEndPointYcoordinate);
                //the X coordinate is calculated by finding the difference between the start and end point of the X-axis, and multiply that by 0.04 (4%) multiplied by the segment of the bar plus the x starting point
                int xCoord = (int)((xAxisEndPointXcoordinate - xAxisStartPointXcoordinate) * (movementPercentage * P) + xAxisStartPointXcoordinate);
                //create a new rectangle with the appropriate values of starting point X, starting point Y, width and height
                solidRectangle = new Rectangle(xCoord, yPoint - (int)(yHeight), xWidth, (int)(yHeight + 1));
                //calls method to set up the locations of the labels, does one label at a time
                SetValueLabelsLocations(xCoord, yPoint - (int)(yHeight), index);
                //draws the bar(s) using paint and graphics
                using (Graphics panelGraphics = panelChart.CreateGraphics())
                using (solidBrush = new SolidBrush(solidColour))
                {
                    panelGraphics.FillRectangle(solidBrush, solidRectangle);
                }      
            }
        }
        /// <summary>
        /// sets the points/locations of the labels above the twelve bars
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="J"></param>
        private void SetValueLabelsLocations(int x, int y, int J)
        {
            //sets the string decimal or round to blank
            string roundOrDecimal = "";
            //if the type of data to be displayed is in terms of money then display to two decimal points, otherwise no decimal points
            if (money)
            {
                roundOrDecimal = "#.##";
            }
            else
            {
                roundOrDecimal = "#";
            }
            //use of numberFour and numberFifteen to avoid magic numbers
            int numberFour = 4;
            int numberFifteen = 15;

            graphLabelValues[J].Location = new Point(x - numberFour, y - numberFifteen);
            graphLabelValues[J].Text = month[J].ToString(roundOrDecimal);         
        }
        /// <summary>
        /// This method interacts with the DataProcesser class to perform the algorithms on each month which produce the values to be displayed on the graph
        /// </summary>
        private void PerformAlgorithm()
        {
            //variable to avoid magic numbers
            int finalMonth = 12;
            //creates a new object for the DataProcesser class
            DataProcesser DataProcess;
            //allocates the List<> to the object
            DataProcess = new DataProcesser(recordList);
            //display the loading bar, mostly for the large data file
            progressBarLoading.Visible = true;
            progressBarLoading.Value += 1;
            //if the first option in the comboBox is selected then follow code below
            if (comboBoxDataToDisplay.Text == "Total quantity of items sold per calendar month")
            {
                //repeat a for loop 12 times for the twelve months
                for (int m = 1; m <= finalMonth; m++)
                {
                    //for each month perform the algorithm in Dataprocesser
                    month[m - 1] = DataProcess.TotalQuantityOfItemsSoldPerCalendarMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;                 
                }
                //set money equal to false as it is not a money based display
                money = false;
            }
            //if the section option in the comboBox is selected then follow the code below
            else if (comboBoxDataToDisplay.Text == "Total value, in pounds of items sold per calendar month")
            {
                //for each month perform the algorithm in Dataprocesser
                for (int m = 1; m <= finalMonth; m++)
                {
                    month[m - 1] = DataProcess.TotalValueInPoundsOfItemsSoldPerMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;
                }
                //set money equal to true as it is a money based display
                money = true;
            }
            //if the third option in the comboBox is selected then follow the code below
            else if (comboBoxDataToDisplay.Text == "Total number of unique customers per month")
            {
                //for each month perform the algorithm in Dataprocesser
                for (int m = 1; m <= finalMonth; m++)
                {
                    month[m - 1] = DataProcess.TotalNumberOfUniqueCustomersPerMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;
                }
                //set money equal to false as it is not a money based display
                money = false;
            }
            //if the fourth option in the comboBox is selected then follow the code below
            else if (comboBoxDataToDisplay.Text == "Total number of invoices generated per month")
            {
                //for each month perform the algorithm in Dataprocesser
                for (int m = 1; m <= finalMonth; m++)
                {
                    month[m - 1] = DataProcess.TotalNumberOfInvoicesGeneratedPerMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;
                }
                //set money equal to false as it is not a money based display
                money = false;
            }
            //if the fifth option in the comboBox is selected then follow the code below
            else if (comboBoxDataToDisplay.Text == "Average number of items per invoice per calendar month")
            {
                //for each month perform the algorithm in Dataprocesser
                for (int m = 1; m <= finalMonth; m++)
                {
                    month[m - 1] = DataProcess.AverageNumberOfItemsPerInvoicePerMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;
                }
                //set money equal to false as it is not a money based display
                money = false;
            }
            //if the sixth option in the comboBox is selected then follow the code below
            else if (comboBoxDataToDisplay.Text == "Average spend, in pounds, per customer per month")
            {
                //for each month perform the algorithm in Dataprocesser
                for (int m = 1; m <= finalMonth; m++)
                {
                    month[m - 1] = DataProcess.AverageSpendInPoundsPerCustomerPerMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;
                }
                //set money equal to true as it is a money based display
                money = true;
            }
            //if the seventh option in the comboBox is selected then follow the code below
            else if (comboBoxDataToDisplay.Text == "Average spend, in pounds, per invoice per calendar month")
            {
                //for each month perform the algorithm in Dataprocesser
                for (int m = 1; m <= finalMonth; m++)
                {
                    month[m - 1] = DataProcess.AverageSpendInPoundsPerInvoicePerCalendarMonth(m);
                    //increment the loading bar value by 1 each time a month has been calculated
                    progressBarLoading.Value += 1;
                }
                //set money equal to true as it is a money based display
                money = true;
            }
            //set the visibility of the loading bar to invisible/false to hide it
            progressBarLoading.Visible = false;
            //reset the loading bar value back to 0 so it can be used again
            progressBarLoading.Value = 0;
        }
        /// <summary>
        /// calls the ResizeGraph method when the form is resized, but only if the graph has already been drawn, aka redraws if an option has already been chosen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelChart_Paint(object sender, PaintEventArgs e)
        {
            //if the graph has already been drawn, then will redraw
            if (drawn)
            {
                ResizeGraph();
            }
        }
        /// <summary>
        /// when an option in the comboBox is chosen then the DrawChart method is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDataToDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //calls the DrawChart method
            DrawChart();
            //sets the drawn boolean to true
            drawn = true;
        }
        /// <summary>
        /// switches between displaying the graph and displaying the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChange_Click(object sender, EventArgs e)
        {
            //if the chart is not being displayed then execute the code below to display the chart
            if (!chart)
            {
                //make the panel visible
                panelChart.Visible = true;
                //set chart equal to true as chart is now being shown
                chart = true;
                //enable the comboBox if a file has been loaded
                if (fileExists)
                {
                  comboBoxDataToDisplay.Enabled = true;
                }
                //set textboxes to be visible and enabled
                textBoxOutput.Visible = false;
                textBoxSearch.Enabled = false;
                buttonSearch.Enabled = false;
                groupBoxOption.Enabled = false;
                //change the text of the button to search the data instead
                buttonChange.Text = "Click to search data";
            }
            else
            {
                //make the chart invisible 
                panelChart.Visible = false;
                //set chart equal to false as the chart is not being displayed
                chart = false;
                //disable the comboBox for selecting data type to display
                comboBoxDataToDisplay.Enabled = false;
                //make the textbox to display the output visible
                textBoxOutput.Visible = true;
                //enable the search textbox and the search button and the groupBox
                textBoxSearch.Enabled = true;
                buttonSearch.Enabled = true;
                groupBoxOption.Enabled = true;
                //change the text of the button to view the graph instead
                buttonChange.Text = "Click to view graph";
            }
        }
        /// <summary>
        /// prints out the data that matches the search to the textbox
        /// </summary>
        public void PrintFile()
        {
            //sets the boolean found to false initially
            bool found = false;
            //if the button for customer ID is checked then execute the code below
            if (radioButtonCustomer.Checked)
            {
                //for each row of data in the file loaded (which is now in the recordList List<>), perform the code below
                foreach (DataManagement R in recordList)
                {
                    //if the CustomerID is equal to what was entered into the search box then display the data
                    if (R.CustomerID.ToString() == textBoxSearch.Text)
                    {
                        //append the text to the textbox, then add two clear lines below to make sure there are plenty of spacing
                        textBoxOutput.AppendText("INVOICE NUMBER: " + (R.InvoiceNo).ToString() + ", STOCK CODE: " + R.StockCode + ", DESCRIPTION: " + R.Description + ", QUANTITY: " + (R.Quantity).ToString() + ", INVOICE DATE: " + (R.InvoiceDate).ToString() + ", UNIT PRICE: " + (R.UnitPrice).ToString() + ", CUSTOMER ID: " + (R.CustomerID).ToString());
                        textBoxOutput.AppendText(Environment.NewLine);
                        textBoxOutput.AppendText(Environment.NewLine);
                        //set found equal to true as there is at least one thing that matches
                        found = true;
                    }
                }
            }
            //if the Invoice ID is checked then execute the code below
            else if(radioButtonInvoice.Checked)
            {
                //for each object in recordList repeat code below
                foreach (DataManagement R in recordList)
                {
                    //if the invoice ID matches the contents of the textbox then append the data to the textbox
                    if (R.InvoiceNo.ToString() == textBoxSearch.Text)
                    {
                        //append the data to the textbox with two lines below for spacing
                        textBoxOutput.AppendText("INVOICE NUMBER: " + (R.InvoiceNo).ToString() + ", STOCK CODE: " + R.StockCode + ", DESCRIPTION: " + R.Description + ", QUANTITY: " + (R.Quantity).ToString() + ", INVOICE DATE: " + (R.InvoiceDate).ToString() + ", UNIT PRICE: " + (R.UnitPrice).ToString() + ", CUSTOMER ID: " + (R.CustomerID).ToString());
                        textBoxOutput.AppendText(Environment.NewLine);
                        textBoxOutput.AppendText(Environment.NewLine);
                        //found is set to true as at least one row of data matches
                        found = true;
                    }
                }
            }
            //if no data is found in a search then display an error message stating that there was no data and that they should check the ID
            if (!found)
            {
                MessageBox.Show("Not a valid ID, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// when the search button is clicked the method clears the textbox and calls the PrintFile method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            textBoxOutput.Clear();
            PrintFile();
        }
        /// <summary>
        /// the purpose of this method is to re-draw the bar chart when the form is made smaller, as the chart is not redrawn/resized when the form is made smaller using the 'panelChart_Paint' method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelChart_Resize(object sender, EventArgs e)
        {
            if (drawn)
            {
                ResizeGraph();
            }
        }
    }
}