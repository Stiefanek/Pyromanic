using MelonLoader;
using Il2CppAssets.Scripts.Models.Towers;
using PyroTower;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper;
using Il2CppAssets.Scripts.Models.TowerSets;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2Cpp;
using Il2CppAssets.Scripts.Unity.Display;
using weapondisplays;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Utils;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using System.Linq;
using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using PyroWorkPLS;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using static PyroTower.Main;
using PyroTower.PYM2.PYM1;
using PyroTower.PYM2;
using static Il2CppTMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using PyroTower.PYM2.PYM1.PYM3;
using PyroTower.PYM2.PYM1.PYM3.PYM4;
using PyroTower.PYM2.PYM1.PYM3.PYM4.PYM5;

[assembly: MelonInfo(typeof(PyroTower.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]






namespace weapondisplays
{
    public class BananaDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "ScientistBuffIcon");
        }
    }
}

namespace PyroTower
{
    internal class Main : BloonsMod
    {
        public class PyroManic : ModTower
        {
            public override string Name => nameof(PyroManic);


            public override string DisplayName => "Pyromanic";

            public override string Description => "The Dad of Gwen, a powerful fire user and extremely insane engineer";

            public override string BaseTower => "DartMonkey";

            public override int Cost => 900;

            public override int TopPathUpgrades => 5;

            public override int MiddlePathUpgrades => 5;

            public override int BottomPathUpgrades => 5;

            public override TowerSet TowerSet => TowerSet.Magic;
            public override bool IsValidCrosspath(int[] tiers) => ModHelper.HasMod("UltimateCrosspathing") ? true : base.IsValidCrosspath(tiers);


            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {
                towerModel.RemoveBehaviors<AttackModel>();
                towerModel.AddBehavior(Game.instance.model.GetTowerFromId("Gwendolin").GetBehavior<AttackModel>().Duplicate());
                towerModel.display = new PrefabReference() { guidRef = "dcdf2fe8b1b8c8c45a7cd6dcb485e4c1" };
                towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "dcdf2fe8b1b8c8c45a7cd6dcb485e4c1" };

            }

            public override string Icon => "MonkeyIcons[GwendolinIcon]";
            public override string Portrait => "83395332c36e7104e818daa4f653a032";



        }

        public class Upgrade100 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Boom";

            public override string Description => "Ooooh Bomby!!";

            public override int Cost => 180;

            public override int Path => 0;

            public override int Tier => 1;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    var attackModel = towerModel.GetAttackModel();
                    var projectile = attackModel.weapons[0].projectile;
                    projectile.GetDamageModel().damage += 25;
                    towerModel.range += 15;
                    attackModel.range += 15;



                    if (towerModel.tier == 1)
                    {
                        towerModel.GetWeapon().emission = new InstantDamageEmissionModel("InstantDamageEmissionModel_", null);
                        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-200").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
                        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-200").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
                        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-200").GetAttackModel().weapons[0].projectile.GetBehavior<CreateSoundOnProjectileCollisionModel>().Duplicate());
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 15.0f;
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce = 85.0f;
                        // attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 1.0f;
                        weaponModel.projectile.GetDamageModel().immuneBloonProperties = BloonProperties.None;
                        weaponModel.projectile.pierce = 1;

                        projectile.GetBehavior<TravelStraitModel>().Lifespan = 0.01f;
                    }
                }

            }

            public override string Icon => "T12";

            public override string Portrait => "T12";
        }

        public class Upgrade200 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "BiggerBoom";

            public override string Description => "gains the ability to push bloons back on hit";

            public override int Cost => 440;

            public override int Path => 0;

            public override int Tier => 2;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                foreach (var weaponModel in towerModel.GetWeapons())
                {
                    var attackModel = towerModel.GetAttackModel();
                    var projectile = attackModel.weapons[0].projectile;
                    projectile.GetDamageModel().damage += 10;
                    towerModel.range += 20;
                    attackModel.range += 20;



                    if (towerModel.tier == 2)
                    {
                        towerModel.GetWeapon().emission = new InstantDamageEmissionModel("InstantDamageEmissionModel_", null);
                        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-300").GetAttackModel().weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
                        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-300").GetAttackModel().weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().Duplicate());
                        attackModel.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-300").GetAttackModel().weapons[0].projectile.GetBehavior<CreateSoundOnProjectileCollisionModel>().Duplicate());
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage = 25.0f;
                        attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.pierce = 100.0f;
                        // attackModel.weapons[0].projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.radius = 1.0f;
                        weaponModel.projectile.pierce = 3;

                        projectile.GetBehavior<TravelStraitModel>().Lifespan = 0.01f;
                    }
                }

            }

            public override string Icon => "T12";

            public override string Portrait => "T12";
        }

        public class Upgrade300 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Even Great Bombs!";

            public override string Description => " Throws Bigger Bombs";

            public override int Cost => 780;

            public override int Path => 0;

            public override int Tier => 3;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var Damage = attackModel.weapons[0].projectile;

                Damage.GetBehavior<DamageModel>().damage += 8;
            }

            public override string Icon => "T3";

            public override string Portrait => "T3";
        }

        public class Upgrade400 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "OH GOD ITS A BOOM";

            public override string Description => "The Training From Master Flame allows Pyromanic to destroy every bloon.\r\n";

            public override int Cost => 9730;

            public override int Path => 0;

            public override int Tier => 4;

            public override void ApplyUpgrade(TowerModel towerModel)
            {

                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var Damage = attackModel.weapons[0].projectile;

                Damage.GetBehavior<DamageModel>().damage += 10;
                Damage.pierce += 30;
            }

            public override string Icon => "T4";

            public override string Portrait => "T4";
        }

        public class Upgrade500 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Grenades";

            public override string Description => "Doesn't get more explosive than this!";

            public override int Cost => 96690;

            public override int Path => 0;

            public override int Tier => 5;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var Damage = attackModel.weapons[0].projectile;

                Damage.GetBehavior<DamageModel>().damage += 2;
                Damage.pierce += 15;

            }

            public override string Icon => "T5";

            public override string Portrait => "T5";
        }

        public class Upgrade010 : ModUpgrade<PyroManic>
        {
            public override string DisplayName => "Flamethrower";

            public override string Description => "Pyromanic obtains a working Flamethrower to melt the bloons and also Camo!";

            public override int Cost => 300;

            public override int Path => 1;

            public override int Tier => 1;

            public override void ApplyUpgrade(TowerModel towerModel)
            {

                towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
            }

            public override string Icon => "t1";

            public override string Portrait => "t1";
        }

        public class Upgrade020 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Better Flames";

            public override string Description => "The flames become hotter and more powerful";

            public override int Cost => 1500;

            public override int Path => 1;

            public override int Tier => 2;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var Proj = attackModel.weapons[0].projectile;


                Proj.AddBehavior(new DamageModel("DamageModel_", 20, 20, true, true, true, BloonProperties.None, BloonProperties.None));
            }

            public override string Icon => "T12";

            public override string Portrait => "T12";
        }

        public class Upgrade030 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Blue Flames";

            public override string Description => "The flames burn at 3000 degrees fahrenheit, increases range and damage";

            public override int Cost => 1560;

            public override int Path => 1;

            public override int Tier => 3;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                var Damage = attackModel.weapons[0].projectile;

                Damage.GetBehavior<DamageModel>().damage = 20;

            }

            public override string Icon => "T3";

            public override string Portrait => "T3";
        }

        public class Upgrade040 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Consuming Flames";

            public override string Description => "The flames do WAY more damage";

            public override int Cost => 5120;

            public override int Path => 1;

            public override int Tier => 4;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>();
                attackModel.weapons[0].projectile.pierce += 3;
                var bouncy = Game.instance.model.GetTowerFromId("SniperMonkey-030").Duplicate<TowerModel>().GetBehavior<AttackModel>().weapons[0].projectile.GetBehavior<RetargetOnContactModel>();
                bouncy.distance = 100;
                attackModel.weapons[0].projectile.AddBehavior(bouncy);
                var Damage = attackModel.weapons[0].projectile;

                Damage.GetBehavior<DamageModel>().damage += 10;
            }

            public override string Icon => "T4";

            public override string Portrait => "T4";
        }

        public class Upgrade050 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "World on Fire";

            public override string Description => "No one knows how...";

            public override int Cost => 22500;

            public override int Path => 1;

            public override int Tier => 5;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                AttackModel attackModel = towerModel.GetBehavior<AttackModel>(); ;
                var Damage = attackModel.weapons[0].projectile;

                Damage.GetBehavior<DamageModel>().damage = 20;
                attackModel.range = 200;
                towerModel.range = 200;

                var ability = Game.instance.model.GetTowerFromId("AdmiralBrickell 3").Duplicate<TowerModel>().GetBehavior<AbilityModel>();
                ability.GetBehavior<ActivateRateSupportZoneModel>().filters[0] = new FilterInBaseTowerIdModel("lol", new string[] { "DartMonkey", "MortarMonkey" });
                towerModel.AddBehavior(ability);
                ability.livesCost = 20;

            }

            public override string Icon => "T5";

            public override string Portrait => "T5";
        }

        public class Upgrade001 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Burning Minions";

            public override string Description => "These minions haven’t been used in a long time, treat them well";

            public override int Cost => 530;

            public override int Path => 2;

            public override int Tier => 1;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel1 = towerModel.GetAttackModel();
                var farm1 = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel().Duplicate();
                farm1.range = towerModel.range;
                farm1.name = "Farm_Weapon";
                farm1.weapons[0].Rate = 15f;
                farm1.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                farm1.weapons[0].projectile.ApplyDisplay<BananaDisplay>();
                farm1.weapons[0].projectile.AddBehavior(new CreateTowerModel("BananaFarm000place", GetTowerModel<PYML1>().Duplicate(), 0f, true, false, false, true, true));
                towerModel.AddBehavior(farm1);
            }

            public override string Icon => "T12";

            public override string Portrait => "T12";
        }

        public class Upgrade002 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Hotter minions";

            public override string Description => "Level up! Minions become more powerful ";

            public override int Cost => 1050;

            public override int Path => 2;

            public override int Tier => 2;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel2 = towerModel.GetAttackModel();
                var farm2 = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel().Duplicate();
                farm2.range = towerModel.range;
                farm2.name = "Farm_Weapon";
                farm2.weapons[0].Rate = 15f;
                farm2.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                farm2.weapons[0].projectile.ApplyDisplay<BananaDisplay>();
                farm2.weapons[0].projectile.AddBehavior(new CreateTowerModel("BananaFarm000place", GetTowerModel<PYML2>().Duplicate(), 0f, true, false, false, true, true));
                towerModel.AddBehavior(farm2);
            }

            public override string Icon => "T12";

            public override string Portrait => "T12";
        }

        public class Upgrade003 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Loss of control";

            public override string Description => "Minions gained a mind of their own";

            public override int Cost => 1260;

            public override int Path => 2;

            public override int Tier => 3;

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.range += 30;
                towerModel.GetAttackModel().range += 30;


                var attackModel3 = towerModel.GetAttackModel();
                var farm3 = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel().Duplicate();
                farm3.range = towerModel.range;
                farm3.name = "Farm_Weapon";
                farm3.weapons[0].Rate = 15f;
                farm3.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                farm3.weapons[0].projectile.ApplyDisplay<BananaDisplay>();
                farm3.weapons[0].projectile.AddBehavior(new CreateTowerModel("BananaFarm000place", GetTowerModel<PYML3>().Duplicate(), 0f, true, false, false, true, true));
                towerModel.AddBehavior(farm3);
            }

            public override string Icon => "T3";

            public override string Portrait => "T3";
        }

        public class Upgrade004 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Minions of Damnation";

            public override string Description => "Converts to a powerful energy blaster; explosions pierce with each hit.";

            public override int Cost => 8020;

            public override int Path => 2;

            public override int Tier => 4;

            public override void ApplyUpgrade(TowerModel towerModel)
            {

                var attackModel4 = towerModel.GetAttackModel();
                var farm4 = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel().Duplicate();
                farm4.range = towerModel.range;
                farm4.name = "Farm_Weapon";
                farm4.weapons[0].Rate = 15f;
                farm4.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                farm4.weapons[0].projectile.ApplyDisplay<BananaDisplay>();
                farm4.weapons[0].projectile.AddBehavior(new CreateTowerModel("BananaFarm000place", GetTowerModel<PYML4>().Duplicate(), 0f, true, false, false, true, true));
                towerModel.AddBehavior(farm4);

            }

            public override string Icon => "T4";

            public override string Portrait => "T4";
        }

        public class Upgrade005 : ModUpgrade<PyroManic>
        {

            public override string DisplayName => "Minions of Chaos";

            public override string Description => "Minions created in the depths of Chaos, NOTHING CAN ESCAPE \r\n";

            public override int Cost => 200000;

            public override int Path => 2;

            public override int Tier => 5;

            public override void ApplyUpgrade(TowerModel towerModel)
            {

                var attackModel5 = towerModel.GetAttackModel();
                var farm5 = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel().Duplicate();
                farm5.range = towerModel.range;
                farm5.name = "Farm_Weapon";
                farm5.weapons[0].Rate = 15f;
                farm5.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                farm5.weapons[0].projectile.ApplyDisplay<BananaDisplay>();
                farm5.weapons[0].projectile.AddBehavior(new CreateTowerModel("BananaFarm000place", GetTowerModel<PYML5>().Duplicate(), 0f, true, false, false, true, true));
                towerModel.AddBehavior(farm5);
            }

            public override string Icon => "T5";

            public override string Portrait => "T5";
        }
    }
    namespace PYM2
    {
        public class PYML2 : ModTower
        {
            public override string Portrait => "MonkeyIcons[GwendolinIcon]";
            public override string Name => "Pyromanic Minion Level 2";
            public override TowerSet TowerSet => TowerSet.Support;
            public override string BaseTower => "Gwendolin 7";

            public override bool DontAddToShop => true;
            public override int Cost => 0;

            public override int TopPathUpgrades => 0;
            public override int MiddlePathUpgrades => 0;
            public override int BottomPathUpgrades => 0;


            public override string DisplayName => "Pyromanic Minion Level 2";
            public override string Description => "WOOO Level 2!!";

            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {
                var attackModel = towerModel.GetBehavior<AttackModel>();
                towerModel.isSubTower = true;
                towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
                attackModel.range = 40;
                towerModel.range = 40;
                var projectile = attackModel.weapons[0].projectile;
                projectile.GetDamageModel().damage += 10;

            }
            public class PYML2Display : ModTowerDisplay<PYML2>
            {
                public override float Scale => .6f;
                public override string BaseDisplay => GetDisplay(TowerType.Gwendolin);

                public override bool UseForTower(int[] tiers)
                {
                    return true;
                }
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {

                }
            }

        }

        namespace PYM1
        {
            public class PYML1 : ModTower
            {
                public override string Portrait => "MonkeyIcons[GwendolinIcon]";
                public override string Name => "Pyromanic Minions";
                public override TowerSet TowerSet => TowerSet.Support;
                public override string BaseTower => "Gwendolin 3";

                public override bool DontAddToShop => true;
                public override int Cost => 0;

                public override int TopPathUpgrades => 0;
                public override int MiddlePathUpgrades => 0;
                public override int BottomPathUpgrades => 0;


                public override string DisplayName => "Pyromanic Minions Level 1";
                public override string Description => "a minion???";

                public override void ModifyBaseTowerModel(TowerModel towerModel)
                {
                    var attackModel = towerModel.GetBehavior<AttackModel>();
                    towerModel.isSubTower = true;
                    towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
                    attackModel.range = 45;
                    towerModel.range = 45;
                    var projectile = attackModel.weapons[0].projectile;
                    projectile.GetDamageModel().damage += 5;


                }
                public class PYML1Display : ModTowerDisplay<PYML1>
                {
                    public override float Scale => .4f;
                    public override string BaseDisplay => GetDisplay(TowerType.Gwendolin);

                    public override bool UseForTower(int[] tiers)
                    {
                        return true;
                    }
                    public override void ModifyDisplayNode(UnityDisplayNode node)
                    {

                    }
                }

            }
            namespace PYM3
            {
                public class PYML3 : ModTower
                {
                    public override string Portrait => "MonkeyIcons[GwendolinIcon]";
                    public override string Name => "Pyromanic Minion Level 3";
                    public override TowerSet TowerSet => TowerSet.Support;
                    public override string BaseTower => "Gwendolin 10";


                    public override bool DontAddToShop => true;
                    public override int Cost => 0;

                    public override int TopPathUpgrades => 0;
                    public override int MiddlePathUpgrades => 0;
                    public override int BottomPathUpgrades => 0;


                    public override string DisplayName => "Pyromanic Minion Level 3";
                    public override string Description => "so uh lets think about this you might not wanna upgrade this anymore";

                    public override void ModifyBaseTowerModel(TowerModel towerModel)
                    {
                        var attackModel = towerModel.GetBehavior<AttackModel>();
                        towerModel.isSubTower = true;
                        towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
                        attackModel.range = 50;
                        towerModel.range = 50;
                        var projectile = attackModel.weapons[0].projectile;
                        projectile.GetDamageModel().damage += 25;


                    }
                    public class PYML3Display : ModTowerDisplay<PYML3>
                    {
                        public override float Scale => 1f;
                        public override string BaseDisplay => GetDisplay(TowerType.Gwendolin);

                        public override bool UseForTower(int[] tiers)
                        {
                            return true;
                        }
                        public override void ModifyDisplayNode(UnityDisplayNode node)
                        {

                        }
                    }

                }
                namespace PYM4
                {
                    public class PYML4 : ModTower
                    {
                        public override string Portrait => "MonkeyIcons[GwendolinIcon]";
                        public override string Name => "Pyromanic Minion Level 4";
                        public override TowerSet TowerSet => TowerSet.Support;
                        public override string BaseTower => "Gwendolin 15";


                        public override bool DontAddToShop => true;
                        public override int Cost => 0;

                        public override int TopPathUpgrades => 0;
                        public override int MiddlePathUpgrades => 0;
                        public override int BottomPathUpgrades => 0;


                        public override string DisplayName => "Pyromanic Minion Level 4";
                        public override string Description => "Just stop at this point!!!";

                        public override void ModifyBaseTowerModel(TowerModel towerModel)
                        {
                            var attackModel = towerModel.GetBehavior<AttackModel>();
                            towerModel.isSubTower = true;
                            towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
                            attackModel.range = 75;
                            towerModel.range = 75;
                            var projectile = attackModel.weapons[0].projectile;
                            projectile.GetDamageModel().damage += 50;

                        }
                        public class PYML4Display : ModTowerDisplay<PYML4>
                        {
                            public override float Scale => 1.5f;
                            public override string BaseDisplay => GetDisplay(TowerType.Gwendolin);

                            public override bool UseForTower(int[] tiers)
                            {
                                return true;
                            }
                            public override void ModifyDisplayNode(UnityDisplayNode node)
                            {

                            }
                        }

                    }
                    namespace PYM5
                    {
                        public class PYML5 : ModTower
                        {
                            public override string Portrait => "MonkeyIcons[GwendolinIcon]";
                            public override string Name => "Pyromanic Minion Level 5";
                            public override TowerSet TowerSet => TowerSet.Support;
                            public override string BaseTower => "Gwendolin 20";

                            public override bool DontAddToShop => true;
                            public override int Cost => 0;

                            public override int TopPathUpgrades => 0;
                            public override int MiddlePathUpgrades => 0;
                            public override int BottomPathUpgrades => 0;


                            public override string DisplayName => "Pyromanic Minion Level 5";
                            public override string Description => "Yeah you did it?";

                            public override void ModifyBaseTowerModel(TowerModel towerModel)
                            {
                                var attackModel = towerModel.GetBehavior<AttackModel>();
                                towerModel.isSubTower = true;
                                towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 70f, 3, false, false));
                                attackModel.weapons[0].Rate = 0.2f;
                                attackModel.range = 100;
                                towerModel.range = 100;
                                var projectile = attackModel.weapons[0].projectile;
                                projectile.GetDamageModel().damage += 100;


                            }
                            public class PYML5Display : ModTowerDisplay<PYML5>
                            {
                                public override float Scale => 2f;
                                public override string BaseDisplay => GetDisplay(TowerType.Gwendolin);

                                public override bool UseForTower(int[] tiers)
                                {
                                    return true;
                                }
                                public override void ModifyDisplayNode(UnityDisplayNode node)
                                {

                                }
                            }

                        }

                        [HarmonyLib.HarmonyPatch(typeof(Il2CppAssets.Scripts.Unity.UI_New.InGame.InGame), "Update")]
                        public class Update_Patch
                        {
                            [HarmonyLib.HarmonyPostfix]
                            public static void Postfix()
                            {
                            }
                        }
                    }
                }
            }
        }
    }
}
