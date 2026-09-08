using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiper;
    [SerializeField] private float inageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }
    public void Move(float distamseToMove)
    {
        background.position += new Vector3(distamseToMove * parallaxMultiper, 0, 0);
    }

    public void LoopBackground(float cameraLeftRange, float cameraRightRange)
    {
        float imageRightRange = (background.position.x + imageHalfWidth) - inageWidthOffset;
        float imageLeftRange = (background.position.x - imageHalfWidth) + inageWidthOffset;

        if (imageRightRange < cameraLeftRange)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if (imageLeftRange > cameraRightRange)
        {
            background.position += Vector3.right * -imageFullWidth;
        }
    }
}
