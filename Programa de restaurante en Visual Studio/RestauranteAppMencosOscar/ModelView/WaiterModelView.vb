Imports System.ComponentModel
Imports RestauranteAppMencosOscar.OscarMencos.RestauranteAppMencosOscar.Model
Public Class WaiterModelView
        Implements ICommand, INotifyPropertyChanged, IDataErrorInfo




#Region "Campos"
        Private _LastName As String
    Private _FirstName As String
    Private _Telephone As Integer
    Private _Address As String
    Private _Model As WaiterModelView
    Private _ListWaiters As New List(Of Waiter)
    Private _ListOrders As New List(Of Order)
    Private _Element As Waiter
    Private _BtnNew As Boolean = True
    Private _BtnSave As Boolean = False
    Private _BtnDelete As Boolean = True
    Private _BtnUpdate As Boolean = True
    Private _BtnCancel As Boolean = False
    Private DB As New RestauranteAppMencosOscarDataContext
#End Region
#Region "Propiedades"
    Public Property LastName As String
        Get
            Return _LastName
        End Get
        Set(value As String)
            _LastName = value
            NotificarCambio("LastName")
        End Set
    End Property

    Public Property FirstName As String
        Get
            Return _FirstName
        End Get
        Set(value As String)
            _FirstName = value
            NotificarCambio("FirstName")
        End Set
    End Property

    Public Property Telephone As Integer
        Get
            Return _Telephone
        End Get
        Set(value As Integer)
            _Telephone = value
            NotificarCambio("Telephone")
        End Set
    End Property

    Public Property Address As String
        Get
            Return _Address
        End Get
        Set(value As String)
            _Address = value
            NotificarCambio("Address")
        End Set
    End Property

    Public Property Model As WaiterModelView
        Get
            Return _Model
        End Get
        Set(value As WaiterModelView)
            _Model = value
        End Set
    End Property

    Public Property ListWaiters As List(Of Waiter)
        Get
            If (_ListWaiters.Count = 0) Then
                _ListWaiters = (From W In DB.Waiters Select W).ToList
            End If
            Return _ListWaiters
        End Get
        Set(value As List(Of Waiter))
            _ListWaiters = value
            NotificarCambio("ListWaiters")
        End Set
    End Property

    Public Property ListOrders As List(Of Order)
        Get
            If (_ListOrders.Count = 0) Then
                _ListOrders = (From O In DB.Orders Select O Where _Element.WaiterID = O.WaiterID).ToList
            End If

            Return _ListOrders
        End Get
        Set(value As List(Of Order))
            _ListOrders = value
        End Set
    End Property

    Public Property Element As Waiter
        Get
            Return _Element
        End Get
        Set(value As Waiter)
            _Element = value
            NotificarCambio("Element")
            If (value IsNot Nothing) Then
                Me.LastName = _Element.LastName
                Me.FirstName = _Element.FirstName
                Me.Telephone = _Element.Telephone
                Me.Address = _Element.Address
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
            Dim Registro As New Waiter
            Registro.LastName = Me.LastName
            Registro.FirstName = Me.FirstName
            Registro.Telephone = Me.Telephone
            Registro.Address = Me.Address
            If Me.LastName = "" Or Me.FirstName = "" Or Me.Telephone = 0 Or Me.Address = "" Then
                MsgBox("No se pueden dejar campos vacios", MsgBoxStyle.Exclamation)
            Else
                DB.Waiters.Add(Registro)
                DB.SaveChanges()
                MsgBox("Registro ingresado")
                Limpiar()
                Me.ListWaiters = (From W In DB.Waiters Select W).ToList
            End If
        ElseIf parameter.Equals("delete") Then

            If Element IsNot Nothing Then
                If Me.ListOrders.Count > 0 Then
                    MsgBox("Este registro no se puede eliminar, porque esta en otra tabla", MsgBoxStyle.Exclamation)
                    ListOrders.Clear()
                    Limpiar()
                Else
                    Dim Respuesta As MsgBoxResult = MsgBoxResult.No
                    Respuesta = MsgBox("¿Quiere eliminar el registro?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Eliminar")
                    If (Respuesta = MsgBoxResult.Yes) Then
                        DB.Waiters.Remove(Me.Element)
                        DB.SaveChanges()
                        Limpiar()
                        Me.ListWaiters = (From W In DB.Waiters Select W).ToList
                    Else
                        Limpiar()
                    End If
                End If
            Else
                    MsgBox("Debe seleccionar un registro")
                End If

            ElseIf parameter.Equals("update") Then
            If Element IsNot Nothing Then
                Dim Respuesta As MsgBoxResult = MsgBoxResult.No
                Respuesta = MsgBox("¿Quiere actualizar el registro?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Actualizar")
                If (Respuesta = MsgBoxResult.Yes) Then
                    Me.Element.LastName = Me.LastName
                    Me.Element.FirstName = Me.FirstName
                    Me.Element.Telephone = Me.Telephone
                    Me.Element.Address = Me.Address
                    DB.Entry(Me.Element).State = System.Data.Entity.EntityState.Modified
                    DB.SaveChanges()
                    Limpiar()
                    Me.ListWaiters = (From W In DB.Waiters Select W).ToList
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
        Me.LastName = ""
        Me.FirstName = ""
        Me.Telephone = 0
        Me.Address = ""
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
