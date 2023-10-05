using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;

    private void Start()
    {
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
    private void OnEnable()
    {
        m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }
    IEnumerator Func_PlayAnimUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_Speed);

            if (m_IndexSprite >= m_SpriteArray.Length)
            {
                m_IndexSprite = 0;
            }

            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite += 1;

            // You might want to add a condition to break out of the loop eventually
            if (IsDone)
            {
                break; // Exit the loop when IsDone becomes true
            }
        }
    }
}
