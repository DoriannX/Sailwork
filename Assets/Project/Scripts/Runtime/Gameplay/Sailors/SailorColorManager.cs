using UnityEngine;

namespace Project.Scripts.Runtime.Gameplay.Sailors
{
    /// <summary>
    /// this class is responsible for managing the whole sailor color
    /// </summary>
    public class SailorColorManager : MonoBehaviour
    {
        private Renderer[] spriteRenderers;
        private Color _sailorColor;
        
        /// <summary>
        /// A color property field is used to simultaneously set the color variable
        /// and the color of the sprite renderers
        /// </summary>
        public Color sailorColor
        {
            get => _sailorColor;
            private set
            {
                _sailorColor = value;
                foreach (Renderer spriteRenderer in spriteRenderers)
                {
                    spriteRenderer.material.color = value;
                }
            }
        }
        
        private void Awake()
        {
            spriteRenderers = GetComponentsInChildren<Renderer>();
        }
        
        /// <summary>
        /// A color setter method is used to prevent unwanted changes to the color
        /// if the developpers want to add behavior
        /// </summary>
        /// <param name="color"></param>
        public void SetSailorColor(Color color)
        {
            sailorColor = color;
        }
    }
}