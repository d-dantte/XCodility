using System;
using System.Collections.Generic;
using System.Text;

namespace Codility.Challenges
{
    public class InsurancePremiumCalculator
    {

        public static readonly decimal FlatRate = 1000;

        /// <summary>
        /// Calculate the premium given the employee info and pricing models.
        /// </summary>
        /// <param name="info">Employee information</param>
        /// <param name="policyPeriod">Policy period</param>
        /// <param name="flatrateModel">Flate rate calculation model</param>
        /// <param name="prorateModel">Prorate calculation model</param>
        /// <returns></returns>
        public (string, decimal, decimal) CalculatePremium(
            EmployeeInsuranceInfo info,
            Period policyPeriod,
            FlatRatePricimgModel flatrateModel,
            ProratePricingModel prorateModel)
        {
            // this is a switch expression: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression
            var fullPremium = flatrateModel switch
            {
                FlatRatePricimgModel.FlatRate => FlatRate,

                FlatRatePricimgModel.Age => CalculateAgeRate(info.Age),

                FlatRatePricimgModel.GenderedAge => CalculateGenderedRate(info.Sex, info.Age),

                _ => throw new Exception($"Invalid flat-rate model: {flatrateModel}")
            };

            // switch expression again.
            var prorated = prorateModel switch
            {
                ProratePricingModel.DayProrated => CalculateDayProrated(fullPremium, policyPeriod, info.JoinDate),

                ProratePricingModel.MonthProrated => CalculateMonthProrated(fullPremium, policyPeriod, info.JoinDate),

                _ => throw new Exception($"Invalid prorated model: {prorateModel}")
            };

            return (info.ID, fullPremium, prorated);
        }

        private decimal CalculateAgeRate(int age) => ((age / 10) + 1) * 100m;

        private decimal CalculateGenderedRate(Sex sex, int age)
        {
            var ageRate = CalculateAgeRate(age);
            return sex == Sex.Female && age >= 18
                ? ageRate * 1.5m
                : ageRate;
        }

        private decimal CalculateDayProrated(decimal fullPremium, Period policyPeriod, DateTimeOffset joinDate)
        {
            ValidateInsuranceParameters(policyPeriod, joinDate);
            return (fullPremium / 366) * new Period(joinDate, policyPeriod.End).InclusiveDayCount;
        }

        private decimal CalculateMonthProrated(decimal fullPremium, Period policyPeriod, DateTimeOffset joinDate)
        {
            ValidateInsuranceParameters(policyPeriod, joinDate);

            return (fullPremium / 12) * new Period(joinDate, policyPeriod.End).InclusiveMonthCount;
        }

        private void ValidateInsuranceParameters(Period policyPeriod, DateTimeOffset memberJoinDate)
        {
            if (memberJoinDate < policyPeriod.Start
                || memberJoinDate > policyPeriod.End)
                throw new Exception($"Member Join-Date is outside policy period");
        }


        #region Nested types
        /// <summary>
        /// Enum identifying different flat-rate calculation models
        /// </summary>
        public enum FlatRatePricimgModel
        {
            FlatRate,
            Age,
            GenderedAge
        }

        /// <summary>
        /// Enum identifying different prorated calculation models
        /// </summary>
        public enum ProratePricingModel
        {
            MonthProrated,
            DayProrated
        }

        public enum Sex
        {
            Male,
            Female
        }

        public struct EmployeeInsuranceInfo
        {
            public Sex Sex { get; }

            public DateTimeOffset DateOfBirth { get; }

            public int Age
            {
                get
                {
                    var now = DateTimeOffset.Now;
                    if (DateOfBirth > now)
                        throw new InvalidOperationException("Invalid DOB");

                    return now.Year - DateOfBirth.Year - (now.Month < DateOfBirth.Month ? 1 : 0);
                }
            }

            public DateTimeOffset JoinDate { get; }

            public string ID { get; }


            public EmployeeInsuranceInfo(string id, Sex sex, DateTimeOffset dob, DateTimeOffset joined)
            {
                ID = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException("Invalid id");
                Sex = sex;
                JoinDate = joined;
                DateOfBirth = dob;
            }
        }

        public struct Period
        {
            public DateTimeOffset Start { get; }

            public DateTimeOffset End { get; }

            public int InclusiveMonthCount => (End.Month + (12 * (End.Year - Start.Year))) - Start.Month + 1;

            public int InclusiveDayCount => (End - Start).Days + 1;

            public Period(DateTimeOffset start, DateTimeOffset end)
            {
                if (end <= start)
                    throw new ArgumentException("Invalid end date");

                Start = start;
                End = end;
            }

            public Period(DateTimeOffset start)
            {
                Start = start;
                End = start.AddYears(1);
            }
        }
        #endregion
    }
}
