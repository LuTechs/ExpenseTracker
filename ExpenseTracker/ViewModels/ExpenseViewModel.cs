using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}