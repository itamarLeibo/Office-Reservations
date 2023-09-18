using System.Data;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using InterviewAssignment.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using InterviewAssignment.Repositories;

namespace InterviewAssignment.Data
{

    class CsvDataTable : DbContext
    {
        string CsvFilePath { get; set; } = "";
        public DataTable? dt { get; set; } = null;
        public DbSet<OfficeReservation> CsvDataTables { get; set; }
        public CsvDataTable()
        {
            string CsvFilePath = @"rent_data.csv";
            using (var reader = new StreamReader(CsvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                using (var dr = new CsvDataReader(csv))
                {
                    dt = new DataTable();
                    dt.Load(dr);

                    Console.WriteLine("Total Columns: {0}", dt.Columns.Count);
                    Console.WriteLine("Total Rows: {0}", dt.Rows.Count);
                }
            }

            
        }

        public DataTable? getDT() { return dt; }

    }
}





