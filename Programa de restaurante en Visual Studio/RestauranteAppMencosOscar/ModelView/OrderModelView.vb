Imports System.ComponentModel
Imports RestauranteAppMencosOscar
Imports RestauranteAppMencosOscar.OscarMencos.RestauranteAppMencosOscar.Model
Public Class OrderModelView
    Implements ICommand, INotifyPropertyChanged, IDataErrorInfo




#Region "Campos"

    Private _Client As Client
    Private _OrderType As String
    Private _OrderDate As String
    Private _Waiter As Waiter
    Private _Model As OrderModelView
    Private _ListOrders As New List(Of Order)
    Private _ListClients As New List(Of Client)
    Private _ListWaiters As New List(Of Waiter)
    Private _Element As Order
    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private DB As New RestauranteAppMencosOscarDataContext
#End Region
#Region "Propiedades"
    Public Property Client As Client
        Get
            Return _Client
        End Get
        Set(value As Client)
            _Client = value
            NotificarCambio("Client")
        End Set
    End Property

    Public Property OrderType As String
        Get
            Return _OrderType
        End Get
        Set(value As String)
            _OrderType = value
            NotificarCambio("OrderType")
        End Set
    End Property

    Public Property OrderDate As String
        Get
            Return _OrderDate
        End Get
        Set(value As String)
            _OrderDate = value
            NotificarCambio("OrderDate")
        End Set
    End Property

    Public Property Waiter As Waiter
        Get
            Return _Waiter
        End Get
        Set(value As Waiter)
            _Waiter = value
            NotificarCambio("Waiter")
        End Set
    End Property

    Public Property Model As OrderModelView
        Get
            Return _Model
        End Get
        Set(value As OrderModelView)
            _Model = value
        End Set
    End Property

    Public Property ListOrders As List(Of Order)

        Get
            If (_ListOrders.Count = 0) Then
                _ListOrders = (From O In DB.Orders Select O).ToList
            End If

            Return _ListOrders
        End Get
        Set(value As List(Of Order))
            _ListOrders = value
            NotificarCambio("ListOrders")
        End Set
    End Property


    Public Property ListClients As List(Of Client)
        Get
            If (_ListClients.Count = 0) Then
                _ListClients = (From C In DB.Clients Select C).ToList
            End If
            Return _ListClients
        End Get
        Set(value As List(Of Client))
            _ListClients = value
            NotificarCambio("ListClients")
        End Set
    End Property

    Public Property ListWaiters As List(Of Waiter)
        Get
            If (_ListWaiters.Count = 0) Then
                _ListWaiters = (From w In DB.Waiters Select w).ToList
            End If
            Return _ListWaiters
        End Get
        Set(value As List(Of Waiter))
            _ListWaiters = value
            NotificarCambio("ListWaiters")
        End Set
    End Property

    Public Property Element As Order
        Get
            Return _Element
        End Get
        Set(value As Order)
            _Element = value
            NotificarCambio("Element")
            If (value IsNot Nothing) Then
                Me.Client = _Element.Client
                Me.OrderType = _Element.OrderType
                Me.OrderDate = _Element.OrderDate
                Me.Waiter = _Element.Waiter
            End If
        End Set
    End Property

    Public Property BtnNew As Boolean
        Get
            Return _BtnNew
        End Get
        Set(value As Boolean)
            _BtnNew = value
            NotificarCambio("BtnNew")
        End Set
    End Property

    Public Property BtnSave As Boolean
        Get
            Return _BtnSave
        End Get
        Set(value As Boolean)
            _BtnSave = value
            NotificarCambio("BtnSave")
        End Set
    End Property

    Public Property BtnDelete As Boolean
        Get
            Return _BtnDelete
        End Get
        Set(value As Boolean)
            _BtnDelete = value
            NotificarCambio("BtnDelete")
        End Set
    End Property

    Public Property BtnUpdate As Boolean
        Get
            Return _BtnUpdate
        End Get
        Set(value As Boolean)
            _BtnUpdate = value
            NotificarCambio("BtnUpdate")
        End Set
    End Property

    Public Property BtnCancel As Boolean
        Get
            Return _BtnCancel
        End Get
        Set(value As Boolean)
            _BtnCancel = value
            NotificarCambio("BtnCancel")
        End Set
    End Property

#End Region
#Region "Constructor"
    Public Sub New()
        Me.Model = Me
    End Sub
#End Region
#Region "ICommand"
    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged


    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        If parameter.Equals("new") Then
            BtnNew = False
            BtnSave = True
            BtnDelete = False
            BtnUpdate = False
            BtnCancel = True
        ElseIf parameter.Equals("save") Then
            BtnNew = True
            BtnSave = False
            BtnDelete = True
            BtnUpdate = True
            BtnCancel = False
            Dim Registro As New Order
            Registro.Client = Me.Client
            Registro.OrderType = Me.OrderType
            Registro.OrderDate = Me.OrderDate
            Registro.Waiter = Me.Waiter
            If Me.Client Is Nothing Or Me.OrderType = "" Or Me.OrderDate = "" Or Me.Waiter Is Nothing Then
                MsgBox("No se pueden dejar campos vacios", MsgBoxStyle.Exclamation)
            Else

                DB.Orders.Add(Registro)
                    DB.SaveChanges()
                    MsgBox("Registro ingresado")
                    Limpiar()
                    Me.ListOrders = (From O In DB.Orders Select O).ToList

            End If
        ElseIf parameter.Equals("delete") Then
            If Element IsNot Nothing Then
                Dim Respuesta As MsgBoxResult = MsgBoxResult.No
                Respuesta = MsgBox("¿Quiere eliminar el registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Eliminar")
                If (Respuesta = MsgBoxResult.Yes) Then
                    DB.Orders.Remove(Me.Element)
                    DB.SaveChanges()
                    Limpiar()
                    Me.ListOrders = (From O In DB.Orders Select O).ToList
                Else
                    Limpiar()
                End If
            Else
                MsgBox("Debe seleccionar un registro")
            End If
        ElseIf parameter.Equals("update") Then
            If Element IsNot Nothing Then
                Dim Respuesta As MsgBoxResult = MsgBoxResult.No
                Respuesta = MsgBox("¿Quiere actualizar el registro?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Actualizar")
                If (Respuesta = MsgBoxResult.Yes) Then
                    Me.Element.OrderType = Me.OrderType
                    Me.Element.OrderDate = Me.OrderDate
                    DB.Entry(Me.Element).State = System.Data.Entity.EntityState.Modified
                    DB.SaveChanges()
                    Limpiar()
                    Me.ListOrders = (From O In DB.Orders Select O).ToList
                Else
                    Limpiar()
                End If
            Else
                MsgBox("Debe seleccionar un registro")
            End If
        ElseIf parameter.Equals("cancel") Then
            BtnNew = True
            BtnSave = False
            BtnDelete = True
            BtnUpdate = True
            BtnCancel = False
            Limpiar()

        End If
    End Sub

    Public Sub Limpiar()
        Me.Client = Nothing
        Me.OrderType = ""
        Me.OrderDate = ""
        Me.Waiter = Nothing
        Me.Element = Nothing

    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function
#End Region
#Region "INotifyPropertyChanged"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Sub NotificarCambio(ByVal Propiedad As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Propiedad))
    End Sub
#End Region
#Region "IDataErrorInfo"
    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Throw New NotImplementedException()
        End Get
    End Property




#End Region
End Class
