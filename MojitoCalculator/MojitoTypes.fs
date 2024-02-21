module MojitoCalculator.MojitoTypes

open Measurements
open MojitoCalculator.Measurements
open UnitOfMeasureHelpers
open MojitoCalculator.MojitoRecipeCalculations
    
type MojitoRecipeParts<[<Measure>] 'u> = 
    { BasePart: float<'u> }
    member this.Rum = this.BasePart * 3.0
    member this.Soda = this.BasePart * 2.0
    member this.SimpleSyrup = this.BasePart
    member this.LimeJuice = this.BasePart
  
type SuperJuiceRecipe =
    { Amount: float<liter> }
    member this.LimePeel = gramsLimePeelPerLiterSuperJuice * this.Amount
    member this.CitricAcid = gramsCitricAcidPerLiterSuperJuice * this.Amount
    member this.MalicAcid = gramsMalicAcidPerLiterSuperJuice * this.Amount
    member this.Water = gramsWaterPerLiterSuperJuice * this.Amount
     
type ShoppingList = 
    { Parts: MojitoRecipeParts<liter>; LimePeel: float<gram>}
    member this.Rum = this.Parts.Rum / fluidOzPerLiter / litersInLargeLiquorBottle |> roundUpMeasure
    member this.Soda = this.Parts.Soda / fluidOzPerLiter |> roundUpMeasure
    member this.OldLimes = this.Parts.LimeJuice / flOzJuicePerLime / limesPerLb |> toIntMeasure
    member this.Limes = this.LimePeel * (1.0/gramsLimeZestPerLime) / limesPerLb |> toIntMeasure
    member this.Sugar = this.Parts.SimpleSyrup * cupsPerFlOz * cupsSugarPerCup / cupsSugarPerLb |> roundUpMeasure
    member this.Mint = this.Parts.SimpleSyrup * cupsPerFlOz / cupsPerBunchMint * ozPerBunchMint |> roundUpMeasure

type SimpleSyrupRecipe = 
    { Amount: float<liter>; }
    member this.Water = calculateWaterForSimpleSyrup this.Amount
    member this.Sugar = calculateSugarForSimpleSyrup this.Amount
    member this.Mint = calculateMintForSimpleSyrup this.Amount
    
type MojitoRecipe = 
    { Amounts: MojitoRecipeParts<liter>; }
    member this.Rum = calculateRumForMojitos this.Amounts.Rum
    member this.Soda = calculateSodaForMojitos this.Amounts.Soda
    member this.SimpleSyrup = { Amount = this.Amounts.SimpleSyrup }
    member this.LimeJuice = calculateLimeJuiceForMojitos this.Amounts.LimeJuice
    member this.SuperJuice: SuperJuiceRecipe =  { Amount = this.Amounts.SimpleSyrup }

        
// 8limes/1L Juice = x limes / Amount Liters super juice
// 8limes * Amount Liters = num limes 