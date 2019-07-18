Imports System.Drawing
Imports RandomizedAry

Module Module1
    Dim Width As Integer
    Dim Height As Integer
    Dim GGroup As Integer
    Dim s As String = vbCrLf
    Dim tempArray() As Char = s.ToCharArray
    Dim NewLineChar1 As Char = tempArray(0)
    Dim NewLineChar2 As Char = tempArray(1)

    Sub Main()

        Console.Write("Width: ")
        Width = Console.ReadLine()
        Console.Write("Height: ")
        Height = Console.ReadLine()
        Console.Write("Types: ")
        GGroup = Console.ReadLine()
        Console.Write("file location: ")
        Dim fileInLoc As String = Console.ReadLine()
        fileInLoc = "C:\test\debug\" & fileInLoc
        ''
        Dim nonRndS = $"{fileInLoc}.txt"
        Dim RndS = $"{fileInLoc}(rnd).txt"
        Dim data = New DataAry(Width, Height, nonRndS)
        data.Randomize()
        data.WriteAry()
        Dim dataRnd = New TestDataAry(Width, Height, RndS, GGroup)
        dataRnd.RandomizeTestAry()
        dataRnd.WriteTestAry()
        ''
        Console.Write("Save Image to: ")
        Dim ImageOutLoc As String = Console.ReadLine()
        ImageOutLoc = "C:\test\debug\" & ImageOutLoc
        Dim nonRndO = ImageOutLoc & ".jpg"
        Dim RndO = ImageOutLoc & "(rnd).jpg"
        'ReadFile(nonRndS, nonRndO)
        ReadFile(RndS, RndO)
        'ReadFile("C:\test\outTest.txt", "C:\test\outImage.jpg")
        'ReadFile("C:\test\outTestRandomized.txt", "C:\test\outImageRandomized.jpg")
        Console.ReadKey()
    End Sub

    Public Sub RenderImage(ByRef charArray() As Char, ByVal fileLoc As String)
        Dim binList() As Integer = New Integer(Width * Height) {}
        Dim binCounter As Integer = 0
        Dim imageTest = New Bitmap(Width, Height)
        GetTypes(charArray, binList)
        For x = 0 To imageTest.Width - 1
            For y = 0 To imageTest.Height - 1
                Dim nColor As PixelRGB = ColorPicker(binList(binCounter))

                Dim newColor As Color = Color.FromArgb(nColor.R, nColor.G, nColor.B)
                imageTest.SetPixel(x, y, newColor)
                binCounter = binCounter + 1
            Next
        Next
        imageTest.Save(fileLoc)
        Console.WriteLine($"binList.Count = {binList.Count}")
        Console.WriteLine($"binCounter = {binCounter}")
    End Sub

    Public Sub ReadFile(ByVal fileLoc As String, ByVal toFile As String)
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(fileLoc)
        Dim charArray() As Char = fileReader.ToCharArray()
        RenderImage(charArray, toFile)
        Console.WriteLine($"charArry.Count = {charArray.Count}")

        Console.WriteLine("finish rendering...")
    End Sub

    Public Sub GetTypes(ByRef charArray() As Char, ByRef binList() As Integer)
        Dim binCounter As Integer = 0
        Dim nonNewLineCounter As Integer = 0
        For i As Integer = 0 To charArray.Count - 1

            If Not (i = charArray.Count - 1) Then
                If charArray(i) = NewLineChar1 Then
                    Dim int As Integer
                    If charArray(i - 2) = "," Then
                        int = charArray(i - 1).ToString()
                    Else
                        int = charArray(i - 2).ToString() & charArray(i - 1).ToString()
                    End If
                    binList(binCounter) = int
                    binCounter += 1
                ElseIf charArray(i) = NewLineChar2 Then

                Else
                    'Console.WriteLine(charArray(i))
                    nonNewLineCounter += 1
                End If
            End If
            If i > 0 Then
                'Console.WriteLine($"charArray({i}) = {charArray(i)}? -> binList({binCounter}) = {charArray(i - 1)}")
            End If
        Next
        Console.WriteLine($"binCounter2 = {binCounter}")
        Console.WriteLine($"NonNewLineCOunter = {nonNewLineCounter}")
    End Sub

    Public Function ColorPicker(ByVal group As Integer) As PixelRGB
        'Console.WriteLine($"group: {group}")
        Dim pool As Integer = 1020 / GGroup

        Dim assignPool As Integer = pool * group
        Dim R As Integer
        Dim G As Integer
        Dim B As Integer
        If assignPool <= 255 And assignPool > 0 Then
            R = 255
            G = assignPool
            B = 0
            'Console.Write("R")
        ElseIf assignPool > 255 And assignPool <= 510 Then
            R = 255
            G = 255
            B = assignPool - 255
            'Console.Write("Y")
        ElseIf assignPool > 510 And assignPool <= 765 Then
            R = 255 - (assignPool - 510)
            G = 255
            B = 255
            'Console.Write("B")
        ElseIf assignPool > 765 And assignPool <= 1020 Then
            R = assignPool - 765
            G = 0
            B = 255
            'Console.Write("P")
        Else
            R = 0
            G = 0
            B = 0
            'Console.WriteLine(group)
            'Console.WriteLine("else case")
        End If

        Dim color As New PixelRGB(R, G, B)
        Return color

    End Function

    Public Class PixelRGB
        Public R
        Public G
        Public B
        Public Sub New(ByVal _r As Integer, ByVal _g As Integer, ByVal _b As Integer)
            R = _r
            G = _g
            B = _b
        End Sub
    End Class

End Module