// For more information see https://aka.ms/fsharp-console-apps
// I need 1 bunch of mint for 1.5 cups of simple syrup
open MojitoCalculator.Measurements
open MojitoCalculator.UnitOfMeasureHelpers
open MojitoCalculator.MojitoRecipeCalculations
open MojitoCalculator.MojitoTypes

let calculateMojitoParts (gallonsofMojitoToMake: float<gallon>) =
    // the mojito ratio is 3 parts run to two parts seltzer to 1 part lime to 1 part mint syrup
    // 3 + 2 + 1 + 1 = 7.
    // Each part is 1/7 of the recipe 
    let onePart = gallonsofMojitoToMake * litersPerGallon / 7.0
    { Rum = onePart * 3.0; Soda = onePart * 2.0; LimeJuice = onePart; SimpleSyrup = onePart; }

let calculateSuperJuiceRecipe (limeJuice: float<liter>): SuperJuiceRecipe =
    // 1L = 100g lime peels + 44g Citric Acid + 7g Malic Acid + 1L Water
    { LimePeel = limePeelPerLiterSuperJuice * limeJuice;
       CitricAcid = gramsCitricAcidPerLiterSuperJuice * limeJuice;
       MalicAcid = gramsMalicAcidPerLiterSuperJuice * limeJuice;
       Water = calculateCupsNeededForLiters limeJuice }

let calculateSimpleSyrupRecipe (total: float<liter>): SimpleSyrupRecipe =
    { Water = calculateWaterForSimpleSyrup total
      Sugar = calculateSugarForSimpleSyrup total
      Mint = calculateMintForSimpleSyrup total }

let calculateShoppingList (limePeel: float<gram>)
    (sugar: Ingredient<cupOfSugar,cupOfSugar,liter>)
    (soda: float<liter>)
    (rum: float<liter>)
    (simpleSyrup: float<liter>): ShoppingList =
    // todo - calculate 1.5 l vs 750ml. Bottles of rum can be 1.5L vs 750ml
    // todo - calculate 1 vs 2 l bottles soda
    { BottlesRum =  rum   / litersInLargeLiquorBottle |> roundUpMeasure;
      Soda =  soda |> roundUpMeasure |> int;
      Limes = limePeel * (1.0/gramsLimeZestPerLime) / limesPerLb;
      Sugar = sugar.Total * cupsPerLiter * cupsSugarPerCup * (1.0/cupsSugarPerLb);
      Mint = simpleSyrup * cupsPerLiter / cupsPerBunchMint * ozPerBunchMint |> roundUpMeasure;
    }

[<EntryPoint>]
let main argv = 
    printfn $"%A{argv}"
    
    let gallonsofMojitoToMake: float<gallon> = argv[0] |> float |> LanguagePrimitives.FloatWithMeasure
    
    // the recipes. overallMojitoRecipe should also be ingredient type
    let overallMojitoRecipe = calculateMojitoParts gallonsofMojitoToMake    
    let superJuiceRecipe = calculateSuperJuiceRecipe overallMojitoRecipe.LimeJuice
    let simpleSyrupRecipe = calculateSimpleSyrupRecipe overallMojitoRecipe.SimpleSyrup
      
    // the shopping list   
    let numberOfLimesToBuy = superJuiceRecipe.LimePeel * (1.0/gramsLimeZestPerLime)
    
    let lbsLimesToBuy: float<lb> = numberOfLimesToBuy / limesPerLb
    
    // calculate the lbs of sugar to buy - conversion factor?
    let lbsSugarToBuy: float<lb> = simpleSyrupRecipe.Sugar.Total * cupsPerLiter * cupsSugarPerCup * (1.0/cupsSugarPerLb)

    // calculate the actual amount of rum to use in bottles and cups
    let rumToUse = calculateRumForMojitos overallMojitoRecipe.Rum
    let sodaToUse = calculateSodaForMojitos overallMojitoRecipe.Soda
    let simpleSyrupToUse = calculateSimpleSyrupForMojitos overallMojitoRecipe.SimpleSyrup
    let limeJuiceToUse = calculateLimeJuiceForMojitos overallMojitoRecipe.LimeJuice
    
    printfn $"To make %f{gallonsofMojitoToMake} gallons of mojitos you will need:"
    printfn $"\t%i{rumToUse.Whole} bottles and %f{rumToUse.Part} cups of rum for a total of %f{rumToUse.Total} liters."
    printfn $"\t%i{sodaToUse.Whole} liters and %f{sodaToUse.Part} cups of soda for a total of %f{sodaToUse.Total} liters."
    printfn $"\t%i{simpleSyrupToUse.Whole} and %f{simpleSyrupToUse.Part} cups of simple syrup for a total of %f{simpleSyrupToUse.Total} liters."
    printfn $"\t%i{limeJuiceToUse.Whole} and %f{limeJuiceToUse.Part} cups of lime juice for a total of %f{limeJuiceToUse.Total} liters."
    printfn ""
    
    // simple syrup recipe
    printfn $"To make %f{simpleSyrupToUse.Total} liters of simple syrup you will need:"
    printfn $"\t%i{simpleSyrupRecipe.Water.Whole} and %f{simpleSyrupRecipe.Water.Part} cups of water for a total of %f{simpleSyrupRecipe.Water.Total} liters."
    printfn $"\t%i{simpleSyrupRecipe.Sugar.Whole} and %f{simpleSyrupRecipe.Sugar.Part} cups of sugar for a total of %f{simpleSyrupRecipe.Sugar.Total} liters."
    printfn ""
    
    // super juice recipe
    printfn $"To make %f{overallMojitoRecipe.LimeJuice} liters of super juice you will need:"
    printfn $"\t%f{superJuiceRecipe.LimePeel} grams of lime peels from approximately %f{numberOfLimesToBuy} limes."
    printfn $"\t%f{superJuiceRecipe.CitricAcid} grams of citric acid."
    printfn $"\t%f{superJuiceRecipe.MalicAcid} grams of malic acid."
    printfn $"\t%i{superJuiceRecipe.Water.Whole} and %f{superJuiceRecipe.Water.Part} cups of water for a total of %f{superJuiceRecipe.Water.Total} liters."
    printfn ""
    
    // shopping list
    let shoppingList = calculateShoppingList superJuiceRecipe.LimePeel simpleSyrupRecipe.Sugar overallMojitoRecipe.Soda overallMojitoRecipe.Rum overallMojitoRecipe.SimpleSyrup
    printfn $"To make all of this you will need:"
    printfn $"\t%i{shoppingList.BottlesRum} 1.5L bottles of rum."
    printfn $"\t%i{shoppingList.Soda} 1L bottles of soda."
    printfn $"\t%f{lbsLimesToBuy} lbs of limes."
    printfn $"\t%f{lbsSugarToBuy} lbs of sugar."
    
    // todo - refactor the file to group data that's used together, then pull those out into functions. 
    
    0 // return an integer exit code
    
// todo: idea - track actuals of recipes over time 
