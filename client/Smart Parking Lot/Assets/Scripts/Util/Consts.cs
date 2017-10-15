public class Consts
{
    // config
    public const int MAXLEVEL = 3;

    // url
    public const string URL_Download = "http://www.littleredhat1997.com/game/game_download.php";
    public const string URL_Upload = "http://www.littleredhat1997.com/game/game_upload.php";

    // scene
    public const int Scene_Main = 0;
    // public const int Scene_Level_One = 1;
    // public const int Scene_Level_Two = 2;
    // public const int Scene_Level_Three = 3;

    // save
    public const string Music_Volume = "MusicVolume";
    public const string Effect_Volume = "EffectVolume";
    public const string Max_Level = "MaxLevel";

    // loading
    public const string Loading_Progress = "LoadingProgress";
    public const string Loading_Bar = "LoadingBar";
    // public const string Loading_Icon = "LoadingIcon";

    // audio
    public const string Audio_Click = "click";
    public const string Audio_Lift = "lift";
    public const string Audio_Win = "win";
    public const string Audio_Over = "over";

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
    /// belt    0   2   4   2
    /// lift    0   0   0   1
    /// robot_1 0   0   2   3
    /// robot_3 0   3   4   4
    /// </summary>
    public static int[,] Level = { { 0, 0, 0, 0 }, { 2, 0, 0, 3 }, { 4, 0, 2, 4 }, { 2, 1, 3, 4 } };
}
