using UnityEngine;
using UnityEngine.UI;

public class SpawnImagesOnClick : MonoBehaviour
{
    public Image[] images; // Array to hold references to your images
    private int currentImageIndex = 0; // To keep track of which image to spawn next

    void Update()
    {
        // Check for a mouse click or touch input
        if (Input.GetMouseButtonDown(0))
        {
            SpawnNextImage();
        }
    }

    void SpawnNextImage()
    {
        // Check if there are more images to spawn
        if (currentImageIndex < images.Length)
        {
            // Start the scaling animation for the next image
            images[currentImageIndex].transform.localScale = Vector3.one;
            currentImageIndex++;
        }
    }
}
