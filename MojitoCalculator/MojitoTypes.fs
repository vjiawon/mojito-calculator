module MojitoCalculator.MojitoTypes

open Measurements
open MojitoCalculator.Measurements
open UnitOfMeasureHelpers
open Microsoft.FSharp.Core.LanguagePrimitives
open MojitoCalculator.MojitoRecipeCalculations

type SuperJuiceRecipe =
    { Amount: float<liter> }
    member this.LimePeel = limePeelToLiterSuperJuice * this.Amount
    member this.CitricAcid = citricAcidToLiterSuperJuice * this.Amount
    member this.MalicAcid = malicAcidToLiterSuperJuice * this.Amount
    member this.Water = waterToLiterSuperJuice * this.Amount

type MojitoRecipeParts<[<Measure>] 'u> = 
    { BasePart: float<'u> }
    member this.Rum = this.BasePart * 3.0
    member this.Soda = this.BasePart * 2.0
    member this.SimpleSyrup = this.BasePart
    member this.LimeJuice = this.BasePart
     
type ShoppingList = 
    { Parts: MojitoRecipeParts<fluidOz>; }
    member this.Rum = this.Parts.Rum / fluidOzPerLiter / litersInLargeLiquorBottle |> roundUpMeasure
    member this.Soda = this.Parts.Soda / fluidOzPerLiter |> roundUpMeasure
    member this.Limes = this.Parts.LimeJuice / flOzJuicePerLime / limesPerLb |> toIntMeasure
    member this.Sugar = this.Parts.SimpleSyrup * cupsPerFlOz * cupsSugarPerCup / cupsSugarPerLb |> roundUpMeasure
    member this.Mint = this.Parts.SimpleSyrup * cupsPerFlOz / cupsPerBunchMint * ozPerBunchMint |> roundUpMeasure

type SimpleSyrupRecipe = 
    { Amount: float<fluidOz>; }
    member this.Water = calculateWaterForSimpleSyrup this.Amount
    member this.Sugar = calculateSugarForSimpleSyrup this.Amount
    member this.Mint = calculateMintForSimpleSyrup this.Amount
    
type MojitoRecipe = 
    { Amounts: MojitoRecipeParts<fluidOz>; }
    member this.Rum = calculateRumForMojitos this.Amounts.Rum
    member this.Soda = calculateSodaForMojitos this.Amounts.Soda
    member this.SimpleSyrup = { Amount = this.Amounts.SimpleSyrup }
    member this.LimeJuice = calculateLimeJuiceForMojitos this.Amounts.LimeJuice
    member this.SuperJuice: SuperJuiceRecipe =  { Amount = 1.0/fluidOzPerLiter * this.Amounts.LimeJuice }

        
// 8limes/1L Juice = x limes / Amount Liters super juice
// 8limes * Amount Liters = num limes 