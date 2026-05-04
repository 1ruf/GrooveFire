using System.Collections.Generic;
using UnityEngine;

public abstract class InfinitePattern : MonoBehaviour
{
    public abstract void Execute(InfinitePatternListSO PatternList, List<InfinitePattern> _activePatterns);
    public virtual void ExecuteNextPattern(InfinitePatternListSO PatternList, List<InfinitePattern> _activePatterns)
    {
        InfinitePattern nextPattern = PatternList.GetActiveRandomPattern(_activePatterns);
        if (nextPattern == null) return;

        nextPattern.Execute(PatternList, _activePatterns);
    }
}
