// For more information see https://aka.ms/fsharp-console-apps
// I need 1 bunch of mint for 1.5 cups of simple syrup
open MojitoCalculator.Measurements
open MojitoCalculator.UnitOfMeasureHelpers
open MojitoCalculator.MojitoRecipeCalculations

[<EntryPoint>]
let main argv = 
    printfn $"%A{argv}"
    
    let gallonsofMojitoToMake: float<gallon> = argv[0] |> float |> LanguagePrimitives.FloatWithMeasure
    
    // the mojito ratio is 3 parts run to two parts seltzer to 1 part lime to 1 part mint syrup
    // 3 + 2 + 1 + 1 = 7.
    // Each part is 1/7 of the recipe 
    let onePart = gallonsofMojitoToMake * litersPerGallon / 7.0
    let rumNeeded = onePart * 3.0 // three parts rum
    let sodaNeeded = onePart * 2.0 // two parts soda
    let limeJuiceNeeded = onePart
    let simpleSyrupNeeded = onePart
    
    // todo - calculate 1.5 l vs 750ml. Bottles of rum can be 1.5L vs 750ml
    let bottlesRumToBuy = rumNeeded * (1.0/fluidOzPerLiter) * (1.0/litersInLargeLiquorBottle) |> roundUpMeasure
    // todo - calculate 1 vs 2 l bottles 
    let literBottlesOfSodaToBuy = sodaNeeded |> roundUpMeasure
    
    // I need limeJuiceNeeded Super Juice. Calculate how many limes, citric acid, malic acid, and water I need for super juice 
    // 1L = 100g lime peels + 44g Citric Acid + 7g Malic Acid + 1L Water
    // todo - these are conversion factors 
    let limePeelNeededForSuperJuice: double<gram> = 100.0<gram> * limeJuiceNeeded/1.0<liter>
    let citricAcidNeededForSuperJuice: double<gram> = 44.0<gram> * limeJuiceNeeded/1.0<liter>
    let malicAcidNeededForSuperJuice: double<gram> = 7.0<gram> * limeJuiceNeeded/1.0<liter>
    let waterNeededForSuperJuice = calculateCupsNeededForLiters limeJuiceNeeded
    
    // Simple Syrup Recipe: 1L Simple Syrup = 1L Water + 1L Sugar
    let waterNeededForSimpleSyrup = calculateCupsNeededForLiters simpleSyrupNeeded
    let sugarNeededForSimpleSyrup = waterNeededForSimpleSyrup
    
    let numberOfLimesToBuy = limePeelNeededForSuperJuice * (1.0/gramsLimeZestPerLime)
    
    let lbsLimesToBuy: float<lb> = numberOfLimesToBuy / limesPerLb
    
    // calculate the lbs of sugar to buy
    let lbsSugarToBuy: float<lb> = sugarNeededForSimpleSyrup.Total * cupsPerLiter * cupsSugarPerCup * (1.0/cupsSugarPerLb)

    // calculate the actual amount of rum to use in bottles and cups
    let rumToUse = calculateRumForMojitos rumNeeded
    let sodaToUse = calculateSodaForMojitos sodaNeeded
    let simpleSyrupToUse = calculateSimpleSyrupForMojitos simpleSyrupNeeded
    let limeJuiceToUse = calculateLimeJuiceForMojitos limeJuiceNeeded
    
    printfn $"To make %f{gallonsofMojitoToMake} gallons of mojitos you will need:"
    printfn $"\t%i{rumToUse.Whole} bottles and %f{rumToUse.Part} cups of rum for a total of %f{rumToUse.Total} liters."
    printfn $"\t%i{sodaToUse.Whole} liters and %f{sodaToUse.Part} cups of soda for a total of %f{sodaToUse.Total} liters."
    printfn $"\t%i{simpleSyrupToUse.Whole} and %f{simpleSyrupToUse.Part} cups of simple syrup for a total of %f{simpleSyrupToUse.Total} liters."
    printfn $"\t%i{limeJuiceToUse.Whole} and %f{limeJuiceToUse.Part} cups of lime juice for a total of %f{limeJuiceToUse.Total} liters."
    printfn ""
    
    // simple syrup recipe
    printfn $"To make %f{simpleSyrupToUse.Total} liters of simple syrup you will need:"
    printfn $"\t%i{waterNeededForSimpleSyrup.Whole} and %f{waterNeededForSimpleSyrup.Part} cups of water for a total of %f{waterNeededForSimpleSyrup.Total} liters."
    printfn $"\t%i{sugarNeededForSimpleSyrup.Whole} and %f{sugarNeededForSimpleSyrup.Part} cups of water for a total of %f{sugarNeededForSimpleSyrup.Total} liters."
    printfn ""
    
    // super juice recipe
    printfn $"To make %f{limeJuiceNeeded} liters of super juice you will need:"
    printfn $"\t%f{limePeelNeededForSuperJuice} grams of lime peels from approximately %f{numberOfLimesToBuy} limes."
    printfn $"\t%f{citricAcidNeededForSuperJuice} grams of citric acid."
    printfn $"\t%f{malicAcidNeededForSuperJuice} grams of malic acid."
    printfn $"\t%i{waterNeededForSuperJuice.Whole} and %f{waterNeededForSuperJuice.Part} cups of water for a total of %f{waterNeededForSuperJuice.Total} liters."
    printfn ""
    
    // shopping list
    printfn $"To make all of this you will need:"
    printfn $"\t%i{bottlesRumToBuy} 1.5L bottles of rum."
    printfn $"\t%i{literBottlesOfSodaToBuy} 1L bottles of soda."
    printfn $"\t%f{lbsLimesToBuy} lbs of limes."
    printfn $"\t%f{lbsSugarToBuy} lbs of sugar."
    
    //
    // let amountsNeeded = { BasePart = onePart }
    // let mojitoRecipe = { Amounts = amountsNeeded }   
    // let shoppingList = { Parts = amountsNeeded; LimePeel = mojitoRecipe.SuperJuice.LimePeel }    
    //
    // printfn "This is our shopping list:%s%a" Environment.NewLine shoppingListTextFormatter shoppingList
    // printfn "This is our Mojito Recipe:%s%s%a" Environment.NewLine Environment.NewLine mojitoRecipeTextFormatter (mojitoRecipe, gallonsofMojitoToMake)
   
    0 // return an integer exit code
    
// todo: idea - track actuals of recipes over time 
