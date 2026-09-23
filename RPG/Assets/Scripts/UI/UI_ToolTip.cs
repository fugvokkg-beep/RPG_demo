using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private Vector2 offest = new Vector2(300, 20);

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public virtual void ShowtoolTip(bool show, RectTransform targetRect)
    {
        if (show == false)
        {
            rect.position = new Vector2(9999, 9999);
            return;
        }

        UpdatePosition(targetRect);
    }

    private void UpdatePosition(RectTransform targetRect)
    {
        float screenCenterX = Screen.width / 2f;
        float screenTop = Screen.height;
        float screenButton = 0;

        Vector2 targetPosition = targetRect.position;
        targetPosition.x = targetPosition.x > screenCenterX ? targetPosition.x - offest.x : targetPosition.x + offest.x;

        float veritcalHalf = rect.sizeDelta.y / 2f;
        float topY = targetPosition.y + veritcalHalf;
        float buttonY = targetPosition.y - veritcalHalf;
        if (topY > screenTop)
        {
            targetPosition.y = screenTop - veritcalHalf;
        }
        else if (buttonY < screenButton)
        {
            targetPosition.y = screenButton + veritcalHalf;
        }


        rect.position = targetPosition;
    }
}
