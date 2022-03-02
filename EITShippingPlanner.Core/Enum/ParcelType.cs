using System;
using System.Collections.Generic;
using System.Text;

namespace EITShippingPlanner.Core.Enum
{
    public class ParcelType
    {
        public ParcelType(string name, ParcelTypeEnum @enum)
        {
            Name = name;
            Enum = @enum;
        }

        public string Name { get; set; }
        public ParcelTypeEnum Enum { get; set; }
        
        public enum ParcelTypeEnum
        {
            RecordedDelivery = 0,
            Weapons,
            LiveAnimals,
            CautiousParcels,
            RefrigeratedGoods
        }

        public static ParcelType RecordedDelivery = new ParcelType("Recorded Delivery", ParcelTypeEnum.RecordedDelivery);

        public static ParcelType Weapons = new ParcelType("Weapons", ParcelTypeEnum.Weapons);

        public static ParcelType LiveAnimals = new ParcelType("Live Animals", ParcelTypeEnum.LiveAnimals);

        public static ParcelType CautiousParcels = new ParcelType("Cautious Parcels", ParcelTypeEnum.CautiousParcels);

        public static ParcelType RefrigeratedGoods = new ParcelType("Refrigerated Goods", ParcelTypeEnum.RefrigeratedGoods);
    }
}
