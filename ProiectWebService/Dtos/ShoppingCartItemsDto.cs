using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace ProiectWebService.Dtos
{
    public class ShoppingCartItemsDto
    {
        [System.Text.Json.Serialization.JsonIgnore]
       // [Newtonsoft.Json.JsonIgnore]
        [IgnoreDataMember]
        public int ShoppingCartId { get; set; }
        public int BookId { get; set; }
        [Ignore]
        public int ShoppingCartItemsId { get; set; }
        public int Quantity { get; set; }
        [Ignore]
        public float FinalAmount { get; set; }
    }
}
