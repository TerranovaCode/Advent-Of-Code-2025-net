Imports System.Globalization

Public Class day11
    Inherits DayTemplate
    Friend Property DeviceList As New List(Of Device)
    Friend Property YouDevices As New List(Of Device)
    Friend Property OutDevice As New List(Of Device)
    Friend Property ProblemPaths As Long

    Private DeviceMap As Dictionary(Of String, Device)
    Sub New(_Input)
        MyBase.New(_Input)
    End Sub
    Public Overrides Function Part1() As String
        '        input = "aaa: you hhh
        'you: bbb ccc
        'bbb: ddd eee
        'ccc: ddd eee fff
        'ddd: ggg
        'eee: out
        'fff: out
        'ggg: out
        'hhh: ccc fff iii
        'iii: out"
        FillDeviceLists("you")
        Dim pPaths As Long
        For Each starter In YouDevices
            pPaths += PathsToEndP1(starter)
        Next
        Return pPaths
    End Function
    Friend Function PathsToEndP1(Device As Device, Optional PathString As List(Of String) = Nothing) As Long
        Dim pPaths As Long
        If PathString IsNot Nothing Then PathString.Add(Device.Name)
        For Each Output In Device.outputs
            If Output = "out" Then
                pPaths += 1
                Continue For
            End If
            Dim pCurrentDevice = GetDeviceFromList(Output)

            If pCurrentDevice IsNot Nothing Then
                pPaths += PathsToEndP1(pCurrentDevice)
            End If
        Next
        Return pPaths
    End Function
    'Friend Function PathsToEndP2(Device As Device, Optional PathString As List(Of String) = Nothing) As Long
    '    Dim pPaths As Long
    '    If PathString Is Nothing Then
    '        PathString = New List(Of String)
    '    End If
    '    PathString.Add(Device.Name)
    '    For Each Output In Device.outputs
    '        If Output = "out" Then
    '            If PathString IsNot Nothing Then
    '                If PathString.Contains("fft") AndAlso PathString.Contains("dac") Then
    '                    pPaths += 1
    '                End If
    '            End If
    '            Continue For
    '        End If
    '        Dim pCurrentDevice As Device = Nothing
    '        If DeviceMap.TryGetValue(Output, pCurrentDevice) Then 'auto assigns 
    '        End If
    '        If pCurrentDevice IsNot Nothing Then
    '            Dim NewPath As New List(Of String)(PathString)
    '            pPaths += PathsToEndP2(pCurrentDevice, NewPath)
    '        End If
    '    Next
    '    Return pPaths
    'End Function
    Friend Function PathsToEndP2(Device As Device, seenFFT As Boolean, seenDAC As Boolean) As Long
        Dim pPaths As Long
        If Device.Name = "fft" Then seenFFT = True
        If Device.Name = "dac" Then seenDAC = True
        For Each Output In Device.outputs
            If Output = "out" Then
                If seenFFT AndAlso seenDAC Then
                    pPaths += 1
                End If
                Continue For
            End If
            Dim nextDevice As Device = Nothing
            If DeviceMap.TryGetValue(Output, nextDevice) Then
                pPaths += PathsToEndP2(nextDevice, seenFFT, seenDAC)
            End If
        Next
        Return pPaths
    End Function
    Sub BuildDeviceMap()
        DeviceMap = DeviceList.ToDictionary(Function(d) d.Name)
    End Sub
    Friend Function GetDeviceFromList(Name As String) As Device
        For Each pDevice In DeviceList
            If pDevice.Name = Name Then Return pDevice
        Next
        Return Nothing
    End Function
    ' Ensures value is within [0, 99]
    Public Overrides Function Part2() As String
        '        input = "svr: aaa bbb
        'aaa: fft
        'fft: ccc
        'bbb: tty
        'tty: ccc
        'ccc: ddd eee
        'ddd: hub
        'hub: fff
        'eee: dac
        'dac: fff
        'fff: ggg hhh
        'ggg: out
        'hhh: out"
        FillDeviceLists("svr")
        BuildDeviceMap()
        Dim ppaths As Long = 0
        For Each starter In YouDevices
            ppaths += PathsToEndP2(starter, False, False)
        Next
        Return ppaths
    End Function
    Friend Sub FillDeviceLists(StartName As String)
        Dim DeviceLines = SplitLines(input)
        For Each pDeviceString In DeviceLines
            Dim Name = pDeviceString.Split(":").First
            Dim Outputs = pDeviceString.Split(":").Last
            Dim pDevice = New Device(Name)
            For Each pOutput In Outputs.Split(" ")
                If pOutput = "" Then Continue For
                pDevice.outputs.Add(pOutput)
            Next
            DeviceList.Add(pDevice)
            Select Case pDevice.GetDeviceType(StartName)
                Case DeviceTypeEnum.Starter
                    YouDevices.Add(pDevice)
                Case DeviceTypeEnum.Terminator
                    OutDevice.Add(pDevice)
            End Select
        Next
    End Sub
End Class
Class Device
    Property Name As String
    Property inputs As List(Of String)
    Property outputs As New List(Of String)
    Property DeviceType As DeviceTypeEnum
    Sub New(_Name As String)
        Name = _Name
    End Sub
    Friend Function GetDeviceType(StarterName As String)
        Dim type As DeviceTypeEnum = Nothing
        If Me.Name = StarterName Then Return DeviceTypeEnum.Starter
        For Each output In outputs
            If output = "you" Then
                If type <> Nothing Then
                    Stop
                End If
                'type = DeviceTypeEnum.Starter

            ElseIf output = "out" Then
                If type <> Nothing Then
                    Stop
                End If
                type = DeviceTypeEnum.Terminator
            End If
        Next
        If type = Nothing Then type = DeviceTypeEnum.Linker
        DeviceType = type
        Return DeviceType
    End Function
End Class
Friend Enum DeviceTypeEnum
    Linker
    Starter
    Terminator
End Enum