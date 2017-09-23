public class Consts
{
    // url
    public const string URL_Download = "http://www.littleredhat1997.com/game/game_download.php";
    public const string URL_Upload = "http://www.littleredhat1997.com/game/game_upload.php";

    // tag
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string Car = "Car";
    public const string Grid = "Grid";
    public const string Des = "Des";

    // scene
    public const int Scene_Main = 0;

    // save
    public const string Music_Volume = "MusicVolume";
    public const string Effect_Volume = "EffectVolume";
    public const string Max_Chapter = "MaxChapter";
    public const string Max_Level = "MaxLevel";
    public const string Help_Belt = "HelpBelt";
    public const string Help_Lift = "HelpLift";
    public const string Help_Robot1 = "HelpRobot1";
    public const string Help_Robot3 = "HelpRobot3";

    // loading
    public const string Loading_Progress = "LoadingProgress";
    public const string Loading_Bar = "LoadingBar";
    // public const string Loading_Icon = "LoadingIcon";

    // audio
    public const string Audio_Click = "music/click";
    public const string Audio_Belt = "music/belt";
    public const string Audio_Lift = "music/lift";
    public const string Audio_Robot = "music/robot";
    public const string Audio_Put = "music/put";
    public const string Audio_Win = "music/win";
    public const string Audio_Over = "music/over";

    // effect
    public const string Effect_Put = "effect/putEffect";
    public const string Effect_Car = "effect/carEffect";
    public const string Effect_Win = "effect/winEffect";
    public const string Effect_Over = "effect/overEffect";

    // config
    public static int[] Chapter = { 3, 2, 0 };
    public static string[] ChapterName = { "街道", "城镇", "沙漠" };

    // score
    /// <summary>
    /// null    0
    /// belt    50
    /// lift    80
    /// robot_1 10
    /// robot_3 30
    /// </summary>
    public static int[] Score = { 0, 50, 80, 10, 30 };

    // level
    /// <summary>
    /// belt    0   2   4   2   1   7
    /// lift    0   0   0   1   1   3
    /// robot_1 0   0   2   3   13  6
    /// robot_3 0   3   4   4   2   14
    /// </summary>
    public static int[,] Level = { { 0, 0, 0, 0 }, { 2, 0, 0, 3 }, { 4, 0, 2, 4 }, { 2, 1, 3, 4 }, { 1, 1, 13, 2 }, { 7, 3, 6, 14 } };
}
