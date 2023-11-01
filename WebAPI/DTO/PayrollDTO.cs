﻿namespace WebAPI.DTO
{
    public class PayrollDTO
    {
        public int PayrollId { get; set; }

        public int HourlyRate { get; set; }

        public int GrossSalary { get; set; }

        public int NetSalary { get; set; }

        public DateOnly EffectiveDate { get; set; }

        public DateOnly LastChanged { get; set; }
    }
}
