using I2.Loc;
using UnityEngine;

public class LanguageSave : MonoBehaviour
{
    private const string LanguageKey = "SelectedLanguage"; // Kaydetmek için anahtar

    private void Start()
    {
        // Kaydedilmiþ dili kontrol et
        if (!PlayerPrefs.HasKey(LanguageKey))
        {
            // Telefonun sistem dilini al ve kaydet
            string systemLanguage = GetSystemLanguage();
            LocalizationManager.CurrentLanguage = systemLanguage;

            PlayerPrefs.SetString(LanguageKey, systemLanguage);
            PlayerPrefs.Save();

            Debug.Log("Sistem dili ayarlandý: " + systemLanguage);
        }
        else
        {
            // Kaydedilmiþ dili yükle
            string savedLanguage = PlayerPrefs.GetString(LanguageKey);
            LocalizationManager.CurrentLanguage = savedLanguage;

            Debug.Log("Kaydedilmiþ dil yüklendi: " + savedLanguage);
        }
    }

    // Telefonun sistem dilini algýlayan fonksiyon
    private string GetSystemLanguage()
    {
        string systemLanguage = Application.systemLanguage.ToString();

        // Eðer sistem dili I2 Localization'da mevcutsa kullan, yoksa varsayýlan dil ata
        if (LocalizationManager.HasLanguage(systemLanguage))
        {
            return systemLanguage;
        }
        else
        {
            return "English"; // Varsayýlan dil
        }
    }

    // Seçilen dili kaydetmek için çaðýrabileceðiniz bir fonksiyon
    public void SaveSelectedLanguage(string language)
    {
        LocalizationManager.CurrentLanguage = language;

        PlayerPrefs.SetString(LanguageKey, language);
        PlayerPrefs.Save();

        Debug.Log("Seçilen dil kaydedildi: " + language);
    }
}
