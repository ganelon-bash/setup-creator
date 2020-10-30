module App.Effect
open App.ModelLibrary

[<Struct; StructuralEquality; StructuralComparison>]
type defenseModifier =
    | RepulseStrengthCarriesOver
    | RepulseStrengthAppliesToAllEqually of Count: integer
    | RepulseAppliesToOne
[<Struct; StructuralEquality; StructuralComparison>]
type rawEffect =
    | Attack of AttackStrength: integer
    | Defend of RepulseStrength: integer * RepulseModifier: defenseModifier
    | Heal of HealingStrength: integer
    | Roleblock
    | CheckRole
    | CheckRoleType of RMapping: Map<string, string>
    | CheckFaction
    | CheckAlignment of FMapping: Map<string, string>
   

module DefenseModifier =
    let description = function
        | RepulseStrengthCarriesOver -> "The effect is applied to as many attackers as possible, until the defense strength has been exhausted."
        | RepulseAppliesToOne -> "The effect is only applied to the first attacker."
        | RepulseStrengthAppliesToAllEqually Infinite -> "The effect is applied to all attackers, equally."
        | RepulseStrengthAppliesToAllEqually (Int v) -> sprintf "The effect is applied to at most %d attackers, equally." v