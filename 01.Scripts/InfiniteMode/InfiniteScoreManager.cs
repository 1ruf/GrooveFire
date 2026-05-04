using UnityEngine;
using TMPro;
using GondrLib.Dependencies;

[Provide]
public class InfiniteScoreManager : MonoBehaviour, IDependencyProvider
{
    [SerializeField] private ClearPlayerReaderBoard readerBoard;
    public TextMeshProUGUI TimeText;

    private float _startTime;
    private bool _isCounting;

    private const string _saveKey = "InfiniteModeBestTime";


    private void Start()
    {
        CountStart();
    }
    void Update()
    {
        if (_isCounting)
        {
            float currentElapsedTime = Time.time - _startTime;
            UpdateTimeUI(currentElapsedTime);
        }
    }

    public void CountStart()
    {
        _startTime = Time.time;
        _isCounting = true;
    }

    public void CountEnd()
    {
        if (!_isCounting) return;

        float finalTime = Time.time - _startTime;
        _isCounting = false;

        SaveScore(Mathf.RoundToInt(finalTime));
        UpdateTimeUI(finalTime);
    }

    private void UpdateTimeUI(float time)
    {
        TimeText.text = Mathf.FloorToInt(time).ToString("F0");
    }

    public void SaveScore(int score) //?„ì‹œ
    {
        int bestScore = LoadScore();
        if (score > bestScore)
        {
            PlayerPrefs.SetInt(_saveKey, score);
            PlayerPrefs.Save();
        }
    }

    public int LoadScore() //?„ì‹œ
    {
        return PlayerPrefs.GetInt(_saveKey, 0);
    }

    public float GetCurrentTime()
    {
        return _isCounting ? Time.time - _startTime : 0f;
    }

    public void SetReaderBoard()
    {
        readerBoard.SetClearTime((int)(Time.time - _startTime));
    }
}
