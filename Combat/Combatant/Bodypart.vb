Public Class Bodypart
#Region "Constructors"
    Public Shared Function Construct(ByVal rawdata As List(Of String), Optional ByVal attack As Attack = Nothing) As Bodypart
        Dim bp As New Bodypart
        For Each l As String In rawdata
            Dim ln As String() = l.Split(":")
            Dim header As String = ln(0).Trim
            Dim entry As String = ln(1).Trim
            bp.Construct(header, entry)
        Next
        If attack Is Nothing = False Then bp.Attack = attack
        Return bp
    End Function
    Private Sub Construct(ByVal header As String, ByVal entry As String)
        Select Case header
            Case "Name" : _Name = entry
            Case "Weight" : _BonusWeight = CInt(entry)
            Case "Carry" : _BonusCarry = CInt(entry)
            Case "Speed" : _BonusSpeed = CInt(entry)
            Case "Dodge" : _BonusDodge = CInt(entry)
            Case "ShockCapacity" : _BonusShockCapacity = CInt(entry)

            Case "Agility" : Agility = CInt(entry)
            Case "Armour" : Armour = CInt(entry)
            Case "Health" : Health = CInt(entry)
            Case "ShockAbsorb" : ShockAbsorb = CDbl(entry)
            Case "ShockLoss" : _ShockLoss = CInt(entry)
            Case "Attack" : Attack = Attack.construct(entry)
        End Select
    End Sub
#End Region

#Region "Personal Identifiers"
    Private _Name As String
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
    Public Event PerformsMove(ByVal mover As Combatant)
    Public Event PerformsAttack(ByVal attacker As Combatant, ByVal attack As Attack, ByVal target As Combatant, ByVal targetBp As Bodypart)
    Public Event IsAttacked(ByVal attacker As Combatant, ByVal attack As Attack, ByVal target As Combatant, ByVal targetBp As Bodypart)
    Public Event IsDestroyed(ByVal attacker As Combatant, ByVal attack As Attack, ByVal target As Combatant)
#End Region

#Region "Combatant Bonuses"
    Private _BonusWeight As Integer
    Public ReadOnly Property BonusWeight As Integer
        Get
            Return _BonusWeight
        End Get
    End Property
    Private _BonusCarry As Integer
    Public ReadOnly Property BonusCarry As Integer
        Get
            Return _BonusCarry
        End Get
    End Property
    Private _BonusSpeed As Integer
    Public ReadOnly Property BonusSpeed As Integer
        Get
            Return _BonusSpeed
        End Get
    End Property
    Private _BonusDodge As Integer
    Public ReadOnly Property BonusDodge As Integer
        Get
            Return _BonusDodge
        End Get
    End Property
    Private _BonusShockCapacity As Integer
    Public ReadOnly Property BonusShockCapacity As Integer
        Get
            Return _BonusShockCapacity
        End Get
    End Property
#End Region

#Region "BP Specific Properties"
    Private Agility As Integer
    Private Armour As Integer
    Private Health As Integer
    Private ShockAbsorb As Double
    Private _ShockLoss As Integer
    Public ReadOnly Property ShockLoss As Integer
        Get
            Return _ShockLoss
        End Get
    End Property

    Private _Attack As Attack
    Private Property Attack As Attack
        Get
            Return _Attack
        End Get
        Set(ByVal value As Attack)
            _Attack = value
            _Attack.Bodypart = Me
        End Set
    End Property
    Private AttackCooldown As Integer
    Public ReadOnly Property AttackReady As Boolean
        Get
            If Attack Is Nothing Then Return False
            If AttackCooldown > 0 Then Return False

            Return True
        End Get
    End Property

    Public Sub Tick()
        If AttackCooldown > 0 Then AttackCooldown -= 1
    End Sub
#End Region
End Class
