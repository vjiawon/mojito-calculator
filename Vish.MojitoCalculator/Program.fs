// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

// I need 1 bunch of mint for 1.5 cups of simple syrup
open Measurements
open LanguagePrimitives
open System
open System.IO

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

    // figure out what one part is in fluid oz
    let onePart = 4.5 * 1.0<gallon> * fluidOzPerGallon / 7.0

    // everything else we need relative to one part in that same 
    // measurement. These are the ratios based on some unit (the basis being
    // one part)
    let amountsNeeded = { BasePart = onePart }

    // figure out what we should buy based on the amounts needed
    let shoppingList = buildShoppingList amountsNeeded

    // figure out what we should actually use in the recipe
    // how much rum, soda, simple syrup, lime juice in the mojito
    // how much sugar, water, bunches of mint in the simple syrup?
    //let simpleSyrupRecipe = 
    //    { Amount = amountsNeeded.SimpleSyrup }

    let mojitoRecipe = { Amounts = amountsNeeded }
    // print out everything: what we'll need to buy and what we'll need 
    // to use
    printfn "This is our shopping list:%s%a" Environment.NewLine shoppingListTextFormatter shoppingList
    //printfn "This is our simple syrup recipe:%s%a" Environment.NewLine simpleSyrupRecipeTextFormatter simpleSyrupRecipe
    printfn "This is our Mojito Recipe:%s%a" Environment.NewLine mojitoRecipeTextFormatter mojitoRecipe
    // the simple syrup recipe is 3 ingredients 

    //// what we need for the recipe in fl oz
    //let rumNeeded = onePart*3.0 /fluidOzPerLiter
    //let sodaNeeded = onePart*2.0 /fluidOzPerLiter
    //let limeJuiceNeeded = onePart
    //let simpleSyrupNeeded = onePart

    //// how much rum do we need to buy -> round up to the nearest bottle 
    //// because we are going to assume that we only buy large bottles 
    //let bottlesNeeded =  rumNeeded / litersInLargeLiquorBottle |> roundUpMeasure 

    //// how much rum do we need to use?
    //// find out how many full bottles, and how many cups 
    //// the remainder of bottles needed / rumNeeded will be the amount of rum 
    //// outside of a single bottle. Let's convert that amount to cups
    //let extraRum = bottlesNeeded * litersInLargeLiquorBottle % rumNeeded * cupsPerLiter
    //let bottlesToUse = 
    //    if extraRum = 0.0<cup>
    //    then bottlesNeeded
    //    else bottlesNeeded - 1.0<largeLiquorBottle>
    //// use bottlesToUse rum + an additional extraRum cups

    //// how much soda do we need to buy
    //let wholeUnitsSodaNeeded = sodaNeeded |> float |> floor |> int |> Int32WithMeasure
    //let remainderSoda = (sodaNeeded - (sodaNeeded |> float |> floor |> FloatWithMeasure)) * cupsPerLiter
    //// we need wholeUnitsSodaNeeded + remainderSoda cups 

    //// how many lbs limes do we need to buy
    //let lbsLimesNeeded = limeJuiceNeeded / flOzJuicePerLime / limesPerLb

    //// how much sugar do I need to buy? Simple Syrup is equal parts sugar and water
    //// equal parts by volume. Therefore, I'll convert the fl oz of simple syrup to cups 
    //// to get the cups of sugar 
    //let cupsSugarNeeded = simpleSyrupNeeded * cupsPerFlOz

    //printfn "You need %f liters rum" rumNeeded
    //printfn "You need %f liters soda" sodaNeeded
    //printfn "You need %f liters lime juice and simple syrup" onePart

    //printfn "You'll need to buy %f bottles. You'll use %f bottles and an additional %f cups" 
    //    bottlesNeeded bottlesToUse extraRum

    //printfn "You'll need to use %d liters soda and an additional %f cups" wholeUnitsSodaNeeded remainderSoda

    //printfn "You'll need to buy %f lbs of limes" lbsLimesNeeded

    //printfn "You'll need %f cups of sugar" cupsSugarNeeded

    Console.ReadLine() |> ignore

    0 // return an integer exit code
