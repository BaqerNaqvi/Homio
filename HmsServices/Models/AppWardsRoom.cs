using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsServices.DbContext;

namespace HmsServices.Models
{
    public class AppWardsRoom
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Parent { get; set; }
        public DateTime? Created { get; set; }
        public string Code { get; set; }
        public bool Booked { get; set; }
    }

    public static class WardsMapper
    {
        public static AppWardsRoom MappAppWardsRoom(this WardsRoom source)
        {
            return new AppWardsRoom
            {
                Name = source.Name,
                Id = source.Id,
                Code = source.Code,
                Created = source.Created,
                Parent = source.Parent,
                Booked = source.Booked
            };
        }
    }
}
