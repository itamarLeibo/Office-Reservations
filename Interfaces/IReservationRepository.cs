using System.Data;

namespace InterviewAssignment.Interfaces
{
    public interface IReservationRepository
    {

        public string? GetRevenueOfMonth(DateTime targetMonth);
        public string? GetTotalUnreservedOffices(DateTime targetMonth);
    }
}
