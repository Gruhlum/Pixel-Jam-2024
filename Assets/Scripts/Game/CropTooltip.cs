using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexTecGames
{
	public class CropTooltip : MonoBehaviour
	{
		[SerializeField] private TMP_Text growGUI = default;
		[SerializeField] private TMP_Text dryGUI = default;
		[SerializeField] private TMP_Text durationGUI = default;

		[SerializeField] private GraphicRaycaster raycaster = default;
        [SerializeField] private GameObject go = default;
        void Update()
        {
            CropDataDisplay display = RaycastUI(Input.mousePosition);
            if (display != null && display.Item is CropData cropData)
            {
                Setup(cropData);
                go.SetActive(true);
            }
            else go.SetActive(false);
        }

        private CropDataDisplay RaycastUI(Vector2 mousepos)
        {
            //Set up the new Pointer Event
            PointerEventData m_PointerEventData = new PointerEventData(EventSystem.current);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = mousepos;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent(out CropDataDisplay cropDisplay))
                {
                    return cropDisplay;
                }
            }
            return null;
        }

        public void Setup(CropData cropData)
		{
			dryGUI.text = "<mspace=20>" + cropData.MaximumDryTicks.ToString();
			growGUI.text = "<mspace=20>" + cropData.RequiredGrowthTicks.ToString();
			durationGUI.text = "<mspace=20>" + cropData.WaterPerTick.ToString();
		}
	}
}