
using UnityEngine;

namespace HibbyGames
{

    //This script is used to change the color of the material on a GameObject//
    //The gameObject you attach this script to must have a Renderer component//
    [RequireComponent(typeof(Renderer))]
    public class colorChanger : MonoBehaviour
    {

        [Header("Setup Custom Colors")]
        [SerializeField] private Color[] customColors;//The array of custom colors to use//
        [Header("Default Color from Custom Colors Array, use the Element Number")]
        [SerializeField] private int defaultColorN = 0;//The default color to start with, indexed by the customColors array
        [Header("Material to affect in the Renderer Materials Array")]
        [SerializeField] private int materialN = 0;//The material to change the color of, indexed by the materials array on the renderer component
        [Header("Shader Property the material uses for it's main color, such as _BaseColor or _Color")]
        [SerializeField] private string shaderProperty = "_BaseColor";//The name of the property on the material's shader used for the main color
        [Header("Start with a random color from the Custom Colors Array")]
        [SerializeField] private bool randomColor = false;//Should the script start with a random color from the customColors array

        private Renderer rend;//A reference to the renderer component on this GameObject

        private void Awake()
        {
            rend = GetComponent<Renderer>();

            //restrict material to range of Renderer materials array//
            materialN = Mathf.Clamp(materialN, 0, rend.materials.Length - 1);
            if (materialN < 0)
            {
                materialN = 0;
            }

            if (rend.materials.Length > 0 && materialN > 0)
            {
                //If the renderer component doesn't have the specified material//
                if (!rend.materials[materialN])
                {
                    //Log an error and disable the script//
                    Debug.Log("ERROR: the Renderer component of " + gameObject.name + " needs to have a material at element " + materialN);
                    this.enabled = false;
                    return;
                }
                //If the material doesn't have a color property//
                if (!rend.materials[materialN].HasProperty("_Color"))
                {
                    //Log an error and disable the script//
                    Debug.Log("ERROR: the Material of " + gameObject.name + " needs to have a color property");
                    this.enabled = false;
                    return;
                }
            }
            else
            {
                //If the renderer component doesn't have a material//
                if (!rend.material)
                {
                    //Log an error and disable the script//
                    Debug.Log("ERROR: the Renderer component of " + gameObject.name + " needs to have a material");
                    this.enabled = false;
                    return;
                }
                //If the material doesn't have a color property//
                if (!rend.material.HasProperty("_Color"))
                {
                    //Log an error and disable the script//
                    Debug.Log("ERROR: the Material of " + gameObject.name + " needs to have a color property");
                    this.enabled = false;
                    return;
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //If no custom colors have been set up, create a default color//
            if (customColors.Length == 0)
            {
                //Use the color that's already on the material//
                customColors = new Color[] { rend.material.color };
            }

            //restrict default color to range of custom colors array//
            defaultColorN = Mathf.Clamp(defaultColorN, 0, customColors.Length - 1);
            if (defaultColorN < 0)
            {
                defaultColorN = 0;
            }



            //start with a random color from the custom colors array//
            if (randomColor)
            {
                randomizeColor();
            }
            //start with the default color//
            else
            {
                setMatColor(defaultColorN);
            }
        }

        //set the material to a color from the custom colors array//
        public void setMatColor(int _customColorN)
        {
            setMatColor(customColors[_customColorN], materialN, shaderProperty);
        }

        //set the material to a new specific color//
        public void setNewColor(Color _color)
        {
            // Pass the new color, the material index, and the shader property to the setMatColor method
            setMatColor(_color, materialN, shaderProperty);
        }

        //set the material to a random color from the custom colors array//
        public void randomizeColor()
        {
            //Generate a random number within the range of the customColors array//
            int randNum = UnityEngine.Random.Range(0, customColors.Length);
            //Pass the random index to the setMatColor method//
            setMatColor(randNum);
        }

        //apply the material color//
        private void setMatColor(Color _color, int _matN = 0, string _shaderProperty = "_BaseColor")
        {
            //Create a new MaterialPropertyBlock//
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            //set the color property on the block//
            block.SetColor(_shaderProperty, _color);
            //apply the block to the specefic material in the renderer//
            rend.SetPropertyBlock(block, _matN);
        }

    }
}
//1.0.0 - Hibby Games//