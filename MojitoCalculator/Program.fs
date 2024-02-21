// For more information see https://aka.ms/fsharp-console-apps
// I need 1 bunch of mint for 1.5 cups of simple syrup
open MojitoCalculator.Measurements
open System
open MojitoCalculator.MojitoTypes
open MojitoCalculator.TextFormatters

[<EntryPoint>]
let main argv = 
    printfn $"%A{argv}"
    
    let gallonsofMojitoToMake = (argv[0] |> float) * 1.0<gallon>
    
    // the mojito ratio is 3 parts run to two parts seltzer to 1 part lime to 1 part mint syrup
    // 3 + 2 + 1 + 1 = 7.
    // Each part is 1/7 of the recipe 
    let onePart = gallonsofMojitoToMake * litersPerGallon / 7.0
    
    let amountsNeeded = { BasePart = onePart }
    let mojitoRecipe = { Amounts = amountsNeeded }   
    let shoppingList = { Parts = amountsNeeded; LimePeel = mojitoRecipe.SuperJuice.LimePeel }    

    printfn "This is our shopping list:%s%a" Environment.NewLine shoppingListTextFormatter shoppingList
    printfn "This is our Mojito Recipe:%s%s%a" Environment.NewLine Environment.NewLine mojitoRecipeTextFormatter (mojitoRecipe, gallonsofMojitoToMake)
   
    0 // return an integer exit code
    
// todo: idea - track actuals of recipes over time 
