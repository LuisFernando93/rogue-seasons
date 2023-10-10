[System.Serializable]
public class OptionsData
{
    private float _masterVolume;
    private float _volumeMusic;
    private float _volumeSFX;
    private string _language;

    public OptionsData(float masterVolume, float volumeMusic, float volumeSFX, string language)
    {
        this._masterVolume = masterVolume;
        this._volumeMusic = volumeMusic;
        this._volumeSFX = volumeSFX;
        this._language = language;
    }

    public float getMasterVolume()
    {
        return this._masterVolume;
    }
    public float getVolumeMusic()
    {
        return this._volumeMusic;
    }

    public float getVolumeSFX()
    {
        return this._volumeSFX;
    }

    public string getLanguage()
    {
        return this._language;
    }
}
