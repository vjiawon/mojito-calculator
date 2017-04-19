module Types

open Measurements
open UnitOfMeasureHelpers
open Microsoft.FSharp.Core.LanguagePrimitives
open Calculations

type MojitoRecipeParts<[<Measure>] 'u> = 
    { BasePart: float<'u> }
    member this.Rum = this.BasePart * 3.0
    member this.Soda = this.BasePart * 2.0
    member this.SimpleSyrup = this.BasePart
    member this.LimeJuice = this.BasePart
     
type ShoppingList = 
    { Parts: MojitoRecipeParts<fluidOunce>; }
    member this.Rum = this.Parts.Rum / fluidOzPerLiter / litersInLargeLiquorBottle |> roundUpMeasure
    member this.Soda = this.Parts.Soda / fluidOzPerLiter |> roundUpMeasure
    member this.Limes = this.Parts.LimeJuice / flOzJuicePerLime / limesPerLb |> toIntMeasure
    member this.Sugar = this.Parts.SimpleSyrup * cupsPerFlOz * cupsSugarPerCup / cupsSugarPerLb |> roundUpMeasure
    member this.Mint = this.Parts.SimpleSyrup * cupsPerFlOz / cupsPerBunchMint * ozPerBunchMint |> roundUpMeasure

type SimpleSyrupRecipe = 
    { Amount: float<fluidOunce>; }
    member this.Water = calculateWaterForSimpleSyrup this.Amount
    member this.Sugar = calculateSugarForSimpleSyrup this.Amount
    member this.Mint = calculateMintForSimpleSyrup this.Amount
    
type MojitoRecipe = 
    { Amounts: MojitoRecipeParts<fluidOunce>; }
    member this.Rum = calculateRumForMojitos this.Amounts.Rum
    member this.Soda = calculateSodaForMojitos this.Amounts.Soda
    member this.SimpleSyrup = { Amount = this.Amounts.SimpleSyrup }
    member this.LimeJuice = calculateLimeJuiceForMojitos this.Amounts.LimeJuice