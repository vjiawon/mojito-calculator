module MojitoCalculator.MojitoRecipeCalculations

open Measurements
open UnitOfMeasureHelpers

// a single ingredient, represented as some whole unit e.g. bottles of liquor 
// and some part measurement of that whole e.g. cups of liqor remainder
type Ingredient<[<Measure>] 'whole, [<Measure>] 'part> = 
    { Whole: int<'whole>; Part: float<'part> }

// all of the functions used to calculate the mojito ingredients

let calculateWaterForSimpleSyrup (amount: float<fluidOz>) =
    let cupsNeeded = amount / 1.5 * cupsPerFlOz
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure )
    { Whole = wholeCupsNeeded; Part = remainderCupsNeeded }

let calculateSugarForSimpleSyrup (amount: float<fluidOz>) =
    let cupsNeeded = amount / 1.5 * cupsPerFlOz * cupsSugarPerCup
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure )
    { Whole = wholeCupsNeeded; Part = remainderCupsNeeded }

let calculateMintForSimpleSyrup (amount: float<fluidOz>) =
    amount * cupsPerFlOz / cupsPerBunchMint * ozPerBunchMint   

let calculateRumForMojitos (rumNeeded: float<fluidOz>) =
    let liquorBottlesNeeded = rumNeeded / fluidOzPerLiter / litersInLargeLiquorBottle
    let fullBottlesNeeded = liquorBottlesNeeded |> roundDownMeasure
    let partialBottlesNeeded = liquorBottlesNeeded - (fullBottlesNeeded |> toFloatMeasure)
    let partialCups = partialBottlesNeeded * litersInLargeLiquorBottle * fluidOzPerLiter * cupsPerFlOz
    { Whole = fullBottlesNeeded; Part = partialCups }

let calculateSodaForMojitos (sodaNeeded: float<fluidOz>) = 
    let sodaNeeded = sodaNeeded / fluidOzPerLiter
    let wholeLitersSodaNeeded = sodaNeeded |> roundDownMeasure
    let remainderSodaNeeded = (sodaNeeded - (wholeLitersSodaNeeded |> toFloatMeasure)) * cupsPerLiter
    { Whole = wholeLitersSodaNeeded; Part = remainderSodaNeeded }

let calculateLimeJuiceForMojitos (limeJuiceNeeded: float<fluidOz>) =
    let cupsNeeded = limeJuiceNeeded * cupsPerFlOz
    let wholeCupsNeeded = cupsNeeded |> roundDownMeasure
    let partialCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure)
    { Whole = wholeCupsNeeded; Part = partialCupsNeeded; }