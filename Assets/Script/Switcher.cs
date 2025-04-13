using System;
using AYellowpaper.SerializedCollections;
using Script.Util;
using UnityEngine;

namespace Script
{

    public enum UIState
    {
        Home,
        SignLogin,
    }

    public class Switcher : MonoBehaviour
    {
        [SerializeField] SerializedDictionary<UIState, UIFrame> uIStates;

        [SerializeField] UIState startState = UIState.Home;
        [SerializeField] [ReadOnlyEditor] private GameObject current;

        private void Awake()
        {
            foreach (var keyValuePair in uIStates)
                keyValuePair.Value.gameObject.SetActive(false);
        }

        private void Start()
        {
            SetUIState(startState);
        }

        public void SetUIState(UIState state)
        {
            if (current)
                current.SetActive(false);
            GameObject go = uIStates[state].gameObject;
            go.SetActive(true);
            current = go;
        }
    }
}