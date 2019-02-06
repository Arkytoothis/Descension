using Descension.Characters;
using Descension.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Descension
{
    public class EncounterManager : Singleton<EncounterManager>
    {
        [SerializeField] CurrentCharacterPanel characterPanel = null;
        [SerializeField] ActionBarPanel actionBarPanel = null;
        [SerializeField] InitiativePanel initiativePanel = null;
        [SerializeField] EncounterData encounterData = null;
        [SerializeField] EncounterSpawner spawner = null;
        [SerializeField] int initiativeIndex = -1;

        [SerializeField] List<NpcData> npcs = new List<NpcData>();
        [SerializeField] List<InitiativeData> initiativeList = new List<InitiativeData>();

        public List<NpcData> Npcs { get => npcs; set => npcs = value; }
        public List<InitiativeData> InitiativeList { get => initiativeList; set => initiativeList = value; }

        private bool allowInput = true;

        private void Awake()
        {
            Reload();
        }

        public void Initialize()
        {
        }

        public void StartEncounter(EncounterData encounterData, EncounterSpawner spawner)
        {
            this.encounterData = new EncounterData(encounterData);
            this.spawner = spawner;

            WorldCursor.instance.Disable();
            EncounterRoom.instance.Initialize(encounterData.MapWidth, encounterData.MapHeight);
            AdventureManager.instance.SetMode(AdventureManager.Mode.Encounter);

            GenerateNpcs();
            LoadNpcModels();
            EncounterRoom.instance.UpdateCells();

            RollInitiative();
            initiativePanel.LoadInitiativeOrder();

            SetupTurn();
        }

        public void EndEncounter()
        {
            WorldCursor.instance.Enable();
            AdventureManager.instance.SetMode(AdventureManager.Mode.Explore);
            spawner.Disable();

            EncounterRoom.instance.Cursor.enabled = false;
            EncounterRoom.instance.ClearCells();
            EncounterRoom.instance.ClearNpcs();
        }

        public void GenerateNpcs()
        {
            ClearNpcs();

            int numNpcs = Random.Range(encounterData.MinNpcs, encounterData.MaxNpcs);

            for (int i = 0; i < numNpcs; i++)
            {
                string definition = Helper.RandomKey(Database.NPCs);
                NpcData npcData = NpcGenerator.Generate(i, definition);

                npcs.Add(npcData);
            }
        }

        public void LoadNpcModels()
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                EncounterRoom.instance.AddNpc(ModelManager.instance.SpawnNpcModel(npcs[i]), i);
            }
        }

        public void ClearNpcs()
        {
            npcs.Clear();
            EncounterRoom.instance.ClearNpcs();
        }

        public void RollInitiative()
        {
            initiativeList.Clear();

            for (int i = 0; i < PcManager.instance.PartyData.Pcs.Count; i++)
            {
                InitiativeData.Type type = InitiativeData.Type.Player;
                string details = PcManager.instance.PartyData.Pcs[i].Name.ShortName;
                int initiative = Random.Range(1, 101);
                InitiativeData initiativeData = new InitiativeData(type, details, initiative, i, Faction.Player);
                initiativeList.Add(initiativeData);
            }

            for (int i = 0; i < npcs.Count; i++)
            {
                InitiativeData.Type type = InitiativeData.Type.Enemy;
                string details = npcs[i].Name.ShortName;
                int initiative = Random.Range(1, 101);
                InitiativeData initiativeData = new InitiativeData(type, details, initiative, i, npcs[i].Faction);
                initiativeList.Add(initiativeData);
            }

            initiativeList.Sort((p1, p2) => p1.Initiative.CompareTo(p2.Initiative));

            for (int i = 0; i < initiativeList.Count; i++)
            {
                if (initiativeList[i].Faction == Faction.Player)
                {
                    PcManager.instance.PartyData.Pcs[initiativeList[i].Index].InitiativeIndex = i;
                }
                else if (initiativeList[i].Faction == Faction.Enemy)
                {
                    npcs[initiativeList[i].Index].InitiativeIndex = i;
                }
            }
        }

        private void SelectPc(int index)
        {
            EncounterRoom.instance.SelectPc(index);
            initiativePanel.Select(PcManager.instance.PartyData.Pcs[index].InitiativeIndex);
            characterPanel.SetCharacterData(PcManager.instance.PartyData.Pcs[index]);
        }

        private void SelectNpc(int index)
        {
            EncounterRoom.instance.SelectNpc(index);
            initiativePanel.Select(npcs[index].InitiativeIndex);
            characterPanel.SetCharacterData(npcs[index]);
        }

        public void SetupTurn()
        {
            EncounterRoom.instance.DeselectAll();

            initiativeIndex = -1;
            ProcessTurn();
        }
        
        public void ProcessTurn()
        {
            initiativeIndex++;

            if (initiativeIndex >= initiativeList.Count)
                initiativeIndex = 0;

            InitiativeData initiativeData = initiativeList[initiativeIndex];

            if (initiativeData != null)
            {
                if (initiativeData.Faction == Faction.Player)
                {
                    SelectPc(initiativeData.Index);
                }
                else if (initiativeData.Faction == Faction.Enemy)
                {
                    SelectNpc(initiativeData.Index);
                    ProcessNpcActions(initiativeData.Index);
                }
            }

            initiativePanel.LoadInitiativeOrder();
        }

        private void ProcessNpcActions(int index)
        {
            allowInput = false;
            Invoke("AttackPc", 1f);
        }

        private void AttackPc()
        {
            int attackerIndex = initiativeList[initiativeIndex].Index;
            int targetIndex = Random.Range(0, PcManager.instance.PartyData.Pcs.Count);

            NpcData npc = npcs[attackerIndex];
            PcData pc = PcManager.instance.PartyData.Pcs[targetIndex];

            CombatManager.instance.ProcessMeleeAttack(npc, pc);

            EndNpcTurn();
        }

        private void AttackNpc()
        {
            int attackerIndex = initiativeList[initiativeIndex].Index;
            int targetIndex = Random.Range(0, npcs.Count);

            PcData pc = PcManager.instance.PartyData.Pcs[attackerIndex];
            NpcData npc = npcs[targetIndex];

            CombatManager.instance.ProcessMeleeAttack(pc, npc);
        }

        private void EndNpcTurn()
        {
            allowInput = true;
            ProcessTurn();
        }

        private void EndPcTurn()
        {
            AttackNpc();
            ProcessTurn();
        }

        private void Update()
        {
            if (allowInput == true)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    EndPcTurn();
                }
            }
        }
    }
}