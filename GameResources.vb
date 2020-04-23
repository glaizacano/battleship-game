Imports SwinGameSDK
Imports System.Collections.Generic

''' <summary>
''' The GameResources is responsible for managing game resources 
''' such as fonts, images, sounds and music, loading and the unloading 
''' of these resources
''' </summary>
Public Module GameResources

    ''' <summary>
    ''' Load Font Resources
    ''' </summary>

    Private Sub LoadFonts()
        NewFont("ArialLarge", "arial.ttf", 80)
        NewFont("Courier", "cour.ttf", 14)
        NewFont("CourierSmall", "cour.ttf", 8)
        NewFont("Menu", "ffaccess.ttf", 8)
    End Sub

    ''' <summary>
    ''' Load Image Resources
    ''' </summary>

    Private Sub LoadImages()
        'Backgrounds
        NewImage("Menu", "main_page.jpg")
        NewImage("Discovery", "discover.jpg")
        NewImage("Deploy", "deploy.jpg")

        'Deployment
        NewImage("LeftRightButton", "deploy_dir_button_horiz.png")
        NewImage("UpDownButton", "deploy_dir_button_vert.png")
        NewImage("SelectedShip", "deploy_button_hl.png")
        NewImage("PlayButton", "deploy_play_button.png")
        NewImage("RandomButton", "deploy_randomize_button.png")

        'Ships
        Dim i as Integer
For i  = 1 To 5
            NewImage("ShipLR" & i, "ship_deploy_horiz_" & i & ".png")
            NewImage("ShipUD" & i, "ship_deploy_vert_" & i & ".png")
        Next

        'Explosions
        NewImage("Explosion", "explosion.png")
        NewImage("Splash", "splash.png")

    End Sub

    ''' <summary>
    ''' Load Sound Resources
    ''' </summary>

    Private Sub LoadSounds()
        NewSound("Error", "error.wav")
        NewSound("Hit", "hit.wav")
        NewSound("Sink", "sink.wav")
        NewSound("Siren", "siren.wav")
        NewSound("Miss", "watershot.wav")
        NewSound("Winner", "winner.wav")
        NewSound("Lose", "lose.wav")
    End Sub

    ''' <summary>
    ''' Loads Background Music
    ''' </summary>

    Private Sub LoadMusic()
        NewMusic("Background", "horrordrone.mp3")
    End Sub

    ''' <summary>
    ''' Gets a Font Loaded in the Resources
    ''' </summary>
    ''' <param name="font">Name of Font</param>
    ''' <returns>The Font Loaded with this Name</returns>

    Public Function GameFont(ByVal font As String) As Font
        Return _Fonts(font)
    End Function

    ''' <summary>
    ''' Gets an Image loaded in the Resources
    ''' </summary>
    ''' <param name="image">Name of image</param>
    ''' <returns>The image loaded with this name</returns>

    Public Function GameImage(ByVal image As String) As Bitmap
        Return _Images(image)
    End Function

    ''' <summary>
    ''' Gets an sound loaded in the Resources
    ''' </summary>
    ''' <param name="sound">Name of sound</param>
    ''' <returns>The sound with this name</returns>

    Public Function GameSound(ByVal sound As String) As SoundEffect
        Return _Sounds(sound)
    End Function

    ''' <summary>
    ''' Gets the music loaded in the Resources
    ''' </summary>
    ''' <param name="music">Name of music</param>
    ''' <returns>The music with this name</returns>

    Public Function GameMusic(ByVal music As String) As Music
        Return _Music(music)
    End Function

    Private _Images As New Dictionary(Of String, Bitmap)
    Private _Fonts As New Dictionary(Of String, Font)
    Private _Sounds As New Dictionary(Of String, SoundEffect)
    Private _Music As New Dictionary(Of String, Music)

    Private _Background As Bitmap
    Private _Animation As Bitmap
    Private _LoaderFull As Bitmap
    Private _LoaderEmpty As Bitmap
    Private _LoadingFont As Font
    Private _StartSound As SoundEffect

    ''' <summary>
    ''' The Resources Class stores all of the Games Media Resources, such as Images, Fonts
    ''' Sounds, Music.
    ''' </summary>

    Public Sub LoadResources()
        Dim width, height As Integer

        width = SwinGame.ScreenWidth()
        height = SwinGame.ScreenHeight()

        SwinGame.ChangeScreenSize(800, 600)

        ShowLoadingScreen()

        ShowMessage("Loading fonts...", 0)
        LoadFonts()
        SwinGame.Delay(100)

        ShowMessage("Loading images...", 1)
        LoadImages()
        SwinGame.Delay(100)

        ShowMessage("Loading sounds...", 2)
        LoadSounds()
        SwinGame.Delay(100)

        ShowMessage("Loading music...", 3)
        LoadMusic()
        SwinGame.Delay(100)

        SwinGame.Delay(100)
        ShowMessage("Game loaded...", 5)
        SwinGame.Delay(100)
        EndLoadingScreen(width, height)
    End Sub

    ''' <summary>
    ''' Gets the loading screen images loaded in Resources
    ''' </summary>

    Private Sub ShowLoadingScreen()
        _Background = SwinGame.LoadBitmap(SwinGame.PathToResource("SplashBack.png", ResourceKind.BitmapResource))
        SwinGame.DrawBitmap(_Background, 0, 0)
        SwinGame.RefreshScreen()
        SwinGame.ProcessEvents()

        _Animation = SwinGame.LoadBitmap(SwinGame.PathToResource("SwinGameAni.jpg", ResourceKind.BitmapResource))
        _LoadingFont = SwinGame.LoadFont(SwinGame.PathToResource("arial.ttf", ResourceKind.FontResource), 12)
        _StartSound = Audio.LoadSoundEffect(SwinGame.PathToResource("SwinGameStart.ogg", ResourceKind.SoundResource))

		_LoaderFull = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_full.png", ResourceKind.BitmapResource))
		_LoaderEmpty = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_empty.png", ResourceKind.BitmapResource))

        PlaySwinGameIntro()
    End Sub

    ''' <summary>
    ''' Plays the SwinGame introductory animation
    ''' </summary>

    Private Sub PlaySwinGameIntro()
        Const ANI_CELL_COUNT As Integer = 11

        Audio.PlaySoundEffect(_StartSound)
        SwinGame.Delay(200)

        Dim i As Integer
        For i = 0 To ANI_CELL_COUNT - 1
	        SwinGame.DrawBitmap(_Background, 0, 0)
	        SwinGame.Delay(20)
            SwinGame.RefreshScreen()
            SwinGame.ProcessEvents()
        Next i

        SwinGame.Delay(1500)

    End Sub

    ''' <summary>
    ''' Display strings and mesasge boxes on the screen
    ''' </summary>

    Private Sub ShowMessage(ByVal message As String, ByVal number As Integer)
        Const TX As Integer = 310, TY As Integer = 493, TW As Integer = 200, TH As Integer = 25, STEPS As Integer = 5, BG_X As Integer = 279, BG_Y As Integer = 453

		Dim fullW As Integer
		Dim toDraw as Rectangle

		fullW = 260 * number \ STEPS
		SwinGame.DrawBitmap(_LoaderEmpty, BG_X, BG_Y)
		SwinGame.DrawCell (_LoaderFull, 0, BG_X, BG_Y)
		' SwinGame.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y)

		toDraw.X = TX
		toDraw.Y = TY
		toDraw.Width = TW
        toDraw.Height = TH
		SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, toDraw)
		' SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH)

        SwinGame.RefreshScreen()
        SwinGame.ProcessEvents()
    End Sub

    ''' <summary>
    ''' Releases resources after loading screen
    ''' </summary>

    Private Sub EndLoadingScreen(ByVal width As Integer, ByVal height As Integer)
        SwinGame.ProcessEvents()
        SwinGame.Delay(500)
        SwinGame.ClearScreen()
        SwinGame.RefreshScreen()
        SwinGame.FreeFont(_LoadingFont)
        SwinGame.FreeBitmap(_Background)
        SwinGame.FreeBitmap(_Animation)
        SwinGame.FreeBitmap(_LoaderEmpty)
        SwinGame.FreeBitmap(_LoaderFull)
        Audio.FreeSoundEffect(_StartSound)
        SwinGame.ChangeScreenSize(width, height)
    End Sub

    ''' <summary>
    ''' Adds font resource from given filename
    ''' </summary>

    Private Sub NewFont(ByVal fontName As String, ByVal filename As String, ByVal size As Integer)
        _Fonts.Add(fontName, SwinGame.LoadFont(SwinGame.PathToResource(filename, ResourceKind.FontResource), size))
    End Sub

    ''' <summary>
    ''' Adds image resource from given filename
    ''' </summary>

    Private Sub NewImage(ByVal imageName As String, ByVal filename As String)
        _Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(filename, ResourceKind.BitmapResource)))
    End Sub

    ''' Redundant code

    Private Sub NewTransparentColorImage(ByVal imageName As String, ByVal fileName As String, ByVal transColor As Color)
        _Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(fileName, ResourceKind.BitmapResource)))
    End Sub

    ''' <summary>
    ''' Adds image resource from given filename and creates transparent color image
    ''' </summary>

    Private Sub NewTransparentColourImage(ByVal imageName As String, ByVal fileName As String, ByVal transColor As Color)
        NewTransparentColorImage(imageName, fileName, transColor)
    End Sub

    ''' <summary>
    ''' Adds sound resource from given filename
    ''' </summary>

    Private Sub NewSound(ByVal soundName As String, ByVal filename As String)
        _Sounds.Add(soundName, Audio.LoadSoundEffect(SwinGame.PathToResource(filename, ResourceKind.SoundResource)))
    End Sub

    ''' <summary>
    ''' Adds music resource from given filename
    ''' </summary>

    Private Sub NewMusic(ByVal musicName As String, ByVal filename As String)
        _Music.Add(musicName, Audio.LoadMusic(SwinGame.PathToResource(filename, ResourceKind.SoundResource)))
    End Sub

    ''' <summary>
    ''' Release font resources
    ''' </summary>

    Private Sub FreeFonts()
        Dim obj As Font
        For Each obj In _Fonts.Values
            SwinGame.FreeFont(obj)
        Next
    End Sub

    ''' <summary>
    ''' Release image resources
    ''' </summary>

    Private Sub FreeImages()
        Dim obj As Bitmap
        For Each obj In _Images.Values
            SwinGame.FreeBitmap(obj)
        Next
    End Sub

    ''' <summary>
    ''' Release sound resources
    ''' </summary>

    Private Sub FreeSounds()
        Dim obj As SoundEffect
        For Each obj In _Sounds.Values
            Audio.FreeSoundEffect(obj)
        Next
    End Sub

    ''' <summary>
    ''' Release music resources
    ''' </summary>

    Private Sub FreeMusic()
        Dim obj As Music
        For Each obj In _Music.Values
            Audio.FreeMusic(obj)
        Next
    End Sub

    ''' <summary>
    ''' Release all font, image, music and sound resources
    ''' </summary>

    Public Sub FreeResources()
        FreeFonts()
        FreeImages()
        FreeMusic()
        FreeSounds()
		SwinGame.ProcessEvents()
    End Sub
End Module
