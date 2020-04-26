﻿using System.Collections.Generic;
using System.Collections.Immutable;
using NodaTime;
using TreniniDotNet.Common.DeliveryDates;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class CatalogItems
    {
        private readonly static ICatalogItemsFactory factory =
            new CatalogItemsFactory(SystemClock.Instance, new GuidSource());

        private readonly ICatalogItem _acme_60458;
        private readonly ICatalogItem _acme_60392;
        private readonly ICatalogItem _rivarossi_HR4298;
        private readonly ICatalogItem _roco_62182;
        private readonly ICatalogItem _Bemo_1252125;
        private readonly ICatalogItem _Bemo_1254134;
        private readonly ICatalogItem _Acme_999999;

        private readonly IList<ICatalogItem> _all;

        internal CatalogItems()
        {
            _acme_60392 = Build_Acme_60392();
            _acme_60458 = Build_Acme_60458();
            _rivarossi_HR4298 = Build_Rivarossi_HR4298();
            _roco_62182 = Build_Roco_62182();
            _Bemo_1252125 = Build_Bemo_1252125();
            _Bemo_1254134 = Build_Bemo_1254134();
            _Acme_999999 = Build_Acme_999999();

            _all = new List<ICatalogItem>()
            {
                _acme_60458,
                _acme_60392,
                _rivarossi_HR4298,
                _roco_62182,
                _Bemo_1252125,
                _Bemo_1254134,
                _Acme_999999
            };
        }

        public IList<ICatalogItem> All() => _all;

        public ICatalogItem Acme_60458() => _acme_60458;
        public ICatalogItem Acme_60392() => _acme_60392;
        public ICatalogItem Rivarossi_HR4298() => _rivarossi_HR4298;
        public ICatalogItem Roco_62182() => _roco_62182;
        public ICatalogItem Bemo_1252125() => _Bemo_1252125;
        public ICatalogItem Bemo_1254134() => _Bemo_1254134;
        public ICatalogItem Acme_999999() => _Acme_999999;

        private static ICatalogItem Build_Acme_60392()
        {
            var rs = factory.NewLocomotive(
                CatalogSeedData.Railways.Fs(),
                Era.IV.ToString(),
                Category.ElectricLocomotive.ToString(),
                LengthOverBuffer.OfMillimeters(210M),
                "E 656",
                "E 656 291",
                DccInterface.Nem652.ToString(),
                Control.DccReady.ToString());

            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Acme(),
                new ItemNumber("60392"),
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC,
                ImmutableList.Create<IRollingStock>(rs),
                @"FS Locomotiva elettrica E.656.291 (terza serie). Livrea d’origine con smorzatori",
                null, null,
                DeliveryDate.FourthQuarterOf(2020),
                false);
        }

        private static ICatalogItem Build_Acme_60458()
        {
            var rs = factory.NewLocomotive(
                CatalogSeedData.Railways.Fs(),
                Era.IV.ToString(),
                Category.ElectricLocomotive.ToString(),
                LengthOverBuffer.OfMillimeters(210M),
                "E 636",
                "E 636 117",
                DccInterface.Nem652.ToString(),
                Control.DccReady.ToString());

            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Acme(),
                new ItemNumber("60458"),
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC,
                ImmutableList.Create<IRollingStock>(rs),
                @"FS Locomotiva elettrica E 636 117 nella livrea storica blu orientale e grigio 
                perla con vomere giallo, logo e scritta Trenitalia, nella fase di fine esercizio",
                null, null,
                DeliveryDate.FourthQuarterOf(2020),
                false);
        }

        private static ICatalogItem Build_Rivarossi_HR4298()
        {
            var rs = factory.NewRollingStock(
                CatalogSeedData.Railways.Fs(),
                Era.IV.ToString(),
                Category.PassengerCar.ToString(),
                LengthOverBuffer.OfMillimeters(195M),
                "Corbellini");

            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Rivarossi(),
                new ItemNumber("HR4298"),
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC,
                ImmutableList.Create<IRollingStock>(rs, rs),
                @"FS set 2 carrozze a due assi tipo ''Corbellini'' livrea grigio ardesia di 2 cl.",
                null, null,
                DeliveryDate.FirstQuarterOf(2020),
                true);
        }

        private static ICatalogItem Build_Roco_62182()
        {
            var rs = factory.NewLocomotive(
                CatalogSeedData.Railways.DR(),
                Era.III.ToString(),
                Category.SteamLocomotive.ToString(),
                LengthOverBuffer.OfMillimeters(254M),
                "BR 50.40",
                null,
                DccInterface.Next18.ToString(),
                Control.DccReady.ToString());

            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Roco(),
                new ItemNumber("62182"),
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC,
                ImmutableList.Create(rs),
                @"Steam locomotive BR 50.40 of the Deutsche Reichsbahn.",
                null, null,
                DeliveryDate.FirstQuarterOf(2020),
                true);
        }

        private static ICatalogItem Build_Bemo_1252125()
        {
            var rs = factory.NewLocomotive(
                CatalogSeedData.Railways.RhB(),
                Era.V.ToString(),
                Category.ElectricLocomotive.ToString(),
                LengthOverBuffer.OfMillimeters(139M),
                "Ge 4/4 I",
                "Ge 4/4 I 605",
                DccInterface.Mtc21.ToString(),
                Control.DccReady.ToString());

            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Bemo(),
                new ItemNumber("1252125"),
                CatalogSeedData.Scales.ScaleH0m(),
                PowerMethod.DC,
                ImmutableList.Create(rs),
                @"Reissue of the Ge 4/4 I 601-610 in the conversion version after the 
                modernization of the prototype, now with revised chassis.",
                null, null,
                DeliveryDate.FirstQuarterOf(2020),
                true);
        }

        private static ICatalogItem Build_Bemo_1254134()
        {
            var rs = factory.NewLocomotive(
                CatalogSeedData.Railways.RhB(),
                Era.V.ToString(),
                Category.ElectricLocomotive.ToString(),
                LengthOverBuffer.OfMillimeters(166.7M),
                "Ge 6/6 II",
                "Ge 6/6 II 704 Davos",
                DccInterface.Nem651.ToString(),
                Control.DccReady.ToString());

            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Bemo(),
                new ItemNumber("1254134"),
                CatalogSeedData.Scales.ScaleH0m(),
                PowerMethod.DC,
                ImmutableList.Create(rs),
                @"Electric Locomotive Ge6/6 II of the RhB",
                null, null,
                DeliveryDate.FirstQuarterOf(2020),
                true);
        }

        private static ICatalogItem Build_Acme_999999()
        {
            return factory.CreateNewCatalogItem(
                CatalogSeedData.Brands.Acme(),
                new ItemNumber("999999"),
                CatalogSeedData.Scales.ScaleH0(),
                PowerMethod.DC,
                ImmutableList<IRollingStock>.Empty,
                "Catalog item - empty",
                null, null,
                DeliveryDate.FirstQuarterOf(2020),
                true);
        }
    }

    public static class ICatalogItemsRepositoryExtensions
    {
        public static void SeedDatabase(this ICatalogItemRepository repo)
        {
            var catalogItems = CatalogSeedData.CatalogItems.All();
            foreach (var item in catalogItems)
            {
                repo.AddAsync(item);
            }
        }
    }
}
