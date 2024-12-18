using I2.Loc;
using UnityEngine;

public class LanguageSave : MonoBehaviour
{
    private const string LanguageKey = "SelectedLanguage"; // Kaydetmek i�in anahtar

    private void Start()
    {
        // Kaydedilmi� dili kontrol et
        if (!PlayerPrefs.HasKey(LanguageKey))
        {
            // Telefonun sistem dilini al ve kaydet
            string systemLanguage = GetSystemLanguage();
            LocalizationManager.CurrentLanguage = systemLanguage;

            PlayerPrefs.SetString(LanguageKey, systemLanguage);
            PlayerPrefs.Save();

            Debug.Log("Sistem dili ayarland�: " + systemLanguage);
        }
        else
        {
            // Kaydedilmi� dili y�kle
            string savedLanguage = PlayerPrefs.GetString(LanguageKey);
            LocalizationManager.CurrentLanguage = savedLanguage;

            Debug.Log("Kaydedilmi� dil y�klendi: " + savedLanguage);
        }
    }

    // Telefonun sistem dilini alg�layan fonksiyon
    private string GetSystemLanguage()
    {
        string systemLanguage = Application.systemLanguage.ToString();

        // E�er sistem dili I2 Localization'da mevcutsa kullan, yoksa varsay�lan dil ata
        if (LocalizationManager.HasLanguage(systemLanguage))
        {
            return systemLanguage;
        }
        else
        {
            return "English"; // Varsay�lan dil
        }
    }

    // Se�ilen dili kaydetmek i�in �a��rabilece�iniz bir fonksiyon
    public void SaveSelectedLanguage(string language)
    {
        LocalizationManager.CurrentLanguage = language;

        PlayerPrefs.SetString(LanguageKey, language);
        PlayerPrefs.Save();

        Debug.Log("Se�ilen dil kaydedildi: " + language);
    }
}
