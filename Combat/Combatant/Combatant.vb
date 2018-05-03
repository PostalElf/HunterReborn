Public MustInherit Class Combatant
#Region "Personal Identifiers"
    Protected _Name As String
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property
    Public Overrides Function ToString() As String
        Return Name
    End Function
#End Region

#Region "Events"
    Public Event IsDestroyed(ByVal combatant As Combatant)
    Public Event IsMoved(ByVal combatant As Combatant, ByVal currentPosition As ePosition, ByVal targetPosition As ePosition)
#End Region

#Region "Battlefield"
    Private _Battlefield As Battlefield
    Public Property Battlefield As Battlefield
        Get
            Return _Battlefield
        End Get
        Set(ByVal value As Battlefield)
            _Battlefield = value
            If value Is Nothing = False Then BattlefieldSetup()
        End Set
    End Property
    Private _BattlefieldPosition As ePosition
    Public ReadOnly Property BattlefieldPosition
        Get
            Return _BattlefieldPosition
        End Get
    End Property
    Private Sub BattlefieldSetup()
        Const MaxPosition As Integer = 4
        _BattlefieldPosition = Rng.Next(0, MaxPosition)

        'randomise initiative
        BattlefieldInitiativeReset()
        BattlefieldInitiative += Rng.Next(1, 11)
    End Sub
    Private BattlefieldInitiative As Integer
    Private Sub BattlefieldInitiativeReset()
        BattlefieldInitiative = Battlefield.GetHighestSpeed - Speed + 10
    End Sub

    Public MustOverride Function Tick() As Boolean
    Protected Function TickBase() As Boolean
        BattlefieldInitiative -= 1
        If BattlefieldInitiative <= 0 Then
            'reset initiative
            BattlefieldInitiativeReset()

            'tick bodyparts
            For n = Bodyparts.Count - 1 To 0 Step -1
                Dim bp As Bodypart = Bodyparts(n)
                bp.Tick()
            Next

            'return true to indicate that turn can be performed
            Return True
        End If

        Return False
    End Function
#End Region

#Region "Bodyparts"
    Protected BaseBodypart As Bodypart
    Protected Bodyparts As New List(Of Bodypart)

    Public ReadOnly Property Speed As Integer
        Get
            Dim total As Integer = BaseBodypart.BonusSpeed
            For Each bp In Bodyparts
                total += bp.BonusSpeed
            Next
            Return total
        End Get
    End Property
#End Region
End Class
