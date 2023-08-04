using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodilityTests
{
    /// <summary>
    /// Imagine that you are responsible for a fleet of electric mail trucks. You need to get all your trucks recharged overnight so they are ready for the next day. Please write code to come up with a schedule for charging your trucks. 
    /// You have:
    ///
    /// Some number of trucks, each with a unique ID, battery capacity(in kilowatt hours), and current level of charge
    /// Some number of chargers, each with a unique ID and charging rate(in kilowatts)
    /// A specified amount of time(an integer number of hours)
    /// 
    /// Your goal is to get as many trucks as possible charged to full capacity in the specified amount of time.
    /// 
    /// You should make the following assumptions:
    /// 
    /// Each charger can charge one truck at a time
    /// Once a truck starts charging, it must continue until it is fully charged
    /// Once a truck is done charging, it takes zero seconds before the next truck can start charging on the same charger
    /// 
    /// Your task is to write code that prints a schedule indicating which trucks should be charged on each charger.
    /// 
    /// For example, if your code prints:
    /// 
    /// 1: 1, 3
    /// 2: 4
    /// 
    /// That means that trucks 1 and 3 should be charged on charger 1 and truck 4 should be charged on charger 2. Truck 2 will not be charged by the end of the time period.
    /// </summary>
    [TestClass]
    public class ChargingVehicles
    {
        public static Dictionary<int, List<int>> EvaluateChargingRoster(
            List<Vehicle> vehicles,
            List<Charger> chargers,
            int chargingDuration)
        {
            var chargerQueues = chargers.ToDictionary(
                c => c.Id,
                c => new ChargerQueue
                {
                    Charger = c,
                    RemainingDuration = chargingDuration,
                    TotalCapacityAssigned = 0.0d
                });

            var orderedVehicles = vehicles.OrderBy(v => v.BatteryCapacity);
            var orderedChargers = chargers.OrderByDescending(c => c.Rate);

            foreach(var vehicle in orderedVehicles)
            {
                foreach(var charger in chargers)
                {
                    var queue = chargerQueues[charger.Id];
                    if (queue.CanChargeWithinDuration(vehicle))
                        queue.Add(vehicle);
                }
            }

            return chargerQueues.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Vehicles.Select(v => v.Id).ToList());
        }
    }

    public class ChargerQueue
    {
        public Charger Charger { get; set; }
        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();
        public int RemainingDuration { get; set; }
        public double TotalCapacityAssigned { get; set; }

        public bool CanChargeWithinDuration(Vehicle vehicle)
        {
            var currentLevel = (vehicle.LevelPercentage * vehicle.BatteryCapacity) / 100;
            var timeToCharge = (int)Math.Ceiling((vehicle.BatteryCapacity - currentLevel) / Charger.Rate);
            return RemainingDuration >= timeToCharge;
        }

        public void Add(Vehicle vehicle)
        {
            var currentLevel = (vehicle.LevelPercentage * vehicle.BatteryCapacity) / 100;
            var timeToCharge = (int) Math.Ceiling((vehicle.BatteryCapacity - currentLevel) / Charger.Rate);
            RemainingDuration -=  timeToCharge;
            Vehicles.Add(vehicle);
        }

        public bool TryAdd(Vehicle vehicle)
        {
            var currentLevel = (vehicle.LevelPercentage * vehicle.BatteryCapacity) / 100;
            var timeToCharge = (int)Math.Ceiling((vehicle.BatteryCapacity - currentLevel) / Charger.Rate);
            if (RemainingDuration >= timeToCharge)
            {
                RemainingDuration -= timeToCharge;
                Vehicles.Add(vehicle);
                return true;
            }
            return false;
        }
    }

    public class Charger: IComparable<Charger>
    {
        public double Rate { get; }

        public int Id { get; }

        public Charger(int id, double rate)
        {
            Rate = rate;
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Charger other
                && other.Rate == Rate
                && other.Id == Id;
        }

        public override int GetHashCode() => HashCode.Combine(Id, Rate);

        public int CompareTo(Charger other) => Rate.CompareTo(other.Rate);

        public static bool operator ==(Charger first, Charger second)
        {
            if (first == null && second == null)
                return true;

            else return first?.Equals(second) == true;
        }

        public static bool operator !=(Charger first, Charger second) => !(first == second);
    }

    public record Vehicle(int Id, double BatteryCapacity, double LevelPercentage);
}
