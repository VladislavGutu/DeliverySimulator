using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UiLoadingIndicator : MonoBehaviour
{
    private int _refCount;

    public async Task ShowLoadingIndicator(Func<Task> action)
    {
        if (action == null)
            throw new NullReferenceException();

        try
        {
            var current = Interlocked.Increment(ref _refCount);
            if(current > 0)
                gameObject?.SetActive(true);

            await action();
        }
        finally
        {
            var current = Interlocked.Decrement(ref _refCount);
            if(current <= 0)
                gameObject?.SetActive(false);
        }
    }

    public void ShowLoadingIndicator(bool show)
    {
        Debug.LogError("show? " + show);
        gameObject?.SetActive(show);
    }
    
}