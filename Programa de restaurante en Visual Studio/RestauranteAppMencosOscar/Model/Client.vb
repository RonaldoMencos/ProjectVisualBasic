Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel
Namespace OscarMencos.RestauranteAppMencosOscar.Model
    Public Class Client
#Region "Campos"
        Private _ClientID As Integer
        Private _FirstName As String
        Private _LastName As String
        Private _NIT As Long
        Private _Address As String
        Private _Observations As String


#End Region
#Region "Propiedades"
        Public Overridable Property Orders() As ICollection(Of Order)
        <Key>
        Public Property ClientID As Integer
            Get
                Return _ClientID
            End Get
            Set(value As Integer)
                _ClientID = value
            End Set
        End Property

        Public Property FirstName As String
            Get
                Return _FirstName
            End Get
            Set(value As String)
                _FirstName = value
            End Set
        End Property

        Public Property LastName As String
            Get
                Return _LastName
            End Get
            Set(value As String)
                _LastName = value
            End Set
        End Property

        Public Property NIT As Long
            Get
                Return _NIT
            End Get
            Set(value As Long)
                _NIT = value
            End Set
        End Property

        Public Property Address As String
            Get
                Return _Address
            End Get
            Set(value As String)
                _Address = value
            End Set
        End Property

        Public Property Observations As String
            Get
                Return _Observations
            End Get
            Set(value As String)
                _Observations = value
            End Set
        End Property
#End Region
    End Class
End Namespace
