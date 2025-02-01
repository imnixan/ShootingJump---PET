using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public static class Localization
{
    public static Dictionary<string, string> CurrentLanguage = EnglishLanguage;

    private static Dictionary<string, string> EnglishLanguage = new Dictionary<string, string>()
    {
        #region GunsDescriptions
        { "M1911_Description", "Simple pistol. Pew, pew." },
        { "Ak-74_Description", "High firerate, strong recoil." },
        { "P90_Description", "Very hight firerate." },
        { "Python_Description", "Strong recoil. Fast as your hand, cowboy." },
        { "Benelli M3_Description", "Strong recoil, very. *BANG!*" },
        { "No_honey_text", "Not enough scores" },
        { "Buy_text", "Press to buy" },
        { "DamageText", "Damage" },
        { "KillsText", "Kills" },
        { "TimeText", "Time" },
        { "BestTimeText", "Best time" },
        { "TimeBonusText", "Time bonus" },
        { "TotalScoresText", "Total scores" },
        { "BalanceText", "Balance" }

        #endregion
    };

    private static Dictionary<string, string> RussianLanguage = new Dictionary<string, string>()
    {
        #region GunsDescriptions
        { "M1911_Description", "Обычный пистолет, пиф-паф." },
        { "Ak-74_Description", "Высокая скорострельность, сильная отдача." },
        { "P90_Description", "Очень высокая скорострельность." },
        {
            "Python_Description",
            "Высокая отдача. Быстр настолько, насколько быстра твоя рука, ковбой."
        },
        { "Benelli M3_Description", "Сильная отдача, очень. *БАМ!*" },
        { "No_honey_text", "Недостаточно очков" },
        { "Buy_text", "Нажми для покупки" },
        { "DamageText", "Урон" },
        { "KillsText", "Убито" },
        { "TimeText", "Время" },
        { "BestTimeText", "Лучшее время" },
        { "TimeBonusText", "Бонус за время" },
        { "TotalScoresText", "Итого очков" },
        { "BalanceText", "Баланс" }
        #endregion
    };

    public enum Language
    {
        Russian,
        English
    }

    public static string SetLanguage(Language chosedLanguage)
    {
        switch (chosedLanguage)
        {
            case Language.English:
                CurrentLanguage = EnglishLanguage;
                return "En";
            default:
                CurrentLanguage = RussianLanguage;
                return "Ру";
        }
    }
}
