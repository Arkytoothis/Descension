using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class EncounterRoom : Singleton<EncounterRoom>
    {
        [SerializeField] GameObject cellPrefab = null;
        [SerializeField] GameObject[,] cells = null;
        [SerializeField] Transform cellsParent = null;

        [SerializeField] List<GameObject> pcObjects = new List<GameObject>();
        [SerializeField] List<GameObject> npcObjects = new List<GameObject>();

        [SerializeField] List<Vector3> pcStartPositions = new List<Vector3>();
        [SerializeField] List<Vector3> npcStartPositions = new List<Vector3>();

        [SerializeField] EncounterCursor cursor = null;

        [SerializeField] Transform pcsParent = null;
        [SerializeField] Transform npcsParent = null;

        [SerializeField] int width = 0;
        [SerializeField] int height = 0;

        public List<GameObject> PcObjects { get => pcObjects; set => pcObjects = value; }
        public List<GameObject> NpcObjects { get => npcObjects; set => npcObjects = value; }
        public EncounterCursor Cursor { get => cursor; set => cursor = value; }
        public List<Vector3> PcStartPositions { get => pcStartPositions; }
        public List<Vector3> NpcStartPositions { get => npcStartPositions; }

        private void Awake()
        {
            Reload();
            cursor.enabled = false;
        }

        public void Initialize(int width, int height)
        {
            this.width = width;
            this.height = height;

            cursor.enabled = true;
            ClearCells();
            cells = new GameObject[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x, y] = Instantiate(cellPrefab, cellsParent);
                    cells[x, y].name = "Cell " + x + " " + y;
                    cells[x, y].transform.localPosition = new Vector3(x, 0, y);

                    EncounterCell cell = cells[x, y].GetComponent<EncounterCell>();
                    cell.Setup(x, y, true);
                }
            }
        }

        public void UpdateCells()
        {
            foreach (PcData pc in PcManager.instance.PartyData.Pcs)
            {
                int x = pc.XCell;
                int y = pc.YCell;
                EncounterCell cell = cells[x, y].GetComponent<EncounterCell>();
                cell.PcIndex = pc.PartyIndex;
            }

            foreach (NpcData npc in EncounterManager.instance.Npcs)
            {
                int x = npc.XCell;
                int y = npc.YCell;
                EncounterCell cell = cells[x, y].GetComponent<EncounterCell>();
                cell.NpcIndex = npc.Index;
            }
        }

        public void ClearCells()
        {
            Helper.ClearTransform(cellsParent);
        }

        public void ClearNpcs()
        {
            npcObjects.Clear();
            Helper.ClearTransform(npcsParent);
        }

        public void AddPc(GameObject pc, int index)
        {
            pc.transform.SetParent(pcsParent, false);
            pc.transform.localPosition = new Vector3(pcStartPositions[index].x - 0.5f, 0, pcStartPositions[index].y + 0.5f);
            pc.transform.Rotate(Vector3.up, 90f);
            pcObjects.Add(pc);

            PcManager.instance.PartyData.Pcs[index].XCell = (int)pcStartPositions[index].x;
            PcManager.instance.PartyData.Pcs[index].YCell = (int)pcStartPositions[index].y;
        }

        public void AddNpc(GameObject npc, int index)
        {
            npc.transform.SetParent(npcsParent, false);
            npc.transform.localPosition = new Vector3(npcStartPositions[index].x - 0.5f, 0, npcStartPositions[index].y + 0.5f);
            npc.transform.Rotate(Vector3.up, -90f);
            npcObjects.Add(npc);

            EncounterManager.instance.Npcs[index].XCell = (int)npcStartPositions[index].x;
            EncounterManager.instance.Npcs[index].YCell = (int)npcStartPositions[index].y;
        }

        public void DeselectAll()
        {
            DeselectPcs();
            DeselectNpcs();
        }

        public void SelectPc(int index)
        {
            DeselectPcs();
            DeselectNpcs();
            pcObjects[index].GetComponentInChildren<Selector>().Show();
        }

        public void SelectNpc(int index)
        {
            DeselectNpcs();
            DeselectPcs();
            npcObjects[index].GetComponentInChildren<Selector>().Show();
        }

        public void DeselectPcs()
        {
            foreach (GameObject pcObject in pcObjects)
            {
                pcObject.GetComponentInChildren<Selector>().Hide();
            }
        }

        public void DeselectNpcs()
        {
            foreach (GameObject npcObject in npcObjects)
            {
                npcObject.GetComponentInChildren<Selector>().Hide();
            }
        }
    }
}