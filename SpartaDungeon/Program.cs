using Newtonsoft.Json;

namespace SpartaDungeon
{
    internal class Program
    {
        //public static int gold = 1500;
        public static int num;
        public static int restGold = 500;
        public static int chosenReward;
        public static float chosenDef = 0;
        public static string input;
        public static bool isNum;
        public static string path = "C:\\Users\\snagg\\source\\repos\\SpartaDungeon\\SpartaDungeon\\json";
        public class Item
        {
            public string altogether, itemEquipped, itemName, itemStatType, itemDesc, itemBought, goldStr, itemType;
            public int statNum, goldNum;
            public bool isEquipped, isBought;
            public float sellNum;
        }

        public class Hero
        {
            public string level, name, job, attack, defense, health, goldLabel, atkLabel, defLabel, armorType, weaponType;
            public int levelNum, attackItemNum, defenseTotal, defenseItemNum, healthTotal, baseDefense, gold;
            public float baseAttack, attackTotal;
        }

        public static Hero hero;

        public static List<Item> itemList = new List<Item>();

        public static void HeroInitialize()
        {
            hero = new Hero();
            hero.levelNum = 1;
            hero.baseAttack = 10;
            hero.baseDefense = 5;
            hero.attackTotal = 10;
            hero.attackItemNum = 0;
            hero.defenseTotal = 5;
            hero.defenseItemNum = 0;
            hero.healthTotal = 100;
            hero.weaponType = "";
            hero.armorType = "";

            hero.level = "Lv. 0";
            hero.name = "Chad";
            hero.job = "( 전사 )";
            hero.attack = "공격력 : ";
            hero.defense = "방어력 : ";
            hero.health = "체력 : ";
            hero.gold = 1500;
            hero.goldLabel = $"Gold : {hero.gold} G";
            hero.atkLabel = "";
            hero.defLabel = "";
        }

        public static void ItemInitialize()
        {
            itemList.AddRange(new List<Item>
            {
                new Item {itemName = "수련자 갑옷", itemStatType = "방어력", itemDesc = "수련에 도움을 주는 갑옷입니다.",
                    statNum = 5, goldNum = 1000, itemType = "방어구"},
                new Item {itemName = "무쇠갑옷", itemStatType = "방어력", itemDesc = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                    statNum = 9, goldNum = 2117, itemType = "방어구"},
                new Item {itemName = "스파르타의 갑옷", itemStatType = "방어력", itemDesc = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                    statNum = 15, goldNum = 3500, itemType = "방어구"},
                new Item {itemName = "낡은 검", itemStatType = "공격력", itemDesc = "쉽게 볼 수 있는 낡은 검 입니다.",
                    statNum = 2, goldNum = 600, itemType = "무기"},
                new Item {itemName = "청동 도끼", itemStatType = "공격력", itemDesc = "어디선가 사용됐던거 같은 도끼입니다.",
                    statNum = 5, goldNum = 1500, itemType = "무기"},
                new Item {itemName = "스파르타의 창", itemStatType = "공격력", itemDesc = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                    statNum = 7, goldNum = 3176, itemType = "무기"}
            });

            for (int i = 0; i < itemList.Count; i++)
            {
                Item item = itemList[i];
                item.isEquipped = false;
                item.isBought = false;
                item.itemEquipped = "";
                item.goldStr = $"{item.goldNum} G";
                item.altogether = item.itemEquipped + item.itemName + "\t|" + item.itemStatType +
                    " +" + item.statNum + " | " + item.itemDesc + "\t |" + item.goldStr;
                item.sellNum = (float)item.goldNum * 0.85f;
                itemList[i] = item;
            }
        }
        public static void Main(string[] args)
        {
            LoadData();
            MainScene();
        }

        public static void MainScene()
        {
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("6. 저장하기\n");

                ActionLine();

                if (isNum)
                {
                    num = int.Parse(input);
                    Console.Clear();

                    switch (num)
                    {
                        case 1:
                            CheckInfo();
                            break;
                        case 2:
                            Inventory();
                            break;
                        case 3:
                            Store();
                            break;
                        case 4:
                            EnterDungeon();
                            break;
                        case 5:
                            Rest();
                            break;
                        case 6:
                            SaveData();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    WrongInput();
                }
            }
        }
        public static void CheckInfo()
        {
            while (true)
            {
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");

                hero.attackItemNum = 0;
                hero.defenseItemNum = 0;
                hero.atkLabel = "";
                hero.defLabel = "";

                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemList[i].isBought)
                    {
                        Item item = itemList[i];
                        if (item.isEquipped)
                        {
                            switch (item.itemStatType)
                            {
                                case "공격력":
                                    hero.attackItemNum += item.statNum;
                                    break;
                                case "방어력":
                                    hero.defenseItemNum += item.statNum;
                                    break;
                            }
                        }
                        else
                        {
                            if (hero.attackItemNum != 0)
                            {
                                switch (item.itemStatType)
                                {
                                    case "공격력":
                                        hero.attackItemNum -= item.statNum;
                                        break;
                                    case "방어력":
                                        hero.defenseItemNum -= item.statNum;
                                        break;
                                }
                            }
                        }
                    }
                }
                hero.attackTotal = hero.baseAttack + (float)hero.attackItemNum;
                hero.defenseTotal = hero.baseDefense + hero.defenseItemNum;

                hero.atkLabel = hero.attackItemNum > 0 ? $" (+{hero.attackItemNum})" : "";
                hero.defLabel = hero.defenseItemNum > 0 ? $" (+{hero.defenseItemNum})" : "";

                hero.goldLabel = $"Gold : {hero.gold} G";

                Console.WriteLine(hero.level + hero.levelNum);
                Console.WriteLine(hero.name + hero.job);
                Console.WriteLine(hero.attack + hero.attackTotal + hero.atkLabel);
                Console.WriteLine(hero.defense + hero.defenseTotal + hero.defLabel);
                Console.WriteLine(hero.health + hero.healthTotal);
                Console.WriteLine(hero.goldLabel);

                Console.WriteLine("\n0. 나가기\n");
                ActionLine();
                Console.Clear();
                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    WrongInput();
                }
            }
        }

        public static void Inventory()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            if (itemList != null)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemList[i].isBought == true)
                    {
                        Item item = itemList[i];
                        Console.WriteLine($"- {item.itemEquipped}{item.itemName}\t| {item.itemStatType} +{item.statNum} | {item.itemDesc}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            ActionLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    Equip();
                    break;

                case "0":
                    Console.Clear();
                    break;

                default:
                    WrongInput();
                    Inventory();
                    break;
            }
        }

        public static void Equip()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            if (itemList != null)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemList[i].isBought == true)
                    {
                        int val = i + 1;
                        Console.WriteLine($"- {val} {itemList[i].altogether}");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            ActionLine();

            if (isNum && num >= 1 && num < itemList.Count + 1)
            {
                Item newItem = itemList[num - 1];

                if (newItem.isEquipped)
                {
                    // 이미 장착된 아이템을 해제
                    newItem.isEquipped = false;
                    newItem.itemEquipped = "";
                }
                else
                {
                    // 같은 타입의 이미 장착된 아이템을 해제
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (itemList[i].isEquipped && itemList[i].itemType == newItem.itemType)
                        {
                            Item equippedItem = itemList[i];
                            equippedItem.isEquipped = false;
                            equippedItem.itemEquipped = "";
                            equippedItem.altogether = equippedItem.itemEquipped + equippedItem.itemName + "\t|" + equippedItem.itemStatType +
                                                      " +" + equippedItem.statNum + " | " + equippedItem.itemDesc + "\t |" + equippedItem.goldStr;
                            itemList[i] = equippedItem;
                        }
                    }

                    // 새 아이템 장착
                    newItem.isEquipped = true;
                    newItem.itemEquipped = "[E]";
                }

                newItem.altogether = newItem.itemEquipped + newItem.itemName + "\t|" + newItem.itemStatType +
                                     " +" + newItem.statNum + " | " + newItem.itemDesc + "\t |" + newItem.goldStr;
                itemList[num - 1] = newItem;

                Console.Clear();
                Inventory();
            }
            else if (isNum && num == 0)
            {
                Inventory();
            }
            else
            {
                WrongInput();
            }
        }
      
        public static void Store()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{hero.gold} G");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            
            for (int i = 0; i < itemList.Count; i++)
            {
                Item item = itemList[i];
                if (!item.isEquipped)
                {
                    item.itemEquipped = "";
                }
                else
                {
                    item.itemEquipped = "[E]";
                }

                if (!item.isBought)
                {
                    item.goldStr = $"{item.goldNum} G";
                }
                else
                {
                    item.goldStr = "구매 완료";
                }
                itemList[i] = item;
                Console.WriteLine($"- {itemList[i].altogether}");
            }

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            ActionLine();

            if (isNum)
            {
                num = int.Parse(input);
                Console.Clear();

                switch (num)
                {
                    case 1:
                        BuyItem();
                        break;
                    case 2:
                        SellItem();
                        break;
                    case 0:
                        break;
                    default:
                        WrongInput();
                        Store();
                        break;
                }
            }
            else
            {
                WrongInput();
                Store();
            }
        }
        public static void BuyItem()
        {
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{hero.gold} G");

            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < itemList.Count; i++)
            {
                int val = i + 1;
                Console.WriteLine($"- {val} {itemList[i].altogether}");
            }

            Console.WriteLine("0. 나가기\n"); 
            ActionLine();

            if (isNum)
            {
                num = int.Parse(input);
                Console.Clear();

                if (num >= 1 && num < itemList.Count + 1)
                {
                    if (hero.gold >= itemList[num - 1].goldNum && itemList[num - 1].isBought == false)
                    {
                        hero.gold = hero.gold - itemList[num - 1].goldNum;
                        Item item = itemList[num - 1];
                        item.goldStr = "구매 완료";
                        item.isBought = true;
                        //itemList[num - 1] = item;
                        item.altogether = item.itemName + "\t|" + item.itemStatType + " +" + item.statNum + " | " + item.itemDesc + "\t |" + item.goldStr;
                        itemList[num - 1] = item;
                        Console.WriteLine("구매를 완료했습니다.\n");
                        Store();
                    }
                    else if (itemList[num - 1].goldNum > hero.gold)
                    {
                        Console.WriteLine("Gold가 부족합니다.\n");
                        BuyItem();
                    }
                    else if (itemList[num - 1].isBought == true)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다\n");
                        BuyItem();
                    }
                }
                else if (num == 0)
                {
                    Store();
                }
                else
                {
                    WrongInput();
                    BuyItem();
                }
            }
            else
            {
                WrongInput();
            }
        }

        public static void SellItem()
        {
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드");
            Console.WriteLine($"{hero.gold} G\n");
            Console.WriteLine("[아이템 목록]");
            int val = 0;
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].isBought == true)
                {
                    val = i + 1;
                    Item item = itemList[i];
                    string strSellNum = item.sellNum.ToString("F0");
                    Console.WriteLine($"- {val} {item.itemName}\t|{item.itemStatType} +{item.statNum} | {item.itemDesc}\t| {strSellNum} G");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            ActionLine();

            if (isNum)
            {
                num = int.Parse(input);
                if (num >= 1 && num <= itemList.Count && itemList[num - 1].isBought)
                {
                    Item item = itemList[num - 1];
                    int soldAt = Convert.ToInt32(item.sellNum.ToString("F0"));
                    hero.gold = hero.gold + soldAt;
                    item.isBought = false;
                    item.goldStr = $"{item.goldNum} G";
                    item.isEquipped = false;
                    item.itemEquipped = "";
                    item.altogether = item.itemEquipped + item.itemName + "\t|" + item.itemStatType +
                                      " +" + item.statNum + " | " + item.itemDesc + "\t |" + item.goldStr;
                    itemList[num - 1] = item;
                    Console.Clear();
                    Console.WriteLine($"{item.itemName}을 판매했습니다.\n");
                    Store();
                }
                else if (num == 0)
                {
                    Console.Clear();
                    Store();
                }
                else
                {
                    WrongInput();
                    Store();
                }
                        
            }
            else
            {
                WrongInput();
                Store();
            }

        }
        public static void EnterDungeon()
        {
            int recDef1 = 5;
            int recDef2 = 11;
            int recDef3 = 17;
            int reward1 = 1000;
            int reward2 = 1700;
            int reward3 = 2500;
            string dungeonName = "";
            Random randStat = new Random();
            int initialRandom = randStat.Next(20, 36);

            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine($"1. 쉬운 던전\t| 방어력 {recDef1} 이상 권장");
            Console.WriteLine($"2. 일반 던전\t| 방어력 {recDef2} 이상 권장");
            Console.WriteLine($"3. 어려운 던전\t| 방어력 {recDef3} 이상 권장");
            Console.WriteLine("0. 나가기\n");
            ActionLine();

            if (isNum)
            {
                num = int.Parse(input);
                switch (num)
                {
                    case 1:
                        chosenDef = recDef1;
                        dungeonName = "쉬운 던전";
                        chosenReward = reward1;
                        CalculateDungeon();
                        break;
                    case 2:
                        chosenDef = recDef2;
                        dungeonName = "일반 던전";
                        chosenReward = reward2;
                        CalculateDungeon();
                        break;
                    case 3:
                        chosenDef = recDef3;
                        dungeonName = "어려운 던전";
                        chosenReward = reward3;
                        CalculateDungeon();
                        break;
                    case 0:
                        Console.Clear();
                        break;
                    default:
                        WrongInput();
                        EnterDungeon();
                        break;
                }
            }
            else
            {
                WrongInput();
                EnterDungeon();
            }
        }

        public static void CalculateDungeon()
        {
            Console.Clear();
            Random randChance = new();
            int randomHealth = randChance.Next(20, 36);
            int defenseDiff = hero.defenseTotal - (int)chosenDef;
            randomHealth += defenseDiff;
            float atkMulti = (float)hero.attackTotal * 2f;
            int randomReward = randChance.Next((int)hero.attackTotal, (int)atkMulti);
            int rewardThisTurn = chosenReward += randomReward;

            if (hero.defenseTotal < chosenDef)
            {
                int loseChance = 40;
                int roll = randChance.Next(1, 101);
                if (roll > loseChance) WinDungeon(randomHealth, rewardThisTurn);
                else FailDungeon();
            }
            else
            {
                WinDungeon(randomHealth, rewardThisTurn);
            }
        }

        public static void WinDungeon(int healthReduction, int reward)
        {
            hero.healthTotal -= healthReduction;
            hero.gold += reward;

            WinScreen();
        }

        public static void WinScreen()
        {
            int heroHealthBefore = hero.healthTotal;
            int goldBefore = hero.gold;

            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하합니다!!\n쉬운 던전을 클리어 하였습니다.\n");
            Console.WriteLine($"[탐험 결과]\n체력 {heroHealthBefore} -> {hero.healthTotal}");
            Console.WriteLine($"Gold {goldBefore} G -> {hero.gold} G\n\n0. 나가기\n");
            ActionLine();

            CheckInput();
        }
        public static void CheckInput()
        {
            if (isNum) num = int.Parse(input);
            if (num == 0)
            {
                Console.Clear();
                EnterDungeon();
            }
            else
            {
                WrongInput();
                WinScreen();
            }
        }
        public static void FailDungeon()
        {
            float health = hero.healthTotal;
            health = hero.healthTotal * 0.5f;
            hero.healthTotal = (int)health;
            LevelUp();
            Console.Clear();
            Console.WriteLine($"탐험에 실패하여 체력이 절반 감소했습니다. 남은 체력은 {hero.healthTotal}입니다.\n");
            EnterDungeon();
        }
        public static void LevelUp()
        {
            hero.levelNum++;
            hero.baseAttack += 0.5f;
            hero.baseDefense++;
        }

        public static void Rest()
        {
            hero.healthTotal = 0;
            Console.WriteLine("휴식하기");
            Console.WriteLine($"{restGold} G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {hero.gold} G)\n");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기\n");

            ActionLine();

            if (isNum)
            {
                num = int.Parse(input);
                if (num !=0 && restGold <= hero.gold)
                {
                    hero.gold -= restGold;
                    hero.healthTotal += 100;
                    if (hero.healthTotal > 100)
                    {
                        hero.healthTotal = 100;
                    }
                    Console.Clear();
                    Console.WriteLine("휴식을 완료했습니다.");
                }
                else if (restGold > hero.gold)
                {
                    Console.Clear();
                    Console.WriteLine("Gold 가 부족합니다.");
                }
                else
                {
                    Console.Clear();
                }

            }
            else
            {
                WrongInput();
            }
        }

        public static void WrongInput()
        {
            Console.Clear();
            Console.WriteLine("잘못된 입력입니다.\n");
        }
        public static void ActionLine()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");
            input = Console.ReadLine();
            isNum = int.TryParse(input, out num);
        }

        public static void SaveData()
        {
            string _heroName, _itemName, _goldName, _userDocFolder, _heroPath, _itemPath, _goldPath, _playerJson, _itemJson, _goldJson;
            _heroName = "player.json";
            _itemName = "item.json";
            _userDocFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _heroPath = Path.Combine(_userDocFolder, _heroName);
            _itemPath = Path.Combine(_userDocFolder, _itemName);

            _playerJson = JsonConvert.SerializeObject(hero, Formatting.Indented);
            _itemJson = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            File.WriteAllText(_heroPath, _playerJson);
            File.WriteAllText(_itemPath, _itemJson);
            Console.WriteLine("저장 완료");

        }
        public static void LoadData()
        {
            string _heroName = "player.json";
            string _itemName = "item.json";
            string _userDocFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string _heroPath = Path.Combine(_userDocFolder, _heroName);
            if (File.Exists(_heroPath))
            {
                string _heroJson = File.ReadAllText(_heroPath);
                hero = JsonConvert.DeserializeObject<Hero>(_heroJson);
                Console.WriteLine("플레이어 데이터 불러오기 성공");
            }
            else
            {
                Console.WriteLine("저장된 플레이어 데이터가 없으므로 초기화합니다.");
                //Thread.Sleep(300); //0.3초 대기
                HeroInitialize();
            }

            string _itemPath = Path.Combine(_userDocFolder, _itemName);
            if (File.Exists(_itemPath))
            {
                string _itemJson = File.ReadAllText(_itemPath);
                itemList = JsonConvert.DeserializeObject<List<Item>>(_itemJson);
                Console.WriteLine("아이템 데이터 불러오기 성공\n");
            }
            else
            {
                Console.WriteLine("저장된 아이템 데이터가 없으므로 초기화합니다.\n");
                itemList = new List<Item>();
                ItemInitialize();
            }
        }
    }
}
