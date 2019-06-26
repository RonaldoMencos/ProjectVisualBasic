Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.ComponentModel
Namespace OscarMencos.RestauranteAppMencosOscar.Model
    Public Class Order
#Region "Campos"
        Private _OrderID As Integer
        Private _ClientID As Integer
        Private _OrderType As String
        Private _OrderDate As String
        Private _WaiterID As Integer


#End Region
#Region "Propiedades"
        Public Overridable Property Client() As Client
        Public Overridable Property Waiter() As Waiter



        <Key>
        Public Property OrderID As Integer
            Get
                Return _OrderID
            End Get
            Set(value As Integer)
                _OrderID = value
            End Set
        End Property
        Public Property ClientID As Integer
            Get
                Return _ClientID
            End Get
            Set(value As Integer)
                _ClientID = value
            End Set
        End Property

        Public Property OrderType As String
            Get
                Return _OrderType
            End Get
            Set(value As String)
                _OrderType = value
            End Set
        End Property

        Public Property OrderDate As String
            Get
                Return _OrderDate
            End Get
            Set(value As String)
                _OrderDate = value
            End Set
        End Property
        Public Property WaiterID As Integer
            Get
                Return _WaiterID
            End Get
            Set(value As Integer)
                _WaiterID = value
            End Set
        End Property



#End Region
    End Class
End Namespace
