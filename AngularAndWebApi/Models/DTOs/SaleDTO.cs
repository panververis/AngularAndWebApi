using System;

namespace AngularAndWebApi.Models.DTOs
{
    public class SaleDTO
    {
        public int      ID         { get; set; }
        public DateTime SaleDate   { get; set; }
        public decimal  SaleValue  { get; set; }
    }
}