Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.SqlClient
Namespace OscarMencos.RestauranteAppMencosOscar.Model
    Public Class RestauranteAppMencosOscarDataContext
        Inherits DbContext
        Public Property Waiters() As DbSet(Of Waiter)
        Public Property Clients() As DbSet(Of Client)
        Public Property Orders() As DbSet(Of Order)
    End Class
End Namespace
