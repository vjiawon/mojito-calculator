module Measurements

open LanguagePrimitives

[<Measure>] type oz

// volume of liquid
[<Measure>] type gallon
[<Measure>] type liter
[<Measure>] type fluidOunce

// weight
[<Measure>] type lb

[<Measure>] type cup

[<Measure>] type tbs
[<Measure>] type lime

[<Measure>] type largeLiquorBottle

[<Measure>] type bunchMint

[<Measure>] type cupSugar

// conversion factors

let litersPerGallon = 3.78541<liter/gallon>

let fluidOzPerGallon = 128.0<fluidOunce/gallon>

let fluidOzPerLiter = 33.814<fluidOunce/liter>

let flOzJuicePerLime = 1.0<fluidOunce/lime>

let litersInLargeLiquorBottle = 1.75<liter/largeLiquorBottle>

let limesPerLb = 5.0<lime/lb>

let cupsPerLiter = 4.227<cup/liter>

let cupsPerFlOz = cupsPerLiter / fluidOzPerLiter

let cupsSugarPerCup = 1.0<cupSugar/cup>

let ozPerBunchMint = 3.2<oz/bunchMint>

let cupsSugarPerLb = 2.25<cupSugar/lb>

let cupsPerBunchMint = 1.5<cup/bunchMint>

// 1 pound of granulated sugar contains approximately 2 1/4 cups of sugar

// utilities
let roundUpMeasure<[<Measure>] 'u>(x: float<'u>): int<'u> = 
    float x |> ceil |> int |> Int32WithMeasure

let roundDownMeasure<[<Measure>] 'u>(x: float<'u>): int<'u> = 
    float x |> floor |> int |> Int32WithMeasure

let toIntMeasure<[<Measure>] 'u>(x: float<'u>): int<'u> = 
    x |> int |> Int32WithMeasure

let toFloatMeasure<[<Measure>] 'u>(x: int<'u>): float<'u> = 
    x |> float |> FloatWithMeasure

// types

// data structure to encapsulate the parts in a mojito 
// (one part simple syrup, 2 parts soda, 3 parts rum, etc.)
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
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> float |> FloatWithMeasure )
    let ingredient = { Whole = wholeCupsNeeded; Part = remainderCupsNeeded }
    ingredient

let calculateSugarForSimpleSyrup (amount: float<fluidOunce>) =
    let cupsNeeded = amount / 1.5 * cupsPerFlOz * cupsSugarPerCup
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> float |> FloatWithMeasure )
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
    let partialBottlesNeeded = liquorBottlesNeeded - (fullBottlesNeeded |> float |> FloatWithMeasure)
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