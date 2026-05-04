using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePatternStarter : MonoBehaviour
{
    [SerializeField] private InfinitePatternListSO listSO;

    private List<InfinitePattern> _activePatterns = new();
    private InfinitePatternListSO _copiedSO;

    private void OnEnable()
    {
        StartCoroutine(StartExecute());   
    }

    private IEnumerator StartExecute()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (listSO == null)
        {
            Debug.LogError("listSO가 할당되지 않았습니다.");
            yield break;
        }

        _copiedSO = Instantiate(listSO);

        if (_copiedSO == null)
        {
            Debug.LogError("listSO 복사 실패");
            yield break;
        }

        _activePatterns = _copiedSO.Init(transform, _activePatterns);

        if (_activePatterns == null || _activePatterns.Count == 0)
        {
            Debug.LogError("패턴 리스트가 비어있거나 초기화 실패");
            yield break;
        }

        InfinitePattern selectedPattern = _copiedSO.GetActiveRandomPattern(_activePatterns);
        if (selectedPattern == null)
        {
            Debug.LogError("실행할 패턴을 선택하지 못했습니다.");
            yield break;
        }

        selectedPattern.Execute(_copiedSO, _activePatterns);
    }
}
