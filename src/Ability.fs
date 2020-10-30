module App.Ability

open App.Effect
open App.ModelLibrary

[<Struct; StructuralEquality; StructuralComparison>]
type feedback =
    | TargetedFeedback of SelfFeedback: string voption * TargetFeedback: string voption
    | UntargetedFeedback of MyFeedback: string voption
    | SelfFeedback of SFeedback: string voption

[<Struct; StructuralEquality; StructuralComparison>]
type effect =
    | Effect of Effects: effect list * Feedback: feedback
[<Struct; StructuralEquality; StructuralComparison>]
type ability = {
    name: string
    description: string
    effect: effect
    charges: integer
}

