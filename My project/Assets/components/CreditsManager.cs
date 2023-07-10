using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Text creditsText;

    void Start()
    {
        creditsText.text = "Kenney: Space Shooter Redux https://kenney.nl/assets/space-shooter-redux \n " +
                           "Kenney: Space Shooter Extension https://kenney.nl/assets/space-shooter-extension \n "+
                           "Kenney: UI Audio - \n https://kenney.nl/assets/ui-audio \n " + 
                           "Kenney: Game Icons - \n https://kenney.nl/assets/game-icons  \n " +
                           "Kenney: Digital Audio - \n https://kenney.nl/assets/digital-audio \n " +
                           "Explosion: https://assetstore.unity.com/packages/2d/textures-materials/2d-flat-explosion-66932 \n \n " ;
    }

}
