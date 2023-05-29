
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace ProiectWebService.Dtos { 
    public class ShoppingCartDto
    {
        public int BookId { get; set; }
        public int ItemQuantity { get; set; }      
        public float FinalAmount { get; set; }       
        public int UserId { get; set; }
    }
}
