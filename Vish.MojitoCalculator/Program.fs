// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

// I need 1 bunch of mint for 1.5 cups of simple syrup
open Measurements
open LanguagePrimitives
open System
open System.IO
open UnitOfMeasureHelpers
open Types

let buildShoppingList (parts: MojitoRecipeParts<fluidOunce>) =   
    { Rum = parts.Rum / fluidOzPerLiter / litersInLargeLiquorBottle |> roundUpMeasure;
      Soda = parts.Soda / fluidOzPerLiter |> roundDownMeasure;
      Limes = parts.LimeJuice / flOzJuicePerLime / limesPerLb |> toIntMeasure;
      Sugar = parts.SimpleSyrup * cupsPerFlOz * cupsSugarPerCup / cupsSugarPerLb |> roundUpMeasure;
      Mint = parts.SimpleSyrup * cupsPerFlOz / cupsPerBunchMint * ozPerBunchMint |> roundUpMeasure; }

let shoppingListTextFormatter (tw: TextWriter) (list: ShoppingList) = 
    tw.WriteLine("{0} large liquor bottles rum", list.Rum)
    tw.WriteLine("{0} liters soda", list.Soda)
    tw.WriteLine("{0} pounds limes", list.Limes)
    tw.WriteLine("{0} pounds sugar", list.Sugar)
    tw.WriteLine("{0} ounces Mint", list.Mint)

let simpleSyrupRecipeTextFormatter (tw: TextWriter) (s: SimpleSyrupRecipe) =
    tw.WriteLine("{0} and {1:0.0#} cups sugar", s.Sugar.Whole, s.Sugar.Part)
    tw.WriteLine("{0} and {1:0.0#} cups water", s.Water.Whole, s.Water.Part)
    tw.WriteLine("{0:0.0#} ounces mint", s.Mint)

let mojitoRecipeTextFormatter (tw: TextWriter) (m: MojitoRecipe) = 
    tw.WriteLine("{0} bottles rum and {1:0.0#} additional cups", m.Rum.Whole, m.Rum.Part)
    tw.WriteLine("{0} liters soda and {1:0.0#} additional cups", m.Soda.Whole, m.Soda.Part)
    simpleSyrupRecipeTextFormatter tw m.SimpleSyrup
    tw.WriteLine("{0} cups lime juice and an additional {1:0.0#} cups", m.LimeJuice.Whole, m.LimeJuice.Part)

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let onePart = 4.5 * 1.0<gallon> * fluidOzPerGallon / 7.0
    
    let amountsNeeded = { BasePart = onePart }

    let shoppingList = buildShoppingList amountsNeeded
    
    let mojitoRecipe = { Amounts = amountsNeeded }   

    printfn "This is our shopping list:%s%a" Environment.NewLine shoppingListTextFormatter shoppingList
    printfn "This is our Mojito Recipe:%s%a" Environment.NewLine mojitoRecipeTextFormatter mojitoRecipe
   
    Console.ReadLine() |> ignore

    0 // return an integer exit code
