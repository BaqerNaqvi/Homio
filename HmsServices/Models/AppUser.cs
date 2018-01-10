﻿using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppUserDoc
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int  Fee { get; set; }

        public bool Status { get; set; }

        public string CreationDateTime { get; set; }

        public string Type { get; set; }
        public string Title { get; set; }
        public string Degree { get; set; }
        public string ShiftFrom { get; set; }
        public string ShifTo { get; set; }
        public string ShiftDays { get; set; }

        public string PMDCNo { get; set; }
    }

    public class AppUserDoc_Dd
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Fee { get; set; }

    }

 

    public static class UserMapper
    {
        public static AppUserDoc MapToDoc(this AspNetUser source)
        {
            return new AppUserDoc
            {
                Id = source.Id,
                Fee = source.Fee,
                FirstName = source.FirstName,
                LastName = source.LastName ,
                Email = source.Email,
                Status = source.Status,
                CreationDateTime = source.CreationTime.ToLongDateString(),
                Degree = source.Degree,
                ShiftFrom = source.ShiftFrom,
                ShifTo = source.ShiftToo,
                ShiftDays = source.ShiftDays,
                Title = source.Title,
                Type = source.Type,
                PMDCNo = source.PMDCNo
            };
        }

        public static AppUserDoc_Dd MapToDocDd(this AspNetUser source)
        {
            return new AppUserDoc_Dd
            {
                Id = source.Id,
                Fee = source.Fee,
                Name = source.FirstName+" "+source.LastName + " (" + source.Degree + ")",
            };
        }
    }
}