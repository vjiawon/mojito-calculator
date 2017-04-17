module Measurements

open LanguagePrimitives
open UnitOfMeasureHelpers

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