// ============================================================================
// DATA 1: List processing functions
// ============================================================================

#load "setup.fsx"
open System
open Setup

// ----------------------------------------------------------------------------
// WALKTHROUGH: List processing functions
// ----------------------------------------------------------------------------

// In this sample, we're going to be working with MSFT and AAPL stock prices.
// To explore the data set, we can get the first and lst value and print
// some details about it using the 'Seq.head' and 'Seq.last' functions.
let af = Yahoo.AAPL |> Seq.head
let al = Yahoo.AAPL |> Seq.last

// In this part, we're going to be using mostly functions from the 'Seq' and
// 'List' modules. There is a number of useful functions that take other
// functions as argument. For example, to calculate the average opening price:

let getOpenPrice (item:Price) =
  item.Open

let avgOpen1 =
  Yahoo.AAPL
  |> Seq.map getOpenPrice
  |> Seq.average

// The same code can be written more easily using inline lambda function
let avgOpen2 =
  Yahoo.AAPL
  |> Seq.map (fun item -> item.Open)
  |> Seq.average

shouldEqual (int avgOpen1) (int avgOpen2)

// Calculate the average opening and average closing price of MSFT stock prices. 
// Then calculate the differnece between closing and opening prices in cents 
// (times 100.0). Rounded, both prices are $61, but difference should be 5c

let avgOpen = __
let avgClose = __
let difference = __
shouldEqual (round avgOpen) 61.0
shouldEqual (round avgClose) 61.0
shouldEqual (round difference) 5.0

// You can compose Seq.filter, Seq.map, Seq.average and other functions to
// calculate pretty much anything you need! For example, the average opening
// price in the year 2013 can be calculated as follows:

let aapl2013 =
  Yahoo.AAPL
  |> Seq.filter (fun item -> item.Date.Year = 2013)
  |> Seq.map (fun item -> item.Open)
  |> Seq.average


// Another useful function is 'Seq.max' and 'Seq.min'. Use them to calcualte
// the highest and lowest opening MSFT price in the year 2013
let msftMax2013 = __
let msftMin2013 = __

shouldEqual msftMax2013 37.96
shouldEqual msftMin2013 26.49

// You can use 'Seq.pairwise' to get a list of pairs containing the
// previous and the next value. For example, to calculate the average
// difference in MSFT opening prices, you can use:
let avgChange =
  Yahoo.MSFT
  |> Seq.map (fun item -> item.Open)
  |> Seq.pairwise
  |> Seq.map (fun (prev, next) -> next - prev)
  |> Seq.average

// In later exercises, you may need two numerical functions.
// The 'sqrt' function is built-in function to calcualte square
// root and 'pown' is a function to calculate nth-power:

let demo = sqrt (float (pown 2.0 2))

// ============================================================================
// TASKS: Working with lists
// ============================================================================

// TASK #1: Find the number of days in the MSFT history when closing
// price is larger than opening price by more than $5.
let msftUpBy5 = __
let aaplUpBy5 = __

shouldEqual msftUpBy5 46
shouldEqual aaplUpBy5 193


// TASK #2: What is the largest difference between the 'Close' price on
// the previous day and the 'Open' price of the next day for two consecutive
// days (i.e. how much has the price changed overnight)? You can use the
// 'abs' function to handle both price going up & down.
let msftOvernight = __
let aaplOvernight = __

shouldEqual msftOvernight 88.01
shouldEqual aaplOvernight 55.36


// TASK #3: Write a function to calculate the standard deviation of the specified
// sequence of stock prices. Use the formula (written using pseudo code):
//
//   sdv = sqrt (1/N * SUM_i (v_i - avg)^2)
//
// For more information, see the Wikipedia page:
// http://en.wikipedia.org/wiki/Standard_deviation#Corrected_sample_standard_deviation

let sdv (data:seq<float>) =
  __

// Calculate the standard deviation of MFST opening prices
shouldEqual (round (sdv (Seq.map getOpenPrice Yahoo.MSFT))) 34.0
shouldEqual (round (sdv (Seq.map getOpenPrice Yahoo.AAPL))) __

// Calculate the standard deviation of MFST opening prices in the year 2013
// (Use 'Seq.filter' to filter the data before callling your 'sdv' function)
let msft2013sdv = __
shouldEqual (round msft2013sdv) 3.0
