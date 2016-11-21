﻿Public Class Task

    ' Fields
    Private _Id As Integer
    Private _Titulo As String
    Private _Completado As Boolean
    Private _Activo As Boolean

    ' Constructors
    Public Sub New()
        _Id = 0
        _Titulo = "Tarea Vacia"
        _Completado = False
        _Activo = False
    End Sub

    Public Sub New(id As Integer, titulo As String, completado As Boolean)
        _Id = id
        _Titulo = titulo
        _Completado = completado
        _Activo = True
    End Sub

    ' Properties
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Public Property Titulo() As String
        Get
            Return _Titulo
        End Get
        Set(ByVal value As String)
            _Titulo = value
        End Set
    End Property

    Public Property Completado() As Boolean
        Get
            Return _Completado
        End Get
        Set(ByVal value As Boolean)
            _Completado = value
        End Set
    End Property

    Public Property Activo() As Boolean
        Get
            Return _Activo
        End Get
        Set(ByVal value As Boolean)
            _Activo = value
        End Set
    End Property

End Class
