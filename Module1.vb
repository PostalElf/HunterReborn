Module Module1

    Sub Main()
        Dim battlefield As New Battlefield
        battlefield.Add(CombatantAI.Construct("Goblin"))
        battlefield.Main()
    End Sub

End Module
