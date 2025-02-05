/*namespace SpartaDungeon
{
    internal class ItemScript
    {
        public class Item
        {
            public string altogether;
            public string equipStr, nameStr, statTypeStr, descStr, boughtStr, goldStr;
            public int statNum, goldNum;
            public bool isEquipped, isBought;
            public float sellNum;

            public static List<Item> itemList = new List<Item>();
            public static Item item;

            public static void InitializeItem()
            {
                itemList.AddRange(new List<Item>
                {
                    #region 수련자 갑옷
                    new Item
                    {
                        nameStr = "수련자 갑옷",
                        statTypeStr = "방어력",
                        descStr = "수련에 도움을 주는 갑옷입니다.",
                        statNum = 5,
                        goldNum = 1000
                    },
                    #endregion
                    #region 무쇠갑옷
                    new Item
                    {
                        nameStr = "무쇠갑옷",
                        statTypeStr = "방어력",
                        descStr = "무쇠로 만들어져 튼튼한 갑옷입니다.",
                        statNum = 9,
                        goldNum = 2117
                    },
                    #endregion
                    #region 스파르타의 갑옷
                    new Item
                    {
                        nameStr = "스파르타의 갑옷",
                        statTypeStr = "방어력",
                        descStr = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                        statNum = 15,
                        goldNum = 3500
                    },
                    #endregion
                    #region 낡은 검
                    new Item
                    {
                        nameStr = "낡은 검",
                        statTypeStr = "공격력",
                        descStr = "쉽게 볼 수 있는 낡은 검 입니다.",
                        statNum = 2,
                        goldNum = 600
                    },
                    #endregion
                    #region 청동 도끼
                    new Item
                    {
                        nameStr = "청동 도끼",
                        statTypeStr = "공격력",
                        descStr = "어디선가 사용됐던거 같은 도끼입니다.",
                        statNum = 5,
                        goldNum = 1500
                    },
                    #endregion
                    #region 스파르타의 창
                    new Item
                    {
                        nameStr = "스파르타의 창",
                        statTypeStr = "공격력",
                        descStr = "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                        statNum = 7,
                        goldNum = 3176
                    }
                    #endregion
                });

                for (int i = 0; i < itemList.Count; i++)
                {
                    item = itemList[i];
                    item.isEquipped = false;
                    item.equipStr = "";
                    item.isBought = false;
                    item.sellNum = (int)(item.goldNum * 0.85f);
                    item.goldStr = $"{item.goldNum} G";
                    item.altogether = $"- {item.equipStr}{item.nameStr}\t|{item.statTypeStr} +{item.statNum} | {item.descStr}\t |{item.goldStr}";
                    itemList[i] = item;
                }
            }

            public static void EquipItem()
            {
                item.equipStr = item.isEquipped ? "[E]" : "";
            }
        }
    }
}*/
