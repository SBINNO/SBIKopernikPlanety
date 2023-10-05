//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    // Akcje dla eventów do obiektu (Enter = patrzy, Exit = nie patrzy, Click = wciśnięcie gdy patrzymy)
    private Action<GameObject, BaseEventData> pointerEnterAction;
    private Action<GameObject, BaseEventData> pointerExitAction;
    private Action<GameObject, BaseEventData> pointerClickAction;

    [SerializeField] private bool isTestingAndroid = false;
    private bool isAndroidBuild = false;
    [SerializeField] private bool isTestingWebGL = false;
    private bool isWebGLBuild = false;
    private bool isIphoneBuild = false;
    private void Awake()
    {
        //Inicjalizujemy akcje dla każdego typu eventów do obiektów 
        pointerEnterAction = ExecutePointerEvent<IPointerEnterHandler>(ExecutePointerEnter);
        pointerExitAction = ExecutePointerEvent<IPointerExitHandler>(ExecutePointerExit);
        pointerClickAction = ExecutePointerEvent<IPointerClickHandler>(ExecutePointerClick);
        isWebGLBuild = Application.platform == RuntimePlatform.WebGLPlayer;
        isAndroidBuild = Application.platform == RuntimePlatform.Android;
        isIphoneBuild = Application.platform == RuntimePlatform.IPhonePlayer;

    }

    private void Update()
    {
        // Wystrzeliwuje niewidzialny laser, który jest cały czas na środku ekranu, potrzebny żeby obiekty wiedziały że sie na nie patrzy.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            //Obiekt który jest obserwowany
            if (_gazedAtObject != hit.transform.gameObject)
            {
                //Odpala sie event OnPointerExit na poprzednim obiekcie
                ExecuteIfNotNull(_gazedAtObject, pointerExitAction);
                _gazedAtObject = hit.transform.gameObject;
                //Odpala sie event OnPointerEnter na nowym obiekcie
                ExecuteIfNotNull(_gazedAtObject, pointerEnterAction);
            }
        }
        else
        {
            //Odpala się kiedy nie mamy obiektu na kamerze, odpala się OnPointerExit na poprzednim obiekcie
            ExecuteIfNotNull(_gazedAtObject, pointerExitAction);
            _gazedAtObject = null;
        }
        Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);

        //Sprawdzamy czy kliknęliśmy button czy LMB
        if (isTestingWebGL || isWebGLBuild)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Odpala się event OnPointerClick jeżeli patrzymy na dany obiekt
                ExecuteIfNotNull(_gazedAtObject, pointerClickAction);
            }
        }
        
    }
    public void HandlePickUpButtonClick()
    {
        if (isTestingAndroid || isAndroidBuild || isIphoneBuild)
        {
            ExecuteIfNotNull(_gazedAtObject, pointerClickAction);
            // Execute the pointerClickAction when the button is clicked
        }
    }
    //Metoda która sprawdza czy dany obiekt jest null, jeżeli nie jest to robi event.
    private void ExecuteIfNotNull(GameObject obj, Action<GameObject, BaseEventData> action)
    {
        if (obj != null)
        {
            var eventData = new BaseEventData(EventSystem.current);
            action(obj, eventData);
        }
    }

    private Action<GameObject, BaseEventData> ExecutePointerEvent<T>(Action<GameObject, BaseEventData> action)
        where T : IEventSystemHandler
    {
        return (target, eventData) =>
        {
            //Egzekucja specyficznej akcji na danym obiekcie.
            action(target, eventData);
        };
    }

    private void ExecutePointerEnter(GameObject target, BaseEventData eventData)
    {
        ExecuteEvents.Execute<IPointerEnterHandler>(target, eventData, (x, y) => x.OnPointerEnter(null));
    }

    private void ExecutePointerExit(GameObject target, BaseEventData eventData)
    {
        ExecuteEvents.Execute<IPointerExitHandler>(target, eventData, (x, y) => x.OnPointerExit(null));
    }

    private void ExecutePointerClick(GameObject target, BaseEventData eventData)
    {
        ExecuteEvents.Execute<IPointerClickHandler>(target, eventData, (x, y) => x.OnPointerClick(null));
    }
}