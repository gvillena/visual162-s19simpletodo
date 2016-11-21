Imports System.IO
Imports Newtonsoft.Json

Public Class TaskIO

#Region " Singleton Implementation "

    Private Shared _Instance As TaskIO = Nothing

    ' Singleton Implementation
    Public Shared ReadOnly Property Instance() As TaskIO
        Get
            If _Instance Is Nothing Then _Instance = New TaskIO()
            Return _Instance
        End Get
    End Property

#End Region

#Region "Constructor"
    Private Sub New()
    End Sub

#End Region

#Region " Sub & Functions "

    Public Shared Sub AddTask(titulo As String)

        ' Obteniendo lista de tareas
        Dim tasklst As List(Of Task) = TaskIO.GetTasks()

        ' Obteniendo id de nueva tarea 
        Dim id As Integer = tasklst.Last.Id + 1

        ' Declarando e inicializando nueva tarea
        Dim newtask As New Task()
        newtask.Id = id
        newtask.Titulo = titulo
        newtask.Completado = False
        newtask.Activo = True

        ' Agregando newtask a la lista de tareas
        tasklst.Add(newtask)

        ' Almacenando lista de tareas actualizada en el archivo json
        WriteTaskListToJsonFile(tasklst)

    End Sub

    Public Shared Sub RemoveTask(id As Integer)

        ' Obteniendo lista de tareas
        Dim tasklst As List(Of Task) = TaskIO.GetTasks()

        ' Obteniendo indice en lista de la tarea a eliminar
        Dim index As Integer = tasklst.FindIndex(Function(x) x.Id = id)

        ' Actualizando estado de tarea 
        tasklst.Item(index).Activo = False

        ' Almacenando lista de tareas actualizada en el archivo json
        WriteTaskListToJsonFile(tasklst)

    End Sub

    Public Shared Sub SetCompleted(id As Integer, taskCompleted As Boolean)

        ' Obteniendo lista de tareas
        Dim tasklst As List(Of Task) = TaskIO.GetTasks()

        ' Obteniendo indice en lista de la tarea a actualizar
        Dim index As Integer = tasklst.FindIndex(Function(x) x.Id = id)

        ' Actualizando estado de tarea 
        tasklst.Item(index).Completado = taskCompleted

        ' Almacenando lista de tareas actualizada en el archivo json
        WriteTaskListToJsonFile(tasklst)

    End Sub

    Public Shared Function GetTask(id As Integer) As Task

        ' Obteniendo lista de tareas
        Dim tasklst As List(Of Task) = TaskIO.GetTasks()

        ' Obteniendo tarea segun id 
        Dim task As Task = tasklst.Find(Function(x) x.Id = id)

        ' Devolviendo tarea encontrada
        Return task

    End Function

    Public Shared Function GetTasks() As List(Of Task)

        ' Declarando variable lista de tareas
        Dim tasklst As List(Of Task) = Nothing

        ' Obteniendo ruta y nombre completo de archivo json
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "task-lst-json.txt")

        ' Verificando si existe archivo de texto
        If IO.File.Exists(path) Then

            ' Declarando objeto serializador
            Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

            ' Leyendo y desearializando desde archivo json 
            Using file As New StreamReader(path)
                tasklst = serializer.Deserialize(file, GetType(List(Of Task)))
                If tasklst Is Nothing Then
                    Debug.WriteLine("Import from file " & path & " failed.")
                End If
            End Using

        Else ' En caso no existiera el archivo json

            ' Inicializando nueva lista de tareas
            tasklst = New List(Of Task)

            ' Agregando tarea vacia a lista de tareas
            tasklst.Add(New Task())

            ' Almacenando lista de tareas en archivo json
            WriteTaskListToJsonFile(tasklst)

        End If

        ' Devolviendo lista de tareas
        Return tasklst
    End Function

    Private Shared Sub WriteTaskListToJsonFile(tasklst As List(Of Task))

        ' Obteniendo ruta y nombre completo de archivo json
        Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "task-lst-json.txt")

        ' Declarando e Inicializando objeto JsonSerializer
        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        ' Serializando y escribiendo objeto lista de tareas en archivo json
        Using sw As New StreamWriter(path), writer As New JsonTextWriter(sw)
            serializer.Serialize(writer, tasklst)
        End Using

    End Sub

#End Region



End Class
