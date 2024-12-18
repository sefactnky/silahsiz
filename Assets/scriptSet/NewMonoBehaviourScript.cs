using UnityEngine;
using I2.Loc; // I2 Localization sistemini kullanabilmek için bu kütüphaneesi

public class LanguageButton : MonoBehaviour
{
    private const string LanguageKey = "SelectedLanguage";
    private void Start()
    {
        if (PlayerPrefs.HasKey(LanguageKey)) // Önceden kaydedilmiþ bir dil var mý kontrol et
        {
            string savedLanguage = PlayerPrefs.GetString(LanguageKey);
            LocalizationManager.CurrentLanguage = savedLanguage;
            Debug.Log("Kaydedilen dil yüklendi: " + savedLanguage);
        }
        else
        {
            Debug.Log("Kaydedilmiþ dil bulunamadý, varsayýlan dil kullanýlýyor.");
        }
    }

// Bu fonksiyon, butona týklanarak çaðrýlacak
public void ChangeLanguage(string languageCode)
    {
        // Seçilen dili aktif yap
        LocalizationManager.CurrentLanguage = languageCode;

        // Tüm UI öðelerini yeniden yükle ve dili uygula
        LocalizationManager.UpdateSources();
    }
    
    public void SetLanguageToArabic()
    {
        LocalizationManager.CurrentLanguage = "Arabic";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Arabic");
    }


    public void SetLanguageToTurkish()
    {
        LocalizationManager.CurrentLanguage = "Turkish";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Turish");
    }

    public void SetLanguageToEnglish()
    {
        LocalizationManager.CurrentLanguage = "English";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("English");
    }

    public void SetLanguageToAfrikaans()
    {
        LocalizationManager.CurrentLanguage = "Afrikaans";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Afrikaans");
    }

    public void SetLanguageToChinese()
    {
        LocalizationManager.CurrentLanguage = "Chinese";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Chinese");
    }

    public void SetLanguageToCroatian()
    {
        LocalizationManager.CurrentLanguage = "Croatian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Croatian");
    }

    public void SetLanguageToDutch()
    {
        LocalizationManager.CurrentLanguage = "Dutch";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Dutch");
    }

    public void SetLanguageToFrench()
    {
        LocalizationManager.CurrentLanguage = "French";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("French");
    }

    public void SetLanguageToGerman()
    {
        LocalizationManager.CurrentLanguage = "German";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("German");
    }

    public void SetLanguageToGreek()
    {
        LocalizationManager.CurrentLanguage = "Greek";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Greek");
    }

    public void SetLanguageToIndonesian()
    {
        LocalizationManager.CurrentLanguage = "Indonesian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Indonesian");
    }

    public void SetLanguageToItalian()
    {
        LocalizationManager.CurrentLanguage = "Italian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Italian");
    }

    public void SetLanguageToJapanese()
    {
        LocalizationManager.CurrentLanguage = "Japanese";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Japanese");
    }

    public void SetLanguageToKorean()
    {
        LocalizationManager.CurrentLanguage = "Korean";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Korean");
    }

    public void SetLanguageToNorwegian()
    {
        LocalizationManager.CurrentLanguage = "Norwegian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Norwegian");
    }

    public void SetLanguageToPersian()
    {
        LocalizationManager.CurrentLanguage = "Persian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Persian");
    }

    public void SetLanguageToPolish()
    {
        LocalizationManager.CurrentLanguage = "Polish";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Polish");
    }

    public void SetLanguageToPortuguese()
    {
        LocalizationManager.CurrentLanguage = "Portuguese";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Portuguese");
    }

    public void SetLanguageToRomanian()
    {
        LocalizationManager.CurrentLanguage = "Romanian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Romanian");
    }

    public void SetLanguageToRussian()
    {
        LocalizationManager.CurrentLanguage = "Russian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Russian");
    }

    public void SetLanguageToSpanish()
    {
        LocalizationManager.CurrentLanguage = "Spanish";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Spanish");
    }

    public void SetLanguageToSwedish()
    {
        LocalizationManager.CurrentLanguage = "Swedish";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Swedish");
    }

    public void SetLanguageToThai()
    {
        LocalizationManager.CurrentLanguage = "Thai";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Thai");
    }

    public void SetLanguageToUkrainian()
    {
        LocalizationManager.CurrentLanguage = "Ukrainian";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Ukrainian");
    }

    public void SetLanguageToVietnamese()
    {
        LocalizationManager.CurrentLanguage = "Vietnamese";
        FindObjectOfType<LanguageSave>().SaveSelectedLanguage("Vietnamese");
    }

}


