module MojitoCalculator.Measurements

open LanguagePrimitives
// imperial weights
[<Measure>] type oz
[<Measure>] type lb

// imperial volumes
[<Measure>] type gallon
[<Measure>] type fluidOz
[<Measure>] type cup
[<Measure>] type tbsp 

// metric volumes
[<Measure>] type liter

// recipe measurements - used only in the recipe
[<Measure>] type bunchOfMint
[<Measure>] type cupOfSugar

// shopping list measurements - what we acually buy in the store 
[<Measure>] type lime
[<Measure>] type largeLiquorBottle

// conversion factors

// volume conversions 
let litersPerGallon = 3.78541<liter/gallon>
let fluidOzPerGallon = 128.0<fluidOz/gallon>
let fluidOzPerLiter = 33.814<fluidOz/liter>
let cupsPerLiter = 4.227<cup/liter>

// derived volume conversions  
let cupsPerFlOz = cupsPerLiter / fluidOzPerLiter

// ingredients to volume conversions 
let flOzJuicePerLime = 1.0<fluidOz/lime>
let litersInLargeLiquorBottle = 1.75<liter/largeLiquorBottle>

// weights to ingredients 
let limesPerLb = 5.0<lime/lb>

let cupsSugarPerCup = 1.0<cupOfSugar/cup>
let cupsSugarPerLb = 2.25<cupOfSugar/lb>

let ozPerBunchMint = 3.2<oz/bunchOfMint>
let cupsPerBunchMint = 1.5<cup/bunchOfMint>
