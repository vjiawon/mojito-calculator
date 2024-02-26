module MojitoCalculator.TextFormatters

open System.IO
open MojitoCalculator.Measurements
open MojitoCalculator.MojitoTypes

let shoppingListTextFormatter (tw: TextWriter) (list: ShoppingList) = 
    tw.WriteLine("{0} large liquor bottles rum", list.Rum)
    tw.WriteLine("{0} liters soda", list.Soda)
    tw.WriteLine("{0} pounds limes in the old method", list.OldLimes)
    tw.WriteLine("{0} pounds limes", list.Limes)
    tw.WriteLine("{0} pounds sugar", list.Sugar)
    tw.WriteLine("{0} ounces Mint", list.Mint)

// todo: Create formatters for incredient entry of format "\t{0} liters of simple syrup," and mess around the the f# printfn options
let mojitoRecipeTextFormatter (tw: TextWriter) (m: MojitoRecipe, amount: float<gallon>) =
    
    tw.WriteLine("\tTo make {0} gallons of mojitos you will need:", amount)
    tw.WriteLine("\t\t{0} liters of simple syrup,", m.SimpleSyrup.Amount)
    tw.WriteLine("\t\t{0} liters of lime juice", m.LimeJuice.Total)
    tw.WriteLine("\t\t{0} liters of seltzer", m.Soda.Total)
    tw.WriteLine("\t\t{0} liters of rum", m.Rum.Total)
    tw.WriteLine();
    
    tw.WriteLine("\tTo make the simple syrup you will need:")
    tw.WriteLine("\t\t{0} and {1:0.0#} cups sugar", m.SimpleSyrup.Sugar.Whole, m.SimpleSyrup.Sugar.Part)
    tw.WriteLine("\t\t{0} and {1:0.0#} cups water", m.SimpleSyrup.Water.Whole, m.SimpleSyrup.Water.Part)
    tw.WriteLine("\t\t{0:0.0#} ounces mint", m.SimpleSyrup.Mint)
    tw.WriteLine()
    
    tw.WriteLine("\tTo make the lime juice from super juice you will need:")
    tw.WriteLine("\t\t{0:0} grams of Lime Peel", m.SuperJuice.LimePeel)
    tw.WriteLine("\t\t{0:0} grams citric acid", m.SuperJuice.CitricAcid)
    tw.WriteLine("\t\t{0:0} grams malic acid", m.SuperJuice.MalicAcid)
    tw.WriteLine("\t\t{0:0} grams water", m.SuperJuice.Water)
    tw.WriteLine()
    
    tw.WriteLine("\tTo measure the lime juice by volume use {0} cups lime juice and an additional {1:0.0#} cups", m.LimeJuice.Whole, m.LimeJuice.Part)    
    tw.WriteLine("\tTo measure the soda by volume use {0} liters soda and {1:0.0#} additional cups", m.Soda.Whole, m.Soda.Part)
    tw.WriteLine("\tTo measure the rum by volume use {0} bottles rum and {1:0.0#} additional cups for a total of {2:0.0#}", m.Rum.Whole, m.Rum.Part, m.Rum.Total)
