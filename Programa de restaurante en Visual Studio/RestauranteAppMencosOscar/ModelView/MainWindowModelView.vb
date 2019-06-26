Imports System.ComponentModel

Public Class MainWindowModelView
    Implements ICommand, INotifyPropertyChanged


#Region "Campos"
    Private _Model As MainWindowModelView
#End Region
#Region "Propiedades"
    Public Property Model As MainWindowModelView
        Get
            Return _Model
        End Get
        Set(value As MainWindowModelView)
            _Model = value
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
        If parameter.Equals("waiter") Then
            Dim WindowWaiters As New WaiterView
            WindowWaiters.ShowDialog()
        ElseIf parameter.Equals("clients") Then
            Dim WindowClients As New ClientView
            WindowClients.ShowDialog()
        ElseIf parameter.Equals("order") Then
            Dim WindowOrder As New OrderView
            WindowOrder.ShowDialog()
        End If
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
End Class





