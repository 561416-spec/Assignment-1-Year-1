using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RPG_Game
{
    class Room
    // Room Defining
    {
        public string Title;
        public string Description;
        public Room North;
        public Room East;
        public Room South;
        public Room West;
        public Item RoomItem;
        public Enemy RoomEnemy;

        public Item RequiredKey;
        public string BlockedDirection;
        public bool Unlocked = true;

        public Room(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }


    class Player
    {
        public string username;
        public int health;
        public int baseDamage = 5;
        public int tempDamageBoost = 0;
    }

    class Enemy
    {
        public string Name;
        public int Health;
        public int Damage;
        public List<Item> LootTable;

        public Enemy(string name, int health, int damage, List<Item> loot)
        {
            Name = name;
            Health = health;
            Damage = damage;
            LootTable = loot;
        }

        public Item TryDropLoot(Random rng)
        {
            if (LootTable.Count == 0) return null;
            if (rng.Next(0, 100) < 50)
            {
                return LootTable[rng.Next(LootTable.Count)];
            }
            return null;
        }
    }

    class Equipment
    {
        public Item Weapon;
        public Item Shield;
        public Item Armor;

        public void Equip(Item item)
        {
            if (item.Type == ItemType.Weapon) Weapon = item;
            else if (item.Type == ItemType.Shield) Shield = item;
            else if (item.Type == ItemType.Armor) Armor = item;
            else { Console.WriteLine("That item cannot be equipped!"); return; }

            Console.WriteLine($"Equipped {item.Name}.");
        }

        public int GetAttackPower()
        {
            return (Weapon != null ? Weapon.Power : 5);
        }

        public int GetDefense()
        {
            int def = 0;
            if (Shield != null) def += Shield.Power;
            if (Armor != null) def += Armor.Power;
            return def;
        }

        public string GetWeaponName() => Weapon != null ? Weapon.Name : "your fists";
    }


    enum ItemType { Key, Potion, Weapon, Armor, Shield, Misc }

    class Item
    {
        public string Name;
        public string Description;
        public ItemType Type;
        public int Power;

        public Item(string name, string description, ItemType type = ItemType.Misc, int power = 0)
        {
            Name = name;
            Description = description;
            Type = type;
            Power = power;
        }
    }


    class Program
    {
        static Player GamePlayer;
        static List<Item> GameInventory = new List<Item>();
        static Equipment GameEquipment = new Equipment();
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool usernamepicked = false;
            string username = "USERNAME NOT SET";
            while (usernamepicked == false)
            {
                Console.Clear();
                int verticalPadding = 12;
                for (int i = 0; i < verticalPadding; i++)
                    Console.WriteLine();
                PrintCentered("Welcome to my RPG");
                PrintCentered("Input your username below");
                username = Console.ReadLine();
                if (username == "")
                {
                    PrintCentered("Username field cannot be empty", ConsoleColor.Red, 2000);
                }
                else if (username.Length <= 2)
                {
                    PrintCentered("Username has to be over 2 characters", ConsoleColor.Red, 2000);
                }
                else if (username.Length >= 15)
                {
                    PrintCentered("Username has to be less than 15 characters", ConsoleColor.Red, 2000);
                }
                else if (username.Contains(" "))
                {
                    PrintCentered("Username must not contain spaces", ConsoleColor.Red, 2000);
                }
                else
                {
                    usernamepicked = true;
                }
            }
            WelcomeScreenAnimation(username);

            LoadingScreenAnimation();

            GamePlayer = new Player { username = username, health = 100 };

            Room currentRoom = SetUpMap();
            string userChoice = "";

            while (userChoice != "q")
            {
                DescribeRoom(currentRoom);
                Console.Write("> ");
                userChoice = Console.ReadLine().ToLower().Trim();

                switch (userChoice)
                {
                    case "w":
                        currentRoom = TryMove(currentRoom, currentRoom.North, "W");
                        break;
                    case "d":
                        currentRoom = TryMove(currentRoom, currentRoom.East, "D");
                        break;
                    case "s":
                        currentRoom = TryMove(currentRoom, currentRoom.South, "S");
                        break;
                    case "a":
                        currentRoom = TryMove(currentRoom, currentRoom.West, "A");
                        break;
                    case "e":
                        if (currentRoom.RoomItem != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"You pick up {currentRoom.RoomItem.Name}.");
                            GameInventory.Add(currentRoom.RoomItem);
                            currentRoom.RoomItem = null;
                        }
                        else
                        {
                            Console.WriteLine("There is nothing to pick up here.");
                        }
                        break;
                    case "b":
                        Console.Clear();
                        DisplayItems(GameInventory);
                        break;
                    case "f":
                        if (currentRoom.RequiredKey != null && !currentRoom.Unlocked)
                        {
                            Item key = GameInventory.FirstOrDefault(i => i.Name.Equals(currentRoom.RequiredKey.Name, StringComparison.OrdinalIgnoreCase));

                            if (key != null)
                            {
                                Console.WriteLine($"You use the {key.Name}. The path opens.");
                                currentRoom.Unlocked = true;
                                GameInventory.Remove(key);
                            }
                            else
                            {
                                Console.WriteLine("You don't have the required key.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is nothing to unlock here.");
                        }
                        break;
                    case "q":
                        Console.Clear();
                        int verticalPadding = 10;
                        for (int i = 0; i < verticalPadding; i++)
                            Console.WriteLine();
                        PrintCentered("End of game");
                        GameEnd();
                        break;


                    default:
                        WriteColLine("w", ConsoleColor.White, "Forward", ConsoleColor.Cyan);
                        WriteColLine("a", ConsoleColor.White, "Left", ConsoleColor.Cyan);
                        WriteColLine("s", ConsoleColor.White, "Backwards", ConsoleColor.Cyan);
                        WriteColLine("d", ConsoleColor.White, "Right", ConsoleColor.Cyan);
                        WriteColLine("e", ConsoleColor.White, "Get Item", ConsoleColor.Yellow);
                        WriteColLine("f", ConsoleColor.White, "Use Key", ConsoleColor.Yellow);
                        WriteColLine("b", ConsoleColor.White, "Backpack", ConsoleColor.Yellow);
                        WriteColLine("q", ConsoleColor.White, "Quit", ConsoleColor.Red);
                        break;
                }
            }
        }

        static void WelcomeScreenAnimation(string username)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;

            int verticalPadding = 7;
            for (int i = 0; i < verticalPadding; i++)
                Console.WriteLine();

            ConsoleColor bannercolor = ConsoleColor.Cyan;

            PrintCentered("██╗       ██╗███████╗██╗      █████╗  █████╗ ███╗   ███╗███████╗", bannercolor, 80);
            PrintCentered("██║  ██╗  ██║██╔════╝██║     ██╔══██╗██╔══██╗████╗ ████║██╔════╝", bannercolor, 80);
            PrintCentered("╚██╗████╗██╔╝█████╗  ██║     ██║  ╚═╝██║  ██║██╔████╔██║█████╗  ", bannercolor, 80);
            PrintCentered(" ████╔═████║ ██╔══╝  ██║     ██║  ██╗██║  ██║██║╚██╔╝██║██╔══╝  ", bannercolor, 80);
            PrintCentered(" ╚██╔╝ ╚██╔╝ ███████╗███████╗╚█████╔╝╚█████╔╝██║ ╚═╝ ██║███████╗", bannercolor, 80);
            PrintCentered("  ╚═╝   ╚═╝  ╚══════╝╚══════╝ ╚════╝  ╚════╝ ╚═╝     ╚═╝╚══════╝", bannercolor, 200);

            Console.WriteLine();
            Console.WriteLine();

            PrintCentered(username, ConsoleColor.White, 300);
            Console.WriteLine();

            string[] bars = {
            "",
            "     ═════════════     ",
            "",
            "   ═════════════════   ",
            "",
            " ═════════════════════ "
        };

            foreach (var bar in bars)
            {
                PrintCentered(bar, ConsoleColor.DarkGray, 250);
            }

            Thread.Sleep(2000);
            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = true;
        }


        static void LoadingScreenAnimation()
        {
            Console.Clear();
            Console.CursorVisible = false;

            int width = Console.WindowWidth;
            int total = 40;
            int current = 0;
            Random rand = new Random();

            int verticalPadding = (Console.WindowHeight - 10) / 2;
            for (int i = 0; i < verticalPadding; i++)
                Console.WriteLine();

            ConsoleColor bannercolor = ConsoleColor.Cyan;

            PrintCentered("██╗      █████╗  █████╗ ██████╗ ██╗███╗  ██╗ ██████╗ ", bannercolor, 80);
            PrintCentered("██║     ██╔══██╗██╔══██╗██╔══██╗██║████╗ ██║██╔════╝ ", bannercolor, 80);
            PrintCentered("██║     ██║  ██║███████║██║  ██║██║██╔██╗██║██║  ██╗ ", bannercolor, 80);
            PrintCentered("██║     ██║  ██║██╔══██║██║  ██║██║██║╚████║██║  ╚██╗", bannercolor, 80);
            PrintCentered("███████╗╚█████╔╝██║  ██║██████╔╝██║██║ ╚███║╚██████╔╝", bannercolor, 80);
            PrintCentered("╚══════╝ ╚════╝ ╚═╝  ╚═╝╚═════╝ ╚═╝╚═╝  ╚══╝ ╚═════╝ ", bannercolor, 80);
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();

            while (current < total)
            {
                int jump = rand.Next(1, 3);
                current += jump;
                if (current > total)
                    current = total;

                string bar = "[";
                for (int i = 0; i < current; i++)
                {
                    bar += "=";
                }
                for (int i = current; i < total; i++)
                {
                    bar += " ";
                }
                bar += "]";

                int percent = (current * 100) / total;
                string loadingText = $"  {percent,3}%  Loading";

                string fullLine = bar + loadingText;

                int padding = (width - fullLine.Length) / 2;
                if (padding < 0) padding = 0;

                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(fullLine);

                Thread.Sleep(rand.Next(300, 500));
                Console.SetCursorPosition(padding, Console.CursorTop);
            }

            Console.WriteLine();
            Console.WriteLine();

            PrintCentered("[ DONE ] Loading Complete!", ConsoleColor.Green, 2000);

            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = true;
        }


        static Room SetUpMap()
        // Main setup for map and items
        {
            // Floor 1
            Room Entrance = new Room("Castle Entrance", "A grand entryway with a sweeping staircase and banners of old kings");
            Room Storage = new Room("Storage Room", "Dusty shelves filled with broken crates and barrels");
            Room Library = new Room("Library", "Tall shelves with ancient tomes line the walls");
            Room Kitchen = new Room("Kitchen", "The smell of rotten meat and moldy bread fills the air");
            Room DiningHall = new Room("Dining Hall", "A massive oak table dominates the center of the room");
            Room GuardPost1 = new Room("Guard Post", "An abandoned guard post, the knight lies defeated.");
            Room Armory = new Room("Armory", "Racks of weapons line the walls, though most are rusted");
            Room Cellar = new Room("Cellar", "Dark, damp, with dripping water echoing in the distance");
            Room ServantsQuarters = new Room("Servants' Quarters", "Tiny, cramped rooms for the castle staff");
            Room HiddenPassage = new Room("Hidden Passage", "A narrow tunnel carved into the stone wall");
            // Floor 2
            Room Barracks = new Room("Barracks", "Rows of bunks where guards once slept");
            Room TrainingYard = new Room("Training Yard", "Dummies and weapon racks are scattered around");
            Room Chapel = new Room("Chapel", "Faded stained glass depicts forgotten saints");
            Room GuardPost2 = new Room("Guard Station", "The station is silent now, its guardian slain.");
            Room Treasury = new Room("Treasury", "Empty chests and shattered locks litter the room");
            Room Courtyard = new Room("Courtyard", "Open sky above, vines creeping over stone walls");
            Room Kitchen2 = new Room("Upper Kitchen", "Better stocked than the lower one, though abandoned");
            Room Infirmary = new Room("Infirmary", "Dusty beds with rotten bandages");
            Room Prison = new Room("Dungeon Prison", "Rows of cells with rusted bars and bones inside");
            // Floor 3
            Room NobleHall = new Room("Noble Hall", "Marble floors and tall windows");
            Room GuestChambers = new Room("Guest Rooms", "Richly decorated rooms now looted and broken");
            Room Balcony = new Room("Balcony", "A sweeping view of the outer lands");
            Room TreasuryVault = new Room("Vault", "A heavily locked chamber, some gold coins remain");
            Room ThroneHall = new Room("Throne Hall", "A grand hall leading toward the throne");
            Room MusicRoom = new Room("Music Room", "Broken harps and lutes lie scattered");
            Room Study = new Room("Study", "A dusty desk covered in scrolls");
            Room AlchemyLab = new Room("Alchemy Lab", "Bubbling flasks long dried out");
            Room Chapel2 = new Room("Upper Chapel", "More ornate than the lower one");
            Room Garden = new Room("Indoor Garden", "Overgrown plants twist around marble statues");
            // Floor 4
            Room WizardsLibrary = new Room("Wizard's Library", "Books of spells and arcane runes glow faintly");
            Room RitualChamber = new Room("Ritual Room", "Strange runes circle the floor");
            Room SummoningHall = new Room("Summoning Room", "Charred markings cover the stone");
            Room Observatory = new Room("Observatory", "A cracked telescope points at the stars");
            Room Laboratory = new Room("Magical Lab", "Glittering powders coat the tables");
            Room ElementalChamber = new Room("Elemental Room", "Air swirls unnaturally around the room");
            Room SealedRoom = new Room("Sealed Room", "The door bears heavy magical wards");
            Room MirrorRoom = new Room("Mirror Room", "Reflections twist and shift unnaturally");
            // Floor 5
            Room ThroneRoom = new Room("Throne Room", "A grand seat lies shattered at the far end");
            Room RoyalVault = new Room("Royal Vault", "Precious relics once guarded here");
            Room WarRoom = new Room("War Room", "Maps of battles cover the walls");
            Room DragonHall = new Room("Dragon Hall", "The air grows hotter as you approach");
            Room EggChamber = new Room("Egg Chamber", "At last, the dragon's egg rests upon a pedestal, glowing faintly");

            // Floor 1 links
            Entrance.East = Storage; Storage.West = Entrance;
            Entrance.West = Library; Library.East = Entrance;
            Entrance.North = DiningHall; DiningHall.South = Entrance;
            DiningHall.East = Kitchen; Kitchen.West = DiningHall;
            DiningHall.West = GuardPost1; GuardPost1.East = DiningHall;
            GuardPost1.South = Cellar; Cellar.North = GuardPost1;
            Storage.South = ServantsQuarters; ServantsQuarters.North = Storage;
            Library.South = HiddenPassage; HiddenPassage.North = Library;
            // Floor 2 links
            DiningHall.North = Barracks; Barracks.South = DiningHall;
            Barracks.East = TrainingYard; TrainingYard.West = Barracks;
            TrainingYard.North = Chapel; Chapel.South = TrainingYard;
            Chapel.East = GuardPost2; GuardPost2.West = Chapel;
            GuardPost2.North = Treasury; Treasury.South = GuardPost2;
            Treasury.East = Courtyard; Courtyard.West = Treasury;
            Courtyard.South = Infirmary; Infirmary.North = Courtyard;
            Chapel.West = Kitchen2; Kitchen2.East = Chapel;
            Kitchen2.South = Infirmary; Infirmary.North = Kitchen2;
            Infirmary.West = Prison; Prison.East = Infirmary;
            // Floor 3 links
            Treasury.North = NobleHall; NobleHall.South = Treasury;
            NobleHall.East = GuestChambers; GuestChambers.West = NobleHall;
            NobleHall.West = Balcony; Balcony.East = NobleHall;
            NobleHall.North = ThroneHall; ThroneHall.South = NobleHall;
            GuestChambers.North = TreasuryVault; TreasuryVault.South = GuestChambers;
            ThroneHall.North = MusicRoom; MusicRoom.South = ThroneHall;
            MusicRoom.East = Study; Study.West = MusicRoom;
            Study.North = AlchemyLab; AlchemyLab.South = Study;
            Balcony.South = Chapel2; Chapel2.North = Balcony;
            Chapel2.West = Garden; Garden.East = Chapel2;
            // Floor 4 links
            AlchemyLab.North = WizardsLibrary; WizardsLibrary.South = AlchemyLab;
            WizardsLibrary.East = RitualChamber; RitualChamber.West = WizardsLibrary;
            RitualChamber.North = SummoningHall; SummoningHall.South = RitualChamber;
            SummoningHall.East = Observatory; Observatory.West = SummoningHall;
            Observatory.North = Laboratory; Laboratory.South = Observatory;
            Laboratory.East = ElementalChamber; ElementalChamber.West = Laboratory;
            ElementalChamber.North = SealedRoom; SealedRoom.South = ElementalChamber;
            SealedRoom.East = MirrorRoom; MirrorRoom.West = SealedRoom;
            // Floor 5 links
            MirrorRoom.North = ThroneRoom; ThroneRoom.South = MirrorRoom;
            ThroneRoom.East = RoyalVault; RoyalVault.West = ThroneRoom;
            ThroneRoom.North = WarRoom; WarRoom.South = ThroneRoom;
            WarRoom.North = DragonHall; DragonHall.South = WarRoom;
            DragonHall.North = EggChamber; EggChamber.South = DragonHall;


            // Keys
            Item rustyKey = new Item("Rusty Key", "An old, corroded key with flakes of rust.");
            Item bronzeKey = new Item("Bronze Key", "A dull bronze key, heavy but worn smooth.");
            Item silverKey = new Item("Silver Key", "A shiny silver key with fine etchings.");
            Item goldKey = new Item("Gold Key", "A polished golden key that gleams faintly.");
            Item obsidianKey = new Item("Obsidian Key", "A black stone key with a cold, glassy surface.");

            Entrance.RoomItem = rustyKey;
            Prison.RoomItem = bronzeKey;
            Study.RoomItem = silverKey;
            Laboratory.RoomItem = goldKey;
            RoyalVault.RoomItem = obsidianKey;

            DiningHall.RequiredKey = rustyKey; DiningHall.BlockedDirection = "W"; DiningHall.Unlocked = false;
            Treasury.RequiredKey = bronzeKey; Treasury.BlockedDirection = "W"; Treasury.Unlocked = false;
            AlchemyLab.RequiredKey = silverKey; AlchemyLab.BlockedDirection = "W"; AlchemyLab.Unlocked = false;
            MirrorRoom.RequiredKey = goldKey; MirrorRoom.BlockedDirection = "W"; MirrorRoom.Unlocked = false;
            DragonHall.RequiredKey = obsidianKey; DragonHall.BlockedDirection = "W"; DragonHall.Unlocked = false;

            // Items
            Item ironSword = new Item("Iron Sword", "A sturdy iron blade.", ItemType.Weapon, 12);
            Item mace = new Item("Mace", "A heavy spiked weapon.", ItemType.Weapon, 15);
            Item wizardStaff = new Item("Wizard Staff", "A powerful magical staff crackling with energy.", ItemType.Weapon, 20);

            Item lightArmor = new Item("Knight Armor", "light steel mesh that protects slightly.", ItemType.Armor, 8);
            Item heavyArmor = new Item("Heavy Armor", "Thick steel plates that protect well.", ItemType.Armor, 12);
            Item wizardRobe = new Item("Wizard Robe", "An enchanted robe that has magical power.", ItemType.Armor, 15);

            Item woodenShield = new Item("Wooden Shield", "Basic protection.", ItemType.Shield, 3);
            Item steelShield = new Item("Steel Shield", "Strong steel shield.", ItemType.Shield, 8);


            Item healthPotion = new Item("Health Potion", "Increases Health by 50 HP.", ItemType.Potion, 50);
            Item damagePotion = new Item("Damage Potion", "Increases damage by +10 for next hit.", ItemType.Potion, 10);


            Storage.RoomItem = ironSword;
            GuardPost1.RoomItem = lightArmor;
            Armory.RoomItem = woodenShield;

            Library.RoomItem = new Item("Ancient Tome", "Contains forgotten spells.");
            GuestChambers.RoomItem = new Item("Lockpick Set", "Useful for breaking into doors.");
            WizardsLibrary.RoomItem = new Item("Crystal Orb", "Hums with magical energy.");
            RitualChamber.RoomItem = new Item("Cursed Amulet", "It whispers dark thoughts.");
            ThroneRoom.RoomItem = new Item("King Crown", "A fallen king's crown.");
            WarRoom.RoomItem = new Item("Battle Plans", "Hints at enemy weaknesses.");

            Kitchen.RoomItem = healthPotion;
            AlchemyLab.RoomItem = damagePotion;
            RoyalVault.RoomItem = steelShield;

            EggChamber.RoomItem = new Item("Dragon Egg", "The final treasure you sought!");

            // Enemies
            Enemy knight = new Enemy("Knight", 50, 12, new List<Item> { ironSword, lightArmor, woodenShield });
            Enemy heavyknight = new Enemy("Heavy Knight", 50, 12, new List<Item> { mace, heavyArmor, steelShield });
            Enemy wizard = new Enemy("Wizard", 50, 12, new List<Item> { wizardStaff, wizardRobe, damagePotion });

            GuardPost1.RoomEnemy = knight;
            GuardPost2.RoomEnemy = heavyknight;
            AlchemyLab.RoomEnemy = wizard;

            return Entrance;
        }


        static void DescribeRoom(Room room)
        {
            Console.WriteLine();
            Console.WriteLine(room.Title);

            Console.WriteLine("".PadLeft(room.Title.Length), '-');
            Console.WriteLine(room.Description);

            if (room.RoomItem != null)
            {
                Console.WriteLine($"You found {room.RoomItem.Name}!");

                if (room.RoomItem.Name == "Dragon Egg")
                {
                    EndingScreen();
                    return;
                }

                GameInventory.Add(room.RoomItem);
                room.RoomItem = null;
            }

            if (room.RequiredKey != null && !room.Unlocked)
            {
                string direction = room.BlockedDirection != null ? room.BlockedDirection.ToUpper() : "";
                string directiontext = direction == "W" ? "north" : direction == "A" ? "west" : direction == "S" ? "south" : direction == "D" ? "east" : "the way";
                Console.WriteLine($"(Locked) The path to the {directiontext} is blocked. Press [f] to try a key.");
            }
            if (room.RoomEnemy != null)
            {
                Console.WriteLine($"You see a {room.RoomEnemy.Name} here!");

                string choice = "";
                while (choice != "1" && choice != "2")
                {
                    Console.WriteLine("[1] Fight   [2] Leave");
                    choice = Console.ReadLine().Trim();

                    if (choice == "1")
                    {
                        bool survived = FightEnemy(GamePlayer, GameInventory, GameEquipment, room.RoomEnemy);
                        if (survived)
                        {
                            Item dropped = room.RoomEnemy.TryDropLoot(new Random());
                            if (dropped != null)
                            {
                                Console.WriteLine($"The {room.RoomEnemy.Name} dropped {dropped.Name}!");
                                room.RoomItem = dropped;
                            }
                            room.RoomEnemy = null;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine($"You decide to avoid the {room.RoomEnemy.Name} and retreat...");
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option! Please choose 1 or 2.");
                        Console.ResetColor();
                    }
                }
            }


            Console.WriteLine("".PadLeft(room.Title.Length), '-');

            Console.WriteLine("Exits: {0}{1}{2}{3}\n",
               room.North == null ? "" : "↑ ",
               room.South == null ? "" : "↓ ",
               room.West == null ? "" : "← ",
               room.East == null ? "" : "→ ");
        }


        static void DisplayItems(List<Item> playerItems)
        // list players inventory
        {
            if (playerItems.Count > 0)
            {
                Console.WriteLine("Your inventory");
                foreach (Item item in playerItems)
                {
                    Console.WriteLine("You have {0}. {1}.", item.Name, item.Description);
                }
            }
            else
            {
                Console.WriteLine("Your inventory is empty.");
            }
        }


        static void WriteColLine(string key, ConsoleColor keyCol, string text, ConsoleColor textCol)
        {
            Console.ForegroundColor = keyCol;
            Console.Write($"[{key}] ");
            Console.ForegroundColor = textCol;
            Console.WriteLine(text);
            Console.ResetColor();
        }


        static void PrintCentered(string text, ConsoleColor col = ConsoleColor.Gray, int delay = 0)
        {
            int width = Console.WindowWidth;
            Console.ForegroundColor = col;
            int position = (width - text.Length) / 2;
            Console.SetCursorPosition(Math.Max(0, position), Console.CursorTop);
            Console.WriteLine(text);
            if (delay > 0) Thread.Sleep(delay);
        }


        static Room TryMove(Room current, Room next, string direction)
        {
            if (next == null)
            {
                Console.WriteLine("You cannot go that way.");
                return current;
            }

            if (current.RequiredKey != null && !current.Unlocked)
            {
                if (string.Equals(current.BlockedDirection, direction, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("This way is locked. Press [f] to try a key.");
                    return current;
                }
            }

            Console.Clear();
            return next;
        }


        static bool FightEnemy(Player player, List<Item> inventory, Equipment equipment, Enemy enemy)
        {
            Random rng = new Random();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            Console.WriteLine($"A {enemy.Name} appears!");

            while (enemy.Health > 0 && player.health > 0)
            {
                Console.WriteLine($"\n{enemy.Name} HP: {enemy.Health} | {player.username} HP: {player.health}");
                Console.WriteLine("[1] Attack   [2] Drink Potion   [3] Run");

                string choice = Console.ReadLine().Trim();
                if (choice == "1")
                {
                    int dmg = equipment.GetAttackPower() + player.tempDamageBoost;
                    enemy.Health -= dmg;
                    Console.WriteLine($"You strike with {equipment.GetWeaponName()} for {dmg} damage!");

                    if (player.tempDamageBoost > 0)
                    {
                        Console.WriteLine("Your strength boost fades...");
                        player.tempDamageBoost = 0;
                    }
                }
                else if (choice == "2")
                {
                    DrinkPotion(player, inventory);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("You flee!");
                    return true;
                }

                if (enemy.Health > 0)
                {
                    int enemyDmg = enemy.Damage - equipment.GetDefense();
                    if (enemyDmg < 0) enemyDmg = 0;
                    player.health -= enemyDmg;
                    Console.WriteLine($"{enemy.Name} hits you for {enemyDmg} damage!");
                }
            }

            if (player.health > 0)
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
                return true;
            }
            else
            {
                Console.WriteLine("\nYou were slain...");
                GameOver();
                return false;
            }
        }


        static void DrinkPotion(Player player, List<Item> inventory)
        {
            var potions = inventory.FindAll(i => i.Type == ItemType.Potion);

            if (potions.Count == 0)
            {
                Console.WriteLine("You have no potions to drink.");
                return;
            }

            Console.WriteLine("Choose a potion to drink:");
            for (int i = 0; i < potions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {potions[i].Name} - {potions[i].Description}");
            }
            Console.WriteLine("0. Cancel");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= potions.Count)
            {
                Item potion = potions[choice - 1];

                if (potion.Name.Contains("Health"))
                {
                    player.health += potion.Power;
                    Console.WriteLine($"{player.username} drinks a {potion.Name} and restores {potion.Power} HP!");
                }
                else if (potion.Name.Contains("Strength"))
                {
                    player.tempDamageBoost += potion.Power;
                    Console.WriteLine($"{player.username} drinks a {potion.Name} and gains +{potion.Power} attack for the next strike!");
                }

                inventory.Remove(potion);
            }
        }


        static void GameEnd()
        {
            Console.Clear();
            int verticalPadding = 10;
            for (int i = 0; i < verticalPadding; i++)
                Console.WriteLine();
            PrintCentered("End of game");
            Console.WriteLine();
            Console.WriteLine();
            PrintCentered(@"   |\                     /)    ");
            PrintCentered(@" /\_\\__               (_//     ");
            PrintCentered(@"|   `>\-`     _._       //`)    ");
            PrintCentered(@" \ /` \\  _.-`:::`-._  //       ");
            PrintCentered(@"  `    \|`    :::    `|/        ");
            PrintCentered(@"        |     :::     |         ");
            PrintCentered(@"        |.....:::.....|         ");
            PrintCentered(@"        |:::::::::::::|         ");
            PrintCentered(@"        |     :::     |         ");
            PrintCentered(@"        \     :::     /         ");
            PrintCentered(@"         \    :::    /          ");
            PrintCentered(@"          `-. ::: .-'           ");
            PrintCentered(@"           //`:::`\\            ");
            PrintCentered(@"          //   '   \\           ");
            PrintCentered(@"         |/         \\          ");
        }


        static void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
                                .                                                      .
                              .n                   .                 .                  n.
                        .   .dP                  dP                   9b                 9b.    .
                       4    qXb         .       dX                     Xb       .        dXp     t
                      dX.    9Xb      .dXb    __                         __    dXb.     dXP     .Xb
                      9XXb._       _.dXXXXb dXXXXbo.                 .odXXXXb dXXXXb._       _.dXXP
                       9XXXXXXXXXXXXXXXXXXXVXXXXXXXXOo.           .oOXXXXXXXXVXXXXXXXXXXXXXXXXXXXP
                        `9XXXXXXXXXXXXXXXXXXXXX'~   ~`OOO8b   d8OOO'~   ~`XXXXXXXXXXXXXXXXXXXXXP'
                          `9XXXXXXXXXXXP' `9XX'          `98v8P'          `XXP' `9XXXXXXXXXXXP'
                              ~~~~~~~       9X.          .db|db.          .XP       ~~~~~~~
                                              )b.  .dbo.dP'`v'`9b.odb.  .dX(
                                            ,dXXXXXXXXXXXb     dXXXXXXXXXXXb.
                                           dXXXXXXXXXXXP'   .   `9XXXXXXXXXXXb
                                          dXXXXXXXXXXXXb   d|b   dXXXXXXXXXXXXb
                                          9XXb'   `XXXXXb.dX|Xb.dXXXXX'   `dXXP
                                           `'      9XXXXXX(   )XXXXXXP      `'
                                                    XXXX X.`v'.X XXXX
                                                    XP^X'`b   d'`X^XX
                                                    X. 9  `   '  P )X
                                                    `b  `       '  d'
                                                     `             '           
");
            PrintCentered("GAME OVER");
            PrintCentered("1. Restart");
            PrintCentered("2. Quit");
            Console.ForegroundColor = ConsoleColor.White;

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Main(new string[] { });
            }
            else
            {
                Environment.Exit(0);
            }
        }


        static void EndingScreen()
        {
            Console.Clear();
            Thread.Sleep(1000);
            int verticalPadding = 10;
            for (int i = 0; i < verticalPadding; i++)
                Console.WriteLine();
            ConsoleColor bannercolor = ConsoleColor.Cyan;
            PrintCentered(@".                                                     ", bannercolor, 80);
            PrintCentered(@"                  .       |         .    .            ", bannercolor, 80);
            PrintCentered(@"            .  *         -*-          *               ", bannercolor, 80);
            PrintCentered(@"                 \        |         /   .             ", bannercolor, 80);
            PrintCentered(@".    .            .      /^\     .              .    .", bannercolor, 80);
            PrintCentered(@"   *    |\   /\    /\  / / \ \  /\    /\   /|    *    ", bannercolor, 80);
            PrintCentered(@" .   .  |  \ \/ /\ \ / /     \ \ / /\ \/ /  | .     . ", bannercolor, 80);
            PrintCentered(@"         \ | _ _\/_ _ \_\_ _ /_/_ _\/_ _ \_/          ", bannercolor, 80);
            PrintCentered(@"           \  *  *  *   \ \/ /  *  *  *  /            ", bannercolor, 80);
            PrintCentered(@"            ` ~ ~ ~ ~ ~  ~\/~ ~ ~ ~ ~ ~ '             ", bannercolor, 80);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            PrintCentered("██╗   ██╗██╗ █████╗ ████████╗ █████╗ ██████╗ ██╗   ██╗ ██╗", bannercolor, 80);
            PrintCentered("██║   ██║██║██╔══██╗╚══██╔══╝██╔══██╗██╔══██╗╚██╗ ██╔╝ ██║", bannercolor, 80);
            PrintCentered("╚██╗ ██╔╝██║██║  ╚═╝   ██║   ██║  ██║██████╔╝ ╚████╔╝  ██║", bannercolor, 80);
            PrintCentered(" ╚████╔╝ ██║██║  ██╗   ██║   ██║  ██║██╔══██╗  ╚██╔╝   ╚═╝", bannercolor, 80);
            PrintCentered("  ╚██╔╝  ██║╚█████╔╝   ██║   ╚█████╔╝██║  ██║   ██║    ██╗", bannercolor, 80);
            PrintCentered("   ╚═╝   ╚═╝ ╚════╝    ╚═╝    ╚════╝ ╚═╝  ╚═╝   ╚═╝    ╚═╝", bannercolor, 3000);
            GameOver();
        }
    }
}
