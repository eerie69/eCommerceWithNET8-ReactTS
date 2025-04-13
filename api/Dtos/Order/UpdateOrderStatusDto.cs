using api.Constants;
using DemoShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DemoShop.Dtos.Order
{
    public class UpdateOrderStatusDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int OrderStatusId { get; set; }

    }
}
