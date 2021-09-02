using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Level settings")]
    [SerializeField] private Transform _currentPlatform;
    [SerializeField] private GameObject _platformTemplate;
    [Range(6, 25)]
    [SerializeField] private int _amountOfPlatforms = 6;
    [SerializeField] private int _DistanceBetweenLevels = 4;
    [SerializeField] private GameObject _endLevelCheckBox;
    [SerializeField] private float _CheckBoxOffsetY = 1;
    [SerializeField] private GameObject _deathTrigger;
    [Header("Platform settrings")]
    [SerializeField] private int _minXLenthPlatform = 1;
    [SerializeField] private int _maxXLenthPlatform = 6;
    [Header("")]
    [SerializeField] private int _minYOffsetPlatform = 1;
    [SerializeField] private int _maxYOffsetPlatform = 5;

    private ChankEndChecker _chankEndChecker;
    private float _firstOffsetX = 0;

    private void Start()
    {
        BuildChanck();
    }

    private void BuildChanck()
    {
        for (int i = 0; i < _amountOfPlatforms; i++)
        {
            GameObject platform = Instantiate(_platformTemplate, transform);

            float lengthPlatformX = Random.Range(_minXLenthPlatform, _maxXLenthPlatform);
            float offsetPlatformY = Random.Range(_minYOffsetPlatform, _maxYOffsetPlatform);

            platform.transform.localScale = new Vector3(lengthPlatformX, platform.transform.localScale.y, platform.transform.localScale.z);

            platform.transform.position = new Vector3(_currentPlatform.position.x + _currentPlatform.localScale.x / 2 + lengthPlatformX / 2 + _firstOffsetX, _currentPlatform.position.y + offsetPlatformY, platform.transform.position.z);
            _firstOffsetX = 0;
            _currentPlatform = platform.transform;

            if (i == _amountOfPlatforms - 1)
            {
                GameObject checkBox = Instantiate(_endLevelCheckBox, transform);
                checkBox.transform.position = new Vector3(_currentPlatform.position.x, _currentPlatform.position.y + _currentPlatform.localScale.y / 2 + _CheckBoxOffsetY, _currentPlatform.position.z);
                _chankEndChecker = checkBox.GetComponent<ChankEndChecker>();
                _chankEndChecker.OnEndOfChanckDelegare += EndLevel;

                GameObject deathTrigger = Instantiate(_deathTrigger, transform);
                deathTrigger.transform.position = new Vector3(transform.position.x + _DistanceBetweenLevels / 2, transform.position.y, transform.transform.position.z);
            }
        }
    }

    private void EndLevel()
    {
        _firstOffsetX = _DistanceBetweenLevels;
        BuildChanck();
    }
}
