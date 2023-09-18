using InterviewAssignment.Data;
using InterviewAssignment.Interfaces;
using Microsoft.VisualBasic;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Data;

namespace InterviewAssignment.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        public DataTable? reservationsTable { get; set; }

        public ReservationRepository(DataTable db)
        {

            reservationsTable = db;
        }

        public string? GetRevenueOfMonth(DateTime targetMonth)
        {
            if (reservationsTable == null)
            {

                Console.WriteLine("DataTable is null");
                return null;
            }

            if (reservationsTable.Rows.Count == 0)
            {
                Console.WriteLine("DataTable is null");
                return null;
            }

            double totalRevenue = 0;

            foreach (DataRow row in reservationsTable.Rows)
            {
                int monthlyPrice = int.Parse((string)row.ItemArray[1]);
                string startString = (string)row.ItemArray[2];
                string[] startArr = startString.Split("-");
                DateTime start = new DateTime(int.Parse(startArr[0]), int.Parse(startArr[1]), int.Parse(startArr[2]));

                string endString = (string)row.ItemArray[3];
                string[] endArr = endString.Split("-");
                DateTime end;
                if (endString == "")
                    end = DateTime.MaxValue;
                else
                    end = new DateTime(int.Parse(endArr[0]), int.Parse(endArr[1]), int.Parse(endArr[2]));

                int daysInMonth = DateTime.DaysInMonth(targetMonth.Year, targetMonth.Month);

                int daysReserved = 0;
                double pricePerDay = (double)monthlyPrice / daysInMonth;
                if (start <= targetMonth && targetMonth <= end)
                {
                    if (start.Year == targetMonth.Year && start.Month == targetMonth.Month &&
                        end.Year == targetMonth.Year && end.Month == targetMonth.Month)
                    {//middle part of the month
                        daysReserved = end.Day - start.Day + 1;
                    }
                    else if (start.Year == targetMonth.Year && start.Month == targetMonth.Month)
                    { //last part of the month
                        daysReserved = daysInMonth - start.Day + 1;
                    }
                    else if (end.Year == targetMonth.Year && end.Month == targetMonth.Month) //last part of a month
                    {//first part of a month
                        daysReserved = end.Day;
                    }
                    else
                    {//whole month
                        daysReserved = daysInMonth;
                    }
                }
                double officeRevenue = pricePerDay * daysReserved;
                totalRevenue += officeRevenue;

            }

            return totalRevenue.ToJson();
        }


        public string? GetTotalUnreservedOffices(DateTime targetMonth)
        {
            if (reservationsTable == null)
            {
                Console.WriteLine("DataTable is null");
                return null;
            }

            if (reservationsTable.Rows.Count == 0)
            {
                Console.WriteLine("DataTable is null");
                return null;
            }

            double totalUnreserved = 0;

            foreach (DataRow row in reservationsTable.Rows)
            {
                int capacity = int.Parse((string)row.ItemArray[0]);
                string startString = (string)row.ItemArray[2];
                string[] startArr = startString.Split("-");
                DateTime start = new DateTime(int.Parse(startArr[0]), int.Parse(startArr[1]), int.Parse(startArr[2]));

                string endString = (string)row.ItemArray[3];
                string[] endArr = endString.Split("-");
                DateTime end;
                if (endString == "")
                    end = DateTime.MaxValue;
                else
                    end = new DateTime(int.Parse(endArr[0]), int.Parse(endArr[1]), int.Parse(endArr[2]));

                if (start > targetMonth || targetMonth > end)
                {
                    totalUnreserved += capacity;
                }
                
            }
            return totalUnreserved.ToJson();
        }
    }
}
