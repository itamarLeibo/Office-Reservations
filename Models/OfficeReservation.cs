namespace InterviewAssignment.Models
{
    public class OfficeReservation
    {
        public int Capacity { get; set; }
        public int MonthlyPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
