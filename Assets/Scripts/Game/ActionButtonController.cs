using HexTecGames.GridBaseSystem;
using HexTecGames.SoundSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames
{
	public class ActionButtonController : MonoBehaviour
	{
		[SerializeField] private List<ActionButton> buttons = default;

		[SerializeField] private GridEventSystem gridEventSys = default;
        [SerializeField] private BaseGrid grid = default;
        [SerializeField] private PlayerUnitController playerUnitC = default;
        [SerializeField] private ResourceController resourceC = default;
        [SerializeField] private ResourceType goldType = default;
        [SerializeField] private GridPlacementController gridPlacementC = default;

        [SerializeField] private SoundClipBase sellSound = default;
        [SerializeField] private SoundClipBase activateSound = default;

        private Resource gold;
        private ActionButton selectedBtn;

        private Coord? mouseCoord;

        void Awake()
        {
            gridEventSys.OnMouseHoverCoordChanged += GridEventSys_OnMouseHoverCoordChanged;
            gridPlacementC.OnSelectedObjectChanged += GridPlacementC_OnSelectedObjectChanged;
        }
     
        void Start()
        {
            var results = resourceC.GetResources();
            gold = results.Find(x => x.Data == goldType);
        }
        void OnDestroy()
        {
            gridEventSys.OnMouseHoverCoordChanged -= GridEventSys_OnMouseHoverCoordChanged;
            gridPlacementC.OnSelectedObjectChanged -= GridPlacementC_OnSelectedObjectChanged;
        }
        void Update()
        {
            if (mouseCoord.HasValue && selectedBtn != null && Input.GetMouseButton(0))
            {
                TileObject obj = grid.GetTileObject(mouseCoord.Value);
                if (obj is Crop crop && crop.IsFullyGrown)
                {
                    if (selectedBtn is SellButton)
                    {
                        gold.Value += 4;
                        sellSound?.Play();
                    }
                    else
                    {
                        playerUnitC.SetupUnit(crop);
                        activateSound?.Play();
                    }
                    crop.Remove();
                }
            }
        }
        private void GridEventSys_OnMouseHoverCoordChanged(Coord obj)
        {
            mouseCoord = obj;
        }
        private void GridPlacementC_OnSelectedObjectChanged(GridObjectData obj)
        {
            if (obj != null)
            {
                if (selectedBtn != null)
                {
                    selectedBtn.IsSelected = false;
                    selectedBtn = null;
                }
            }
        }

        public void ButtonClicked(ActionButton clickedBtn)
        {
            foreach (var button in buttons)
            {
                button.IsSelected = clickedBtn == button;
            }
            selectedBtn = clickedBtn;

            gridPlacementC.ClearSelectedObject();
        }
    }
}