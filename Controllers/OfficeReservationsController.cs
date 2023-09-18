using InterviewAssignment.Data;
using InterviewAssignment.Interfaces;
using InterviewAssignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace InterviewAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeReservationsController : ControllerBase
    {
        private IReservationRepository _reservationRepository;
        private static CsvDataTable _csvTable  = new CsvDataTable();
        private DataTable? dt = _csvTable.getDT();

        public OfficeReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = new ReservationRepository(dt); ;
        }

        [HttpGet("GetRevenueOfMonth")]
        public ActionResult GetRevenueOfMonth(
             [FromQuery(Name = "targetMonth")] DateTime targetMonth
            )
        {
            try
            {
                var res = _reservationRepository.GetRevenueOfMonth(targetMonth);
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("GetTotalUnreservedOffices")]
        public ActionResult GetTotalUnreservedOffices(
            [FromQuery(Name = "targetMonth")] DateTime targetMonth
            )
        {
            try
            {
                var res = _reservationRepository.GetTotalUnreservedOffices(targetMonth);
                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }


}
