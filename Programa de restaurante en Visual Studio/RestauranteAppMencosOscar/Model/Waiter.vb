Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Namespace OscarMencos.RestauranteAppMencosOscar.Model
    Public Class Waiter


#Region "Campos"
        Private _WaiterID As Integer
        Private _LastName As String
        Private _FirstName As String
        Private _Telephone As Integer
        Private _Address As String


#End Region
#Region "Propiedades"
        Public Overridable Property Orders() As ICollection(Of Order)
        <Key>
        Public Property WaiterID As Integer
            Get
                Return _WaiterID
            End Get
            Set(value As Integer)
                _WaiterID = value
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

        Public Property FirstName As String
            Get
                Return _FirstName
            End Get
            Set(value As String)
                _FirstName = value
            End Set
        End Property

        Public Property Telephone As Integer
            Get
                Return _Telephone
            End Get
            Set(value As Integer)
                _Telephone = value
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
#End Region


    End Class
End Namespace
