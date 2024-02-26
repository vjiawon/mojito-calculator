module MojitoCalculator.MojitoRecipeCalculations

open Measurements
open UnitOfMeasureHelpers

// all of the functions used to calculate the mojito ingredients

// a single ingredient, represented as some whole unit e.g. bottles of liquor 
// and some part measurement of that whole e.g. cups of liqor remainder
type Ingredient<[<Measure>] 'whole, [<Measure>] 'part, [<Measure>] 'total> = 
    { Whole: int<'whole>; Part: float<'part>; Total: float<'total> }
    
// todo: write specific function that calculates simple syrup by weight 

let calculateWaterForSimpleSyrup (amount: float<liter>) =
    let cupsNeeded = amount / 1.5 * cupsPerLiter
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure )
    { Whole = wholeCupsNeeded; Part = remainderCupsNeeded; Total = amount }

let calculateSugarForSimpleSyrup (amount: float<liter>) =
    let cupsNeeded = amount / 1.5 * cupsPerLiter * cupsSugarPerCup
    let wholeCupsNeeded = roundDownMeasure cupsNeeded
    let remainderCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure )
    { Whole = wholeCupsNeeded; Part = remainderCupsNeeded; Total = amount }

let calculateMintForSimpleSyrup (amount: float<liter>) =
    amount * cupsPerLiter / cupsPerBunchMint * ozPerBunchMint   

let calculateRumForMojitos (rumNeeded: float<liter>) =
    let liquorBottlesNeeded = rumNeeded / fluidOzPerLiter / litersInLargeLiquorBottle
    let fullBottlesNeeded = liquorBottlesNeeded |> roundDownMeasure
    let partialBottlesNeeded = liquorBottlesNeeded - (fullBottlesNeeded |> toFloatMeasure)
    let partialCups = partialBottlesNeeded * litersInLargeLiquorBottle * fluidOzPerLiter * cupsPerLiter
    { Whole = fullBottlesNeeded; Part = partialCups; Total = rumNeeded }

let calculateSodaForMojitos (sodaNeeded: float<liter>) = 
    let wholeLitersSodaNeeded = sodaNeeded |> roundDownMeasure
    let remainderSodaNeeded = (sodaNeeded - (wholeLitersSodaNeeded |> toFloatMeasure)) * cupsPerLiter
    { Whole = wholeLitersSodaNeeded; Part = remainderSodaNeeded; Total = sodaNeeded }

let calculateLimeJuiceForMojitos (limeJuiceNeeded: float<liter>) =
    let cupsNeeded = limeJuiceNeeded * cupsPerLiter
    let wholeCupsNeeded = cupsNeeded |> roundDownMeasure
    let partialCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure)
    { Whole = wholeCupsNeeded; Part = partialCupsNeeded; Total = limeJuiceNeeded }
    
let calculateSimpleSyrupForMojitos (simpleSyrupNeeded: float<liter>) =
    let cupsNeeded = simpleSyrupNeeded * cupsPerLiter
    let wholeCupsNeeded = cupsNeeded |> roundDownMeasure
    let partialCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure)
    { Whole = wholeCupsNeeded; Part = partialCupsNeeded; Total = simpleSyrupNeeded }
    
let calculateCupsNeededForLiters (l: float<liter>) =
    let cupsNeeded = l * cupsPerLiter
    let wholeCupsNeeded = cupsNeeded |> roundDownMeasure
    let partialCupsNeeded = cupsNeeded - (wholeCupsNeeded |> toFloatMeasure)
    { Whole = wholeCupsNeeded; Part = partialCupsNeeded; Total = l }
    // calculateWaterForSimpleSyrup, calculateSugarForSimpleSyrup, calculateSodaForMojitos
    // calculateLimeJuiceForMojitos, calculateSimpleSyrupForMojitos
    // all have comonalities 