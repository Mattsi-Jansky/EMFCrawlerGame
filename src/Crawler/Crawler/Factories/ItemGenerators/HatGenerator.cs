using System;
using System.Collections.Generic;
using Crawler.Models;
using Crawler.Queryables.Entities;
using Crawler.Queryables.Entities.Components;

namespace Crawler.Factories.ItemGenerators
{
    public class HatGenerator : IItemGenerator
    {
        private readonly Random _random;
        
        private readonly List<HatModel> models = new List<HatModel>()
        {
            new HatModel("Top Hat", Graphic.Tophat, "Sir", "The Dapper"),
            new HatModel("Stetson", Graphic.CowboyHat, "Sheriff", ""),
            new HatModel("Baseball Cap", Graphic.BaseballCap, "", ""),
            new HatModel("Pyramid Hat", Graphic.PyramidHat, "", ""),
            new HatModel("Brown Cap", Graphic.BrownCap, "", ""),
            new HatModel("Winter Hat", Graphic.GreenWinterHat, "", ""),
            new HatModel("Stocking Cap", Graphic.XmasHat, "Saint", "The Jolly"),
            new HatModel("Deerstalker Hat", Graphic.HolmesHat, "Detective", ""),
            new HatModel("Mitre Hat", Graphic.BishopHat, "", ""),
            new HatModel("Pink Hat", Graphic.PinkHat, "", "The Fancy"),
            new HatModel("Green Cap", Graphic.RobinHoodHat, "", "The Brave"),
            new HatModel("Winter Hat", Graphic.BrownCap, "", ""),
            new HatModel("Flat Cap", Graphic.ConductorHat, "", ""),
            new HatModel("Custodian Helmet", Graphic.BobbyHat, "P.C.", ""),
            new HatModel("Crown", Graphic.KingHat, "King", ""),
            new HatModel("Baseball Cap", Graphic.BackwardsBaseballCap, "", ""),
            new HatModel("Flat Cap", Graphic.ChefsHat, "", ""),
            new HatModel("Winged Hat", Graphic.WingedHat, "", ""),
            new HatModel("Green Hat", Graphic.GreenHat, "", ""),
            new HatModel("Yellow Hat", Graphic.YellowStanHat, "", ""),
            new HatModel("Pink Hat", Graphic.PinkStanHat, "", ""),
            new HatModel("Black Hat", Graphic.GrayStanHat, "", ""),
        };

        public HatGenerator(Random random)
        {
            _random = random;
        }

        public Entity Generate()
        {
            var entity = new Entity();
            var model = models[_random.Next(models.Count)];
            var equipmentComponent = new EquipableComponent(EquipableSlot.Hat);
            
            equipmentComponent.Add(new GraphicComponent(model.Graphic));
            equipmentComponent.Add(new PositionComponent());
            equipmentComponent.Add(new NameComponent(model.Name, model.PreTitle, model.PostTitle));
            entity.Add(equipmentComponent);

            return entity;
        }
    }
}