using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnectHandler childNode;
    public NodeDirectionType direction;
    [Range(100f,350f)] public float length;
}


public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectDetails[] connectionDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private Image connectionImage;
    private Color originalColor;

    private void Awake()
    {
        if (connectionImage != null)
            originalColor = connectionImage.color;
    }

    private void OnValidate() 
    {
        if (connectionDetails.Length <= 0)
            return;

        if (connectionDetails.Length != connections.Length)
        {
            Debug.Log("amount error");
            return;
        }

        UpdateConnections();
    }

    private void UpdateConnections()
    {
        for (int i = 0; i < connectionDetails.Length; i++)
        {
            var detail = connectionDetails[i];
            var connection = connections[i];

            Vector2 targetPosition = connection.GetConnectionPoint(rect);
            Image connectionImage = connection.GetConnectionImage();


            connection.DirectConnection(detail.direction,detail.length);

            if (detail.childNode == null)
                return;

            detail.childNode?.SetPosition(targetPosition);
            detail.childNode?.SetConnectionImage(connectionImage);
        }
    }

    public void connectionImageUnlocked(bool unlocked)
    {
        if (connectionImage == null)
            return;
        connectionImage.color = unlocked ? Color.white : originalColor;
    }

    public void SetConnectionImage(Image img) => connectionImage = img;
    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;
}


