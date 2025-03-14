using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Initial = 0,
        NotSelect,
        AttackFailure,
        AttackSuccess,
        DefenceFailure,
        DefenceSuccess,
        Win,
        Lose
    }

    // ĳ���� �̹���
    [SerializeField] Sprite initialImage;
    [SerializeField] Sprite notSelectImage;
    [SerializeField] Sprite attackFailureImage;
    [SerializeField] Sprite attackSuccessImage;
    [SerializeField] Sprite defenceFailureImage;
    [SerializeField] Sprite defenceSuccessImage;
    [SerializeField] Sprite winImage;
    [SerializeField] Sprite loseImage;

    // ���¿� �´� ĳ���� �̹��� ��ȯ
    public Sprite ReturnImage(State characterState)
    {
        switch (characterState)
        {
            case State.Initial: 
                return initialImage;
            case State.NotSelect: 
                return notSelectImage;
            case State.AttackFailure:
                return attackFailureImage;
            case State.AttackSuccess:
                return attackSuccessImage;
            case State.DefenceFailure:
                return defenceFailureImage;
            case State.DefenceSuccess:
                return defenceSuccessImage;
            case State.Win:
                return winImage;
            case State.Lose:
                return loseImage;
        }

        return null;
    }
}
