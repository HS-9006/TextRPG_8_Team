using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_8_Team
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType type {get; set;}
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int MaxHP { get; set; }
        public int Price { get; set; }


        // 역직렬화를 위한 기본 생성자 이걸 해놓아야 저장 데이터가 로드 됨
        public Item() { }


        public Item(string name, ItemType type, int atk, int def, int HP, int price)
        {
            Name = name;
            this.type = type;
            AttackBonus = atk;
            DefenseBonus = def;
            MaxHP = HP;
            Price = price;
        }
        public override string ToString()
        {
            return $"{Name} (공격력 +{AttackBonus}, 방어력 +{DefenseBonus}, 최대체력 +{MaxHP}, 가격: {Price}G)";
        }
    }
}

public enum ItemType
{
    Weapon = 1,
    Armor
}