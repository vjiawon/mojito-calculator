module Types

open Measurements
open UnitOfMeasureHelpers
open Microsoft.FSharp.Core.LanguagePrimitives

type MojitoRecipeParts<[<Measure>] 'u> = 
    { BasePart: float<'u> }
    member this.Rum = this.BasePart * 3.0
    member this.Soda = this.BasePart * 2.0
    member this.SimpleSyrup = this.BasePart
    member this.LimeJuice = this.BasePart

// a single ingredient, represented as some whole unit e.g. bottles of liquor 
// and some part measurement of that whole e.g. cups of liqor remainder
type Ingredient<[<Measure>] 'whole, [<Measure>] 'part> = 
    { Whole: int<'whole>; Part: float<'part> }

let calculateWaterForSimpleSyrup (amount: float<fluidOunce>) =
    let cupsNeeded = amount / 1.5 * cupsPerFlOz
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure )
    { Whole = wholeCupsNeeded; Part = remainderCupsNeeded }

let calculateSugarForSimpleSyrup (amount: float<fluidOunce>) =
    let cupsNeeded = amount / 1.5 * cupsPerFlOz * cupsSugarPerCup
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure )
    { Whole = wholeCupsNeeded; Part = remainderCupsNeeded }

let calculateMintForSimpleSyrup (amount: float<fluidOunce>) =
    amount * cupsPerFlOz / cupsPerBunchMint * ozPerBunchMint    

type ShoppingList = 
    { Rum: int<largeLiquorBottle>; Soda: int<liter>; Limes: int<lb>; 
        Mint: int<oz>; Sugar: int<lb> }

type SimpleSyrupRecipe = 
    { Amount: float<fluidOunce>; }
    member this.Water = calculateWaterForSimpleSyrup this.Amount
    member this.Sugar = calculateSugarForSimpleSyrup this.Amount
    member this.Mint = calculateMintForSimpleSyrup this.Amount

let calculateRumForMojitos (rumNeeded: float<fluidOunce>) =
    let liquorBottlesNeeded = rumNeeded / fluidOzPerLiter / litersInLargeLiquorBottle
    let fullBottlesNeeded = liquorBottlesNeeded |> roundDownMeasure
    let partialBottlesNeeded = liquorBottlesNeeded - (fullBottlesNeeded |> toFloatMeasure)
    let partialCups = partialBottlesNeeded * litersInLargeLiquorBottle * fluidOzPerLiter * cupsPerFlOz
    { Whole = fullBottlesNeeded; Part = partialCups }

let calculateSodaForMojitos (sodaNeeded: float<fluidOunce>) = 
    let sodaNeeded = sodaNeeded / fluidOzPerLiter
    let wholeLitersSodaNeeded = sodaNeeded |> roundDownMeasure
    let remainderSodaNeeded = (sodaNeeded - (wholeLitersSodaNeeded |> toFloatMeasure)) * cupsPerLiter
    { Whole = wholeLitersSodaNeeded; Part = remainderSodaNeeded }

let calculateLimeJuiceForMojitos (limeJuiceNeeded: float<fluidOunce>) =
    let cupsNeeded = limeJuiceNeeded * cupsPerFlOz
    let wholeCupsNeeded = cupsNeeded |> roundDownMeasure
    let partialCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure)
    { Whole = wholeCupsNeeded; Part = partialCupsNeeded; }

type MojitoRecipe = 
    { Amounts: MojitoRecipeParts<fluidOunce>; }
    member this.Rum = calculateRumForMojitos this.Amounts.Rum
    member this.Soda = calculateSodaForMojitos this.Amounts.Soda
    member this.SimpleSyrup = { Amount = this.Amounts.SimpleSyrup }
    member this.LimeJuice = calculateLimeJuiceForMojitos this.Amounts.LimeJuice