﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppLab_Parm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string NormarVal { get; set; }
        public bool Status { get; set; }

        public  List<AppLab_mapping> Lab_Mappings { get; set; }

    }

    public static class LabParmMapper
    {
        public static AppLab_Parm Mapper(this Lab_Parms source)
        {
            List<AppLab_mapping> mapping = null;
            if (source.Lab_Mapping != null && source.Lab_Mapping.Any())
            {
                mapping = source.Lab_Mapping.Select(map => map.Mapper()).ToList();
            }
            return new AppLab_Parm
            {
                Id = source.Id,
                Name = source.Name,
                Status = source.Status,
              NormarVal = source.NormarVal,
              Unit = source.Unit,
              Lab_Mappings = mapping
            };
        }
    }
}