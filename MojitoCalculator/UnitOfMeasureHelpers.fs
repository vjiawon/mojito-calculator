module MojitoCalculator.UnitOfMeasureHelpers

open Microsoft.FSharp.Core.LanguagePrimitives

let roundUpMeasure<[<Measure>] 'u>(x: float<'u>): int<'u> = 
    float x |> ceil |> int |> Int32WithMeasure

let roundDownMeasure<[<Measure>] 'u>(x: float<'u>): int<'u> = 
    float x |> floor |> int |> Int32WithMeasure

let toIntMeasure<[<Measure>] 'u>(x: float<'u>): int<'u> = 
    x |> int |> Int32WithMeasure

let toFloatMeasure<[<Measure>] 'u>(x: int<'u>): float<'u> = 
    x |> float |> FloatWithMeasure