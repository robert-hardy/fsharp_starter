// ============================================================================
// SETUP: Some magic tricks for the script Walkthroughs!
// ============================================================================

let __<'T> : 'T =
  failwith "A place holder '__' has not been filled!"

let shouldEqual (a:'T) b =
  if typeof<'T> = typeof<float> then
    let a = unbox<float> a
    let b = unbox<float> b
    if a > b - 0.0000001 && a < b + 0.0000001 then ()
    else failwithf "The 'shouldEqual' operation failed!\nFirst: %A\nSecond: %A" a b
  else if not (a = b) then failwithf "The 'shouldEqual' operation failed!\nFirst: %A\nSecond: %A" a b

// Loading Stock prices using F# Data (Covered in later lectures)
#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"
open System
open FSharp.Data

let [<Literal>] Sample = __SOURCE_DIRECTORY__ + "/msft.csv"
type Stocks = CsvProvider<Sample>

type Price = { Date : DateTime; Open : float; Close : float; Low : float; High : float }

let loadData ticker =
  Stocks.Load(__SOURCE_DIRECTORY__ + "/" + ticker + ".csv")

let convert (r:Stocks.Row) =
  { Date = r.Date; Open = float r.Open; Close = float r.Close;
    High = float r.High; Low = float r.Low }

module Yahoo =
  let MSFT = (loadData "msft").Rows |> Seq.map convert |> List.ofSeq |> List.rev
  let AAPL = (loadData "aapl").Rows |> Seq.map convert |> List.ofSeq |> List.rev
