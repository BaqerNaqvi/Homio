using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppLab_Test
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Fee { get; set; }
        public bool Status { get; set; }
        public int Interval { get; set; }
        public string CreationDate { get; set; }

        public List<AppLab_mapping> Lab_Mapping { get; set; }

    }

    public static class LabTestMapper
    {
        public static AppLab_Test Mapper(this Lab_Tests source)
        {
            List<AppLab_mapping> mapping = null;
            if (source.Lab_Mapping != null && source.Lab_Mapping.Any())
            {
                mapping = source.Lab_Mapping.ToList().Select(map => map.Mapper()).ToList();
            }
            return new AppLab_Test
            {
                Id = source.Id,
                Name = source.Name,
                Status = source.Status,
                Fee = source.Fee,
                CreationDate = source.CreationDate.ToShortDateString(),
                Interval = source.Interval,
                Lab_Mapping = mapping
            };
        }
    }
}
