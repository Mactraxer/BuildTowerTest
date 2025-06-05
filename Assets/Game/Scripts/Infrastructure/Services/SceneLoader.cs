using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services
{
    public class SceneLoader
    {
        public void LoadScene(string name, Action onLoaded = null)
        {
            if (name == SceneManager.GetActiveScene().name)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(name);
            loadOperation.completed += _ => onLoaded?.Invoke();
        }
    }
}