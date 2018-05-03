Public Class CombatantPlayer
    Inherits Combatant

    Public Overrides Function Tick() As Boolean
        Dim canAct As Boolean = MyBase.TickBase()
    End Function
End Class
