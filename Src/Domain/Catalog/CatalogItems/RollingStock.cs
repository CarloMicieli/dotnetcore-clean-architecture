﻿using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStock : IEquatable<RollingStock>, IRollingStock
    {
        internal RollingStock(
            RollingStockId rollingStockId,
            IRailwayInfo railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            string? className, string? roadNumber, string? typeName,
            PassengerCarType? passengerCarType, ServiceLevel? serviceLevel,
            DccInterface dccInterface, Control control)
        {
            RollingStockId = rollingStockId;
            Railway = railway;
            Category = category;
            Epoch = epoch;
            Length = length;
            ClassName = className;
            RoadNumber = roadNumber;
            TypeName = typeName;
            ServiceLevel = serviceLevel;
            PassengerCarType = passengerCarType;
            DccInterface = dccInterface;
            Control = control;
        }

        #region [ Properties ]
        public RollingStockId RollingStockId { get; }

        public IRailwayInfo Railway { get; }

        public Category Category { get; }

        public Epoch Epoch { get; }

        public LengthOverBuffer? Length { get; }

        public string? ClassName { get; }

        public string? RoadNumber { get; }

        public string? TypeName { get; }

        public PassengerCarType? PassengerCarType { get; }

        public ServiceLevel? ServiceLevel { get; }

        public Control Control { get; }

        public DccInterface DccInterface { get; }
        #endregion

        public static bool operator ==(RollingStock left, RollingStock right) => AreEquals(left, right);

        public static bool operator !=(RollingStock left, RollingStock right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is RollingStock other)
            {
                return AreEquals(this, other);
            }

            return false;
        }

        public bool Equals(RollingStock other) => AreEquals(this, other);

        private static bool AreEquals(RollingStock left, RollingStock right) =>
            left.RollingStockId == right.RollingStockId;

        public override int GetHashCode() => HashCode.Combine(RollingStockId);

        public override string ToString()
        {
            return $"RollingStock({RollingStockId} {Epoch} {Railway.Name} {Category})";
        }
    }
}
