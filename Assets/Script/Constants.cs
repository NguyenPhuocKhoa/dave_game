using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // Scence Name.
    public static string StartScence = "StartScence";
    public static string NextLevel = "NextLevel";
    public static string Level1 = "Level1";
    public static string Level2 = "Level2";
    public static string Level3 = "Level3";
    public static string Level4 = "Level4";
    public static string Level5 = "Level5";
    public static string Level6 = "Level6";
    public static string Level7 = "Level7";
    public static string Level8 = "Level8";
    public static string Level9 = "Level9";
    public static string Level10 = "Level10";
    public static string EndGame = "EndGame";


    // Common text.
    public static string _curLevel = "_curLevel";
    public static string _curLives = "_curLives";
    public static string _curScores = "_curScores";

    // Default value.
    public const int PlayerLives = 5;
    public const int StartScore = 0;
    public const int Diamond_Score = 20;
    public const int VuongMien_Score = 100;
    public const int Den_Score = 50;

    // Current value.
    public static string _curLevelStr = Level1;
    public static int _curLivesInt = PlayerLives;
    public static int _curScoresInt = StartScore;
    public static int _currentTotalScore = StartScore;

    // Use Param.
    public static bool _isAnKimCuong = false;
}
