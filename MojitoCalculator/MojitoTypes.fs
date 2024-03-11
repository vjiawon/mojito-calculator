module MojitoCalculator.MojitoTypes

open Measurements
open MojitoCalculator.MojitoRecipeCalculations
    
type MojitoRecipeParts<[<Measure>] 'u> = {
    Rum: float<'u>
    Soda: float<'u>
    LimeJuice: float<'u>
    SimpleSyrup: float<'u>
}
  
type SuperJuiceRecipe =
    { LimePeel: float<gram>; CitricAcid: float<gram>; MalicAcid: float<gram>; Water: Ingredient<cup, cup, liter> }
     
type ShoppingList =
    { BottlesRum: int<largeLiquorBottle>; Soda: int; Limes: float<lb>; Sugar: float<lb>; Mint: int<oz> }
   
type SimpleSyrupRecipe = 
    { Water: Ingredient<cup, cup, liter>; Sugar: Ingredient<cupOfSugar, cupOfSugar, liter>; Mint: float<oz> }
        
// 8limes/1L Juice = x limes / Amount Liters super juice
// 8limes * Amount Liters = num limes 